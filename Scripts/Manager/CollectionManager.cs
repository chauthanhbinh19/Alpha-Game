using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Threading.Tasks;

public class CollectionManager : MonoBehaviour
{
    private GameObject CollectionButtonPrefab;
    private Transform collectionMenuPanel;
    private GameObject DictionaryPanelPrefab;
    private Transform DictionaryContentPanel;
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
    private Material UI_Green_Gradient_Radius_Mat_MaskPercent_70;
    private Transform MainPanel;
    private Button CloseButton;
    private Button HomeButton;
    //Variable for pagination
    private int Offset = 0;
    private int CurrentPage = 1;
    private int TotalPage;
    private const int PAGE_SIZE = 100;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private string MainType;
    private TextMeshProUGUI TitleText;
    private string Search = "";
    private string Type = AppConstants.Type.ALL;
    private string Rare = AppConstants.Rare.ALL;
    public static CollectionManager Instance { get; private set; }
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        CollectionButtonPrefab = UIManager.Instance.Get("CollectionButtonPrefab");
    }
    public void CreateCollectionButton(Transform tempCollectionMenuPanel)
    {
        Texture2D itemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_COLLECTION_URL);
        //Collection menu
        CreateCollectionButtonUI(1, AppDisplayConstants.Collection.CARD_HEROES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_HERO_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(2, AppDisplayConstants.Collection.BOOKS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.BOOK_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BOOK_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(3, AppDisplayConstants.Collection.PETS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.PET_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PET_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(4, AppDisplayConstants.Collection.CARD_CAPTAINS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_CAPTAIN_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_CAPTAIN_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(5, AppDisplayConstants.Collection.COLLABORATION_EQUIPMENTS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.COLLABORATION_EQUIPMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_EQUIPMENT_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(6, AppDisplayConstants.Collection.CARD_MILITARIES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_MILITARY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MILITARY_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(7, AppDisplayConstants.Collection.CARD_SPELLS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_SPELL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SPELL_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(8, AppDisplayConstants.Collection.COLLABORATIONS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.COLLABORATION_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(9, AppDisplayConstants.Collection.CARD_MONSTERS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_MONSTER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MONSTER_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(10, AppDisplayConstants.Collection.EQUIPMENTS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.EQUIPMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.EQUIPMENT_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(11, AppDisplayConstants.Collection.MEDALS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.MEDAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MEDAL_URL),
             tempCollectionMenuPanel);
        CreateCollectionButtonUI(12, AppDisplayConstants.Collection.SKILLS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.SKILL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SKILL_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(13, AppDisplayConstants.Collection.SYMBOLS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.SYMBOL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SYMBOL_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(14, AppDisplayConstants.Collection.TITLES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.TITLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TITLE_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(15, AppDisplayConstants.Collection.MAGIC_FORMATION_CIRCLES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.MAGIC_FORMATION_CIRCLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MAGIC_FORMATION_CIRCLE_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(16, AppDisplayConstants.Collection.RELICS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.RELIC_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.RELIC_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(17, AppDisplayConstants.Collection.CARD_COLONELS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_COLONEL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_COLONEL_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(18, AppDisplayConstants.Collection.CARD_GENERALS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_GENERAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_GENERAL_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(19, AppDisplayConstants.Collection.CARD_ADMIRALS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_ADMIRAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_ADMIRAL_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(20, AppDisplayConstants.Collection.BORDERS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.BORDER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BORDER_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(21, AppDisplayConstants.Collection.TALISMANS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.TALISMAN_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TALISMAN_URL),  
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(22, AppDisplayConstants.Collection.PUPPETS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.PUPPET_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PUPPET_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(23, AppDisplayConstants.Collection.ALCHEMIES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.ALCHEMY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ALCHEMY_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(24, AppDisplayConstants.Collection.FORGES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.FORGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FORGE_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(25, AppDisplayConstants.Collection.CARD_LIVES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.LIFE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_LIFE_URL),
             tempCollectionMenuPanel);
        CreateCollectionButtonUI(26, AppDisplayConstants.Collection.ARTWORKS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.ARTWORK_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTWORK_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(27, AppDisplayConstants.Collection.SPIRIT_BEASTS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.SPIRIT_BEAST_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_BEAST_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(28, AppDisplayConstants.Collection.AVATARS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.AVATAR_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.AVATAR_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(29, AppDisplayConstants.Collection.SPIRIT_CARDS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.SPIRIT_CARD_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_CARD_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(30, AppDisplayConstants.Collection.ACHIEVEMENTS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.ACHIEVEMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ACHIEVEMENT_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(31, AppDisplayConstants.Collection.ARTIFACTS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.ARTIFACT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTIFACT_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(32, AppDisplayConstants.Collection.ARCHITECTURES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.ARCHITECTURE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARCHITECTURE_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(33, AppDisplayConstants.Collection.TECHNOLOGIES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.TECHNOLOGY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TECHNOLOGY_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(34, AppDisplayConstants.Collection.VEHICLES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.VEHICLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.VEHICLE_URL),
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(35, AppDisplayConstants.Collection.CORES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CORE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CORE_URL),
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(36, AppDisplayConstants.Collection.WEAPONS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.WEAPON_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.WEAPON_URL),  
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(37, AppDisplayConstants.Collection.ROBOTS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.ROBOT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ROBOT_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(38, AppDisplayConstants.Collection.BADGES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.BADGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BADGE_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(39, AppDisplayConstants.Collection.MECHA_BEASTS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.MECHA_BEAST_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MECHA_BEAST_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(40, AppDisplayConstants.Collection.RUNES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.RUNE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.RUNE_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(41, AppDisplayConstants.Collection.FURNITURES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.FURNITURE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FURNITURE_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(42, AppDisplayConstants.Collection.FOODS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.FOOD_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FOOD_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(43, AppDisplayConstants.Collection.BEVERAGES_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.BEVERAGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BEVERAGE_URL),
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(44, AppDisplayConstants.Collection.BUILDINGS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.BUILDING_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BUILDING_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(45, AppDisplayConstants.Collection.PLANTS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.PLANT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PLANT_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(46, AppDisplayConstants.Collection.FASHIONS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.FASHION_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FASHION_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(47, AppDisplayConstants.Collection.EMOJIS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.EMOJI_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.EMOJI_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(48, AppDisplayConstants.Collection.CARD_SOLDIERS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.CARD_SOLDIER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SOLDIER_URL), 
            tempCollectionMenuPanel);
        CreateCollectionButtonUI(49, AppDisplayConstants.Collection.OUTFITS_COLLECTION, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Collection.OUTFIT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.OUTFIT_URL), 
            tempCollectionMenuPanel);

        tempCollectionMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateCollectionButtonUI(int index, string itemName, Texture2D _itemBackground, Texture2D _itemImage, Texture2D _borderImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(CollectionButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán màu cho subBackground
        RawImage subBackground = transform.Find("SubBackground").GetComponent<RawImage>();
        if (subBackground != null && _itemBackground != null)
        {
            subBackground.texture = _itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && _itemImage != null)
        {
            image.texture = _itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        // Gán hình ảnh cho itemBorder
        RawImage borderImage = newButton.transform.Find("BorderImage").GetComponent<RawImage>();
        if (borderImage != null && _borderImage != null)
        {
            borderImage.texture = _borderImage;
        }

        // Gán màu cho subBackground
        RawImage itemBackground = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (itemBackground != null)
        {
            itemBackground.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Flag.FLAG_COLLECTION_URL);
        }
    }
    public void CreateCollection(Transform tempCollectionMenuPanel)
    {
        collectionMenuPanel = tempCollectionMenuPanel;
        DictionaryPanelPrefab = UIManager.Instance.Get("DictionaryPanelPrefab");
        UI_Green_Gradient_Radius_Mat_MaskPercent_70 = MaterialManager.Instance.Get("UI_Green_Gradient_Radius_Mat_MaskPercent_70");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");

        AssignButtonEvent("Button_1", () => GetType(AppConstants.MainType.CARD_HERO));
        AssignButtonEvent("Button_2", () => GetType(AppConstants.MainType.BOOK));
        AssignButtonEvent("Button_3", () => GetType(AppConstants.MainType.PET));
        AssignButtonEvent("Button_4", () => GetType(AppConstants.MainType.CARD_CAPTAIN));
        AssignButtonEvent("Button_5", () => GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT));
        AssignButtonEvent("Button_6", () => GetType(AppConstants.MainType.CARD_MILITARY));
        AssignButtonEvent("Button_7", () => GetType(AppConstants.MainType.CARD_SPELL));
        AssignButtonEvent("Button_8", () => GetType(AppConstants.MainType.COLLABORATION));
        AssignButtonEvent("Button_9", () => GetType(AppConstants.MainType.CARD_MONSTER));
        AssignButtonEvent("Button_10", () => GetType(AppConstants.MainType.EQUIPMENT));
        AssignButtonEvent("Button_11", () => GetType(AppConstants.MainType.MEDAL));
        AssignButtonEvent("Button_12", () => GetType(AppConstants.MainType.SKILL));
        AssignButtonEvent("Button_13", () => GetType(AppConstants.MainType.SYMBOL));
        AssignButtonEvent("Button_14", () => GetType(AppConstants.MainType.TITLE));
        AssignButtonEvent("Button_15", () => GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE));
        AssignButtonEvent("Button_16", () => GetType(AppConstants.MainType.RELIC));
        AssignButtonEvent("Button_17", () => GetType(AppConstants.MainType.CARD_COLONEL));
        AssignButtonEvent("Button_18", () => GetType(AppConstants.MainType.CARD_GENERAL));
        AssignButtonEvent("Button_19", () => GetType(AppConstants.MainType.CARD_ADMIRAL));
        AssignButtonEvent("Button_20", () => GetType(AppConstants.MainType.BORDER));
        AssignButtonEvent("Button_21", () => GetType(AppConstants.MainType.TALISMAN));
        AssignButtonEvent("Button_22", () => GetType(AppConstants.MainType.PUPPET));
        AssignButtonEvent("Button_23", () => GetType(AppConstants.MainType.ALCHEMY));
        AssignButtonEvent("Button_24", () => GetType(AppConstants.MainType.FORGE));
        AssignButtonEvent("Button_25", () => GetType(AppConstants.MainType.CARD_LIFE));
        AssignButtonEvent("Button_26", () => GetType(AppConstants.MainType.ARTWORK));
        AssignButtonEvent("Button_27", () => GetType(AppConstants.MainType.SPIRIT_BEAST));
        AssignButtonEvent("Button_28", () => GetType(AppConstants.MainType.AVATAR));
        AssignButtonEvent("Button_29", () => GetType(AppConstants.MainType.SPIRIT_CARD));
        AssignButtonEvent("Button_30", () => GetType(AppConstants.MainType.ACHIEVEMENT));
        AssignButtonEvent("Button_31", () => GetType(AppConstants.MainType.ARTIFACT));
        AssignButtonEvent("Button_32", () => GetType(AppConstants.MainType.ARCHITECTURE));
        AssignButtonEvent("Button_33", () => GetType(AppConstants.MainType.TECHNOLOGY));
        AssignButtonEvent("Button_34", () => GetType(AppConstants.MainType.VEHICLE));
        AssignButtonEvent("Button_35", () => GetType(AppConstants.MainType.CORE));
        AssignButtonEvent("Button_36", () => GetType(AppConstants.MainType.WEAPON));
        AssignButtonEvent("Button_37", () => GetType(AppConstants.MainType.ROBOT));
        AssignButtonEvent("Button_38", () => GetType(AppConstants.MainType.BADGE));
        AssignButtonEvent("Button_39", () => GetType(AppConstants.MainType.MECHA_BEAST));
        AssignButtonEvent("Button_40", () => GetType(AppConstants.MainType.RUNE));
        AssignButtonEvent("Button_41", () => GetType(AppConstants.MainType.FURNITURE));
        AssignButtonEvent("Button_42", () => GetType(AppConstants.MainType.FOOD));
        AssignButtonEvent("Button_43", () => GetType(AppConstants.MainType.BEVERAGE));
        AssignButtonEvent("Button_44", () => GetType(AppConstants.MainType.BUILDING));
        AssignButtonEvent("Button_45", () => GetType(AppConstants.MainType.PLANT));
        AssignButtonEvent("Button_46", () => GetType(AppConstants.MainType.FASHION));
        AssignButtonEvent("Button_47", () => GetType(AppConstants.MainType.EMOJI));
        AssignButtonEvent("Button_48", () => GetType(AppConstants.MainType.CARD_SOLDIER));
        AssignButtonEvent("Button_49", () => GetType(AppConstants.MainType.OUTFIT));
    }
    void AssignButtonEvent(string buttonName, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = collectionMenuPanel.Find(buttonName);
        if (buttonTransform != null)
        {
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    action();
                });
            }
        }
        else
        {
            Debug.LogWarning($"Button {buttonName} not found!");
        }
    }
    public void GetType(string type)
    {
        MainType = type;
        _ = CreateCollectionManagerAsync();
        TitleText.text = LocalizationManager.Get(type);
    }
    public async Task CreateCollectionManagerAsync()
    {
        // DictionaryPanel.SetActive(true);
        GameObject mainMenuObject = Instantiate(DictionaryPanelPrefab, MainPanel);
        Transform transform = mainMenuObject.transform;
        DictionaryContentPanel = transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
        RightScrollViewContentPanel = transform.Find("RightScrollView/Viewport/Content");
        LeftScrollViewContentPanel = transform.Find("Scroll View/Viewport/ButtonContent");
        PageText = transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = transform.Find("Pagination/Previous").GetComponent<Button>();
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        TMP_Dropdown rareDropdown = transform.Find("DictionaryCards/InputGroup/RareDropdown").GetComponent<TMP_Dropdown>();
        TMP_Dropdown typeDropdown = transform.Find("DictionaryCards/InputGroup/TypeDropdown").GetComponent<TMP_Dropdown>();
        TMP_InputField searchInputField = transform.Find("DictionaryCards/InputGroup/Search").GetComponent<TMP_InputField>();
        Button searchButton = transform.Find("DictionaryCards/InputGroup/SearchButton").GetComponent<Button>();
        CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePanel();
            Destroy(mainMenuObject);
        });
        HomeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener( () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(MainPanel);
        });
        NextButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            ChangeNextPage();
        });
        PreviousButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            ChangePreviousPage();
        });

        Image topBackgroundImage = transform.Find("DictionaryCards/TitleGroup/TopBackground").GetComponent<Image>();
        topBackgroundImage.material = UI_Green_Gradient_Radius_Mat_MaskPercent_70;
        TextMeshProUGUI subTitleText = transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
        subTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLECTION);

        // Transform CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");
        // IUserCurrenciesRepository userCurrencyRepository = new UserCurrenciesRepository();
        // UserCurrenciesService userCurrencyService = new UserCurrenciesService(userCurrencyRepository);
        // List<Currencies> currencies = new List<Currencies>();
        // currencies = await userCurrencyService.GetUserCurrencyAsync(User.CurrentUserId);
        // FindObjectOfType<CurrenciesManager>().GetMainCurrency(currencies, CurrencyPanel);

        searchButton.onClick.AddListener(() =>
        {
            Offset = 0;
            CurrentPage = 1;
            string searchText = searchInputField.text;
            Search = searchText;
            _ = LoadCurrentPageAsync();
        });

        List<string> uniqueRaries = QualityEvaluatorHelper.rarities;
        if (uniqueRaries.Count > 0)
        {
            rareDropdown.ClearOptions();
            rareDropdown.AddOptions(uniqueRaries);

            //Quan trọng: clear listener cũ trước
            rareDropdown.onValueChanged.RemoveAllListeners();

            // Gán sự kiện
            rareDropdown.onValueChanged.AddListener((index) =>
            {
                Offset = 0;
                CurrentPage = 1;
                // Lấy text đang chọn
                string selectedRare = rareDropdown.options[index].text;
                Rare = selectedRare;

                // Gọi async (fire & forget an toàn)
                _ = LoadCurrentPageAsync();
            });

            rareDropdown.value = 0;
            rareDropdown.RefreshShownValue();
        }

        List<string> uniqueTypes = await TypeManager.GetUniqueTypesAsync(MainType);
        uniqueTypes.Insert(0, AppConstants.Type.ALL);
        if (uniqueTypes.Count > 0)
        {
            typeDropdown.ClearOptions();
            typeDropdown.AddOptions(uniqueTypes);

            //Quan trọng: clear listener cũ trước
            typeDropdown.onValueChanged.RemoveAllListeners();

            // Gán sự kiện
            typeDropdown.onValueChanged.AddListener((index) =>
            {
                Offset = 0;
                CurrentPage = 1;
                // Lấy text đang chọn
                string selectedType = typeDropdown.options[index].text;
                Type = selectedType;

                // Gọi async (fire & forget an toàn)
                _ = LoadCurrentPageAsync();
            });

            typeDropdown.value = 0;
            typeDropdown.RefreshShownValue();
        }

        _ = LoadCurrentPageAsync();
        LoadAnimation();
    }
    public async Task LoadCurrentPageAsync()
    {
        int totalRecord = 0;
        ButtonEvent.Instance.Close(DictionaryContentPanel);
        if (MainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            var cardHeroesGalleryService = CardHeroesGalleryService.Create();
            List<CardHeroes> cardHeroes = await cardHeroesGalleryService.GetCardHeroesCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);

            totalRecord = await cardHeroesGalleryService.GetCardHeroesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BOOK))
        {
            var booksGalleryService = BooksGalleryService.Create();
            List<Books> books = await booksGalleryService.GetBooksCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = await booksGalleryService.GetBooksCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
            List<CardCaptains> cardCaptains = await cardCaptainsGalleryService.GetCardCaptainsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);

            totalRecord = await cardCaptainsGalleryService.GetCardCaptainsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            var collaborationEquipmentGalleryService = CollaborationEquipmentsGalleryService.Create();
            List<CollaborationEquipments> collaborationEquipments = await collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CollaborationEquipmentsGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = await collaborationEquipmentGalleryService.GetCollaborationEquipmentsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            var equipmentsGalleryService = EquipmentsGalleryService.Create();
            List<Equipments> equipments = await equipmentsGalleryService.GetEquipmentsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = await equipmentsGalleryService.GetEquipmentsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.PET))
        {
            var petsGalleryService = PetsGalleryService.Create();
            List<Pets> pets = await petsGalleryService.GetPetsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = await petsGalleryService.GetPetsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SKILL))
        {
            var skillsGalleryService = SkillsGalleryService.Create();
            List<Skills> skills = await skillsGalleryService.GetSkillsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = await skillsGalleryService.GetSkillsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SYMBOL))
        {
            var symbolsGalleryService = SymbolsGalleryService.Create();
            List<Symbols> symbols = await symbolsGalleryService.GetSymbolsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = await symbolsGalleryService.GetSymbolsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            var cardMilitaryGalleryService = CardMilitariesGalleryService.Create();
            List<CardMilitaries> cardMilitaries = await cardMilitaryGalleryService.GetCardMilitariesCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardMilitariesGalleryController.Instance.CreateCardMilitariesGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = await cardMilitaryGalleryService.GetCardMilitariesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            var cardSpellGalleryService = CardSpellsGalleryService.Create();
            List<CardSpells> cardSpells = await cardSpellGalleryService.GetCardSpellsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardSpellsGalleryController.Instance.CreateCardSpellsGallery(cardSpells, DictionaryContentPanel);

            totalRecord = await cardSpellGalleryService.GetCardSpellsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            var collaborationGalleryService = CollaborationsGalleryService.Create();
            List<Collaborations> collaborations = await collaborationGalleryService.GetCollaborationsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            CollaborationsGalleryController.Instance.CreateCollaborationsGallery(collaborations, DictionaryContentPanel);

            totalRecord = await collaborationGalleryService.GetCollaborationsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MEDAL))
        {
            var medalsGalleryService = MedalsGalleryService.Create();
            List<Medals> medals = await medalsGalleryService.GetMedalsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);

            totalRecord = await medalsGalleryService.GetMedalsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TITLE))
        {
            var titlesGalleryService = TitlesGalleryService.Create();
            List<Titles> titles = await titlesGalleryService.GetTitlesCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = await titlesGalleryService.GetTitlesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BORDER))
        {
            var bordersGalleryService = BordersGalleryService.Create();
            List<Borders> borders = await bordersGalleryService.GetBordersCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = await bordersGalleryService.GetBordersCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            var magicFormationCircleGalleryService = MagicFormationCirclesGalleryService.Create();
            List<MagicFormationCircles> magicFormationCircles = await magicFormationCircleGalleryService.GetMagicFormationCirclesCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            MagicFormationCirclesGalleryController.Instance.CreateMagicFormationCirclesGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = await magicFormationCircleGalleryService.GetMagicFormationCirclesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.RELIC))
        {
            var relicsGalleryService = RelicsGalleryService.Create();
            List<Relics> relics = await relicsGalleryService.GetRelicsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = await relicsGalleryService.GetRelicsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            var cardMonstersGalleryService = CardMonstersGalleryService.Create();
            List<CardMonsters> monsters = await cardMonstersGalleryService.GetCardMonstersCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);

            totalRecord = await cardMonstersGalleryService.GetCardMonstersCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            var cardColonelsGalleryService = CardColonelsGalleryService.Create();
            List<CardColonels> cardColonels = await cardColonelsGalleryService.GetCardColonelsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = await cardColonelsGalleryService.GetCardColonelsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
            List<CardGenerals> cardGenerals = await cardGeneralsGalleryService.GetCardGeneralsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = await cardGeneralsGalleryService.GetCardGeneralsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
            List<CardAdmirals> cardAdmirals = await cardAdmiralsGalleryService.GetCardAdmiralsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = await cardAdmiralsGalleryService.GetCardAdmiralsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TALISMAN))
        {
            var talismanGalleryService = TalismansGalleryService.Create();
            List<Talismans> talismans = await talismanGalleryService.GetTalismansCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            TalismansGalleryController.Instance.CreateTalismansGallery(talismans, DictionaryContentPanel);

            totalRecord = await talismanGalleryService.GetTalismansCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.PUPPET))
        {
            var puppetGalleryService = PuppetsGalleryService.Create();
            List<Puppets> puppets = await puppetGalleryService.GetPuppetsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            PuppetsGalleryController.Instance.CreatePuppetsGallery(puppets, DictionaryContentPanel);

            totalRecord = await puppetGalleryService.GetPuppetsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            var alchemyGalleryService = AlchemiesGalleryService.Create();
            List<Alchemies> alchemies = await alchemyGalleryService.GetAlchemiesCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            AlchemiesGalleryController.Instance.CreateAlchemiesGallery(alchemies, DictionaryContentPanel);

            totalRecord = await alchemyGalleryService.GetAlchemyCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FORGE))
        {
            var forgeGalleryService = ForgesGalleryService.Create();
            List<Forges> forges = await forgeGalleryService.GetForgesCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            ForgesGalleryController.Instance.CreateForgesGallery(forges, DictionaryContentPanel);

            totalRecord = await forgeGalleryService.GetForgesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            var cardLifeGalleryService = CardLivesGalleryService.Create();
            List<CardLives> cardLives = await cardLifeGalleryService.GetCardLivesCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardLivesGalleryController.Instance.CreateCardLivesGallery(cardLives, DictionaryContentPanel);

            totalRecord = await cardLifeGalleryService.GetCardLivesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARTWORK))
        {
            var artworkGalleryService = ArtworksGalleryService.Create();
            List<Artworks> artworks = await artworkGalleryService.GetArtworksCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            ArtworksGalleryController.Instance.CreateArtworksGallery(artworks, DictionaryContentPanel);

            totalRecord = await artworkGalleryService.GetArtworksCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            var spiritBeastGalleryService = SpiritBeastsGalleryService.Create();
            List<SpiritBeasts> spiritBeasts = await spiritBeastGalleryService.GetSpiritBeastsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            SpiritBeastsGalleryController.Instance.CreateSpiritBeastsGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = await spiritBeastGalleryService.GetSpiritBeastsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.AVATAR))
        {
            var avatarsGalleryService = AvatarsGalleryService.Create();
            List<Avatars> avatars = await avatarsGalleryService.GetAvatarsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            AvatarsGalleryController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

            totalRecord = await avatarsGalleryService.GetAvatarsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            var spiritCardGalleryService = SpiritCardsGalleryService.Create();
            List<SpiritCards> spiritCards = await spiritCardGalleryService.GetSpiritCardsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            SpiritCardsGalleryController.Instance.CreateSpiritCardsGallery(spiritCards, DictionaryContentPanel);

            totalRecord = await spiritCardGalleryService.GetSpiritCardsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            var achievementsGalleryService = AchievementsGalleryService.Create();
            List<Achievements> achievements = await achievementsGalleryService.GetAchievementsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            AchievementsGalleryController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

            totalRecord = await achievementsGalleryService.GetAchievementsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            var artifactsGalleryService = ArtifactsGalleryService.Create();
            List<Artifacts> artifacts = await artifactsGalleryService.GetArtifactsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            ArtifactsGalleryController.Instance.CreateArtifactsGallery(artifacts, DictionaryContentPanel);

            totalRecord = await artifactsGalleryService.GetArtifactsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            var architecturesGalleryService = ArchitecturesGalleryService.Create();
            List<Architectures> architectures = await architecturesGalleryService.GetArchitecturesCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            ArchitecturesGalleryController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

            totalRecord = await architecturesGalleryService.GetArchitecturesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            var technologiesGalleryService = TechnologiesGalleryService.Create();
            List<Technologies> technologies = await technologiesGalleryService.GetTechnologiesCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            TechnologiesGalleryController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

            totalRecord = await technologiesGalleryService.GetTechnologiesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.VEHICLE))
        {
            var vehiclesGalleryService = VehiclesGalleryService.Create();
            List<Vehicles> vehicles = await vehiclesGalleryService.GetVehiclesCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            VehiclesGalleryController.Instance.CreateVehiclesGallery(vehicles, DictionaryContentPanel);

            totalRecord = await vehiclesGalleryService.GetVehiclesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CORE))
        {
            var coresGalleryService = CoresGalleryService.Create();
            List<Cores> cores = await coresGalleryService.GetCoresCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            CoresGalleryController.Instance.CreateCoresGallery(cores, DictionaryContentPanel);

            totalRecord = await coresGalleryService.GetCoresCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.WEAPON))
        {
            var weaponsGalleryService = WeaponsGalleryService.Create();
            List<Weapons> weapons = await weaponsGalleryService.GetWeaponsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            WeaponsGalleryController.Instance.CreateWeaponsGallery(weapons, DictionaryContentPanel);

            totalRecord = await weaponsGalleryService.GetWeaponsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ROBOT))
        {
            var robotsGalleryService = RobotsGalleryService.Create();
            List<Robots> robots = await robotsGalleryService.GetRobotsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            RobotsGalleryController.Instance.CreateRobotsGallery(robots, DictionaryContentPanel);

            totalRecord = await robotsGalleryService.GetRobotsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BADGE))
        {
            var badgesGalleryService = BadgesGalleryService.Create();
            List<Badges> badges = await badgesGalleryService.GetBadgesCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            BadgesGalleryController.Instance.CreateBadgesGallery(badges, DictionaryContentPanel);

            totalRecord = await badgesGalleryService.GetBadgesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            var mechaBeastsGalleryService = MechaBeastsGalleryService.Create();
            List<MechaBeasts> mechaBeasts = await mechaBeastsGalleryService.GetMechaBeastsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            MechaBeastsGalleryController.Instance.CreateMechaBeastsGallery(mechaBeasts, DictionaryContentPanel);

            totalRecord = await mechaBeastsGalleryService.GetMechaBeastsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.RUNE))
        {
            var runesGalleryService = RunesGalleryService.Create();
            List<Runes> runes = await runesGalleryService.GetRunesCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            RunesGalleryController.Instance.CreateRunesGallery(runes, DictionaryContentPanel);

            totalRecord = await runesGalleryService.GetRunesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FURNITURE))
        {
            var furnituresGalleryService = FurnituresGalleryService.Create();
            List<Furnitures> furnitures = await furnituresGalleryService.GetFurnituresCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            FurnituresGalleryController.Instance.CreateFurnituresGallery(furnitures, DictionaryContentPanel);

            totalRecord = await furnituresGalleryService.GetFurnituresCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FOOD))
        {
            var foodsGalleryService = FoodsGalleryService.Create();
            List<Foods> foods = await foodsGalleryService.GetFoodsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            FoodsGalleryController.Instance.CreateFoodsGallery(foods, DictionaryContentPanel);

            totalRecord = await foodsGalleryService.GetFoodsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            var beveragesGalleryService = BeveragesGalleryService.Create();
            List<Beverages> beverages = await beveragesGalleryService.GetBeveragesCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            BeveragesGalleryController.Instance.CreateBeveragesGallery(beverages, DictionaryContentPanel);

            totalRecord = await beveragesGalleryService.GetBeveragesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BUILDING))
        {
            var buildingsGalleryService = BuildingsGalleryService.Create();
            List<Buildings> buildings = await buildingsGalleryService.GetBuildingsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            BuildingsGalleryController.Instance.CreateBuildingsGallery(buildings, DictionaryContentPanel);

            totalRecord = await buildingsGalleryService.GetBuildingsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.PLANT))
        {
            var plantsGalleryService = PlantsGalleryService.Create();
            List<Plants> plants = await plantsGalleryService.GetPlantsCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            PlantsGalleryController.Instance.CreatePlantsGallery(plants, DictionaryContentPanel);

            totalRecord = await plantsGalleryService.GetPlantsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FASHION))
        {
            var fashionsGalleryService = FashionsGalleryService.Create();
            List<Fashions> fashions = await fashionsGalleryService.GetFashionsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            FashionsGalleryController.Instance.CreateFashionsGallery(fashions, DictionaryContentPanel);

            totalRecord = await fashionsGalleryService.GetFashionsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.EMOJI))
        {
            var emojisRepository = EmojisGalleryService.Create();
            List<Emojis> emojis = await emojisRepository.GetEmojisCollectionAsync(Search, PAGE_SIZE, Offset, Rare);
            EmojisGalleryController.Instance.CreateEmojisGallery(emojis, DictionaryContentPanel);

            totalRecord = await emojisRepository.GetEmojisCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            var cardSoldiersGalleryService = CardSoldiersGalleryService.Create();
            List<CardSoldiers> cardSoldiers = await cardSoldiersGalleryService.GetCardSoldiersCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            CardSoldiersGalleryController.Instance.CreateCardSoldiersGallery(cardSoldiers, DictionaryContentPanel);

            totalRecord = await cardSoldiersGalleryService.GetCardSoldiersCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.OUTFIT))
        {
            var outfitsGalleryService = OutfitsGalleryService.Create();
            List<Outfits> outfits = await outfitsGalleryService.GetOutfitsCollectionAsync(Search, Type, PAGE_SIZE, Offset, Rare);
            OutfitsGalleryController.Instance.CreateOutfitsGallery(outfits, DictionaryContentPanel);

            totalRecord = await outfitsGalleryService.GetOutfitsCountAsync(Search, Type, Rare);
        }
        TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
        PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
    }
    public void ClearAllPrefabs()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        foreach (Transform child in DictionaryContentPanel)
        {
            Destroy(child.gameObject);
        }
    }
    public void ClearAllButton()
    {
        foreach (Transform child in LeftScrollViewContentPanel)
        {
            Destroy(child.gameObject);
        }
    }
    public void ChangeNextPage()
    {
        if (CurrentPage < TotalPage)
        {
            ClearAllPrefabs();
            CurrentPage = CurrentPage + 1;
            Offset = Offset + PAGE_SIZE;
            _ = LoadCurrentPageAsync();

            PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();

        }
    }
    public void ChangePreviousPage()
    {
        if (CurrentPage > 1)
        {
            ClearAllPrefabs();
            CurrentPage = CurrentPage - 1;
            Offset = Offset - PAGE_SIZE;
            _ = LoadCurrentPageAsync();

            PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();

        }
    }
    public void ClosePanel()
    {
        ClearAllButton();
        ClearAllPrefabs();
        Offset = 0;
        CurrentPage = 1;
        // foreach (Transform child in MainPanel)
        // {
        //     Destroy(child.gameObject);
        // }
    }
    public void Close(Transform content)
    {
        Offset = 0;
        CurrentPage = 1;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void LoadAnimation()
    {
        LeftScrollViewContentPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
        RightScrollViewContentPanel.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
}
