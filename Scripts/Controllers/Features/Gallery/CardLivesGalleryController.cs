using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardLivesGalleryController : MonoBehaviour
{
    public static CardLivesGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardLifeBlockButtonPrefab;
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
        CardLifeBlockButtonPrefab = UIManager.Instance.Get("CardLifeBlockButtonPrefab");
    }
    public void CreateCardLifeGallery(List<CardLives> cardLives, Transform contentPanel)
    {
        foreach (var cardLife in cardLives)
        {
            GameObject cardLifeObject = Instantiate(CardLifeBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardLifeObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = cardLife.Name.Replace("_", " ");

            RawImage Image = cardLifeObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardLife.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage backgroundImage = cardLifeObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.CARD_LIFE_BUTTON_BACKGROUND_URL);

            Button button = cardLifeObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardLife, MainPanel);
            });

            RawImage rareImage = cardLifeObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardLife.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = cardLifeObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardLifeObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardLife.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (cardLife.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardLife.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var cardLifeGalleryService = CardLivesGalleryService.Create();
                await cardLifeGalleryService.UpdateStatusCardLifeGalleryAsync(cardLife.Id);
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;

                var powerManagerService = PowerManagerService.Create();
                var teamsService = TeamsService.Create();

                await powerManagerService.UpdateUserStatsAsync(User.CurrentUserId);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
            });

            Button Upgrade = cardLifeObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardLife.CurrentStar < cardLife.TempStar) && cardLife.Status.Equals("available"))
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
                await CardLivesGalleryService.Create().UpdateCardLifeGalleryPowerAsync(cardLife.Id);
            });
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(280, 350);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
