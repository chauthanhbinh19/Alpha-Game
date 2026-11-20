using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserCardLivesController : MonoBehaviour
{
    public static UserCardLivesController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardLifeButtonPrefab;
    private GameObject MainMenuDetailPanel2Prefab;
    private GameObject TempCurrentObject;
    private const int MAX_LEVEL = 10000;
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        CardLifeButtonPrefab = UIManager.Instance.Get(PrefabConstants.Component.CARD_LIFE_BUTTON_PREFAB);
        MainMenuDetailPanel2Prefab = UIManager.Instance.Get(PrefabConstants.General.MAIN_MENU_DETAIL_PANEL_2_PREFAB);
    }
    public void CreateUserCardLives(List<CardLives> cardLives, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        foreach (var cardLife in cardLives)
        {
            GameObject cardLifeObject = Instantiate(CardLifeButtonPrefab, contentPanel);
            Transform transform = cardLifeObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardLife.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardLife.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardLife.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = LocalizationManager.Get(AppDisplayConstants.Title.CARD_LIFE);

            TextMeshProUGUI typePanel = transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardLife.Type.ToString().Replace("_", " ");

            Image rareBackground = transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardLife.Rarity));

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardLife.Rarity));
            rareText.text = cardLife.Rarity;

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ShowCardLifeDetails(cardLife);
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
    public void ShowCardLifeDetails(CardLives cardLife)
    {
        GameObject currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        Transform transform = currentObject.transform;
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Transform setButtonGroupPanel = transform.Find("DictionaryCards/SetButtonGroup/Viewport/Content");

        titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.CARD_LIFE);
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
            await MainMenuManager.Instance.GetTypeAsync(AppConstants.MainType.CARD_LIFE);
        });
        Button homeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TempCurrentObject = currentObject;
        CreateDetailsUI(cardLife, currentObject);
    }
    public void CreateDetailsUI(CardLives cardLife, GameObject currentObject)
    {
        _=RefreshDetailsUIAsync(cardLife, currentObject);

        BindButton(cardLife, currentObject);
    }
    public async Task RefreshDetailsUIAsync(CardLives cardLife, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardLife.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI idText = transform.Find("DictionaryCards/Name/IdText").GetComponent<TextMeshProUGUI>();
        idText.text = "ID: " + cardLife.Id;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/Name/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardLife.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/Power/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardLife.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardLife.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/DetailsPanel/Group1/Rare/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"{QualityEvaluatorHelper.CheckRareImage(cardLife.Rarity)}");
        rareImage.texture = rareTexture;

        GameConfig gameConfig = await GameConfigService.Create().GetGameConfigAsync(AppConstants.ObjectType.CARD_LIVES, "LEVEL");
        double maxLevel = double.Parse(gameConfig.MaxValue);
        TextMeshProUGUI nameText2 = transform.Find("DictionaryCards/Name/NameText2").GetComponent<TextMeshProUGUI>();
        nameText2.text = cardLife.Name;
        TextMeshProUGUI levelTitleText2 = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/LevelTitleText2").GetComponent<TextMeshProUGUI>();
        levelTitleText2.text = "Lv. " + "<color=yellow>" + cardLife.Level.ToString() + "</color>" + "/" + maxLevel.ToString();

        Transform starGridLayout = transform.Find("DictionaryCards/DetailsPanel/Group1/Star/GridLayout");
        TextureHelper.SetupStars(starGridLayout, cardLife.Star);

        RawImage mainClassImage = transform.Find("DictionaryCards/Class/MainClassImage").GetComponent<RawImage>();
        RawImage subClassImage = transform.Find("DictionaryCards/Class/SubClassImage").GetComponent<RawImage>();
        TextMeshProUGUI mainClassText = transform.Find("DictionaryCards/Class/MainClassText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subClassText = transform.Find("DictionaryCards/Class/SubClassText").GetComponent<TextMeshProUGUI>();

        // mainClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(achievement.Class.MainImage));
        // subClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(achievement.Class.SubImage));
        // mainClassText.text = achievement.Class.MainType;
        // subClassText.text = achievement.Class.SubType;

        SetupStat(transform, "Health", AppConstants.StatFields.HEALTH, AppDisplayConstants.StatFieldsShort.HEALTH, cardLife.Health);
        SetupStat(transform, "PhysicalAttack", AppConstants.StatFields.PHYSICAL_ATTACK, AppDisplayConstants.StatFieldsShort.PHYSICAL_ATTACK, cardLife.PhysicalAttack);
        SetupStat(transform, "PhysicalDefense", AppConstants.StatFields.PHYSICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.PHYSICAL_DEFENSE, cardLife.PhysicalDefense);
        SetupStat(transform, "MagicalAttack", AppConstants.StatFields.MAGICAL_ATTACK, AppDisplayConstants.StatFieldsShort.MAGICAL_ATTACK, cardLife.MagicalAttack);
        SetupStat(transform, "MagicalDefense", AppConstants.StatFields.MAGICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MAGICAL_DEFENSE, cardLife.MagicalDefense);
        SetupStat(transform, "ChemicalAttack", AppConstants.StatFields.CHEMICAL_ATTACK, AppDisplayConstants.StatFieldsShort.CHEMICAL_ATTACK, cardLife.ChemicalAttack);
        SetupStat(transform, "ChemicalDefense", AppConstants.StatFields.CHEMICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.CHEMICAL_DEFENSE, cardLife.ChemicalDefense);
        SetupStat(transform, "AtomicAttack", AppConstants.StatFields.ATOMIC_ATTACK, AppDisplayConstants.StatFieldsShort.ATOMIC_ATTACK, cardLife.AtomicAttack);
        SetupStat(transform, "AtomicDefense", AppConstants.StatFields.ATOMIC_DEFENSE, AppDisplayConstants.StatFieldsShort.ATOMIC_DEFENSE, cardLife.AtomicDefense);
        SetupStat(transform, "MentalAttack", AppConstants.StatFields.MENTAL_ATTACK, AppDisplayConstants.StatFieldsShort.MENTAL_ATTACK, cardLife.MentalAttack);
        SetupStat(transform, "MentalDefense", AppConstants.StatFields.MENTAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MENTAL_DEFENSE, cardLife.MentalDefense);
        SetupStat(transform, "Speed", AppConstants.StatFields.SPEED, AppDisplayConstants.StatFieldsShort.SPEED, cardLife.Speed);
    }
    public void BindButton(CardLives cardLife, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        Button detailButton = transform.Find("DictionaryCards/DetailsPanel/Group4/Stats/DetailButton").GetComponent<Button>();
        detailButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StatsManager.Instance.CreateStatsManager(cardLife);
        });

        Button upgradeLevelButton = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/UpgradeLevelButton").GetComponent<Button>();
        upgradeLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await LevelController.Instance.CreateLevelPanelAsync(cardLife);
        });

        Button upgradeStarButton = transform.Find("DictionaryCards/DetailsPanel/Group1/Star/UpgradeStarButton").GetComponent<Button>();
        upgradeStarButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await StarController.Instance.CreateStarPanelAsync(cardLife);
        });

        Button moduleButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Module").GetComponent<Button>();
        moduleButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ModuleManager.Instance.CreateModule(cardLife);
        });

        Button upgradeButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            UpgradeManager.Instance.CreateUpgrade(cardLife);
        });

        Button masterButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Master").GetComponent<Button>();
        Transform masterBlockGroup = masterButton.transform.Find("BlockGroup");
        masterBlockGroup.gameObject.SetActive(true);
        masterButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
            NotificationManager.Instance.ShowNotification(LocalizationManager.Get(MessageConstants.FEATURE_NOT_APPLIED_MESSAGE));
        });

        Button rankButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Rank").GetComponent<Button>();
        Transform rankBlockGroup = rankButton.transform.Find("BlockGroup");
        rankBlockGroup.gameObject.SetActive(true);
        rankButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
            NotificationManager.Instance.ShowNotification(LocalizationManager.Get(MessageConstants.FEATURE_NOT_APPLIED_MESSAGE));
        });

        Button equipmentButton = transform.Find("DictionaryCards/DetailsPanel/Group5/Equipment").GetComponent<Button>();
        equipmentButton.gameObject.SetActive(false);
        equipmentButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
        });
    }
    public void RefreshCurrentDetailsUI(CardLives cardLife)
    {
        if (TempCurrentObject == null)
            return;

        _=RefreshDetailsUIAsync(
            cardLife,
            TempCurrentObject);
    }
    private void SetupStat(Transform root, string statObjectName, string statField, string statDisplayName, double value, bool isPercent = false)
    {
        Transform statTransform = root.Find($"DictionaryCards/DetailsPanel/Group4/Stats/GridLayout/{statObjectName}");

        RawImage iconImage = statTransform.Find("IconImage").GetComponent<RawImage>();
        TextMeshProUGUI titleText = statTransform.Find("StatTitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI valueText = statTransform.Find("StatText").GetComponent<TextMeshProUGUI>();

        TextureHelper.CreatePropertyRuneUI(statField, iconImage);

        titleText.text = LocalizationManager.Get(statDisplayName);
        titleText.enableWordWrapping = false;

        if (isPercent)
        {
            valueText.text = NumberFormatterHelper.FormatNumberExtended(value, true) + " %";
        }
        else
        {
            valueText.text = NumberFormatterHelper.FormatNumberExtended(value, true);
        }
    }
}
