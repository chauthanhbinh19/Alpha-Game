using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtworksGalleryController : MonoBehaviour
{
    public static ArtworksGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ArtworkSecondPrefab;
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
        ArtworkSecondPrefab = UIManager.Instance.GetGameObject("ArtworkSecondPrefab");
    }
    public void CreateArtworkGallery(List<Artworks> alchemies, Transform contentPanel)
    {
        foreach (var Artwork in alchemies)
        {
            GameObject ArtworkObject = Instantiate(ArtworkSecondPrefab, contentPanel);

            Text Title = ArtworkObject.transform.Find("Title").GetComponent<Text>();
            Title.text = Artwork.Name.Replace("_", " ");

            RawImage Image = ArtworkObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Artwork.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // RawImage frameImage = ArtworkObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);

            Button button = ArtworkObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(Artwork, MainPanel);
            });

            RawImage rareImage = ArtworkObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Artwork.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = ArtworkObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = ArtworkObject.transform.Find("Unlock").GetComponent<Button>();
            if (Artwork.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (Artwork.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (Artwork.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var artworkGalleryService = ArtworkGalleryService.Create();
                artworkGalleryService.UpdateStatusArtworkGallery(Artwork.Id);
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;

                var powerManagerService = PowerManagerService.Create();
                var teamsService = TeamsService.Create();

                powerManagerService.UpdateUserStats(User.CurrentUserId);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
            });

            Button Upgrade = ArtworkObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((Artwork.CurrentStar < Artwork.TempStar) && Artwork.Status.Equals("available"))
            {
                Upgrade.gameObject.SetActive(true);
            }
            else
            {
                Upgrade.gameObject.SetActive(false);
            }

            Upgrade.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ArtworkGalleryService.Create().UpdateArtworkGalleryPower(Artwork.Id);
            });
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(270, 220);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
