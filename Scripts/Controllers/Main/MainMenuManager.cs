using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class MainMenuManager : MonoBehaviour
{
    private Transform RootPanel;
    private GameObject MainPanelPrefab;
    private GameObject TypeButtonPrefab;
    private GameObject DictionaryPanelPrefab;
    private GameObject PopupMenuPanelPrefab;
    private GameObject ArenaPanelPrefab;
    private GameObject AnimePanelPrefab;
    private GameObject ReactorPanelPrefab;
    private GameObject MasterBoardPanelPrefab;
    private GameObject PopupButtonPanelPrefab;
    private GameObject FeaturePanelPrefab;
    private GameObject RareButtonPrefab;
    private Transform MainPanel;
    private Transform DictionaryContentPanel;
    private Button CloseButton;
    private Button HomeButton;
    private Button SummonButton;
    private Button Summon10Button;
    private GameObject equipmentsPrefab;
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
    private GameObject SummonTabButtonPrefab;

    private GameObject SummonPanelPrefab;
    private Transform PositionPanel;
    private GameObject summonObject;
    private Transform CurrencyPanel;
    private GameObject currentObject;
    private Material UI_Red_Gradient_Radius_Mat_MaskPercent_70;
    //Variable for pagination
    private int offset;
    private int currentPage;
    private int totalPage;
    private const int PAGE_SIZE = 100;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private string mainType;
    private TextMeshProUGUI titleText;
    private string buttonType;
    private string search;
    private string type;
    private string rare;
    private TMP_FontAsset EuroStyleNormalFont;
    private int fontSize;
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

    void Update()
    {

    }
    public void Initialize()
    {
        offset = 0;
        currentPage = 1;
        buttonType = "";
        search = "";
        type = AppConstants.Type.ALL;
        rare = AppConstants.Rare.ALL;
        RootPanel = UIManager.Instance.GetTransform("RootPanel");
        MainPanelPrefab = UIManager.Instance.Get("MainPanelPrefab");
        PopupButtonPanelPrefab = UIManager.Instance.Get("PopupButtonPanelPrefab");
        RareButtonPrefab = UIManager.Instance.Get("RareButtonPrefab");
        // mainMenuCampaignPanel = UIManager.Instance.GetTransform("mainMenuCampaignPanel");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        SummonTabButtonPrefab = UIManager.Instance.Get("SummonTabButtonPrefab");
        DictionaryPanelPrefab = UIManager.Instance.Get("DictionaryPanelPrefab");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        equipmentsPrefab = UIManager.Instance.Get("EquipmentFirstPrefab");
        SummonPanelPrefab = UIManager.Instance.Get("SummonPanelPrefab");
        PopupMenuPanelPrefab = UIManager.Instance.Get("PopupMenuPanelPrefab");
        ArenaPanelPrefab = UIManager.Instance.Get("ArenaPanelPrefab");
        AnimePanelPrefab = UIManager.Instance.Get("AnimePanelPrefab");
        ReactorPanelPrefab = UIManager.Instance.Get("ReactorPanelPrefab");
        MasterBoardPanelPrefab = UIManager.Instance.Get("MasterBoardPanelPrefab");
        FeaturePanelPrefab = UIManager.Instance.Get("FeaturePanelPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
        UI_Red_Gradient_Radius_Mat_MaskPercent_70 = MaterialManager.Instance.Get("UI_Red_Gradient_Radius_Mat_MaskPercent_70");
        fontSize = 24;
    }
    public void CreateMainPanel()
    {
        currentObject = Instantiate(MainPanelPrefab, RootPanel);
        // ButtonLoader.Instance.CreateMainButton(currentObject);
        // GetMainButtonEvent();

        Transform content = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondCircleImage");
        Button homeButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/HomeButton").GetComponent<Button>();
        Button inventoryButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/InventoryContent/InventoryButton").GetComponent<Button>();
        Button eventButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/PlayContent/EventButton").GetComponent<Button>();
        Button campaignButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/PlayContent/CampaignButton").GetComponent<Button>();
        Button shopButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/ShopContent/ShopButton").GetComponent<Button>();
        Button teamButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent/TeamButton").GetComponent<Button>();
        Button masterBoardButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent/MasterBoardButton").GetComponent<Button>();
        Button scienceFictionButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent/ScienceFictionButton").GetComponent<Button>();
        Button galleryButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent/GalleryButton").GetComponent<Button>();
        Button collectionButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent/CollectionButton").GetComponent<Button>();
        Button equipmentButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent/EquipmentButton").GetComponent<Button>();
        Button featureButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent/FeatureButton").GetComponent<Button>();
        Button arenaButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/PlayContent/ArenaButton").GetComponent<Button>();
        Button profileButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/UserButton").GetComponent<Button>();
        Button missionButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/MissionContent/MissionButton").GetComponent<Button>();
        Button guildButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/SocialContent/GuildButton").GetComponent<Button>();
        Button researchButton = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/BuildContent/ResearchButton").GetComponent<Button>();

        _=HomeManager.Instance.CreateHomePanelAsync();

        homeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });

        inventoryButton.onClick.AddListener(() =>
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
            HomeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
                await HomeManager.Instance.CreateHomePanelAsync();
            });
            TextMeshProUGUI titleText = popupButtonPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.INVENTORY);
            ButtonLoader.Instance.CreateInventoryButton(popupButtonPanel);
            GetMainButtonEvent(popupButtonPanel);
        });

        eventButton.onClick.AddListener(() =>
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
            HomeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
                await HomeManager.Instance.CreateHomePanelAsync();
            });
            TextMeshProUGUI titleText = popupButtonPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.EVENT);
            ButtonLoader.Instance.CreateEventButton(popupButtonPanel);
            GetButtonEvent(popupButtonPanel);
        });

        shopButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ShopManager.Instance.CreateShopButtonAsync(MainPanel);
        });

        teamButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await TeamsManager.Instance.CreateTeamsAsync();
        });

        masterBoardButton.onClick.AddListener(async () =>
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
            HomeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
                await HomeManager.Instance.CreateHomePanelAsync();
            });
            // FindObjectOfType<ButtonLoader>().CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
            await MasterBoardController.Instance.CreateMasterBoardAsync(popupObject);
        });

        scienceFictionButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            GameObject popupObject = Instantiate(ReactorPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
                await HomeManager.Instance.CreateHomePanelAsync();
            });
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SCIENCE_FICTION);
            ButtonLoader.Instance.CreateScienceFictionButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
            ScienceFictionManager.Instance.GetScienceFictionButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        });

        galleryButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.GALLERY);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupObject);
            });
            ButtonLoader.Instance.CreateGalleryButton(popupObject.transform.Find("Content"));
            FindAnyObjectByType<GalleryManager>().CreateGallery(popupObject.transform.Find("Content"));
            Transform scrollViewPanel = popupObject.transform.Find("Scroll View");
            scrollViewPanel.gameObject.SetActive(false);
        });

        collectionButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.COLLECTION);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupObject);
            });
            ButtonLoader.Instance.CreateCollectionButton(popupObject.transform.Find("Content"));
            FindAnyObjectByType<CollectionManager>().CreateCollection(popupObject.transform.Find("Content"));
            Transform scrollViewPanel = popupObject.transform.Find("Scroll View");
            scrollViewPanel.gameObject.SetActive(false);
        });

        equipmentButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.EQUIPMENTS);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupObject);
            });
            await ButtonLoader.Instance.CreateEquipmentsButtonAsync(popupObject.transform.Find("Content"));
            Transform scrollViewPanel = popupObject.transform.Find("Scroll View");
            scrollViewPanel.gameObject.SetActive(false);
        });

        featureButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            FeatureManager.Instance.CreateFeature();
        });

        profileButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await ProfileManager.Instance.CreateProfileAsync();
        });

        arenaButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
                await HomeManager.Instance.CreateHomePanelAsync();
            });
            await ButtonLoader.Instance.CreateArenaButtonAsync(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        });

        guildButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            // await ProfileManager.Instance.CreateProfileAsync();
        });
        
        researchButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            ResearchManager.Instance.CreateResearch();
        });
    }
    public void CreateMainPanelUserInformation(AuthResult authResult)
    {
        // Transform userPanel = currentObject.transform.Find("User");
        // Button userButton = userPanel.GetComponent<Button>();
        RawImage avatarImage = currentObject.transform.Find("Header/AvatarImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(authResult.User.Image);
        Texture avatarTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        avatarImage.texture = avatarTexture;

        RawImage borderImage = currentObject.transform.Find("Header/BorderImage").GetComponent<RawImage>();
        fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(authResult.User.Border);
        Texture borderTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        borderImage.texture = borderTexture;

        RawImage userAvatarImage = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/UserButton/AvatarImage").GetComponent<RawImage>();
        fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(authResult.User.Image);
        Texture TuserAvatarexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        userAvatarImage.texture = TuserAvatarexture;

        RawImage userBorderImage = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/UserButton/BorderImage").GetComponent<RawImage>();
        fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(authResult.User.Border);
        Texture userBorderTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        userBorderImage.texture = userBorderTexture;

        // FindObjectOfType<CurrenciesManager>().GetMainCurrency(authResult.User.Currencies, currencyPanel);

        // userButton.onClick.AddListener(async () =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     await ProfileManager.Instance.CreateProfileAsync();
        // });

        TextMeshProUGUI userNameText = currentObject.transform.Find("MainNavigation/Scroll View/Viewport/Content/UserButton/NameText").GetComponent<TextMeshProUGUI>();
        userNameText.text = authResult.User.Name;

        TextMeshProUGUI nameText = currentObject.transform.Find("Header/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = authResult.User.Name;
        // TextMeshProUGUI levelText = currentObject.transform.Find("Header/LevelText").GetComponent<TextMeshProUGUI>();
        // levelText.text = authResult.User.Level.ToString();
        TextMeshProUGUI powerText = currentObject.transform.Find("Header/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = authResult.User.Power.ToString();

        var gold = authResult.User.Currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.GOLD);
        var silver = authResult.User.Currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.SILVER);
        var diamond = authResult.User.Currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.DIAMOND);

        RawImage goldImage = currentObject.transform.Find("Header/GoldCurrency/Image").GetComponent<RawImage>();
        RawImage silverImage = currentObject.transform.Find("Header/SilverCurrency/Image").GetComponent<RawImage>();
        RawImage diamondImage = currentObject.transform.Find("Header/DiamondCurrency/Image").GetComponent<RawImage>();

        goldImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(gold.Image));
        silverImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(silver.Image));
        diamondImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(diamond.Image));

        TextMeshProUGUI goldText = currentObject.transform.Find("Header/GoldCurrency/TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI silverText = currentObject.transform.Find("Header/SilverCurrency/TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI diamondText = currentObject.transform.Find("Header/DiamondCurrency/TitleText").GetComponent<TextMeshProUGUI>();

        goldText.text = gold.Quantity.ToString();
        silverText.text = silver.Quantity.ToString();
        diamondText.text = diamond.Quantity.ToString();
    }
    public void GetMainButtonEvent(GameObject popupButtonObject)
    {
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");
        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, () => GetType(AppConstants.MainType.CARD_HERO));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, () => GetType(AppConstants.MainType.BOOK));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, () => GetType(AppConstants.MainType.PET));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, () => GetType(AppConstants.MainType.CARD_CAPTAIN));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, () => GetType(AppConstants.MainType.CARD_COLONEL));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, () => GetType(AppConstants.MainType.CARD_GENERAL));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, () => GetType(AppConstants.MainType.CARD_ADMIRAL));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, () => GetType(AppConstants.MainType.CARD_MILITARY));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, () => GetType(AppConstants.MainType.CARD_SPELL));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, () => GetType(AppConstants.MainType.CARD_MONSTER));
        // Button_13 Equipments có thể được thêm lại nếu cần
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, () => GetType(AppConstants.MainType.ITEM));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, () => GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, () => GetType(AppConstants.MainType.COLLABORATION));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, () => GetType(AppConstants.MainType.MEDAL));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, () => GetType(AppConstants.MainType.SKILL));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, () => GetType(AppConstants.MainType.SYMBOL));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, () => GetType(AppConstants.MainType.TITLE));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, () => GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE));
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, () => GetType(AppConstants.MainType.RELIC));
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, () => GetType(AppConstants.MainType.TALISMAN));
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, () => GetType(AppConstants.MainType.PUPPET));
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, () => GetType(AppConstants.MainType.ALCHEMY));
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, () => GetType(AppConstants.MainType.FORGE));
        ButtonEvent.Instance.AssignButtonEvent("Button_24", contentPanel, () => GetType(AppConstants.MainType.CARD_LIFE));
        ButtonEvent.Instance.AssignButtonEvent("Button_25", contentPanel, () => GetType(AppConstants.MainType.ARTWORK));
        ButtonEvent.Instance.AssignButtonEvent("Button_26", contentPanel, () => GetType(AppConstants.MainType.SPIRIT_BEAST));
        ButtonEvent.Instance.AssignButtonEvent("Button_27", contentPanel, () => GetType(AppConstants.MainType.SPIRIT_CARD));
        ButtonEvent.Instance.AssignButtonEvent("Button_28", contentPanel, () => GetType(AppConstants.MainType.CARDS));
        ButtonEvent.Instance.AssignButtonEvent("Button_29", contentPanel, () => GetType(AppConstants.MainType.ARCHITECTURE));
        ButtonEvent.Instance.AssignButtonEvent("Button_30", contentPanel, () => GetType(AppConstants.MainType.TECHNOLOGY));
        ButtonEvent.Instance.AssignButtonEvent("Button_31", contentPanel, () => GetType(AppConstants.MainType.VEHICLE));
        ButtonEvent.Instance.AssignButtonEvent("Button_32", contentPanel, () => GetType(AppConstants.MainType.CORE));
        ButtonEvent.Instance.AssignButtonEvent("Button_33", contentPanel, () => GetType(AppConstants.MainType.WEAPON));
        ButtonEvent.Instance.AssignButtonEvent("Button_34", contentPanel, () => GetType(AppConstants.MainType.ROBOT));
        ButtonEvent.Instance.AssignButtonEvent("Button_35", contentPanel, () => GetType(AppConstants.MainType.BADGE));
        ButtonEvent.Instance.AssignButtonEvent("Button_36", contentPanel, () => GetType(AppConstants.MainType.MECHA_BEAST));
        ButtonEvent.Instance.AssignButtonEvent("Button_37", contentPanel, () => GetType(AppConstants.MainType.RUNE));
        ButtonEvent.Instance.AssignButtonEvent("Button_38", contentPanel, () => GetType(AppConstants.MainType.FURNITURE));
        ButtonEvent.Instance.AssignButtonEvent("Button_39", contentPanel, () => GetType(AppConstants.MainType.FOOD));
        ButtonEvent.Instance.AssignButtonEvent("Button_40", contentPanel, () => GetType(AppConstants.MainType.BEVERAGE));
        ButtonEvent.Instance.AssignButtonEvent("Button_41", contentPanel, () => GetType(AppConstants.MainType.BUILDING));
        ButtonEvent.Instance.AssignButtonEvent("Button_42", contentPanel, () => GetType(AppConstants.MainType.PLANT));
        ButtonEvent.Instance.AssignButtonEvent("Button_43", contentPanel, () => GetType(AppConstants.MainType.FASHION));
    }
    public void GetButtonEvent(GameObject popupButtonObject)
    {
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");
        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_HEROES));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, () => GetType(AppConstants.MainType.SUMMON_BOOKS));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_CAPTAINS));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_MONSTERS));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_MILITARY));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_SPELLS));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_COLONELS));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_GENERALS));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_ADMIRALS));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, () => GetType(AppConstants.MainType.ANIME));
        // ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, () => GetType(AppConstants.MainType.GUILD));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, () => GetType(AppConstants.MainType.TOWER));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, () => GetType(AppConstants.MainType.EVENT));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, () => GetType(AppConstants.MainType.DAILY_CHECKIN));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, () => GetType(AppConstants.Market.RARE_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, () => GetType(AppConstants.Market.ULTRA_RARE_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, () => GetType(AppConstants.Market.LEGENDARY_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, () => GetType(AppConstants.Market.MYSTIC_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, () => GetType(AppConstants.MainType.CHIP));
    }
    public void GetType(string type)
    {
        mainType = type; // Gán giá trị cho mainType
        _ = GetButtonTypeAsync(); // Gọi hàm xử lý
        if (titleText != null)
        {
            titleText.text = LocalizationManager.Get(type);
        }
    }
    public async Task GetButtonTypeAsync()
    {
        // MainMenuPanel.SetActive(true);
        if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_HEROES) || mainType.Equals(AppConstants.MainType.SUMMON_BOOKS) || mainType.Equals(AppConstants.MainType.SUMMON_CARD_CAPTAINS) ||
        mainType.Equals(AppConstants.MainType.SUMMON_CARD_COLONELS) || mainType.Equals(AppConstants.MainType.SUMMON_CARD_GENERALS) || mainType.Equals(AppConstants.MainType.SUMMON_CARD_ADMIRALS) ||
        mainType.Equals(AppConstants.MainType.SUMMON_CARD_MILITARY) || mainType.Equals(AppConstants.MainType.SUMMON_CARD_MONSTERS) || mainType.Equals(AppConstants.MainType.SUMMON_CARD_SPELLS))
        {
            buttonType = "button2";
            summonObject = Instantiate(SummonPanelPrefab, MainPanel);
            DictionaryContentPanel = summonObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            LeftScrollViewContentPanel = summonObject.transform.Find("Scroll View/Viewport/ButtonContent");
            PositionPanel = summonObject.transform.Find("DictionaryCards/Position");

            titleText = summonObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = summonObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            SummonButton = summonObject.transform.Find("DictionaryCards/SummonButton").GetComponent<Button>();
            Summon10Button = summonObject.transform.Find("DictionaryCards/Summon10Button").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(summonObject);
            });
            HomeButton = summonObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
                await HomeManager.Instance.CreateHomePanelAsync();
            });
            // SummonAreaPanel = summonObject.transform.Find("SummonArea");
            CurrencyPanel = summonObject.transform.Find("DictionaryCards/Currency");

            TextMeshProUGUI SummonOneButtonText = SummonButton.GetComponentInChildren<TextMeshProUGUI>();
            SummonOneButtonText.font = EuroStyleNormalFont;
            SummonOneButtonText.fontSize = fontSize;
            SummonOneButtonText.fontStyle = FontStyles.Bold;
            SummonOneButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SUMMON_ONE);
            TextMeshProUGUI SummonTenButtonText = Summon10Button.GetComponentInChildren<TextMeshProUGUI>();
            SummonTenButtonText.font = EuroStyleNormalFont;
            SummonTenButtonText.fontSize = fontSize;
            SummonTenButtonText.fontStyle = FontStyles.Bold;
            SummonTenButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SUMMON_TEN);

            List<string> types = await TypeManager.GetUniqueTypesAsync(mainType);

            if (types.Count > 0 && !mainType.Equals(AppConstants.MainType.EQUIPMENT))
            {
                for (int i = 0; i < types.Count; i++)
                {
                    // Tạo một nút mới từ prefab
                    string subType = types[i];
                    GameObject button = null;
                    if (buttonType.Equals("button1"))
                    {
                        button = Instantiate(TypeButtonPrefab, LeftScrollViewContentPanel);
                        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = subType.Replace("_", " ");
                    }
                    else if (buttonType.Equals("button2"))
                    {
                        button = Instantiate(SummonTabButtonPrefab, LeftScrollViewContentPanel);
                        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = subType.Replace("_", " ");
                    }

                    Button btn = button.GetComponent<Button>();
                    btn.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        OnButtonClick(button, subType);
                    });

                    if (i == 0)
                    {
                        type = subType;
                        // if (buttonType.Equals("button1"))
                        // {
                        //     ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                        // }
                        if (buttonType.Equals("button2"))
                        {
                            ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.SUMMON_BUTTON_AFTER_CLICK_URL);
                        }
                        _ = LoadCurrentPageAsync();

                    }
                    else
                    {
                        // if (buttonType.Equals("button1"))
                        // {
                        //     ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                        // }
                        if (buttonType.Equals("button2"))
                        {
                            ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.SUMMON_BUTTON_BEFORE_CLICK_URL);
                        }
                    }
                }
            }
            else
            {
                _ = LoadCurrentPageAsync();
            }

            RawImage dictionaryBackground = summonObject.transform.Find("DictionaryBackground").GetComponent<RawImage>();
            if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_HEROES))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_HEROES_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_BOOKS))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_BOOKS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_CAPTAINS))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_CAPTAINS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_MONSTERS))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_MONSTERS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_MILITARY))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_MILITARIES_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_SPELLS))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_SPELLS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_COLONELS))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_COLONELS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_GENERALS))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_GENERALS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_ADMIRALS))
            {
                Texture texture = Resources.Load<Texture>(ImageConstants.Background.SUMMON_CARD_ADMIRALS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.ANIME))
        {
            GameObject popupObject = Instantiate(AnimePanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
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
            ButtonLoader.Instance.CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.TOWER))
        {
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
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
        else if (mainType.Equals(AppConstants.MainType.DAILY_CHECKIN))
        {
            await DailyCheckinManager.Instance.CreateDailyCheckinGroupAsync();
        }
        else if (mainType.Equals(AppConstants.Market.RARE_MARKET))
        {
            await RareMarketManager.Instance.CreateRareMarketAsync(MainPanel);
        }
        else if (mainType.Equals(AppConstants.Market.ULTRA_RARE_MARKET))
        {
            await UltraRareMarketManager.Instance.CreateUltraRareMarketAsync(MainPanel);
        }
        else if (mainType.Equals(AppConstants.Market.LEGENDARY_MARKET))
        {
            await LegendaryMarketManager.Instance.CreateLegendaryMarketAsync(MainPanel);
        }
        else if (mainType.Equals(AppConstants.Market.MYSTIC_MARKET))
        {
            await MysticMarketManager.Instance.CreateMysticMarketAsync(MainPanel);
        }
        else if (mainType.Equals(AppConstants.MainType.CHIP))
        {
            // await MysticMarketManager.Instance.CreateMysticMarketAsync();
        }
        else
        {
            buttonType = "button1";
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
            topBackgroundImage.material = UI_Red_Gradient_Radius_Mat_MaskPercent_70;
            TextMeshProUGUI subTitleText = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
            subTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.INVENTORY);

            // CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");

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
        }
        LoadAnimation();
    }
    void OnButtonClick(GameObject clickedButton, string subType)
    {
        foreach (Transform child in LeftScrollViewContentPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                if (buttonType.Equals("button1"))
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
                else if (buttonType.Equals("button2"))
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.SUMMON_BUTTON_BEFORE_CLICK_URL);
                }
            }
        }

        type = subType;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        if (buttonType.Equals("button1"))
        {
            ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);

            if (RightScrollViewContentPanel.childCount > 0)
            {
                for (int i = 0; i < RightScrollViewContentPanel.childCount; i++)
                {
                    Transform child = RightScrollViewContentPanel.GetChild(i);
                    Button rareButton = child.GetComponent<Button>();

                    if (rareButton != null)
                    {
                        if (i == 0)
                        {
                            rare = QualityEvaluator.rarities[0];
                            rareButton.transform.Find("Active").gameObject.SetActive(true);
                            rareButton.transform.Find("Unactive").gameObject.SetActive(false);
                        }
                        else
                        {
                            rareButton.transform.Find("Active").gameObject.SetActive(false);
                            rareButton.transform.Find("Unactive").gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        else if (buttonType.Equals("button2"))
        {
            ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.SUMMON_BUTTON_AFTER_CLICK_URL);
        }
        _ = LoadCurrentPageAsync();
    }
    public void OnRareTabButtonClick(GameObject clickedButton, string selectedRare)
    {
        foreach (Transform child in RightScrollViewContentPanel)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.transform.Find("Active").gameObject.SetActive(false);
                button.transform.Find("Unactive").gameObject.SetActive(true);
            }
        }

        rare = selectedRare;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        clickedButton.transform.Find("Active").gameObject.SetActive(true);
        clickedButton.transform.Find("Unactive").gameObject.SetActive(false);
        _ = LoadCurrentPageAsync();
    }
    public async Task LoadCurrentPageAsync()
    {
        int totalRecord = 0;
        int listCount = 0;
        // offset = 0;
        ButtonEvent.Instance.Close(DictionaryContentPanel);
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            List<CardHeroes> cardHeroes = await UserCardHeroesService.Create().GetUserCardHeroesAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            listCount = cardHeroes.Count;

            totalRecord = await UserCardHeroesService.Create().GetUserCardHeroesCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            List<Books> books = await UserBooksService.Create().GetUserBooksAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            listCount = books.Count;

            totalRecord = await UserBooksService.Create().GetUserBooksCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> cardCaptains = await UserCardCaptainsService.Create().GetUserCardCaptainsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            listCount = cardCaptains.Count;

            totalRecord = await UserCardCaptainsService.Create().GetUserCardCaptainsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            List<CollaborationEquipments> collaborationEquipments = await UserCollaborationEquipmentsService.Create().GetUserCollaborationEquipmentsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCollaborationEquipmentsController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            listCount = collaborationEquipments.Count;

            totalRecord = await UserCollaborationEquipmentsService.Create().GetUserCollaborationEquipmentsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ITEM))
        {
            List<Items> items = await UserItemsService.Create().GetUserItemsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset);
            Close(DictionaryContentPanel);
            UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            listCount = items.Count;

            totalRecord = await UserItemsService.Create().GetUserItemsCountAsync(User.CurrentUserId, search, type);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            List<Pets> pets = await UserPetsService.Create().GetUserPetsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            listCount = pets.Count;

            totalRecord = await UserPetsService.Create().GetUserPetsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            List<Skills> skills = await UserSkillsService.Create().GetUserSkillsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            listCount = skills.Count;

            totalRecord = await UserSkillsService.Create().GetUserSkillsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            List<Symbols> symbols = await UserSymbolsService.Create().GetUserSymbolsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            listCount = symbols.Count;

            totalRecord = await UserSymbolsService.Create().GetUserSymbolsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaries = await UserCardMilitariesService.Create().GetUserCardMilitariesAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardMilitariesController.Instance.CreateUserCardMilitaries(cardMilitaries, DictionaryContentPanel);
            listCount = cardMilitaries.Count;

            totalRecord = await UserCardMilitariesService.Create().GetUserCardMilitariesCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpells = await UserCardSpellsService.Create().GetUserCardSpellsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardSpellsController.Instance.CreateUserCardSpells(cardSpells, DictionaryContentPanel);
            listCount = cardSpells.Count;

            totalRecord = await UserCardSpellsService.Create().GetUserCardSpellsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            List<Collaborations> collaborations = await UserCollaborationsService.Create().GetUserCollaborationsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCollaborationsController.Instance.CreateUserCollaborations(collaborations, DictionaryContentPanel);
            listCount = collaborations.Count;

            totalRecord = await UserCollaborationsService.Create().GetUserCollaborationsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            List<Medals> medals = await UserMedalsService.Create().GetUserMedalsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            listCount = medals.Count;

            totalRecord = await UserMedalsService.Create().GetUserMedalsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            List<Titles> titles = await UserTitlesService.Create().GetUserTitlesAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            listCount = titles.Count;

            totalRecord = await UserTitlesService.Create().GetUserTitlesCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            List<MagicFormationCircles> magicFormationCircles = await UserMagicFormationCirclesService.Create().GetUserMagicFormationCirclesAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserMagicFormationCirclesController.Instance.CreateUserMagicFormationCircles(magicFormationCircles, DictionaryContentPanel);
            listCount = magicFormationCircles.Count;

            totalRecord = await UserMagicFormationCirclesService.Create().GetUserMagicFormationCirclesCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            List<Relics> relics = await UserRelicsService.Create().GetUserRelicsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            listCount = relics.Count;

            totalRecord = await UserRelicsService.Create().GetUserRelicsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonsters = await UserCardMonstersService.Create().GetUserCardMonstersAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            listCount = cardMonsters.Count;

            totalRecord = await UserCardMonstersService.Create().GetUserCardMonstersCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonels = await UserCardColonelsService.Create().GetUserCardColonelsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            listCount = cardColonels.Count;

            totalRecord = await UserCardColonelsService.Create().GetUserCardColonelsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGenerals = await UserCardGeneralsService.Create().GetUserCardGeneralsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            listCount = cardGenerals.Count;

            totalRecord = await UserCardGeneralsService.Create().GetUserCardGeneralsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmirals = await UserCardAdmiralsService.Create().GetUserCardAdmiralsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            listCount = cardAdmirals.Count;

            totalRecord = await UserCardAdmiralsService.Create().GetUserCardAdmiralsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_HEROES))
        {
            List<CardHeroes> cardHeroes = await CardHeroesService.Create().GetCardHeroesRandomAsync(type, 3);
            UserCardHeroesController.Instance.CreateUserCardHeroesForSummon(cardHeroes, PositionPanel);



            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HERO_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HERO_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });

            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HERO_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_BOOKS))
        {
            List<Books> books = await BooksService.Create().GetBooksRandomAsync(type, 3);
            UserBooksController.Instance.CreateUserBooksForSummon(books, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HERO_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HERO_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HERO_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_CAPTAINS))
        {
            List<CardCaptains> cardCaptains = await CardCaptainsService.Create().GetCardCaptainsRandomAsync(type, 3);
            UserCardCaptainsController.Instance.CreateUserCardCaptainsForSummon(cardCaptains, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_CAPTAIN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_CAPTAIN_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_CAPTAIN_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create().GetCardMilitariesRandomAsync(type, 3);
            UserCardMilitariesController.Instance.CreateUserCardMilitaryForSummon(cardMilitaries, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MILITARY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MILITARY_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MILITARY_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_SPELLS))
        {
            List<CardSpells> cardSpells = await CardSpellsService.Create().GetCardSpellsRandomAsync(type, 3);
            UserCardSpellsController.Instance.CreateUserCardSpellForSummon(cardSpells, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_SPELL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_SPELL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_SPELL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_MONSTERS))
        {
            List<CardMonsters> cardMonsters = await CardMonstersService.Create().GetCardMonstersRandomAsync(type, 3);
            UserCardMonstersController.Instance.CreateUserCardMonstersForSummon(cardMonsters, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MONSTER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MONSTER_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MONSTER_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_COLONELS))
        {
            List<CardColonels> cardColonels = await CardColonelsService.Create().GetCardColonelsRandomAsync(type, 3);
            UserCardColonelsController.Instance.CreateUserCardColonelsForSummon(cardColonels, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_COLONEL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_COLONEL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_COLONEL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_GENERALS))
        {
            List<CardGenerals> cardGenerals = await CardGeneralsService.Create().GetCardGeneralsRandomAsync(type, 3);
            UserCardGeneralsController.Instance.CreateUserCardGeneralsForSummon(cardGenerals, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_GENERAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_GENERAL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_GENERAL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_ADMIRALS))
        {
            List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create().GetCardAdmiralsRandomAsync(type, 3);
            UserCardAdmiralsController.Instance.CreateUserCardAdmiralsForSummon(cardAdmirals, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_ADMIRAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_ADMIRAL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            Summon10Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_ADMIRAL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            List<Talismans> talismans = await UserTalismansService.Create().GetUserTalismansAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserTalismansController.Instance.CreateUserTalismans(talismans, DictionaryContentPanel);
            listCount = talismans.Count;

            totalRecord = await UserTalismansService.Create().GetUserTalismansCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            List<Puppets> puppets = await UserPuppetsService.Create().GetUserPuppetsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserPuppetsController.Instance.CreateUserPuppets(puppets, DictionaryContentPanel);
            listCount = puppets.Count;

            totalRecord = await UserPuppetsService.Create().GetUserPuppetsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            List<Alchemies> alchemies = await UserAlchemiesService.Create().GetUserAlchemiesAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserAlchemiesController.Instance.CreateUserAlchemies(alchemies, DictionaryContentPanel);
            listCount = alchemies.Count;

            totalRecord = await UserAlchemiesService.Create().GetUserAlchemiesCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            List<Forges> forges = await UserForgesService.Create().GetUserForgesAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserForgesController.Instance.CreateUserForges(forges, DictionaryContentPanel);
            listCount = forges.Count;

            totalRecord = await UserForgesService.Create().GetUserForgesCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            List<CardLives> cardLives = await UserCardLivesService.Create().GetUserCardLivesAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserCardLivesController.Instance.CreateUserCardLives(cardLives, DictionaryContentPanel);
            listCount = cardLives.Count;

            totalRecord = await UserCardLivesService.Create().GetUserCardLivesCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            List<Artworks> artworks = await UserArtworksService.Create().GetUserArtworksAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserArtworksController.Instance.CreateUserArtworks(artworks, DictionaryContentPanel);
            listCount = artworks.Count;

            totalRecord = await UserArtworksService.Create().GetUserArtworksCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            List<SpiritBeasts> spiritBeasts = await UserSpiritBeastsService.Create().GetUserSpiritBeastsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserSpiritBeastsController.Instance.CreateUserSpiritBeasts(spiritBeasts, DictionaryContentPanel);
            listCount = spiritBeasts.Count;

            totalRecord = await UserSpiritBeastsService.Create().GetUserSpiritBeastsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            List<SpiritCards> spiritCards = await UserSpiritCardsService.Create().GetUserSpiritCardAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserSpiritCardsController.Instance.CreateUserSpiritCards(spiritCards, DictionaryContentPanel);
            listCount = spiritCards.Count;

            totalRecord = await UserSpiritCardsService.Create().GetUserSpiritCardCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD))
        {
            List<Cards> cards = await UserCardsService.Create().GetUserCardsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserCardsController.Instance.CreateUserCards(cards, DictionaryContentPanel);
            listCount = cards.Count;

            totalRecord = await UserCardsService.Create().GetUserCardsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            List<Architectures> architectures = await UserArchitecturesService.Create().GetUserArchitecturesAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserArchitecturesController.Instance.CreateUserArchitectures(architectures, DictionaryContentPanel);
            listCount = architectures.Count;

            totalRecord = await UserArchitecturesService.Create().GetUserArchitecturesCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            List<Technologies> technologies = await UserTechnologiesService.Create().GetUserTechnologiesAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserTechnologiesController.Instance.CreateUserTechnologies(technologies, DictionaryContentPanel);
            listCount = technologies.Count;

            totalRecord = await UserTechnologiesService.Create().GetUserTechnologiesCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            List<Vehicles> vehicles = await UserVehiclesService.Create().GetUserVehiclesAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserVehiclesController.Instance.CreateUserVehicles(vehicles, DictionaryContentPanel);
            listCount = vehicles.Count;

            totalRecord = await UserVehiclesService.Create().GetUserVehiclesCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            List<Cores> cores = await UserCoresService.Create().GetUserCoresAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserCoresController.Instance.CreateUserCores(cores, DictionaryContentPanel);
            listCount = cores.Count;

            totalRecord = await UserCoresService.Create().GetUserCoresCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            List<Weapons> weapons = await UserWeaponsService.Create().GetUserWeaponsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserWeaponsController.Instance.CreateUserWeapons(weapons, DictionaryContentPanel);
            listCount = weapons.Count;

            totalRecord = await UserWeaponsService.Create().GetUserWeaponsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            List<Robots> robots = await UserRobotsService.Create().GetUserRobotsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserRobotsController.Instance.CreateUserRobots(robots, DictionaryContentPanel);
            listCount = robots.Count;

            totalRecord = await UserRobotsService.Create().GetUserRobotsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            List<Badges> badges = await UserBadgesService.Create().GetUserBadgesAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserBadgesController.Instance.CreateUserBadges(badges, DictionaryContentPanel);
            listCount = badges.Count;

            totalRecord = await UserBadgesService.Create().GetUserBadgesCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            List<MechaBeasts> mechaBeasts = await UserMechaBeastsService.Create().GetUserMechaBeastsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserMechaBeastsController.Instance.CreateUserMechaBeasts(mechaBeasts, DictionaryContentPanel);
            listCount = mechaBeasts.Count;

            totalRecord = await UserMechaBeastsService.Create().GetUserMechaBeastsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            List<Runes> runes = await UserRunesService.Create().GetUserRunesAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserRunesController.Instance.CreateUserRunes(runes, DictionaryContentPanel);
            listCount = runes.Count;

            totalRecord = await UserRunesService.Create().GetUserRunesCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            List<Furnitures> furnitures = await UserFurnituresService.Create().GetUserFurnituresAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserFurnituresController.Instance.CreateUserFurnitures(furnitures, DictionaryContentPanel);
            listCount = furnitures.Count;

            totalRecord = await UserFurnituresService.Create().GetUserFurnituresCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            List<Foods> foods = await UserFoodsService.Create().GetUserFoodsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserFoodsController.Instance.CreateUserFoods(foods, DictionaryContentPanel);
            listCount = foods.Count;

            totalRecord = await UserFoodsService.Create().GetUserFoodsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            List<Beverages> beverages = await UserBeveragesService.Create().GetUserBeveragesAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserBeveragesController.Instance.CreateUserBeverages(beverages, DictionaryContentPanel);
            listCount = beverages.Count;

            totalRecord = await UserBeveragesService.Create().GetUserBeveragesCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            List<Buildings> buildings = await UserBuildingsService.Create().GetUserBuildingsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserBuildingsController.Instance.CreateUserBuildings(buildings, DictionaryContentPanel);
            listCount = buildings.Count;

            totalRecord = await UserBuildingsService.Create().GetUserBuildingsCountAsync(User.CurrentUserId, search, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            List<Plants> plants = await UserPlantsService.Create().GetUserPlantsAsync(User.CurrentUserId, search, PAGE_SIZE, offset, rare);
            UserPlantsController.Instance.CreateUserPlants(plants, DictionaryContentPanel);
            listCount = plants.Count;

            totalRecord = await UserPlantsService.Create().GetUserPlantsCountAsync(User.CurrentUserId, search, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            List<Fashions> fashions = await UserFashionsService.Create().GetUserFashionsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
            Close(DictionaryContentPanel);
            UserFashionsController.Instance.CreateUserFashions(fashions, DictionaryContentPanel);
            listCount = fashions.Count;

            totalRecord = await UserFashionsService.Create().GetUserFashionsCountAsync(User.CurrentUserId, search, type, rare);
        }

        if (listCount > 0)
        {
            totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
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
    public void CreateTicketUI(List<Items> items)
    {
        foreach (Items item in items)
        {
            RawImage oneTicketImage = summonObject.transform.Find("DictionaryCards/OneTicketImage").GetComponent<RawImage>();
            RawImage tenTicketImage = summonObject.transform.Find("DictionaryCards/TenTicketImage").GetComponent<RawImage>();
            TextMeshProUGUI oneTicketText = summonObject.transform.Find("DictionaryCards/OneTicketText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI tenTicketText = summonObject.transform.Find("DictionaryCards/TenTicketText").GetComponent<TextMeshProUGUI>();

            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(item.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            oneTicketImage.texture = texture;
            tenTicketImage.texture = texture;
            oneTicketText.text = "1";
            tenTicketText.text = "10";
        }
    }
    void AddClickListener(EventTrigger trigger, System.Action callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { callback(); });
        trigger.triggers.Add(entry);
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
    public void loadScence()
    {
        FindAnyObjectByType<SceneLoader>().LoadScene("TeamsScenes");
    }

}
