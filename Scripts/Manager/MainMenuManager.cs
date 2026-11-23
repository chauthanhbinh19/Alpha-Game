using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using System.Threading.Tasks;

public class MainMenuManager : MonoBehaviour
{
    private Transform RootPanel;
    private GameObject MainPanelPrefab;
    private GameObject DictionaryPanelPrefab;
    private GameObject PopupMenuPanelPrefab;
    private GameObject ArenaPanelPrefab;
    private GameObject MasterBoardPanelPrefab;
    private GameObject PopupButtonPanelPrefab;
    private GameObject MainMenuButtonPrefab;
    private Transform MainPanel;
    private Transform DictionaryContentPanel;
    private GameObject CurrentMainMenuObject;
    private GameObject MainTabButtonPrefab;
    private Button CloseButton;
    private Button HomeButton;
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
    private PaginationManager PaginationManager;

    private Transform PositionPanel;
    private GameObject CurrentObject;
    //Variable for pagination
    private int Offset = 0;
    private int CurrentPage = 1;
    private int TotalItems;
    private const int PAGE_SIZE = 100;
    private string MainType;
    private TextMeshProUGUI TitleText;
    private string Search = "";
    private string Type = AppConstants.Type.ALL;
    private string Rare = AppConstants.Rare.ALL;
    private bool IsSearchingOrFiltering = false;
    private Transform contentParent;
    private readonly List<string> categories = new List<string>
    {
        "PLAY", "SHOP", "INVENTORY", "BUILD", "MISSION", "SOCIAL"
    };
    private readonly List<(GameObject defaultObj, GameObject selectedObj)> tabUIList = new List<(GameObject, GameObject)>();
    public static MainMenuManager Instance { get; private set; }
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
        RootPanel = UIManager.Instance.GetTransform(AppConstants.Transform.ROOT_PANEL);
        MainPanelPrefab = UIManager.Instance.Get(PrefabConstants.General.MAIN_PANEL_PREFAB);
        PopupButtonPanelPrefab = UIManager.Instance.Get(PrefabConstants.General.POPUP_BUTTON_PANEL_PREFAB);
        // mainMenuCampaignPanel = UIManager.Instance.GetTransform("mainMenuCampaignPanel");
        DictionaryPanelPrefab = UIManager.Instance.Get(PrefabConstants.General.DICTIONARY_PANEL_PREFAB);
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        PopupMenuPanelPrefab = UIManager.Instance.Get(PrefabConstants.General.POPUP_MENU_PANEL_PREFAB);
        ArenaPanelPrefab = UIManager.Instance.Get(PrefabConstants.Arena.ARENA_PANEL_PREFAB);
        MasterBoardPanelPrefab = UIManager.Instance.Get(PrefabConstants.General.MASTER_BOARD_PANEL_PREFAB);
        MainTabButtonPrefab = UIManager.Instance.Get(PrefabConstants.Component.MAIN_TAB_BUTTON_PREFAB);
        MainMenuButtonPrefab = UIManager.Instance.Get(PrefabConstants.Component.MAIN_MENU_BUTTON_PREFAB);
    }
    public void CreateMainPanel()
    {
        CurrentObject = Instantiate(MainPanelPrefab, RootPanel);
        Transform transform = CurrentObject.transform;
        // ButtonLoader.Instance.CreateMainButton(currentObject);
        // GetMainButtonEvent();

        Button startButton = transform.Find("MainNavigation/StartButton").GetComponent<Button>();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            BattleSpawnController battleSpawnController = FindObjectOfType<BattleSpawnController>();
            if (battleSpawnController != null)
            {
                battleSpawnController.StartBattle("639186151469501545", "639186151625575788");
            }
            else
            {
                Debug.LogWarning("[MainMenuManager] BattleSpawnController not found in scene.");
            }
        });

        Transform content = transform.Find("MainPanel/MainButtonGroup/SecondCircleImage");
        Transform contentTransform = transform.Find("MainNavigation/Scroll View/Viewport/Content");
        Transform tabTransform = transform.Find("MainNavigation/Tab Scroll View/Viewport/Content");
        contentParent = transform.Find("MainNavigation/Scroll View/Viewport/Content");
        // Button homeButton = transform.Find("MainNavigation/Scroll View/Viewport/Content/HomeButton").GetComponent<Button>();


        // Button profileButton = transform.Find("MainNavigation/Scroll View/Viewport/Content/UserButton").GetComponent<Button>();    

        Button leaderboardButton = transform.Find("Sidebar/Content/LeaderboardButton").GetComponent<Button>();
        Button mailButton = transform.Find("Sidebar/Content/MailButton").GetComponent<Button>();

        CreatePlayPanel(transform);
        CreateInventoryPanel(transform);
        CreateShopPanel(transform);
        CreateBuildPanel(transform);
        CreateMissionPanel(transform);
        CreateSocialPanel(transform);

        leaderboardButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await LeaderboardManager.Instance.CreateLeaderboardPanel();
        });

        for (int i = 0; i < categories.Count; i++)
        {
            string category = categories[i];

            // 1. Tạo Nút Tab
            GameObject tabBtnObj = Instantiate(MainTabButtonPrefab, tabTransform);

            // Cache Transform & UI Components
            Transform btnTransform = tabBtnObj.transform;
            GameObject defaultObj = btnTransform.Find("Default").gameObject;
            GameObject selectedObj = btnTransform.Find("Selected").gameObject;

            defaultObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = category;
            selectedObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = category;

            // Thiết lập trạng thái ban đầu (Tab 0 mở mặc định)
            bool isFirst = (i == 0);
            defaultObj.SetActive(!isFirst);
            selectedObj.SetActive(isFirst);

            tabUIList.Add((defaultObj, selectedObj));

            // Cache biến cục bộ riêng cho Lambda Event
            int tabIndex = i;

            // 2. Đăng ký sự kiện Click
            Button tabBtn = tabBtnObj.GetComponent<Button>();
            tabBtn.onClick.AddListener(() =>
            {
                OnTabClicked(tabIndex, category);
            });
        }

        // Bật Content cho Tab đầu tiên lúc vừa vào Game
        if (categories.Count > 0)
        {
            SwitchContentUI(categories[0]);
        }
    }
    public void CreatePlayPanel(Transform transform)
    {
        Transform playTransform = transform.Find("MainNavigation/Scroll View/Viewport/Content/PlayContent");
        
        CreateMainMenuButtonUI(1, AppDisplayConstants.Title.Play.EVENT, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.EVENT_URL), playTransform);
        CreateMainMenuButtonUI(2, AppDisplayConstants.Title.Play.ALLITERIA_CAMPAIGN_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_CAMPAIGN_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(3, AppDisplayConstants.Title.Play.ARENA, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ARENA_URL), playTransform);
        CreateMainMenuButtonUI(4, AppDisplayConstants.Title.Play.GACHA, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.GACHA_URL), playTransform);
        CreateMainMenuButtonUI(5, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_1_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_1_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(6, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_2_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_2_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(7, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_3_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_3_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(8, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_4_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_4_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(9, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_5_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_5_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(10, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_6_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_6_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(11, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_7_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_7_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(12, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_8_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_8_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(13, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_9_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_9_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(14, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_10_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_10_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(15, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_11_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_11_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(16, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_12_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_12_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(17, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_13_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_13_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(18, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_14_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_14_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(19, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_15_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_15_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(20, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_16_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_16_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(21, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_17_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_17_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(22, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_18_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_18_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(23, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_19_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_19_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(24, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_20_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_20_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(25, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_21_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_21_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(26, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_22_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_22_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(27, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_23_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_23_WORLD_URL), playTransform);
        CreateMainMenuButtonUI(28, AppDisplayConstants.Title.Play.ALLITERIA_PATH_NUMBER_24_WORLD, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_24_WORLD_URL), playTransform);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", playTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            GameObject popupButtonPanel = Instantiate(PopupButtonPanelPrefab, MainPanel);
            CloseButton = popupButtonPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupButtonPanel);
            });
            HomeButton = popupButtonPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
            });
            TextMeshProUGUI titleText = popupButtonPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.EVENT);
            ButtonLoader.Instance.CreateEventButton(popupButtonPanel);
            GetButtonEvent(popupButtonPanel);
            SummonManager.Instance.GetButtonEvent(popupButtonPanel);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", playTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            // await ShopManager.Instance.CreateShopButtonAsync(MainPanel);
            await ShopManager.Instance.CreateAlliteriaCampaignWorldShopPanelAsync();
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", playTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            TitleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
            });
            await ArenaManager.Instance.CreateArenaButtonAsync(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", playTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupButtonPanel = Instantiate(PopupButtonPanelPrefab, MainPanel);
            CloseButton = popupButtonPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupButtonPanel);
            });
            HomeButton = popupButtonPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
            });
            TextMeshProUGUI titleText = popupButtonPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.GACHA);
            GachaManager.Instance.CreateGachaButton(popupButtonPanel.transform.Find("Scroll View/Viewport/Content"));
            GachaManager.Instance.CreateGacha(popupButtonPanel.transform.Find("Scroll View/Viewport/Content"));
        });
    }
    public void CreateInventoryPanel(Transform transform)
    {
        Transform inventoryTransform = transform.Find("MainNavigation/Scroll View/Viewport/Content/InventoryContent");

        CreateMainMenuButtonUI(1, AppDisplayConstants.Title.Inventory.GENERAL_INVENTORY, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_24_WORLD_URL), inventoryTransform);
        CreateMainMenuButtonUI(2, AppDisplayConstants.Title.Inventory.ITEM_INVENTORY, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_24_WORLD_URL), inventoryTransform);
        CreateMainMenuButtonUI(3, AppDisplayConstants.Title.Inventory.MILITARY, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_24_WORLD_URL), inventoryTransform);
        CreateMainMenuButtonUI(4, AppDisplayConstants.Title.Inventory.DEPLOYMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.ALLITERIA_PATH_NUMBER_24_WORLD_URL), inventoryTransform);

        // 1. General Inventory
        ButtonEvent.Instance.AssignButtonEvent("Button_1", inventoryTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            GameObject popupButtonPanel = Instantiate(PopupButtonPanelPrefab, MainPanel);
            CloseButton = popupButtonPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupButtonPanel);
            });
            HomeButton = popupButtonPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
            });
            TextMeshProUGUI titleText = popupButtonPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.INVENTORY);
            ButtonLoader.Instance.CreateInventoryButton(popupButtonPanel);
            GetMainButtonEvent(popupButtonPanel);
        });

        // 2. Item Inventory
        ButtonEvent.Instance.AssignButtonEvent("Button_2", inventoryTransform, async () =>
        {
            await GetTypeAsync(AppConstants.MainType.ITEM);
        });
    }
    public void CreateShopPanel(Transform transform)
    {
        Transform shopTransform = transform.Find("MainNavigation/Scroll View/Viewport/Content/ShopContent");

        CreateMainMenuButtonUI(1, AppDisplayConstants.Title.Shop.GENERAL_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.GENERAL_SHOP_URL), shopTransform);
        CreateMainMenuButtonUI(2, AppDisplayConstants.Title.Shop.ALLITERIA_CAMPAIGN_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_CAMPAIGN_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(3, AppDisplayConstants.Title.Shop.ALLITERIA_DARK_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_DARK_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(4, AppDisplayConstants.Title.Shop.ALLITERIA_LIGHT_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_LIGHT_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(5, AppDisplayConstants.Title.Play.GACHA, TextureHelper.LoadTexture2DCached(ImageConstants.Campaign.GACHA_URL), shopTransform);
        CreateMainMenuButtonUI(6, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_1_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_1_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(7, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_2_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_2_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(8, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_3_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_3_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(9, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_4_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_4_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(10, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_5_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_5_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(11, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_6_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_6_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(12, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_7_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_7_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(13, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_8_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_8_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(14, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_9_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_9_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(15, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_10_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_10_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(16, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_11_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_11_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(17, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_12_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_12_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(18, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_13_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_13_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(19, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_14_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_14_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(20, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_15_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_15_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(21, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_16_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_16_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(22, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_17_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_17_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(23, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_18_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_18_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(24, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_19_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_19_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(25, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_20_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_20_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(26, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_21_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_21_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(27, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_22_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_22_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(28, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_23_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_23_WORLD_URL), shopTransform);
        CreateMainMenuButtonUI(29, AppDisplayConstants.Title.Shop.ALLITERIA_PATH_NUMBER_24_WORLD_SHOP, TextureHelper.LoadTexture2DCached(ImageConstants.Shop.ALLITERIA_PATH_NUMBER_24_WORLD_URL), shopTransform);

        // 1. General Shop
        ButtonEvent.Instance.AssignButtonEvent("Button_1", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            // await ShopManager.Instance.CreateShopButtonAsync(MainPanel);
            await ShopManager.Instance.CreateShopPanelAsync();
        });

        // 2. Alliteria Campaign World Shop (lấy từ CreatePlayPanel - Button_2)
        ButtonEvent.Instance.AssignButtonEvent("Button_2", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            // await ShopManager.Instance.CreateShopButtonAsync(MainPanel);
            await ShopManager.Instance.CreateAlliteriaCampaignWorldShopPanelAsync();
        });

        // 3. Alliteria Dark World Shop
        ButtonEvent.Instance.AssignButtonEvent("Button_3", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            // await ShopManager.Instance.CreateShopButtonAsync(MainPanel);
            await ShopManager.Instance.CreateAlliteriaDarkWorldShopPanelAsync();
        });

        // 4. Alliteria Light World Shop
        ButtonEvent.Instance.AssignButtonEvent("Button_4", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            // await ShopManager.Instance.CreateShopButtonAsync(MainPanel);
            await ShopManager.Instance.CreateAlliteriaLightWorldShopPanelAsync();
        });

        // 5. Shop Package
        ButtonEvent.Instance.AssignButtonEvent("Button_5", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await PaymentController.Instance.CreateShopPackageAsync();
        });

        // 6 -> 29: Alliteria Path Number 1 -> 24 World Shop
        ButtonEvent.Instance.AssignButtonEvent("Button_6", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber1WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_7", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber2WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_8", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber3WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_9", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber4WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_10", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber5WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_11", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber6WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_12", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber7WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_13", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber8WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_14", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber9WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_15", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber10WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_16", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber11WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_17", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber12WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_18", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber13WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_19", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber14WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_20", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber15WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_21", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber16WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_22", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber17WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_23", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber18WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_24", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber19WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_25", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber20WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_26", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber21WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_27", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber22WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_28", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber23WorldShopPanelAsync();
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_29", shopTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateAlliteriaPathNumber24WorldShopPanelAsync();
        });
    }
    public void CreateBuildPanel(Transform transform)
    {
        Transform buildTransform = transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent");

        CreateMainMenuButtonUI(1, AppDisplayConstants.Title.Build.TEAM, TextureHelper.LoadTexture2DCached(ImageConstants.Build.TEAM_URL), buildTransform);
        CreateMainMenuButtonUI(2, AppDisplayConstants.Title.Build.MASTER_BOARD, TextureHelper.LoadTexture2DCached(ImageConstants.Build.MASTER_BOARD_URL), buildTransform);
        CreateMainMenuButtonUI(3, AppDisplayConstants.Title.Build.SCIENCE_FICTION, TextureHelper.LoadTexture2DCached(ImageConstants.Build.SCIENCE_FICTION_URL), buildTransform);
        CreateMainMenuButtonUI(4, AppDisplayConstants.Title.Build.GALLERY, TextureHelper.LoadTexture2DCached(ImageConstants.Build.GALLERY_URL), buildTransform);
        CreateMainMenuButtonUI(5, AppDisplayConstants.Title.Build.COLLECTION, TextureHelper.LoadTexture2DCached(ImageConstants.Build.COLLECTION_URL), buildTransform);
        CreateMainMenuButtonUI(6, AppDisplayConstants.Title.Build.EQUIPMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Build.EQUIPMENT_URL), buildTransform);
        CreateMainMenuButtonUI(7, AppDisplayConstants.Title.Build.FEATURE, TextureHelper.LoadTexture2DCached(ImageConstants.Build.FEATURE_URL), buildTransform);
        CreateMainMenuButtonUI(8, AppDisplayConstants.Title.Build.RESEARCH, TextureHelper.LoadTexture2DCached(ImageConstants.Build.RESEARCH_URL), buildTransform);
        CreateMainMenuButtonUI(9, AppDisplayConstants.Title.Build.ARCHIVE, TextureHelper.LoadTexture2DCached(ImageConstants.Build.ARCHIVE_URL), buildTransform);
        CreateMainMenuButtonUI(10, AppDisplayConstants.Title.Build.UNIVERSE, TextureHelper.LoadTexture2DCached(ImageConstants.Build.UNIVERSE_URL), buildTransform);
        CreateMainMenuButtonUI(11, AppDisplayConstants.Title.Build.STRUCTURE, TextureHelper.LoadTexture2DCached(ImageConstants.Build.STRUCTURE_URL), buildTransform);
        CreateMainMenuButtonUI(12, AppDisplayConstants.Title.Build.ANIME, TextureHelper.LoadTexture2DCached(ImageConstants.Build.ANIME_URL), buildTransform);
        CreateMainMenuButtonUI(13, AppDisplayConstants.Title.Build.PATTERN, TextureHelper.LoadTexture2DCached(ImageConstants.Build.PATTERN_URL), buildTransform);
        CreateMainMenuButtonUI(14, AppDisplayConstants.Title.Build.COSMIC_IMPRINT, TextureHelper.LoadTexture2DCached(ImageConstants.Build.COSMIC_IMPRINT_URL), buildTransform);
        CreateMainMenuButtonUI(15, AppDisplayConstants.Title.Build.PRIMORDIAL_IMPRINT, TextureHelper.LoadTexture2DCached(ImageConstants.Build.PRIMORDIAL_IMPRINT_URL), buildTransform);
        CreateMainMenuButtonUI(16, AppDisplayConstants.Title.Build.PATH_OF_LORD_IMPRINT, TextureHelper.LoadTexture2DCached(ImageConstants.Build.PATH_OF_LORD_IMPRINT_URL), buildTransform);

        // 1. Team
        ButtonEvent.Instance.AssignButtonEvent("Button_1", buildTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await TeamsManager.Instance.CreateTeamsAsync();
        });

        // 2. Master Board
        ButtonEvent.Instance.AssignButtonEvent("Button_2", buildTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            GameObject popupObject = Instantiate(MasterBoardPanelPrefab, MainPanel);
            TextMeshProUGUI titleTMPText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
            });
            // FindObjectOfType<ButtonLoader>().CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
            await MasterBoardController.Instance.CreateMasterBoardAsync(popupObject);
        });

        // 3. Science Fiction
        ButtonEvent.Instance.AssignButtonEvent("Button_3", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            ScienceFictionManager.Instance.CreateScienceFiction();
        });

        // 4. Gallery
        ButtonEvent.Instance.AssignButtonEvent("Button_4", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupButtonPanel = Instantiate(PopupButtonPanelPrefab, MainPanel);
            CloseButton = popupButtonPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupButtonPanel);
            });
            HomeButton = popupButtonPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
            });
            TextMeshProUGUI titleText = popupButtonPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.GALLERY);
            Transform popupTransform = popupButtonPanel.transform.Find("Scroll View/Viewport/Content");
            GalleryManager.Instance.CreateGalleryButton(popupTransform);
            GalleryManager.Instance.CreateGallery(popupTransform);

            GridLayoutGroup gridLayout = popupTransform.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(400, 200);
            }
        });

        // 5. Collection
        ButtonEvent.Instance.AssignButtonEvent("Button_5", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupButtonPanel = Instantiate(PopupButtonPanelPrefab, MainPanel);
            CloseButton = popupButtonPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupButtonPanel);
            });
            HomeButton = popupButtonPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
            });
            TextMeshProUGUI titleText = popupButtonPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.COLLECTION);
            Transform popupTransform = popupButtonPanel.transform.Find("Scroll View/Viewport/Content");
            CollectionManager.Instance.CreateCollectionButton(popupTransform);
            CollectionManager.Instance.CreateCollection(popupTransform);

            GridLayoutGroup gridLayout = popupTransform.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(400, 200);
            }
        });

        // 6. Equipment
        ButtonEvent.Instance.AssignButtonEvent("Button_6", buildTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.EQUIPMENTS);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupObject);
            });
            await EquipmentManager.Instance.CreateEquipmentsButtonAsync(popupObject.transform.Find("Content"));
            Transform scrollViewPanel = popupObject.transform.Find("Scroll View");
            scrollViewPanel.gameObject.SetActive(false);
        });

        // 7. Feature
        ButtonEvent.Instance.AssignButtonEvent("Button_7", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            FeatureManager.Instance.CreateFeature();
        });

        // 8. Research
        ButtonEvent.Instance.AssignButtonEvent("Button_8", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            ResearchManager.Instance.CreateResearch();
        });

        // 9. Archive
        ButtonEvent.Instance.AssignButtonEvent("Button_9", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            ArchiveManager.Instance.CreateArchive();
        });

        // 10. Universe
        ButtonEvent.Instance.AssignButtonEvent("Button_10", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            UniverseManager.Instance.CreateUniverse();
        });

        // 11. Structure
        ButtonEvent.Instance.AssignButtonEvent("Button_11", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            StructureManager.Instance.CreateStructure();
        });

        // 12. Anime
        ButtonEvent.Instance.AssignButtonEvent("Button_12", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            AnimeManager.Instance.CreateAnime();
        });

        // 13. Pattern
        ButtonEvent.Instance.AssignButtonEvent("Button_13", buildTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await PatternController.Instance.CreatePatternPanel();
        });

        // 14. Cosmic Imprint
        ButtonEvent.Instance.AssignButtonEvent("Button_14", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            CosmicImprintManager.Instance.CreateCosmicImprint();
        });

        // 15. Primordial Imprint
        ButtonEvent.Instance.AssignButtonEvent("Button_15", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            PrimordialImprintManager.Instance.CreatePrimordialImprint();
        });

        // 16. Path Of Lord Imprint
        ButtonEvent.Instance.AssignButtonEvent("Button_16", buildTransform, () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            PathOfLordImprintManager.Instance.CreatePathOfLordImprint();
        });
    }
    public void CreateMissionPanel(Transform transform)
    {
        Transform missionTransform = transform.Find("MainNavigation/Scroll View/Viewport/Content/MissionContent");

        CreateMainMenuButtonUI(1, AppDisplayConstants.Title.Mission.MISSION, TextureHelper.LoadTexture2DCached(ImageConstants.Mission.MISSION_URL), missionTransform);

    }
    public void CreateSocialPanel(Transform transform)
    {
        Transform socialTransform = transform.Find("MainNavigation/Scroll View/Viewport/Content/SocialContent");

        CreateMainMenuButtonUI(1, AppDisplayConstants.Title.Social.GUILD, TextureHelper.LoadTexture2DCached(ImageConstants.Social.GUILD_URL), socialTransform);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", socialTransform, async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
    }
    private void CreateMainMenuButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(MainMenuButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        // RawImage image = transform.Find("Image").GetComponent<RawImage>();
        // if (image != null && _itemImage != null)
        // {
        //     image.texture = _itemImage;
        // }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        // RawImage borderImage = transform.Find("BorderCircleImage").GetComponent<RawImage>();
        // if (borderImage != null)
        // {
        //     borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_BORDER_URL);
        // }

        RawImage backgroundImage = transform.Find("Decoration1/Background3").GetComponent<RawImage>();
        if (backgroundImage != null)
        {
            backgroundImage.texture = TextureHelper.LoadTexture2DCached("UI/Background4/Button/Button_Theme_Background_1");
        }

        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        if (iconImage != null)
        {
            iconImage.texture = _itemImage;
        }
    }
    private void OnTabClicked(int selectedIndex, string category)
    {
        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND_2);

        // A. Cập nhật UI Nút Tab (Bật/Tắt Default & Selected)
        for (int i = 0; i < tabUIList.Count; i++)
        {
            bool isSelected = (i == selectedIndex);
            tabUIList[i].defaultObj.SetActive(!isSelected);
            tabUIList[i].selectedObj.SetActive(isSelected);
        }

        // B. Cập nhật UI Content (Bật/Tắt Title & Content tương ứng)
        SwitchContentUI(category);
    }

    // Kết hợp từ câu hỏi trước: Tự động Bật/Tắt Content theo tên Category
    private void SwitchContentUI(string activeCategory)
    {
        if (contentParent == null) return;

        foreach (string category in categories)
        {
            bool isActive = category.Equals(activeCategory, System.StringComparison.OrdinalIgnoreCase);

            // Chuyển chuỗi chữ hoa (VD: "PLAY" -> "Play")
            string formattedName = char.ToUpper(category[0]) + category.Substring(1).ToLower();

            // Tìm Title & Content theo format tên GameObject
            Transform title = contentParent.Find($"Main{formattedName}Title");
            Transform content = contentParent.Find($"{formattedName}Content");

            if (title != null) title.gameObject.SetActive(isActive);
            if (content != null) content.gameObject.SetActive(isActive);
        }
    }
    public void CreateMainPanelUserInformation(AuthResult authResult)
    {
        Transform transform = CurrentObject.transform;
        // Transform userPanel = currentObject.transform.Find("User");
        // Button userButton = userPanel.GetComponent<Button>();
        RawImage avatarImage = transform.Find("Header/AvatarImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(authResult.User.Image);
        Texture avatarTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        avatarImage.texture = avatarTexture;

        RawImage borderImage = transform.Find("Header/BorderImage").GetComponent<RawImage>();
        fileNameWithoutExtension = ImageHelper.RemoveImageExtension(authResult.User.Border);
        Texture borderTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        borderImage.texture = borderTexture;

        // RawImage userAvatarImage = transform.Find("MainNavigation/Scroll View/Viewport/Content/UserButton/AvatarImage").GetComponent<RawImage>();
        // fileNameWithoutExtension = ImageHelper.RemoveImageExtension(authResult.User.Image);
        // Texture TuserAvatarexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        // userAvatarImage.texture = TuserAvatarexture;

        // RawImage userBorderImage = transform.Find("MainNavigation/Scroll View/Viewport/Content/UserButton/BorderImage").GetComponent<RawImage>();
        // fileNameWithoutExtension = ImageHelper.RemoveImageExtension(authResult.User.Border);
        // Texture userBorderTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        // userBorderImage.texture = userBorderTexture;

        // FindObjectOfType<CurrenciesManager>().GetMainCurrency(authResult.User.Currencies, currencyPanel);

        // userButton.onClick.AddListener(async () =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     await ProfileManager.Instance.CreateProfileAsync();
        // });

        // TextMeshProUGUI userNameText = transform.Find("MainNavigation/Scroll View/Viewport/Content/UserButton/NameText").GetComponent<TextMeshProUGUI>();
        // userNameText.text = authResult.User.Name;

        TextMeshProUGUI nameText = transform.Find("Header/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = authResult.User.Name;
        // TextMeshProUGUI levelText = transform.Find("Header/LevelText").GetComponent<TextMeshProUGUI>();
        // levelText.text = authResult.User.Level.ToString();
        TextMeshProUGUI powerText = transform.Find("Header/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = authResult.User.Power.ToString();

        // var gold = authResult.User.Currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.GOLD);
        // var silver = authResult.User.Currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.SILVER);
        // var diamond = authResult.User.Currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.DIAMOND);

        // RawImage goldImage = transform.Find("Header/GoldCurrency/Image").GetComponent<RawImage>();
        // RawImage silverImage = transform.Find("Header/SilverCurrency/Image").GetComponent<RawImage>();
        // RawImage diamondImage = transform.Find("Header/DiamondCurrency/Image").GetComponent<RawImage>();

        // goldImage.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(gold.Image));
        // silverImage.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(silver.Image));
        // diamondImage.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(diamond.Image));

        // TextMeshProUGUI goldText = transform.Find("Header/GoldCurrency/TitleText").GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI silverText = transform.Find("Header/SilverCurrency/TitleText").GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI diamondText = transform.Find("Header/DiamondCurrency/TitleText").GetComponent<TextMeshProUGUI>();

        // goldText.text = gold.Quantity.ToString();
        // silverText.text = silver.Quantity.ToString();
        // diamondText.text = diamond.Quantity.ToString();
    }
    public void GetMainButtonEvent(GameObject popupButtonObject)
    {
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");
        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_HERO));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.BOOK));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.PET));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_CAPTAIN));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_COLONEL));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_GENERAL));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_ADMIRAL));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_MILITARY));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_SPELL));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_MONSTER));
        // Button_13 Equipments có thể được thêm lại nếu cần
        // ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.ITEM));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.COLLABORATION_EQUIPMENT));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.COLLABORATION));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.MEDAL));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.SKILL));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.SYMBOL));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.TITLE));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.MAGIC_FORMATION_CIRCLE));
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.RELIC));
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.TALISMAN));
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.PUPPET));
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.ALCHEMY));
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.FORGE));
        ButtonEvent.Instance.AssignButtonEvent("Button_24", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_LIFE));
        ButtonEvent.Instance.AssignButtonEvent("Button_25", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.ARTWORK));
        ButtonEvent.Instance.AssignButtonEvent("Button_26", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.SPIRIT_BEAST));
        ButtonEvent.Instance.AssignButtonEvent("Button_27", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.SPIRIT_CARD));
        ButtonEvent.Instance.AssignButtonEvent("Button_28", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.ARTIFACT));
        ButtonEvent.Instance.AssignButtonEvent("Button_29", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.ARCHITECTURE));
        ButtonEvent.Instance.AssignButtonEvent("Button_30", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.TECHNOLOGY));
        ButtonEvent.Instance.AssignButtonEvent("Button_31", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.VEHICLE));
        ButtonEvent.Instance.AssignButtonEvent("Button_32", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CORE));
        ButtonEvent.Instance.AssignButtonEvent("Button_33", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.WEAPON));
        ButtonEvent.Instance.AssignButtonEvent("Button_34", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.ROBOT));
        ButtonEvent.Instance.AssignButtonEvent("Button_35", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.BADGE));
        ButtonEvent.Instance.AssignButtonEvent("Button_36", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.MECHA_BEAST));
        ButtonEvent.Instance.AssignButtonEvent("Button_37", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.RUNE));
        ButtonEvent.Instance.AssignButtonEvent("Button_38", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.FURNITURE));
        ButtonEvent.Instance.AssignButtonEvent("Button_39", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.FOOD));
        ButtonEvent.Instance.AssignButtonEvent("Button_40", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.BEVERAGE));
        ButtonEvent.Instance.AssignButtonEvent("Button_41", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.BUILDING));
        ButtonEvent.Instance.AssignButtonEvent("Button_42", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.PLANT));
        ButtonEvent.Instance.AssignButtonEvent("Button_43", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.FASHION));
        ButtonEvent.Instance.AssignButtonEvent("Button_44", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.EMOJI));
        ButtonEvent.Instance.AssignButtonEvent("Button_45", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CARD_SOLDIER));
        ButtonEvent.Instance.AssignButtonEvent("Button_46", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.OUTFIT));
        ButtonEvent.Instance.AssignButtonEvent("Button_47", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.ACHIEVEMENT));
        ButtonEvent.Instance.AssignButtonEvent("Button_48", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.AVATAR));
        ButtonEvent.Instance.AssignButtonEvent("Button_49", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.BORDER));
    }
    public void GetButtonEvent(GameObject popupButtonObject)
    {
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");
        // ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, () => GetType(AppConstants.MainType.ANIME));
        // ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, () => GetType(AppConstants.MainType.GUILD));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.TOWER));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.EVENT));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.DAILY_CHECKIN));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () => await GetTypeAsync(AppConstants.Market.RARE_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () => await GetTypeAsync(AppConstants.Market.ULTRA_RARE_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () => await GetTypeAsync(AppConstants.Market.LEGENDARY_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () => await GetTypeAsync(AppConstants.Market.MYSTIC_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () => await GetTypeAsync(AppConstants.MainType.CHIP));
    }
    public async Task GetTypeAsync(string type)
    {
        MainType = type; // Gán giá trị cho mainType

        // 1. Dọn dẹp Panel cũ nếu đã tồn tại trước khi Instantiate cái mới
        if (CurrentMainMenuObject != null)
        {
            Destroy(CurrentMainMenuObject);
            CurrentMainMenuObject = null;
        }

        // 2. Chờ khởi tạo xong hoàn toàn UI mới
        await CreateMainMenuManagerAsync();

        if (TitleText != null)
        {
            TitleText.text = LocalizationManager.Get(type);
        }
    }
    public async Task CreateMainMenuManagerAsync()
    {
        // MainMenuPanel.SetActive(true)
        if (MainType.Equals(AppConstants.MainType.TOWER))
        {
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            TitleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            // CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // CloseButton.onClick.AddListener(() =>
            // {
            //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            //     Destroy(popupObject);
            // });
            // HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            // HomeButton.onClick.AddListener(() =>
            // {
            //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            //     Close(MainPanel);
            // });
            ButtonLoader.Instance.CreateTowerButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (MainType.Equals(AppConstants.MainType.DAILY_CHECKIN))
        {
            await DailyCheckinManager.Instance.CreateDailyCheckinGroupAsync();
        }
        else if (MainType.Equals(AppConstants.Market.RARE_MARKET))
        {
            await RareMarketManager.Instance.CreateRareMarketAsync(MainPanel);
        }
        else if (MainType.Equals(AppConstants.Market.ULTRA_RARE_MARKET))
        {
            await UltraRareMarketManager.Instance.CreateUltraRareMarketAsync(MainPanel);
        }
        else if (MainType.Equals(AppConstants.Market.LEGENDARY_MARKET))
        {
            await LegendaryMarketManager.Instance.CreateLegendaryMarketAsync(MainPanel);
        }
        else if (MainType.Equals(AppConstants.Market.MYSTIC_MARKET))
        {
            await MysticMarketManager.Instance.CreateMysticMarketAsync(MainPanel);
        }
        else if (MainType.Equals(AppConstants.MainType.CHIP))
        {
            // await MysticMarketManager.Instance.CreateMysticMarketAsync();
        }
        else
        {
            CurrentMainMenuObject = Instantiate(DictionaryPanelPrefab, MainPanel);
            Transform transform = CurrentMainMenuObject.transform;
            DictionaryContentPanel = transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            RightScrollViewContentPanel = transform.Find("RightScrollView/Viewport/Content");
            LeftScrollViewContentPanel = transform.Find("Scroll View/Viewport/ButtonContent");
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
                Destroy(CurrentMainMenuObject);
            });
            HomeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);

            });

            RawImage inventoryBackgroundImage = transform.Find("DictionaryCards/TitleGroup/TopBackground/InventoryBackground").GetComponent<RawImage>();
            RawImage collectionBackgroundImage = transform.Find("DictionaryCards/TitleGroup/TopBackground/CollectionBackground").GetComponent<RawImage>();
            RawImage galleryBackgroundImage = transform.Find("DictionaryCards/TitleGroup/TopBackground/GalleryBackground").GetComponent<RawImage>();
            inventoryBackgroundImage.gameObject.SetActive(true);
            collectionBackgroundImage.gameObject.SetActive(false);
            galleryBackgroundImage.gameObject.SetActive(false);
            TextMeshProUGUI subTitleText = transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
            subTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.INVENTORY);

            // CurrencyPanel = transform.Find("DictionaryCards/Currency");
            PaginationManager = transform.Find("PaginationPanelPrefab").GetComponent<PaginationManager>();


            // List<Currencies> currencies = new List<Currencies>();
            // currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
            // FindObjectOfType<CurrenciesManager>().GetMainCurrency(currencies, CurrencyPanel);
            searchButton.onClick.AddListener(() =>
            {
                Offset = 0;
                CurrentPage = 1;
                string searchText = searchInputField.text;
                Search = searchText;
                IsSearchingOrFiltering = true;
                _ = LoadCurrentPageAsync();
            });

            List<string> uniqueRaries = QualityEvaluatorHelper.rarities;
            if (uniqueRaries.Count > 0)
            {
                rareDropdown.ClearOptions();
                rareDropdown.AddOptions(uniqueRaries);

                // Quan trọng: clear listener cũ trước
                rareDropdown.onValueChanged.RemoveAllListeners();

                // Gán sự kiện
                rareDropdown.onValueChanged.AddListener((index) =>
                {
                    Offset = 0;
                    CurrentPage = 1;
                    // Lấy text đang chọn
                    string selectedRare = rareDropdown.options[index].text;
                    Rare = selectedRare;
                    IsSearchingOrFiltering = true;
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

                // Quan trọng: clear listener cũ trước
                typeDropdown.onValueChanged.RemoveAllListeners();

                // Gán sự kiện
                typeDropdown.onValueChanged.AddListener((index) =>
                {
                    Offset = 0;
                    CurrentPage = 1;
                    // Lấy text đang chọn
                    string selectedType = typeDropdown.options[index].text;
                    Type = selectedType;
                    IsSearchingOrFiltering = true;
                    // Gọi async (fire & forget an toàn)
                    _ = LoadCurrentPageAsync();
                });

                typeDropdown.value = 0;
                typeDropdown.RefreshShownValue();
            }
            IsSearchingOrFiltering = true;
            await LoadCurrentPageAsync();
        }
        LoadAnimation();
    }
    public async Task LoadCurrentPageAsync()
    {
        int totalRecord = 0;
        int listCount = 0;
        // offset = 0;
        ButtonEvent.Instance.Close(DictionaryContentPanel);
        if (MainType.Equals(AppConstants.MainType.CARD_HERO))
        {

            List<CardHeroes> cardHeroes = await UserCardHeroesService.Create().GetUserCardHeroesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            listCount = cardHeroes.Count;

            totalRecord = await UserCardHeroesService.Create().GetUserCardHeroesCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BOOK))
        {
            List<Books> books = await UserBooksService.Create().GetUserBooksAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            listCount = books.Count;

            totalRecord = await UserBooksService.Create().GetUserBooksCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {

            List<CardCaptains> cardCaptains = await UserCardCaptainsService.Create().GetUserCardCaptainsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            listCount = cardCaptains.Count;

            totalRecord = await UserCardCaptainsService.Create().GetUserCardCaptainsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            List<CollaborationEquipments> collaborationEquipments = await UserCollaborationEquipmentsService.Create().GetUserCollaborationEquipmentsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCollaborationEquipmentsController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            listCount = collaborationEquipments.Count;

            totalRecord = await UserCollaborationEquipmentsService.Create().GetUserCollaborationEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ITEM))
        {
            List<Items> items = await UserItemsService.Create().GetUserItemsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset);
            Close(DictionaryContentPanel);
            UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            listCount = items.Count;

            totalRecord = await UserItemsService.Create().GetUserItemsCountAsync(User.CurrentUserId, Search, Type);
        }
        else if (MainType.Equals(AppConstants.MainType.PET))
        {
            List<Pets> pets = await UserPetsService.Create().GetUserPetsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            listCount = pets.Count;

            totalRecord = await UserPetsService.Create().GetUserPetsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SKILL))
        {
            List<Skills> skills = await UserSkillsService.Create().GetUserSkillsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            listCount = skills.Count;

            totalRecord = await UserSkillsService.Create().GetUserSkillsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SYMBOL))
        {
            List<Symbols> symbols = await UserSymbolsService.Create().GetUserSymbolsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            listCount = symbols.Count;

            totalRecord = await UserSymbolsService.Create().GetUserSymbolsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {

            List<CardMilitaries> cardMilitaries = await UserCardMilitariesService.Create().GetUserCardMilitariesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardMilitariesController.Instance.CreateUserCardMilitaries(cardMilitaries, DictionaryContentPanel);
            listCount = cardMilitaries.Count;

            totalRecord = await UserCardMilitariesService.Create().GetUserCardMilitariesCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SPELL))
        {

            List<CardSpells> cardSpells = await UserCardSpellsService.Create().GetUserCardSpellsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardSpellsController.Instance.CreateUserCardSpells(cardSpells, DictionaryContentPanel);
            listCount = cardSpells.Count;

            totalRecord = await UserCardSpellsService.Create().GetUserCardSpellsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            List<Collaborations> collaborations = await UserCollaborationsService.Create().GetUserCollaborationsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCollaborationsController.Instance.CreateUserCollaborations(collaborations, DictionaryContentPanel);
            listCount = collaborations.Count;

            totalRecord = await UserCollaborationsService.Create().GetUserCollaborationsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MEDAL))
        {
            List<Medals> medals = await UserMedalsService.Create().GetUserMedalsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            listCount = medals.Count;

            totalRecord = await UserMedalsService.Create().GetUserMedalsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TITLE))
        {
            List<Titles> titles = await UserTitlesService.Create().GetUserTitlesAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            listCount = titles.Count;

            totalRecord = await UserTitlesService.Create().GetUserTitlesCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            List<MagicFormationCircles> magicFormationCircles = await UserMagicFormationCirclesService.Create().GetUserMagicFormationCirclesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserMagicFormationCirclesController.Instance.CreateUserMagicFormationCircles(magicFormationCircles, DictionaryContentPanel);
            listCount = magicFormationCircles.Count;

            totalRecord = await UserMagicFormationCirclesService.Create().GetUserMagicFormationCirclesCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.RELIC))
        {
            List<Relics> relics = await UserRelicsService.Create().GetUserRelicsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            listCount = relics.Count;

            totalRecord = await UserRelicsService.Create().GetUserRelicsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {

            List<CardMonsters> cardMonsters = await UserCardMonstersService.Create().GetUserCardMonstersAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            listCount = cardMonsters.Count;

            totalRecord = await UserCardMonstersService.Create().GetUserCardMonstersCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {

            List<CardColonels> cardColonels = await UserCardColonelsService.Create().GetUserCardColonelsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            listCount = cardColonels.Count;

            totalRecord = await UserCardColonelsService.Create().GetUserCardColonelsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {

            List<CardGenerals> cardGenerals = await UserCardGeneralsService.Create().GetUserCardGeneralsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            listCount = cardGenerals.Count;

            totalRecord = await UserCardGeneralsService.Create().GetUserCardGeneralsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {

            List<CardAdmirals> cardAdmirals = await UserCardAdmiralsService.Create().GetUserCardAdmiralsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            listCount = cardAdmirals.Count;

            totalRecord = await UserCardAdmiralsService.Create().GetUserCardAdmiralsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TALISMAN))
        {
            List<Talismans> talismans = await UserTalismansService.Create().GetUserTalismansAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserTalismansController.Instance.CreateUserTalismans(talismans, DictionaryContentPanel);
            listCount = talismans.Count;

            totalRecord = await UserTalismansService.Create().GetUserTalismansCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.PUPPET))
        {
            List<Puppets> puppets = await UserPuppetsService.Create().GetUserPuppetsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserPuppetsController.Instance.CreateUserPuppets(puppets, DictionaryContentPanel);
            listCount = puppets.Count;

            totalRecord = await UserPuppetsService.Create().GetUserPuppetsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            List<Alchemies> alchemies = await UserAlchemiesService.Create().GetUserAlchemiesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserAlchemiesController.Instance.CreateUserAlchemies(alchemies, DictionaryContentPanel);
            listCount = alchemies.Count;

            totalRecord = await UserAlchemiesService.Create().GetUserAlchemiesCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FORGE))
        {
            List<Forges> forges = await UserForgesService.Create().GetUserForgesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserForgesController.Instance.CreateUserForges(forges, DictionaryContentPanel);
            listCount = forges.Count;

            totalRecord = await UserForgesService.Create().GetUserForgesCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            List<CardLives> cardLives = await UserCardLivesService.Create().GetUserCardLivesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardLivesController.Instance.CreateUserCardLives(cardLives, DictionaryContentPanel);
            listCount = cardLives.Count;

            totalRecord = await UserCardLivesService.Create().GetUserCardLivesCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARTWORK))
        {
            List<Artworks> artworks = await UserArtworksService.Create().GetUserArtworksAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserArtworksController.Instance.CreateUserArtworks(artworks, DictionaryContentPanel);
            listCount = artworks.Count;

            totalRecord = await UserArtworksService.Create().GetUserArtworksCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            List<SpiritBeasts> spiritBeasts = await UserSpiritBeastsService.Create().GetUserSpiritBeastsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserSpiritBeastsController.Instance.CreateUserSpiritBeasts(spiritBeasts, DictionaryContentPanel);
            listCount = spiritBeasts.Count;

            totalRecord = await UserSpiritBeastsService.Create().GetUserSpiritBeastsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            List<SpiritCards> spiritCards = await UserSpiritCardsService.Create().GetUserSpiritCardAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserSpiritCardsController.Instance.CreateUserSpiritCards(spiritCards, DictionaryContentPanel);
            listCount = spiritCards.Count;

            totalRecord = await UserSpiritCardsService.Create().GetUserSpiritCardCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            List<Artifacts> artifacts = await UserArtifactsService.Create().GetUserArtifactsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserArtifactsController.Instance.CreateUserArtifacts(artifacts, DictionaryContentPanel);
            listCount = artifacts.Count;

            totalRecord = await UserArtifactsService.Create().GetUserArtifactsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            List<Architectures> architectures = await UserArchitecturesService.Create().GetUserArchitecturesAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserArchitecturesController.Instance.CreateUserArchitectures(architectures, DictionaryContentPanel);
            listCount = architectures.Count;

            totalRecord = await UserArchitecturesService.Create().GetUserArchitecturesCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            List<Technologies> technologies = await UserTechnologiesService.Create().GetUserTechnologiesAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserTechnologiesController.Instance.CreateUserTechnologies(technologies, DictionaryContentPanel);
            listCount = technologies.Count;

            totalRecord = await UserTechnologiesService.Create().GetUserTechnologiesCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.VEHICLE))
        {
            List<Vehicles> vehicles = await UserVehiclesService.Create().GetUserVehiclesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserVehiclesController.Instance.CreateUserVehicles(vehicles, DictionaryContentPanel);
            listCount = vehicles.Count;

            totalRecord = await UserVehiclesService.Create().GetUserVehiclesCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CORE))
        {
            List<Cores> cores = await UserCoresService.Create().GetUserCoresAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCoresController.Instance.CreateUserCores(cores, DictionaryContentPanel);
            listCount = cores.Count;

            totalRecord = await UserCoresService.Create().GetUserCoresCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.WEAPON))
        {
            List<Weapons> weapons = await UserWeaponsService.Create().GetUserWeaponsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserWeaponsController.Instance.CreateUserWeapons(weapons, DictionaryContentPanel);
            listCount = weapons.Count;

            totalRecord = await UserWeaponsService.Create().GetUserWeaponsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ROBOT))
        {
            List<Robots> robots = await UserRobotsService.Create().GetUserRobotsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserRobotsController.Instance.CreateUserRobots(robots, DictionaryContentPanel);
            listCount = robots.Count;

            totalRecord = await UserRobotsService.Create().GetUserRobotsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BADGE))
        {
            List<Badges> badges = await UserBadgesService.Create().GetUserBadgesAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserBadgesController.Instance.CreateUserBadges(badges, DictionaryContentPanel);
            listCount = badges.Count;

            totalRecord = await UserBadgesService.Create().GetUserBadgesCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            List<MechaBeasts> mechaBeasts = await UserMechaBeastsService.Create().GetUserMechaBeastsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserMechaBeastsController.Instance.CreateUserMechaBeasts(mechaBeasts, DictionaryContentPanel);
            listCount = mechaBeasts.Count;

            totalRecord = await UserMechaBeastsService.Create().GetUserMechaBeastsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.RUNE))
        {
            List<Runes> runes = await UserRunesService.Create().GetUserRunesAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserRunesController.Instance.CreateUserRunes(runes, DictionaryContentPanel);
            listCount = runes.Count;

            totalRecord = await UserRunesService.Create().GetUserRunesCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FURNITURE))
        {
            List<Furnitures> furnitures = await UserFurnituresService.Create().GetUserFurnituresAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserFurnituresController.Instance.CreateUserFurnitures(furnitures, DictionaryContentPanel);
            listCount = furnitures.Count;

            totalRecord = await UserFurnituresService.Create().GetUserFurnituresCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FOOD))
        {
            List<Foods> foods = await UserFoodsService.Create().GetUserFoodsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserFoodsController.Instance.CreateUserFoods(foods, DictionaryContentPanel);
            listCount = foods.Count;

            totalRecord = await UserFoodsService.Create().GetUserFoodsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            List<Beverages> beverages = await UserBeveragesService.Create().GetUserBeveragesAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserBeveragesController.Instance.CreateUserBeverages(beverages, DictionaryContentPanel);
            listCount = beverages.Count;

            totalRecord = await UserBeveragesService.Create().GetUserBeveragesCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BUILDING))
        {
            List<Buildings> buildings = await UserBuildingsService.Create().GetUserBuildingsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserBuildingsController.Instance.CreateUserBuildings(buildings, DictionaryContentPanel);
            listCount = buildings.Count;

            totalRecord = await UserBuildingsService.Create().GetUserBuildingsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.PLANT))
        {
            List<Plants> plants = await UserPlantsService.Create().GetUserPlantsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserPlantsController.Instance.CreateUserPlants(plants, DictionaryContentPanel);
            listCount = plants.Count;

            totalRecord = await UserPlantsService.Create().GetUserPlantsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.FASHION))
        {
            List<Fashions> fashions = await UserFashionsService.Create().GetUserFashionsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserFashionsController.Instance.CreateUserFashions(fashions, DictionaryContentPanel);
            listCount = fashions.Count;

            totalRecord = await UserFashionsService.Create().GetUserFashionsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            List<Achievements> achievements = await UserAchievementsService.Create().GetUserAchievementsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserAchievementsController.Instance.CreateUserAchievements(achievements, DictionaryContentPanel);
            listCount = achievements.Count;

            totalRecord = await UserAchievementsService.Create().GetUserAchievementsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.AVATAR))
        {
            List<Avatars> avatars = await UserAvatarsService.Create().GetUserAvatarsAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserAvatarsController.Instance.CreateUserAvatars(avatars, DictionaryContentPanel);
            listCount = avatars.Count;

            totalRecord = await UserAvatarsService.Create().GetUserAvatarsCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.BORDER))
        {
            List<Borders> borders = await UserBordersService.Create().GetUserBordersAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserBordersController.Instance.CreateUserBorders(borders, DictionaryContentPanel);
            listCount = borders.Count;

            totalRecord = await UserBordersService.Create().GetUserBordersCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.EMOJI))
        {
            List<Emojis> emojis = await UserEmojisService.Create().GetUserEmojisAsync(User.CurrentUserId, Search, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserEmojisController.Instance.CreateUserEmojis(emojis, DictionaryContentPanel);
            listCount = emojis.Count;

            totalRecord = await UserEmojisService.Create().GetUserEmojisCountAsync(User.CurrentUserId, Search, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {

            List<CardSoldiers> cardSoldiers = await UserCardSoldiersService.Create().GetUserCardSoldiersAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserCardSoldiersController.Instance.CreateUserCardSoldiers(cardSoldiers, DictionaryContentPanel);
            listCount = cardSoldiers.Count;

            totalRecord = await UserCardSoldiersService.Create().GetUserCardSoldiersCountAsync(User.CurrentUserId, Search, Type, Rare);
        }
        else if (MainType.Equals(AppConstants.MainType.OUTFIT))
        {
            List<Outfits> outfits = await UserOutfitsService.Create().GetUserOutfitsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
            Close(DictionaryContentPanel);
            UserOutfitsController.Instance.CreateUserOutfits(outfits, DictionaryContentPanel);
            listCount = outfits.Count;

            totalRecord = await UserOutfitsService.Create().GetUserOutfitsCountAsync(User.CurrentUserId, Search, Type, Rare);
        }

        if (listCount > 0)
        {
            TotalItems = totalRecord;
        }

        if (IsSearchingOrFiltering && PaginationManager != null)
        {
            // Tạm thời gỡ sự kiện để việc Init không kích hoạt ngược lại hàm Load lần nữa
            PaginationManager.OnPageChanged -= OnPageSelected;

            // Vẽ lại dải nút số dựa trên TotalItems mới sau khi đã Lọc/Search
            // Luôn ép về Trang 1 vì mỗi lần Search/Filter là tính lại từ đầu
            PaginationManager.InitPagination(TotalItems, PAGE_SIZE, CurrentPage);

            // Đăng ký lại sự kiện sau khi Init đã hoàn tất sạch sẽ
            PaginationManager.OnPageChanged += OnPageSelected;
        }
    }
    public void ClearAllPrefabs()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        if (DictionaryContentPanel != null)
        {
            foreach (Transform child in DictionaryContentPanel)
            {
                Destroy(child.gameObject);
            }
        }
        if (PositionPanel != null)
        {
            foreach (Transform child in PositionPanel)
            {
                Destroy(child.gameObject);
            }
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
    public void ClosePanel()
    {
        ClearAllButton();
        ClearAllPrefabs();
        Offset = 0;
        CurrentPage = 1;
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

    // Hàm hứng sự kiện click nút phân trang
    private void OnPageSelected(int pageNumber)
    {
        CurrentPage = pageNumber;
        Offset = (CurrentPage - 1) * PAGE_SIZE;
        IsSearchingOrFiltering = false;
        _ = LoadCurrentPageAsync();
    }

    private void OnDestroy()
    {
        // Luôn luôn hủy đăng ký sự kiện khi Object bị xóa để tránh lỗi bộ nhớ
        if (PaginationManager != null)
        {
            PaginationManager.OnPageChanged -= OnPageSelected;
        }
    }
    public void LoadAnimation()
    {
        if (LeftScrollViewContentPanel != null)
        {
            LeftScrollViewContentPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
        }

        if (RightScrollViewContentPanel != null)
        {
            RightScrollViewContentPanel.gameObject.AddComponent<SlideRightToLeftAnimation>();
        }
    }
}
