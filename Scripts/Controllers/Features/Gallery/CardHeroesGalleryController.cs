using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHeroesGalleryController : MonoBehaviour
{
    public static CardHeroesGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardHeroBlockButtonPrefab;
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
        CardHeroBlockButtonPrefab = UIManager.Instance.Get("CardHeroBlockButtonPrefab");
    }
    public void CreateCardHeroesGallery(List<CardHeroes> cardHeroes, Transform contentPanel)
    {
        foreach (var cardHero in cardHeroes)
        {
            GameObject cardHeroObject = Instantiate(CardHeroBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardHeroObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = cardHero.Name.Replace("_", " ");

            RawImage Image = cardHeroObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardHero.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI levelText = cardHeroObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardHero.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = cardHeroObject.transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = "Card Hero";

            TextMeshProUGUI typePanel = cardHeroObject.transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardHero.Type.ToString().Replace("_", " ");

            Image rareBackground = cardHeroObject.transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardHero.Rare));

            RawImage backgroundImage = cardHeroObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.CARD_HERO_BUTTON_BACKGROUND_URL);

            Button button = cardHeroObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardHero, MainPanel);
            });

            TextMeshProUGUI rareText = cardHeroObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardHero.Rare));
            rareText.text = cardHero.Rare;

            RawImage blockImage = cardHeroObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardHeroObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardHero.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (cardHero.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardHero.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var cardHeroesGalleryService = CardHeroesGalleryService.Create();
                await cardHeroesGalleryService.UpdateStatusCardHeroGalleryAsync(cardHero.Id);
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

            Button Upgrade = cardHeroObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardHero.CurrentStar < cardHero.TempStar) && cardHero.Status.Equals("available"))
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
                await CardHeroesGalleryService.Create().UpdateCardHeroGalleryPowerAsync(cardHero.Id);
            });
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(280, 360);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
