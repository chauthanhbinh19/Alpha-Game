using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class GachaManager : MonoBehaviour
{
    public static GachaManager Instance { get; private set; }
    private Transform MainPanel;
    public GameObject GachaPanelPrefab;
    private GameObject MainButtonPrefab;
    public Transform gachaMenuPanel;
    public RawImage backgroundImage;
    public GameObject GachaButtonPrefab;
    public GameObject currentObject;
    public Texture2D itemBackground;
    public Texture2D subBackground;
    private string mainType;
    private TextMeshProUGUI titleText;
    List<Items> tickets;
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
        MainButtonPrefab = UIManager.Instance.Get("MainButtonPrefab");
        GachaPanelPrefab = UIManager.Instance.Get("GachaPanelPrefab");
        GachaButtonPrefab = UIManager.Instance.Get("GachaButtonPrefab");
        // AdvancedButtonPrefab = UIManager.Instance.Get("AdvancedButtonPrefab");
        // AdvancedSubButtonPrefab = UIManager.Instance.Get("AdvancedSubButtonPrefab");
        // PopupMenuPanelPrefab = UIManager.Instance.Get("PopupMenuPanelPrefab");
        // FeatureButtonPrefab = UIManager.Instance.Get("FeatureButtonPrefab");
        // MainPanel = UIManager.Instance.GetTransform("MainPanel");
    }
    public void CreateGachaButton(Transform tempGachaMenuPanel)
    {
        subBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Flag.FLAG_INVENTORY_URL);
        itemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_GALLERY_URL);
        //Gallery menu
        CreateGachaButtonUI(1, AppDisplayConstants.Gallery.CARD_HEROES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            tempGachaMenuPanel);
        CreateGachaButtonUI(2, AppDisplayConstants.Gallery.BOOKS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BOOK_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(3, AppDisplayConstants.Gallery.PETS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PET_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(4, AppDisplayConstants.Gallery.CARD_CAPTAINS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_CAPTAIN_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(5, AppDisplayConstants.Gallery.COLLABORATION_EQUIPMENTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_EQUIPMENT_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(6, AppDisplayConstants.Gallery.CARD_MILITARIES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MILITARY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MILITARY_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(7, AppDisplayConstants.Gallery.CARD_SPELLS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SPELL_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(8, AppDisplayConstants.Gallery.COLLABORATIONS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(9, AppDisplayConstants.Gallery.CARD_MONSTERS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MONSTER_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(10, AppDisplayConstants.Gallery.EQUIPMENTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.EQUIPMENT_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(11, AppDisplayConstants.Gallery.MEDALS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MEDAL_URL),
             tempGachaMenuPanel);
        CreateGachaButtonUI(12, AppDisplayConstants.Gallery.SKILLS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SKILL_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(13, AppDisplayConstants.Gallery.SYMBOLS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SYMBOL_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(14, AppDisplayConstants.Gallery.TITLES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TITLE_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(15, AppDisplayConstants.Gallery.MAGIC_FORMATION_CIRCLES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MAGIC_FORMATION_CIRCLE_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(16, AppDisplayConstants.Gallery.RELICS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.RELIC_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(17, AppDisplayConstants.Gallery.CARD_COLONELS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_COLONEL_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(18, AppDisplayConstants.Gallery.CARD_GENERALS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_GENERAL_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(19, AppDisplayConstants.Gallery.CARD_ADMIRALS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_ADMIRAL_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(20, AppDisplayConstants.Gallery.BORDERS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BORDER_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(21, AppDisplayConstants.Gallery.TALISMANS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TALISMAN_URL),  
            tempGachaMenuPanel);
        CreateGachaButtonUI(22, AppDisplayConstants.Gallery.PUPPETS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PUPPET_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(23, AppDisplayConstants.Gallery.ALCHEMIES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ALCHEMY_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(24, AppDisplayConstants.Gallery.FORGES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FORGE_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(25, AppDisplayConstants.Gallery.CARD_LIVES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.LIFE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_LIFE_URL),
             tempGachaMenuPanel);
        CreateGachaButtonUI(26, AppDisplayConstants.Gallery.ARTWORKS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTWORK_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(27, AppDisplayConstants.Gallery.SPIRIT_BEASTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_BEAST_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(28, AppDisplayConstants.Gallery.AVATARS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.AVATAR_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.AVATAR_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(29, AppDisplayConstants.Gallery.SPIRIT_CARDS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_CARD_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(30, AppDisplayConstants.Gallery.ACHIEVEMENTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ACHIEVEMENT_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(31, AppDisplayConstants.Gallery.ARTIFACTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTIFACT_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(32, AppDisplayConstants.Gallery.ARCHITECTURES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARCHITECTURE_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(33, AppDisplayConstants.Gallery.TECHNOLOGIES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TECHNOLOGY_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(34, AppDisplayConstants.Gallery.VEHICLES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.VEHICLE_URL),
            tempGachaMenuPanel);
        CreateGachaButtonUI(35, AppDisplayConstants.Gallery.CORES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CORE_URL),
            tempGachaMenuPanel);
        CreateGachaButtonUI(36, AppDisplayConstants.Gallery.WEAPONS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.WEAPON_URL),  
            tempGachaMenuPanel);
        CreateGachaButtonUI(37, AppDisplayConstants.Gallery.ROBOTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ROBOT_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(38, AppDisplayConstants.Gallery.BADGES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BADGE_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(39, AppDisplayConstants.Gallery.MECHA_BEASTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MECHA_BEAST_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(40, AppDisplayConstants.Gallery.RUNES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.RUNE_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(41, AppDisplayConstants.Gallery.FURNITURES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FURNITURE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FURNITURE_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(42, AppDisplayConstants.Gallery.FOODS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FOOD_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FOOD_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(43, AppDisplayConstants.Gallery.BEVERAGES_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BEVERAGE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BEVERAGE_URL),
            tempGachaMenuPanel);
        CreateGachaButtonUI(44, AppDisplayConstants.Gallery.BUILDINGS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BUILDING_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(45, AppDisplayConstants.Gallery.PLANTS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PLANT_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(46, AppDisplayConstants.Gallery.FASHIONS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FASHION_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(47, AppDisplayConstants.Gallery.EMOJIS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.EMOJI_URL), 
            tempGachaMenuPanel);
        CreateGachaButtonUI(48, AppDisplayConstants.Gallery.CARD_SOLDIERS_GALLERY, itemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SOLDIER_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SOLDIER_URL), 
            tempGachaMenuPanel);

        tempGachaMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateGachaButtonUI(int index, string itemName, Texture2D _itemBackground, Texture2D _itemImage, Texture2D _borderImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(MainButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage itemBackground = transform.Find("ItemBackground").GetComponent<RawImage>();
        if (itemBackground != null && _itemBackground != null)
        {
            itemBackground.texture = _itemBackground;
        }

        // RawImage subBackground = transform.Find("SubBackground").GetComponent<RawImage>();
        // if (subBackground != null && _subBackground != null)
        // {
        //     subBackground.texture = _subBackground;
        // }

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
    }
    public void CreateGacha(Transform tempGachaMenuPanel)
    {
        gachaMenuPanel = tempGachaMenuPanel;
        // DictionaryPanelPrefab = UIManager.Instance.Get("DictionaryPanelPrefab");
        // UI_Blue_Gradient_Radius_Mat_MaskPercent_70 = MaterialManager.Instance.Get("UI_Blue_Gradient_Radius_Mat_MaskPercent_70");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");

        AssignButtonEvent("Button_1", () => GetType(AppConstants.MainType.CARD_HERO, AppDisplayConstants.Gacha.CARD_HEROES_GACHA));
        AssignButtonEvent("Button_2", () => GetType(AppConstants.MainType.BOOK, AppDisplayConstants.Gacha.BOOKS_GACHA));
        AssignButtonEvent("Button_3", () => GetType(AppConstants.MainType.PET, AppDisplayConstants.Gacha.PETS_GACHA));
        AssignButtonEvent("Button_4", () => GetType(AppConstants.MainType.CARD_CAPTAIN, AppDisplayConstants.Gacha.CARD_CAPTAINS_GACHA));
        AssignButtonEvent("Button_5", () => GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT, AppDisplayConstants.Gacha.COLLABORATIONS_GACHA));
        AssignButtonEvent("Button_6", () => GetType(AppConstants.MainType.CARD_MILITARY, AppDisplayConstants.Gacha.CARD_MILITARIES_GACHA));
        AssignButtonEvent("Button_7", () => GetType(AppConstants.MainType.CARD_SPELL, AppDisplayConstants.Gacha.CARD_SPELLS_GACHA));
        AssignButtonEvent("Button_8", () => GetType(AppConstants.MainType.COLLABORATION, AppDisplayConstants.Gacha.COLLABORATIONS_GACHA));
        AssignButtonEvent("Button_9", () => GetType(AppConstants.MainType.CARD_MONSTER, AppDisplayConstants.Gacha.CARD_MONSTERS_GACHA));
        AssignButtonEvent("Button_10", () => GetType(AppConstants.MainType.EQUIPMENT, AppDisplayConstants.Gacha.EQUIPMENTS_GACHA));
        AssignButtonEvent("Button_11", () => GetType(AppConstants.MainType.MEDAL, AppDisplayConstants.Gacha.MEDALS_GACHA));
        AssignButtonEvent("Button_12", () => GetType(AppConstants.MainType.SKILL, AppDisplayConstants.Gacha.SKILLS_GACHA));
        AssignButtonEvent("Button_13", () => GetType(AppConstants.MainType.SYMBOL, AppDisplayConstants.Gacha.SYMBOLS_GACHA));
        AssignButtonEvent("Button_14", () => GetType(AppConstants.MainType.TITLE, AppDisplayConstants.Gacha.TITLES_GACHA));
        AssignButtonEvent("Button_15", () => GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE, AppDisplayConstants.Gacha.MAGIC_FORMATION_CIRCLES_GACHA));
        AssignButtonEvent("Button_16", () => GetType(AppConstants.MainType.RELIC, AppDisplayConstants.Gacha.RELICS_GACHA));
        AssignButtonEvent("Button_17", () => GetType(AppConstants.MainType.CARD_COLONEL, AppDisplayConstants.Gacha.CARD_COLONELS_GACHA));
        AssignButtonEvent("Button_18", () => GetType(AppConstants.MainType.CARD_GENERAL, AppDisplayConstants.Gacha.CARD_GENERALS_GACHA));
        AssignButtonEvent("Button_19", () => GetType(AppConstants.MainType.CARD_ADMIRAL, AppDisplayConstants.Gacha.CARD_ADMIRALS_GACHA));
        AssignButtonEvent("Button_20", () => GetType(AppConstants.MainType.BORDER, AppDisplayConstants.Gacha.BORDERS_GACHA));
        AssignButtonEvent("Button_21", () => GetType(AppConstants.MainType.TALISMAN, AppDisplayConstants.Gacha.TALISMANS_GACHA));
        AssignButtonEvent("Button_22", () => GetType(AppConstants.MainType.PUPPET, AppDisplayConstants.Gacha.PUPPETS_GACHA));
        AssignButtonEvent("Button_23", () => GetType(AppConstants.MainType.ALCHEMY, AppDisplayConstants.Gacha.ALCHEMIES_GACHA));
        AssignButtonEvent("Button_24", () => GetType(AppConstants.MainType.FORGE, AppDisplayConstants.Gacha.FORGES_GACHA));
        AssignButtonEvent("Button_25", () => GetType(AppConstants.MainType.CARD_LIFE, AppDisplayConstants.Gacha.CARD_LIVES_GACHA));
        AssignButtonEvent("Button_26", () => GetType(AppConstants.MainType.ARTWORK, AppDisplayConstants.Gacha.ARTWORKS_GACHA));
        AssignButtonEvent("Button_27", () => GetType(AppConstants.MainType.SPIRIT_BEAST, AppDisplayConstants.Gacha.SPIRIT_BEASTS_GACHA));
        AssignButtonEvent("Button_28", () => GetType(AppConstants.MainType.AVATAR, AppDisplayConstants.Gacha.AVATARS_GACHA));
        AssignButtonEvent("Button_29", () => GetType(AppConstants.MainType.SPIRIT_CARD, AppDisplayConstants.Gacha.SPIRIT_CARDS_GACHA));
        // AssignButtonEvent("Button_30", () => GetType(AppConstants.MainType.ACHIEVEMENT));
        AssignButtonEvent("Button_31", () => GetType(AppConstants.MainType.ARTIFACT, AppDisplayConstants.Gacha.ARTIFACTS_GACHA));
        AssignButtonEvent("Button_32", () => GetType(AppConstants.MainType.ARCHITECTURE, AppDisplayConstants.Gacha.ARCHITECTURES_GACHA));
        AssignButtonEvent("Button_33", () => GetType(AppConstants.MainType.TECHNOLOGY, AppDisplayConstants.Gacha.TECHNOLOGIES_GACHA));
        AssignButtonEvent("Button_34", () => GetType(AppConstants.MainType.VEHICLE, AppDisplayConstants.Gacha.VEHICLES_GACHA));
        AssignButtonEvent("Button_35", () => GetType(AppConstants.MainType.CORE, AppDisplayConstants.Gacha.CORES_GACHA));
        AssignButtonEvent("Button_36", () => GetType(AppConstants.MainType.WEAPON, AppDisplayConstants.Gacha.WEAPONS_GACHA));
        AssignButtonEvent("Button_37", () => GetType(AppConstants.MainType.ROBOT, AppDisplayConstants.Gacha.ROBOTS_GACHA));
        AssignButtonEvent("Button_38", () => GetType(AppConstants.MainType.BADGE, AppDisplayConstants.Gacha.BADGES_GACHA));
        AssignButtonEvent("Button_39", () => GetType(AppConstants.MainType.MECHA_BEAST, AppDisplayConstants.Gacha.MECHA_BEASTS_GACHA));
        AssignButtonEvent("Button_40", () => GetType(AppConstants.MainType.RUNE, AppDisplayConstants.Gacha.RUNES_GACHA));
        AssignButtonEvent("Button_41", () => GetType(AppConstants.MainType.FURNITURE, AppDisplayConstants.Gacha.FURNITURES_GACHA));
        AssignButtonEvent("Button_42", () => GetType(AppConstants.MainType.FOOD, AppDisplayConstants.Gacha.FOODS_GACHA));
        AssignButtonEvent("Button_43", () => GetType(AppConstants.MainType.BEVERAGE, AppDisplayConstants.Gacha.BEVERAGES_GACHA));
        AssignButtonEvent("Button_44", () => GetType(AppConstants.MainType.BUILDING, AppDisplayConstants.Gacha.BUILDINGS_GACHA));
        AssignButtonEvent("Button_45", () => GetType(AppConstants.MainType.PLANT, AppDisplayConstants.Gacha.PLANTS_GACHA));
        AssignButtonEvent("Button_46", () => GetType(AppConstants.MainType.FASHION, AppDisplayConstants.Gacha.FASHIONS_GACHA));
        AssignButtonEvent("Button_47", () => GetType(AppConstants.MainType.EMOJI, AppDisplayConstants.Gacha.EMOJIS_GACHA));
        // GetCardsType();
    }
    void AssignButtonEvent(string buttonName, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = gachaMenuPanel.Find(buttonName);
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
    public void GetType(string type, string typeText)
    {
        mainType = type;
        _ = CreateGachaManagerAsync();
        titleText.text = LocalizationManager.Get(typeText);
    }
    public async Task CreateGachaManagerAsync()
    {
        currentObject = Instantiate(GachaPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        backgroundImage = transform.Find("DictionaryBackground").GetComponent<RawImage>();
        titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Transform currencyTransform = transform.Find("DictionaryCards/Currency");
        Button summonOneButton = transform.Find("DictionaryCards/SummonOneButton").GetComponent<Button>();
        Button summonTenButton = transform.Find("DictionaryCards/SummonTenButton").GetComponent<Button>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        summonOneButton.onClick.AddListener(async () =>
        {
            await LoadGachaEventAsync(1);
        });
        summonTenButton.onClick.AddListener(async () =>
        {
            await LoadGachaEventAsync(10);
        });
        await LoadTicketAsync(currencyTransform);
    }
    public async Task LoadTicketAsync(Transform transform)
    {
        // tickets.Clear();
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BOOK_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_CAPTAIN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MONSTER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_COLONEL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_GENERAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_ADMIRAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MILITARY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SPELL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.COLLABORATION_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.COLLABORATION_EQUIPMENT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.EQUIPMENT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PET_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SKILL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SYMBOL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MEDAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TITLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BORDER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MAGIC_FORMATION_CIRCLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.RELIC_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TALISMAN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PUPPET_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ALCHEMY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FORGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_LIFE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARTWORK_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SPIRIT_BEAST_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.AVATAR_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SPIRIT_CARD_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        // else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        // {
        //     items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ACHIE_TICKET) };
        //     CurrenciesManager.Instance.GetTicketsCurrency(
        //         items,
        //         transform
        //     );

        //     CreateTicketUI(items, transform);
        // }
        else if (mainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARTIFACT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARCHITECTURE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TECHNOLOGY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.VEHICLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CORE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.WEAPON_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ROBOT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BADGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MECHA_BEAST_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.RUNE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FURNITURE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FOOD_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BEVERAGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BUILDING_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PLANT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FASHION_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.EMOJI_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SOLDIER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else
        {
            Debug.Log("Error");
        }
    }
    public async Task LoadGachaEventAsync(int rollNumber)
    {
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_HERO_URL);
            await GachaCardHeroesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BOOK_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_CAPTAIN_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_MONSTER_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_COLONEL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_GENERAL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_ADMIRAL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_MILITARY_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_SPELL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.COLLABORATION_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.COLLABORATION_EQUIPMENT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.EQUIPMENT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.PET_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.SKILL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.SYMBOL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.MEDAL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.TITLE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BORDER_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.MAGIC_FORMATION_CIRCLE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.RELIC_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.TALISMAN_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.PUPPET_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ALCHEMY_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.FORGE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_LIFE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ARTWORK_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.SPIRIT_BEAST_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.AVATAR_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.SPIRIT_CARD_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ACHIEVEMENT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ARTIFACT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ARCHITECTURE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.TECHNOLOGY_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.VEHICLE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CORE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.WEAPON_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ROBOT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BADGE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.MECHA_BEAST_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.RUNE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.FURNITURE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.FOOD_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BEVERAGE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BUILDING_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.PLANT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.FASHION_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.EMOJI_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_SOLDIER_URL);
        }
    }
    public void CreateTicketUI(List<Items> items, Transform transform)
    {
        foreach (Items item in items)
        {
            RawImage oneTicketImage = transform.Find("DictionaryCards/OneTicketImage").GetComponent<RawImage>();
            RawImage tenTicketImage = transform.Find("DictionaryCards/TenTicketImage").GetComponent<RawImage>();
            TextMeshProUGUI oneTicketText = transform.Find("DictionaryCards/OneTicketText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI tenTicketText = transform.Find("DictionaryCards/TenTicketText").GetComponent<TextMeshProUGUI>();

            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(item.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            oneTicketImage.texture = texture;
            tenTicketImage.texture = texture;
            oneTicketText.text = "1";
            tenTicketText.text = "10";
        }
    }
    public async Task GachaCardHeroesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();
        // Load data 1 lần
        var allCardHeroes = await CardHeroesService.Create()
            .GetCardHeroesWithoutLimitAsync();

        var allItems = await ItemsService.Create()
            .GetItemsAsync();

        // Load rates từ config mới
        var mainRates = await GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_HERO);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_HERO);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserCurrenciesService.Create()
                .UpdateUserCurrencyAsync(item.Id, rollNumber);
        }

        // Group theo type
        var cardByType = allCardHeroes
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        var itemByType = allItems
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        List<CardHeroes> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_HERO)
            {
                // Tier 2
                string cardType = RollHelper.RollByRate(
                    cardTypeRates
                );

                if (cardByType.TryGetValue(
                    cardType,
                    out var filteredCards
                ) && filteredCards.Any())
                {
                    var selectedCard = filteredCards[
                        UnityEngine.Random.Range(
                            0,
                            filteredCards.Count
                        )
                    ];

                    rewardedCards.Add(selectedCard);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.RUNE,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rare,
                    });
                }
            }
            else if (mainReward == AppConstants.MainType.ITEM)
            {
                string itemType = RollHelper.RollByRate(
                    itemTypeRates
                );

                if (itemByType.TryGetValue(
                    itemType,
                    out var filteredItems
                ) && filteredItems.Any())
                {
                    var selectedItem = filteredItems[
                        UnityEngine.Random.Range(
                            0,
                            filteredItems.Count
                        )
                    ];

                    if (rewardedItems.ContainsKey(
                        selectedItem.Id
                    ))
                    {
                        var current =
                            rewardedItems[selectedItem.Id];

                        rewardedItems[selectedItem.Id] = (
                            current.item,
                            current.quantity + 1
                        );
                    }
                    else
                    {
                        rewardedItems[selectedItem.Id] = (
                            selectedItem,
                            1
                        );
                    }

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.ITEM,
                        RewardId = selectedItem.Id,
                        Name = selectedItem.Name,
                        Image = selectedItem.Image,
                        Rare = AppConstants.Rare.NONE,
                    });
                }
            }
        }

        // giữ nguyên insert card
        if (rewardedCards.Any())
        {
            await UserCardHeroesService.Create()
                .InsertOrUpdateUserCardHeroesBatchAsync(
                    rewardedCards
                );
        }

        if (rewardedItems.Any())
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemsBatchAsync(
                    rewardedItems.Values.ToList()
                );
        }
    }
    public async Task GachaRunesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load data 1 lần
        var allRunes = await RunesService.Create()
            .GetRunesWithoutLimitAsync();

        var allItems = await ItemsService.Create()
            .GetItemsAsync();

        // Load rates
        var mainRates = await GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.RUNE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserCurrenciesService.Create()
                .UpdateUserCurrencyAsync(item.Id, rollNumber);
        }

        // Item group theo type
        var itemByType = allItems
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        List<Runes> rewardedRunes = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.RUNE)
            {
                // random trực tiếp rune
                if (allRunes.Any())
                {
                    var selectedRune = allRunes[
                        UnityEngine.Random.Range(
                            0,
                            allRunes.Count
                        )
                    ];

                    rewardedRunes.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.RUNE,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rare,
                    });
                }
            }
            else if (mainReward == AppConstants.MainType.ITEM)
            {
                string itemType = RollHelper.RollByRate(
                    itemTypeRates
                );

                if (itemByType.TryGetValue(
                    itemType,
                    out var filteredItems
                ) && filteredItems.Any())
                {
                    var selectedItem = filteredItems[
                        UnityEngine.Random.Range(
                            0,
                            filteredItems.Count
                        )
                    ];

                    if (rewardedItems.ContainsKey(
                        selectedItem.Id
                    ))
                    {
                        var current =
                            rewardedItems[selectedItem.Id];

                        rewardedItems[selectedItem.Id] = (
                            current.item,
                            current.quantity + 1
                        );
                    }
                    else
                    {
                        rewardedItems[selectedItem.Id] = (
                            selectedItem,
                            1
                        );
                    }

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.ITEM,
                        RewardId = selectedItem.Id,
                        Name = selectedItem.Name,
                        Image = selectedItem.Image,
                        Rare = AppConstants.Rare.NONE,
                    });
                }
            }
        }

        // Insert rune
        if (rewardedRunes.Any())
        {
            await UserRunesService.Create()
                .InsertOrUpdateUserRunesBatchAsync(
                    rewardedRunes
                );
        }

        // Insert item
        if (rewardedItems.Any())
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemsBatchAsync(
                    rewardedItems.Values.ToList()
                );
        }
    }
    public void CreateSummonArea(List<GachaRewardResultDTO> rewards)
    {
        Transform summonArea = currentObject.transform.Find("SummonArea");
        Transform gridLayout = summonArea.Find("GridLayout");
        summonArea.gameObject.SetActive(true);

        // clear old UI
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }

        foreach (var reward in rewards)
        {
            GameObject rewardObject = Instantiate(GachaButtonPrefab, gridLayout);

            RawImage image = rewardObject.transform.Find("Image").GetComponent<RawImage>();
            Image background1Image = rewardObject.transform.Find("Background1").GetComponent<Image>();
            Image background2Image = rewardObject.transform.Find("Background2").GetComponent<Image>();

            background1Image.material = MaterialHelper.GetMaterial(reward.Rare);
            background2Image.material = MaterialHelper.GetMaterial(reward.Rare);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(reward.Image);

            if (rewardTexture != null)
            {
                image.texture = rewardTexture;
            }
        }
    }
}