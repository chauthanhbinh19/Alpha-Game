using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class ButtonLoader : MonoBehaviour
{
    private GameObject ItemButtonPrefab; // Prefab của button
    private GameObject MainButtonPrefab;
    private GameObject TabButtonPrefab;
    private GameObject AdvancedButtonPrefab;
    private GameObject AdvancedSubButtonPrefab;
    private GameObject ArenaButtonPrefab;
    private GameObject AnimeButtonPrefab;
    private GameObject ReactorButtonPrefab;
    private GameObject FeatureButtonPrefab;
    private GameObject PopupMenuPanelPrefab;
    private Transform MainPanel;
    private int set;
    Texture2D backgroundImage;
    Texture2D backgroundImage2;
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
        ItemButtonPrefab = UIManager.Instance.Get("ItemButtonPrefab");
        MainButtonPrefab = UIManager.Instance.Get("MainButtonPrefab");
        TabButtonPrefab = UIManager.Instance.Get("TabButtonPrefab");
        AdvancedButtonPrefab = UIManager.Instance.Get("AdvancedButtonPrefab");
        AdvancedSubButtonPrefab = UIManager.Instance.Get("AdvancedSubButtonPrefab");
        ArenaButtonPrefab = UIManager.Instance.Get("ArenaButtonPrefab");
        AnimeButtonPrefab = UIManager.Instance.Get("AnimeButtonPrefab");
        ReactorButtonPrefab = UIManager.Instance.Get("ReactorButtonPrefab");
        PopupMenuPanelPrefab = UIManager.Instance.Get("PopupMenuPanelPrefab");
        FeatureButtonPrefab = UIManager.Instance.Get("FeatureButtonPrefab");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");


        backgroundImage2 = Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder");


    }
    public void CreateInventoryButton(GameObject popupButtonObject)
    {
        backgroundImage = Resources.Load<Texture2D>(ImageConstants.Badge.BADGE_INVENTORY_URL);
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");
        // CreateButton(1, AppConstants.MainType.CAMPAIGNS, Resources.Load<Texture2D>($"UI/Background4/Background_V4_110"), Resources.Load<Texture2D>($"UI/UI/Campaign"), mainMenuCampaignPanel);
        //Main menu
        CreateButton(1, AppConstants.MainType.CARD_HEROES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_HERO_URL), contentPanel);
        CreateButton(2, AppConstants.MainType.BOOKS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.BOOK_URL), contentPanel);
        CreateButton(3, AppConstants.MainType.PETS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.PET_URL), contentPanel);
        CreateButton(4, AppConstants.MainType.CARD_CAPTAINS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_CAPTAIN_URL), contentPanel);
        CreateButton(5, AppConstants.MainType.CARD_COLONELS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_COLONEL_URL), contentPanel);
        CreateButton(6, AppConstants.MainType.CARD_GENERALS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_GENERAL_URL), contentPanel);
        CreateButton(7, AppConstants.MainType.CARD_ADMIRALS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_ADMIRAL_URL), contentPanel);
        CreateButton(8, AppConstants.MainType.CARD_MILITARIES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_MILITARY_URL), contentPanel);
        CreateButton(9, AppConstants.MainType.CARD_SPELLS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_SPELL_URL), contentPanel);
        CreateButton(10, AppConstants.MainType.CARD_MONSTERS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_MONSTER_URL), contentPanel);
        // CreateButton(13, "equipments",backgroundImage,Resources.Load<Texture2D>($"UI/UI/equipments"), mainMenuButtonPanel);
        CreateButton(11, AppConstants.MainType.BAG, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.BAG_URL), contentPanel);
        CreateButton(12, AppConstants.MainType.COLLABORATION_EQUIPMENTS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.COLLABORATION_EQUIPMENT_URL), contentPanel);
        CreateButton(13, AppConstants.MainType.COLLABORATIONS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.COLLABORATION_URL), contentPanel);
        CreateButton(14, AppConstants.MainType.MEDALS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.MEDAL_URL), contentPanel);
        CreateButton(15, AppConstants.MainType.SKILLS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SKILL_URL), contentPanel);
        CreateButton(16, AppConstants.MainType.SYMBOLS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SYMBOL_URL), contentPanel);
        CreateButton(17, AppConstants.MainType.TITLES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.TITLE_URL), contentPanel);
        CreateButton(18, AppConstants.MainType.MAGIC_FORMATION_CIRCLES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.MAGIC_FORMATION_CIRCLE_URL), contentPanel);
        CreateButton(19, AppConstants.MainType.RELICS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.RELIC_URL), contentPanel);
        CreateButton(20, AppConstants.MainType.TALISMANS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.TALISMAN_URL), contentPanel);
        CreateButton(21, AppConstants.MainType.PUPPETS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.PUPPET_URL), contentPanel);
        CreateButton(22, AppConstants.MainType.ALCHEMIES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.ALCHEMY_URL), contentPanel);
        CreateButton(23, AppConstants.MainType.FORGES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.FORGE_URL), contentPanel);
        CreateButton(24, AppConstants.MainType.CARD_LIVES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_LIFE_URL), contentPanel);
        CreateButton(25, AppConstants.MainType.ARTWORK, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.ARTWORK_URL), contentPanel);
        CreateButton(26, AppConstants.MainType.SPIRIT_BEAST, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SPIRIT_BEAST_URL), contentPanel);
        CreateButton(27, AppConstants.MainType.SPIRIT_CARD, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SPIRIT_CARD_URL), contentPanel);
        CreateButton(28, AppConstants.MainType.CARDS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_URL), contentPanel);
        CreateButton(29, AppConstants.MainType.ARCHITECTURES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.ARCHITECTURE_URL), contentPanel);
        CreateButton(30, AppConstants.MainType.TECHONOLOGIES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.TECHNOLOGY_URL), contentPanel);
        CreateButton(31, AppConstants.MainType.VEHICLES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.VEHICLE_URL), contentPanel);
        CreateButton(32, AppConstants.MainType.CORES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CORE_URL), contentPanel);
        CreateButton(33, AppConstants.MainType.WEAPONS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.WEAPON_URL), contentPanel);
        CreateButton(34, AppConstants.MainType.ROBOTS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.ROBOT_URL), contentPanel);
        CreateButton(35, AppConstants.MainType.BADGES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.BADGE_URL), contentPanel);
        CreateButton(36, AppConstants.MainType.MECHA_BEASTS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.MECHA_BEAST_URL), contentPanel);
        CreateButton(37, AppConstants.MainType.RUNES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.RUNE_URL), contentPanel);
        CreateButton(38, AppConstants.MainType.FURNITURES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.FURNITURE_URL), contentPanel);
        CreateButton(39, AppConstants.MainType.FOODS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.FOOD_URL), contentPanel);
        CreateButton(40, AppConstants.MainType.BEVERAGES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.BEVERAGE_URL), contentPanel);
        CreateButton(41, AppConstants.MainType.BUILDINGS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.BUILDING_URL), contentPanel);
        CreateButton(42, AppConstants.MainType.PLANTS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.PLANT_URL), contentPanel);
        // CreateButton(1, AppConstants.MainType.EMAIL, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.EMAIL_URL), mainMenuSubButtonGroupPanel);
        // CreateButton(1, AppConstants.MainType.EMAIL, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.EMAIL_URL), mainMenuSubButtonGroupPanel);
    }
    public void CreateEventButton(GameObject popupButtonObject)
    {
        backgroundImage = Resources.Load<Texture2D>(ImageConstants.Badge.BADGE_INVENTORY_URL);
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");

        CreateButton(1, AppConstants.MainType.SUMMON_CARD_HEROES, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SUMMON_CARD_HERO_URL), contentPanel);
        CreateButton(2, AppConstants.MainType.SUMMON_BOOKS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SUMMON_BOOK_URL), contentPanel);
        CreateButton(3, AppConstants.MainType.SUMMON_CARD_CAPTAINS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_CAPTAIN_URL), contentPanel);
        CreateButton(4, AppConstants.MainType.SUMMON_CARD_MONSTERS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_MONSTER_URL), contentPanel);
        CreateButton(5, AppConstants.MainType.SUMMON_CARD_MILITARY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.CARD_MILITARY_URL), contentPanel);
        CreateButton(6, AppConstants.MainType.SUMMON_CARD_SPELLS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SUMMON_CARD_SPELL_URL), contentPanel);
        CreateButton(7, AppConstants.MainType.SUMMON_CARD_COLONELS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SUMMON_CARD_COLONEL_URL), contentPanel);
        CreateButton(8, AppConstants.MainType.SUMMON_CARD_GENERALS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SUMMON_CARD_GENERAL_URL), contentPanel);
        CreateButton(9, AppConstants.MainType.SUMMON_CARD_ADMIRALS, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.SUMMON_CARD_ADMIRAL_URL), contentPanel);
        CreateButton(10, AppConstants.MainType.ANIME, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.ANIME_URL), contentPanel);
        CreateButton(11, AppConstants.MainType.TOWER, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.TOWER_URL), contentPanel);
        CreateButton(12, AppConstants.MainType.EVENT, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.EVENT_URL), contentPanel);
        CreateButton(13, AppConstants.MainType.DAILY_CHECKIN, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Main.DAILY_CHECKIN_URL), contentPanel);
        CreateButton(14, AppConstants.Market.RARE_MARKET, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Market.RARE_MARKET_URL), contentPanel);
        CreateButton(15, AppConstants.Market.ULTRA_RARE_MARKET, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Market.ULTRA_RARE_MARKET_URL), contentPanel);
        CreateButton(16, AppConstants.Market.LEGENDARY_MARKET, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Market.LEGENDARY_MARKET_URL), contentPanel);
        CreateButton(17, AppConstants.Market.MYSTIC_MARKET, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Market.MYSTIC_MARKET_URL), contentPanel);
    }
    public void CreateMoreButton(Transform moreMenuPanel)
    {
        moreMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateButton(int index, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(MainButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    private void CreateButtonWithName(string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ItemButtonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName.Replace("_", ""));
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
    private void CreateArenaButtonUI(string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ArenaButtonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        // RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        // if (background != null && itemBackground != null)
        // {
        //     background.texture = itemBackground;
        // }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = itemName;
        }
    }
    private void CreateAnimeButtonUI(string itemDisplayName, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(AnimeButtonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        // RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        // if (background != null && itemBackground != null)
        // {
        //     background.texture = itemBackground;
        // }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemDisplayName);
        }

        //Tạo animation cho border image
        RawImage borderImage = newButton.transform.Find("BorderImage").GetComponent<RawImage>();
        // Gán script RotateUI
        borderImage.gameObject.AddComponent<RotateAnimation>();
    }
    private void CreateScienceFictionButtonUI(string itemName, int number, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ReactorButtonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        // RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        // if (background != null && itemBackground != null)
        // {
        //     background.texture = itemBackground;
        // }

        RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        TextMeshProUGUI nameText = newButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        TextMeshProUGUI numberText = newButton.transform.Find("NumberText").GetComponent<TextMeshProUGUI>();
        if (numberText != null)
        {
            numberText.text = number.ToString("D2");
        }

        // RawImage borderImage = newButton.transform.Find("BorderImage").GetComponent<RawImage>();
    }
    public void CreateFeatureButton(Transform featureMenuPanel)
    {
        backgroundImage = Resources.Load<Texture2D>(ImageConstants.Badge.BADGE_GALLERY_URL);
        //Gallery menu
        CreateFeatureButtonUI(1, AppDisplayConstants.Feature.BASE, Resources.Load<Texture2D>(ImageConstants.Feature.BASE_URL), Resources.Load<Texture2D>(ImageConstants.Feature.BASE_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(2, AppDisplayConstants.Feature.TRAIN, Resources.Load<Texture2D>(ImageConstants.Feature.TRAIN_URL), Resources.Load<Texture2D>(ImageConstants.Feature.TRAIN_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(3, AppDisplayConstants.Feature.RESEARCH, Resources.Load<Texture2D>(ImageConstants.Feature.RESEARCH_URL), Resources.Load<Texture2D>(ImageConstants.Feature.RESEARCH_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(4, AppDisplayConstants.Feature.EMPLOYEE, Resources.Load<Texture2D>(ImageConstants.Feature.EMPLOYEE_URL), Resources.Load<Texture2D>(ImageConstants.Feature.EMPLOYEE_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(5, AppDisplayConstants.Feature.WORLD, Resources.Load<Texture2D>(ImageConstants.Feature.WORLD_URL), Resources.Load<Texture2D>(ImageConstants.Feature.BASE_ICON_URL), featureMenuPanel);
        CreateFeatureButtonUI(6, AppDisplayConstants.Feature.CITY, Resources.Load<Texture2D>(ImageConstants.Feature.CITY_URL), Resources.Load<Texture2D>(ImageConstants.Feature.CITY_ICON_URL), featureMenuPanel);

        // FindAnyObjectByType<GalleryManager>().CreateGallery(galleryMenuPanel);
        featureMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateFeatureButtonUI(int index, string itemName, Texture2D buttonBackground, Texture2D iconImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(FeatureButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("Background").GetComponent<RawImage>();
        if (background != null && buttonBackground != null)
        {
            background.texture = buttonBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("IconImage").GetComponent<RawImage>();
        if (image != null && iconImage != null)
        {
            image.texture = iconImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("IconName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateGalleryButton(Transform galleryMenuPanel)
    {
        backgroundImage = Resources.Load<Texture2D>(ImageConstants.Badge.BADGE_GALLERY_URL);
        //Gallery menu
        CreateGalleryButtonUI(1, AppDisplayConstants.Gallery.CARD_HEROES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_HERO_URL), galleryMenuPanel);
        CreateGalleryButtonUI(2, AppDisplayConstants.Gallery.BOOKS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.BOOK_URL), galleryMenuPanel);
        CreateGalleryButtonUI(3, AppDisplayConstants.Gallery.PETS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.PET_URL), galleryMenuPanel);
        CreateGalleryButtonUI(4, AppDisplayConstants.Gallery.CARD_CAPTAINS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_CAPTAIN_URL), galleryMenuPanel);
        CreateGalleryButtonUI(5, AppDisplayConstants.Gallery.COLLABORATION_EQUIPMENTS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(6, AppDisplayConstants.Gallery.CARD_MILITARIES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_MILITARY_URL), galleryMenuPanel);
        CreateGalleryButtonUI(7, AppDisplayConstants.Gallery.CARD_SPELL_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_SPELL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(8, AppDisplayConstants.Gallery.COLLABORATIONS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.COLLABORATION_URL), galleryMenuPanel);
        CreateGalleryButtonUI(9, AppDisplayConstants.Gallery.CARD_MONSTERS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_MONSTER_URL), galleryMenuPanel);
        CreateGalleryButtonUI(10, AppDisplayConstants.Gallery.EQUIPMENTS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.EQUIPMENT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(11, AppDisplayConstants.Gallery.MEDALS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.MEDAL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(12, AppDisplayConstants.Gallery.SKILLS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.SKILL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(13, AppDisplayConstants.Gallery.SYMBOLS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.SYMBOL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(14, AppDisplayConstants.Gallery.TITLES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.TITLE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(15, AppDisplayConstants.Gallery.MAGIC_FORMATION_CRICLES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(16, AppDisplayConstants.Gallery.RELICS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.RELIC_URL), galleryMenuPanel);
        CreateGalleryButtonUI(17, AppDisplayConstants.Gallery.CARD_COLONELS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_COLONEL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(18, AppDisplayConstants.Gallery.CARD_GENERALS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_GENERAL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(19, AppDisplayConstants.Gallery.CARD_ADMIRALS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_ADMIRAL_URL), galleryMenuPanel);
        CreateGalleryButtonUI(20, AppDisplayConstants.Gallery.BORDERS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.BORDER_URL), galleryMenuPanel);
        CreateGalleryButtonUI(21, AppDisplayConstants.Gallery.TALISMANS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.TALISMAN_URL), galleryMenuPanel);
        CreateGalleryButtonUI(22, AppDisplayConstants.Gallery.PUPPETS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.PUPPET_URL), galleryMenuPanel);
        CreateGalleryButtonUI(23, AppDisplayConstants.Gallery.ALCHEMIES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.ALCHEMY_URL), galleryMenuPanel);
        CreateGalleryButtonUI(24, AppDisplayConstants.Gallery.FORGES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.FORGE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(25, AppDisplayConstants.Gallery.CARD_LIVES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.LIFE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(26, AppDisplayConstants.Gallery.ARTWORKS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.ARTWORK_URL), galleryMenuPanel);
        CreateGalleryButtonUI(27, AppDisplayConstants.Gallery.SPIRIT_BEASTS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.SPIRIT_BEAST_URL), galleryMenuPanel);
        CreateGalleryButtonUI(28, AppDisplayConstants.Gallery.AVATARS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.AVATAR_URL), galleryMenuPanel);
        CreateGalleryButtonUI(29, AppDisplayConstants.Gallery.SPIRIT_CARDS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.SPIRIT_CARD_URL), galleryMenuPanel);
        CreateGalleryButtonUI(30, AppDisplayConstants.Gallery.ACHIEVEMENTS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.ACHIEVEMENT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(31, AppDisplayConstants.Gallery.CARDS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_URL), galleryMenuPanel);
        CreateGalleryButtonUI(32, AppDisplayConstants.Gallery.ARCHITECTURES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.ARCHITECTURE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(33, AppDisplayConstants.Gallery.TECHNOLOGIES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.TECHNOLOGY_URL), galleryMenuPanel);
        CreateGalleryButtonUI(34, AppDisplayConstants.Gallery.VEHICLES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.VEHICLE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(35, AppDisplayConstants.Gallery.CORES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.CORE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(36, AppDisplayConstants.Gallery.WEAPONS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.WEAPON_URL), galleryMenuPanel);
        CreateGalleryButtonUI(37, AppDisplayConstants.Gallery.ROBOTS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.ROBOT_URL), galleryMenuPanel);
        CreateGalleryButtonUI(38, AppDisplayConstants.Gallery.BADGES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.BADGE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(39, AppDisplayConstants.Gallery.MECHA_BEASTS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.MECHA_BEAST_URL), galleryMenuPanel);
        CreateGalleryButtonUI(40, AppDisplayConstants.Gallery.RUNES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.RUNE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(41, AppDisplayConstants.Gallery.FURNITURES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.BADGE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(42, AppDisplayConstants.Gallery.FOODS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.MECHA_BEAST_URL), galleryMenuPanel);
        CreateGalleryButtonUI(43, AppDisplayConstants.Gallery.BEVERAGES_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.RUNE_URL), galleryMenuPanel);
        CreateGalleryButtonUI(44, AppDisplayConstants.Gallery.BUILDINGS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.BUILDING_URL), galleryMenuPanel);
        CreateGalleryButtonUI(45, AppDisplayConstants.Gallery.PLANTS_GALLERY, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Gallery.PLANT_URL), galleryMenuPanel);

        FindAnyObjectByType<GalleryManager>().CreateGallery(galleryMenuPanel);
        galleryMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateGalleryButtonUI(int index, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ItemButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateCollectionButton(Transform collectionMenuPanel)
    {
        backgroundImage = Resources.Load<Texture2D>(ImageConstants.Badge.BADGE_COLLECTION_URL);
        //Collection menu
        CreateCollectionButtonUI(1, AppDisplayConstants.Collection.CARD_HEROES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_HERO_URL), collectionMenuPanel);
        CreateCollectionButtonUI(2, AppDisplayConstants.Collection.BOOKS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.BOOK_URL), collectionMenuPanel);
        CreateCollectionButtonUI(3, AppDisplayConstants.Collection.PETS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.PET_URL), collectionMenuPanel);
        CreateCollectionButtonUI(4, AppDisplayConstants.Collection.CARD_CAPTAINS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_CAPTAIN_URL), collectionMenuPanel);
        CreateCollectionButtonUI(5, AppDisplayConstants.Collection.COLLABORATION_EQUIPMENTS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.COLLABORATION_EQUIPMENT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(6, AppDisplayConstants.Collection.CARD_MILITARIES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_MILITARY_URL), collectionMenuPanel);
        CreateCollectionButtonUI(7, AppDisplayConstants.Collection.CARD_SPELLS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_SPELL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(8, AppDisplayConstants.Collection.COLLABORATIONS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.COLLABORATION_URL), collectionMenuPanel);
        CreateCollectionButtonUI(9, AppDisplayConstants.Collection.CARD_MONSTERS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_MONSTER_URL), collectionMenuPanel);
        CreateCollectionButtonUI(10, AppDisplayConstants.Collection.EQUIPMENTS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.EQUIPMENT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(11, AppDisplayConstants.Collection.MEDALS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.MEDAL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(12, AppDisplayConstants.Collection.SKILLS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.SKILL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(13, AppDisplayConstants.Collection.SYMBOLS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.SYMBOL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(14, AppDisplayConstants.Collection.TITLES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.TITLE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(15, AppDisplayConstants.Collection.MAGIC_FORMATION_CIRCLES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.MAGIC_FORMATION_CIRCLE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(16, AppDisplayConstants.Collection.RELICS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.RELIC_URL), collectionMenuPanel);
        CreateCollectionButtonUI(17, AppDisplayConstants.Collection.CARD_COLONELS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_COLONEL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(18, AppDisplayConstants.Collection.CARD_GENERALS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_GENERAL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(19, AppDisplayConstants.Collection.CARD_ADMIRALS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_ADMIRAL_URL), collectionMenuPanel);
        CreateCollectionButtonUI(20, AppDisplayConstants.Collection.BORDERS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.BORDER_URL), collectionMenuPanel);
        CreateCollectionButtonUI(21, AppDisplayConstants.Collection.TALISMANS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.TALISMAN_URL), collectionMenuPanel);
        CreateCollectionButtonUI(22, AppDisplayConstants.Collection.PUPPETS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.PUPPET_URL), collectionMenuPanel);
        CreateCollectionButtonUI(23, AppDisplayConstants.Collection.ALCHEMIES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.ALCHEMY_URL), collectionMenuPanel);
        CreateCollectionButtonUI(24, AppDisplayConstants.Collection.FORGES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.FORGE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(25, AppDisplayConstants.Collection.CARD_LIVES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.LIFE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(26, AppDisplayConstants.Collection.ARTWORKS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.ARTWORK_URL), collectionMenuPanel);
        CreateCollectionButtonUI(27, AppDisplayConstants.Collection.SPIRIT_BEASTS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.SPIRIT_BEAST_URL), collectionMenuPanel);
        CreateCollectionButtonUI(28, AppDisplayConstants.Collection.AVATARS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.AVATAR_URL), collectionMenuPanel);
        CreateCollectionButtonUI(29, AppDisplayConstants.Collection.SPIRIT_CARDS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.SPIRIT_CARD_URL), collectionMenuPanel);
        CreateCollectionButtonUI(30, AppDisplayConstants.Collection.ACHIEVEMENTS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.ACHIEVEMENT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(31, AppDisplayConstants.Collection.CARDS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CARD_URL), collectionMenuPanel);
        CreateCollectionButtonUI(32, AppDisplayConstants.Collection.ARCHITECTURES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.ARCHITECTURE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(33, AppDisplayConstants.Collection.TECHNOLOGIES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.TECHNOLOGY_URL), collectionMenuPanel);
        CreateCollectionButtonUI(34, AppDisplayConstants.Collection.VEHICLES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.VEHICLE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(35, AppDisplayConstants.Collection.CORES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.CORE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(36, AppDisplayConstants.Collection.WEAPOMS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.WEAPON_URL), collectionMenuPanel);
        CreateCollectionButtonUI(37, AppDisplayConstants.Collection.ROBOTS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.ROBOT_URL), collectionMenuPanel);
        CreateCollectionButtonUI(38, AppDisplayConstants.Collection.BADGES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.BADGE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(39, AppDisplayConstants.Collection.MECHA_BEASTS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.MECHA_BEAST_URL), collectionMenuPanel);
        CreateCollectionButtonUI(40, AppDisplayConstants.Collection.RUNES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.RUNE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(41, AppDisplayConstants.Collection.FURNITURES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.FURNITURE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(42, AppDisplayConstants.Collection.FOODS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.FOOD_URL), collectionMenuPanel);
        CreateCollectionButtonUI(43, AppDisplayConstants.Collection.BEVERAGES_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.BEVERAGE_URL), collectionMenuPanel);
        CreateCollectionButtonUI(44, AppDisplayConstants.Collection.BUILDINGS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.BUILDING_URL), collectionMenuPanel);
        CreateCollectionButtonUI(45, AppDisplayConstants.Collection.PLANTS_COLLECTION, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Collection.PLANT_URL), collectionMenuPanel);

        FindAnyObjectByType<CollectionManager>().CreateCollection(collectionMenuPanel);
        collectionMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateCollectionButtonUI(int index, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ItemButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public async Task CreateEquipmentsButtonAsync(Transform equipmentMenuPanel)
    {
        backgroundImage = Resources.Load<Texture2D>(ImageConstants.Badge.BADGE_EQUIPMENT_URL);
        //Equipment menu
        List<string> uniqueTypes = await GetUniqueTypesAsync();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                string subtype = uniqueTypes[i];
                CreateButtonWithName(subtype, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Equipments/{subtype}"), equipmentMenuPanel);
            }
        }
        FindAnyObjectByType<EquipmentManager>().CreateEquipments(equipmentMenuPanel);
        equipmentMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateAnimeButton(Transform animeMenuPanel)
    {
        CreateAnimeButtonUI(AppDisplayConstants.Anime.ONE_PIECE, AppConstants.Anime.ONE_PIECE, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.ONE_PIECE_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.NARUTO, AppConstants.Anime.NARUTO, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.NARUTO_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.DRAGON_BALL, AppConstants.Anime.DRAGON_BALL, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.DRAGON_BALL_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.FAIRY_TAIL, AppConstants.Anime.FAIRY_TAIL, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.FAIRY_TAIL_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.SWORD_ART_ONLINE, AppConstants.Anime.SWORD_ART_ONLINE, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.SWORD_ART_ONLINE_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.DEMON_SLAYER, AppConstants.Anime.DEMON_SLAYER, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.DEMON_SLAYER_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.BLEACH, AppConstants.Anime.BLEACH, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.BLEACH_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.JUJUTSU_KAISEN, AppConstants.Anime.JUJUTSU_KAISEN, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.JUJUTSU_KAISEN_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.BLACK_CLOVER, AppConstants.Anime.BLACK_CLOVER, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.BLACK_CLOVER_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.HUNTER_X_HUNTER, AppConstants.Anime.HUNTER_X_HUNTER, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.HUNTER_X_HUNTER_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.ONE_PUNCH_MAN, AppConstants.Anime.ONE_PUNCH_MAN, backgroundImage, Resources.Load<Texture2D>(ImageConstants.Anime.ONE_PUNCH_MAN_URL), animeMenuPanel);

        FindAnyObjectByType<MainMenuAnimeStatsManager>().CreateAnimeButton(animeMenuPanel);
        animeMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateScienceFictionButton(Transform reactorMenuPanel)
    {
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_1, 1, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_1_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_2, 2, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_2_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_3, 3, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_3_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_4, 4, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_4_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_5, 5, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_5_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_6, 6, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_6_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_7, 7, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_7_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_8, 8, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_8_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_9, 9, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_9_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_10, 10, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_10_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_11, 11, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_11_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_12, 12, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_12_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_13, 13, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_13_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_14, 14, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_14_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_15, 15, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_15_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_16, 16, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_16_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_17, 17, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_17_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_18, 18, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_18_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_19, 19, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_19_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_20, 20, backgroundImage, Resources.Load<Texture2D>(ImageConstants.ScienceFiction.REACTOR_NUMBER_20_URL), reactorMenuPanel);

        FindAnyObjectByType<MainMenuAnimeStatsManager>().CreateAnimeButton(reactorMenuPanel);
        reactorMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public async Task CreateArenaButtonAsync(Transform arenaMenuPanel)
    {

        var uniqueMode = await ArenaService.Create().GetUniqueTypesAsync();
        foreach (var type in uniqueMode)
        {
            CreateArenaButtonUI(type, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Arena/{type}"), arenaMenuPanel);
        }
        FindAnyObjectByType<ArenaManager>().CreateArenaButton(arenaMenuPanel);
        arenaMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateTowerButton(Transform towerMenuPanel)
    {
        // CreateArenaButton(1, "Tower 1",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_1"), towerMenuPanel);
        // CreateArenaButton(2, "Tower 2",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_2"), towerMenuPanel);
        // CreateArenaButton(3, "Tower 3",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_3"), towerMenuPanel);
        // CreateArenaButton(4, "Tower 4",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_4"), towerMenuPanel);
        // CreateArenaButton(5, "Tower 5",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_5"), towerMenuPanel);
        // CreateArenaButton(6, "Tower 6",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_6"), towerMenuPanel);
        // CreateArenaButton(7, "Tower 7",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_7"), towerMenuPanel);
    }
    public void CreateButton(int index, string itemName, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(TabButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán tên cho itemName
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
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
            Texture texture = Resources.Load<Texture>($"{image}");
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
    public void CreateSetButtonGroup(object data, Transform buttonPanel)
    {
        if (data is CardHeroes cardHero || data is CardCaptains cardCaptain ||
        data is CardColonels cardColonel || data is CardGenerals cardGeneral ||
        data is CardAdmirals cardAdmiral || data is CardMonsters cardMonster ||
        data is CardMilitaries cardMilitary || data is CardSpells cardSpell ||
        data is Books book || data is Pets pet || data is Equipments equipment
        )
        {
            int setButtonNumber = 8;
            for (int i = 0; i < setButtonNumber; i++)
            {
                int index = i;
                GameObject button = Instantiate(AdvancedButtonPrefab, buttonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = (index + 1).ToString();

                Button btn = button.GetComponent<Button>();

                if (index == 0)
                {
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 1)
                {
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 2)
                {
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 3)
                {
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 4)
                {
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 5)
                {
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 6)
                {
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 7)
                {
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        CreateButtonGroup(data);
                    });
                }
                // else if (index == 8)
                // {
                //     btn.onClick.AddListener(() =>
                //     {
                //         set = index + 1;
                //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                //         CreateButtonGroup(data);
                //     });
                // }
            }
        }
    }
    public void CreateButtonGroup(object data)
    {
        GameObject popUpPanelGameObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
        Transform content = popUpPanelGameObject.transform.Find("Scroll View/Viewport/Content");
        content.gameObject.SetActive(true);
        GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(280, 450);
        // content.position = new Vector3(transform.position.x, 200f, transform.position.z);

        TextMeshProUGUI titleText = popUpPanelGameObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        titleText.text = "Set " + set.ToString();

        Button CloseButton = popUpPanelGameObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popUpPanelGameObject);
        });

        if (data is CardHeroes cardHero)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardCaptains cardCaptain)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardColonels cardColonel)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardGenerals cardGeneral)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardMonsters cardMonster)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardSpells cardSpell)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is Books book)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is Pets pet)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is Equipments equipment)
        {
            CreateButtonGroupDetails(data, content);
        }
    }
    private void CreateButtonWithBackground(int index, string itemName, string itemBackground, Texture2D itemImage, Transform panel)
    {
        if (panel == null)
        {
            Debug.Log("Panel is null for index: " + index);
            return;
        }
        // Tạo button từ prefab
        GameObject newButton = Instantiate(AdvancedSubButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("Background").GetComponent<RawImage>();
        Texture texture = Resources.Load<Texture>($"{itemBackground}");
        if (background != null && itemBackground != null)
        {
            background.texture = texture;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("MainImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateButtonGroupDetails(object data, Transform content)
    {
        if (data is CardHeroes cardHero || data is CardCaptains cardCaptain ||
        data is CardColonels cardColonel || data is CardGenerals cardGeneral ||
        data is CardAdmirals cardAdmiral || data is CardMonsters cardMonster ||
        data is CardMilitaries cardMilitary || data is CardSpells cardSpell ||
        data is Books book || data is Pets pet
        )
        {
            if (set == 1)
            {
                CreateButtonSet1(data, content);
            }
            else if (set == 2)
            {
                CreateButtonSet2(data, content);
            }
            else if (set == 3)
            {
                CreateButtonSet3(data, content);
            }
            else if (set == 4)
            {
                CreateButtonSet4(data, content);
            }
            else if (set == 5)
            {
                CreateButtonSet5(data, content);
            }
            else if (set == 6)
            {
                CreateButtonSet6(data, content);
            }
            else if (set == 7)
            {
                CreateButtonSet7(data, content);
            }
            else if (set == 8)
            {
                CreateButtonSet8(data, content);
            }
        }
        else if (data is Equipments equipments)
        {
            // CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet1.Equipments, ImageConstants.Background.AdvancedBackground1, Resources.Load<Texture2D>($"UI/Button/Main/Equipments"), content);
            CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet1.REALM, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/Realm"), content);
            CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet1.UPGRADE, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/Upgrade"), content);
            CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet1.APTITUDE, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/Aptitude"), content);
            // CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet1.Affinity, ImageConstants.Background.AdvancedBackground5, Resources.Load<Texture2D>($"UI/Button/Main/Affinity"), content);
            CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet1.BLESSING, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/Blessing"), content);
            CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet1.CORE, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/Core"), content);
            CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet1.PHYSIQUE, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/Physique"), content);
            CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet1.BLOODLINE, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/Bloodline"), content);

            CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet1.OMNIVISION, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnivision"), content);
            CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet1.OMNIPOTENCE, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnipotence"), content);
            CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet1.OMNIPRESENCE, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnipresence"), content);
            CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet1.OMNISCIENCE, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omniscience"), content);
            CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet1.OMNIVORY, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnivory"), content);
            CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet1.ANGEL, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, Resources.Load<Texture2D>($"UI/Button/Main/Angel"), content);
            CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet1.DEMON, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, Resources.Load<Texture2D>($"UI/Button/Main/Demon"), content);

            CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet1.SWORD, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, Resources.Load<Texture2D>($"UI/Button/Main/Sword"), content);
            CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet1.SPEAR, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, Resources.Load<Texture2D>($"UI/Button/Main/Spear"), content);
            CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet1.SHIELD, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, Resources.Load<Texture2D>($"UI/Button/Main/Shield"), content);
            CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet1.BOW, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, Resources.Load<Texture2D>($"UI/Button/Main/Bow"), content);
            CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet1.GUN, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, Resources.Load<Texture2D>($"UI/Button/Main/Gun"), content);
            CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet1.CYBER, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, Resources.Load<Texture2D>($"UI/Button/Main/Cyber"), content);
            CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet1.FAIRY, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, Resources.Load<Texture2D>($"UI/Button/Main/Fairy"), content);

            // ButtonEvent.Instance.AssignButtonEvent("Button_1", content, () =>
            // {
            //     FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManager(data);
            // });
            ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
            {
                await FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
            {
                await FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
            {
                await FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManagerAsync(data);
            });
            // ButtonEvent.Instance.AssignButtonEvent("Button_5", content, () =>
            // {
            //     FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(data);
            // });
            ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
            {
                await FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
            {
                await FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
            {
                await FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
            {
                await FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
            {
                await FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
            {
                await FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_12", content, async () =>
            {
                await FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_13", content, async () =>
            {
                await FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_14", content, async () =>
            {
                await FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_15", content, async () =>
            {
                await FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_16", content, async () =>
            {
                await FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_17", content, async () =>
            {
                await FindAnyObjectByType<MainMenuSwordManager>().CreateMainMenuSwordManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_18", content, async () =>
            {
                await FindAnyObjectByType<MainMenuSpearManager>().CreateMainMenuSpearManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_19", content, async () =>
            {
                await FindAnyObjectByType<MainMenuShieldManager>().CreateMainMenuShieldManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_20", content, async () =>
            {
                await FindAnyObjectByType<MainMenuBowManager>().CreateMainMenuBowManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_21", content, async () =>
            {
                await FindAnyObjectByType<MainMenuGunManager>().CreateMainMenuGunManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_22", content, async () =>
            {
                await FindAnyObjectByType<MainMenuCyberManager>().CreateMainMenuCyberManagerAsync(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_23", content, async () =>
            {
                await FindAnyObjectByType<MainMenuFairyManager>().CreateMainMenuFairyManagerAsync(data);
            });
        }
        content.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateButtonSet1(object data, Transform content)
    {
        CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet1.EQUIPMENTS, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, Resources.Load<Texture2D>($"UI/Button/Main/Equipments"), content);
        CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet1.REALM, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/Realm"), content);
        CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet1.UPGRADE, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/Upgrade"), content);
        CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet1.APTITUDE, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/Aptitude"), content);
        CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet1.AFFINITY, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, Resources.Load<Texture2D>($"UI/Button/Main/Affinity"), content);
        CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet1.BLESSING, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/Blessing"), content);
        CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet1.CORE, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/Core"), content);
        CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet1.PHYSIQUE, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/Physique"), content);
        CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet1.BLOODLINE, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/Bloodline"), content);

        CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet1.OMNIVISION, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnivision"), content);
        CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet1.OMNIPOTENCE, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnipotence"), content);
        CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet1.OMNIPRESENCE, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnipresence"), content);
        CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet1.OMNISCIENCE, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omniscience"), content);
        CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet1.OMNIVORY, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnivory"), content);
        CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet1.ANGEL, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, Resources.Load<Texture2D>($"UI/Button/Main/Angel"), content);
        CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet1.DEMON, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, Resources.Load<Texture2D>($"UI/Button/Main/Demon"), content);

        CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet1.SWORD, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, Resources.Load<Texture2D>($"UI/Button/Main/Sword"), content);
        CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet1.SPEAR, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, Resources.Load<Texture2D>($"UI/Button/Main/Spear"), content);
        CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet1.SHIELD, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, Resources.Load<Texture2D>($"UI/Button/Main/Shield"), content);
        CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet1.BOW, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, Resources.Load<Texture2D>($"UI/Button/Main/Bow"), content);
        CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet1.GUN, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, Resources.Load<Texture2D>($"UI/Button/Main/Gun"), content);
        CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet1.CYBER, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, Resources.Load<Texture2D>($"UI/Button/Main/Cyber"), content);
        CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet1.FAIRY, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, Resources.Load<Texture2D>($"UI/Button/Main/Fairy"), content);


        ButtonEvent.Instance.AssignButtonEvent("Button_1", content, async () =>
        {
            await FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
        {
            await FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
        {
            await FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", content, () =>
        {
            FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
        {
            await FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
        {
            await FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
        {
            await FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
        {
            await FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", content, async () =>
        {
            await FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", content, async () =>
        {
            await FindAnyObjectByType<MainMenuSwordManager>().CreateMainMenuSwordManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", content, async () =>
        {
            await FindAnyObjectByType<MainMenuSpearManager>().CreateMainMenuSpearManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", content, async () =>
        {
            await FindAnyObjectByType<MainMenuShieldManager>().CreateMainMenuShieldManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", content, async () =>
        {
            await FindAnyObjectByType<MainMenuBowManager>().CreateMainMenuBowManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", content, async () =>
        {
            await FindAnyObjectByType<MainMenuGunManager>().CreateMainMenuGunManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", content, async () =>
        {
            await FindAnyObjectByType<MainMenuCyberManager>().CreateMainMenuCyberManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", content, async () =>
        {
            await FindAnyObjectByType<MainMenuFairyManager>().CreateMainMenuFairyManagerAsync(data);
        });
    }
    public void CreateButtonSet2(object data, Transform content)
    {
        CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet2.DARK, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, Resources.Load<Texture2D>($"UI/Button/Main/Dark"), content);
        CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet2.LIGHT, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/Light"), content);
        CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet2.FIRE, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/Fire"), content);
        CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet2.ICE, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/Ice"), content);
        CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet2.EARTH, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, Resources.Load<Texture2D>($"UI/Button/Main/Earth"), content);
        CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet2.THUNDER, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/Thunder"), content);
        CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet2.LIFE, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/Life"), content);
        CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet2.SPACE, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/Space"), content);
        CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet2.TIME, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/Time"), content);

        CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet2.NANOTECH, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nanotech"), content);
        CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet2.QUANTUM, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/Quantum"), content);
        CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet2.HOLOGRAPHY, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, Resources.Load<Texture2D>($"UI/Button/Main/Holography"), content);
        CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet2.PLASMAN, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, Resources.Load<Texture2D>($"UI/Button/Main/Plasma"), content);
        CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet2.BIOMECH, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, Resources.Load<Texture2D>($"UI/Button/Main/Biomech"), content);
        CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet2.CRYOTECH, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, Resources.Load<Texture2D>($"UI/Button/Main/Cryotech"), content);
        CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet2.PSIONICS, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, Resources.Load<Texture2D>($"UI/Button/Main/Psionics"), content);

        CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet2.NEUROTECH, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, Resources.Load<Texture2D>($"UI/Button/Main/Neurotech"), content);
        CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet2.ANIMATTER, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, Resources.Load<Texture2D>($"UI/Button/Main/Antimatter"), content);
        CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet2.PHANTOMWARE, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, Resources.Load<Texture2D>($"UI/Button/Main/Phantomware"), content);
        CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet2.GRAVITECH, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, Resources.Load<Texture2D>($"UI/Button/Main/Gravitech"), content);
        CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet2.AETHERNET, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, Resources.Load<Texture2D>($"UI/Button/Main/Aethernet"), content);
        CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet2.STARFORGE, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, Resources.Load<Texture2D>($"UI/Button/Main/Starforge"), content);
        CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet2.ORBITALIS, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, Resources.Load<Texture2D>($"UI/Button/Main/Orbitalis"), content);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", content, async () =>
        {
            await FindAnyObjectByType<MainMenuDarkManager>().CreateMainMenuDarkManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
        {
            await FindAnyObjectByType<MainMenuLightManager>().CreateMainMenuLightManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
        {
            await FindAnyObjectByType<MainMenuFireManager>().CreateMainMenuFireManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
        {
            await FindAnyObjectByType<MainMenuIceManager>().CreateMainMenuIceManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", content, async () =>
        {
            await FindAnyObjectByType<MainMenuEarthManager>().CreateMainMenuEarthManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
        {
            await FindAnyObjectByType<MainMenuThunderManager>().CreateMainMenuThunderManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
        {
            await FindAnyObjectByType<MainMenuLifeManager>().CreateMainMenuLifeManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
        {
            await FindAnyObjectByType<MainMenuSpaceManager>().CreateMainMenuSpaceManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
        {
            await FindAnyObjectByType<MainMenuTimeManager>().CreateMainMenuTimeManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNanotechManager>().CreateMainMenuNanotechManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
        {
            await FindAnyObjectByType<MainMenuQuantumManager>().CreateMainMenuQuantumManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", content, async () =>
        {
            await FindAnyObjectByType<MainMenuHolographyManager>().CreateMainMenuHolographyManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", content, async () =>
        {
            await FindAnyObjectByType<MainMenuPlasmaManager>().CreateMainMenuPlasmaManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", content, async () =>
        {
            await FindAnyObjectByType<MainMenuBiomechManager>().CreateMainMenuBiomechManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", content, async () =>
        {
            await FindAnyObjectByType<MainMenuCryotechManager>().CreateMainMenuCryotechManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", content, async () =>
        {
            await FindAnyObjectByType<MainMenuPsionicsManager>().CreateMainMenuPsionicsManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNeurotechManager>().CreateMainMenuNeurotechManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAntimatterManager>().CreateMainMenuAntimatterManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", content, async () =>
        {
            await FindAnyObjectByType<MainMenuPhantomwareManager>().CreateMainMenuPhantomwareManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", content, async () =>
        {
            await FindAnyObjectByType<MainMenuGravitechManager>().CreateMainMenuGravitechManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAethernetManager>().CreateMainMenuAethernetManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", content, async () =>
        {
            await FindAnyObjectByType<MainMenuStarforgeManager>().CreateMainMenuStarforgeManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOrbitalisManager>().CreateMainMenuOrbitalisManagerAsync(data);
        });
    }
    public void CreateButtonSet3(object data, Transform content)
    {
        CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet3.AZATHOTH, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, Resources.Load<Texture2D>($"UI/Button/Main/Azathoth"), content);
        CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet3.YOG_SOTHOTH, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/Yog-Sothoth"), content);
        CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet3.NYARLATHOTEP, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nyarlathotep"), content);
        CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet3.SHUB_NIGGURATH, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/Shub-Niggurath"), content);
        CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet3.NIHORATH, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nihorath"), content);
        CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet3.AEONAX, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/Aeonax"), content);
        CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet3.SERAPHIROS, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/Seraphiros"), content);
        CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet3.THORINDAR, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/Thorindar"), content);
        CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet3.ZILTHROS, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/Zilthros"), content);

        CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet3.KHORAZAL, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/Khorazal"), content);
        CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet3.IXITHRA, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/Ixithra"), content);
        CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet3.OMNITHEUS, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnitheus"), content);
        CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet3.PHYRIXA, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, Resources.Load<Texture2D>($"UI/Button/Main/Phyrixa"), content);
        CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet3.ATHERION, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, Resources.Load<Texture2D>($"UI/Button/Main/Atherion"), content);
        CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet3.VORATHOS, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, Resources.Load<Texture2D>($"UI/Button/Main/Vorathos"), content);
        CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet3.TENEBRIS, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, Resources.Load<Texture2D>($"UI/Button/Main/Tenebris"), content);

        CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet3.XYLKOR, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, Resources.Load<Texture2D>($"UI/Button/Main/Xylkor"), content);
        CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet3.VELTHARION, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, Resources.Load<Texture2D>($"UI/Button/Main/Veltharion"), content);
        CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet3.ARCANOS, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, Resources.Load<Texture2D>($"UI/Button/Main/Arcanos"), content);
        CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet3.DOLOMATH, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, Resources.Load<Texture2D>($"UI/Button/Main/Dolomath"), content);
        CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet3.ARATHOR, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, Resources.Load<Texture2D>($"UI/Button/Main/Arathor"), content);
        CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet3.XYPHOS, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, Resources.Load<Texture2D>($"UI/Button/Main/Xyphos"), content);
        CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet3.VAELITH, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, Resources.Load<Texture2D>($"UI/Button/Main/Vaelith"), content);


        ButtonEvent.Instance.AssignButtonEvent("Button_1", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAzathothManager>().CreateMainMenuAzathothManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
        {
            await FindAnyObjectByType<MainMenuYogSothothManager>().CreateMainMenuYogSothothManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNyarlathotepManager>().CreateMainMenuNyarlathotepManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
        {
            await FindAnyObjectByType<MainMenuShubNiggurathManager>().CreateMainMenuShubNiggurathManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNihorathManager>().CreateMainMenuNihorathManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAeonaxManager>().CreateMainMenuAeonaxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
        {
            await FindAnyObjectByType<MainMenuSeraphirosManager>().CreateMainMenuSeraphirosManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
        {
            await FindAnyObjectByType<MainMenuThorindarManager>().CreateMainMenuThorindarManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
        {
            await FindAnyObjectByType<MainMenuZilthrosManager>().CreateMainMenuZilthrosManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
        {
            await FindAnyObjectByType<MainMenuKhorazalManager>().CreateMainMenuKhorazalManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
        {
            await FindAnyObjectByType<MainMenuIxithraManager>().CreateMainMenuIxithraManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnitheusManager>().CreateMainMenuOmnitheusManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", content, async () =>
        {
            await FindAnyObjectByType<MainMenuPhyrixaManager>().CreateMainMenuPhyrixaManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAtherionManager>().CreateMainMenuAtherionManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", content, async () =>
        {
            await FindAnyObjectByType<MainMenuVorathosManager>().CreateMainMenuVorathosManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", content, async () =>
        {
            await FindAnyObjectByType<MainMenuTenebrisManager>().CreateMainMenuTenebrisManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", content, async () =>
        {
            await FindAnyObjectByType<MainMenuXylkorManager>().CreateMainMenuXylkorManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", content, async () =>
        {
            await FindAnyObjectByType<MainMenuVeltharionManager>().CreateMainMenuVeltharionManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", content, async () =>
        {
            await FindAnyObjectByType<MainMenuArcanosManager>().CreateMainMenuArcanosManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", content, async () =>
        {
            await FindAnyObjectByType<MainMenuDolomathManager>().CreateMainMenuDolomathManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", content, async () =>
        {
            await FindAnyObjectByType<MainMenuArathorManager>().CreateMainMenuArathorManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", content, async () =>
        {
            await FindAnyObjectByType<MainMenuXyphosManager>().CreateMainMenuXyphosManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", content, async () =>
        {
            await FindAnyObjectByType<MainMenuVaelithManager>().CreateMainMenuVaelithManagerAsync(data);
        });
    }
    public void CreateButtonSet4(object data, Transform content)
    {
        CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet4.ZARX, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, Resources.Load<Texture2D>($"UI/Button/Main/Zarx"), content);
        CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet4.RAIK, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/Raik"), content);
        CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet4.DRAX, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/Drax"), content);
        CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet4.KRON, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/Kron"), content);
        CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet4.ZOLT, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, Resources.Load<Texture2D>($"UI/Button/Main/Zolt"), content);
        CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet4.GORR, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/Gorr"), content);
        CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet4.RYZE, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/Ryze"), content);
        CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet4.JAXX, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/Jaxx"), content);
        CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet4.THAR, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/Thar"), content);

        CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet4.VORN, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/Vorn"), content);
        CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet4.NYX, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nyx"), content);
        CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet4.AROS, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, Resources.Load<Texture2D>($"UI/Button/Main/Aros"), content);
        CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet4.HEX, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, Resources.Load<Texture2D>($"UI/Button/Main/Hex"), content);
        CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet4.LORN, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, Resources.Load<Texture2D>($"UI/Button/Main/Lorn"), content);
        CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet4.BAXX, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, Resources.Load<Texture2D>($"UI/Button/Main/Baxx"), content);
        CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet4.ZEPH, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, Resources.Load<Texture2D>($"UI/Button/Main/Zeph"), content);

        CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet4.KAEL, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, Resources.Load<Texture2D>($"UI/Button/Main/Kael"), content);
        CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet4.DRAV, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, Resources.Load<Texture2D>($"UI/Button/Main/Drav"), content);
        CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet4.TORN, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, Resources.Load<Texture2D>($"UI/Button/Main/Torn"), content);
        CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet4.MYRR, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, Resources.Load<Texture2D>($"UI/Button/Main/Myrr"), content);
        CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet4.VASK, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, Resources.Load<Texture2D>($"UI/Button/Main/Vask"), content);
        CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet4.JORR, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, Resources.Load<Texture2D>($"UI/Button/Main/Jorr"), content);
        CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet4.QUEN, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, Resources.Load<Texture2D>($"UI/Button/Main/Quen"), content);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", content, async () =>
        {
            await FindAnyObjectByType<MainMenuZarxManager>().CreateMainMenuZarxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
        {
            await FindAnyObjectByType<MainMenuRaikManager>().CreateMainMenuRaikManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
        {
            await FindAnyObjectByType<MainMenuDraxManager>().CreateMainMenuDraxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
        {
            await FindAnyObjectByType<MainMenuKronManager>().CreateMainMenuKronManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", content, async () =>
        {
            await FindAnyObjectByType<MainMenuZoltManager>().CreateMainMenuZoltManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
        {
            await FindAnyObjectByType<MainMenuGorrManager>().CreateMainMenuGorrManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
        {
            await FindAnyObjectByType<MainMenuRyzeManager>().CreateMainMenuRyzeManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
        {
            await FindAnyObjectByType<MainMenuJaxxManager>().CreateMainMenuJaxxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
        {
            await FindAnyObjectByType<MainMenuTharManager>().CreateMainMenuTharManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
        {
            await FindAnyObjectByType<MainMenuVornManager>().CreateMainMenuVornManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNyxManager>().CreateMainMenuNyxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", content, async () =>
        {
            await FindAnyObjectByType<MainMenuArosManager>().CreateMainMenuArosManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", content, async () =>
        {
            await FindAnyObjectByType<MainMenuHexManager>().CreateMainMenuHexManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", content, async () =>
        {
            await FindAnyObjectByType<MainMenuLornManager>().CreateMainMenuLornManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", content, async () =>
        {
            await FindAnyObjectByType<MainMenuBaxxManager>().CreateMainMenuBaxxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", content, async () =>
        {
            await FindAnyObjectByType<MainMenuZephManager>().CreateMainMenuZephManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", content, async () =>
        {
            await FindAnyObjectByType<MainMenuKaelManager>().CreateMainMenuKaelManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", content, async () =>
        {
            await FindAnyObjectByType<MainMenuDravManager>().CreateMainMenuDravManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", content, async () =>
        {
            await FindAnyObjectByType<MainMenuTornManager>().CreateMainMenuTornManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", content, async () =>
        {
            await FindAnyObjectByType<MainMenuMyrrManager>().CreateMainMenuMyrrManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", content, async () =>
        {
            await FindAnyObjectByType<MainMenuVaskManager>().CreateMainMenuVaskManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", content, async () =>
        {
            await FindAnyObjectByType<MainMenuJorrManager>().CreateMainMenuJorrManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", content, async () =>
        {
            await FindAnyObjectByType<MainMenuQuenManager>().CreateMainMenuQuenManagerAsync(data);
        });
    }
    public void CreateButtonSet5(object data, Transform content)
    {
        CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet5.ASTRAL_VOICE, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, Resources.Load<Texture2D>($"UI/Button/Main/AstralVoice"), content);
        CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet5.BRANCH_BLADE_SONG, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/BranchBladeSong"), content);
        CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet5.CHAOS_JAZZ, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/ChaosJazz"), content);
        CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet5.CHAOTIC_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/ChaoticMetal"), content);
        CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet5.DAWN_S_BLOOM, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, Resources.Load<Texture2D>($"UI/Button/Main/DawnSBloom"), content);
        CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet5.FANGED_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/FangedMetal"), content);
        CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet5.FREEDOM_BLUES, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/FreedomBlues"), content);
        CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet5.HORMONE_PUNK, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/HormonePunk"), content);
        CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet5.INFERNO_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/InfernoMetal"), content);

        CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet5.KING_OF_THE_SUMMIT, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/KingOfTheSummit"), content);
        CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet5.MOONLIGHT_LULLABY, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/MoonlightLullaby"), content);
        CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet5.PHAETON_S_MELODY, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, Resources.Load<Texture2D>($"UI/Button/Main/PhaetonSMelody"), content);
        CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet5.POLAR_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, Resources.Load<Texture2D>($"UI/Button/Main/PolarMetal"), content);
        CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet5.PROTO_PUNK, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, Resources.Load<Texture2D>($"UI/Button/Main/ProtoPunk"), content);
        CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet5.PUFFER_ELECTRO, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, Resources.Load<Texture2D>($"UI/Button/Main/PufferElectro"), content);
        CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet5.SHADOW_HARMONY, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, Resources.Load<Texture2D>($"UI/Button/Main/ShadowHarmony"), content);

        CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet5.SHOCKSTAR_DISCO, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, Resources.Load<Texture2D>($"UI/Button/Main/ShockstarDisco"), content);
        CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet5.SOUL_ROCK, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, Resources.Load<Texture2D>($"UI/Button/Main/SoulRock"), content);
        CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet5.SWING_JAZZ, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, Resources.Load<Texture2D>($"UI/Button/Main/SwingJazz"), content);
        CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet5.THUNDER_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, Resources.Load<Texture2D>($"UI/Button/Main/ThunderMetal"), content);
        CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet5.WOODPECKER_ELECTRO, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, Resources.Load<Texture2D>($"UI/Button/Main/WoodpeckerElectro"), content);
        CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet5.YUNKUI_TALES, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, Resources.Load<Texture2D>($"UI/Button/Main/YunkuiTales"), content);
        // CreateButtonWithBackground(92, AppDisplayConstants.MainMenuSet5.QUEN, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, Resources.Load<Texture2D>($"UI/Button/Main/Quen"), content);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAstralVoiceManager>().CreateMainMenuAstralVoiceManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
        {
            await FindAnyObjectByType<MainMenuBranchBladeSongManager>().CreateMainMenuBranchBladeSongManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
        {
            await FindAnyObjectByType<MainMenuChaosJazzManager>().CreateMainMenuChaosJazzManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
        {
            await FindAnyObjectByType<MainMenuChaoticMetalManager>().CreateMainMenuChaoticMetalManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", content, async () =>
        {
            await FindAnyObjectByType<MainMenuDawnSBloomManager>().CreateMainMenuDawnSBloomManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
        {
            await FindAnyObjectByType<MainMenuFangedMetalManager>().CreateMainMenuFangedMetalManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
        {
            await FindAnyObjectByType<MainMenuFreedomBluesManager>().CreateMainMenuFreedomBluesManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
        {
            await FindAnyObjectByType<MainMenuHormonePunkManager>().CreateMainMenuHormonePunkManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
        {
            await FindAnyObjectByType<MainMenuInfernoMetalManager>().CreateMainMenuInfernoMetalManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
        {
            await FindAnyObjectByType<MainMenuKingOfTheSummitManager>().CreateMainMenuKingOfTheSummitManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
        {
            await FindAnyObjectByType<MainMenuMoonlightLullabyManager>().CreateMainMenuMoonlightLullabyManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", content, async () =>
        {
            await FindAnyObjectByType<MainMenuPhaetonSMelodyManager>().CreateMainMenuPhaetonSMelodyManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", content, async () =>
        {
            await FindAnyObjectByType<MainMenuPolarMetalManager>().CreateMainMenuPolarMetalManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", content, async () =>
        {
            await FindAnyObjectByType<MainMenuProtoPunkManager>().CreateMainMenuProtoPunkManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", content, async () =>
        {
            await FindAnyObjectByType<MainMenuPufferElectroManager>().CreateMainMenuPufferElectroManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", content, async () =>
        {
            await FindAnyObjectByType<MainMenuShadowHarmonyManager>().CreateMainMenuShadowHarmonyManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", content, async () =>
        {
            await FindAnyObjectByType<MainMenuShockstarDiscoManager>().CreateMainMenuShockstarDiscoManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", content, async () =>
        {
            await FindAnyObjectByType<MainMenuSoulRockManager>().CreateMainMenuSoulRockManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", content, async () =>
        {
            await FindAnyObjectByType<MainMenuSwingJazzManager>().CreateMainMenuSwingJazzManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", content, async () =>
        {
            await FindAnyObjectByType<MainMenuThunderManager>().CreateMainMenuThunderManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", content, async () =>
        {
            await FindAnyObjectByType<MainMenuWoodpeckerElectroManager>().CreateMainMenuWoodpeckerElectroManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", content, async () =>
        {
            await FindAnyObjectByType<MainMenuYunkuiTalesManager>().CreateMainMenuYunkuiTalesManagerAsync(data);
        });
        // ButtonEvent.Instance.AssignButtonEvent("Button_92", content, async () =>
        // {
        //     await FindAnyObjectByType<MainMenuQuenManager>().CreateMainMenuQuenManagerAsync(data);
        // });
    }
    public void CreateButtonSet6(object data, Transform content)
    {
        CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet6.APOTHEON, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, Resources.Load<Texture2D>($"UI/Button/Main/Apotheon"), content);
        CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet6.AXIOM, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/Axiom"), content);
        CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet6.CATACLYSM, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/Cataclysm"), content);
        CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet6.CATALYST, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/Catalyst"), content);
        CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet6.DOMINION, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, Resources.Load<Texture2D>($"UI/Button/Main/Dominion"), content);
        CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet6.ECLIPSE, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/Eclipse"), content);
        CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet6.ELYSIUM, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/Elysium"), content);
        CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet6.EMPYREAN, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/Empyrean"), content);
        CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet6.ENTROPY, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/Entropy"), content);

        CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet6.FLUX, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/Flux"), content);
        CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet6.GENESIS, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/Genesis"), content);
        CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet6.HELIX, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, Resources.Load<Texture2D>($"UI/Button/Main/Helix"), content);
        CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet6.HYPERION, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, Resources.Load<Texture2D>($"UI/Button/Main/Hyperion"), content);
        CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet6.INFERNUM, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, Resources.Load<Texture2D>($"UI/Button/Main/Infernum"), content);
        CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet6.NEXUS, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nexus"), content);
        CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet6.NULLITY, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nullity"), content);

        CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet6.OBLIVION, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, Resources.Load<Texture2D>($"UI/Button/Main/Oblivion"), content);
        CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet6.OBLIVIUM, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, Resources.Load<Texture2D>($"UI/Button/Main/Oblivium"), content);
        CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet6.PARAGON, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, Resources.Load<Texture2D>($"UI/Button/Main/Paragon"), content);
        CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet6.PARALLAX, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, Resources.Load<Texture2D>($"UI/Button/Main/Parallax"), content);
        CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet6.SINGULARITY, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, Resources.Load<Texture2D>($"UI/Button/Main/Singularity"), content);
        CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet6.UMBRA, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, Resources.Load<Texture2D>($"UI/Button/Main/Umbra"), content);
        CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet6.ZENITH, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, Resources.Load<Texture2D>($"UI/Button/Main/Zenith"), content);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", content, async () =>
        {
            await FindAnyObjectByType<MainMenuApotheonManager>().CreateMainMenuApotheonManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAxiomManager>().CreateMainMenuAxiomManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
        {
            await FindAnyObjectByType<MainMenuCataclysmManager>().CreateMainMenuCataclysmManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
        {
            await FindAnyObjectByType<MainMenuCatalystManager>().CreateMainMenuCatalystManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", content, async () =>
        {
            await FindAnyObjectByType<MainMenuDominionManager>().CreateMainMenuDominionManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
        {
            await FindAnyObjectByType<MainMenuEclipseManager>().CreateMainMenuEclipseManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
        {
            await FindAnyObjectByType<MainMenuElysiumManager>().CreateMainMenuElysiumManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
        {
            await FindAnyObjectByType<MainMenuEmpyreanManager>().CreateMainMenuEmpyreanManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
        {
            await FindAnyObjectByType<MainMenuEntropyManager>().CreateMainMenuEntropyManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
        {
            await FindAnyObjectByType<MainMenuFluxManager>().CreateMainMenuFluxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
        {
            await FindAnyObjectByType<MainMenuGenesisManager>().CreateMainMenuGenesisManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", content, async () =>
        {
            await FindAnyObjectByType<MainMenuHelixManager>().CreateMainMenuHelixManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", content, async () =>
        {
            await FindAnyObjectByType<MainMenuHyperionManager>().CreateMainMenuHyperionManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", content, async () =>
        {
            await FindAnyObjectByType<MainMenuInfernumManager>().CreateMainMenuInfernumManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNexusManager>().CreateMainMenuNexusManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNullityManager>().CreateMainMenuNullityManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOblivionManager>().CreateMainMenuOblivionManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", content, async () =>
        {
            await FindAnyObjectByType<MainMenuObliviumManager>().CreateMainMenuObliviumManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", content, async () =>
        {
            await FindAnyObjectByType<MainMenuParagonManager>().CreateMainMenuParagonManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", content, async () =>
        {
            await FindAnyObjectByType<MainMenuParallaxManager>().CreateMainMenuParallaxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", content, async () =>
        {
            await FindAnyObjectByType<MainMenuSingularityManager>().CreateMainMenuSingularityManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", content, async () =>
        {
            await FindAnyObjectByType<MainMenuUmbraManager>().CreateMainMenuUmbraManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", content, async () =>
        {
            await FindAnyObjectByType<MainMenuZenithManager>().CreateMainMenuZenithManagerAsync(data);
        });
    }
    public void CreateButtonSet7(object data, Transform content)
    {
        CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet7.ABYSSAL, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, Resources.Load<Texture2D>($"UI/Button/Main/Abyssal"), content);
        CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet7.ARCANE, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/Arcane"), content);
        CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet7.ASHFRAME, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/Ashframe"), content);
        CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet7.ASTRION, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/Astrion"), content);
        CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet7.AXIOMATA, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, Resources.Load<Texture2D>($"UI/Button/Main/Axiomata"), content);
        CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet7.CHRONYX, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/Chronyx"), content);
        CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet7.COGNITUM, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/Cognitum"), content);
        CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet7.CONTINUUM, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/Continuum"), content);
        CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet7.COSMOS, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/Cosmos"), content);

        CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet7.ETERNUM, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/Eternum"), content);
        CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet7.FERRUMAX, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/Ferrumax"), content);
        CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet7.HORIZON, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, Resources.Load<Texture2D>($"UI/Button/Main/Horizon"), content);
        CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet7.KAELTHRA, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, Resources.Load<Texture2D>($"UI/Button/Main/Kaelthra"), content);
        CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet7.LUMINARY, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, Resources.Load<Texture2D>($"UI/Button/Main/Luminary"), content);
        CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet7.MORVANE, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, Resources.Load<Texture2D>($"UI/Button/Main/Morvane"), content);
        CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet7.NEOTERRA, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, Resources.Load<Texture2D>($"UI/Button/Main/Neoterra"), content);

        CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet7.NEXARIUM, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nexarium"), content);
        CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet7.NOVA, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nova"), content);
        CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet7.OMNIVEX, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, Resources.Load<Texture2D>($"UI/Button/Main/Omnivex"), content);
        CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet7.PARADOX, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, Resources.Load<Texture2D>($"UI/Button/Main/Paradox"), content);
        CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet7.THRENODY, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, Resources.Load<Texture2D>($"UI/Button/Main/Threnody"), content);
        CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet7.VELKRYN, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, Resources.Load<Texture2D>($"UI/Button/Main/Velkryn"), content);
        CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet7.XARPHIS, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, Resources.Load<Texture2D>($"UI/Button/Main/Xarphis"), content);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAbyssalManager>().CreateMainMenuAbyssalManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
        {
            await FindAnyObjectByType<MainMenuArcaneManager>().CreateMainMenuArcaneManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAshframeManager>().CreateMainMenuAshframeManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAstrionManager>().CreateMainMenuAstrionManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", content, async () =>
        {
            await FindAnyObjectByType<MainMenuAxiomataManager>().CreateMainMenuAxiomataManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
        {
            await FindAnyObjectByType<MainMenuChronyxManager>().CreateMainMenuChronyxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
        {
            await FindAnyObjectByType<MainMenuCognitumManager>().CreateMainMenuCognitumManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
        {
            await FindAnyObjectByType<MainMenuContinuumManager>().CreateMainMenuContinuumManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
        {
            await FindAnyObjectByType<MainMenuCosmosManager>().CreateMainMenuCosmosManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
        {
            await FindAnyObjectByType<MainMenuEternumManager>().CreateMainMenuEternumManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
        {
            await FindAnyObjectByType<MainMenuFerrumaxManager>().CreateMainMenuFerrumaxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", content, async () =>
        {
            await FindAnyObjectByType<MainMenuHorizonManager>().CreateMainMenuHorizonManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", content, async () =>
        {
            await FindAnyObjectByType<MainMenuKaelthraManager>().CreateMainMenuKaelthraManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", content, async () =>
        {
            await FindAnyObjectByType<MainMenuLuminaryManager>().CreateMainMenuLuminaryManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", content, async () =>
        {
            await FindAnyObjectByType<MainMenuMorvaneManager>().CreateMainMenuMorvaneManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNeoterraManager>().CreateMainMenuNeoterraManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNexariumManager>().CreateMainMenuNexariumManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", content, async () =>
        {
            await FindAnyObjectByType<MainMenuNovaManager>().CreateMainMenuNovaManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", content, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnivexManager>().CreateMainMenuOmnivexManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", content, async () =>
        {
            await FindAnyObjectByType<MainMenuParadoxManager>().CreateMainMenuParadoxManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", content, async () =>
        {
            await FindAnyObjectByType<MainMenuThrenodyManager>().CreateMainMenuThrenodyManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", content, async () =>
        {
            await FindAnyObjectByType<MainMenuVelkrynManager>().CreateMainMenuVelkrynManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", content, async () =>
        {
            await FindAnyObjectByType<MainMenuXarphisManager>().CreateMainMenuXarphisManagerAsync(data);
        });
    }
    public void CreateButtonSet8(object data, Transform content)
    {
        CreateButtonWithBackground(1, AppDisplayConstants.Master.MASTER_OF_BEAST, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, Resources.Load<Texture2D>($"UI/Button/Main/Zarx"), content);
        CreateButtonWithBackground(2, AppDisplayConstants.Master.MASTER_OF_DRAGON, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, Resources.Load<Texture2D>($"UI/Button/Main/Raik"), content);
        CreateButtonWithBackground(3, AppDisplayConstants.Master.MASTER_OF_MAGIC, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, Resources.Load<Texture2D>($"UI/Button/Main/Drax"), content);
        CreateButtonWithBackground(4, AppDisplayConstants.Master.MASTER_OF_MUSIC, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, Resources.Load<Texture2D>($"UI/Button/Main/Kron"), content);
        CreateButtonWithBackground(5, AppDisplayConstants.Master.MASTER_OF_SCIENCE, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, Resources.Load<Texture2D>($"UI/Button/Main/Zolt"), content);
        CreateButtonWithBackground(6, AppDisplayConstants.Master.MASTER_OF_SPIRIT, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, Resources.Load<Texture2D>($"UI/Button/Main/Gorr"), content);
        CreateButtonWithBackground(7, AppDisplayConstants.Master.MASTER_OF_WEAPON, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, Resources.Load<Texture2D>($"UI/Button/Main/Ryze"), content);
        CreateButtonWithBackground(8, AppDisplayConstants.Master.MASTER_OF_CHEMICAL, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, Resources.Load<Texture2D>($"UI/Button/Main/Jaxx"), content);
        CreateButtonWithBackground(9, AppDisplayConstants.Master.MASTER_OF_PHYSICAL, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, Resources.Load<Texture2D>($"UI/Button/Main/Thar"), content);

        CreateButtonWithBackground(10, AppDisplayConstants.Master.MASTER_OF_ATOMIC, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, Resources.Load<Texture2D>($"UI/Button/Main/Vorn"), content);
        CreateButtonWithBackground(11, AppDisplayConstants.Master.MASTER_OF_MENTAL, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, Resources.Load<Texture2D>($"UI/Button/Main/Nyx"), content);
        // CreateButtonWithBackground(104, AppDisplayConstants.MainMenuSet4.Aros, ImageConstants.Background.AdvancedBackground12, Resources.Load<Texture2D>($"UI/Button/Main/Aros"), content);
        // CreateButtonWithBackground(105, AppDisplayConstants.MainMenuSet4.Hex, ImageConstants.Background.AdvancedBackground13, Resources.Load<Texture2D>($"UI/Button/Main/Hex"), content);
        // CreateButtonWithBackground(83, AppDisplayConstants.MainMenuSet4.Lorn, ImageConstants.Background.AdvancedBackground14, Resources.Load<Texture2D>($"UI/Button/Main/Lorn"), content);
        // CreateButtonWithBackground(84, AppDisplayConstants.MainMenuSet4.Baxx, ImageConstants.Background.AdvancedBackground15, Resources.Load<Texture2D>($"UI/Button/Main/Baxx"), content);
        // CreateButtonWithBackground(85, AppDisplayConstants.MainMenuSet4.Zeph, ImageConstants.Background.AdvancedBackground16, Resources.Load<Texture2D>($"UI/Button/Main/Zeph"), content);

        // CreateButtonWithBackground(86, AppDisplayConstants.MainMenuSet4.Kael, ImageConstants.Background.AdvancedBackground17, Resources.Load<Texture2D>($"UI/Button/Main/Kael"), content);
        // CreateButtonWithBackground(87, AppDisplayConstants.MainMenuSet4.Drav, ImageConstants.Background.AdvancedBackground18, Resources.Load<Texture2D>($"UI/Button/Main/Drav"), content);
        // CreateButtonWithBackground(88, AppDisplayConstants.MainMenuSet4.Torn, ImageConstants.Background.AdvancedBackground19, Resources.Load<Texture2D>($"UI/Button/Main/Torn"), content);
        // CreateButtonWithBackground(89, AppDisplayConstants.MainMenuSet4.Myrr, ImageConstants.Background.AdvancedBackground20, Resources.Load<Texture2D>($"UI/Button/Main/Myrr"), content);
        // CreateButtonWithBackground(90, AppDisplayConstants.MainMenuSet4.Vask, ImageConstants.Background.AdvancedBackground21, Resources.Load<Texture2D>($"UI/Button/Main/Vask"), content);
        // CreateButtonWithBackground(91, AppDisplayConstants.MainMenuSet4.Jorr, ImageConstants.Background.AdvancedBackground22, Resources.Load<Texture2D>($"UI/Button/Main/Jorr"), content);
        // CreateButtonWithBackground(92, AppDisplayConstants.MainMenuSet4.Quen, ImageConstants.Background.AdvancedBackground23, Resources.Load<Texture2D>($"UI/Button/Main/Quen"), content);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", content, async () =>
        {
            await MasterOfBeastManager.Instance.CreateMasterOfBeastManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", content, async () =>
        {
            await MasterOfDragonManager.Instance.CreateMasterOfDragonManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", content, async () =>
        {
            await MasterOfMagicManager.Instance.CreateMasterOfMagicManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", content, async () =>
        {
            await MasterOfMusicManager.Instance.CreateMasterOfMusicManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", content, async () =>
        {
            await MasterOfScienceManager.Instance.CreateMasterOfScienceManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", content, async () =>
        {
            await MasterOfSpiritManager.Instance.CreateMasterOfSpiritManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", content, async () =>
        {
            await MasterOfWeaponManager.Instance.CreateMasterOfWeaponManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", content, async () =>
        {
            await MasterOfChemicalManager.Instance.CreateMasterOfChemicalManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", content, async () =>
        {
            await MasterOfPhysicalManager.Instance.CreateMasterOfPhysicalManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", content, async () =>
        {
            await MasterOfAtomicManager.Instance.CreateMasterOfAtomicManagerAsync(data);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", content, async () =>
        {
            await MasterOfMentalManager.Instance.CreateMasterOfMentalManagerAsync(data);
        });
        // ButtonEvent.Instance.AssignButtonEvent("Button_81", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuArosManager>().CreateMainMenuArosManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_82", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuHexManager>().CreateMainMenuHexManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_83", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuLornManager>().CreateMainMenuLornManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_84", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuBaxxManager>().CreateMainMenuBaxxManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_85", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuZephManager>().CreateMainMenuZephManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_86", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuKaelManager>().CreateMainMenuKaelManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_87", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuDravManager>().CreateMainMenuDravManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_88", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuTornManager>().CreateMainMenuTornManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_89", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuMyrrManager>().CreateMainMenuMyrrManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_90", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuVaskManager>().CreateMainMenuVaskManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_91", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuJorrManager>().CreateMainMenuJorrManager(data);
        // });
        // ButtonEvent.Instance.AssignButtonEvent("Button_92", content, () =>
        // {
        //     FindAnyObjectByType<MainMenuQuenManager>().CreateMainMenuQuenManager(data);
        // });
    }
}
