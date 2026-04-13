using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpiritBeastsGalleryController : MonoBehaviour
{
    public static SpiritBeastsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject SpiritBeastBlockButtonPrefab;
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
        SpiritBeastBlockButtonPrefab = UIManager.Instance.Get("SpiritBeastBlockButtonPrefab");
    }
    public void CreateSpiritBeastsGallery(List<SpiritBeasts> spiritBeasts, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.SPIRIT_BEAST_BUTTON_BACKGROUND_URL);

        foreach (var spiritBeast in spiritBeasts)
        {
            try
            {
                GameObject spiritBeastObject = Instantiate(SpiritBeastBlockButtonPrefab, contentPanel);
                Transform transform = spiritBeastObject.transform;

                TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                titleText.text = spiritBeast.Name.Replace("_", " ");

                RawImage image = transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(spiritBeast.Image);
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                image.texture = texture;

                // Kích thước của RawImage (khung hiển thị)
                RectTransform rect = image.GetComponent<RectTransform>();
                float maxWidth = rect.rect.width;
                float maxHeight = rect.rect.height;

                // Kích thước thật của texture
                float texWidth = texture.width;
                float texHeight = texture.height;

                // Tính scale để texture nằm gọn trong khung
                float widthRatio = maxWidth / texWidth;
                float heightRatio = maxHeight / texHeight;
                float finalScale = Mathf.Min(widthRatio, heightRatio);  // scale nhỏ nhất

                // Áp dụng scale theo tỉ lệ đúng
                image.SetNativeSize();
                image.transform.localScale = new Vector3(finalScale, finalScale, 1f);

                RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = bgTexture;

                Button button = transform.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(spiritBeast, MainPanel);
                });

                TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.HexToColor(QualityEvaluator.CheckRareColor(spiritBeast.Rare));
                rareText.text = spiritBeast.Rare;

                RawImage blockImage = transform.Find("Block").GetComponent<RawImage>();
                Button unlockButton = transform.Find("UnlockButton").GetComponent<Button>();
                if (spiritBeast.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    blockImage.gameObject.SetActive(false);
                    unlockButton.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (spiritBeast.Status.Equals(AppConstants.Status.PENDING))
                {
                    blockImage.gameObject.SetActive(true);
                    unlockButton.gameObject.SetActive(true);
                }
                else if (spiritBeast.Status.Equals(AppConstants.Status.BLOCK))
                {
                    blockImage.gameObject.SetActive(true);
                    unlockButton.gameObject.SetActive(false);
                }

                unlockButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var spiritBeastGalleryService = SpiritBeastsGalleryService.Create();
                    await spiritBeastGalleryService.UpdateStatusSpiritBeastGalleryAsync(spiritBeast.Id);
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
                if ((spiritBeast.CurrentStar < spiritBeast.TempStar) && spiritBeast.Status.Equals(AppConstants.Status.AVAILABLE))
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
                    await TitlesGalleryService.Create().UpdateTitleGalleryPowerAsync(spiritBeast.Id);
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
