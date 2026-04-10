using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardAdmiralsGalleryController : MonoBehaviour
{
    public static CardAdmiralsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardAdmiralBlockButtonPrefab;
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
        CardAdmiralBlockButtonPrefab = UIManager.Instance.Get("CardAdmiralBlockButtonPrefab");
    }
    public void CreateCardAdmiralsGallery(List<CardAdmirals> cardAdmirals, Transform contentPanel)
    {
        foreach (var cardAdmiral in cardAdmirals)
        {
            GameObject cardAdmiralObject = Instantiate(CardAdmiralBlockButtonPrefab, contentPanel);

            TextMeshProUGUI titleText = cardAdmiralObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardAdmiral.Name.Replace("_", " ");

            RawImage image = cardAdmiralObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardAdmiral.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = cardAdmiralObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardAdmiral.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = cardAdmiralObject.transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = "Card Admiral";

            TextMeshProUGUI typePanel = cardAdmiralObject.transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardAdmiral.Type.ToString().Replace("_", " ");

            Image rareBackground = cardAdmiralObject.transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardAdmiral.Rare));

            RawImage backgroundImage = cardAdmiralObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.CARD_ADMIRAL_BUTTON_BACKGROUND_URL);

            Button button = cardAdmiralObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardAdmiral, MainPanel);
            });

            TextMeshProUGUI rareText = cardAdmiralObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardAdmiral.Rare));
            rareText.text = cardAdmiral.Rare;

            RawImage blockImage = cardAdmiralObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardAdmiralObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardAdmiral.Status.Equals(AppConstants.Status.AVAILABLE))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                image.color = Color.white;
            }
            else if (cardAdmiral.Status.Equals(AppConstants.Status.PENDING))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardAdmiral.Status.Equals(AppConstants.Status.BLOCK))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var admiralGalleryService = CardAdmiralsGalleryService.Create();
                await admiralGalleryService.UpdateStatusCardAdmiralGalleryAsync(cardAdmiral.Id);
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

            Button Upgrade = cardAdmiralObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardAdmiral.CurrentStar < cardAdmiral.TempStar) && cardAdmiral.Status.Equals(AppConstants.Status.AVAILABLE))
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
                await CardAdmiralsGalleryService.Create().UpdateCardAdmiralGalleryPowerAsync(cardAdmiral.Id);
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
