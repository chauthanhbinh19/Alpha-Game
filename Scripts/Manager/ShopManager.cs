using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Threading.Tasks;

public class ShopManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform CurrentContent;
    private Transform CurrencyPanel;
    private GameObject ShopButtonPrefab;
    private GameObject ShopManagerPrefab;
    private GameObject CurrentObject;
    private GameObject ShopPrefab;
    private GameObject TypeButtonPrefab;
    private GameObject EquipmentShopPrefab;
    private Transform PopupPanel;
    private RawImage FirstDecorationImage;
    private RawImage SecondDecorationImage;
    private Button CloseButton;
    private Button HomeButton;
    private int Offset = 0;
    private int CurrentPage = 1;
    private int TotalPage;
    private const int PAGE_SIZE = 100;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private string MainType;
    private string Type;
    private TextMeshProUGUI TitleText;
    // private string rare;
    public static ShopManager Instance { get; private set; }
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
        ShopButtonPrefab = UIManager.Instance.Get("ShopButtonPrefab");
        ShopManagerPrefab = UIManager.Instance.Get("ShopManagerPrefab");
        ShopPrefab = UIManager.Instance.Get("ShopPrefab");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        EquipmentShopPrefab = UIManager.Instance.Get("EquipmentShopPrefab");
        PopupPanel = UIManager.Instance.GetTransform("popupPanel");
    }
    void AssignButtonEvent(string buttonName, Transform panel, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = panel.Find(buttonName);
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
    public async Task CreateShopButtonAsync(Transform panel)
    {
        MainPanel = panel;
        CurrentObject = Instantiate(ShopManagerPrefab, MainPanel);
        Transform transform = CurrentObject.transform;
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        TitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SHOP);
        CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(CurrentObject);
        });
        Transform CurrencyPanel = transform.Find("DictionaryCards/Currency");

        List<Currencies> currencies = new List<Currencies>();
        currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
        FindObjectOfType<CurrenciesManager>().GetMainCurrency(currencies, CurrencyPanel);

        Transform tempContent = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        CreateButton(1, AppDisplayConstants.MainType.CARD_HEROES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL), tempContent);
        CreateButton(2, AppDisplayConstants.MainType.BOOKS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL), tempContent);
        CreateButton(3, AppDisplayConstants.MainType.PETS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL), tempContent);
        CreateButton(4, AppDisplayConstants.MainType.CARD_CAPTAINS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL), tempContent);
        CreateButton(5, AppDisplayConstants.MainType.COLLABORATION_EQUIPMENTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL), tempContent);
        CreateButton(6, AppDisplayConstants.MainType.CARD_MILITARIES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MILITARY_URL), tempContent);
        CreateButton(7, AppDisplayConstants.MainType.CARD_SPELLS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL), tempContent);
        CreateButton(8, AppDisplayConstants.MainType.COLLABORATIONS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL), tempContent);
        CreateButton(9, AppDisplayConstants.MainType.CARD_MONSTERS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL), tempContent);
        CreateButton(10, AppDisplayConstants.MainType.BORDERS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL), tempContent);
        CreateButton(11, AppDisplayConstants.MainType.MEDALS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL), tempContent);
        CreateButton(12, AppDisplayConstants.MainType.SKILLS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL), tempContent);
        CreateButton(13, AppDisplayConstants.MainType.SYMBOLS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL), tempContent);
        CreateButton(14, AppDisplayConstants.MainType.TITLES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL), tempContent);
        CreateButton(15, AppDisplayConstants.MainType.MAGIC_FORMATION_CIRCLES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), tempContent);
        CreateButton(16, AppDisplayConstants.MainType.RELICS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL), tempContent);
        CreateButton(17, AppDisplayConstants.MainType.ARTWORKS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL), tempContent);
        CreateButton(18, AppDisplayConstants.MainType.ACHIEVEMENTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL), tempContent);
        CreateButton(19, AppDisplayConstants.MainType.CARD_COLONELS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL), tempContent);
        CreateButton(20, AppDisplayConstants.MainType.CARD_GENERALS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL), tempContent);
        CreateButton(21, AppDisplayConstants.MainType.CARD_ADMIRALS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL), tempContent);
        CreateButton(22, AppDisplayConstants.MainType.TALISMANS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL), tempContent);
        CreateButton(23, AppDisplayConstants.MainType.PUPPETS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL), tempContent);
        CreateButton(24, AppDisplayConstants.MainType.ALCHEMIES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL), tempContent);
        CreateButton(25, AppDisplayConstants.MainType.FORGES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL), tempContent);
        CreateButton(26, AppDisplayConstants.MainType.CARD_LIVES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_LIFE_URL), tempContent);
        CreateButton(27, AppDisplayConstants.MainType.SPIRIT_BEASTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL), tempContent);
        CreateButton(28, AppDisplayConstants.MainType.SPIRIT_CARDS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL), tempContent);
        CreateButton(29, AppDisplayConstants.MainType.ARTIFACTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL), tempContent);
        CreateButton(30, AppDisplayConstants.MainType.ARCHITECTURES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL), tempContent);
        CreateButton(31, AppDisplayConstants.MainType.TECHNOLOGIES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL), tempContent);
        CreateButton(32, AppDisplayConstants.MainType.VEHICLES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL), tempContent);
        CreateButton(33, AppDisplayConstants.MainType.CORES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL), tempContent);
        CreateButton(34, AppDisplayConstants.MainType.WEAPONS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL), tempContent);
        CreateButton(35, AppDisplayConstants.MainType.ROBOTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL), tempContent);
        CreateButton(36, AppDisplayConstants.MainType.BADGES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), tempContent);
        CreateButton(37, AppDisplayConstants.MainType.MECHA_BEASTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), tempContent);
        CreateButton(38, AppDisplayConstants.MainType.RUNES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), tempContent);
        CreateButton(39, AppDisplayConstants.MainType.FURNITURES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FURNITURE_URL), tempContent);
        CreateButton(40, AppDisplayConstants.MainType.FOODS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FOOD_URL), tempContent);
        CreateButton(41, AppDisplayConstants.MainType.BEVERAGES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BEVERAGE_URL), tempContent);
        CreateButton(42, AppDisplayConstants.MainType.BUILDINGS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL), tempContent);
        CreateButton(43, AppDisplayConstants.MainType.PLANTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL), tempContent);
        CreateButton(44, AppDisplayConstants.MainType.FASHIONS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL), tempContent);
        CreateButton(45, AppDisplayConstants.MainType.EMOJIS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL), tempContent);
        CreateButton(46, AppDisplayConstants.MainType.CARD_SOLDIERS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SOLDIER_URL), tempContent);
        CreateButton(47, AppDisplayConstants.MainType.OUTFITS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.OUTFIT_URL), tempContent);

        AssignButtonEvent("Button_1", tempContent, () => GetType(AppConstants.MainType.CARD_HERO));
        AssignButtonEvent("Button_2", tempContent, () => GetType(AppConstants.MainType.BOOK));
        AssignButtonEvent("Button_3", tempContent, () => GetType(AppConstants.MainType.PET));
        AssignButtonEvent("Button_4", tempContent, () => GetType(AppConstants.MainType.CARD_CAPTAIN));
        AssignButtonEvent("Button_5", tempContent, () => GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT));
        AssignButtonEvent("Button_6", tempContent, () => GetType(AppConstants.MainType.CARD_MILITARY));
        AssignButtonEvent("Button_7", tempContent, () => GetType(AppConstants.MainType.CARD_SPELL));
        AssignButtonEvent("Button_8", tempContent, () => GetType(AppConstants.MainType.COLLABORATION));
        AssignButtonEvent("Button_9", tempContent, () => GetType(AppConstants.MainType.CARD_MONSTER));
        AssignButtonEvent("Button_10", tempContent, () => GetType(AppConstants.MainType.BORDER));
        AssignButtonEvent("Button_11", tempContent, () => GetType(AppConstants.MainType.MEDAL));
        AssignButtonEvent("Button_12", tempContent, () => GetType(AppConstants.MainType.SKILL));
        AssignButtonEvent("Button_13", tempContent, () => GetType(AppConstants.MainType.SYMBOL));
        AssignButtonEvent("Button_14", tempContent, () => GetType(AppConstants.MainType.TITLE));
        AssignButtonEvent("Button_15", tempContent, () => GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE));
        AssignButtonEvent("Button_16", tempContent, () => GetType(AppConstants.MainType.RELIC));
        AssignButtonEvent("Button_17", tempContent, () => GetType(AppConstants.MainType.ARTWORK));
        AssignButtonEvent("Button_18", tempContent, () => GetType(AppConstants.MainType.ACHIEVEMENT));
        AssignButtonEvent("Button_19", tempContent, () => GetType(AppConstants.MainType.CARD_COLONEL));
        AssignButtonEvent("Button_20", tempContent, () => GetType(AppConstants.MainType.CARD_GENERAL));
        AssignButtonEvent("Button_21", tempContent, () => GetType(AppConstants.MainType.CARD_ADMIRAL));
        AssignButtonEvent("Button_22", tempContent, () => GetType(AppConstants.MainType.TALISMAN));
        AssignButtonEvent("Button_23", tempContent, () => GetType(AppConstants.MainType.PUPPET));
        AssignButtonEvent("Button_24", tempContent, () => GetType(AppConstants.MainType.ALCHEMY));
        AssignButtonEvent("Button_25", tempContent, () => GetType(AppConstants.MainType.FORGE));
        AssignButtonEvent("Button_26", tempContent, () => GetType(AppConstants.MainType.CARD_LIFE));
        AssignButtonEvent("Button_27", tempContent, () => GetType(AppConstants.MainType.SPIRIT_BEAST));
        AssignButtonEvent("Button_28", tempContent, () => GetType(AppConstants.MainType.SPIRIT_CARD));
        AssignButtonEvent("Button_29", tempContent, () => GetType(AppConstants.MainType.ARTIFACT));
        AssignButtonEvent("Button_30", tempContent, () => GetType(AppConstants.MainType.ARCHITECTURE));
        AssignButtonEvent("Button_31", tempContent, () => GetType(AppConstants.MainType.TECHNOLOGY));
        AssignButtonEvent("Button_32", tempContent, () => GetType(AppConstants.MainType.VEHICLE));
        AssignButtonEvent("Button_33", tempContent, () => GetType(AppConstants.MainType.CORE));
        AssignButtonEvent("Button_34", tempContent, () => GetType(AppConstants.MainType.WEAPON));
        AssignButtonEvent("Button_35", tempContent, () => GetType(AppConstants.MainType.ROBOT));
        AssignButtonEvent("Button_36", tempContent, () => GetType(AppConstants.MainType.BADGE));
        AssignButtonEvent("Button_37", tempContent, () => GetType(AppConstants.MainType.MECHA_BEAST));
        AssignButtonEvent("Button_38", tempContent, () => GetType(AppConstants.MainType.RUNE));
        AssignButtonEvent("Button_39", tempContent, () => GetType(AppConstants.MainType.FURNITURE));
        AssignButtonEvent("Button_40", tempContent, () => GetType(AppConstants.MainType.FOOD));
        AssignButtonEvent("Button_41", tempContent, () => GetType(AppConstants.MainType.BEVERAGE));
        AssignButtonEvent("Button_42", tempContent, () => GetType(AppConstants.MainType.BUILDING));
        AssignButtonEvent("Button_43", tempContent, () => GetType(AppConstants.MainType.PLANT));
        AssignButtonEvent("Button_44", tempContent, () => GetType(AppConstants.MainType.FASHION));
        AssignButtonEvent("Button_45", tempContent, () => GetType(AppConstants.MainType.EMOJI));
        AssignButtonEvent("Button_46", tempContent, () => GetType(AppConstants.MainType.CARD_SOLDIER));
        AssignButtonEvent("Button_47", tempContent, () => GetType(AppConstants.MainType.OUTFIT));

        tempContent.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateButton(int index, string itemName, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ShopButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void GetType(string type)
    {
        MainType = type; // Gán giá trị cho mainType
        _=GetButtonTypeAsync(); // Gọi hàm xử lý
        TitleText.text = LocalizationManager.Get(type); // Cập nhật tiêu đề
    }
    public async Task GetButtonTypeAsync()
    {
        // DictionaryPanel.SetActive(true);
        GameObject equipmentObject = Instantiate(ShopPrefab, MainPanel);
        Transform transform = equipmentObject.transform;
        CurrentContent = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TabButtonPanel = transform.Find("Scroll View/Viewport/Content");
        CurrencyPanel = transform.Find("DictionaryCards/Currency");
        FirstDecorationImage = transform.Find("DictionaryCards/FirstDecorationImage").GetComponent<RawImage>();
        SecondDecorationImage = transform.Find("DictionaryCards/SecondDecorationImage").GetComponent<RawImage>();
        PageText = transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = transform.Find("Pagination/Previous").GetComponent<Button>();
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(equipmentObject);
        });
        HomeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Offset = 0;
            CurrentPage = 1;
            ButtonEvent.Instance.Close(MainPanel);
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

        // Transform CurrencyPanel = transform.Find("DictionaryCards/Currency");
        // 
        // List<Currency> currencies = new List<Currency>();
        // currencies = currency.GetUserCurrency();
        // FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);

        List<string> uniqueTypes = await TypeManager.GetUniqueTypesAsync(MainType);
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(TypeButtonPrefab, TabButtonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    OnButtonClick(button, subtype);
                });

                if (i == 0)
                {
                    Type = subtype;
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    _=LoadCurrentPageAsync();

                }
                else
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
            }
        }
        else
        {
            _=LoadCurrentPageAsync();
        }

    }
    void OnButtonClick(GameObject clickedButton, string type)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL); // Giả sử bạn có texture trắng
            }
        }

        this.Type = type;
        CurrentPage = 1;
        Offset = 0;
        ButtonEvent.Instance.Close(CurrentContent);
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
        _=LoadCurrentPageAsync();
    }
    public async Task LoadCurrentPageAsync()
    {
        int totalRecord = 0;

        if (MainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_1_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_2_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardHeroes> cardHeroes = await CardHeroesService.Create().GetCardHeroesWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardHeroesController.Instance.CreateCardHeroesTradeAsync(cardHeroes, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardHeroesService.Create().GetCardHeroesWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.BOOK))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_3_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_4_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Books> books = await BooksService.Create().GetBooksWithPriceAsync(Type, PAGE_SIZE, Offset);
            await BooksController.Instance.CreateBooksTradeAsync(books, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await BooksService.Create().GetBooksWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_5_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_6_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardCaptains> cardCaptains = await CardCaptainsService.Create().GetCardCaptainsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardCaptainsController.Instance.CreateCardCaptainsTradeAsync(cardCaptains, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardCaptainsService.Create().GetCardCaptainsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_7_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_8_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CollaborationEquipments> collaborationEquipments = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CollaborationEquipmentsController.Instance.CreateCollaborationEquipmentsTradeAsync(collaborationEquipments, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsWithPriceCountAsync(Type);
        }
        // else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        // {
        //     Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_9_URL);
        //     Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_10_URL);
        //     firstDecorationImage.texture = firstDecorationTexture;
        //     secondDecorationImage.texture = secondDecorationTexture;
        //     List<Equipments> equipments = await EquipmentsService.Create().GetEquipmentsWithCurrencyAsync(type, pageSize, offset, rare);
        //     createEquipments(equipments);

        //     totalRecord = await EquipmentsService.Create().GetEquipmentsWithCurrencyAsync(type, rare);
        // }
        else if (MainType.Equals(AppConstants.MainType.PET))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_11_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_12_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Pets> pets = await PetsService.Create().GetPetsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await PetsController.Instance.CreatePetsTradeAsync(pets, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await PetsService.Create().GetPetsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.SKILL))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_13_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_14_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Skills> skills = await SkillsService.Create().GetSkillsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await SkillsController.Instance.CreateSkillsTradeAsync(skills, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await SkillsService.Create().GetSkillsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.SYMBOL))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_15_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_16_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Symbols> symbols = await SymbolsService.Create().GetSymbolsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await SymbolsController.Instance.CreateSymbolsTradeAsync(symbols, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await SymbolsService.Create().GetSymbolsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_17_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_18_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create().GetCardMilitariesWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardMilitariesController.Instance.CreateCardMilitariesTradeAsync(cardMilitaries, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardMilitariesService.Create().GetCardMilitariesWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_19_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_20_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardSpells> cardSpells = await CardSpellsService.Create().GetCardSpellsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardSpellsController.Instance.CreateCardSpellsTradeAsync(cardSpells, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardSpellsService.Create().GetCardSpellsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_21_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_22_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<MagicFormationCircles> magicFormationCircles = await MagicFormationCirclesService.Create().GetMagicFormationCirclesWithPriceAsync(Type, PAGE_SIZE, Offset);
            await MagicFormationCirclesController.Instance.CreateMagicFormationCirclesTradeAsync(magicFormationCircles, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await MagicFormationCirclesService.Create().GetMagicFormationCirclesWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.RELIC))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_23_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_24_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Relics> relics = await RelicsService.Create().GetRelicsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await RelicsController.Instance.CreateRelicsTradeAsync(relics, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await RelicsService.Create().GetRelicsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_25_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_26_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardMonsters> cardMonsters = await CardMonstersService.Create().GetCardMonstersWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardMonstersController.Instance.CreateCardMonstersTradeAsync(cardMonsters, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardMonstersService.Create().GetCardMonstersWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_27_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_28_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardColonels> cardColonels = await CardColonelsService.Create().GetCardColonelsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardColonelsController.Instance.CreateCardColonelsTradeAsync(cardColonels, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardColonelsService.Create().GetCardColonelsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_29_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_30_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardGenerals> cardGenerals = await CardGeneralsService.Create().GetCardGeneralsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardGeneralsController.Instance.CreateCardGeneralsTradeAsync(cardGenerals, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardGeneralsService.Create().GetCardGeneralsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_31_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_32_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create().GetCardAdmiralsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardAdmiralsController.Instance.CreateCardAdmiralsTradeAsync(cardAdmirals, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardAdmiralsService.Create().GetCardAdmiralsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.TALISMAN))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_33_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_34_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Talismans> talismans = await TalismansService.Create().GetTalismansWithPriceAsync(Type, PAGE_SIZE, Offset);
            await TalismansController.Instance.CreateTalismansTradeAsync(talismans, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await TalismansService.Create().GetTalismansWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.PUPPET))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_35_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_36_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Puppets> puppets = await PuppetsService.Create().GetPuppetsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await PuppetsController.Instance.CreatePuppetsTradeAsync(puppets, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await PuppetsService.Create().GetPuppetsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_37_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_38_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Alchemies> alchemies = await AlchemiesService.Create().GetAlchemiesWithPriceAsync(Type, PAGE_SIZE, Offset);
            await AlchemiesController.Instance.CreateAlchemiesTradeAsync(alchemies, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await AlchemiesService.Create().GetAlchemiesWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.FORGE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_39_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_40_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Forges> forges = await ForgesService.Create().GetForgesWithPriceAsync(Type, PAGE_SIZE, Offset);
            await ForgesController.Instance.CreateForgesTradeAsync(forges, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await ForgesService.Create().GetForgesWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_41_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_42_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardLives> cardLives = await CardLivesService.Create().GetCardLivesWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardLivesController.Instance.CreateCardLivesTradeAsync(cardLives, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardLivesService.Create().GetCardLivesWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.ARTWORK))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_43_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_44_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Artworks> artworks = await ArtworksService.Create().GetArtworksWithPriceAsync(Type, PAGE_SIZE, Offset);
            await ArtworksController.Instance.CreateArtworksTradeAsync(artworks, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await ArtworksService.Create().GetArtworksWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_45_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_46_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<SpiritBeasts> spiritBeasts = await SpiritBeastsService.Create().GetSpiritBeastsWithPriceAsync(PAGE_SIZE, Offset);
            await SpiritBeastsController.Instance.CreateSpiritBeastsTradeAsync(spiritBeasts, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await SpiritBeastsService.Create().GetSpiritBeastsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_47_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_48_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<SpiritCards> spiritCards = await SpiritCardsService.Create().GetSpiritCardsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await SpiritCardsController.Instance.CreateSpiritCardTradeAsync(spiritCards, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await SpiritCardsService.Create().GetSpiritCardsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_57_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_58_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Artifacts> artifacts = await ArtifactsService.Create().GetArtifactsWithPriceAsync(PAGE_SIZE, Offset);
            await ArtifactsController.Instance.CreateArtifactsTradeAsync(artifacts, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await ArtifactsService.Create().GetArtifactsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_59_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_60_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Architectures> architectures = await ArchitecturesService.Create().GetArchitecturesWithPriceAsync(PAGE_SIZE, Offset);
            await ArchitecturesController.Instance.CreateArchitecturesTradeAsync(architectures, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await ArchitecturesService.Create().GetArchitecturesWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_61_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_62_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Technologies> technologies = await TechnologiesService.Create().GetTechnologiesWithPriceAsync(PAGE_SIZE, Offset);
            await TechnologiesController.Instance.CreateTechnologiesTradeAsync(technologies, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await TechnologiesService.Create().GetTechnologiesWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.VEHICLE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_63_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_64_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Vehicles> vehicles = await VehiclesService.Create().GetVehiclesWithPriceAsync(Type, PAGE_SIZE, Offset);
            await VehiclesController.Instance.CreateVehiclesTradeAsync(vehicles, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await VehiclesService.Create().GetVehiclesWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.CORE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_63_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_64_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Cores> cores = await CoresService.Create().GetCoresWithPriceAsync(PAGE_SIZE, Offset);
            await CoresController.Instance.CreateCoresTradeAsync(cores, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CoresService.Create().GetCoresWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.WEAPON))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_65_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_66_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Weapons> weapons = await WeaponsService.Create().GetWeaponsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await WeaponsController.Instance.CreateWeaponsTradeAsync(weapons, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await WeaponsService.Create().GetWeaponsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.ROBOT))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_67_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_68_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Robots> robots = await RobotsService.Create().GetRobotsWithPriceAsync(PAGE_SIZE, Offset);
            await RobotsController.Instance.CreateRobotsTradeAsync(robots, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await RobotsService.Create().GetRobotsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.BADGE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_69_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_70_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Badges> badges = await BadgesService.Create().GetBadgesWithPriceAsync(PAGE_SIZE, Offset);
            await BadgesController.Instance.CreateBadgesTradeAsync(badges, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await BadgesService.Create().GetBadgesWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_71_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_72_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<MechaBeasts> mechaBeasts = await MechaBeastsService.Create().GetMechaBeastsWithPriceAsync(PAGE_SIZE, Offset);
            await MechaBeastsController.Instance.CreateMechaBeastsTradeAsync(mechaBeasts, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await MechaBeastsService.Create().GetMechaBeastsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.RUNE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_73_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_74_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Runes> runes = await RunesService.Create().GetRunesWithPriceAsync(PAGE_SIZE, Offset);
            await RunesController.Instance.CreateRunesTradeAsync(runes, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await RunesService.Create().GetRunesWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.FURNITURE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_75_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_76_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Furnitures> furnitures = await FurnituresService.Create().GetFurnituresWithPriceAsync(Type, PAGE_SIZE, Offset);
            await FurnituresController.Instance.CreateFurnituresTradeAsync(furnitures, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await FurnituresService.Create().GetFurnituresWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.FOOD))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_77_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_78_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Foods> foods = await FoodsService.Create().GetFoodsWithPriceAsync(PAGE_SIZE, Offset);
            await FoodsController.Instance.CreateFoodsTradeAsync(foods, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await FoodsService.Create().GetFoodsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_79_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_80_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Beverages> beverages = await BeveragesService.Create().GetBeveragesWithPriceAsync(PAGE_SIZE, Offset);
            await BeveragesController.Instance.CreateBeveragesTradeAsync(beverages, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await BeveragesService.Create().GetBeveragesWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.BUILDING))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_81_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_82_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Buildings> buildings = await BuildingsService.Create().GetBuildingsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await BuildingsController.Instance.CreateBuildingsTradeAsync(buildings, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await BuildingsService.Create().GetBuildingsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.PLANT))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_83_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_84_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Plants> plants = await PlantsService.Create().GetPlantsWithPriceAsync(PAGE_SIZE, Offset);
            await PlantsController.Instance.CreatePlantsTradeAsync(plants, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await PlantsService.Create().GetPlantsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.FASHION))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_85_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_86_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Fashions> fashions = await FashionsService.Create().GetFashionsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await FashionsController.Instance.CreateFashionsTradeAsync(fashions, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await FashionsService.Create().GetFashionsWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.EMOJI))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_85_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_86_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Emojis> emojis = await EmojisService.Create().GetEmojisWithPriceAsync(PAGE_SIZE, Offset);
            await EmojisController.Instance.CreateEmojisTradeAsync(emojis, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await EmojisService.Create().GetEmojisWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.MEDAL))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_83_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_84_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Medals> medals = await MedalsService.Create().GetMedalsWithPriceAsync(PAGE_SIZE, Offset);
            await MedalsController.Instance.CreateMedalsTradeAsync(medals, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await MedalsService.Create().GetMedalsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.TITLE))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_83_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_84_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Titles> titles = await TitlesService.Create().GetTitlesWithPriceAsync(PAGE_SIZE, Offset);
            await TitlesController.Instance.CreateTitlesTradeAsync(titles, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await TitlesService.Create().GetTitlesWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_83_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_84_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Collaborations> collaborations = await CollaborationsService.Create().GetCollaborationsWithPriceAsync(PAGE_SIZE, Offset);
            await CollaborationsController.Instance.CreateCollaborationTradeAsync(collaborations, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CollaborationsService.Create().GetCollaborationsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.BORDER))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_83_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_84_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Borders> borders = await BordersService.Create().GetBordersWithPriceAsync(PAGE_SIZE, Offset);
            await BordersController.Instance.CreateBordersTradeAsync(borders, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await BordersService.Create().GetBordersWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_83_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_84_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Achievements> achievements = await AchievementsService.Create().GetAchievementsWithPriceAsync(PAGE_SIZE, Offset);
            await AchievementsController.Instance.CreateAchievementsTradeAsync(achievements, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await AchievementsService.Create().GetAchievementsWithPriceCountAsync();
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_31_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_32_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<CardSoldiers> cardSoldiers = await CardSoldiersService.Create().GetCardSoldiersWithPriceAsync(Type, PAGE_SIZE, Offset);
            await CardSoldiersController.Instance.CreateCardSoldiersTradeAsync(cardSoldiers, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await CardSoldiersService.Create().GetCardSoldiersWithPriceCountAsync(Type);
        }
        else if (MainType.Equals(AppConstants.MainType.OUTFIT))
        {
            Texture firstDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_65_URL);
            Texture secondDecorationTexture = TextureHelper.LoadTextureCached(ImageConstants.Artifact.ARTIFACT_66_URL);
            FirstDecorationImage.texture = firstDecorationTexture;
            SecondDecorationImage.texture = secondDecorationTexture;
            List<Outfits> outfits = await OutfitsService.Create().GetOutfitsWithPriceAsync(Type, PAGE_SIZE, Offset);
            await OutfitsController.Instance.CreateOutfitsTradeAsync(outfits, Type, CurrentContent, CurrencyPanel, PopupPanel);

            totalRecord = await OutfitsService.Create().GetOutfitsWithPriceCountAsync(Type);
        }
        TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
        PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
    }
    public void ChangeNextPage()
    {
        if (CurrentPage < TotalPage)
        {
            ButtonEvent.Instance.Close(CurrentContent);
            CurrentPage = CurrentPage + 1;
            Offset = Offset + PAGE_SIZE;
            _=LoadCurrentPageAsync();

            PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();

        }
    }
    public void ChangePreviousPage()
    {
        if (CurrentPage > 1)
        {
            ButtonEvent.Instance.Close(CurrentContent);
            CurrentPage = CurrentPage - 1;
            Offset = Offset - PAGE_SIZE;
            _=LoadCurrentPageAsync();

            PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();

        }
    }

}
