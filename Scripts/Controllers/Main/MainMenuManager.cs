using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

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
    //Variable for pagination
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private Text PageText;
    private Button NextButton;
    private Button PreviousButton;
    private string mainType;
    private string subType;
    private Text titleText;
    private TextMeshProUGUI titleText2;
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
        // mainMenuPanel = UIManager.Instance.GetTransform("mainMenuButtonPanel");
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
        ReactorPanelPrefab = UIManager.Instance.GetGameObjectScienceFiction("ReactorPanelPrefab");
        MasterBoardPanelPrefab = UIManager.Instance.GetGameObject("MasterBoardPanelPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
        fontSize = 24;
    }
    public void CreateMainPanel()
    {
        currentObject = Instantiate(MainPanelPrefab, RootPanel);
        // ButtonLoader.Instance.CreateMainButton(currentObject);
        // GetMainButtonEvent();
        GetPrimaryButtonEvent();

        Button inventoryButton = currentObject.transform.Find("MainPanel/MainButtonGroup/InventoryButton").GetComponent<Button>();
        Button eventButton = currentObject.transform.Find("MainPanel/MainButtonGroup/EventButton").GetComponent<Button>();
        Button campaignButton = currentObject.transform.Find("MainPanel/MainButtonGroup/CampaignButton").GetComponent<Button>();
        Button shopButton = currentObject.transform.Find("MainPanel/MainButtonGroup/ShopButton").GetComponent<Button>();

        inventoryButton.onClick.AddListener(() =>
        {
            GameObject popupButtonPanel = Instantiate(PopupButtonPrefab, MainPanel);
            CloseButton = popupButtonPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupButtonPanel);
            });
            HomeButton = popupButtonPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateInventoryButton(popupButtonPanel);
            GetMainButtonEvent(popupButtonPanel);
        });

        eventButton.onClick.AddListener(() =>
        {
            GameObject popupButtonPanel = Instantiate(PopupButtonPrefab, MainPanel);
            CloseButton = popupButtonPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupButtonPanel);
            });
            HomeButton = popupButtonPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateEventButton(popupButtonPanel);
            GetEventButtonEvent(popupButtonPanel);
        });

        shopButton.onClick.AddListener(() =>
        {
            ShopManager.Instance.CreateShopButton();
        });
    }
    public Transform GetSummonPanel()
    {
        Transform summonPanel = currentObject.transform.Find("SummonPanel");
        return summonPanel;
    }
    public void CreateMainPanelUserInformation(AuthResult authResult)
    {
        Transform userPanel = currentObject.transform.Find("User");
        Transform currencyPanel = currentObject.transform.Find("Currency");
        Text nameText = userPanel.transform.Find("NameText").GetComponent<Text>();
        nameText.text = authResult.User.Name;
        Text levelText = userPanel.transform.Find("LevelText").GetComponent<Text>();
        levelText.text = authResult.User.Level.ToString();
        Text powerText = userPanel.transform.Find("PowerText").GetComponent<Text>();
        powerText.text = authResult.User.Power.ToString();
        RawImage avatarImage = userPanel.transform.Find("AvatarImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = authResult.User.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        avatarImage.texture = texture;

        RawImage borderImage = userPanel.transform.Find("BorderImage").GetComponent<RawImage>();

        fileNameWithoutExtension = authResult.User.Border.Replace(".png", "");
        Texture borderTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        borderImage.texture = borderTexture;

        FindObjectOfType<CurrencyManager>().GetMainCurrency(authResult.User.Currencies, currencyPanel);
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
        ButtonEvent.Instance.AssignButtonEvent("Button_25", contentPanel, () => GetType(AppConstants.MainType.MASTER_BOARD));
        ButtonEvent.Instance.AssignButtonEvent("Button_26", contentPanel, () => GetType(AppConstants.MainType.ARTWORK));
        ButtonEvent.Instance.AssignButtonEvent("Button_27", contentPanel, () => GetType(AppConstants.MainType.SPIRIT_BEAST));
        ButtonEvent.Instance.AssignButtonEvent("Button_28", contentPanel, () => GetType(AppConstants.MainType.SCIENCE_FICTION));
        ButtonEvent.Instance.AssignButtonEvent("Button_29", contentPanel, () => GetType(AppConstants.MainType.SPIRIT_CARD));
        ButtonEvent.Instance.AssignButtonEvent("Button_30", contentPanel, () => GetType(AppConstants.MainType.TEAMS));  
    }
    public void GetPrimaryButtonEvent()
    {
        Transform mainButtonGroupPanel = currentObject.transform.Find("MainPanel/MainButtonGroup");
        mainButtonGroupPanel.gameObject.SetActive(true);
    }
    public void GetEventButtonEvent(GameObject popupButtonObject)
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

        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, () => GetType(AppConstants.MainType.GALLERY));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, () => GetType(AppConstants.MainType.COLLECTION));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, () => GetType(AppConstants.MainType.EQUIPMENT));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, () => GetType(AppConstants.MainType.ANIME));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, () => GetType(AppConstants.MainType.ARENA));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, () => GetType(AppConstants.MainType.GUILD));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, () => GetType(AppConstants.MainType.TOWER));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, () => GetType(AppConstants.MainType.EVENT));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, () => GetType(AppConstants.MainType.DAILY_CHECKIN));
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, () => GetType(AppConstants.Market.RARE_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, () => GetType(AppConstants.Market.ULTRA_RARE_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, () => GetType(AppConstants.Market.LEGENDARY_MARKET));
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, () => GetType(AppConstants.Market.MYSTIC_MARKET));
    }
    public void GetType(string type)
    {
        mainType = type; // Gán giá trị cho mainType
        GetButtonType(); // Gọi hàm xử lý
        if (titleText != null)
        {
            titleText.text = LocalizationManager.Get(type);
        }
    }
    public void GetButtonType()
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
            titleText2 = summonObject.transform.Find("DictionaryCards/TitleText2").GetComponent<TextMeshProUGUI>();
            CloseButton = summonObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            SummonButton = summonObject.transform.Find("DictionaryCards/SummonButton").GetComponent<Button>();
            Summon10Button = summonObject.transform.Find("DictionaryCards/Summon10Button").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(summonObject);
            });
            HomeButton = summonObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
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
            RawImage rawImage1 = summonObject.transform.Find("DictionaryCards/RawImage1").GetComponent<RawImage>();
            RawImage rawImage2 = summonObject.transform.Find("DictionaryCards/RawImage2").GetComponent<RawImage>();
            RawImage background2 = summonObject.transform.Find("DictionaryCards/Background2").GetComponent<RawImage>();
            if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_HEROES))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_52");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_5");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_BOOKS) || mainType.Equals(AppConstants.MainType.SUMMON_CARD_COLONELS))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_51");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_6");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_CAPTAINS) || mainType.Equals(AppConstants.MainType.SUMMON_CARD_GENERALS))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_50");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_7");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_MONSTERS) || mainType.Equals(AppConstants.MainType.SUMMON_CARD_ADMIRALS))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_49");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_8");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_MILITARY))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_48");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_9");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_SPELLS))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_47");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_10");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.GALLERY))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.GALLERY);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupObject);
            });
            ButtonLoader.Instance.CreateGalleryButton(popupObject.transform.Find("Content"));
            Transform scrollViewPanel = popupObject.transform.Find("Scroll View");
            scrollViewPanel.gameObject.SetActive(false);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLECTION))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.COLLECTION);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupObject);
            });
            ButtonLoader.Instance.CreateCollectionButton(popupObject.transform.Find("Content"));
            Transform scrollViewPanel = popupObject.transform.Find("Scroll View");
            scrollViewPanel.gameObject.SetActive(false);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.EQUIPMENTS);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupObject);
            });
            ButtonLoader.Instance.CreateEquipmentsButton(popupObject.transform.Find("Content"));
            Transform scrollViewPanel = popupObject.transform.Find("Scroll View");
            scrollViewPanel.gameObject.SetActive(false);
        }
        else if (mainType.Equals(AppConstants.MainType.ANIME))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(AnimePanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.SCIENCE_FICTION))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(ReactorPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateScienceFictionButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
            ScienceFictionManager.Instance.GetScienceFictionButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.ARENA))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateArenaButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.GUILD))
        {
            canUseRareButton = false;
            // GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            // titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            // CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // CloseButton.onClick.AddListener(() => Close(MainPanel));
            // HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            // HomeButton.onClick.AddListener(() => Close(MainPanel));
            // FindObjectOfType<ButtonLoader>().CreateArenaButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.TOWER))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Close(MainPanel);
            });
            ButtonLoader.Instance.CreateTowerButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.EVENT))
        {
            canUseRareButton = false;
        }
        else if (mainType.Equals(AppConstants.MainType.MASTER_BOARD))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(MasterBoardPanelPrefab, MainPanel);
            TextMeshProUGUI titleTMPText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupObject);
            });
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Close(MainPanel);
            });
            titleTMPText.text = LocalizationManager.Get(AppConstants.MainType.MASTER_BOARD);
            // FindObjectOfType<ButtonLoader>().CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
            MasterBoardController.Instance.CreateMasterBoard(popupObject);
        }
        else if (mainType.Equals(AppConstants.MainType.TEAMS))
        {
            canUseRareButton = false;
            TeamsManager.Instance.CreateTeams();
        }
        else if (mainType.Equals(AppConstants.MainType.DAILY_CHECKIN))
        {
            canUseRareButton = false;
            DailyCheckinManager.Instance.CreateDailyCheckinGroup();
        }
        else if (mainType.Equals(AppConstants.Market.RARE_MARKET))
        {
            canUseRareButton = false;
            RareMarketManager.Instance.CreateRareMarket();
        }
        else if (mainType.Equals(AppConstants.Market.ULTRA_RARE_MARKET))
        {
            canUseRareButton = false;
            UltraRareMarketManager.Instance.CreateUltraRareMarket();
        }
        else if (mainType.Equals(AppConstants.Market.LEGENDARY_MARKET))
        {
            canUseRareButton = false;
            LegendaryMarketManager.Instance.CreateLegendaryMarket();
        }
        else if (mainType.Equals(AppConstants.Market.MYSTIC_MARKET))
        {
            canUseRareButton = false;
            MysticMarketManager.Instance.CreateMysticMarket();
        }
        else
        {
            buttonType = "button1";
            canUseRareButton = true;
            GameObject mainMenuObject = Instantiate(DictionaryPanel, MainPanel);
            DictionaryContentPanel = mainMenuObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            RightScrollViewContentPanel = mainMenuObject.transform.Find("RightScrollView/Viewport/Content");
            LeftScrollViewContentPanel = mainMenuObject.transform.Find("Scroll View/Viewport/ButtonContent");
            PageText = mainMenuObject.transform.Find("Pagination/Page").GetComponent<Text>();
            NextButton = mainMenuObject.transform.Find("Pagination/Next").GetComponent<Button>();
            PreviousButton = mainMenuObject.transform.Find("Pagination/Previous").GetComponent<Button>();
            titleText = mainMenuObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = mainMenuObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                ClosePanel();
            });
            HomeButton = mainMenuObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Close(MainPanel);
            });
            NextButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
                ChangeNextPage();
            });
            PreviousButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
                ChangePreviousPage();
            });

            CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");

            List<Currencies> currencies = new List<Currencies>();
            currencies = UserCurrencyService.Create().GetUserCurrency();
            FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);
        }

        List<string> uniqueRaries = QualityEvaluator.rarities;
        List<string> uniqueTypes = TypeManager.GetUniqueTypes(mainType);

        if (uniqueRaries.Count > 0 && uniqueTypes.Count > 0 && !mainType.Equals(AppConstants.MainType.ITEM) && canUseRareButton)
        {
            for (int i = 0; i < uniqueRaries.Count; i++)
            {
                string selectedRare = uniqueRaries[i];
                string rareTemp = selectedRare;
                GameObject button = Instantiate(buttonPrefab, RightScrollViewContentPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = LocalizationManager.Get(selectedRare);

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    OnRareTabButtonClick(button, rareTemp);
                });

                if (i == 0)
                {
                    if (buttonType.Equals("button1"))
                    {
                        rare = selectedRare;
                        ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.RARE_BUTTON_AFTER_CLICK_URL);
                        LoadCurrentPage();
                    }
                }
                else
                {
                    if (buttonType.Equals("button1"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.RARE_BUTTON_BEFORE_CLICK_URL);
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
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
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
                    LoadCurrentPage();

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
            int totalRecord = 0;
            int listCount = 0;
            if (mainType.Equals(AppConstants.MainType.COLLABORATION))
            {
                List<Collaborations> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset, rare);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
                listCount = collaborations.Count;

                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId, rare);
            }
            else if (mainType.Equals(AppConstants.MainType.MEDAL))
            {
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset, rare);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
                listCount = medals.Count;

                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId, rare);
            }
            else if (mainType.Equals(AppConstants.MainType.TITLE))
            {
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset, rare);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
                listCount = titles.Count;

                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
            }
            else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
            {
                List<SpiritBeasts> spiritBeasts = UserSpiritBeastService.Create().GetUserSpiritBeast(User.CurrentUserId, pageSize, offset, rare);
                UserSpiritBeastController.Instance.CreateUserSpiritBeast(spiritBeasts, DictionaryContentPanel);
                listCount = spiritBeasts.Count;

                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
            }

            if (listCount > 0)
            {
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            }
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
                            rare = QualityEvaluator.rarities[0]; // hoặc AppConstants.MainType.All
                            ButtonLoader.Instance.ChangeButtonBackground(child.gameObject, ImageConstants.Button.RARE_BUTTON_AFTER_CLICK_URL);
                        }
                        else
                        {
                            ButtonLoader.Instance.ChangeButtonBackground(child.gameObject, ImageConstants.Button.RARE_BUTTON_BEFORE_CLICK_URL);
                        }
                    }
                }
            }
        }
        else if (buttonType.Equals("button2"))
        {
            ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.SUMMON_BUTTON_AFTER_CLICK_URL);
        }
        LoadCurrentPage();
    }
    public void OnRareTabButtonClick(GameObject clickedButton, string selectedRare)
    {
        foreach (Transform child in RightScrollViewContentPanel)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.RARE_BUTTON_BEFORE_CLICK_URL);
            }
        }

        rare = selectedRare;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.RARE_BUTTON_AFTER_CLICK_URL);
        LoadCurrentPage();
    }
    public void LoadCurrentPage()
    {
        int totalRecord = 0;
        int listCount = 0;
        // offset = 0;

        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            listCount = cardHeroes.Count;

            totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            listCount = books.Count;

            totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            listCount = cardCaptains.Count;

            totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            List<CollaborationEquipments> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            listCount = collaborationEquipments.Count;

            totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ITEM))
        {
            List<Items> items = UserItemsService.Create().GetUserItems(User.CurrentUserId, type, pageSize, offset);
            Close(DictionaryContentPanel);
            UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            listCount = items.Count;

            totalRecord = UserItemsService.Create().GetUserItemCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            listCount = pets.Count;

            totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            listCount = skills.Count;

            totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            listCount = symbols.Count;

            totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            listCount = cardMilitaries.Count;

            totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            listCount = cardSpells.Count;

            totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            List<Collaborations> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            listCount = collaborations.Count;

            totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            listCount = medals.Count;

            totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            listCount = titles.Count;

            totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            List<MagicFormationCircles> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            listCount = magicFormationCircles.Count;

            totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            listCount = relics.Count;

            totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            listCount = cardMonsters.Count;

            totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            listCount = cardColonels.Count;

            totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            listCount = cardGenerals.Count;

            totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            listCount = cardAdmirals.Count;

            totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SUMMON_CARD_HEROES))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardHeroes> cardHeroes = CardHeroesService.Create().GetCardHeroesRandom(type, 3);
            UserCardHeroesController.Instance.CreateUserCardHeroesForSummon(cardHeroes, PositionPanel);



            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_HEROES_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_HEROES_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_HEROES_TICKET) },
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
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            List<Books> books = BooksService.Create().GetBooksRandom(type, 3);
            UserBooksController.Instance.CreateUserBooksForSummon(books, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_HEROES_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_HEROES_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_HEROES_TICKET) },
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
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptainsRandom(type, 3);
            UserCardCaptainsController.Instance.CreateUserCardCaptainsForSummon(cardCaptains, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_CAPTAINS_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_CAPTAINS_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_CAPTAINS_TICKET) },
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
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardMilitaries> cardMilitaries = CardMilitaryService.Create().GetCardMilitaryRandom(type, 3);
            UserCardMilitaryController.Instance.CreateUserCardMilitaryForSummon(cardMilitaries, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_MILITARY_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_MILITARY_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_MILITARY_TICKET) },
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
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardSpells> cardSpells = CardSpellService.Create().GetCardSpellRandom(type, 3);
            UserCardSpellController.Instance.CreateUserCardSpellForSummon(cardSpells, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_SPELL_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_SPELL_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_SPELL_TICKET) },
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
            titleText2.text = "Summon " + string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonstersRandom(type, 3);
            UserCardMonstersController.Instance.CreateUserCardMonstersForSummon(cardMonsters, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_MONSTERS_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_MONSTERS_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_MONSTERS_TICKET) },
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
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonelsRandom(type, 3);
            UserCardColonelsController.Instance.CreateUserCardColonelsForSummon(cardColonels, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_COLONELS_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_COLONELS_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_COLONELS_TICKET) },
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
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGeneralsRandom(type, 3);
            UserCardGeneralsController.Instance.CreateUserCardGeneralsForSummon(cardGenerals, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_GENERALS_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_GENERALS_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_GENERALS_TICKET) },
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
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmiralsRandom(type, 3);
            UserCardAdmiralsController.Instance.CreateUserCardAdmiralsForSummon(cardAdmirals, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_ADMIRALS_TICKET) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_ADMIRALS_TICKET) },
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CARD_ADMIRALS_TICKET) },
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
            List<Talismans> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            listCount = talismans.Count;

            totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            List<Puppets> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            listCount = puppets.Count;

            totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            List<Alchemies> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            listCount = alchemies.Count;

            totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            List<Forges> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            listCount = forges.Count;

            totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            List<CardLives> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            listCount = cardLives.Count;

            totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            List<Artworks> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
            listCount = artworks.Count;

            totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            List<SpiritBeasts> spiritBeasts = UserSpiritBeastService.Create().GetUserSpiritBeast(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSpiritBeastController.Instance.CreateUserSpiritBeast(spiritBeasts, DictionaryContentPanel);
            listCount = spiritBeasts.Count;

            totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            List<SpiritCards> spiritCards = UserSpiritCardService.Create().GetUserSpiritCard(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSpiritCardController.Instance.CreateUserSpiritCard(spiritCards, DictionaryContentPanel);
            listCount = spiritCards.Count;

            totalRecord = UserSpiritCardService.Create().GetUserSpiritCardCount(User.CurrentUserId, type, rare);
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
            int totalRecord = 0;

            if (mainType.Equals(AppConstants.MainType.CARD_HERO))
            {
                totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.BOOK))
            {
                totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subType, pageSize, offset, rare);
                UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
            {
                totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
            {
                totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipments> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
            {
                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaborations> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset, rare);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.MEDAL))
            {
                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset, rare);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
            {
                totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.PET))
            {
                totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, subType, pageSize, offset, rare);
                UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SKILL))
            {
                totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SYMBOL))
            {
                totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.TITLE))
            {
                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset, rare);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
            {
                totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitaries> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
            {
                totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpells> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
            {
                totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircles> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset, rare);
                UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.RELIC))
            {
                totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset, rare);
                UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.TALISMAN))
            {
                totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talismans> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset, rare);
                UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.PUPPET))
            {
                totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppets> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset, rare);
                UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
            {
                totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemies> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset, rare);
                UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.FORGE))
            {
                totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forges> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset, rare);
                UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
            {
                totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
            {
                totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
            {
                totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
            {
                totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLives> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.ARTWORK))
            {
                totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Artworks> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, subType, pageSize, offset, rare);
                UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.ITEM))
            {
                totalRecord = UserItemsService.Create().GetUserItemCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Items> items = UserItemsService.Create().GetUserItems(User.CurrentUserId, subType, pageSize, offset);
                UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
            {
                totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<SpiritBeasts> spiritBeasts = UserSpiritBeastService.Create().GetUserSpiritBeast(User.CurrentUserId, pageSize, offset, rare);
                UserSpiritBeastController.Instance.CreateUserSpiritBeast(spiritBeasts, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
            {
                totalRecord = UserSpiritCardService.Create().GetUserSpiritCardCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<SpiritCards> spiritCards = UserSpiritCardService.Create().GetUserSpiritCard(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSpiritCardController.Instance.CreateUserSpiritCard(spiritCards, DictionaryContentPanel);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage()
    {
        if (currentPage > 1)
        {
            ClearAllPrefabs();
            int totalRecord = 0;

            if (mainType.Equals(AppConstants.MainType.CARD_HERO))
            {
                totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.BOOK))
            {
                totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subType, pageSize, offset, rare);
                UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
            {
                totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
            {
                totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipments> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
            {
                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaborations> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset, rare);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.MEDAL))
            {
                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset, rare);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
            {
                totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.PET))
            {
                totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, subType, pageSize, offset, rare);
                UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SKILL))
            {
                totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SYMBOL))
            {
                totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.TITLE))
            {
                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset, rare);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
            {
                totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitaries> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
            {
                totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpells> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
            {
                totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircles> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset, rare);
                UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.RELIC))
            {
                totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset, rare);
                UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.TALISMAN))
            {
                totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talismans> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset, rare);
                UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.PUPPET))
            {
                totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppets> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset, rare);
                UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
            {
                totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemies> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset, rare);
                UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.FORGE))
            {
                totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forges> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset, rare);
                UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
            {
                totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
            {
                totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
            {
                totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
            {
                totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLives> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.ARTWORK))
            {
                totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Artworks> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, subType, pageSize, offset, rare);
                UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.ITEM))
            {
                totalRecord = UserItemsService.Create().GetUserItemCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Items> items = UserItemsService.Create().GetUserItems(User.CurrentUserId, subType, pageSize, offset);
                UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
            {
                totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<SpiritBeasts> spiritBeasts = UserSpiritBeastService.Create().GetUserSpiritBeast(User.CurrentUserId, pageSize, offset, rare);
                UserSpiritBeastController.Instance.CreateUserSpiritBeast(spiritBeasts, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
            {
                totalRecord = UserSpiritCardService.Create().GetUserSpiritCardCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<SpiritCards> spiritCards = UserSpiritCardService.Create().GetUserSpiritCard(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSpiritCardController.Instance.CreateUserSpiritCard(spiritCards, DictionaryContentPanel);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ClosePanel()
    {
        ClearAllButton();
        ClearAllPrefabs();
        offset = 0;
        currentPage = 1;
        foreach (Transform child in MainPanel)
        {
            Destroy(child.gameObject);
        }
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
