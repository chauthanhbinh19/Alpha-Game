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
            TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_LIFE_URL),
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
        // RawImage oneTicketImage = transform.Find("DictionaryCards/OneTicketImage").GetComponent<RawImage>();
        // RawImage tenTicketImage = transform.Find("DictionaryCards/TenTicketImage").GetComponent<RawImage>();
        // TextMeshProUGUI oneTicketText = transform.Find("DictionaryCards/OneTicketText").GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI tenTicketText = transform.Find("DictionaryCards/TenTicketText").GetComponent<TextMeshProUGUI>();
        Button summonOneButton = transform.Find("DictionaryCards/SummonOneButton").GetComponent<Button>();
        Button summonTenButton = transform.Find("DictionaryCards/SummonTenButton").GetComponent<Button>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        summonOneButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await LoadGachaEventAsync(1);
        });
        summonTenButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await LoadGachaEventAsync(10);
        });

        await LoadGachaUIAsync(transform);        
    }
    public async Task LoadGachaUIAsync(Transform transform)
    {
        Transform currencyTransform = transform.Find("DictionaryCards/Currency");
        // tickets.Clear();
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_HERO_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BOOK_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BOOK_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_CAPTAIN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_CAPTAIN_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MONSTER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_MONSTER_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_COLONEL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_COLONEL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_GENERAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_GENERAL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_ADMIRAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_ADMIRAL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MILITARY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_MILITARY_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SPELL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_SPELL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.COLLABORATION_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.COLLABORATION_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.COLLABORATION_EQUIPMENT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.COLLABORATION_EQUIPMENT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.EQUIPMENT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.EQUIPMENT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PET_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.PET_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SKILL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.SKILL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SYMBOL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.SYMBOL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MEDAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.MEDAL_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TITLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.TITLE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BORDER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BORDER_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MAGIC_FORMATION_CIRCLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.MAGIC_FORMATION_CIRCLE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.RELIC_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.RELIC_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TALISMAN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.TALISMAN_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PUPPET_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.PUPPET_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ALCHEMY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ALCHEMY_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FORGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.FORGE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_LIFE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_LIFE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARTWORK_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ARTWORK_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SPIRIT_BEAST_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.SPIRIT_BEAST_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.AVATAR_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.AVATAR_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SPIRIT_CARD_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.SPIRIT_CARD_URL);
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
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ARTIFACT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARCHITECTURE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ARCHITECTURE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TECHNOLOGY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.TECHNOLOGY_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.VEHICLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.VEHICLE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CORE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CORE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.WEAPON_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.WEAPON_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ROBOT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.ROBOT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BADGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BADGE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MECHA_BEAST_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.MECHA_BEAST_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.RUNE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.RUNE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FURNITURE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.FURNITURE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FOOD_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.FOOD_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BEVERAGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BEVERAGE_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BUILDING_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.BUILDING_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PLANT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.PLANT_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FASHION_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.FASHION_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.EMOJI_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.EMOJI_URL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SOLDIER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                currencyTransform
            );

            CreateTicketUI(tickets, transform);
            backgroundImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gacha.CARD_SOLDIER_URL);
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
            await GachaCardHeroesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            await GachaBooksAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            await GachaCardCaptainsAsync(rollNumber);   
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            await GachaCardMonstersAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            await GachaCardColonelsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            await GachaCardGeneralsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            await GachaCardAdmiralsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            await GachaCardMilitariesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            await GachaCardSpellsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            await GachaCollaborationsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            await GachaCollaborationEquipmentsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            await GachaEquipmentsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            await GachaPetsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            await GachaSkillsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            await GachaSymbolsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            await GachaMedalsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            await GachaTitlesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            await GachaBordersAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            await GachaMagicFormationCirclesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            await GachaRelicsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            await GachaTalismansAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            await GachaPuppetsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            await GachaAlchemiesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            await GachaForgesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            await GachaCardLivesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            await GachaArtworksAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            await GachaSpiritBeastsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            await GachaAvatarsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            await GachaSpiritCardsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            await GachaAchievementsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            await GachaArtifactsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            await GachaArchitecturesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            await GachaTechnologiesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            await GachaVehiclesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            await GachaCoresAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            await GachaWeaponsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            await GachaRobotsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            await GachaBadgesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            await GachaMechaBeastsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            await GachaRunesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            await GachaFurnituresAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            await GachaFoodsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            await GachaBeveragesAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            await GachaBuildingsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            await GachaPlantsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            await GachaFashionsAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            await GachaEmojisAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            await GachaCardSoldiersAsync(rollNumber);
        }
        else if (mainType.Equals(AppConstants.MainType.OUTFIT))
        {
            await GachaOutfitsAsync(rollNumber);
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
    public async Task GachaAchievementsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.ACHIEVEMENT);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allAchievements = GameDataCacheConfig.Instance.AllAchievements;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Achievements> rewardedAchievements = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.ACHIEVEMENT)
            {
                // random trực tiếp rune
                if (allAchievements.Any())
                {
                    var selectedRune = allAchievements[
                        UnityEngine.Random.Range(
                            0,
                            allAchievements.Count
                        )
                    ];

                    rewardedAchievements.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.ACHIEVEMENT,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedAchievements.Any())
        {
            await UserAchievementsService.Create()
                .InsertOrUpdateUserAchievementsBatchAsync(
                    rewardedAchievements
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

        CreateSummonArea(results);
    }
    public async Task GachaAlchemiesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.ALCHEMY);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.ALCHEMY);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.AlchemiesByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Alchemies> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.ALCHEMY)
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
                        MainType = AppConstants.MainType.ALCHEMY,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserAlchemiesService.Create()
                .InsertOrUpdateUserAlchemiesBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaArtworksAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.ARTWORK);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.ARTWORK);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.ArtworksByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Artworks> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.ARTWORK)
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
                        MainType = AppConstants.MainType.ARTWORK,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserArtworksService.Create()
                .InsertOrUpdateUserArtworksBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaArchitecturesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.ARCHITECTURE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allArchitectures = GameDataCacheConfig.Instance.AllArchitectures;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Architectures> rewardedArchitectures = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.ARCHITECTURE)
            {
                // random trực tiếp rune
                if (allArchitectures.Any())
                {
                    var selectedRune = allArchitectures[
                        UnityEngine.Random.Range(
                            0,
                            allArchitectures.Count
                        )
                    ];

                    rewardedArchitectures.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.ARCHITECTURE,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedArchitectures.Any())
        {
            await UserArchitecturesService.Create()
                .InsertOrUpdateUserArchitecturesBatchAsync(
                    rewardedArchitectures
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

        CreateSummonArea(results);
    }
    public async Task GachaArtifactsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.ARTIFACT);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allArtifacts = GameDataCacheConfig.Instance.AllArtifacts;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Artifacts> rewardedArtifacts = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.ARTIFACT)
            {
                // random trực tiếp rune
                if (allArtifacts.Any())
                {
                    var selectedRune = allArtifacts[
                        UnityEngine.Random.Range(
                            0,
                            allArtifacts.Count
                        )
                    ];

                    rewardedArtifacts.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.ARTIFACT,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedArtifacts.Any())
        {
            await UserArtifactsService.Create()
                .InsertOrUpdateUserArtifactsBatchAsync(
                    rewardedArtifacts
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

        CreateSummonArea(results);
    }
    public async Task GachaAvatarsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.AVATAR);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allAvatars = GameDataCacheConfig.Instance.AllAvatars;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Avatars> rewardedAvatars = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.AVATAR)
            {
                // random trực tiếp rune
                if (allAvatars.Any())
                {
                    var selectedRune = allAvatars[
                        UnityEngine.Random.Range(
                            0,
                            allAvatars.Count
                        )
                    ];

                    rewardedAvatars.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.AVATAR,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedAvatars.Any())
        {
            await UserAvatarsService.Create()
                .InsertOrUpdateUserAvatarsBatchAsync(
                    rewardedAvatars
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

        CreateSummonArea(results);
    }
    public async Task GachaBadgesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.BADGE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allBadges = GameDataCacheConfig.Instance.AllBadges;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Badges> rewardedBadges = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.BADGE)
            {
                // random trực tiếp rune
                if (allBadges.Any())
                {
                    var selectedRune = allBadges[
                        UnityEngine.Random.Range(
                            0,
                            allBadges.Count
                        )
                    ];

                    rewardedBadges.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.BADGE,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedBadges.Any())
        {
            await UserBadgesService.Create()
                .InsertOrUpdateUserBadgesBatchAsync(
                    rewardedBadges
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

        CreateSummonArea(results);
    }
    public async Task GachaBeveragesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.BEVERAGE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allBeverages = GameDataCacheConfig.Instance.AllBeverages;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Beverages> rewardedBeverages = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.BEVERAGE)
            {
                // random trực tiếp rune
                if (allBeverages.Any())
                {
                    var selectedRune = allBeverages[
                        UnityEngine.Random.Range(
                            0,
                            allBeverages.Count
                        )
                    ];

                    rewardedBeverages.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.BEVERAGE,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedBeverages.Any())
        {
            await UserBeveragesService.Create()
                .InsertOrUpdateUserBeveragesBatchAsync(
                    rewardedBeverages
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

        CreateSummonArea(results);
    }
    public async Task GachaBooksAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.BOOK);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.BOOK);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.BooksByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Books> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.BOOK)
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
                        MainType = AppConstants.MainType.BOOK,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserBooksService.Create()
                .InsertOrUpdateUserBooksBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaBordersAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.BORDER);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allBorders = GameDataCacheConfig.Instance.AllBorders;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Borders> rewardedBorders = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.BORDER)
            {
                // random trực tiếp rune
                if (allBorders.Any())
                {
                    var selectedRune = allBorders[
                        UnityEngine.Random.Range(
                            0,
                            allBorders.Count
                        )
                    ];

                    rewardedBorders.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.BORDER,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedBorders.Any())
        {
            await UserBordersService.Create()
                .InsertOrUpdateUserBordersBatchAsync(
                    rewardedBorders
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

        CreateSummonArea(results);
    }
    public async Task GachaBuildingsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.BUILDING);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.BUILDING);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.BuildingsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Buildings> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.BUILDING)
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
                        MainType = AppConstants.MainType.BUILDING,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserBuildingsService.Create()
                .InsertOrUpdateUserBuildingsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardAdmiralsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_ADMIRAL);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_ADMIRAL);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardAdmiralsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardAdmirals> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_ADMIRAL)
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
                        MainType = AppConstants.MainType.CARD_ADMIRAL,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardAdmiralsService.Create()
                .InsertOrUpdateUserCardAdmiralsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardCaptainsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_CAPTAIN);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_CAPTAIN);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardCaptainsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardCaptains> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_CAPTAIN)
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
                        MainType = AppConstants.MainType.CARD_CAPTAIN,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardCaptainsService.Create()
                .InsertOrUpdateUserCardCaptainsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardColonelsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_COLONEL);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_COLONEL);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardColonelsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardColonels> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_COLONEL)
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
                        MainType = AppConstants.MainType.CARD_COLONEL,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardColonelsService.Create()
                .InsertOrUpdateUserCardColonelsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardGeneralsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_GENERAL);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_GENERAL);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardGeneralsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardGenerals> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_GENERAL)
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
                        MainType = AppConstants.MainType.CARD_GENERAL,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardGeneralsService.Create()
                .InsertOrUpdateUserCardGeneralsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardHeroesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_HERO);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_HERO);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardHeroesByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

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
                        MainType = AppConstants.MainType.CARD_HERO,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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

        CreateSummonArea(results);
    }
    public async Task GachaCardLivesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_LIFE);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_LIFE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardLivesByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardLives> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_LIFE)
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
                        MainType = AppConstants.MainType.CARD_LIFE,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardLivesService.Create()
                .InsertOrUpdateUserCardLivesBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardMonstersAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_MONSTER);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_MONSTER);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardMonstersByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardMonsters> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_MONSTER)
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
                        MainType = AppConstants.MainType.CARD_MONSTER,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardMonstersService.Create()
                .InsertOrUpdateUserCardMonstersBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardMilitariesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_MILITARY);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_MILITARY);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardMilitariesByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardMilitaries> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_MILITARY)
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
                        MainType = AppConstants.MainType.CARD_MILITARY,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardMilitariesService.Create()
                .InsertOrUpdateUserCardMilitariesBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardSoldiersAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_SOLDIER);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_SOLDIER);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardSoldiersByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardSoldiers> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_SOLDIER)
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
                        MainType = AppConstants.MainType.CARD_SOLDIER,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardSoldiersService.Create()
                .InsertOrUpdateUserCardSoldiersBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCardSpellsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_SPELL);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_SPELL);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CardSpellsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CardSpells> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_SPELL)
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
                        MainType = AppConstants.MainType.CARD_SPELL,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCardSpellsService.Create()
                .InsertOrUpdateUserCardSpellsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCollaborationEquipmentsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.COLLABORATION_EQUIPMENT);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.COLLABORATION_EQUIPMENT);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.CollaborationEquipmentsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<CollaborationEquipments> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.COLLABORATION_EQUIPMENT)
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
                        MainType = AppConstants.MainType.COLLABORATION_EQUIPMENT,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserCollaborationEquipmentsService.Create()
                .InsertOrUpdateUserCollaborationEquipmentsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaCollaborationsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.COLLABORATION);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allCollaborations = GameDataCacheConfig.Instance.AllCollaborations;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Collaborations> rewardedCollaborations = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.COLLABORATION)
            {
                // random trực tiếp rune
                if (allCollaborations.Any())
                {
                    var selectedRune = allCollaborations[
                        UnityEngine.Random.Range(
                            0,
                            allCollaborations.Count
                        )
                    ];

                    rewardedCollaborations.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.COLLABORATION,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedCollaborations.Any())
        {
            await UserCollaborationsService.Create()
                .InsertOrUpdateUserCollaborationsBatchAsync(
                    rewardedCollaborations
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

        CreateSummonArea(results);
    }
    public async Task GachaCoresAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CORE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allCores = GameDataCacheConfig.Instance.AllCores;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Cores> rewardedCores = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CORE)
            {
                // random trực tiếp rune
                if (allCores.Any())
                {
                    var selectedRune = allCores[
                        UnityEngine.Random.Range(
                            0,
                            allCores.Count
                        )
                    ];

                    rewardedCores.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.CORE,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedCores.Any())
        {
            await UserCoresService.Create()
                .InsertOrUpdateUserCoresBatchAsync(
                    rewardedCores
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

        CreateSummonArea(results);
    }
    public async Task GachaEmojisAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.EMOJI);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allEmojis = GameDataCacheConfig.Instance.AllEmojis;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Emojis> rewardedEmojis = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.EMOJI)
            {
                // random trực tiếp rune
                if (allEmojis.Any())
                {
                    var selectedRune = allEmojis[
                        UnityEngine.Random.Range(
                            0,
                            allEmojis.Count
                        )
                    ];

                    rewardedEmojis.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.EMOJI,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedEmojis.Any())
        {
            await UserEmojisService.Create()
                .InsertOrUpdateUserEmojisBatchAsync(
                    rewardedEmojis
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

        CreateSummonArea(results);
    }
    public async Task GachaEquipmentsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.EQUIPMENT);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.EQUIPMENT);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.EquipmentsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Equipments> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.EQUIPMENT)
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
                        MainType = AppConstants.MainType.EQUIPMENT,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
        // if (rewardedCards.Any())
        // {
        //     await UserEquipmentsService.Create()
        //         .InsertOrUpdateUserEquipmentsBatchAsync(
        //             rewardedCards
        //         );
        // }

        if (rewardedItems.Any())
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemsBatchAsync(
                    rewardedItems.Values.ToList()
                );
        }

        CreateSummonArea(results);
    }
    public async Task GachaFashionsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.FASHION);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.FASHION);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.FashionsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Fashions> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.FASHION)
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
                        MainType = AppConstants.MainType.FASHION,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserFashionsService.Create()
                .InsertOrUpdateUserFashionsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaFoodsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.FOOD);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allFoods = GameDataCacheConfig.Instance.AllFoods;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Foods> rewardedFoods = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.FOOD)
            {
                // random trực tiếp rune
                if (allFoods.Any())
                {
                    var selectedRune = allFoods[
                        UnityEngine.Random.Range(
                            0,
                            allFoods.Count
                        )
                    ];

                    rewardedFoods.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.FOOD,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedFoods.Any())
        {
            await UserFoodsService.Create()
                .InsertOrUpdateUserFoodsBatchAsync(
                    rewardedFoods
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

        CreateSummonArea(results);
    }
    public async Task GachaForgesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.FORGE);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.FORGE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.ForgesByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Forges> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.FORGE)
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
                        MainType = AppConstants.MainType.FORGE,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserForgesService.Create()
                .InsertOrUpdateUserForgesBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaFurnituresAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.FURNITURE);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.FURNITURE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.FurnituresByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Furnitures> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.FURNITURE)
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
                        MainType = AppConstants.MainType.FURNITURE,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserFurnituresService.Create()
                .InsertOrUpdateUserFurnituresBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaMagicFormationCirclesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.MAGIC_FORMATION_CIRCLE);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.MAGIC_FORMATION_CIRCLE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.MagicFormationCirclesByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<MagicFormationCircles> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.MAGIC_FORMATION_CIRCLE)
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
                        MainType = AppConstants.MainType.MAGIC_FORMATION_CIRCLE,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserMagicFormationCirclesService.Create()
                .InsertOrUpdateUserMagicFormationCirclesBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaMechaBeastsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.MECHA_BEAST);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allMechaBeasts = GameDataCacheConfig.Instance.AllMechaBeasts;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<MechaBeasts> rewardedMechaBeasts = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.MECHA_BEAST)
            {
                // random trực tiếp rune
                if (allMechaBeasts.Any())
                {
                    var selectedRune = allMechaBeasts[
                        UnityEngine.Random.Range(
                            0,
                            allMechaBeasts.Count
                        )
                    ];

                    rewardedMechaBeasts.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.MECHA_BEAST,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedMechaBeasts.Any())
        {
            await UserMechaBeastsService.Create()
                .InsertOrUpdateUserMechaBeastsBatchAsync(
                    rewardedMechaBeasts
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

        CreateSummonArea(results);
    }
    public async Task GachaMedalsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.MEDAL);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allMedals = GameDataCacheConfig.Instance.AllMedals;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Medals> rewardedMedals = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.MEDAL)
            {
                // random trực tiếp rune
                if (allMedals.Any())
                {
                    var selectedRune = allMedals[
                        UnityEngine.Random.Range(
                            0,
                            allMedals.Count
                        )
                    ];

                    rewardedMedals.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.MEDAL,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedMedals.Any())
        {
            await UserMedalsService.Create()
                .InsertOrUpdateUserMedalsBatchAsync(
                    rewardedMedals
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

        CreateSummonArea(results);
    }
    public async Task GachaPetsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.PET);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.PET);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.PetsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Pets> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.PET)
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
                        MainType = AppConstants.MainType.PET,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserPetsService.Create()
                .InsertOrUpdateUserPetsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaPlantsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.PLANT);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allPlants = GameDataCacheConfig.Instance.AllPlants;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Plants> rewardedPlants = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.PLANT)
            {
                // random trực tiếp rune
                if (allPlants.Any())
                {
                    var selectedRune = allPlants[
                        UnityEngine.Random.Range(
                            0,
                            allPlants.Count
                        )
                    ];

                    rewardedPlants.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.PLANT,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedPlants.Any())
        {
            await UserPlantsService.Create()
                .InsertOrUpdateUserPlantsBatchAsync(
                    rewardedPlants
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

        CreateSummonArea(results);
    }
    public async Task GachaPuppetsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.PUPPET);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.PUPPET);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.PuppetsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Puppets> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.PUPPET)
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
                        MainType = AppConstants.MainType.PUPPET,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserPuppetsService.Create()
                .InsertOrUpdateUserPuppetsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaRelicsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.RELIC);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.RELIC);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.RelicsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Relics> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.RELIC)
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
                        MainType = AppConstants.MainType.RELIC,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserRelicsService.Create()
                .InsertOrUpdateUserRelicsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaRobotsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.ROBOT);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allRobots = GameDataCacheConfig.Instance.AllRobots;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Robots> rewardedRobots = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.ROBOT)
            {
                // random trực tiếp rune
                if (allRobots.Any())
                {
                    var selectedRune = allRobots[
                        UnityEngine.Random.Range(
                            0,
                            allRobots.Count
                        )
                    ];

                    rewardedRobots.Add(selectedRune);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.ROBOT,
                        RewardId = selectedRune.Id,
                        Name = selectedRune.Name,
                        Image = selectedRune.Image,
                        Rare = selectedRune.Rarity,
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
        if (rewardedRobots.Any())
        {
            await UserRobotsService.Create()
                .InsertOrUpdateUserRobotsBatchAsync(
                    rewardedRobots
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

        CreateSummonArea(results);
    }
    public async Task GachaRunesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.RUNE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allRunes = GameDataCacheConfig.Instance.AllRunes;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

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
                        Rare = selectedRune.Rarity,
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

        CreateSummonArea(results);
    }
    public async Task GachaSkillsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.SKILL);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.SKILL);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.SkillsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Skills> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.SKILL)
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
                        MainType = AppConstants.MainType.SKILL,
                        RewardId = selectedCard.Id,
                        Name = selectedCard.Name,
                        Image = selectedCard.Image,
                        Rare = selectedCard.Rarity,
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
            await UserSkillsService.Create()
                .InsertOrUpdateUserSkillsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaSpiritBeastsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.SPIRIT_BEAST);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allSpiritBeasts = GameDataCacheConfig.Instance.AllSpiritBeasts;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<SpiritBeasts> rewardedSpiritBeasts = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.SPIRIT_BEAST)
            {
                // random trực tiếp rune
                if (allSpiritBeasts.Any())
                {
                    var selectedSpiritBeast = allSpiritBeasts[
                        UnityEngine.Random.Range(
                            0,
                            allSpiritBeasts.Count
                        )
                    ];

                    rewardedSpiritBeasts.Add(selectedSpiritBeast);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.SPIRIT_BEAST,
                        RewardId = selectedSpiritBeast.Id,
                        Name = selectedSpiritBeast.Name,
                        Image = selectedSpiritBeast.Image,
                        Rare = selectedSpiritBeast.Rarity,
                        Quantity = selectedSpiritBeast.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // Insert rune
        if (rewardedSpiritBeasts.Any())
        {
            await UserSpiritBeastsService.Create()
                .InsertOrUpdateUserSpiritBeastsBatchAsync(
                    rewardedSpiritBeasts
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

        CreateSummonArea(results);
    }
    public async Task GachaSpiritCardsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.SPIRIT_CARD);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.SPIRIT_CARD);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.SpiritCardsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<SpiritCards> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.SPIRIT_CARD)
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
                    var selectedSpiritCard = filteredCards[
                        UnityEngine.Random.Range(
                            0,
                            filteredCards.Count
                        )
                    ];

                    rewardedCards.Add(selectedSpiritCard);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.SPIRIT_CARD,
                        RewardId = selectedSpiritCard.Id,
                        Name = selectedSpiritCard.Name,
                        Image = selectedSpiritCard.Image,
                        Rare = selectedSpiritCard.Rarity,
                        Quantity = selectedSpiritCard.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // giữ nguyên insert card
        if (rewardedCards.Any())
        {
            await UserSpiritCardsService.Create()
                .InsertOrUpdateUserSpiritCardsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaSymbolsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.SYMBOL);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.SYMBOL);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.SymbolsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Symbols> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.SYMBOL)
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
                    var selectedSymbol = filteredCards[
                        UnityEngine.Random.Range(
                            0,
                            filteredCards.Count
                        )
                    ];

                    rewardedCards.Add(selectedSymbol);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.SYMBOL,
                        RewardId = selectedSymbol.Id,
                        Name = selectedSymbol.Name,
                        Image = selectedSymbol.Image,
                        Rare = selectedSymbol.Rarity,
                        Quantity = selectedSymbol.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // giữ nguyên insert card
        if (rewardedCards.Any())
        {
            await UserSymbolsService.Create()
                .InsertOrUpdateUserSymbolsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaTalismansAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.TALISMAN);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.TALISMAN);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.TalismansByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Talismans> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.TALISMAN)
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
                    var selectedTalisman = filteredCards[
                        UnityEngine.Random.Range(
                            0,
                            filteredCards.Count
                        )
                    ];

                    rewardedCards.Add(selectedTalisman);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.TALISMAN,
                        RewardId = selectedTalisman.Id,
                        Name = selectedTalisman.Name,
                        Image = selectedTalisman.Image,
                        Rare = selectedTalisman.Rarity,
                        Quantity = selectedTalisman.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // giữ nguyên insert card
        if (rewardedCards.Any())
        {
            await UserTalismansService.Create()
                .InsertOrUpdateUserTalismansBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaTechnologiesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.TECHNOLOGY);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allTechnologies = GameDataCacheConfig.Instance.AllTechnologies;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Technologies> rewardedTechnologies = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.TECHNOLOGY)
            {
                // random trực tiếp rune
                if (allTechnologies.Any())
                {
                    var selectedTechnology = allTechnologies[
                        UnityEngine.Random.Range(
                            0,
                            allTechnologies.Count
                        )
                    ];

                    rewardedTechnologies.Add(selectedTechnology);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.TECHNOLOGY,
                        RewardId = selectedTechnology.Id,
                        Name = selectedTechnology.Name,
                        Image = selectedTechnology.Image,
                        Rare = selectedTechnology.Rarity,
                        Quantity = selectedTechnology.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // Insert rune
        if (rewardedTechnologies.Any())
        {
            await UserTechnologiesService.Create()
                .InsertOrUpdateUserTechnologiesBatchAsync(
                    rewardedTechnologies
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

        CreateSummonArea(results);
    }
    public async Task GachaTitlesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.TITLE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        
        // Item group theo type
        var allTitles = GameDataCacheConfig.Instance.AllTitles;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Titles> rewardedTitles = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.TITLE)
            {
                // random trực tiếp rune
                if (allTitles.Any())
                {
                    var selectedTitle = allTitles[
                        UnityEngine.Random.Range(
                            0,
                            allTitles.Count
                        )
                    ];

                    rewardedTitles.Add(selectedTitle);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.TITLE,
                        RewardId = selectedTitle.Id,
                        Name = selectedTitle.Name,
                        Image = selectedTitle.Image,
                        Rare = selectedTitle.Rarity,
                        Quantity = selectedTitle.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // Insert rune
        if (rewardedTitles.Any())
        {
            await UserTitlesService.Create()
                .InsertOrUpdateUserTitlesBatchAsync(
                    rewardedTitles
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

        CreateSummonArea(results);
    }
    public async Task GachaVehiclesAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.VEHICLE);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.VEHICLE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.VehiclesByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Vehicles> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.VEHICLE)
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
                    var selectedVehicle = filteredCards[
                        UnityEngine.Random.Range(
                            0,
                            filteredCards.Count
                        )
                    ];

                    rewardedCards.Add(selectedVehicle);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.VEHICLE,
                        RewardId = selectedVehicle.Id,
                        Name = selectedVehicle.Name,
                        Image = selectedVehicle.Image,
                        Rare = selectedVehicle.Rarity,
                        Quantity = selectedVehicle.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // giữ nguyên insert card
        if (rewardedCards.Any())
        {
            await UserVehiclesService.Create()
                .InsertOrUpdateUserVehiclesBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaWeaponsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.WEAPON);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.WEAPON);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.WeaponsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Weapons> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.WEAPON)
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
                    var selectedWeapon = filteredCards[
                        UnityEngine.Random.Range(
                            0,
                            filteredCards.Count
                        )
                    ];

                    rewardedCards.Add(selectedWeapon);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.WEAPON,
                        RewardId = selectedWeapon.Id,
                        Name = selectedWeapon.Name,
                        Image = selectedWeapon.Image,
                        Rare = selectedWeapon.Rarity,
                        Quantity = selectedWeapon.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // giữ nguyên insert card
        if (rewardedCards.Any())
        {
            await UserWeaponsService.Create()
                .InsertOrUpdateUserWeaponsBatchAsync(
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

        CreateSummonArea(results);
    }
    public async Task GachaOutfitsAsync(int rollNumber)
    {
        var results = new List<GachaRewardResultDTO>();

        // Load rates từ config mới
        var mainRates = GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.OUTFIT);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.OUTFIT);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, item, rollNumber);
        }

        // Group theo type
        var cardByType = GameDataCacheConfig.Instance.OutfitsByType;
        var itemByType = GameDataCacheConfig.Instance.ItemsByType;

        List<Outfits> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.VEHICLE)
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
                    var selectedOutfit = filteredCards[
                        UnityEngine.Random.Range(
                            0,
                            filteredCards.Count
                        )
                    ];

                    rewardedCards.Add(selectedOutfit);

                    results.Add(new GachaRewardResultDTO
                    {
                        MainType = AppConstants.MainType.OUTFIT,
                        RewardId = selectedOutfit.Id,
                        Name = selectedOutfit.Name,
                        Image = selectedOutfit.Image,
                        Rare = selectedOutfit.Rarity,
                        Quantity = selectedOutfit.Quantity
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
                        Quantity = selectedItem.Quantity
                    });
                }
            }
        }

        // giữ nguyên insert card
        if (rewardedCards.Any())
        {
            await UserOutfitsService.Create()
                .InsertOrUpdateUserOutfitsBatchAsync(
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

        CreateSummonArea(results);
    }
    public void CreateSummonArea(List<GachaRewardResultDTO> rewards)
    {
        Transform summonArea = currentObject.transform.Find("SummonArea");
        Transform gridLayout = summonArea.Find("GridLayout");
        summonArea.gameObject.SetActive(true);

        Button closeButton = summonArea.GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(gridLayout);
            summonArea.gameObject.SetActive(false);
        });

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
            // Image background2Image = rewardObject.transform.Find("Background2").GetComponent<Image>();

            background1Image.material = MaterialHelper.GetMaterial(reward.Rare);
            // background2Image.material = MaterialHelper.GetMaterial(reward.Rare);
            LoadGachaSummonArea(rewardObject.transform, reward);
        }
    }
    public void LoadGachaSummonArea(Transform transform, GachaRewardResultDTO reward)
    {
        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        RawImage borderImage = transform.Find("BorderImage").GetComponent<RawImage>();
        RawImage cardImage = transform.Find("CardImage").GetComponent<RawImage>();
        RawImage itemImage = transform.Find("ItemImage").GetComponent<RawImage>();
        TextMeshProUGUI nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.BOOK_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_CAPTAIN_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MONSTER_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_COLONEL_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_GENERAL_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_ADMIRAL_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SPELL_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SPELL_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_EQUIPMENT_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.EQUIPMENT_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.PET_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.SKILL_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.SYMBOL_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.MEDAL_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.TITLE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.BORDER_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.MAGIC_FORMATION_CIRCLE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.RELIC_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.TALISMAN_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.PUPPET_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.ALCHEMY_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.FORGE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_LIFE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_LIFE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTWORK_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_BEAST_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.AVATAR_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.AVATAR_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_CARD_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.ACHIEVEMENT_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTIFACT_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARCHITECTURE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.TECHNOLOGY_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.VEHICLE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CORE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.WEAPON_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.ROBOT_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.BADGE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.MECHA_BEAST_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.RUNE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FURNITURE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.FURNITURE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FOOD_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.FOOD_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BEVERAGE_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.BEVERAGE_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.BUILDING_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.PLANT_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.FASHION_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.EMOJI_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(true);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                itemImage.texture = rewardTexture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(itemImage, rewardTexture);
            }
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SOLDIER_URL);
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SOLDIER_URL);
            nameText.text = reward.Name;
            quantityText.text = reward.Quantity.ToString();
            rareText.text = reward.Rare;

            cardImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);

            Texture2D rewardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(reward.Image));

            if (rewardTexture != null)
            {
                cardImage.texture = rewardTexture;
            }
        }
    }
}