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
        foreach (var artwork in artworks)
        {
            try
            {
                GameObject artworkObject = Instantiate(ArtworkBlockButtonPrefab, contentPanel);

                TextMeshProUGUI Title = artworkObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                Title.text = artwork.Name.Replace("_", " ");

                RawImage image = artworkObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(artwork.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                image.texture = texture;

                // Set size 200x130
                RectTransform rect = image.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(200, 130);

                RawImage backgroundImage = artworkObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.ARTWORK_BUTTON_BACKGROUND_URL);

                // RawImage frameImage = ArtworkObject.transform.Find("FrameImage").GetComponent<RawImage>();
                // frameImage.gameObject.SetActive(true);

                Button button = artworkObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(artwork, MainPanel);
                });

                RawImage rareImage = artworkObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{artwork.Rare}");
                rareImage.texture = rareTexture;

                RawImage blockImage = artworkObject.transform.Find("Block").GetComponent<RawImage>();
                Button Unlock = artworkObject.transform.Find("UnlockButton").GetComponent<Button>();
                if (artwork.Status.Equals("available"))
                {
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (artwork.Status.Equals("pending"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(true);
                }
                else if (artwork.Status.Equals("block"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(false);
                }

                Unlock.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var artworkGalleryService = ArtworksGalleryService.Create();
                    await artworkGalleryService.UpdateStatusArtworkGalleryAsync(artwork.Id);
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;

                    var powerManagerService = PowerManagerService.Create();
                    var teamsService = TeamsService.Create();

                    await powerManagerService.UpdateUserStatsAsync(User.CurrentUserId);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                });

                Button Upgrade = artworkObject.transform.Find("UpgradeButton").GetComponent<Button>();
                if ((artwork.CurrentStar < artwork.TempStar) && artwork.Status.Equals("available"))
                {
                    Upgrade.gameObject.SetActive(true);
                }
                else
                {
                    Upgrade.gameObject.SetActive(false);
                }

                Upgrade.onClick.AddListener(async () =>
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
