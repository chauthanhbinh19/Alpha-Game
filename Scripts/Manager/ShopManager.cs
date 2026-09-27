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
    private PaginationManager PaginationManager;
    private int Offset = 0;
    private int CurrentPage = 1;
    private int TotalItems;
    private const int PAGE_SIZE = 100;
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        ShopButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Shop.SHOP_BUTTON_PREFAB);
        ShopManagerPrefab = UIManager.Instance.Get(AppConstants.Prefab.Shop.SHOP_MANAGER_PREFAB);
        ShopPrefab = UIManager.Instance.Get(AppConstants.Prefab.Shop.SHOP_PREFAB);
        TypeButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.TAB_BUTTON_PREFAB);
        EquipmentShopPrefab = UIManager.Instance.Get(AppConstants.Prefab.Equipment.EQUIPMENT_SHOP_PREFAB);
        PopupPanel = UIManager.Instance.GetTransform(AppConstants.Transform.POPUP_PANEL);
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
    public async Task CreateShopPanelAsync()
    {
        // MainPanel = panel;
        CurrentObject = Instantiate(ShopManagerPrefab, MainPanel);
        Transform transform = CurrentObject.transform;
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        TitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.SHOP);
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
        CreateButton(1, AppDisplayConstants.Title.ACHIEVEMENTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL), tempContent);
        CreateButton(2, AppDisplayConstants.Title.ALCHEMIES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL), tempContent);
        CreateButton(3, AppDisplayConstants.Title.ARCHITECTURES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL), tempContent);
        CreateButton(4, AppDisplayConstants.Title.ARTIFACTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL), tempContent);
        CreateButton(5, AppDisplayConstants.Title.ARTWORKS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL), tempContent);
        CreateButton(6, AppDisplayConstants.Title.AVATAR, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.AVATAR_URL), tempContent);
        CreateButton(7, AppDisplayConstants.Title.BADGES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), tempContent);
        CreateButton(8, AppDisplayConstants.Title.BEVERAGES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BEVERAGE_URL), tempContent);
        CreateButton(9, AppDisplayConstants.Title.BOOKS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL), tempContent);
        CreateButton(10, AppDisplayConstants.Title.BORDERS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL), tempContent);
        CreateButton(11, AppDisplayConstants.Title.BUILDINGS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL), tempContent);
        CreateButton(12, AppDisplayConstants.Title.CARD_ADMIRALS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL), tempContent);
        CreateButton(13, AppDisplayConstants.Title.CARD_CAPTAINS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL), tempContent);
        CreateButton(14, AppDisplayConstants.Title.CARD_COLONELS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL), tempContent);
        CreateButton(15, AppDisplayConstants.Title.CARD_GENERALS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL), tempContent);
        CreateButton(16, AppDisplayConstants.Title.CARD_HEROES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL), tempContent);
        CreateButton(17, AppDisplayConstants.Title.CARD_LIVES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_LIFE_URL), tempContent);
        CreateButton(18, AppDisplayConstants.Title.CARD_MILITARIES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MILITARY_URL), tempContent);
        CreateButton(19, AppDisplayConstants.Title.CARD_MONSTERS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL), tempContent);
        CreateButton(20, AppDisplayConstants.Title.CARD_SOLDIERS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SOLDIER_URL), tempContent);
        CreateButton(21, AppDisplayConstants.Title.CARD_SPELLS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL), tempContent);
        CreateButton(22, AppDisplayConstants.Title.COLLABORATIONS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL), tempContent);
        CreateButton(23, AppDisplayConstants.Title.COLLABORATION_EQUIPMENTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL), tempContent);
        CreateButton(24, AppDisplayConstants.Title.CORES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL), tempContent);
        CreateButton(25, AppDisplayConstants.Title.EMOJIS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL), tempContent);
        CreateButton(26, AppDisplayConstants.Title.EQUIPMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL), tempContent);
        CreateButton(27, AppDisplayConstants.Title.FASHIONS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL), tempContent);
        CreateButton(28, AppDisplayConstants.Title.FOODS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FOOD_URL), tempContent);
        CreateButton(29, AppDisplayConstants.Title.FORGES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL), tempContent);
        CreateButton(30, AppDisplayConstants.Title.FURNITURES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FURNITURE_URL), tempContent);
        CreateButton(31, AppDisplayConstants.Title.MAGIC_FORMATION_CIRCLES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), tempContent);
        CreateButton(32, AppDisplayConstants.Title.MECHA_BEASTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), tempContent);
        CreateButton(33, AppDisplayConstants.Title.MEDALS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL), tempContent);
        CreateButton(34, AppDisplayConstants.Title.OUTFITS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.OUTFIT_URL), tempContent);
        CreateButton(35, AppDisplayConstants.Title.PETS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL), tempContent);
        CreateButton(36, AppDisplayConstants.Title.PLANTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL), tempContent);
        CreateButton(37, AppDisplayConstants.Title.PUPPETS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL), tempContent);
        CreateButton(38, AppDisplayConstants.Title.RELICS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL), tempContent);
        CreateButton(39, AppDisplayConstants.Title.ROBOTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL), tempContent);
        CreateButton(40, AppDisplayConstants.Title.RUNES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), tempContent);
        CreateButton(41, AppDisplayConstants.Title.SKILLS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL), tempContent);
        CreateButton(42, AppDisplayConstants.Title.SPIRIT_BEASTS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL), tempContent);
        CreateButton(43, AppDisplayConstants.Title.SPIRIT_CARDS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL), tempContent);
        CreateButton(44, AppDisplayConstants.Title.SYMBOLS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL), tempContent);
        CreateButton(45, AppDisplayConstants.Title.TALISMANS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL), tempContent);
        CreateButton(46, AppDisplayConstants.Title.TECHNOLOGIES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL), tempContent);
        CreateButton(47, AppDisplayConstants.Title.TITLES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL), tempContent);
        CreateButton(48, AppDisplayConstants.Title.VEHICLES, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL), tempContent);
        CreateButton(49, AppDisplayConstants.Title.WEAPONS, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL), tempContent);

        CreateShopButtonEvent(tempContent);
    }
    public void CreateShopButtonEvent(Transform panel)
    {
        // AssignButtonEvent("Button_1", panel, () => GetType(AppConstants.Shop.ShopCodeName.ACHIEVEMENTS_SHOP));
        // AssignButtonEvent("Button_2", panel, () => GetType(AppConstants.Shop.ShopCodeName.ALCHEMIES_SHOP));
        // AssignButtonEvent("Button_3", panel, () => GetType(AppConstants.Shop.ShopCodeName.ARCHITECTURES_SHOP));
        // AssignButtonEvent("Button_4", panel, () => GetType(AppConstants.Shop.ShopCodeName.ARTIFACTS_SHOP));
        // AssignButtonEvent("Button_5", panel, () => GetType(AppConstants.Shop.ShopCodeName.ARTWORKS_SHOP));
        // AssignButtonEvent("Button_6", panel, () => GetType(AppConstants.Shop.ShopCodeName.AVATARS_SHOP));
        // AssignButtonEvent("Button_7", panel, () => GetType(AppConstants.Shop.ShopCodeName.BADGES_SHOP));
        // AssignButtonEvent("Button_8", panel, () => GetType(AppConstants.Shop.ShopCodeName.BEVERAGES_SHOP));
        // AssignButtonEvent("Button_9", panel, () => GetType(AppConstants.Shop.ShopCodeName.BOOKS_SHOP));
        // AssignButtonEvent("Button_10", panel, () => GetType(AppConstants.Shop.ShopCodeName.BORDERS_SHOP));
        // AssignButtonEvent("Button_11", panel, () => GetType(AppConstants.Shop.ShopCodeName.BUILDINGS_SHOP));
        // AssignButtonEvent("Button_12", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_ADMIRALS_SHOP));
        // AssignButtonEvent("Button_13", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_CAPTAINS_SHOP));
        // AssignButtonEvent("Button_14", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_COLONELS_SHOP));
        // AssignButtonEvent("Button_15", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_GENERALS_SHOP));
        AssignButtonEvent("Button_16", panel, async () => await CardHeroesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_HEROES_SHOP));
        // AssignButtonEvent("Button_17", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_LIVES_SHOP));
        // AssignButtonEvent("Button_18", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_MILITARIES_SHOP));
        // AssignButtonEvent("Button_19", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_MONSTERS_SHOP));
        // AssignButtonEvent("Button_20", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_SOLDIERS_SHOP));
        // AssignButtonEvent("Button_21", panel, () => GetType(AppConstants.Shop.ShopCodeName.CARD_SPELLS_SHOP));
        // AssignButtonEvent("Button_22", panel, () => GetType(AppConstants.Shop.ShopCodeName.COLLABORATIONS_SHOP));
        // AssignButtonEvent("Button_23", panel, () => GetType(AppConstants.Shop.ShopCodeName.COLLABORATION_EQUIPMENTS_SHOP));
        // AssignButtonEvent("Button_24", panel, () => GetType(AppConstants.Shop.ShopCodeName.CORES_SHOP));
        // AssignButtonEvent("Button_25", panel, () => GetType(AppConstants.Shop.ShopCodeName.EMOJIS_SHOP));
        // AssignButtonEvent("Button_26", panel, () => GetType(AppConstants.Shop.ShopCodeName.EQUIPMENTS_SHOP));
        // AssignButtonEvent("Button_27", panel, () => GetType(AppConstants.Shop.ShopCodeName.FASHIONS_SHOP));
        // AssignButtonEvent("Button_28", panel, () => GetType(AppConstants.Shop.ShopCodeName.FOODS_SHOP));
        // AssignButtonEvent("Button_29", panel, () => GetType(AppConstants.Shop.ShopCodeName.FORGES_SHOP));
        // AssignButtonEvent("Button_30", panel, () => GetType(AppConstants.Shop.ShopCodeName.FURNITURES_SHOP));
        // AssignButtonEvent("Button_31", panel, () => GetType(AppConstants.Shop.ShopCodeName.MAGIC_FORMATION_CIRCLES_SHOP));
        // AssignButtonEvent("Button_32", panel, () => GetType(AppConstants.Shop.ShopCodeName.MECHA_BEASTS_SHOP));
        // AssignButtonEvent("Button_33", panel, () => GetType(AppConstants.Shop.ShopCodeName.MEDALS_SHOP));
        // AssignButtonEvent("Button_34", panel, () => GetType(AppConstants.Shop.ShopCodeName.OUTFITS_SHOP));
        // AssignButtonEvent("Button_35", panel, () => GetType(AppConstants.Shop.ShopCodeName.PETS_SHOP));
        // AssignButtonEvent("Button_36", panel, () => GetType(AppConstants.Shop.ShopCodeName.PLANTS_SHOP));
        // AssignButtonEvent("Button_37", panel, () => GetType(AppConstants.Shop.ShopCodeName.PUPPETS_SHOP));
        // AssignButtonEvent("Button_38", panel, () => GetType(AppConstants.Shop.ShopCodeName.RELICS_SHOP));
        // AssignButtonEvent("Button_39", panel, () => GetType(AppConstants.Shop.ShopCodeName.ROBOTS_SHOP));
        // AssignButtonEvent("Button_40", panel, () => GetType(AppConstants.Shop.ShopCodeName.RUNES_SHOP));
        // AssignButtonEvent("Button_41", panel, () => GetType(AppConstants.Shop.ShopCodeName.SKILLS_SHOP));
        // AssignButtonEvent("Button_42", panel, () => GetType(AppConstants.Shop.ShopCodeName.SPIRIT_BEASTS_SHOP));
        // AssignButtonEvent("Button_43", panel, () => GetType(AppConstants.Shop.ShopCodeName.SPIRIT_CARDS_SHOP));
        // AssignButtonEvent("Button_44", panel, () => GetType(AppConstants.Shop.ShopCodeName.SYMBOLS_SHOP));
        // AssignButtonEvent("Button_45", panel, () => GetType(AppConstants.Shop.ShopCodeName.TALISMANS_SHOP));
        // AssignButtonEvent("Button_46", panel, () => GetType(AppConstants.Shop.ShopCodeName.TECHNOLOGIES_SHOP));
        // AssignButtonEvent("Button_47", panel, () => GetType(AppConstants.Shop.ShopCodeName.TITLES_SHOP));
        // AssignButtonEvent("Button_48", panel, () => GetType(AppConstants.Shop.ShopCodeName.VEHICLES_SHOP));
        // AssignButtonEvent("Button_49", panel, () => GetType(AppConstants.Shop.ShopCodeName.WEAPONS_SHOP));
    }
}
