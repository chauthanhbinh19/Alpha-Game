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
    private Button CloseButton;
    private Button HomeButton;
    private PaginationManager PaginationManager;
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
        ShopButtonPrefab = UIManager.Instance.Get(PrefabConstants.Shop.SHOP_BUTTON_PREFAB);
        ShopManagerPrefab = UIManager.Instance.Get(PrefabConstants.Shop.SHOP_MANAGER_PREFAB);
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
        CreateButton(1, AppDisplayConstants.Title.Shop.ACHIEVEMENTS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL), tempContent);
        CreateButton(2, AppDisplayConstants.Title.Shop.ALCHEMIES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL), tempContent);
        CreateButton(3, AppDisplayConstants.Title.Shop.ARCHITECTURES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL), tempContent);
        CreateButton(4, AppDisplayConstants.Title.Shop.ARTIFACTS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL), tempContent);
        CreateButton(5, AppDisplayConstants.Title.Shop.ARTWORKS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL), tempContent);
        CreateButton(6, AppDisplayConstants.Title.Shop.AVATARS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.AVATAR_URL), tempContent);
        CreateButton(7, AppDisplayConstants.Title.Shop.BADGES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), tempContent);
        CreateButton(8, AppDisplayConstants.Title.Shop.BEVERAGES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BEVERAGE_URL), tempContent);
        CreateButton(9, AppDisplayConstants.Title.Shop.BOOKS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL), tempContent);
        CreateButton(10, AppDisplayConstants.Title.Shop.BORDERS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL), tempContent);
        CreateButton(11, AppDisplayConstants.Title.Shop.BUILDINGS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL), tempContent);
        CreateButton(12, AppDisplayConstants.Title.Shop.CARD_ADMIRALS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL), tempContent);
        CreateButton(13, AppDisplayConstants.Title.Shop.CARD_CAPTAINS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL), tempContent);
        CreateButton(14, AppDisplayConstants.Title.Shop.CARD_COLONELS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL), tempContent);
        CreateButton(15, AppDisplayConstants.Title.Shop.CARD_GENERALS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL), tempContent);
        CreateButton(16, AppDisplayConstants.Title.Shop.CARD_HEROES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL), tempContent);
        CreateButton(17, AppDisplayConstants.Title.Shop.CARD_LIVES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_LIFE_URL), tempContent);
        CreateButton(18, AppDisplayConstants.Title.Shop.CARD_MILITARIES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MILITARY_URL), tempContent);
        CreateButton(19, AppDisplayConstants.Title.Shop.CARD_MONSTERS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL), tempContent);
        CreateButton(20, AppDisplayConstants.Title.Shop.CARD_SOLDIERS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SOLDIER_URL), tempContent);
        CreateButton(21, AppDisplayConstants.Title.Shop.CARD_SPELLS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL), tempContent);
        CreateButton(22, AppDisplayConstants.Title.Shop.COLLABORATIONS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL), tempContent);
        CreateButton(23, AppDisplayConstants.Title.Shop.COLLABORATION_EQUIPMENTS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL), tempContent);
        CreateButton(24, AppDisplayConstants.Title.Shop.CORES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL), tempContent);
        CreateButton(25, AppDisplayConstants.Title.Shop.EMOJIS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL), tempContent);
        CreateButton(26, AppDisplayConstants.Title.Shop.EQUIPMENTS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL), tempContent);
        CreateButton(27, AppDisplayConstants.Title.Shop.FASHIONS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL), tempContent);
        CreateButton(28, AppDisplayConstants.Title.Shop.FOODS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FOOD_URL), tempContent);
        CreateButton(29, AppDisplayConstants.Title.Shop.FORGES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL), tempContent);
        CreateButton(30, AppDisplayConstants.Title.Shop.FURNITURES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FURNITURE_URL), tempContent);
        CreateButton(31, AppDisplayConstants.Title.Shop.MAGIC_FORMATION_CIRCLES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), tempContent);
        CreateButton(32, AppDisplayConstants.Title.Shop.MECHA_BEASTS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), tempContent);
        CreateButton(33, AppDisplayConstants.Title.Shop.MEDALS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL), tempContent);
        CreateButton(34, AppDisplayConstants.Title.Shop.OUTFITS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.OUTFIT_URL), tempContent);
        CreateButton(35, AppDisplayConstants.Title.Shop.PETS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL), tempContent);
        CreateButton(36, AppDisplayConstants.Title.Shop.PLANTS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL), tempContent);
        CreateButton(37, AppDisplayConstants.Title.Shop.PUPPETS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL), tempContent);
        CreateButton(38, AppDisplayConstants.Title.Shop.RELICS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL), tempContent);
        CreateButton(39, AppDisplayConstants.Title.Shop.ROBOTS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL), tempContent);
        CreateButton(40, AppDisplayConstants.Title.Shop.RUNES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), tempContent);
        CreateButton(41, AppDisplayConstants.Title.Shop.SKILLS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL), tempContent);
        CreateButton(42, AppDisplayConstants.Title.Shop.SPIRIT_BEASTS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL), tempContent);
        CreateButton(43, AppDisplayConstants.Title.Shop.SPIRIT_CARDS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL), tempContent);
        CreateButton(44, AppDisplayConstants.Title.Shop.SYMBOLS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL), tempContent);
        CreateButton(45, AppDisplayConstants.Title.Shop.TALISMANS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL), tempContent);
        CreateButton(46, AppDisplayConstants.Title.Shop.TECHNOLOGIES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL), tempContent);
        CreateButton(47, AppDisplayConstants.Title.Shop.TITLES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL), tempContent);
        CreateButton(48, AppDisplayConstants.Title.Shop.VEHICLES_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL), tempContent);
        CreateButton(49, AppDisplayConstants.Title.Shop.WEAPONS_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL), tempContent);

        CreateShopButtonEvent(tempContent);
    }
    public void CreateShopButtonEvent(Transform panel)
    {
        AssignButtonEvent("Button_1", panel, async () => await AchievementsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ACHIEVEMENTS_SHOP));
        AssignButtonEvent("Button_2", panel, async () => await AlchemiesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALCHEMIES_SHOP));
        AssignButtonEvent("Button_3", panel, async () => await ArchitecturesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ARCHITECTURES_SHOP));
        AssignButtonEvent("Button_4", panel, async () => await ArtifactsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ARTIFACTS_SHOP));
        AssignButtonEvent("Button_5", panel, async () => await ArtworksController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ARTWORKS_SHOP));
        AssignButtonEvent("Button_6", panel, async () => await AvatarsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.AVATARS_SHOP));
        AssignButtonEvent("Button_7", panel, async () => await BadgesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.BADGES_SHOP));
        AssignButtonEvent("Button_8", panel, async () => await BeveragesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.BEVERAGES_SHOP));
        AssignButtonEvent("Button_9", panel, async () => await BooksController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.BOOKS_SHOP));
        AssignButtonEvent("Button_10", panel, async () => await BordersController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.BORDERS_SHOP));
        AssignButtonEvent("Button_11", panel, async () => await BuildingsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.BUILDINGS_SHOP));
        AssignButtonEvent("Button_12", panel, async () => await CardAdmiralsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_ADMIRALS_SHOP));
        AssignButtonEvent("Button_13", panel, async () => await CardCaptainsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_CAPTAINS_SHOP));
        AssignButtonEvent("Button_14", panel, async () => await CardColonelsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_COLONELS_SHOP));
        AssignButtonEvent("Button_15", panel, async () => await CardGeneralsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_GENERALS_SHOP));
        AssignButtonEvent("Button_16", panel, async () => await CardHeroesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_HEROES_SHOP));
        AssignButtonEvent("Button_17", panel, async () => await CardLivesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_LIVES_SHOP));
        AssignButtonEvent("Button_18", panel, async () => await CardMilitariesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_MILITARIES_SHOP));
        AssignButtonEvent("Button_19", panel, async () => await CardMonstersController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_MONSTERS_SHOP));
        AssignButtonEvent("Button_20", panel, async () => await CardSoldiersController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_SOLDIERS_SHOP));
        AssignButtonEvent("Button_21", panel, async () => await CardSpellsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CARD_SPELLS_SHOP));
        AssignButtonEvent("Button_22", panel, async () => await CollaborationsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.COLLABORATIONS_SHOP));
        AssignButtonEvent("Button_23", panel, async () => await CollaborationEquipmentsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.COLLABORATION_EQUIPMENTS_SHOP));
        AssignButtonEvent("Button_24", panel, async () => await CoresController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.CORES_SHOP));
        AssignButtonEvent("Button_25", panel, async () => await EmojisController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.EMOJIS_SHOP));
        AssignButtonEvent("Button_26", panel, async () => await EquipmentsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.EQUIPMENTS_SHOP));
        AssignButtonEvent("Button_27", panel, async () => await FashionsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.FASHIONS_SHOP));
        AssignButtonEvent("Button_28", panel, async () => await FoodsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.FOODS_SHOP));
        AssignButtonEvent("Button_29", panel, async () => await ForgesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.FORGES_SHOP));
        AssignButtonEvent("Button_30", panel, async () => await FurnituresController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.FURNITURES_SHOP));
        AssignButtonEvent("Button_31", panel, async () => await MagicFormationCirclesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.MAGIC_FORMATION_CIRCLES_SHOP));
        AssignButtonEvent("Button_32", panel, async () => await MechaBeastsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.MECHA_BEASTS_SHOP));
        AssignButtonEvent("Button_33", panel, async () => await MedalsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.MEDALS_SHOP));
        AssignButtonEvent("Button_34", panel, async () => await OutfitsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.OUTFITS_SHOP));
        AssignButtonEvent("Button_35", panel, async () => await PetsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.PETS_SHOP));
        AssignButtonEvent("Button_36", panel, async () => await PlantsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.PLANTS_SHOP));
        AssignButtonEvent("Button_37", panel, async () => await PuppetsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.PUPPETS_SHOP));
        AssignButtonEvent("Button_38", panel, async () => await RelicsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.RELICS_SHOP));
        AssignButtonEvent("Button_39", panel, async () => await RobotsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ROBOTS_SHOP));
        AssignButtonEvent("Button_40", panel, async () => await RunesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.RUNES_SHOP));
        AssignButtonEvent("Button_41", panel, async () => await SkillsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.SKILLS_SHOP));
        AssignButtonEvent("Button_42", panel, async () => await SpiritBeastsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.SPIRIT_BEASTS_SHOP));
        AssignButtonEvent("Button_43", panel, async () => await SpiritCardsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.SPIRIT_CARDS_SHOP));
        AssignButtonEvent("Button_44", panel, async () => await SymbolsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.SYMBOLS_SHOP));
        AssignButtonEvent("Button_45", panel, async () => await TalismansController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.TALISMANS_SHOP));
        AssignButtonEvent("Button_46", panel, async () => await TechnologiesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.TECHNOLOGIES_SHOP));
        AssignButtonEvent("Button_47", panel, async () => await TitlesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.TITLES_SHOP));
        AssignButtonEvent("Button_48", panel, async () => await VehiclesController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.VEHICLES_SHOP));
        AssignButtonEvent("Button_49", panel, async () => await WeaponsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.WEAPONS_SHOP));
    }
    public async Task CreateAlliteriaCampaignWorldShopPanelAsync()
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
        CreateButton(1, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_A_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_A_URL), tempContent);
        CreateButton(2, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_B_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_B_URL), tempContent);
        CreateButton(3, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_C_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_C_URL), tempContent);
        CreateButton(4, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_D_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_D_URL), tempContent);
        CreateButton(5, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_E_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_E_URL), tempContent);
        CreateButton(6, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_F_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_F_URL), tempContent);
        CreateButton(7, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_G_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_G_URL), tempContent);
        CreateButton(8, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_H_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_H_URL), tempContent);
        CreateButton(9, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_I_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_I_URL), tempContent);
        CreateButton(10, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_J_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_J_URL), tempContent);
        CreateButton(11, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_K_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_K_URL), tempContent);
        CreateButton(12, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_L_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_L_URL), tempContent);
        CreateButton(13, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_M_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_M_URL), tempContent);
        CreateButton(14, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_N_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_N_URL), tempContent);
        CreateButton(15, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_O_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_O_URL), tempContent);
        CreateButton(16, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_P_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_P_URL), tempContent);
        CreateButton(17, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_Q_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_Q_URL), tempContent);
        CreateButton(18, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_R_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_R_URL), tempContent);
        CreateButton(19, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_S_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_S_URL), tempContent);
        CreateButton(20, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_T_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_T_URL), tempContent);
        CreateButton(21, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_U_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_U_URL), tempContent);
        CreateButton(22, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_V_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_V_URL), tempContent);
        CreateButton(23, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_W_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_W_URL), tempContent);
        CreateButton(24, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_X_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_X_URL), tempContent);
        CreateButton(25, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_Y_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_Y_URL), tempContent);
        CreateButton(26, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_Z_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_CAMPAIGN_WORLD_Z_URL), tempContent);

        CreateCampaignShopButtonEvent(tempContent);
    }
    public void CreateCampaignShopButtonEvent(Transform panel)
    {
        AssignButtonEvent("Button_1", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_A_SHOP));
        AssignButtonEvent("Button_2", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_B_SHOP));
        AssignButtonEvent("Button_3", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_C_SHOP));
        AssignButtonEvent("Button_4", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_D_SHOP));
        AssignButtonEvent("Button_5", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_E_SHOP));
        AssignButtonEvent("Button_6", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_F_SHOP));
        AssignButtonEvent("Button_7", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_G_SHOP));
        AssignButtonEvent("Button_8", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_H_SHOP));
        AssignButtonEvent("Button_9", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_I_SHOP));
        AssignButtonEvent("Button_10", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_J_SHOP));
        AssignButtonEvent("Button_11", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_K_SHOP));
        AssignButtonEvent("Button_12", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_L_SHOP));
        AssignButtonEvent("Button_13", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_M_SHOP));
        AssignButtonEvent("Button_14", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_N_SHOP));
        AssignButtonEvent("Button_15", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_O_SHOP));
        AssignButtonEvent("Button_16", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_P_SHOP));
        AssignButtonEvent("Button_17", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_Q_SHOP));
        AssignButtonEvent("Button_18", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_R_SHOP));
        AssignButtonEvent("Button_19", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_S_SHOP));
        AssignButtonEvent("Button_20", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_T_SHOP));
        AssignButtonEvent("Button_21", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_U_SHOP));
        AssignButtonEvent("Button_22", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_V_SHOP));
        AssignButtonEvent("Button_23", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_W_SHOP));
        AssignButtonEvent("Button_24", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_X_SHOP));
        AssignButtonEvent("Button_25", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_Y_SHOP));
        AssignButtonEvent("Button_26", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_CAMPAIGN_WORLD_Z_SHOP));
    }
    public async Task CreateAlliteriaDarkWorldShopPanelAsync()
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
        CreateButton(1, AppDisplayConstants.Title.Shop.ALLITERIA_DARK_WORLD_BRONZE_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_DARK_WORLD_BRONZE_MYTHIC_URL), tempContent);
        CreateButton(2, AppDisplayConstants.Title.Shop.ALLITERIA_DARK_WORLD_SILVER_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_DARK_WORLD_SILVER_MYTHIC_URL), tempContent);
        CreateButton(3, AppDisplayConstants.Title.Shop.ALLITERIA_DARK_WORLD_EMERALD_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_DARK_WORLD_EMERALD_MYTHIC_URL), tempContent);
        CreateButton(4, AppDisplayConstants.Title.Shop.ALLITERIA_DARK_WORLD_AMETHYST_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_DARK_WORLD_AMETHYST_MYTHIC_URL), tempContent);
        CreateButton(5, AppDisplayConstants.Title.Shop.ALLITERIA_DARK_WORLD_GOLD_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_DARK_WORLD_GOLD_MYTHIC_URL), tempContent);
        CreateButton(6, AppDisplayConstants.Title.Shop.ALLITERIA_DARK_WORLD_DIAMOND_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_DARK_WORLD_DIAMOND_MYTHIC_URL), tempContent);
        CreateButton(7, AppDisplayConstants.Title.Shop.ALLITERIA_DARK_WORLD_SOLAR_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_DARK_WORLD_SOLAR_MYTHIC_URL), tempContent);

        CreateDarkWorldShopButtonEvent(tempContent);
    }
    public void CreateDarkWorldShopButtonEvent(Transform panel)
    {
        AssignButtonEvent("Button_1", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_DARK_WORLD_BRONZE_MYTHIC_SHOP));
        AssignButtonEvent("Button_2", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_DARK_WORLD_SILVER_MYTHIC_SHOP));
        AssignButtonEvent("Button_3", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_DARK_WORLD_EMERALD_MYTHIC_SHOP));
        AssignButtonEvent("Button_4", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_DARK_WORLD_AMETHYST_MYTHIC_SHOP));
        AssignButtonEvent("Button_5", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_DARK_WORLD_GOLD_MYTHIC_SHOP));
        AssignButtonEvent("Button_6", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_DARK_WORLD_DIAMOND_MYTHIC_SHOP));
        AssignButtonEvent("Button_7", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_DARK_WORLD_SOLAR_MYTHIC_SHOP));
    }
    public async Task CreateAlliteriaLightWorldShopPanelAsync()
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
        CreateButton(1, AppDisplayConstants.Title.Shop.ALLITERIA_LIGHT_WORLD_BRONZE_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_LIGHT_WORLD_BRONZE_MYTHIC_URL), tempContent);
        CreateButton(2, AppDisplayConstants.Title.Shop.ALLITERIA_LIGHT_WORLD_SILVER_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_LIGHT_WORLD_SILVER_MYTHIC_URL), tempContent);
        CreateButton(3, AppDisplayConstants.Title.Shop.ALLITERIA_LIGHT_WORLD_EMERALD_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_LIGHT_WORLD_EMERALD_MYTHIC_URL), tempContent);
        CreateButton(4, AppDisplayConstants.Title.Shop.ALLITERIA_LIGHT_WORLD_AMETHYST_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_LIGHT_WORLD_AMETHYST_MYTHIC_URL), tempContent);
        CreateButton(5, AppDisplayConstants.Title.Shop.ALLITERIA_LIGHT_WORLD_GOLD_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_LIGHT_WORLD_GOLD_MYTHIC_URL), tempContent);
        CreateButton(6, AppDisplayConstants.Title.Shop.ALLITERIA_LIGHT_WORLD_DIAMOND_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_LIGHT_WORLD_DIAMOND_MYTHIC_URL), tempContent);
        CreateButton(7, AppDisplayConstants.Title.Shop.ALLITERIA_LIGHT_WORLD_SOLAR_MYTHIC_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.World.ALLITERIA_LIGHT_WORLD_SOLAR_MYTHIC_URL), tempContent);

        CreateLightWorldShopButtonEvent(tempContent);
    }
    public void CreateLightWorldShopButtonEvent(Transform panel)
    {
        AssignButtonEvent("Button_1", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_LIGHT_WORLD_BRONZE_MYTHIC_SHOP));
        AssignButtonEvent("Button_2", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_LIGHT_WORLD_SILVER_MYTHIC_SHOP));
        AssignButtonEvent("Button_3", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_LIGHT_WORLD_EMERALD_MYTHIC_SHOP));
        AssignButtonEvent("Button_4", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_LIGHT_WORLD_AMETHYST_MYTHIC_SHOP));
        AssignButtonEvent("Button_5", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_LIGHT_WORLD_GOLD_MYTHIC_SHOP));
        AssignButtonEvent("Button_6", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_LIGHT_WORLD_DIAMOND_MYTHIC_SHOP));
        AssignButtonEvent("Button_7", panel, async () => await ItemsController.Instance.CreateShopAsync(AppConstants.Shop.ShopCodeName.ALLITERIA_LIGHT_WORLD_SOLAR_MYTHIC_SHOP));
    }
}
