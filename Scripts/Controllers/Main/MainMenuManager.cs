using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    private Transform mainMenuPanel;
    private Transform mainMenuCampaignPanel;
    private GameObject buttonPrefab;
    private GameObject DictionaryPanel;
    private GameObject PopupMenuPanelPrefab;
    private GameObject ArenaPanelPrefab;
    private GameObject AnimePanelPrefab;
    private GameObject ReactorPanelPrefab;
    private GameObject MasterBoardPanelPrefab;
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
    private Transform SummonMainMenuPanel;
    private Transform CurrencyPanel;
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
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        buttonType = "";
        rare = AppConstants.Rare.All;
        mainMenuPanel = UIManager.Instance.GetTransform("mainMenuButtonPanel");
        mainMenuCampaignPanel = UIManager.Instance.GetTransform("mainMenuCampaignPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        buttonPrefab2 = UIManager.Instance.GetGameObject("TabButton2");
        DictionaryPanel = UIManager.Instance.GetGameObject("DictionaryPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        SummonPanel = UIManager.Instance.GetGameObject("SummonPanelPrefab");
        SummonMainMenuPanel = UIManager.Instance.GetTransform("summonPanel");
        PopupMenuPanelPrefab = UIManager.Instance.GetGameObject("PopupMenuPanelPrefab");
        ArenaPanelPrefab = UIManager.Instance.GetGameObject("ArenaPanelPrefab");
        AnimePanelPrefab = UIManager.Instance.GetGameObject("AnimePanelPrefab");
        ReactorPanelPrefab = UIManager.Instance.GetGameObjectScienceFiction("ReactorPanelPrefab");
        MasterBoardPanelPrefab = UIManager.Instance.GetGameObject("MasterBoardPanelPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
        fontSize = 24;

        ButtonEvent.Instance.AssignButtonEvent("Button_1", mainMenuCampaignPanel, () => loadScence());

        ButtonEvent.Instance.AssignButtonEvent("Button_1", mainMenuPanel, () => GetType(AppConstants.MainType.CardHero));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", mainMenuPanel, () => GetType(AppConstants.MainType.Book));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", mainMenuPanel, () => GetType(AppConstants.MainType.Pet));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", mainMenuPanel, () => GetType(AppConstants.MainType.CardCaptain));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", mainMenuPanel, () => GetType(AppConstants.MainType.CardColonel));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", mainMenuPanel, () => GetType(AppConstants.MainType.CardGeneral));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", mainMenuPanel, () => GetType(AppConstants.MainType.CardAdmiral));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", mainMenuPanel, () => GetType(AppConstants.MainType.CardMilitary));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", mainMenuPanel, () => GetType(AppConstants.MainType.CardSpell));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", mainMenuPanel, () => GetType(AppConstants.MainType.CardMonster));
        // Button_13 Equipments có thể được thêm lại nếu cần
        ButtonEvent.Instance.AssignButtonEvent("Button_11", mainMenuPanel, () => GetType(AppConstants.MainType.Item));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", mainMenuPanel, () => GetType(AppConstants.MainType.Teams));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", mainMenuPanel, () => GetType(AppConstants.MainType.More));

        ButtonEvent.Instance.AssignButtonEvent("Button_14", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonCardHeroes));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonBooks));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonCardCaptains));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonCardMonsters));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonCardMilitaries));
        ButtonEvent.Instance.AssignButtonEvent("Button_19", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonCardSpells));
        ButtonEvent.Instance.AssignButtonEvent("Button_20", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonCardColonels));
        ButtonEvent.Instance.AssignButtonEvent("Button_21", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonCardGenerals));
        ButtonEvent.Instance.AssignButtonEvent("Button_22", SummonMainMenuPanel, () => GetType(AppConstants.MainType.SummonCardAdmirals));

        ButtonEvent.Instance.AssignButtonEvent("Button_24", SummonMainMenuPanel, () => GetType(AppConstants.MainType.Gallery));
        ButtonEvent.Instance.AssignButtonEvent("Button_25", SummonMainMenuPanel, () => GetType(AppConstants.MainType.Collection));
        ButtonEvent.Instance.AssignButtonEvent("Button_26", SummonMainMenuPanel, () => GetType(AppConstants.MainType.Equipment));
        ButtonEvent.Instance.AssignButtonEvent("Button_27", SummonMainMenuPanel, () => GetType(AppConstants.MainType.Anime));
        ButtonEvent.Instance.AssignButtonEvent("Button_28", SummonMainMenuPanel, () => GetType(AppConstants.MainType.Arena));
        ButtonEvent.Instance.AssignButtonEvent("Button_29", SummonMainMenuPanel, () => GetType(AppConstants.MainType.Guild));
        ButtonEvent.Instance.AssignButtonEvent("Button_30", SummonMainMenuPanel, () => GetType(AppConstants.MainType.Tower));
        ButtonEvent.Instance.AssignButtonEvent("Button_31", SummonMainMenuPanel, () => GetType(AppConstants.MainType.Event));
        ButtonEvent.Instance.AssignButtonEvent("Button_32", SummonMainMenuPanel, () => GetType(AppConstants.MainType.DailyCheckin));
        ButtonEvent.Instance.AssignButtonEvent("Button_33", SummonMainMenuPanel, () => GetType(AppConstants.Market.RareMarket));
        ButtonEvent.Instance.AssignButtonEvent("Button_34", SummonMainMenuPanel, () => GetType(AppConstants.Market.UltraRareMarket));
        ButtonEvent.Instance.AssignButtonEvent("Button_35", SummonMainMenuPanel, () => GetType(AppConstants.Market.LegendaryMarket));
        ButtonEvent.Instance.AssignButtonEvent("Button_36", SummonMainMenuPanel, () => GetType(AppConstants.Market.MysticMarket));
        // GetCardsType();
    }

    void Update()
    {

    }
    public void GetMoreButtonEvent(Transform moreMenuPanel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", moreMenuPanel, () => GetType(AppConstants.MainType.CollaborationEquipment));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", moreMenuPanel, () => GetType(AppConstants.MainType.Collaboration));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", moreMenuPanel, () => GetType(AppConstants.MainType.Medal));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", moreMenuPanel, () => GetType(AppConstants.MainType.Skill));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", moreMenuPanel, () => GetType(AppConstants.MainType.Symbol));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", moreMenuPanel, () => GetType(AppConstants.MainType.Title));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", moreMenuPanel, () => GetType(AppConstants.MainType.MagicFormationCircle));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", moreMenuPanel, () => GetType(AppConstants.MainType.Relic));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", moreMenuPanel, () => GetType(AppConstants.MainType.Talisman));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", moreMenuPanel, () => GetType(AppConstants.MainType.Puppet));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", moreMenuPanel, () => GetType(AppConstants.MainType.Alchemy));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", moreMenuPanel, () => GetType(AppConstants.MainType.Forge));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", moreMenuPanel, () => GetType(AppConstants.MainType.CardLife));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", moreMenuPanel, () => GetType(AppConstants.MainType.MasterBoard));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", moreMenuPanel, () => GetType(AppConstants.MainType.Artwork));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", moreMenuPanel, () => GetType(AppConstants.MainType.SpiritBeast));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", moreMenuPanel, () => GetType(AppConstants.MainType.ScienceFiction));
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
        if (mainType.Equals(AppConstants.MainType.SummonCardHeroes) || mainType.Equals(AppConstants.MainType.SummonBooks) || mainType.Equals(AppConstants.MainType.SummonCardCaptains) ||
        mainType.Equals(AppConstants.MainType.SummonCardColonels) || mainType.Equals(AppConstants.MainType.SummonCardGenerals) || mainType.Equals(AppConstants.MainType.SummonCardAdmirals) ||
        mainType.Equals(AppConstants.MainType.SummonCardMilitaries) || mainType.Equals(AppConstants.MainType.SummonCardMonsters) || mainType.Equals(AppConstants.MainType.SummonCardSpells))
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
            CloseButton.onClick.AddListener(ClosePanel);
            HomeButton = summonObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            // SummonAreaPanel = summonObject.transform.Find("SummonArea");
            CurrencyPanel = summonObject.transform.Find("DictionaryCards/Currency");

            TextMeshProUGUI SummonOneButtonText = SummonButton.GetComponentInChildren<TextMeshProUGUI>();
            SummonOneButtonText.font = EuroStyleNormalFont;
            SummonOneButtonText.fontSize = fontSize;
            SummonOneButtonText.fontStyle = FontStyles.Bold;
            SummonOneButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SummonOne);
            TextMeshProUGUI SummonTenButtonText = Summon10Button.GetComponentInChildren<TextMeshProUGUI>();
            SummonTenButtonText.font = EuroStyleNormalFont;
            SummonTenButtonText.fontSize = fontSize;
            SummonTenButtonText.fontStyle = FontStyles.Bold;
            SummonTenButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SummonTen);

            RawImage dictionaryBackground = summonObject.transform.Find("DictionaryBackground").GetComponent<RawImage>();
            RawImage rawImage1 = summonObject.transform.Find("DictionaryCards/RawImage1").GetComponent<RawImage>();
            RawImage rawImage2 = summonObject.transform.Find("DictionaryCards/RawImage2").GetComponent<RawImage>();
            RawImage background2 = summonObject.transform.Find("DictionaryCards/Background2").GetComponent<RawImage>();
            if (mainType.Equals(AppConstants.MainType.SummonCardHeroes))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_52");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_5");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SummonBooks) || mainType.Equals(AppConstants.MainType.SummonCardColonels))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_51");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_6");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SummonCardCaptains) || mainType.Equals(AppConstants.MainType.SummonCardGenerals))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_50");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_7");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SummonCardMonsters) || mainType.Equals(AppConstants.MainType.SummonCardAdmirals))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_49");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_8");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SummonCardMilitaries))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_48");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_9");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.MainType.SummonCardSpells))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_47");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_10");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
        }
        else if (mainType.Equals(AppConstants.MainType.Gallery))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.Gallery);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateGalleryButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.Collection))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.Collection);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateCollectionButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.Equipment))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.Equipments);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateEquipmentsButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.Anime))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(AnimePanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.ScienceFiction))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(ReactorPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateScienceFictionButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.Arena))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateArenaButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.Guild))
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
        else if (mainType.Equals(AppConstants.MainType.Tower))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateTowerButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.Event))
        {
            canUseRareButton = false;
        }
        else if (mainType.Equals(AppConstants.MainType.MasterBoard))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(MasterBoardPanelPrefab, MainPanel);
            TextMeshProUGUI titleTMPText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            titleTMPText.text = LocalizationManager.Get(AppConstants.MainType.MasterBoard);
            // FindObjectOfType<ButtonLoader>().CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
            MasterBoardController.Instance.CreateMasterBoard(popupObject);
        }
        else if (mainType.Equals(AppConstants.MainType.Teams))
        {
            canUseRareButton = false;
            TeamsManager.Instance.CreateTeams();
        }
        else if (mainType.Equals(AppConstants.MainType.More))
        {
            canUseRareButton = false;
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.MainType.More);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateMoreButton(popupObject.transform.Find("Content"));
            GetMoreButtonEvent(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals(AppConstants.MainType.DailyCheckin))
        {
            canUseRareButton = false;
            DailyCheckinManager.Instance.CreateDailyCheckinGroup();
        }
        else if (mainType.Equals(AppConstants.Market.RareMarket))
        {
            canUseRareButton = false;
            RareMarketManager.Instance.CreateRareMarket();
        }
        else if (mainType.Equals(AppConstants.Market.UltraRareMarket))
        {
            canUseRareButton = false;
            UltraRareMarketManager.Instance.CreateUltraRareMarket();
        }
        else if (mainType.Equals(AppConstants.Market.LegendaryMarket))
        {
            canUseRareButton = false;
            LegendaryMarketManager.Instance.CreateLegendaryMarket();
        }
        else if (mainType.Equals(AppConstants.Market.MysticMarket))
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
            CloseButton.onClick.AddListener(ClosePanel);
            HomeButton = mainMenuObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            NextButton.onClick.AddListener(ChangeNextPage);
            PreviousButton.onClick.AddListener(ChangePreviousPage);

            CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");

            List<Currency> currencies = new List<Currency>();
            currencies = UserCurrencyService.Create().GetUserCurrency();
            FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);
        }

        List<string> uniqueRaries = QualityEvaluator.rarities;
        List<string> uniqueTypes = TypeManager.GetUniqueTypes(mainType);

        if (uniqueRaries.Count > 0 && uniqueTypes.Count > 0 && !mainType.Equals(AppConstants.MainType.Item) && canUseRareButton)
        {
            for (int i = 0; i < uniqueRaries.Count; i++)
            {
                string selectedRare = uniqueRaries[i];
                string rareTemp = selectedRare;
                GameObject button = Instantiate(buttonPrefab, RightScrollViewContentPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = LocalizationManager.Get(selectedRare);

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnRareTabButtonClick(button, rareTemp));

                if (i == 0)
                {
                    if (buttonType.Equals("button1"))
                    {
                        rare = selectedRare;
                        ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_84_2");
                        LoadCurrentPage();
                    }
                }
                else
                {
                    if (buttonType.Equals("button1"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_84_1");
                    }
                }
            }
        }

        if (uniqueTypes.Count > 0 && !mainType.Equals(AppConstants.MainType.Equipment))
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
                btn.onClick.AddListener(() => OnButtonClick(button, subType));

                if (i == 0)
                {
                    type = subType;
                    if (buttonType.Equals("button1"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_166");
                    }
                    else if (buttonType.Equals("button2"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_211");
                    }
                    LoadCurrentPage();

                }
                else
                {
                    if (buttonType.Equals("button1"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_167");
                    }
                    else if (buttonType.Equals("button2"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_210");
                    }
                }
            }
        }
        else
        {
            int totalRecord = 0;
            int listCount = 0;
            if (mainType.Equals(AppConstants.MainType.Collaboration))
            {
                List<Collaboration> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset, rare);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
                listCount = collaborations.Count;

                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId, rare);
            }
            else if (mainType.Equals(AppConstants.MainType.Medal))
            {
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset, rare);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
                listCount = medals.Count;

                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId, rare);
            }
            else if (mainType.Equals(AppConstants.MainType.Title))
            {
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset, rare);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
                listCount = titles.Count;

                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
            }
            else if (mainType.Equals(AppConstants.MainType.SpiritBeast))
            {
                List<SpiritBeast> spiritBeasts = UserSpiritBeastService.Create().GetUserSpiritBeast(User.CurrentUserId, pageSize, offset, rare);
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
                    ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_167");
                }
                else if (buttonType.Equals("button2"))
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_210");
                }
            }
        }

        type = subType;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        if (buttonType.Equals("button1"))
        {
            ButtonLoader.Instance.ChangeButtonBackground(clickedButton, "Background_V4_166");

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
                            ButtonLoader.Instance.ChangeButtonBackground(child.gameObject, "Background_V4_84_2");
                        }
                        else
                        {
                            ButtonLoader.Instance.ChangeButtonBackground(child.gameObject, "Background_V4_84_1");
                        }
                    }
                }
            }
        }
        else if (buttonType.Equals("button2"))
        {
            ButtonLoader.Instance.ChangeButtonBackground(clickedButton, "Background_V4_211");
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
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_84_1");
            }
        }

        rare = selectedRare;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, "Background_V4_84_2");
        LoadCurrentPage();
    }
    public void LoadCurrentPage()
    {
        int totalRecord = 0;
        int listCount = 0;
        // offset = 0;

        if (mainType.Equals(AppConstants.MainType.CardHero))
        {
            List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            listCount = cardHeroes.Count;

            totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Book))
        {
            List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            listCount = books.Count;

            totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CardCaptain))
        {
            List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            listCount = cardCaptains.Count;

            totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CollaborationEquipment))
        {
            List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            listCount = collaborationEquipments.Count;

            totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Item))
        {
            List<Items> items = UserItemsService.Create().GetUserItems(User.CurrentUserId, type, pageSize, offset);
            Close(DictionaryContentPanel);
            UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            listCount = items.Count;

            totalRecord = UserItemsService.Create().GetUserItemCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.MainType.Pet))
        {
            List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            listCount = pets.Count;

            totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Skill))
        {
            List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            listCount = skills.Count;

            totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Symbol))
        {
            List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            listCount = symbols.Count;

            totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CardMilitary))
        {
            List<CardMilitary> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            listCount = cardMilitaries.Count;

            totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CardSpell))
        {
            List<CardSpell> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            listCount = cardSpells.Count;

            totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Collaboration))
        {
            List<Collaboration> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            listCount = collaborations.Count;

            totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Medal))
        {
            List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            listCount = medals.Count;

            totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Title))
        {
            List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            listCount = titles.Count;

            totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MagicFormationCircle))
        {
            List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            listCount = magicFormationCircles.Count;

            totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Relic))
        {
            List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            listCount = relics.Count;

            totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CardMonster))
        {
            List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            listCount = cardMonsters.Count;

            totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CardColonel))
        {
            List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            listCount = cardColonels.Count;

            totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CardGeneral))
        {
            List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            listCount = cardGenerals.Count;

            totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CardAdmiral))
        {
            List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            listCount = cardAdmirals.Count;

            totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SummonCardHeroes))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardHeroes> cardHeroes = CardHeroesService.Create().GetCardHeroesRandom(type, 3);
            UserCardHeroesController.Instance.CreateUserCardHeroesForSummon(cardHeroes, PositionPanel);



            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.SummonBooks))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            List<Books> books = BooksService.Create().GetBooksRandom(type, 3);
            UserBooksController.Instance.CreateUserBooksForSummon(books, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.SummonCardCaptains))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptainsRandom(type, 3);
            UserCardCaptainsController.Instance.CreateUserCardCaptainsForSummon(cardCaptains, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardCaptainsTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardCaptainsTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardCaptainsTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.SummonCardMilitaries))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitaryRandom(type, 3);
            UserCardMilitaryController.Instance.CreateUserCardMilitaryForSummon(cardMilitaries, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardMilitaryTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardMilitaryTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardMilitaryTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.SummonCardSpells))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpellRandom(type, 3);
            UserCardSpellController.Instance.CreateUserCardSpellForSummon(cardSpells, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardSpellTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardSpellTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardSpellTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.SummonCardMonsters))
        {
            titleText2.text = "Summon " + string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonstersRandom(type, 3);
            UserCardMonstersController.Instance.CreateUserCardMonstersForSummon(cardMonsters, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardMonstersTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardMonstersTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardMonstersTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.SummonCardColonels))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonelsRandom(type, 3);
            UserCardColonelsController.Instance.CreateUserCardColonelsForSummon(cardColonels, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardColonelsTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardColonelsTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardColonelsTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.SummonCardGenerals))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGeneralsRandom(type, 3);
            UserCardGeneralsController.Instance.CreateUserCardGeneralsForSummon(cardGenerals, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardGeneralsTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardGeneralsTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardGeneralsTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.SummonCardAdmirals))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmiralsRandom(type, 3);
            UserCardAdmiralsController.Instance.CreateUserCardAdmiralsForSummon(cardAdmirals, PositionPanel);

            List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardAdmiralsTicket) };
            CurrencyManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 1, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardAdmiralsTicket) },
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
                FindObjectOfType<GachaSystem>().Summon(mainType, type, summonObject, 10, items, (success) =>
                {
                    if (success)
                    {
                        CurrencyManager.Instance.GetTicketsCurrency(
                        new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardAdmiralsTicket) },
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
        else if (mainType.Equals(AppConstants.MainType.Talisman))
        {
            List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            listCount = talismans.Count;

            totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Puppet))
        {
            List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            listCount = puppets.Count;

            totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Alchemy))
        {
            List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            listCount = alchemies.Count;

            totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Forge))
        {
            List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            listCount = forges.Count;

            totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CardLife))
        {
            List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            listCount = cardLives.Count;

            totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.Artwork))
        {
            List<Artwork> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, type, pageSize, offset, rare);
            Close(DictionaryContentPanel);
            UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
            listCount = artworks.Count;

            totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SpiritBeast))
        {
            List<SpiritBeast> spiritBeasts = UserSpiritBeastService.Create().GetUserSpiritBeast(User.CurrentUserId, pageSize, offset, rare);
            Close(DictionaryContentPanel);
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
    private void createEquipments(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.image.Replace(".png", "");
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
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.rare}");
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

            if (mainType.Equals(AppConstants.MainType.CardHero))
            {
                totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Book))
            {
                totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subType, pageSize, offset, rare);
                UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardCaptain))
            {
                totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CollaborationEquipment))
            {
                totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Collaboration))
            {
                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset, rare);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Medal))
            {
                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset, rare);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardMonster))
            {
                totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Pet))
            {
                totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, subType, pageSize, offset, rare);
                UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Skill))
            {
                totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Symbol))
            {
                totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Title))
            {
                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset, rare);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardMilitary))
            {
                totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardSpell))
            {
                totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.MagicFormationCircle))
            {
                totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset, rare);
                UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Relic))
            {
                totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset, rare);
                UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Talisman))
            {
                totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset, rare);
                UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Puppet))
            {
                totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset, rare);
                UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Alchemy))
            {
                totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset, rare);
                UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Forge))
            {
                totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset, rare);
                UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardColonel))
            {
                totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardGeneral))
            {
                totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardAdmiral))
            {
                totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardLife))
            {
                totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Artwork))
            {
                totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Artwork> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, subType, pageSize, offset, rare);
                UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Item))
            {
                totalRecord = UserItemsService.Create().GetUserItemCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Items> items = UserItemsService.Create().GetUserItems(User.CurrentUserId, subType, pageSize, offset);
                UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SpiritBeast))
            {
                totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<SpiritBeast> spiritBeasts = UserSpiritBeastService.Create().GetUserSpiritBeast(User.CurrentUserId, pageSize, offset, rare);
                UserSpiritBeastController.Instance.CreateUserSpiritBeast(spiritBeasts, DictionaryContentPanel);
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

            if (mainType.Equals(AppConstants.MainType.CardHero))
            {
                totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Book))
            {
                totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subType, pageSize, offset, rare);
                UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardCaptain))
            {
                totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CollaborationEquipment))
            {
                totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Collaboration))
            {
                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset, rare);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Medal))
            {
                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset, rare);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardMonster))
            {
                totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Pet))
            {
                totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, subType, pageSize, offset, rare);
                UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Skill))
            {
                totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Symbol))
            {
                totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subType, pageSize, offset, rare);
                UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Title))
            {
                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset, rare);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardMilitary))
            {
                totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardSpell))
            {
                totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.MagicFormationCircle))
            {
                totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset, rare);
                UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Relic))
            {
                totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset, rare);
                UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Talisman))
            {
                totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset, rare);
                UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Puppet))
            {
                totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset, rare);
                UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Alchemy))
            {
                totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset, rare);
                UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Forge))
            {
                totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset, rare);
                UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardColonel))
            {
                totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardGeneral))
            {
                totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardAdmiral))
            {
                totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.CardLife))
            {
                totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset, rare);
                UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Artwork))
            {
                totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Artwork> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, subType, pageSize, offset, rare);
                UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.Item))
            {
                totalRecord = UserItemsService.Create().GetUserItemCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Items> items = UserItemsService.Create().GetUserItems(User.CurrentUserId, subType, pageSize, offset);
                UserItemsController.Instance.CreateUserItems(items, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MainType.SpiritBeast))
            {
                totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<SpiritBeast> spiritBeasts = UserSpiritBeastService.Create().GetUserSpiritBeast(User.CurrentUserId, pageSize, offset, rare);
                UserSpiritBeastController.Instance.CreateUserSpiritBeast(spiritBeasts, DictionaryContentPanel);
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

            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(item.image);
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
