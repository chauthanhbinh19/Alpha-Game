using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Reflection;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class GalleryManager : MonoBehaviour
{
    private GameObject ItemButtonPrefab;
    private Transform galleryMenuPanel;
    private GameObject TypeButtonPrefab;
    private GameObject DictionaryPanelPrefab;
    private GameObject RareButtonPrefab;
    private Transform DictionaryContentPanel;
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
    private Material UI_Blue_Gradient_Radius_Mat_MaskPercent_70;
    private Transform MainPanel;
    private Button closeButton;
    private Button homeButton;
    //Variable for pagination
    private int offset;
    private int currentPage;
    private int totalPage;
    private const int PAGE_SIZE = 100;
    private TextMeshProUGUI PageText;
    private Button nextButton;
    private Button previousButton;
    private string mainType;
    private string subType;
    private TextMeshProUGUI titleText;
    private string search;
    private string type;
    private string rare;
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
        ItemButtonPrefab = UIManager.Instance.Get("ItemButtonPrefab");
    }
    public void CreateGalleryButton(Transform galleryMenuPanel)
    {
        Texture2D itemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_GALLERY_URL);
        //Gallery menu
        CreateGalleryButtonUI(1, AppDisplayConstants.Gallery.CARD_HEROES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL), galleryMenuPanel);
        CreateGalleryButtonUI(2, AppDisplayConstants.Gallery.BOOKS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL), galleryMenuPanel);
        CreateGalleryButtonUI(3, AppDisplayConstants.Gallery.PETS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL), galleryMenuPanel);
        CreateGalleryButtonUI(4, AppDisplayConstants.Gallery.CARD_CAPTAINS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL), galleryMenuPanel);
        CreateGalleryButtonUI(5, AppDisplayConstants.Gallery.COLLABORATION_EQUIPMENTS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(6, AppDisplayConstants.Gallery.CARD_MILITARIES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MILITARY_URL), galleryMenuPanel);
        CreateGalleryButtonUI(7, AppDisplayConstants.Gallery.CARD_SPELL_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(8, AppDisplayConstants.Gallery.COLLABORATIONS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL), galleryMenuPanel);
        CreateGalleryButtonUI(9, AppDisplayConstants.Gallery.CARD_MONSTERS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL), galleryMenuPanel);
        CreateGalleryButtonUI(10, AppDisplayConstants.Gallery.EQUIPMENTS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(11, AppDisplayConstants.Gallery.MEDALS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(12, AppDisplayConstants.Gallery.SKILLS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(13, AppDisplayConstants.Gallery.SYMBOLS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(14, AppDisplayConstants.Gallery.TITLES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(15, AppDisplayConstants.Gallery.MAGIC_FORMATION_CRICLES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(16, AppDisplayConstants.Gallery.RELICS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL), galleryMenuPanel);
        CreateGalleryButtonUI(17, AppDisplayConstants.Gallery.CARD_COLONELS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(18, AppDisplayConstants.Gallery.CARD_GENERALS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(19, AppDisplayConstants.Gallery.CARD_ADMIRALS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(20, AppDisplayConstants.Gallery.BORDERS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL), galleryMenuPanel);
        CreateGalleryButtonUI(21, AppDisplayConstants.Gallery.TALISMANS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL), galleryMenuPanel);
        CreateGalleryButtonUI(22, AppDisplayConstants.Gallery.PUPPETS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL), galleryMenuPanel);
        CreateGalleryButtonUI(23, AppDisplayConstants.Gallery.ALCHEMIES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL), galleryMenuPanel);
        CreateGalleryButtonUI(24, AppDisplayConstants.Gallery.FORGES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(25, AppDisplayConstants.Gallery.CARD_LIVES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.LIFE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(26, AppDisplayConstants.Gallery.ARTWORKS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL), galleryMenuPanel);
        CreateGalleryButtonUI(27, AppDisplayConstants.Gallery.SPIRIT_BEASTS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL), galleryMenuPanel);
        CreateGalleryButtonUI(28, AppDisplayConstants.Gallery.AVATARS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.AVATAR_URL), galleryMenuPanel);
        CreateGalleryButtonUI(29, AppDisplayConstants.Gallery.SPIRIT_CARDS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL), galleryMenuPanel);
        CreateGalleryButtonUI(30, AppDisplayConstants.Gallery.ACHIEVEMENTS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(31, AppDisplayConstants.Gallery.ARTIFACTS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(32, AppDisplayConstants.Gallery.ARCHITECTURES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(33, AppDisplayConstants.Gallery.TECHNOLOGIES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL), galleryMenuPanel);
        CreateGalleryButtonUI(34, AppDisplayConstants.Gallery.VEHICLES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(35, AppDisplayConstants.Gallery.CORES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(36, AppDisplayConstants.Gallery.WEAPONS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL), galleryMenuPanel);
        CreateGalleryButtonUI(37, AppDisplayConstants.Gallery.ROBOTS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(38, AppDisplayConstants.Gallery.BADGES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(39, AppDisplayConstants.Gallery.MECHA_BEASTS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), galleryMenuPanel);
        CreateGalleryButtonUI(40, AppDisplayConstants.Gallery.RUNES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(41, AppDisplayConstants.Gallery.FURNITURES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(42, AppDisplayConstants.Gallery.FOODS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), galleryMenuPanel);
        CreateGalleryButtonUI(43, AppDisplayConstants.Gallery.BEVERAGES_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(44, AppDisplayConstants.Gallery.BUILDINGS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL), galleryMenuPanel);
        CreateGalleryButtonUI(45, AppDisplayConstants.Gallery.PLANTS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(46, AppDisplayConstants.Gallery.FASHIONS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL), galleryMenuPanel);
        CreateGalleryButtonUI(47, AppDisplayConstants.Gallery.EMOJIS_GALLERY, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL), galleryMenuPanel);

        galleryMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateGalleryButtonUI(int index, string itemName, Texture2D _itemBackground, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ItemButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && _itemBackground != null)
        {
            background.texture = _itemBackground;
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
    public void CreateGallery(Transform GalleryMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        search = "";
        type = AppConstants.Type.ALL;
        rare = AppConstants.Rare.ALL;
        galleryMenuPanel = GalleryMenuPanel;
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        RareButtonPrefab = UIManager.Instance.Get("RareButtonPrefab");
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
        // GetCardsType();
    }
    void AssignButtonEvent(string buttonName, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = galleryMenuPanel.Find(buttonName);
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
        mainType = type; // Gán giá trị cho mainType
        _ = GetButtonTypeAsync(); // Gọi hàm xử lý
        titleText.text = LocalizationManager.Get(type);
    }
    public async Task GetButtonTypeAsync()
    {
        // DictionaryPanel.SetActive(true);
        GameObject mainMenuObject = Instantiate(DictionaryPanelPrefab, MainPanel);
        Transform transform = mainMenuObject.transform;
        DictionaryContentPanel = transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
        RightScrollViewContentPanel = transform.Find("RightScrollView/Viewport/Content");
        LeftScrollViewContentPanel = transform.Find("Scroll View/Viewport/ButtonContent");
        PageText = transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        nextButton = transform.Find("Pagination/Next").GetComponent<Button>();
        previousButton = transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        TMP_Dropdown rareDropdown = transform.Find("DictionaryCards/InputGroup/RareDropdown").GetComponent<TMP_Dropdown>();
        TMP_Dropdown typeDropdown = transform.Find("DictionaryCards/InputGroup/TypeDropdown").GetComponent<TMP_Dropdown>();
        TMP_InputField searchInputField = transform.Find("DictionaryCards/InputGroup/Search").GetComponent<TMP_InputField>();
        Button searchButton = transform.Find("DictionaryCards/InputGroup/SearchButton").GetComponent<Button>();
        closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePanel();
            Destroy(mainMenuObject);
        });
        homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });
        nextButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            ChangeNextPage();
        });
        previousButton.onClick.AddListener(() =>
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

            //Quan trọng: clear listener cũ trước
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

            //Quan trọng: clear listener cũ trước
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
            List<CardHeroes> cards = await CardHeroesService.Create().GetCardHeroesAsync(search, type, rare, PAGE_SIZE, offset);
            CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);

            totalRecord = await CardHeroesService.Create().GetCardHeroesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            List<Books> books = await BooksService.Create().GetBooksAsync(search, type, rare, PAGE_SIZE, offset);
            BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = await BooksService.Create().GetBooksCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> captains = await CardCaptainsService.Create().GetCardCaptainsAsync(search, type, rare, PAGE_SIZE, offset);
            CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

            totalRecord = await CardCaptainsService.Create().GetCardCaptainsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            List<CollaborationEquipments> collaborationEquipments = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsAsync(search, type, rare, PAGE_SIZE, offset);
            CollaborationEquipmentsController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            List<Equipments> equipments = await EquipmentsService.Create().GetEquipmentsAsync(search, type, rare, PAGE_SIZE, offset);
            EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = await EquipmentsService.Create().GetEquipmentsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            List<Pets> pets = await PetsService.Create().GetPetsAsync(search, type, rare, PAGE_SIZE, offset);
            PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = await PetsService.Create().GetPetsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            List<Skills> skills = await SkillsService.Create().GetSkillsAsync(search, type, rare, PAGE_SIZE, offset);
            SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = await SkillsService.Create().GetSkillsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            List<Symbols> symbols = await SymbolsService.Create().GetSymbolsAsync(search, type, rare, PAGE_SIZE, offset);
            SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = await SymbolsService.Create().GetSymbolsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create().GetCardMilitariesAsync(search, type, rare, PAGE_SIZE, offset);
            CardMilitariesController.Instance.CreateCardMilitariesGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = await CardMilitariesService.Create().GetCardMilitariesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpells = await CardSpellsService.Create().GetCardSpellsAsync(search, type, rare, PAGE_SIZE, offset);
            CardSpellsController.Instance.CreateCardSpellsGallery(cardSpells, DictionaryContentPanel);

            totalRecord = await CardSpellsService.Create().GetCardSpellsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            List<Collaborations> collaborations = await CollaborationsService.Create().GetCollaborationsAsync(search, rare, PAGE_SIZE, offset);
            CollaborationsController.Instance.CreateCollaborationsGallery(collaborations, DictionaryContentPanel);

            totalRecord = await CollaborationsService.Create().GetCollaborationsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            List<Medals> medalsList = await MedalsService.Create().GetMedalsAsync(search, rare, PAGE_SIZE, offset);
            MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

            totalRecord = await MedalsService.Create().GetMedalsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            List<Titles> titles = await TitlesService.Create().GetTitlesAsync(search, rare, PAGE_SIZE, offset);
            TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = await TitlesService.Create().GetTitlesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            List<Borders> borders = await BordersService.Create().GetBordersAsync(search, rare, PAGE_SIZE, offset);
            BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = await BordersService.Create().GetBordersCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            List<MagicFormationCircles> magicFormationCircles = await MagicFormationCirclesService.Create().GetMagicFormationCirclesAsync(search, type, rare, PAGE_SIZE, offset);
            MagicFormationCirclesController.Instance.CreateMagicFormationCirclesGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = await MagicFormationCirclesService.Create().GetMagicFormationCirclesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            List<Relics> relics = await RelicsService.Create().GetRelicsAsync(search, type, rare, PAGE_SIZE, offset);
            RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = await RelicsService.Create().GetRelicsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonsters = await CardMonstersService.Create().GetCardMonstersAsync(search, type, rare, PAGE_SIZE, offset);
            CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

            totalRecord = await CardMonstersService.Create().GetCardMonstersCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonels = await CardColonelsService.Create().GetCardColonelsAsync(search, type, rare, PAGE_SIZE, offset);
            CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = await CardColonelsService.Create().GetCardColonelsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGenerals = await CardGeneralsService.Create().GetCardGeneralsAsync(search, type, rare, PAGE_SIZE, offset);
            CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = await CardGeneralsService.Create().GetCardGeneralsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create().GetCardAdmiralsAsync(search, type, rare, PAGE_SIZE, offset);
            CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = await CardAdmiralsService.Create().GetCardAdmiralsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            List<Talismans> talismans = await TalismansService.Create().GetTalismansAsync(search, type, rare, PAGE_SIZE, offset);
            TalismansController.Instance.CreateTalismansGallery(talismans, DictionaryContentPanel);

            totalRecord = await TalismansService.Create().GetTalismansCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            List<Puppets> puppets = await PuppetsService.Create().GetPuppetsAsync(search, type, rare, PAGE_SIZE, offset);
            PuppetsController.Instance.CreatePuppetsGallery(puppets, DictionaryContentPanel);

            totalRecord = await PuppetsService.Create().GetPuppetsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            List<Alchemies> alchemies = await AlchemiesService.Create().GetAlchemiesAsync(search, type, rare, PAGE_SIZE, offset);
            AlchemiesController.Instance.CreateAlchemiesGallery(alchemies, DictionaryContentPanel);

            totalRecord = await AlchemiesService.Create().GetAlchemiesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            List<Forges> forges = await ForgesService.Create().GetForgesAsync(search, rare, type, PAGE_SIZE, offset);
            ForgesController.Instance.CreateForgesGallery(forges, DictionaryContentPanel);

            totalRecord = await ForgesService.Create().GetForgesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            List<CardLives> cardLives = await CardLivesService.Create().GetCardLivesAsync(search, type, rare, PAGE_SIZE, offset);
            CardLivesController.Instance.CreateCardLivesGallery(cardLives, DictionaryContentPanel);

            totalRecord = await CardLivesService.Create().GetCardLivesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            List<Artworks> artworks = await ArtworksService.Create().GetArtworksAsync(search, type, rare, PAGE_SIZE, offset);
            ArtworksController.Instance.CreateArtworksGallery(artworks, DictionaryContentPanel);

            totalRecord = await ArtworksService.Create().GetArtworksCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            List<SpiritBeasts> spiritBeasts = await SpiritBeastsService.Create().GetSpiritBeastsAsync(search, rare, PAGE_SIZE, offset);
            SpiritBeastsController.Instance.CreateSpiritBeastsGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = await SpiritBeastsService.Create().GetSpiritBeastsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            List<Avatars> avatars = await AvatarsService.Create().GetAvatarsAsync(search, rare, PAGE_SIZE, offset);
            AvatarsController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

            totalRecord = await AvatarsService.Create().GetAvatarsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            List<SpiritCards> spiritCards = await SpiritCardsService.Create().GetSpiritCardsAsync(search, type, rare, PAGE_SIZE, offset);
            SpiritCardsController.Instance.CreateSpiritCardsGallery(spiritCards, DictionaryContentPanel);

            totalRecord = await SpiritCardsService.Create().GetSpiritCardsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            List<Achievements> achievements = await AchievementsService.Create().GetAchievementsAsync(search, rare, PAGE_SIZE, offset);
            AchievementsController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

            totalRecord = await AchievementsService.Create().GetAchievementsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            List<Artifacts> artifacts = await ArtifactsService.Create().GetArtifactsAsync(search, rare, PAGE_SIZE, offset);
            ArtifactsController.Instance.CreateArtifactsGallery(artifacts, DictionaryContentPanel);

            totalRecord = await ArtifactsService.Create().GetArtifactsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            List<Architectures> architectures = await ArchitecturesService.Create().GetArchitecturesAsync(search, rare, PAGE_SIZE, offset);
            ArchitecturesController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

            totalRecord = await ArchitecturesService.Create().GetArchitecturesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            List<Technologies> technologies = await TechnologiesService.Create().GetTechnologiesAsync(search, rare, PAGE_SIZE, offset);
            TechnologiesController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

            totalRecord = await TechnologiesService.Create().GetTechnologiesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            List<Vehicles> vehicles = await VehiclesService.Create().GetVehiclesAsync(search, type, rare, PAGE_SIZE, offset);
            VehiclesController.Instance.CreateVehiclesGallery(vehicles, DictionaryContentPanel);

            totalRecord = await VehiclesService.Create().GetVehiclesCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            List<Cores> cores = await CoresService.Create().GetCoresAsync(search, rare, PAGE_SIZE, offset);
            CoresController.Instance.CreateCoresGallery(cores, DictionaryContentPanel);

            totalRecord = await CoresService.Create().GetCoresCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            List<Weapons> weapons = await WeaponsService.Create().GetWeaponsAsync(search, rare, PAGE_SIZE, offset);
            WeaponsController.Instance.CreateWeaponsGallery(weapons, DictionaryContentPanel);

            totalRecord = await WeaponsService.Create().GetWeaponsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            List<Robots> robots = await RobotsService.Create().GetRobotsAsync(search, rare, PAGE_SIZE, offset);
            RobotsController.Instance.CreateRobotsGallery(robots, DictionaryContentPanel);

            totalRecord = await RobotsService.Create().GetRobotsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            List<Badges> badges = await BadgesService.Create().GetBadgesAsync(search, rare, PAGE_SIZE, offset);
            BadgesController.Instance.CreateBadgesGallery(badges, DictionaryContentPanel);

            totalRecord = await BadgesService.Create().GetBadgesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            List<MechaBeasts> mechaBeasts = await MechaBeastsService.Create().GetMechaBeastsAsync(search, rare, PAGE_SIZE, offset);
            MechaBeastsController.Instance.CreateMechaBeastsGallery(mechaBeasts, DictionaryContentPanel);

            totalRecord = await MechaBeastsService.Create().GetMechaBeastsCountAsync(search,rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            List<Runes> runes = await RunesService.Create().GetRunesAsync(search, rare, PAGE_SIZE, offset);
            RunesController.Instance.CreateRunesGallery(runes, DictionaryContentPanel);

            totalRecord = await RunesService.Create().GetRunesCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            List<Furnitures> furnitures = await FurnituresService.Create().GetFurnituresAsync(search, type, rare, PAGE_SIZE, offset);
            FurnituresController.Instance.CreateFurnituresGallery(furnitures, DictionaryContentPanel);

            totalRecord = await FurnituresService.Create().GetFurnituresCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            List<Foods> foods = await FoodsService.Create().GetFoodsAsync(search, rare, PAGE_SIZE, offset);
            FoodsController.Instance.CreateFoodsGallery(foods, DictionaryContentPanel);

            totalRecord = await FoodsService.Create().GetFoodsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            List<Beverages> beverages = await BeveragesService.Create().GetBeveragesAsync(search, rare, PAGE_SIZE, offset);
            BeveragesController.Instance.CreateBeveragesGallery(beverages, DictionaryContentPanel);

            totalRecord = await BeveragesService.Create().GetBeveragesCountAsync(search,rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            List<Buildings> buildings = await BuildingsService.Create().GetBuildingsAsync(search, type, rare, PAGE_SIZE, offset);
            BuildingsController.Instance.CreateBuildingsGallery(buildings, DictionaryContentPanel);

            totalRecord = await BuildingsService.Create().GetBuildingsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            List<Plants> plants = await PlantsService.Create().GetPlantsAsync(search, rare, PAGE_SIZE, offset);
            PlantsController.Instance.CreatePlantsGallery(plants, DictionaryContentPanel);

            totalRecord = await PlantsService.Create().GetPlantsCountAsync(search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            List<Fashions> fashions = await FashionsService.Create().GetFashionsAsync(search, type, rare, PAGE_SIZE, offset);
            FashionsController.Instance.CreateFashionsGallery(fashions, DictionaryContentPanel);

            totalRecord = await FashionsService.Create().GetFashionsCountAsync(search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            List<Emojis> plants = await EmojisService.Create().GetEmojisAsync(search, rare, PAGE_SIZE, offset);
            EmojisController.Instance.CreateEmojisGallery(plants, DictionaryContentPanel);

            totalRecord = await EmojisService.Create().GetEmojisCountAsync(search, rare);
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
        // Duyệt qua tất cả các con cái của cardsContent
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
