using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSoldiersGalleryController : MonoBehaviour
{
    public static CardSoldiersGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardSoldierBlockButtonPrefab;
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
        CardSoldierBlockButtonPrefab = UIManager.Instance.Get("CardSoldierBlockButtonPrefab");
    }
    public void CreateCardSoldiersGallery(List<CardSoldiers> cardAdmirals, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        foreach (var cardAdmiral in cardAdmirals)
        {
            GameObject cardAdmiralObject = Instantiate(CardSoldierBlockButtonPrefab, contentPanel);
            Transform transform = cardAdmiralObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardAdmiral.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardAdmiral.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardAdmiral.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);

            TextMeshProUGUI typePanel = transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardAdmiral.Type.ToString().Replace("_", " ");

            Image rareBackground = transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardAdmiral.Rare));

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardAdmiral, MainPanel);
            });

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardAdmiral.Rare));
            rareText.text = cardAdmiral.Rare;

            RawImage blockImage = transform.Find("Block").GetComponent<RawImage>();
            Button unlockButton = transform.Find("UnlockButton").GetComponent<Button>();
            if (cardAdmiral.Status.Equals(AppConstants.Status.AVAILABLE))
            {
                blockImage.gameObject.SetActive(false);
                unlockButton.gameObject.SetActive(false);
                image.color = Color.white;
            }
            else if (cardAdmiral.Status.Equals(AppConstants.Status.PENDING))
            {
                blockImage.gameObject.SetActive(true);
                unlockButton.gameObject.SetActive(true);
            }
            else if (cardAdmiral.Status.Equals(AppConstants.Status.BLOCK))
            {
                blockImage.gameObject.SetActive(true);
                unlockButton.gameObject.SetActive(false);
            }

            unlockButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var admiralGalleryService = CardSoldiersGalleryService.Create();
                await admiralGalleryService.UpdateStatusCardSoldierGalleryAsync(cardAdmiral.Id);
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
            if ((cardAdmiral.CurrentStar < cardAdmiral.TempStar) && cardAdmiral.Status.Equals(AppConstants.Status.AVAILABLE))
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
                await CardSoldiersGalleryService.Create().UpdateCardSoldierGalleryPowerAsync(cardAdmiral.Id);
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
