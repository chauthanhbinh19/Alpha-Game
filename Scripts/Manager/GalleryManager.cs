using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Threading.Tasks;

public class GalleryManager : MonoBehaviour
{
    private GameObject GalleryButtonPrefab;
    private Transform GalleryMenuPanel;
    private GameObject DictionaryPanelPrefab;
    private Transform DictionaryContentPanel;
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
    private Material UI_Blue_Gradient_Radius_Mat_MaskPercent_70;
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
    public static GalleryManager Instance { get; private set; }
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
        GalleryButtonPrefab = UIManager.Instance.Get("GalleryButtonPrefab");
    }
    public void CreateGalleryButton(Transform tempGalleryMenuPanel)
    {
        Texture2D itemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_GALLERY_URL);
        //Gallery menu
        CreateGalleryButtonUI(1, AppDisplayConstants.Gallery.CARD_HEROES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(2, AppDisplayConstants.Gallery.BOOKS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BOOK_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(3, AppDisplayConstants.Gallery.PETS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PET_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(4, AppDisplayConstants.Gallery.CARD_CAPTAINS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_CAPTAIN_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(5, AppDisplayConstants.Gallery.COLLABORATION_EQUIPMENTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_EQUIPMENT_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(6, AppDisplayConstants.Gallery.CARD_MILITARIES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MILITARY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MILITARY_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(7, AppDisplayConstants.Gallery.CARD_SPELLS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SPELL_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(8, AppDisplayConstants.Gallery.COLLABORATIONS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(9, AppDisplayConstants.Gallery.CARD_MONSTERS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MONSTER_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(10, AppDisplayConstants.Gallery.EQUIPMENTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.EQUIPMENT_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(11, AppDisplayConstants.Gallery.MEDALS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MEDAL_URL),
             tempGalleryMenuPanel);
        CreateGalleryButtonUI(12, AppDisplayConstants.Gallery.SKILLS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SKILL_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(13, AppDisplayConstants.Gallery.SYMBOLS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SYMBOL_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(14, AppDisplayConstants.Gallery.TITLES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TITLE_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(15, AppDisplayConstants.Gallery.MAGIC_FORMATION_CIRCLES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MAGIC_FORMATION_CIRCLE_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(16, AppDisplayConstants.Gallery.RELICS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.RELIC_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(17, AppDisplayConstants.Gallery.CARD_COLONELS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_COLONEL_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(18, AppDisplayConstants.Gallery.CARD_GENERALS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_GENERAL_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(19, AppDisplayConstants.Gallery.CARD_ADMIRALS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_ADMIRAL_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(20, AppDisplayConstants.Gallery.BORDERS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BORDER_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(21, AppDisplayConstants.Gallery.TALISMANS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TALISMAN_URL),  
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(22, AppDisplayConstants.Gallery.PUPPETS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PUPPET_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(23, AppDisplayConstants.Gallery.ALCHEMIES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ALCHEMY_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(24, AppDisplayConstants.Gallery.FORGES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FORGE_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(25, AppDisplayConstants.Gallery.CARD_LIVES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_LIFE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_LIFE_URL),
             tempGalleryMenuPanel);
        CreateGalleryButtonUI(26, AppDisplayConstants.Gallery.ARTWORKS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTWORK_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(27, AppDisplayConstants.Gallery.SPIRIT_BEASTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_BEAST_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(28, AppDisplayConstants.Gallery.AVATARS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.AVATAR_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.AVATAR_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(29, AppDisplayConstants.Gallery.SPIRIT_CARDS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_CARD_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(30, AppDisplayConstants.Gallery.ACHIEVEMENTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ACHIEVEMENT_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(31, AppDisplayConstants.Gallery.ARTIFACTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTIFACT_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(32, AppDisplayConstants.Gallery.ARCHITECTURES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARCHITECTURE_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(33, AppDisplayConstants.Gallery.TECHNOLOGIES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TECHNOLOGY_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(34, AppDisplayConstants.Gallery.VEHICLES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.VEHICLE_URL),
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(35, AppDisplayConstants.Gallery.CORES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CORE_URL),
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(36, AppDisplayConstants.Gallery.WEAPONS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.WEAPON_URL),  
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(37, AppDisplayConstants.Gallery.ROBOTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ROBOT_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(38, AppDisplayConstants.Gallery.BADGES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BADGE_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(39, AppDisplayConstants.Gallery.MECHA_BEASTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MECHA_BEAST_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(40, AppDisplayConstants.Gallery.RUNES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.RUNE_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(41, AppDisplayConstants.Gallery.FURNITURES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FURNITURE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FURNITURE_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(42, AppDisplayConstants.Gallery.FOODS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FOOD_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FOOD_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(43, AppDisplayConstants.Gallery.BEVERAGES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BEVERAGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BEVERAGE_URL),
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(44, AppDisplayConstants.Gallery.BUILDINGS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BUILDING_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(45, AppDisplayConstants.Gallery.PLANTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PLANT_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(46, AppDisplayConstants.Gallery.FASHIONS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FASHION_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(47, AppDisplayConstants.Gallery.EMOJIS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.EMOJI_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(48, AppDisplayConstants.Gallery.CARD_SOLDIERS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SOLDIER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SOLDIER_URL), 
            tempGalleryMenuPanel);
        CreateGalleryButtonUI(49, AppDisplayConstants.Gallery.OUTFITS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.OUTFIT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.OUTFIT_URL), 
            tempGalleryMenuPanel);

        tempGalleryMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateGalleryButtonUI(int index, string itemName, Texture2D _itemBackground, Texture2D _itemImage, Texture2D _borderImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(GalleryButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho subBackground
        RawImage subBackground = newButton.transform.Find("SubBackground").GetComponent<RawImage>();
        if (subBackground != null && _itemBackground != null)
        {
            subBackground.texture = _itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && _itemImage != null)
        {
            image.texture = _itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
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
            itemBackground.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Flag.FLAG_GALLERY_URL);
        }
    }
    public void CreateGallery(Transform tempGalleryMenuPanel)
    {
        GalleryMenuPanel = tempGalleryMenuPanel;
        DictionaryPanelPrefab = UIManager.Instance.Get("DictionaryPanelPrefab");
        UI_Blue_Gradient_Radius_Mat_MaskPercent_70 = MaterialManager.Instance.Get("UI_Blue_Gradient_Radius_Mat_MaskPercent_70");
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
        // GetCardsType();
    }
    void AssignButtonEvent(string buttonName, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = GalleryMenuPanel.Find(buttonName);
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
        MainType = type; // Gán giá trị cho mainType
        _ = CreateGalleryManagerAsync(); // Gọi hàm xử lý
        TitleText.text = LocalizationManager.Get(type);
    }
    public async Task CreateGalleryManagerAsync()
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
        topBackgroundImage.material = UI_Blue_Gradient_Radius_Mat_MaskPercent_70;
        TextMeshProUGUI subTitleText = transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
        subTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.GALLERY);

        // Transform CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");
        // List<Currencies> currencies = new List<Currencies>();
        // currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
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
            List<CardHeroes> cardHeroes = await CardHeroesService.Create().GetCardHeroesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardHeroesController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);

            totalRecord = await CardHeroesService.Create().GetCardHeroesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BOOK))
        {
            List<Books> books = await BooksService.Create().GetBooksAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = await BooksService.Create().GetBooksCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> captains = await CardCaptainsService.Create().GetCardCaptainsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

            totalRecord = await CardCaptainsService.Create().GetCardCaptainsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            List<CollaborationEquipments> collaborationEquipments = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CollaborationEquipmentsController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            List<Equipments> equipments = await EquipmentsService.Create().GetEquipmentsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = await EquipmentsService.Create().GetEquipmentsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.PET))
        {
            List<Pets> pets = await PetsService.Create().GetPetsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = await PetsService.Create().GetPetsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SKILL))
        {
            List<Skills> skills = await SkillsService.Create().GetSkillsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = await SkillsService.Create().GetSkillsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SYMBOL))
        {
            List<Symbols> symbols = await SymbolsService.Create().GetSymbolsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = await SymbolsService.Create().GetSymbolsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create().GetCardMilitariesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardMilitariesController.Instance.CreateCardMilitariesGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = await CardMilitariesService.Create().GetCardMilitariesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpells = await CardSpellsService.Create().GetCardSpellsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardSpellsController.Instance.CreateCardSpellsGallery(cardSpells, DictionaryContentPanel);

            totalRecord = await CardSpellsService.Create().GetCardSpellsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            List<Collaborations> collaborations = await CollaborationsService.Create().GetCollaborationsAsync(Search, Rare, PAGE_SIZE, Offset);
            CollaborationsController.Instance.CreateCollaborationsGallery(collaborations, DictionaryContentPanel);

            totalRecord = await CollaborationsService.Create().GetCollaborationsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MEDAL))
        {
            List<Medals> medalsList = await MedalsService.Create().GetMedalsAsync(Search, Rare, PAGE_SIZE, Offset);
            MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

            totalRecord = await MedalsService.Create().GetMedalsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TITLE))
        {
            List<Titles> titles = await TitlesService.Create().GetTitlesAsync(Search, Rare, PAGE_SIZE, Offset);
            TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = await TitlesService.Create().GetTitlesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BORDER))
        {
            List<Borders> borders = await BordersService.Create().GetBordersAsync(Search, Rare, PAGE_SIZE, Offset);
            BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = await BordersService.Create().GetBordersCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            List<MagicFormationCircles> magicFormationCircles = await MagicFormationCirclesService.Create().GetMagicFormationCirclesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            MagicFormationCirclesController.Instance.CreateMagicFormationCirclesGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = await MagicFormationCirclesService.Create().GetMagicFormationCirclesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.RELIC))
        {
            List<Relics> relics = await RelicsService.Create().GetRelicsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = await RelicsService.Create().GetRelicsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonsters = await CardMonstersService.Create().GetCardMonstersAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

            totalRecord = await CardMonstersService.Create().GetCardMonstersCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonels = await CardColonelsService.Create().GetCardColonelsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = await CardColonelsService.Create().GetCardColonelsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGenerals = await CardGeneralsService.Create().GetCardGeneralsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = await CardGeneralsService.Create().GetCardGeneralsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create().GetCardAdmiralsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = await CardAdmiralsService.Create().GetCardAdmiralsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TALISMAN))
        {
            List<Talismans> talismans = await TalismansService.Create().GetTalismansAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            TalismansController.Instance.CreateTalismansGallery(talismans, DictionaryContentPanel);

            totalRecord = await TalismansService.Create().GetTalismansCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.PUPPET))
        {
            List<Puppets> puppets = await PuppetsService.Create().GetPuppetsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            PuppetsController.Instance.CreatePuppetsGallery(puppets, DictionaryContentPanel);

            totalRecord = await PuppetsService.Create().GetPuppetsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            List<Alchemies> alchemies = await AlchemiesService.Create().GetAlchemiesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            AlchemiesController.Instance.CreateAlchemiesGallery(alchemies, DictionaryContentPanel);

            totalRecord = await AlchemiesService.Create().GetAlchemiesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FORGE))
        {
            List<Forges> forges = await ForgesService.Create().GetForgesAsync(Search, Rare, Type, PAGE_SIZE, Offset);
            ForgesController.Instance.CreateForgesGallery(forges, DictionaryContentPanel);

            totalRecord = await ForgesService.Create().GetForgesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            List<CardLives> cardLives = await CardLivesService.Create().GetCardLivesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardLivesController.Instance.CreateCardLivesGallery(cardLives, DictionaryContentPanel);

            totalRecord = await CardLivesService.Create().GetCardLivesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARTWORK))
        {
            List<Artworks> artworks = await ArtworksService.Create().GetArtworksAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            ArtworksController.Instance.CreateArtworksGallery(artworks, DictionaryContentPanel);

            totalRecord = await ArtworksService.Create().GetArtworksCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            List<SpiritBeasts> spiritBeasts = await SpiritBeastsService.Create().GetSpiritBeastsAsync(Search, Rare, PAGE_SIZE, Offset);
            SpiritBeastsController.Instance.CreateSpiritBeastsGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = await SpiritBeastsService.Create().GetSpiritBeastsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.AVATAR))
        {
            List<Avatars> avatars = await AvatarsService.Create().GetAvatarsAsync(Search, Rare, PAGE_SIZE, Offset);
            AvatarsController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

            totalRecord = await AvatarsService.Create().GetAvatarsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            List<SpiritCards> spiritCards = await SpiritCardsService.Create().GetSpiritCardsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            SpiritCardsController.Instance.CreateSpiritCardsGallery(spiritCards, DictionaryContentPanel);

            totalRecord = await SpiritCardsService.Create().GetSpiritCardsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            List<Achievements> achievements = await AchievementsService.Create().GetAchievementsAsync(Search, Rare, PAGE_SIZE, Offset);
            AchievementsController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

            totalRecord = await AchievementsService.Create().GetAchievementsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            List<Artifacts> artifacts = await ArtifactsService.Create().GetArtifactsAsync(Search, Rare, PAGE_SIZE, Offset);
            ArtifactsController.Instance.CreateArtifactsGallery(artifacts, DictionaryContentPanel);

            totalRecord = await ArtifactsService.Create().GetArtifactsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            List<Architectures> architectures = await ArchitecturesService.Create().GetArchitecturesAsync(Search, Rare, PAGE_SIZE, Offset);
            ArchitecturesController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

            totalRecord = await ArchitecturesService.Create().GetArchitecturesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            List<Technologies> technologies = await TechnologiesService.Create().GetTechnologiesAsync(Search, Rare, PAGE_SIZE, Offset);
            TechnologiesController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

            totalRecord = await TechnologiesService.Create().GetTechnologiesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.VEHICLE))
        {
            List<Vehicles> vehicles = await VehiclesService.Create().GetVehiclesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            VehiclesController.Instance.CreateVehiclesGallery(vehicles, DictionaryContentPanel);

            totalRecord = await VehiclesService.Create().GetVehiclesCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CORE))
        {
            List<Cores> cores = await CoresService.Create().GetCoresAsync(Search, Rare, PAGE_SIZE, Offset);
            CoresController.Instance.CreateCoresGallery(cores, DictionaryContentPanel);

            totalRecord = await CoresService.Create().GetCoresCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.WEAPON))
        {
            List<Weapons> weapons = await WeaponsService.Create().GetWeaponsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            WeaponsController.Instance.CreateWeaponsGallery(weapons, DictionaryContentPanel);

            totalRecord = await WeaponsService.Create().GetWeaponsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ROBOT))
        {
            List<Robots> robots = await RobotsService.Create().GetRobotsAsync(Search, Rare, PAGE_SIZE, Offset);
            RobotsController.Instance.CreateRobotsGallery(robots, DictionaryContentPanel);

            totalRecord = await RobotsService.Create().GetRobotsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BADGE))
        {
            List<Badges> badges = await BadgesService.Create().GetBadgesAsync(Search, Rare, PAGE_SIZE, Offset);
            BadgesController.Instance.CreateBadgesGallery(badges, DictionaryContentPanel);

            totalRecord = await BadgesService.Create().GetBadgesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            List<MechaBeasts> mechaBeasts = await MechaBeastsService.Create().GetMechaBeastsAsync(Search, Rare, PAGE_SIZE, Offset);
            MechaBeastsController.Instance.CreateMechaBeastsGallery(mechaBeasts, DictionaryContentPanel);

            totalRecord = await MechaBeastsService.Create().GetMechaBeastsCountAsync(Search,Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.RUNE))
        {
            List<Runes> runes = await RunesService.Create().GetRunesAsync(Search, Rare, PAGE_SIZE, Offset);
            RunesController.Instance.CreateRunesGallery(runes, DictionaryContentPanel);

            totalRecord = await RunesService.Create().GetRunesCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FURNITURE))
        {
            List<Furnitures> furnitures = await FurnituresService.Create().GetFurnituresAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            FurnituresController.Instance.CreateFurnituresGallery(furnitures, DictionaryContentPanel);

            totalRecord = await FurnituresService.Create().GetFurnituresCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FOOD))
        {
            List<Foods> foods = await FoodsService.Create().GetFoodsAsync(Search, Rare, PAGE_SIZE, Offset);
            FoodsController.Instance.CreateFoodsGallery(foods, DictionaryContentPanel);

            totalRecord = await FoodsService.Create().GetFoodsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            List<Beverages> beverages = await BeveragesService.Create().GetBeveragesAsync(Search, Rare, PAGE_SIZE, Offset);
            BeveragesController.Instance.CreateBeveragesGallery(beverages, DictionaryContentPanel);

            totalRecord = await BeveragesService.Create().GetBeveragesCountAsync(Search,Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BUILDING))
        {
            List<Buildings> buildings = await BuildingsService.Create().GetBuildingsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            BuildingsController.Instance.CreateBuildingsGallery(buildings, DictionaryContentPanel);

            totalRecord = await BuildingsService.Create().GetBuildingsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.PLANT))
        {
            List<Plants> plants = await PlantsService.Create().GetPlantsAsync(Search, Rare, PAGE_SIZE, Offset);
            PlantsController.Instance.CreatePlantsGallery(plants, DictionaryContentPanel);

            totalRecord = await PlantsService.Create().GetPlantsCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FASHION))
        {
            List<Fashions> fashions = await FashionsService.Create().GetFashionsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            FashionsController.Instance.CreateFashionsGallery(fashions, DictionaryContentPanel);

            totalRecord = await FashionsService.Create().GetFashionsCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.EMOJI))
        {
            List<Emojis> plants = await EmojisService.Create().GetEmojisAsync(Search, Rare, PAGE_SIZE, Offset);
            EmojisController.Instance.CreateEmojisGallery(plants, DictionaryContentPanel);

            totalRecord = await EmojisService.Create().GetEmojisCountAsync(Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            List<CardSoldiers> cardSoldiers = await CardSoldiersService.Create().GetCardSoldiersAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            CardSoldiersController.Instance.CreateCardSoldiersGallery(cardSoldiers, DictionaryContentPanel);

            totalRecord = await CardSoldiersService.Create().GetCardSoldiersCountAsync(Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.OUTFIT))
        {
            List<Outfits> outfits = await OutfitsService.Create().GetOutfitsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
            OutfitsController.Instance.CreateOutfitsGallery(outfits, DictionaryContentPanel);

            totalRecord = await OutfitsService.Create().GetOutfitsCountAsync(Search, Type, Rare);
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
        // Duyệt qua tất cả các con cái của cardsContent
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
