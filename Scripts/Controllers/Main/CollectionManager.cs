using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Threading.Tasks;

public class CollectionManager : MonoBehaviour
{
    private GameObject ItemButtonPrefab;
    private Transform collectionMenuPanel;
    private GameObject TypeButtonPrefab;
    private GameObject DictionaryPanelPrefab;
    private GameObject RareButtonPrefab;
    private Transform DictionaryContentPanel;
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
    private Material UI_Green_Gradient_Radius_Mat_MaskPercent_70;
    private Transform MainPanel;
    private Button CloseButton;
    private Button HomeButton;
    //Variable for pagination
    private int offset;
    private int currentPage;
    private int totalPage;
    private const int PAGE_SIZE = 100;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private string mainType;
    private string subType;
    private TextMeshProUGUI titleText;
    private string search;
    private string type;
    private string rare;
    public List<Button> rareTabButtons;
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
        ItemButtonPrefab = UIManager.Instance.Get("ItemButtonPrefab");
    }
    public void CreateCollectionButton(Transform collectionMenuPanel)
    {
        Texture2D itemBackground = Resources.Load<Texture2D>(ImageConstants.Badge.BADGE_COLLECTION_URL);
        //Collection menu
        CreateCollectionButtonUI(1, AppDisplayConstants.Collection.CARD_HEROES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_HERO_URL), collectionMenuPanel);
        CreateCollectionButtonUI(2, AppDisplayConstants.Collection.BOOKS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.BOOK_URL), collectionMenuPanel);
        CreateCollectionButtonUI(3, AppDisplayConstants.Collection.PETS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.PET_URL), collectionMenuPanel);
        CreateCollectionButtonUI(4, AppDisplayConstants.Collection.CARD_CAPTAINS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_CAPTAIN_URL), collectionMenuPanel);
        CreateCollectionButtonUI(5, AppDisplayConstants.Collection.COLLABORATION_EQUIPMENTS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.COLLABORATION_EQUIPMENT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(6, AppDisplayConstants.Collection.CARD_MILITARIES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_MILITARY_URL), collectionMenuPanel);
        CreateCollectionButtonUI(7, AppDisplayConstants.Collection.CARD_SPELLS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_SPELL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(8, AppDisplayConstants.Collection.COLLABORATIONS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.COLLABORATION_URL), collectionMenuPanel);
        CreateCollectionButtonUI(9, AppDisplayConstants.Collection.CARD_MONSTERS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_MONSTER_URL), collectionMenuPanel);
        CreateCollectionButtonUI(10, AppDisplayConstants.Collection.EQUIPMENTS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.EQUIPMENT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(11, AppDisplayConstants.Collection.MEDALS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.MEDAL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(12, AppDisplayConstants.Collection.SKILLS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.SKILL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(13, AppDisplayConstants.Collection.SYMBOLS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.SYMBOL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(14, AppDisplayConstants.Collection.TITLES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.TITLE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(15, AppDisplayConstants.Collection.MAGIC_FORMATION_CIRCLES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.MAGIC_FORMATION_CIRCLE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(16, AppDisplayConstants.Collection.RELICS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.RELIC_URL), collectionMenuPanel);
        CreateCollectionButtonUI(17, AppDisplayConstants.Collection.CARD_COLONELS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_COLONEL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(18, AppDisplayConstants.Collection.CARD_GENERALS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_GENERAL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(19, AppDisplayConstants.Collection.CARD_ADMIRALS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_ADMIRAL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(20, AppDisplayConstants.Collection.BORDERS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.BORDER_URL), collectionMenuPanel);
        CreateCollectionButtonUI(21, AppDisplayConstants.Collection.TALISMANS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.TALISMAN_URL), collectionMenuPanel);
        CreateCollectionButtonUI(22, AppDisplayConstants.Collection.PUPPETS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.PUPPET_URL), collectionMenuPanel);
        CreateCollectionButtonUI(23, AppDisplayConstants.Collection.ALCHEMIES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.ALCHEMY_URL), collectionMenuPanel);
        CreateCollectionButtonUI(24, AppDisplayConstants.Collection.FORGES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.FORGE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(25, AppDisplayConstants.Collection.CARD_LIVES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.LIFE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(26, AppDisplayConstants.Collection.ARTWORKS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.ARTWORK_URL), collectionMenuPanel);
        CreateCollectionButtonUI(27, AppDisplayConstants.Collection.SPIRIT_BEASTS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.SPIRIT_BEAST_URL), collectionMenuPanel);
        CreateCollectionButtonUI(28, AppDisplayConstants.Collection.AVATARS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.AVATAR_URL), collectionMenuPanel);
        CreateCollectionButtonUI(29, AppDisplayConstants.Collection.SPIRIT_CARDS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.SPIRIT_CARD_URL), collectionMenuPanel);
        CreateCollectionButtonUI(30, AppDisplayConstants.Collection.ACHIEVEMENTS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.ACHIEVEMENT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(31, AppDisplayConstants.Collection.CARDS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_URL), collectionMenuPanel);
        CreateCollectionButtonUI(32, AppDisplayConstants.Collection.ARCHITECTURES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.ARCHITECTURE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(33, AppDisplayConstants.Collection.TECHNOLOGIES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.TECHNOLOGY_URL), collectionMenuPanel);
        CreateCollectionButtonUI(34, AppDisplayConstants.Collection.VEHICLES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.VEHICLE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(35, AppDisplayConstants.Collection.CORES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.CORE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(36, AppDisplayConstants.Collection.WEAPOMS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.WEAPON_URL), collectionMenuPanel);
        CreateCollectionButtonUI(37, AppDisplayConstants.Collection.ROBOTS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.ROBOT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(38, AppDisplayConstants.Collection.BADGES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.BADGE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(39, AppDisplayConstants.Collection.MECHA_BEASTS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.MECHA_BEAST_URL), collectionMenuPanel);
        CreateCollectionButtonUI(40, AppDisplayConstants.Collection.RUNES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.RUNE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(41, AppDisplayConstants.Collection.FURNITURES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.FURNITURE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(42, AppDisplayConstants.Collection.FOODS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.FOOD_URL), collectionMenuPanel);
        CreateCollectionButtonUI(43, AppDisplayConstants.Collection.BEVERAGES_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.BEVERAGE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(44, AppDisplayConstants.Collection.BUILDINGS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.BUILDING_URL), collectionMenuPanel);
        CreateCollectionButtonUI(45, AppDisplayConstants.Collection.PLANTS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.PLANT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(46, AppDisplayConstants.Collection.FASHIONS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.FASHION_URL), collectionMenuPanel);
        CreateCollectionButtonUI(47, AppDisplayConstants.Collection.EMOJIS_COLLECTION, itemBackground, Resources.Load<Texture2D>(ImageConstants.Collection.EMOJI_URL), collectionMenuPanel);

        collectionMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateCollectionButtonUI(int index, string itemName, Texture2D _itemBackground, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ItemButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage itemBackground = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (itemBackground != null && _itemBackground != null)
        {
            itemBackground.texture = _itemBackground;
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
    }
    public void CreateCollection(Transform CollectionMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        search = "";
        type = AppConstants.Type.ALL;
        rare = AppConstants.Rare.ALL;
        collectionMenuPanel = CollectionMenuPanel;
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        RareButtonPrefab = UIManager.Instance.Get("RareButtonPrefab");
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
        AssignButtonEvent("Button_31", () => GetType(AppConstants.MainType.CARD));
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
        mainType = type;
        _ = GetButtonTypeAsync();
        titleText.text = LocalizationManager.Get(type);
    }
    public async Task GetButtonTypeAsync()
    {
        // DictionaryPanel.SetActive(true);
        GameObject mainMenuObject = Instantiate(DictionaryPanelPrefab, MainPanel);
        DictionaryContentPanel = mainMenuObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
        RightScrollViewContentPanel = mainMenuObject.transform.Find("RightScrollView/Viewport/Content");
        LeftScrollViewContentPanel = mainMenuObject.transform.Find("Scroll View/Viewport/ButtonContent");
        PageText = mainMenuObject.transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = mainMenuObject.transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = mainMenuObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = mainMenuObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        TMP_Dropdown rareDropdown = mainMenuObject.transform.Find("DictionaryCards/InputGroup/RareDropdown").GetComponent<TMP_Dropdown>();
        TMP_Dropdown typeDropdown = mainMenuObject.transform.Find("DictionaryCards/InputGroup/TypeDropdown").GetComponent<TMP_Dropdown>();
        TMP_InputField searchInputField = mainMenuObject.transform.Find("DictionaryCards/InputGroup/Search").GetComponent<TMP_InputField>();
        Button searchButton = mainMenuObject.transform.Find("DictionaryCards/InputGroup/SearchButton").GetComponent<Button>();
        CloseButton = mainMenuObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePanel();
            Destroy(mainMenuObject);
        });
        HomeButton = mainMenuObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
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

        Image topBackgroundImage = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TopBackground").GetComponent<Image>();
        topBackgroundImage.material = UI_Green_Gradient_Radius_Mat_MaskPercent_70;
        TextMeshProUGUI subTitleText = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
        subTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLECTION);

        // Transform CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");
        // IUserCurrenciesRepository userCurrencyRepository = new UserCurrenciesRepository();
        // UserCurrenciesService userCurrencyService = new UserCurrenciesService(userCurrencyRepository);
        // List<Currencies> currencies = new List<Currencies>();
        // currencies = await userCurrencyService.GetUserCurrencyAsync(User.CurrentUserId);
        // FindObjectOfType<CurrenciesManager>().GetMainCurrency(currencies, CurrencyPanel);

        searchButton.onClick.AddListener(() =>
        {
            offset = 0;
            currentPage = 1;
            string searchText = searchInputField.text;
            search = searchText;
            _ = LoadCurrentPageAsync();
        });

        List<string> uniqueRaries = QualityEvaluator.rarities;
        if (uniqueRaries.Count > 0)
        {
            rareDropdown.ClearOptions();
            rareDropdown.AddOptions(uniqueRaries);

            // ⚠️ Quan trọng: clear listener cũ trước
            rareDropdown.onValueChanged.RemoveAllListeners();

            // Gán sự kiện
            rareDropdown.onValueChanged.AddListener((index) =>
            {
                offset = 0;
                currentPage = 1;
                // Lấy text đang chọn
                string selectedRare = rareDropdown.options[index].text;
                rare = selectedRare;

                // Gọi async (fire & forget an toàn)
                _ = LoadCurrentPageAsync();
            });

            rareDropdown.value = 0;
            rareDropdown.RefreshShownValue();
        }

        List<string> uniqueTypes = await TypeManager.GetUniqueTypesAsync(mainType);
        uniqueTypes.Insert(0, AppConstants.Type.ALL);
        if (uniqueTypes.Count > 0)
        {
            typeDropdown.ClearOptions();
            typeDropdown.AddOptions(uniqueTypes);

            // ⚠️ Quan trọng: clear listener cũ trước
            typeDropdown.onValueChanged.RemoveAllListeners();

            // Gán sự kiện
            typeDropdown.onValueChanged.AddListener((index) =>
            {
                offset = 0;
                currentPage = 1;
                // Lấy text đang chọn
                string selectedType = typeDropdown.options[index].text;
                type = selectedType;

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
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            var cardHeroesGalleryService = CardHeroesGalleryService.Create();
            List<CardHeroes> cardHeroes = await cardHeroesGalleryService.GetCardHeroesCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);

            totalRecord = await cardHeroesGalleryService.GetCardHeroesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            var booksGalleryService = BooksGalleryService.Create();
            List<Books> books = await booksGalleryService.GetBooksCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = await booksGalleryService.GetBooksCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
            List<CardCaptains> cardCaptains = await cardCaptainsGalleryService.GetCardCaptainsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);

            totalRecord = await cardCaptainsGalleryService.GetCardCaptainsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            var collaborationEquipmentGalleryService = CollaborationEquipmentsGalleryService.Create();
            List<CollaborationEquipments> collaborationEquipments = await collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CollaborationEquipmentsGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = await collaborationEquipmentGalleryService.GetCollaborationEquipmentsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            var equipmentsGalleryService = EquipmentsGalleryService.Create();
            List<Equipments> equipments = await equipmentsGalleryService.GetEquipmentsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = await equipmentsGalleryService.GetEquipmentsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            var petsGalleryService = PetsGalleryService.Create();
            List<Pets> pets = await petsGalleryService.GetPetsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = await petsGalleryService.GetPetsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            var skillsGalleryService = SkillsGalleryService.Create();
            List<Skills> skills = await skillsGalleryService.GetSkillsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = await skillsGalleryService.GetSkillsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            var symbolsGalleryService = SymbolsGalleryService.Create();
            List<Symbols> symbols = await symbolsGalleryService.GetSymbolsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = await symbolsGalleryService.GetSymbolsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            var cardMilitaryGalleryService = CardMilitariesGalleryService.Create();
            List<CardMilitaries> cardMilitaries = await cardMilitaryGalleryService.GetCardMilitariesCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardMilitariesGalleryController.Instance.CreateCardMilitariesGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = await cardMilitaryGalleryService.GetCardMilitariesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            var cardSpellGalleryService = CardSpellsGalleryService.Create();
            List<CardSpells> cardSpells = await cardSpellGalleryService.GetCardSpellsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardSpellsGalleryController.Instance.CreateCardSpellsGallery(cardSpells, DictionaryContentPanel);

            totalRecord = await cardSpellGalleryService.GetCardSpellsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            var collaborationGalleryService = CollaborationsGalleryService.Create();
            List<Collaborations> collaborations = await collaborationGalleryService.GetCollaborationsCollectionAsync(search, PAGE_SIZE, offset, rare);
            CollaborationsGalleryController.Instance.CreateCollaborationsGallery(collaborations, DictionaryContentPanel);

            totalRecord = await collaborationGalleryService.GetCollaborationsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            var medalsGalleryService = MedalsGalleryService.Create();
            List<Medals> medals = await medalsGalleryService.GetMedalsCollectionAsync(search, PAGE_SIZE, offset, rare);
            MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);

            totalRecord = await medalsGalleryService.GetMedalsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            var titlesGalleryService = TitlesGalleryService.Create();
            List<Titles> titles = await titlesGalleryService.GetTitlesCollectionAsync(search, PAGE_SIZE, offset, rare);
            TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = await titlesGalleryService.GetTitlesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            var bordersGalleryService = BordersGalleryService.Create();
            List<Borders> borders = await bordersGalleryService.GetBordersCollectionAsync(search, PAGE_SIZE, offset, rare);
            BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = await bordersGalleryService.GetBordersCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            var magicFormationCircleGalleryService = MagicFormationCirclesGalleryService.Create();
            List<MagicFormationCircles> magicFormationCircles = await magicFormationCircleGalleryService.GetMagicFormationCirclesCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            MagicFormationCirclesGalleryController.Instance.CreateMagicFormationCirclesGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = await magicFormationCircleGalleryService.GetMagicFormationCirclesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            var relicsGalleryService = RelicsGalleryService.Create();
            List<Relics> relics = await relicsGalleryService.GetRelicsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = await relicsGalleryService.GetRelicsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            var cardMonstersGalleryService = CardMonstersGalleryService.Create();
            List<CardMonsters> monsters = await cardMonstersGalleryService.GetCardMonstersCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);

            totalRecord = await cardMonstersGalleryService.GetCardMonstersCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            var cardColonelsGalleryService = CardColonelsGalleryService.Create();
            List<CardColonels> cardColonels = await cardColonelsGalleryService.GetCardColonelsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = await cardColonelsGalleryService.GetCardColonelsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
            List<CardGenerals> cardGenerals = await cardGeneralsGalleryService.GetCardGeneralsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = await cardGeneralsGalleryService.GetCardGeneralsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
            List<CardAdmirals> cardAdmirals = await cardAdmiralsGalleryService.GetCardAdmiralsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = await cardAdmiralsGalleryService.GetCardAdmiralsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            var talismanGalleryService = TalismansGalleryService.Create();
            List<Talismans> talismans = await talismanGalleryService.GetTalismansCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            TalismansGalleryController.Instance.CreateTalismansGallery(talismans, DictionaryContentPanel);

            totalRecord = await talismanGalleryService.GetTalismansCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            var puppetGalleryService = PuppetsGalleryService.Create();
            List<Puppets> puppets = await puppetGalleryService.GetPuppetsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            PuppetsGalleryController.Instance.CreatePuppetsGallery(puppets, DictionaryContentPanel);

            totalRecord = await puppetGalleryService.GetPuppetsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            var alchemyGalleryService = AlchemiesGalleryService.Create();
            List<Alchemies> alchemies = await alchemyGalleryService.GetAlchemyCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            AlchemiesGalleryController.Instance.CreateAlchemiesGallery(alchemies, DictionaryContentPanel);

            totalRecord = await alchemyGalleryService.GetAlchemyCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            var forgeGalleryService = ForgesGalleryService.Create();
            List<Forges> forges = await forgeGalleryService.GetForgesCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            ForgesGalleryController.Instance.CreateForgesGallery(forges, DictionaryContentPanel);

            totalRecord = await forgeGalleryService.GetForgesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            var cardLifeGalleryService = CardLivesGalleryService.Create();
            List<CardLives> cardLives = await cardLifeGalleryService.GetCardLivesCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            CardLivesGalleryController.Instance.CreateCardLivesGallery(cardLives, DictionaryContentPanel);

            totalRecord = await cardLifeGalleryService.GetCardLivesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            var artworkGalleryService = ArtworksGalleryService.Create();
            List<Artworks> artworks = await artworkGalleryService.GetArtworksCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            ArtworksGalleryController.Instance.CreateArtworksGallery(artworks, DictionaryContentPanel);

            totalRecord = await artworkGalleryService.GetArtworksCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            var spiritBeastGalleryService = SpiritBeastsGalleryService.Create();
            List<SpiritBeasts> spiritBeasts = await spiritBeastGalleryService.GetSpiritBeastsCollectionAsync(search, PAGE_SIZE, offset, rare);
            SpiritBeastsGalleryController.Instance.CreateSpiritBeastsGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = await spiritBeastGalleryService.GetSpiritBeastsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            var avatarsGalleryService = AvatarsGalleryService.Create();
            List<Avatars> avatars = await avatarsGalleryService.GetAvatarsCollectionAsync(search, PAGE_SIZE, offset, rare);
            AvatarsGalleryController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

            totalRecord = await avatarsGalleryService.GetAvatarsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            var spiritCardGalleryService = SpiritCardsGalleryService.Create();
            List<SpiritCards> spiritCards = await spiritCardGalleryService.GetSpiritCardsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            SpiritCardsGalleryController.Instance.CreateSpiritCardsGallery(spiritCards, DictionaryContentPanel);

            totalRecord = await spiritCardGalleryService.GetSpiritCardsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            var achievementsGalleryService = AchievementsGalleryService.Create();
            List<Achievements> achievements = await achievementsGalleryService.GetAchievementCollectionAsync(search, PAGE_SIZE, offset, rare);
            AchievementsGalleryController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

            totalRecord = await achievementsGalleryService.GetAchievementsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD))
        {
            var cardsGalleryService = CardsGalleryService.Create();
            List<Cards> cards = await cardsGalleryService.GetCardsCollectionAsync(search, PAGE_SIZE, offset, rare);
            CardsGalleryController.Instance.CreateCardsGallery(cards, DictionaryContentPanel);

            totalRecord = await cardsGalleryService.GetCardsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            var architecturesGalleryService = ArchitecturesGalleryService.Create();
            List<Architectures> architectures = await architecturesGalleryService.GetArchitecturesCollectionAsync(search, PAGE_SIZE, offset, rare);
            ArchitecturesGalleryController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

            totalRecord = await architecturesGalleryService.GetArchitecturesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            var technologiesGalleryService = TechnologiesGalleryService.Create();
            List<Technologies> technologies = await technologiesGalleryService.GetTechnologiesCollectionAsync(search, PAGE_SIZE, offset, rare);
            TechnologiesGalleryController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

            totalRecord = await technologiesGalleryService.GetTechnologiesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            var vehiclesGalleryService = VehiclesGalleryService.Create();
            List<Vehicles> vehicles = await vehiclesGalleryService.GetVehiclesCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            VehiclesGalleryController.Instance.CreateVehiclesGallery(vehicles, DictionaryContentPanel);

            totalRecord = await vehiclesGalleryService.GetVehiclesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            var coresGalleryService = CoresGalleryService.Create();
            List<Cores> cores = await coresGalleryService.GetCoresCollectionAsync(search, PAGE_SIZE, offset, rare);
            CoresGalleryController.Instance.CreateCoresGallery(cores, DictionaryContentPanel);

            totalRecord = await coresGalleryService.GetCoresCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            var weaponsGalleryService = WeaponsGalleryService.Create();
            List<Weapons> weapons = await weaponsGalleryService.GetWeaponsCollectionAsync(search, PAGE_SIZE, offset, rare);
            WeaponsGalleryController.Instance.CreateWeaponsGallery(weapons, DictionaryContentPanel);

            totalRecord = await weaponsGalleryService.GetWeaponsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            var robotsGalleryService = RobotsGalleryService.Create();
            List<Robots> robots = await robotsGalleryService.GetRobotsCollectionAsync(search, PAGE_SIZE, offset, rare);
            RobotsGalleryController.Instance.CreateRobotsGallery(robots, DictionaryContentPanel);

            totalRecord = await robotsGalleryService.GetRobotsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            var badgesGalleryService = BadgesGalleryService.Create();
            List<Badges> badges = await badgesGalleryService.GetBadgesCollectionAsync(search, PAGE_SIZE, offset, rare);
            BadgesGalleryController.Instance.CreateBadgesGallery(badges, DictionaryContentPanel);

            totalRecord = await badgesGalleryService.GetBadgesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            var mechaBeastsGalleryService = MechaBeastsGalleryService.Create();
            List<MechaBeasts> mechaBeasts = await mechaBeastsGalleryService.GetMechaBeastsCollectionAsync(search, PAGE_SIZE, offset, rare);
            MechaBeastsGalleryController.Instance.CreateMechaBeastsGallery(mechaBeasts, DictionaryContentPanel);

            totalRecord = await mechaBeastsGalleryService.GetMechaBeastsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            var runesGalleryService = RunesGalleryService.Create();
            List<Runes> runes = await runesGalleryService.GetRunesCollectionAsync(search, PAGE_SIZE, offset, rare);
            RunesGalleryController.Instance.CreateRunesGallery(runes, DictionaryContentPanel);

            totalRecord = await runesGalleryService.GetRunesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            var furnituresGalleryService = FurnituresGalleryService.Create();
            List<Furnitures> furnitures = await furnituresGalleryService.GetFurnituresCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            FurnituresGalleryController.Instance.CreateFurnituresGallery(furnitures, DictionaryContentPanel);

            totalRecord = await furnituresGalleryService.GetFurnituresCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            var foodsGalleryService = FoodsGalleryService.Create();
            List<Foods> foods = await foodsGalleryService.GetFoodsCollectionAsync(search, PAGE_SIZE, offset, rare);
            FoodsGalleryController.Instance.CreateFoodsGallery(foods, DictionaryContentPanel);

            totalRecord = await foodsGalleryService.GetFoodsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            var beveragesGalleryService = BeveragesGalleryService.Create();
            List<Beverages> beverages = await beveragesGalleryService.GetBeveragesCollectionAsync(search, PAGE_SIZE, offset, rare);
            BeveragesGalleryController.Instance.CreateBeveragesGallery(beverages, DictionaryContentPanel);

            totalRecord = await beveragesGalleryService.GetBeveragesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            var buildingsGalleryService = BuildingsGalleryService.Create();
            List<Buildings> buildings = await buildingsGalleryService.GetBuildingsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            BuildingsGalleryController.Instance.CreateBuildingsGallery(buildings, DictionaryContentPanel);

            totalRecord = await buildingsGalleryService.GetBuildingsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            var plantsGalleryService = PlantsGalleryService.Create();
            List<Plants> plants = await plantsGalleryService.GetPlantsCollectionAsync(search, PAGE_SIZE, offset, rare);
            PlantsGalleryController.Instance.CreatePlantsGallery(plants, DictionaryContentPanel);

            totalRecord = await plantsGalleryService.GetPlantsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            var fashionsGalleryService = FashionsGalleryService.Create();
            List<Fashions> fashions = await fashionsGalleryService.GetFashionsCollectionAsync(search, type, PAGE_SIZE, offset, rare);
            FashionsGalleryController.Instance.CreateFashionsGallery(fashions, DictionaryContentPanel);

            totalRecord = await fashionsGalleryService.GetFashionsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            var emojisRepository = EmojisGalleryService.Create();
            List<Emojis> emojis = await emojisRepository.GetEmojisCollectionAsync(search, PAGE_SIZE, offset, rare);
            EmojisGalleryController.Instance.CreateEmojisGallery(emojis, DictionaryContentPanel);

            totalRecord = await emojisRepository.GetEmojisCountAsync(search, rare);
        }
        totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
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
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void ChangeNextPage()
    {
        if (currentPage < totalPage)
        {
            ClearAllPrefabs();
            currentPage = currentPage + 1;
            offset = offset + PAGE_SIZE;
            _ = LoadCurrentPageAsync();

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage()
    {
        if (currentPage > 1)
        {
            ClearAllPrefabs();
            currentPage = currentPage - 1;
            offset = offset - PAGE_SIZE;
            _ = LoadCurrentPageAsync();

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ClosePanel()
    {
        ClearAllButton();
        ClearAllPrefabs();
        offset = 0;
        currentPage = 1;
        // foreach (Transform child in MainPanel)
        // {
        //     Destroy(child.gameObject);
        // }
    }
    public void Close(Transform content)
    {
        offset = 0;
        currentPage = 1;
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
