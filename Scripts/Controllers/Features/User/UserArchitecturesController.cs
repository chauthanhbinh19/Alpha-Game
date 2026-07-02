using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserArchitecturesController : MonoBehaviour
{
    public static UserArchitecturesController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ArchitectureButtonPrefab;
    private GameObject MainMenuDetailPanel2Prefab;
    private GameObject TempCurrentObject;
    private const int MAX_LEVEL = 100000;
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
        ArchitectureButtonPrefab = UIManager.Instance.Get("ArchitectureButtonPrefab");
        MainMenuDetailPanel2Prefab = UIManager.Instance.Get("MainMenuDetailPanel2Prefab");
    }
    public void CreateUserArchitectures(List<Architectures> architectures, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.ARCHITECTURE_BUTTON_BACKGROUND_URL);

        foreach (var architecture in architectures)
        {
            GameObject architectureObject = Instantiate(ArchitectureButtonPrefab, contentPanel);
            Transform transform = architectureObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = architecture.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(architecture.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            // Kích thước của RawImage (khung hiển thị)
            RectTransform rect = image.GetComponent<RectTransform>();
            float maxWidth = rect.rect.width;
            float maxHeight = rect.rect.height;

            // Kích thước thật của texture
            float texWidth = texture.width;
            float texHeight = texture.height;

            // Tính scale để texture nằm gọn trong khung
            float widthRatio = maxWidth / texWidth;
            float heightRatio = maxHeight / texHeight;
            float finalScale = Mathf.Min(widthRatio, heightRatio);  // scale nhỏ nhất

            // Áp dụng scale theo tỉ lệ đúng
            image.SetNativeSize();
            image.transform.localScale = new Vector3(finalScale, finalScale, 1f);

            RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = bgTexture;

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ShowArchitectureDetails(architecture);
            });

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(architecture.Rarity));
            rareText.text = architecture.Rarity;
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 240);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void ShowArchitectureDetails(Architectures architecture, int buttonType = 1)
    {
        GameObject currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        Transform transform = currentObject.transform;
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Transform setButtonGroupPanel = transform.Find("DictionaryCards/SetButtonGroup/Viewport/Content");
        RawImage cardBackground = transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        RawImage backgroundCircle1Image = transform.Find("DictionaryCards/CircleImage1").GetComponent<RawImage>();
        ButtonLoader.Instance.CreateSetButtonGroup(architecture, setButtonGroupPanel);
        backgroundCircle1Image.gameObject.AddComponent<RotateAnimation>();

        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ARCHITECTURE);
        Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.ARCHITECTURE_BACKGROUND_URL);
        cardBackground.texture = texture;
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
            MainMenuManager.Instance.GetType(AppConstants.MainType.ARCHITECTURE);
        });
        TempCurrentObject = currentObject;
        CreateDetailsUI(architecture, currentObject);
    }
    public void CreateDetailsUI(Architectures architecture, GameObject currentObject)
    {
        RefreshDetailsUI(architecture, currentObject);

        BindButton(architecture, currentObject);
    }
    public void RefreshDetailsUI(Architectures architecture, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(architecture.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        ImageManager.Instance.ChangeSizeImageByTextureScale(image, texture);

        TextMeshProUGUI idText = transform.Find("DictionaryCards/Name/IdText").GetComponent<TextMeshProUGUI>();
        idText.text = "ID: " + architecture.Id;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/Name/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = architecture.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/Power/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(architecture.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = architecture.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/DetailsPanel/Group2/Rare/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{architecture.Rarity}");
        rareImage.texture = rareTexture;

        Transform starGridLayout = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/GridLayout");
        TextureHelper.SetupStars(starGridLayout, architecture.Star);

        RawImage mainClassImage = transform.Find("DictionaryCards/Class/MainClassImage").GetComponent<RawImage>();
        RawImage subClassImage = transform.Find("DictionaryCards/Class/SubClassImage").GetComponent<RawImage>();
        TextMeshProUGUI mainClassText = transform.Find("DictionaryCards/Class/MainClassText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subClassText = transform.Find("DictionaryCards/Class/SubClassText").GetComponent<TextMeshProUGUI>();

        // mainClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(achievement.Class.MainImage));
        // subClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(achievement.Class.SubImage));
        // mainClassText.text = achievement.Class.MainType;
        // subClassText.text = achievement.Class.SubType;

        SetupStat(transform, "Health", AppConstants.StatFields.HEALTH, AppDisplayConstants.StatFieldsShort.HEALTH, architecture.Health);
        SetupStat(transform, "PhysicalAttack", AppConstants.StatFields.PHYSICAL_ATTACK, AppDisplayConstants.StatFieldsShort.PHYSICAL_ATTACK, architecture.PhysicalAttack);
        SetupStat(transform, "PhysicalDefense", AppConstants.StatFields.PHYSICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.PHYSICAL_DEFENSE, architecture.PhysicalDefense);
        SetupStat(transform, "MagicalAttack", AppConstants.StatFields.MAGICAL_ATTACK, AppDisplayConstants.StatFieldsShort.MAGICAL_ATTACK, architecture.MagicalAttack);
        SetupStat(transform, "MagicalDefense", AppConstants.StatFields.MAGICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MAGICAL_DEFENSE, architecture.MagicalDefense);
        SetupStat(transform, "ChemicalAttack", AppConstants.StatFields.CHEMICAL_ATTACK, AppDisplayConstants.StatFieldsShort.CHEMICAL_ATTACK, architecture.ChemicalAttack);
        SetupStat(transform, "ChemicalDefense", AppConstants.StatFields.CHEMICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.CHEMICAL_DEFENSE, architecture.ChemicalDefense);
        SetupStat(transform, "AtomicAttack", AppConstants.StatFields.ATOMIC_ATTACK, AppDisplayConstants.StatFieldsShort.ATOMIC_ATTACK, architecture.AtomicAttack);
        SetupStat(transform, "AtomicDefense", AppConstants.StatFields.ATOMIC_DEFENSE, AppDisplayConstants.StatFieldsShort.ATOMIC_DEFENSE, architecture.AtomicDefense);
        SetupStat(transform, "MentalAttack", AppConstants.StatFields.MENTAL_ATTACK, AppDisplayConstants.StatFieldsShort.MENTAL_ATTACK, architecture.MentalAttack);
        SetupStat(transform, "MentalDefense", AppConstants.StatFields.MENTAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MENTAL_DEFENSE, architecture.MentalDefense);
        SetupStat(transform, "Speed", AppConstants.StatFields.SPEED, AppDisplayConstants.StatFieldsShort.SPEED, architecture.Speed);
    }
    public void BindButton(Architectures architecture, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        Button detailButton = transform.Find("DictionaryCards/DetailsPanel/Group4/Stats/DetailButton").GetComponent<Button>();
        detailButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StatsManager.Instance.CreateStatsManager(architecture);
        });

        Button upgradeLevelButton = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/UpgradeLevelButton").GetComponent<Button>();
        upgradeLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ItemExperienceDTO itemExperienceDTO = await UserItemsService.Create().GetUserItemExperienceByCodeNameAsync(ItemConstants.Experiment.EXP_CARD_HEROES);
            LevelController.Instance.CreateLevelPanel(architecture, itemExperienceDTO, MAX_LEVEL, level => Math.Max(level, 1) * 500d);
        });

        Button upgradeStarButton = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/UpgradeStarButton").GetComponent<Button>();
        upgradeStarButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StarController.Instance.CreateStarPanel(architecture, MAX_LEVEL, level => Math.Max(level, 1) * 2d);
        });

        Button moduleButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Module").GetComponent<Button>();
        moduleButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ModuleManager.Instance.CreateModule(architecture);
        });

        Button upgradeButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            UpgradeManager.Instance.CreateUpgrade(architecture);
        });
    }
    public void RefreshCurrentDetailsUI(Architectures architecture)
    {
        if (TempCurrentObject == null)
            return;

        RefreshDetailsUI(
            architecture,
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
