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
    public void CreateCardLivesGallery(List<CardLives> cardLives, Transform contentPanel)
    {
        foreach (var cardLife in cardLives)
        {
            GameObject cardLifeObject = Instantiate(CardLifeBlockButtonPrefab, contentPanel);

            TextMeshProUGUI titleText = cardLifeObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardLife.Name.Replace("_", " ");

            RawImage image = cardLifeObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardLife.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = cardLifeObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardLife.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = cardLifeObject.transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = "Card Life";

            TextMeshProUGUI typePanel = cardLifeObject.transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardLife.Type.ToString().Replace("_", " ");

            Image rareBackground = cardLifeObject.transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardLife.Rare));

            RawImage backgroundImage = cardLifeObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.CARD_LIFE_BUTTON_BACKGROUND_URL);

            Button button = cardLifeObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardLife, MainPanel);
            });

            TextMeshProUGUI rareText = cardLifeObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardLife.Rare));
            rareText.text = cardLife.Rare;

            RawImage blockImage = cardLifeObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardLifeObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardLife.Status.Equals(AppConstants.Status.AVAILABLE))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                image.color = Color.white;
            }
            else if (cardLife.Status.Equals(AppConstants.Status.PENDING))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardLife.Status.Equals(AppConstants.Status.BLOCK))
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
                image.color = Color.white;

                var powerManagerService = PowerManagerService.Create();
                var teamsService = TeamsService.Create();

                await powerManagerService.UpdateUserStatsAsync(User.CurrentUserId);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
            });

            Button Upgrade = cardLifeObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardLife.CurrentStar < cardLife.TempStar) && cardLife.Status.Equals(AppConstants.Status.AVAILABLE))
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
            gridLayout.cellSize = new Vector2(250, 360);
            gridLayout.spacing = new Vector2(23, 10);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
