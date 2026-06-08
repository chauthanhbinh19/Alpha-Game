using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserFurnituresController : MonoBehaviour
{
    public static UserFurnituresController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject FurnitureButtonPrefab;
    private GameObject MainMenuDetailPanel2Prefab;
    private GameObject tempCurrentObject;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        FurnitureButtonPrefab = UIManager.Instance.Get("FurnitureButtonPrefab");
        MainMenuDetailPanel2Prefab = UIManager.Instance.Get("MainMenuDetailPanel2Prefab");
    }
    public void CreateUserFurnitures(List<Furnitures> furnitures, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.FURNITURE_BUTTON_BACKGROUND_URL);

        foreach (var furniture in furnitures)
        {
            GameObject furnitureObject = Instantiate(FurnitureButtonPrefab, contentPanel);
            Transform transform = furnitureObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = furniture.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(furniture.Image);
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
                ShowFurnitureDetails(furniture);
            });

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(furniture.Rarity));
            rareText.text = furniture.Rarity;

        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void ShowFurnitureDetails(Furnitures furniture, int buttonType = 1)
    {
        GameObject currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        Transform transform = currentObject.transform;
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Transform setButtonGroupPanel = transform.Find("DictionaryCards/SetButtonGroup/Viewport/Content");
        RawImage cardBackground = transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        RawImage backgroundCircle1Image = transform.Find("DictionaryCards/CircleImage1").GetComponent<RawImage>();
        ButtonLoader.Instance.CreateSetButtonGroup(furniture, setButtonGroupPanel);
        backgroundCircle1Image.gameObject.AddComponent<RotateAnimation>();

        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FURNITURE);
        Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.FURNITURE_BACKGROUND_URL);
        cardBackground.texture = texture;
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
            MainMenuManager.Instance.GetType(AppConstants.MainType.FURNITURE);
        });
        tempCurrentObject = currentObject;
        CreateDetailsUI(furniture, currentObject);
    }
    public void CreateDetailsUI(Furnitures furniture, GameObject currentObject)
    {
        RefreshDetailsUI(furniture, currentObject);

        BindButton(furniture, currentObject);
    }
    public void RefreshDetailsUI(Furnitures furniture, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(furniture.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI idText = transform.Find("DictionaryCards/Name/IdText").GetComponent<TextMeshProUGUI>();
        idText.text = "ID: " + furniture.Id;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/Name/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = furniture.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/Power/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(furniture.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = furniture.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/DetailsPanel/Group2/Rare/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{furniture.Rarity}");
        rareImage.texture = rareTexture;

        Transform starGridLayout = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/GridLayout");
        TextureHelper.SetupStars(starGridLayout, furniture.Star);

        RawImage mainClassImage = transform.Find("DictionaryCards/Class/MainClassImage").GetComponent<RawImage>();
        RawImage subClassImage = transform.Find("DictionaryCards/Class/SubClassImage").GetComponent<RawImage>();
        TextMeshProUGUI mainClassText = transform.Find("DictionaryCards/Class/MainClassText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subClassText = transform.Find("DictionaryCards/Class/SubClassText").GetComponent<TextMeshProUGUI>();

        // mainClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(achievement.Class.MainImage));
        // subClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(achievement.Class.SubImage));
        // mainClassText.text = achievement.Class.MainType;
        // subClassText.text = achievement.Class.SubType;

        SetupStat(transform, "Health", AppConstants.StatFields.HEALTH, AppDisplayConstants.StatFieldsShort.HEALTH, furniture.Health);
        SetupStat(transform, "PhysicalAttack", AppConstants.StatFields.PHYSICAL_ATTACK, AppDisplayConstants.StatFieldsShort.PHYSICAL_ATTACK, furniture.PhysicalAttack);
        SetupStat(transform, "PhysicalDefense", AppConstants.StatFields.PHYSICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.PHYSICAL_DEFENSE, furniture.PhysicalDefense);
        SetupStat(transform, "MagicalAttack", AppConstants.StatFields.MAGICAL_ATTACK, AppDisplayConstants.StatFieldsShort.MAGICAL_ATTACK, furniture.MagicalAttack);
        SetupStat(transform, "MagicalDefense", AppConstants.StatFields.MAGICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MAGICAL_DEFENSE, furniture.MagicalDefense);
        SetupStat(transform, "ChemicalAttack", AppConstants.StatFields.CHEMICAL_ATTACK, AppDisplayConstants.StatFieldsShort.CHEMICAL_ATTACK, furniture.ChemicalAttack);
        SetupStat(transform, "ChemicalDefense", AppConstants.StatFields.CHEMICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.CHEMICAL_DEFENSE, furniture.ChemicalDefense);
        SetupStat(transform, "AtomicAttack", AppConstants.StatFields.ATOMIC_ATTACK, AppDisplayConstants.StatFieldsShort.ATOMIC_ATTACK, furniture.AtomicAttack);
        SetupStat(transform, "AtomicDefense", AppConstants.StatFields.ATOMIC_DEFENSE, AppDisplayConstants.StatFieldsShort.ATOMIC_DEFENSE, furniture.AtomicDefense);
        SetupStat(transform, "MentalAttack", AppConstants.StatFields.MENTAL_ATTACK, AppDisplayConstants.StatFieldsShort.MENTAL_ATTACK, furniture.MentalAttack);
        SetupStat(transform, "MentalDefense", AppConstants.StatFields.MENTAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MENTAL_DEFENSE, furniture.MentalDefense);
        SetupStat(transform, "Speed", AppConstants.StatFields.SPEED, AppDisplayConstants.StatFieldsShort.SPEED, furniture.Speed);
    }
    public void BindButton(Furnitures furniture, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        Button detailButton = transform.Find("DictionaryCards/DetailsPanel/Group4/Stats/DetailButton").GetComponent<Button>();
        detailButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StatsManager.Instance.CreateStatsManager(furniture);
        });

        Button upgradeLevelButton = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/UpgradeLevelButton").GetComponent<Button>();
        upgradeLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ItemExperienceDTO itemExperienceDTO = await UserItemsService.Create().GetUserItemExperienceByCodeNameAsync(ItemConstants.Experiment.EXP_CARD_HEROES);
            LevelController.Instance.CreateLevelPanel(furniture, itemExperienceDTO, MAX_LEVEL, level => Math.Max(level, 1) * 500d);
        });

        Button upgradeStarButton = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/UpgradeStarButton").GetComponent<Button>();
        upgradeStarButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StarController.Instance.CreateStarPanel(furniture, MAX_LEVEL, level => Math.Max(level, 1) * 2d);
        });

        Button moduleButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Module").GetComponent<Button>();
        moduleButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ModuleManager.Instance.CreateModule(furniture);
        });

        Button upgradeButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            UpgradeManager.Instance.CreateUpgrade(furniture);
        });
    }
    public void RefreshCurrentDetailsUI(Furnitures furniture)
    {
        if (tempCurrentObject == null)
            return;

        RefreshDetailsUI(
            furniture,
            tempCurrentObject);
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
