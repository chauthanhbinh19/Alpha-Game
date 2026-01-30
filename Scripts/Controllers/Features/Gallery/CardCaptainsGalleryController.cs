using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardCaptainsGalleryController : MonoBehaviour
{
    public static CardCaptainsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardCaptainBlockButtonPrefab;
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
        CardCaptainBlockButtonPrefab = UIManager.Instance.Get("CardCaptainBlockButtonPrefab");
    }
    public void CreateCardCaptainsGallery(List<CardCaptains> cardCaptains, Transform contentPanel)
    {
        foreach (var cardCaptain in cardCaptains)
        {
            GameObject cardCaptainObject = Instantiate(CardCaptainBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardCaptainObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = cardCaptain.Name.Replace("_", " ");

            RawImage Image = cardCaptainObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardCaptain.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI levelText = cardCaptainObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardCaptain.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = cardCaptainObject.transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = "Card Captain";

            TextMeshProUGUI typePanel = cardCaptainObject.transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardCaptain.Type.ToString().Replace("_", " ");

            Image rareBackground = cardCaptainObject.transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardCaptain.Rare));

            RawImage backgroundImage = cardCaptainObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.CARD_CAPTAIN_BUTTON_BACKGROUND_URL);

            Button button = cardCaptainObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardCaptain, MainPanel);
            });

            TextMeshProUGUI rareText = cardCaptainObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardCaptain.Rare));
            rareText.text = cardCaptain.Rare;

            RawImage blockImage = cardCaptainObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardCaptainObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardCaptain.Status.Equals(AppConstants.Status.AVAILABLE))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (cardCaptain.Status.Equals(AppConstants.Status.PENDING))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardCaptain.Status.Equals(AppConstants.Status.BLOCK))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }
            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
                await cardCaptainsGalleryService.UpdateStatusCardCaptainGalleryAsync(cardCaptain.Id);
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

            Button Upgrade = cardCaptainObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardCaptain.CurrentStar < cardCaptain.TempStar) && cardCaptain.Status.Equals(AppConstants.Status.AVAILABLE))
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
                await CardCaptainsGalleryService.Create().UpdateCardCaptainGalleryPowerAsync(cardCaptain.Id);
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
