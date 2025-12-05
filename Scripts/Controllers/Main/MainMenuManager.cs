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
    private GameObject buttonPrefab;
    private GameObject DictionaryPanel;
    private GameObject PopupMenuPanelPrefab;
    private GameObject ArenaPanelPrefab;
    private GameObject AnimePanelPrefab;
    private GameObject ReactorPanelPrefab;
    private GameObject MasterBoardPanelPrefab;
    private GameObject PopupButtonPrefab;
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
    private GameObject buttonPrefab2;

    private GameObject SummonPanel;
    private Transform PositionPanel;
    private GameObject summonObject;
    private Transform CurrencyPanel;
    private GameObject currentObject;
    private Material UI_Red_Gradient_Radius_Mat_MaskPercent_45;
    //Variable for pagination
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private string mainType;
    private Text titleText;
    private string buttonType;
    private string type;
    private string rare;
    private bool canUseRareButton;
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
        pageSize = 100;
        buttonType = "";
        rare = AppConstants.Rare.ALL;
        RootPanel = UIManager.Instance.GetTransform("RootPanel");
        MainPanelPrefab = UIManager.Instance.GetGameObject("MainPanelPrefab");
        PopupButtonPrefab = UIManager.Instance.GetGameObject("PopupButtonPrefab");
        RareButtonPrefab = UIManager.Instance.GetGeneralButton("RareButtonPrefab");
        // mainMenuCampaignPanel = UIManager.Instance.GetTransform("mainMenuCampaignPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        buttonPrefab2 = UIManager.Instance.GetGameObject("TabButton2");
        DictionaryPanel = UIManager.Instance.GetGameObject("DictionaryPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        SummonPanel = UIManager.Instance.GetGameObject("SummonPanelPrefab");
        PopupMenuPanelPrefab = UIManager.Instance.GetGameObject("PopupMenuPanelPrefab");
        ArenaPanelPrefab = UIManager.Instance.GetGameObject("ArenaPanelPrefab");
        AnimePanelPrefab = UIManager.Instance.GetGameObject("AnimePanelPrefab");
        ReactorPanelPrefab = UIManager.Instance.GetScienceFictionPanel("ReactorPanelPrefab");
        MasterBoardPanelPrefab = UIManager.Instance.GetGameObject("MasterBoardPanelPrefab");
        FeaturePanelPrefab = UIManager.Instance.GetFeaturePanel("FeaturePanelPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
        UI_Red_Gradient_Radius_Mat_MaskPercent_45 = MaterialManager.Instance.GetRedMaterial("UI_Red_Gradient_Radius_Mat_MaskPercent_45");
        fontSize = 24;
    }
    public void CreateMainPanel()
    {
        currentObject = Instantiate(MainPanelPrefab, RootPanel);
        // ButtonLoader.Instance.CreateMainButton(currentObject);
        // GetMainButtonEvent();
        GetPrimaryButtonEvent();

        Transform content = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondCircleImage");
        Button inventoryButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/InventoryButton").GetComponent<Button>();
        Button eventButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/EventButton").GetComponent<Button>();
        Button campaignButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/CampaignButton").GetComponent<Button>();
        Button shopButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/ShopButton").GetComponent<Button>();
        Button teamButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/TeamButton").GetComponent<Button>();
        Button masterBoardButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/MasterBoardButton").GetComponent<Button>();
        Button scienceFictionButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/ScienceFictionButton").GetComponent<Button>();
        Button galleryButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/GalleryButton").GetComponent<Button>();
        Button collectionButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/CollectionButton").GetComponent<Button>();
        Button equipmentButton = currentObject.transform.Find("MainPanel/MainButtonGroup/SecondaryContent/EquipmentButton").GetComponent<Button>();
        Button featureButton = currentObject.transform.Find("RightButtonGroup/FeatureButton").GetComponent<Button>();
        Button arenaButton = currentObject.transform.Find("RightButtonGroup/ArenaButton").GetComponent<Button>();
        Button profileButton = currentObject.transform.Find("RightButtonGroup/ProfileButton").GetComponent<Button>();
        Button missionButton = currentObject.transform.Find("RightButtonGroup/MissionButton").GetComponent<Button>();
        Button guildButton = currentObject.transform.Find("RightButtonGroup/GuildButton").GetComponent<Button>();

        content.AddComponent<RotateAnimation>();

        inventoryButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupButtonPanel = Instantiate(PopupButtonPrefab, MainPanel);
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
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateInventoryButton(popupButtonPanel);
            GetMainButtonEvent(popupButtonPanel);
        });

        eventButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupButtonPanel = Instantiate(PopupButtonPrefab, MainPanel);
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
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateEventButton(popupButtonPanel);
            GetButtonEvent(popupButtonPanel);
        });

        shopButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await ShopManager.Instance.CreateShopButtonAsync();
        });

        teamButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await TeamsManager.Instance.CreateTeamsAsync();
        });

        masterBoardButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
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
                Close(MainPanel);
            });
            titleTMPText.text = LocalizationManager.Get(AppConstants.MainType.MASTER_BOARD);
            // FindObjectOfType<ButtonLoader>().CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
            await MasterBoardController.Instance.CreateMasterBoardAsync(popupObject);
        });

        scienceFictionButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupObject = Instantiate(ReactorPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
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
                Close(MainPanel);
            });
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
            FeatureManager.Instance.CreateFeature();
        });

        profileButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await ProfileManager.Instance.CreateProfileAsync();
        });

        arenaButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
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
                Close(MainPanel);
            });
            await ButtonLoader.Instance.CreateArenaButtonAsync(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        });

        guildButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await ProfileManager.Instance.CreateProfileAsync();
        });
    }
    public void CreateMainPanelUserInformation(AuthResult authResult)
    {
        Transform userPanel = currentObject.transform.Find("User");
        Button userButton = userPanel.GetComponent<Button>();
        Transform currencyPanel = currentObject.transform.Find("Currency");
        TextMeshProUGUI nameText = userPanel.transform.Find("UserGroup/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = authResult.User.Name;
        TextMeshProUGUI levelText = userPanel.transform.Find("UserGroup/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = authResult.User.Level.ToString();
        TextMeshProUGUI powerText = userPanel.transform.Find("UserGroup/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = authResult.User.Power.ToString();
        RawImage avatarImage = userPanel.transform.Find("UserGroup/AvatarImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(authResult.User.Image);
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        avatarImage.texture = texture;

        RawImage borderImage = userPanel.transform.Find("UserGroup/BorderImage").GetComponent<RawImage>();

        fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(authResult.User.Border);
        Texture borderTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        borderImage.texture = borderTexture;

        FindObjectOfType<CurrenciesManager>().GetMainCurrency(authResult.User.Currencies, currencyPanel);

        userButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await ProfileManager.Instance.CreateProfileAsync();
        });
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
    }
    public void GetPrimaryButtonEvent()
    {
        Transform mainButtonGroupPanel = currentObject.transform.Find("MainPanel/MainButtonGroup");
        mainButtonGroupPanel.gameObject.SetActive(true);
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
            canUseRareButton = false;
            summonObject = Instantiate(SummonPanel, MainPanel);
            DictionaryContentPanel = summonObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            LeftScrollViewContentPanel = summonObject.transform.Find("Scroll View/Viewport/ButtonContent");
            PositionPanel = summonObject.transform.Find("DictionaryCards/Position");

            titleText = summonObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = summonObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            SummonButton = summonObject.transform.Find("DictionaryCards/SummonButton").GetComponent<Button>();
            Summon10Button = summonObject.transform.Find("DictionaryCards/Summon10Button").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(summonObject);
            });
            HomeButton = summonObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
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
            canUseRareButton = false;
            GameObject popupObject = Instantiate(AnimePanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
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
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.TOWER))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
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
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateTowerButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.DAILY_CHECKIN))
        {
            canUseRareButton = false;
            await DailyCheckinManager.Instance.CreateDailyCheckinGroupAsync();
        }
        else if (mainType.Equals(AppConstants.Market.RARE_MARKET))
        {
            canUseRareButton = false;
            await RareMarketManager.Instance.CreateRareMarketAsync();
        }
        else if (mainType.Equals(AppConstants.Market.ULTRA_RARE_MARKET))
        {
            canUseRareButton = false;
            await UltraRareMarketManager.Instance.CreateUltraRareMarketAsync();
        }
        else if (mainType.Equals(AppConstants.Market.LEGENDARY_MARKET))
        {
            canUseRareButton = false;
            await LegendaryMarketManager.Instance.CreateLegendaryMarketAsync();
        }
        else if (mainType.Equals(AppConstants.Market.MYSTIC_MARKET))
        {
            canUseRareButton = false;
            await MysticMarketManager.Instance.CreateMysticMarketAsync();
        }
        else
        {
            buttonType = "button1";
            canUseRareButton = true;
            GameObject mainMenuObject = Instantiate(DictionaryPanel, MainPanel);
            DictionaryContentPanel = mainMenuObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            RightScrollViewContentPanel = mainMenuObject.transform.Find("RightScrollView/Viewport/Content");
            LeftScrollViewContentPanel = mainMenuObject.transform.Find("Scroll View/Viewport/ButtonContent");
            PageText = mainMenuObject.transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
            NextButton = mainMenuObject.transform.Find("Pagination/Next").GetComponent<Button>();
            PreviousButton = mainMenuObject.transform.Find("Pagination/Previous").GetComponent<Button>();
            titleText = mainMenuObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = mainMenuObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ClosePanel();
                Destroy(mainMenuObject);
            });
            HomeButton = mainMenuObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
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

            RawImage topBackgroundImage = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TopBackground").GetComponent<RawImage>();
            topBackgroundImage.material = UI_Red_Gradient_Radius_Mat_MaskPercent_45;
            TextMeshProUGUI subTitleText = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
            subTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.INVENTORY);

            CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");

            List<Currencies> currencies = new List<Currencies>();
            currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
            FindObjectOfType<CurrenciesManager>().GetMainCurrency(currencies, CurrencyPanel);
        }

        List<string> uniqueRaries = QualityEvaluator.rarities;
        List<string> uniqueTypes = await TypeManager.GetUniqueTypesAsync(mainType);

        if (uniqueRaries.Count > 0 && uniqueTypes.Count > 0 && !mainType.Equals(AppConstants.MainType.ITEM) && canUseRareButton)
        {
            for (int i = 0; i < uniqueRaries.Count; i++)
            {
                string selectedRare = uniqueRaries[i];
                string rareTemp = selectedRare;
                GameObject button = Instantiate(RareButtonPrefab, RightScrollViewContentPanel);

                TextMeshProUGUI buttonText = button.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                buttonText.text = LocalizationManager.Get(selectedRare);

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    OnRareTabButtonClick(button, rareTemp);
                });

                if (i == 0)
                {
                    if (buttonType.Equals("button1"))
                    {
                        rare = selectedRare;
                        button.transform.Find("Active").gameObject.SetActive(true);
                        button.transform.Find("Unactive").gameObject.SetActive(false);
                        _ = LoadCurrentPageAsync();
                    }
                }
                else
                {
                    if (buttonType.Equals("button1"))
                    {
                        button.transform.Find("Active").gameObject.SetActive(false);
                        button.transform.Find("Unactive").gameObject.SetActive(true);
                    }
                }
            }
        }

        if (uniqueTypes.Count > 0 && !mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subType = uniqueTypes[i];
                GameObject button = null;
                if (buttonType.Equals("button1"))
                {
                    button = Instantiate(buttonPrefab, LeftScrollViewContentPanel);
                    Text buttonText = button.GetComponentInChildren<Text>();
                    buttonText.text = subType.Replace("_", " ");
                }
                else if (buttonType.Equals("button2"))
                {
                    button = Instantiate(buttonPrefab2, LeftScrollViewContentPanel);
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
                    if (buttonType.Equals("button1"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    }
                    else if (buttonType.Equals("button2"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.SUMMON_BUTTON_AFTER_CLICK_URL);
                    }
                    _ = LoadCurrentPageAsync();

                }
                else
                {
                    if (buttonType.Equals("button1"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                    }
                    else if (buttonType.Equals("button2"))
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

        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            List<CardHeroes> cardHeroes = await UserCardHeroesService.Create().GetUserCardHeroesAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            listCount = cardHeroes.Count;

            totalRecord = await UserCardHeroesService.Create().GetUserCardHeroesCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            List<Books> books = await UserBooksService.Create().GetUserBooksAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            listCount = books.Count;

            totalRecord = await UserBooksService.Create().GetUserBooksCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> cardCaptains = await UserCardCaptainsService.Create().GetUserCardCaptainsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            listCount = cardCaptains.Count;

            totalRecord = await UserCardCaptainsService.Create().GetUserCardCaptainsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            List<CollaborationEquipments> collaborationEquipments = await UserCollaborationEquipmentsService.Create().GetUserCollaborationEquipmentsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCollaborationEquipmentsController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            listCount = collaborationEquipments.Count;

            totalRecord = await UserCollaborationEquipmentsService.Create().GetUserCollaborationEquipmentsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ITEM))
        {
            List<Items> items = await UserItemsService.Create().GetUserItemsAsync(User.CurrentUserId, type, pageSize, offset);
            Close(DictionaryContentPanel);
            UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            listCount = items.Count;

            totalRecord = await UserItemsService.Create().GetUserItemsCountAsync(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            List<Pets> pets = await UserPetsService.Create().GetUserPetsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            listCount = pets.Count;

            totalRecord = await UserPetsService.Create().GetUserPetsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            List<Skills> skills = await UserSkillsService.Create().GetUserSkillsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            listCount = skills.Count;

            totalRecord = await UserSkillsService.Create().GetUserSkillsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            List<Symbols> symbols = await UserSymbolsService.Create().GetUserSymbolsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            listCount = symbols.Count;

            totalRecord = await UserSymbolsService.Create().GetUserSymbolsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaries = await UserCardMilitariesService.Create().GetUserCardMilitariesAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardMilitariesController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            listCount = cardMilitaries.Count;

            totalRecord = await UserCardMilitariesService.Create().GetUserCardMilitariesCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpells = await UserCardSpellsService.Create().GetUserCardSpellsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardSpellsController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            listCount = cardSpells.Count;

            totalRecord = await UserCardSpellsService.Create().GetUserCardSpellsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            List<Collaborations> collaborations = await UserCollaborationsService.Create().GetUserCollaborationsAsync(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCollaborationsController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            listCount = collaborations.Count;

            totalRecord = await UserCollaborationsService.Create().GetUserCollaborationsCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            List<Medals> medals = await UserMedalsService.Create().GetUserMedalsAsync(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            listCount = medals.Count;

            totalRecord = await UserMedalsService.Create().GetUserMedalsCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            List<Titles> titles = await UserTitlesService.Create().GetUserTitlesAsync(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            listCount = titles.Count;

            totalRecord = await UserTitlesService.Create().GetUserTitlesCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            List<MagicFormationCircles> magicFormationCircles = await UserMagicFormationCirclesService.Create().GetUserMagicFormationCirclesAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserMagicFormationCirclesController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            listCount = magicFormationCircles.Count;

            totalRecord = await UserMagicFormationCirclesService.Create().GetUserMagicFormationCirclesCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            List<Relics> relics = await UserRelicsService.Create().GetUserRelicsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            listCount = relics.Count;

            totalRecord = await UserRelicsService.Create().GetUserRelicsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonsters = await UserCardMonstersService.Create().GetUserCardMonstersAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            listCount = cardMonsters.Count;

            totalRecord = await UserCardMonstersService.Create().GetUserCardMonstersCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonels = await UserCardColonelsService.Create().GetUserCardColonelsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            listCount = cardColonels.Count;

            totalRecord = await UserCardColonelsService.Create().GetUserCardColonelsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGenerals = await UserCardGeneralsService.Create().GetUserCardGeneralsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            listCount = cardGenerals.Count;

            totalRecord = await UserCardGeneralsService.Create().GetUserCardGeneralsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmirals = await UserCardAdmiralsService.Create().GetUserCardAdmiralsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            listCount = cardAdmirals.Count;

            totalRecord = await UserCardAdmiralsService.Create().GetUserCardAdmiralsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_HEROES))
        {
            List<CardHeroes> cardHeroes = await CardHeroesService.Create().GetCardHeroesRandomAsync(type, 3);
            UserCardHeroesController.Instance.CreateUserCardHeroesForSummon(cardHeroes, PositionPanel);



            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HEROES_TICKET) };
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HEROES_TICKET) },
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HEROES_TICKET) },
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

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HEROES_TICKET) };
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HEROES_TICKET) },
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HEROES_TICKET) },
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

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_CAPTAINS_TICKET) };
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_CAPTAINS_TICKET) },
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_CAPTAINS_TICKET) },
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

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MONSTERS_TICKET) };
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MONSTERS_TICKET) },
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MONSTERS_TICKET) },
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

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_COLONELS_TICKET) };
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_COLONELS_TICKET) },
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_COLONELS_TICKET) },
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

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_GENERALS_TICKET) };
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_GENERALS_TICKET) },
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_GENERALS_TICKET) },
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

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_ADMIRALS_TICKET) };
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_ADMIRALS_TICKET) },
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
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_ADMIRALS_TICKET) },
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
            List<Talismans> talismans = await UserTalismansService.Create().GetUserTalismansAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserTalismansController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            listCount = talismans.Count;

            totalRecord = await UserTalismansService.Create().GetUserTalismansCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            List<Puppets> puppets = await UserPuppetsService.Create().GetUserPuppetsAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserPuppetsController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            listCount = puppets.Count;

            totalRecord = await UserPuppetsService.Create().GetUserPuppetsCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            List<Alchemies> alchemies = await UserAlchemiesService.Create().GetUserAlchemiesAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserAlchemiesController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            listCount = alchemies.Count;

            totalRecord = await UserAlchemiesService.Create().GetUserAlchemiesCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            List<Forges> forges = await UserForgesService.Create().GetUserForgesAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserForgesController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            listCount = forges.Count;

            totalRecord = await UserForgesService.Create().GetUserForgesCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            List<CardLives> cardLives = await UserCardLivesService.Create().GetUserCardLivesAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardLivesController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            listCount = cardLives.Count;

            totalRecord = await UserCardLivesService.Create().GetUserCardLivesCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            List<Artworks> artworks = await UserArtworksService.Create().GetUserArtworksAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserArtworksController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
            listCount = artworks.Count;

            totalRecord = await UserArtworksService.Create().GetUserArtworksCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            List<SpiritBeasts> spiritBeasts = await UserSpiritBeastsService.Create().GetUserSpiritBeastsAsync(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSpiritBeastsController.Instance.CreateUserSpiritBeast(spiritBeasts, DictionaryContentPanel);
            listCount = spiritBeasts.Count;

            totalRecord = await UserSpiritBeastsService.Create().GetUserSpiritBeastsCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            List<SpiritCards> spiritCards = await UserSpiritCardsService.Create().GetUserSpiritCardAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSpiritCardsController.Instance.CreateUserSpiritCard(spiritCards, DictionaryContentPanel);
            listCount = spiritCards.Count;

            totalRecord = await UserSpiritCardsService.Create().GetUserSpiritCardCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD))
        {
            List<Cards> cards = await UserCardsService.Create().GetUserCardsAsync(User.CurrentUserId, pageSize, offset, rare);
            UserCardsController.Instance.CreateUserCards(cards, DictionaryContentPanel);
            listCount = cards.Count;

            totalRecord = await UserCardsService.Create().GetUserCardsCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            List<Architectures> architectures = await UserArchitecturesService.Create().GetUserArchitecturesAsync(User.CurrentUserId, pageSize, offset, rare);
            UserArchitecturesController.Instance.CreateUserArchitectures(architectures, DictionaryContentPanel);
            listCount = architectures.Count;

            totalRecord = await UserArchitecturesService.Create().GetUserArchitecturesCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            List<Technologies> technologies = await UserTechnologiesService.Create().GetUserTechnologiesAsync(User.CurrentUserId, pageSize, offset, rare);
            UserTechnologiesController.Instance.CreateUserTechnologies(technologies, DictionaryContentPanel);
            listCount = technologies.Count;

            totalRecord = await UserTechnologiesService.Create().GetUserTechnologiesCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            List<Vehicles> vehicles = await UserVehicleService.Create().GetUserVehiclesAsync(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserVehiclesController.Instance.CreateUserVehicle(vehicles, DictionaryContentPanel);
            listCount = vehicles.Count;

            totalRecord = await UserVehicleService.Create().GetUserVehiclesCountAsync(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            List<Cores> cores = await UserCoresService.Create().GetUserCoresAsync(User.CurrentUserId, pageSize, offset, rare);
            UserCoresController.Instance.CreateUserCores(cores, DictionaryContentPanel);
            listCount = cores.Count;

            totalRecord = await UserCoresService.Create().GetUserCoresCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            List<Weapons> weapons = await UserWeaponsService.Create().GetUserWeaponsAsync(User.CurrentUserId, pageSize, offset, rare);
            UserWeaponsController.Instance.CreateUserWeapons(weapons, DictionaryContentPanel);
            listCount = weapons.Count;

            totalRecord = await UserWeaponsService.Create().GetUserWeaponsCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            List<Robots> robots = await UserRobotsService.Create().GetUserRobotsAsync(User.CurrentUserId, pageSize, offset, rare);
            UserRobotsController.Instance.CreateUserRobots(robots, DictionaryContentPanel);
            listCount = robots.Count;

            totalRecord = await UserRobotsService.Create().GetUserRobotsCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            List<Badges> badges = await UserBadgesService.Create().GetUserBadgesAsync(User.CurrentUserId, pageSize, offset, rare);
            UserBadgesController.Instance.CreateUserBadges(badges, DictionaryContentPanel);
            listCount = badges.Count;

            totalRecord = await UserBadgesService.Create().GetUserBadgesCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            List<MechaBeasts> mechaBeasts = await UserMechaBeastsService.Create().GetUserMechaBeastsAsync(User.CurrentUserId, pageSize, offset, rare);
            UserMechaBeastsController.Instance.CreateUserMechaBeasts(mechaBeasts, DictionaryContentPanel);
            listCount = mechaBeasts.Count;

            totalRecord = await UserMechaBeastsService.Create().GetUserMechaBeastsCountAsync(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            List<Runes> runes = await UserRunesService.Create().GetUserRunesAsync(User.CurrentUserId, pageSize, offset, rare);
            UserRunesController.Instance.CreateUserRunes(runes, DictionaryContentPanel);
            listCount = runes.Count;

            totalRecord = await UserRunesService.Create().GetUserRunesCountAsync(User.CurrentUserId, rare);
        }

        if (listCount > 0)
        {
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        }
    }
    private void createEquipments(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.Name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.Image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(equipment, MainPanel));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = DictionaryContentPanel.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.Rare}");
            rareImage.texture = rareTexture;

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
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
            offset = offset + pageSize;
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
            offset = offset - pageSize;
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
