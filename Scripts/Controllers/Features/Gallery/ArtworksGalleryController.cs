using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtworksGalleryController : MonoBehaviour
{
    public static ArtworksGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ArtworkBlockButtonPrefab;
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
        ArtworkBlockButtonPrefab = UIManager.Instance.Get("ArtworkBlockButtonPrefab");
    }
    public void CreateArtworksGallery(List<Artworks> artworks, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.ARTWORK_BUTTON_BACKGROUND_URL);

        foreach (var artwork in artworks)
        {
            try
            {
                GameObject artworkObject = Instantiate(ArtworkBlockButtonPrefab, contentPanel);
                Transform transform = artworkObject.transform;

                TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                titleText.text = artwork.Name.Replace("_", " ");

                RawImage image = transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(artwork.Image);
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                image.texture = texture;

                // Set size 200x130
                RectTransform rect = image.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(200, 130);

                RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = bgTexture;

                // RawImage frameImage = ArtworkObject.transform.Find("FrameImage").GetComponent<RawImage>();
                // frameImage.gameObject.SetActive(true);

                Button button = GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(artwork, MainPanel);
                });

                TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.HexToColor(QualityEvaluator.CheckRareColor(artwork.Rare));
                rareText.text = artwork.Rare;

                RawImage blockImage = transform.Find("Block").GetComponent<RawImage>();
                Button unlockButton = transform.Find("UnlockButton").GetComponent<Button>();
                if (artwork.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    blockImage.gameObject.SetActive(false);
                    unlockButton.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (artwork.Status.Equals(AppConstants.Status.PENDING))
                {
                    blockImage.gameObject.SetActive(true);
                    unlockButton.gameObject.SetActive(true);
                }
                else if (artwork.Status.Equals(AppConstants.Status.BLOCK))
                {
                    blockImage.gameObject.SetActive(true);
                    unlockButton.gameObject.SetActive(false);
                }

                unlockButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var artworkGalleryService = ArtworksGalleryService.Create();
                    await artworkGalleryService.UpdateStatusArtworkGalleryAsync(artwork.Id);
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
                if ((artwork.CurrentStar < artwork.TempStar) && artwork.Status.Equals(AppConstants.Status.AVAILABLE))
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
                    await ArtworksGalleryService.Create().UpdateArtworkGalleryPowerAsync(artwork.Id);
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
