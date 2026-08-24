using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class ButtonLoader : MonoBehaviour
{
    private GameObject MainButtonPrefab;
    private GameObject TabButtonPrefab;
    private GameObject FeatureButtonPrefab;
    Texture2D ItemBackground;
    Texture2D SubBackground;
    // Start is called before the first frame update
    public static ButtonLoader Instance { get; private set; }
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
        MainButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.MAIN_BUTTON_PREFAB);
        TabButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.TAB_BUTTON_PREFAB);
        FeatureButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.FEATURE_BUTTON_PREFAB);
    }
    public void CreateInventoryButton(GameObject popupButtonObject)
    {
        ItemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_INVENTORY_URL);
        SubBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Flag.FLAG_INVENTORY_URL);
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");
        // CreateButton(1, AppConstants.MainType.CAMPAIGNS, TextureHelper.LoadTexture2DCached($"UI/Background4/Background_V4_110"), TextureHelper.LoadTexture2DCached($"UI/UI/Campaign"), mainMenuCampaignPanel);
        //Main menu
        CreateButtonUI(1, AppConstants.MainType.CARD_HEROES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_HERO_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(2, AppConstants.MainType.BOOKS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.BOOK_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BOOK_URL),
            contentPanel);
        CreateButtonUI(3, AppConstants.MainType.PETS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.PET_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PET_URL),
            contentPanel);
        CreateButtonUI(4, AppConstants.MainType.CARD_CAPTAINS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_CAPTAIN_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_CAPTAIN_URL),
            contentPanel);
        CreateButtonUI(5, AppConstants.MainType.CARD_COLONELS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_COLONEL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_COLONEL_URL),
            contentPanel);
        CreateButtonUI(6, AppConstants.MainType.CARD_GENERALS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_GENERAL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_GENERAL_URL),
            contentPanel);
        CreateButtonUI(7, AppConstants.MainType.CARD_ADMIRALS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_ADMIRAL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_ADMIRAL_URL),
            contentPanel);
        CreateButtonUI(8, AppConstants.MainType.CARD_MILITARIES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_MILITARY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MILITARY_URL), 
            contentPanel);
        CreateButtonUI(9, AppConstants.MainType.CARD_SPELLS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_SPELL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SPELL_URL),
            contentPanel);
        CreateButtonUI(10, AppConstants.MainType.CARD_MONSTERS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_MONSTER_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_MONSTER_URL),
            contentPanel);
        // CreateButton(13, "equipments",backgroundImage,TextureHelper.LoadTexture2DCached($"UI/UI/equipments"), mainMenuButtonPanel);
        CreateButtonUI(11, AppConstants.MainType.BAG, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.BAG_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BAG_URL),
            contentPanel);
        CreateButtonUI(12, AppConstants.MainType.COLLABORATION_EQUIPMENTS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.COLLABORATION_EQUIPMENT_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_EQUIPMENT_URL),
            contentPanel);
        CreateButtonUI(13, AppConstants.MainType.COLLABORATIONS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.COLLABORATION_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.COLLABORATION_URL),
            contentPanel);
        CreateButtonUI(14, AppConstants.MainType.MEDALS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.MEDAL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MEDAL_URL),
            contentPanel);
        CreateButtonUI(15, AppConstants.MainType.SKILLS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SKILL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SKILL_URL),
            contentPanel);
        CreateButtonUI(16, AppConstants.MainType.SYMBOLS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SYMBOL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SYMBOL_URL),
            contentPanel);
        CreateButtonUI(17, AppConstants.MainType.TITLES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.TITLE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TITLE_URL),
            contentPanel);
        CreateButtonUI(18, AppConstants.MainType.MAGIC_FORMATION_CIRCLES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.MAGIC_FORMATION_CIRCLE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MAGIC_FORMATION_CIRCLE_URL),
            contentPanel);
        CreateButtonUI(19, AppConstants.MainType.RELICS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.RELIC_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.RELIC_URL),
            contentPanel);
        CreateButtonUI(20, AppConstants.MainType.TALISMANS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.TALISMAN_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TALISMAN_URL),
            contentPanel);
        CreateButtonUI(21, AppConstants.MainType.PUPPETS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.PUPPET_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PUPPET_URL),
            contentPanel);
        CreateButtonUI(22, AppConstants.MainType.ALCHEMIES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.ALCHEMY_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ALCHEMY_URL),
            contentPanel);
        CreateButtonUI(23, AppConstants.MainType.FORGES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.FORGE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FORGE_URL),
            contentPanel);
        CreateButtonUI(24, AppConstants.MainType.CARD_LIVES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_LIFE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_LIFE_URL),
            contentPanel);
        CreateButtonUI(25, AppConstants.MainType.ARTWORK, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.ARTWORK_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTWORK_URL),
            contentPanel);
        CreateButtonUI(26, AppConstants.MainType.SPIRIT_BEAST, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SPIRIT_BEAST_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_BEAST_URL),
            contentPanel);
        CreateButtonUI(27, AppConstants.MainType.SPIRIT_CARD, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SPIRIT_CARD_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.SPIRIT_CARD_URL),
            contentPanel);
        CreateButtonUI(28, AppConstants.MainType.ARTIFACTS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.ARTIFACT_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARTIFACT_URL),
            contentPanel);
        CreateButtonUI(29, AppConstants.MainType.ARCHITECTURES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.ARCHITECTURE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ARCHITECTURE_URL),
            contentPanel);
        CreateButtonUI(30, AppConstants.MainType.TECHONOLOGIES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.TECHNOLOGY_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.TECHNOLOGY_URL), 
            contentPanel);
        CreateButtonUI(31, AppConstants.MainType.VEHICLES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.VEHICLE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.VEHICLE_URL),
            contentPanel);
        CreateButtonUI(32, AppConstants.MainType.CORES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CORE_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CORE_URL), 
            contentPanel);
        CreateButtonUI(33, AppConstants.MainType.WEAPONS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.WEAPON_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.WEAPON_URL),
            contentPanel);
        CreateButtonUI(34, AppConstants.MainType.ROBOTS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.ROBOT_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ROBOT_URL),
            contentPanel);
        CreateButtonUI(35, AppConstants.MainType.BADGES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.BADGE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BADGE_URL),
            contentPanel);
        CreateButtonUI(36, AppConstants.MainType.MECHA_BEASTS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.MECHA_BEAST_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.MECHA_BEAST_URL),
            contentPanel);
        CreateButtonUI(37, AppConstants.MainType.RUNES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.RUNE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.RUNE_URL),
            contentPanel);
        CreateButtonUI(38, AppConstants.MainType.FURNITURES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.FURNITURE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FURNITURE_URL),
            contentPanel);
        CreateButtonUI(39, AppConstants.MainType.FOODS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.FOOD_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FOOD_URL),
            contentPanel);
        CreateButtonUI(40, AppConstants.MainType.BEVERAGES, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.BEVERAGE_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BEVERAGE_URL),
            contentPanel);
        CreateButtonUI(41, AppConstants.MainType.BUILDINGS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.BUILDING_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BUILDING_URL),
            contentPanel);
        CreateButtonUI(42, AppConstants.MainType.PLANTS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.PLANT_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.PLANT_URL),
            contentPanel);
        CreateButtonUI(43, AppConstants.MainType.FASHIONS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.FASHION_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.FASHION_URL),
            contentPanel);
        CreateButtonUI(44, AppConstants.MainType.EMOJIS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.EMOJI_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.EMOJI_URL),
            contentPanel);
        CreateButtonUI(45, AppConstants.MainType.CARD_SOLDIERS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CARD_SOLDIER_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_SOLDIER_URL),
            contentPanel);
        CreateButtonUI(46, AppConstants.MainType.OUTFITS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.OUTFIT_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.OUTFIT_URL),
            contentPanel);
        CreateButtonUI(47, AppConstants.MainType.ACHIEVEMENTS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.ACHIEVEMENT_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.ACHIEVEMENT_URL),
            contentPanel);
        CreateButtonUI(48, AppConstants.MainType.AVATARS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.BORDER_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.BORDER_URL),
            contentPanel);
        CreateButtonUI(49, AppConstants.MainType.BORDERS, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.AVATAR_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.AVATAR_URL),
            contentPanel);
        // CreateButton(1, AppConstants.MainType.EMAIL, backgroundImage, TextureHelper.LoadTexture2DCached(ImageConstants.Main.EMAIL_URL), mainMenuSubButtonGroupPanel);
    }
    public void CreateEventButton(GameObject popupButtonObject)
    {
        ItemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_INVENTORY_URL);
        SubBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Flag.FLAG_INVENTORY_URL);
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");

        CreateButtonUI(1, AppConstants.MainType.SUMMON_CARD_HERO, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_CARD_HERO_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL), 
            contentPanel);
        CreateButtonUI(2, AppConstants.MainType.SUMMON_BOOK, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_BOOK_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(3, AppConstants.MainType.SUMMON_CARD_CAPTAIN, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_CARD_CAPTAIN_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(4, AppConstants.MainType.SUMMON_CARD_MONSTER, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_CARD_MONSTER_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(5, AppConstants.MainType.SUMMON_CARD_MILITARY, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_CARD_MILITARY_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(6, AppConstants.MainType.SUMMON_CARD_SPELL, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_CARD_SPELL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(7, AppConstants.MainType.SUMMON_CARD_COLONEL, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_CARD_COLONEL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(8, AppConstants.MainType.SUMMON_CARD_GENERAL, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_CARD_GENERAL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(9, AppConstants.MainType.SUMMON_CARD_ADMIRAL, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.SUMMON_CARD_ADMIRAL_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(11, AppConstants.MainType.TOWER, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.TOWER_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(12, AppConstants.MainType.EVENT, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.EVENT_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(13, AppConstants.MainType.DAILY_CHECKIN, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.DAILY_CHECKIN_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(14, AppConstants.Market.RARE_MARKET, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Market.RARE_MARKET_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(15, AppConstants.Market.ULTRA_RARE_MARKET, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Market.ULTRA_RARE_MARKET_URL),
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL), 
            contentPanel);
        CreateButtonUI(16, AppConstants.Market.LEGENDARY_MARKET, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Market.LEGENDARY_MARKET_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(17, AppConstants.Market.MYSTIC_MARKET, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Market.MYSTIC_MARKET_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
        CreateButtonUI(18, AppConstants.MainType.CHIP, SubBackground, ItemBackground, 
            TextureHelper.LoadTexture2DCached(ImageConstants.Main.CHIP_URL), 
            TextureHelper.LoadTexture2DCached(ImageConstants.Border.CARD_HERO_URL),
            contentPanel);
    }
    private void CreateButtonUI(int index, string itemName, Texture2D _itemBackground, Texture2D _subBackground, Texture2D _itemImage, Texture2D _borderImage, Transform panel)
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

        RawImage subBackground = transform.Find("SubBackground").GetComponent<RawImage>();
        if (subBackground != null && _subBackground != null)
        {
            subBackground.texture = _subBackground;
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
    }
    public async Task<List<string>> GetUniqueTypesAsync()
    {
        // if (mainType.Equals("Equipments"))
        // {
        //     return Equipments.GetUniqueEquipmentsTypes();
        // }
        var equipment = EquipmentsService.Create();
        return await equipment.GetUniqueEquipmentsTypesAsync();
    }
    public void CreateFeatureButton(Transform featureMenuPanel)
    {
        ItemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_GALLERY_URL);
        //Gallery menu
        CreateFeatureButtonUI(1, AppDisplayConstants.Feature.BASE, TextureHelper.LoadTexture2DCached(ImageConstants.Feature.BASE_URL), TextureHelper.LoadTexture2DCached(ImageConstants.Feature.BASE_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(2, AppDisplayConstants.Feature.TRAIN, TextureHelper.LoadTexture2DCached(ImageConstants.Feature.TRAIN_URL), TextureHelper.LoadTexture2DCached(ImageConstants.Feature.TRAIN_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(3, AppDisplayConstants.Feature.RESEARCH, TextureHelper.LoadTexture2DCached(ImageConstants.Feature.RESEARCH_URL), TextureHelper.LoadTexture2DCached(ImageConstants.Feature.RESEARCH_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(4, AppDisplayConstants.Feature.EMPLOYEE, TextureHelper.LoadTexture2DCached(ImageConstants.Feature.EMPLOYEE_URL), TextureHelper.LoadTexture2DCached(ImageConstants.Feature.EMPLOYEE_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(5, AppDisplayConstants.Feature.WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Feature.WORLD_URL), TextureHelper.LoadTexture2DCached(ImageConstants.Feature.BASE_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(6, AppDisplayConstants.Feature.CITY, TextureHelper.LoadTexture2DCached(ImageConstants.Feature.CITY_URL), TextureHelper.LoadTexture2DCached(ImageConstants.Feature.CITY_ICON_URL), featureMenuPanel);

        // FindAnyObjectByType<GalleryManager>().CreateGallery(galleryMenuPanel);
        featureMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateFeatureButtonUI(int index, string itemName, Texture2D buttonBackground, Texture2D iconImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(FeatureButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = transform.Find("Background").GetComponent<RawImage>();
        if (background != null && buttonBackground != null)
        {
            background.texture = buttonBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("IconImage").GetComponent<RawImage>();
        if (image != null && iconImage != null)
        {
            image.texture = iconImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("IconName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateTowerButton(Transform towerMenuPanel)
    {
        // CreateArenaButton(1, "Tower 1",backgroundImage,TextureHelper.LoadTexture2DCached($"UI/Button/Tower_1"), towerMenuPanel);
        // CreateArenaButton(2, "Tower 2",backgroundImage,TextureHelper.LoadTexture2DCached($"UI/Button/Tower_2"), towerMenuPanel);
        // CreateArenaButton(3, "Tower 3",backgroundImage,TextureHelper.LoadTexture2DCached($"UI/Button/Tower_3"), towerMenuPanel);
        // CreateArenaButton(4, "Tower 4",backgroundImage,TextureHelper.LoadTexture2DCached($"UI/Button/Tower_4"), towerMenuPanel);
        // CreateArenaButton(5, "Tower 5",backgroundImage,TextureHelper.LoadTexture2DCached($"UI/Button/Tower_5"), towerMenuPanel);
        // CreateArenaButton(6, "Tower 6",backgroundImage,TextureHelper.LoadTexture2DCached($"UI/Button/Tower_6"), towerMenuPanel);
        // CreateArenaButton(7, "Tower 7",backgroundImage,TextureHelper.LoadTexture2DCached($"UI/Button/Tower_7"), towerMenuPanel);
    }
    public void CreateButton(int index, string itemName, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(TabButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán tên cho itemName
        TextMeshProUGUI buttonText = transform.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = itemName;
        }
    }
    public void OnButtonClicked(string buttonName, Transform tabPanel)
    {
        // Tìm button hiện tại từ RightButtonContent
        Button button = tabPanel.Find(buttonName)?.GetComponent<Button>();
        if (button == null) return;

        // Đổi background các button
        ChangeBackgroundButtonTab(button, tabPanel);
    }
    public void ChangeBackgroundButtonTab(Button clickedButton, Transform tabPanel)
    {
        foreach (Transform child in tabPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                ChangeButtonBackground(button.gameObject, ImageConstants.Button.DETAIL_TAB_BUTTON_BEFORE_CLICK_URL);
            }
        }
        // Đổi background cho button được nhấn
        if (clickedButton != null)
        {
            ChangeButtonBackground(clickedButton.gameObject, ImageConstants.Button.DETAIL_TAB_BUTTON_AFTER_CLICK_URL); // Background clicked
        }
    }
    public void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = TextureHelper.LoadTextureCached($"{image}");
            if (texture != null)
            {
                buttonImage.texture = texture;
            }
            else
            {
                Debug.LogError($"Texture '{image}' not found in Resources.");
            }
        }
        else
        {
            Debug.LogError("Button does not have a RawImage component.");
        }
    }
    
}
