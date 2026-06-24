using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutfitsGalleryController : MonoBehaviour
{
    public static OutfitsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject OutfitBlockButtonPrefab;
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        OutfitBlockButtonPrefab = UIManager.Instance.Get("OutfitBlockButtonPrefab");
    }
    public void CreateOutfitsGallery(List<Outfits> outfits, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.OUTFIT_BUTTON_BACKGROUND_URL);

        foreach (var outfit in outfits)
        {
            try
            {
                GameObject outfitObject = Instantiate(OutfitBlockButtonPrefab, contentPanel);
                Transform transform = outfitObject.transform;

                TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                titleText.text = outfit.Name.Replace("_", " ");

                RawImage image = transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(outfit.Image);
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                image.texture = texture;

                ImageManager.Instance.ChangeSizeImageByTextureScale(image, texture);

                RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = bgTexture;

                Button button = transform.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(outfit, MainPanel);
                });

                TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(outfit.Rarity));
                rareText.text = outfit.Rarity;

                RawImage blockImage = transform.Find("Block").GetComponent<RawImage>();
                Button unlockButton = transform.Find("UnlockButton").GetComponent<Button>();
                if (outfit.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    blockImage.gameObject.SetActive(false);
                    unlockButton.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (outfit.Status.Equals(AppConstants.Status.PENDING))
                {
                    blockImage.gameObject.SetActive(true);
                    unlockButton.gameObject.SetActive(true);
                }
                else if (outfit.Status.Equals(AppConstants.Status.BLOCK))
                {
                    blockImage.gameObject.SetActive(true);
                    unlockButton.gameObject.SetActive(false);
                }

                unlockButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var outfitGalleryService = OutfitsGalleryService.Create();
                    await outfitGalleryService.UpdateStatusOutfitGalleryAsync(outfit.Id);
                    blockImage.gameObject.SetActive(false);
                    unlockButton.gameObject.SetActive(false);
                    image.color = Color.white;

                    var powerManagerService = PowerManagerService.Create();
                    var teamsService = TeamsService.Create();

                    await powerManagerService.UpdateUserStatsAsync(User.CurrentUserId);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                });

                Button upgradeButton = transform.Find("UpgradeButton").GetComponent<Button>();
                if ((outfit.CurrentStar < outfit.TempStar) && outfit.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    upgradeButton.gameObject.SetActive(true);
                }
                else
                {
                    upgradeButton.gameObject.SetActive(false);
                }

                upgradeButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await OutfitsGalleryService.Create().UpdateOutfitGalleryPowerAsync(outfit.Id);
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 240);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
