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
    private GameObject MasterBoardPanelPrefab;
    private Transform MainPanel;
    private Transform DictionaryContentPanel;
    private Button CloseButton;
    private Button HomeButton;
    private Button SummonButton;
    private Button Summon10Button;
    private GameObject equipmentsPrefab;
    private Transform TabButtonPanel;
    private GameObject buttonPrefab2;

    private GameObject SummonPanel;
    private Transform PositionPanel;
    private GameObject summonObject;
    private Transform SummonAreaPanel;
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
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
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
        MasterBoardPanelPrefab = UIManager.Instance.GetGameObject("MasterBoardPanelPrefab");

        ButtonEvent.Instance.AssignButtonEvent("Button_1", mainMenuCampaignPanel, () => loadScence());

        ButtonEvent.Instance.AssignButtonEvent("Button_1", mainMenuPanel, () => GetType(AppConstants.CardHero));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", mainMenuPanel, () => GetType(AppConstants.Book));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", mainMenuPanel, () => GetType(AppConstants.Pet));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", mainMenuPanel, () => GetType(AppConstants.CardCaptain));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", mainMenuPanel, () => GetType(AppConstants.CardColonel));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", mainMenuPanel, () => GetType(AppConstants.CardGeneral));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", mainMenuPanel, () => GetType(AppConstants.CardAdmiral));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", mainMenuPanel, () => GetType(AppConstants.CardMilitary));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", mainMenuPanel, () => GetType(AppConstants.CardSpell));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", mainMenuPanel, () => GetType(AppConstants.CardMonster));
        // Button_13 Equipments có thể được thêm lại nếu cần
        ButtonEvent.Instance.AssignButtonEvent("Button_11", mainMenuPanel, () => GetType(AppConstants.Bag));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", mainMenuPanel, () => GetType(AppConstants.Teams));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", mainMenuPanel, () => GetType(AppConstants.More));

        ButtonEvent.Instance.AssignButtonEvent("Button_14", SummonMainMenuPanel, () => GetType(AppConstants.SummonCardHeroes));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", SummonMainMenuPanel, () => GetType(AppConstants.SummonBooks));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", SummonMainMenuPanel, () => GetType(AppConstants.SummonCardCaptains));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", SummonMainMenuPanel, () => GetType(AppConstants.SummonCardMonsters));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", SummonMainMenuPanel, () => GetType(AppConstants.SummonCardMilitaries));
        ButtonEvent.Instance.AssignButtonEvent("Button_19", SummonMainMenuPanel, () => GetType(AppConstants.SummonCardSpells));
        ButtonEvent.Instance.AssignButtonEvent("Button_20", SummonMainMenuPanel, () => GetType(AppConstants.SummonCardColonels));
        ButtonEvent.Instance.AssignButtonEvent("Button_21", SummonMainMenuPanel, () => GetType(AppConstants.SummonCardGenerals));
        ButtonEvent.Instance.AssignButtonEvent("Button_22", SummonMainMenuPanel, () => GetType(AppConstants.SummonCardAdmirals));

        ButtonEvent.Instance.AssignButtonEvent("Button_24", SummonMainMenuPanel, () => GetType(AppConstants.Gallery));
        ButtonEvent.Instance.AssignButtonEvent("Button_25", SummonMainMenuPanel, () => GetType(AppConstants.Collection));
        ButtonEvent.Instance.AssignButtonEvent("Button_26", SummonMainMenuPanel, () => GetType(AppConstants.Equipment));
        ButtonEvent.Instance.AssignButtonEvent("Button_27", SummonMainMenuPanel, () => GetType(AppConstants.Anime));
        ButtonEvent.Instance.AssignButtonEvent("Button_28", SummonMainMenuPanel, () => GetType(AppConstants.Arena));
        ButtonEvent.Instance.AssignButtonEvent("Button_29", SummonMainMenuPanel, () => GetType(AppConstants.Guild));
        ButtonEvent.Instance.AssignButtonEvent("Button_30", SummonMainMenuPanel, () => GetType(AppConstants.Tower));
        ButtonEvent.Instance.AssignButtonEvent("Button_31", SummonMainMenuPanel, () => GetType(AppConstants.Event));
        ButtonEvent.Instance.AssignButtonEvent("Button_32", SummonMainMenuPanel, () => GetType(AppConstants.DailyCheckin));
        ButtonEvent.Instance.AssignButtonEvent("Button_33", SummonMainMenuPanel, () => GetType(AppConstants.RareMarket));
        ButtonEvent.Instance.AssignButtonEvent("Button_34", SummonMainMenuPanel, () => GetType(AppConstants.UltraRareMarket));
        ButtonEvent.Instance.AssignButtonEvent("Button_35", SummonMainMenuPanel, () => GetType(AppConstants.LegendaryMarket));
        ButtonEvent.Instance.AssignButtonEvent("Button_36", SummonMainMenuPanel, () => GetType(AppConstants.MysticMarket));
        // GetCardsType();
    }

    void Update()
    {

    }
    public void GetMoreButtonEvent(Transform moreMenuPanel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", moreMenuPanel, () => GetType(AppConstants.CollaborationEquipment));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", moreMenuPanel, () => GetType(AppConstants.Collaboration));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", moreMenuPanel, () => GetType(AppConstants.Medal));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", moreMenuPanel, () => GetType(AppConstants.Skill));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", moreMenuPanel, () => GetType(AppConstants.Symbol));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", moreMenuPanel, () => GetType(AppConstants.Title));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", moreMenuPanel, () => GetType(AppConstants.MagicFormationCircle));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", moreMenuPanel, () => GetType(AppConstants.Relic));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", moreMenuPanel, () => GetType(AppConstants.Talisman));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", moreMenuPanel, () => GetType(AppConstants.Puppet));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", moreMenuPanel, () => GetType(AppConstants.Alchemy));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", moreMenuPanel, () => GetType(AppConstants.Forge));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", moreMenuPanel, () => GetType(AppConstants.CardLife));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", moreMenuPanel, () => GetType(AppConstants.MasterBoard));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", moreMenuPanel, () => GetType(AppConstants.Artwork));
    }
    public void GetType(string type)
    {
        mainType = type; // Gán giá trị cho mainType
        GetButtonType(); // Gọi hàm xử lý
        if (titleText != null)
        {
            titleText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
        }
    }
    public void GetButtonType()
    {
        // MainMenuPanel.SetActive(true);
        if (mainType.Equals(AppConstants.SummonCardHeroes) || mainType.Equals(AppConstants.SummonBooks) || mainType.Equals(AppConstants.SummonCardCaptains) ||
        mainType.Equals(AppConstants.SummonCardColonels) || mainType.Equals(AppConstants.SummonCardGenerals) || mainType.Equals(AppConstants.SummonCardAdmirals) ||
        mainType.Equals(AppConstants.SummonCardMilitaries) || mainType.Equals(AppConstants.SummonCardMonsters) || mainType.Equals(AppConstants.SummonCardSpells))
        {
            buttonType = "button2";
            summonObject = Instantiate(SummonPanel, MainPanel);
            DictionaryContentPanel = summonObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            TabButtonPanel = summonObject.transform.Find("Scroll View/Viewport/ButtonContent");
            PositionPanel = summonObject.transform.Find("DictionaryCards/Position");

            titleText = summonObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            titleText2 = summonObject.transform.Find("DictionaryCards/TitleText2").GetComponent<TextMeshProUGUI>();
            CloseButton = summonObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            SummonButton = summonObject.transform.Find("DictionaryCards/SummonButton").GetComponent<Button>();
            Summon10Button = summonObject.transform.Find("DictionaryCards/Summon10Button").GetComponent<Button>();
            CloseButton.onClick.AddListener(ClosePanel);
            HomeButton = summonObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            SummonAreaPanel = summonObject.transform.Find("SummonArea");
            CurrencyPanel = summonObject.transform.Find("DictionaryCards/Currency");

            RawImage dictionaryBackground = summonObject.transform.Find("DictionaryBackground").GetComponent<RawImage>();
            RawImage rawImage1 = summonObject.transform.Find("DictionaryCards/RawImage1").GetComponent<RawImage>();
            RawImage rawImage2 = summonObject.transform.Find("DictionaryCards/RawImage2").GetComponent<RawImage>();
            RawImage background2 = summonObject.transform.Find("DictionaryCards/Background2").GetComponent<RawImage>();
            if (mainType.Equals(AppConstants.SummonCardHeroes))
            {

            }
            else if (mainType.Equals(AppConstants.SummonBooks) || mainType.Equals(AppConstants.SummonCardColonels))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_51");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_48");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.SummonCardCaptains) || mainType.Equals(AppConstants.SummonCardGenerals))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_50");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_63");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.SummonCardMonsters) || mainType.Equals(AppConstants.SummonCardAdmirals))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_49");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_69");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.SummonCardMilitaries))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_48");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_85");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals(AppConstants.SummonCardSpells))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_47");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_94");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
        }
        else if (mainType.Equals(AppConstants.Gallery))
        {
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.Gallery);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateGalleryButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals(AppConstants.Collection))
        {
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.Collection);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateCollectionButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals(AppConstants.Equipment))
        {
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.Equipments);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateEquipmentsButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals(AppConstants.Anime))
        {
            GameObject popupObject = Instantiate(AnimePanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.Arena))
        {
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateArenaButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.Guild))
        {
            // GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            // titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            // CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // CloseButton.onClick.AddListener(() => Close(MainPanel));
            // HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            // HomeButton.onClick.AddListener(() => Close(MainPanel));
            // FindObjectOfType<ButtonLoader>().CreateArenaButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.Tower))
        {
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateTowerButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals(AppConstants.Event))
        {

        }
        else if (mainType.Equals(AppConstants.MasterBoard))
        {
            GameObject popupObject = Instantiate(MasterBoardPanelPrefab, MainPanel);
            TextMeshProUGUI titleTMPText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            titleTMPText.text = LocalizationManager.Get(AppConstants.MasterBoard);
            // FindObjectOfType<ButtonLoader>().CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
            MasterBoardController.Instance.CreateMasterBoard(popupObject);
        }
        else if (mainType.Equals(AppConstants.Teams))
        {
            TeamsManager.Instance.CreateTeams();
        }
        else if (mainType.Equals(AppConstants.More))
        {
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = LocalizationManager.Get(AppConstants.More);
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            ButtonLoader.Instance.CreateMoreButton(popupObject.transform.Find("Content"));
            GetMoreButtonEvent(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals(AppConstants.DailyCheckin))
        {
            DailyCheckinManager.Instance.CreateDailyCheckinGroup();
        }
        else if (mainType.Equals(AppConstants.RareMarket))
        {
            RareMarketManager.Instance.CreateRareMarket();
        }
        else if (mainType.Equals(AppConstants.UltraRareMarket))
        {
            UltraRareMarketManager.Instance.CreateUltraRareMarket();
        }
        else if (mainType.Equals(AppConstants.LegendaryMarket))
        {
            LegendaryMarketManager.Instance.CreateLegendaryMarket();
        }
        else if (mainType.Equals(AppConstants.MysticMarket))
        {
            MysticMarketManager.Instance.CreateMysticMarket();
        }
        else
        {
            buttonType = "button1";
            GameObject mainMenuObject = Instantiate(DictionaryPanel, MainPanel);
            DictionaryContentPanel = mainMenuObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            TabButtonPanel = mainMenuObject.transform.Find("Scroll View/Viewport/ButtonContent");
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
        List<string> uniqueTypes = TypeManager.GetUniqueTypes(mainType);
        if (uniqueTypes.Count > 0 && !mainType.Equals(AppConstants.Equipment))
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = null;
                if (buttonType.Equals("button1"))
                {
                    button = Instantiate(buttonPrefab, TabButtonPanel);
                    Text buttonText = button.GetComponentInChildren<Text>();
                    buttonText.text = subtype.Replace("_", " ");
                }
                else if (buttonType.Equals("button2"))
                {
                    button = Instantiate(buttonPrefab2, TabButtonPanel);
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    buttonText.text = subtype.Replace("_", " ");
                }

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, subtype));

                if (i == 0)
                {
                    subType = subtype;
                    if (buttonType.Equals("button1"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_166");
                    }
                    else if (buttonType.Equals("button2"))
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_211");
                    }
                    int totalRecord = 0;
                    int listCount = 0;
                    if (mainType.Equals(AppConstants.CardHero))
                    {
                        List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subtype, pageSize, offset);
                        UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
                        listCount = cardHeroes.Count;

                        totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals(AppConstants.Book))
                    {
                        List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subtype, pageSize, offset);
                        UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
                        listCount = books.Count;

                        totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardCaptain))
                    {
                        List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subtype, pageSize, offset);
                        UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
                        listCount = cardCaptains.Count;

                        totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals(AppConstants.CollaborationEquipment))
                    {
                        List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subtype, pageSize, offset);
                        UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
                        listCount = collaborationEquipments.Count;

                        totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subtype);
                    }
                    // else if (mainType.Equals("Equipments"))
                    // {
                    //     List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subtype, pageSize, offset);
                    //     createEquipments(equipments);
                    //     listCount = equipments.Count;

                    //     totalRecord = EquipmentsService.Create().GetEquipmentsCount(subtype);
                    // }
                    else if (mainType.Equals(AppConstants.Pet))
                    {
                        List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, subtype, pageSize, offset);
                        UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
                        listCount = pets.Count;

                        totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals(AppConstants.Skill))
                    {
                        List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subtype, pageSize, offset);
                        UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
                        listCount = skills.Count;

                        totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals(AppConstants.Symbol))
                    {
                        List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subtype, pageSize, offset);
                        UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
                        listCount = symbols.Count;

                        totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardMilitary))
                    {
                        List<CardMilitary> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subtype, pageSize, offset);
                        UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
                        listCount = cardMilitaries.Count;

                        totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.CardSpell))
                    {
                        List<CardSpell> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subtype, pageSize, offset);
                        UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
                        listCount = cardSpells.Count;

                        totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.MagicFormationCircle))
                    {
                        List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset);
                        UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
                        listCount = magicFormationCircles.Count;

                        totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.Relic))
                    {
                        List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset);
                        UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
                        listCount = relics.Count;

                        totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.CardMonster))
                    {
                        List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, subType, pageSize, offset);
                        UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
                        listCount = cardMonsters.Count;

                        totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardColonel))
                    {
                        List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subtype, pageSize, offset);
                        UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
                        listCount = cardColonels.Count;

                        totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.CardGeneral))
                    {
                        List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subtype, pageSize, offset);
                        UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
                        listCount = cardGenerals.Count;

                        totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.CardAdmiral))
                    {
                        List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subtype, pageSize, offset);
                        UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
                        listCount = cardAdmirals.Count;

                        totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.SummonCardHeroes))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardHeroes> cardHeroes = CardHeroesService.Create().GetCardHeroesRandom(subtype, 3);
                        UserCardHeroesController.Instance.CreateUserCardHeroesForSummon(cardHeroes, PositionPanel);



                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.SummonBooks))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
                        List<Books> books = BooksService.Create().GetBooksRandom(subtype, 3);
                        UserBooksController.Instance.CreateUserBooksForSummon(books, PositionPanel);

                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.SummonCardCaptains))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptainsRandom(subtype, 3);
                        UserCardCaptainsController.Instance.CreateUserCardCaptainsForSummon(cardCaptains, PositionPanel);

                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardCaptainsTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.SummonCardMilitaries))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitaryRandom(subtype, 3);
                        UserCardMilitaryController.Instance.CreateUserCardMilitaryForSummon(cardMilitaries, PositionPanel);

                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardMilitaryTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.SummonCardSpells))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpellRandom(subtype, 3);
                        UserCardSpellController.Instance.CreateUserCardSpellForSummon(cardSpells, PositionPanel);

                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardSpellTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.SummonCardMonsters))
                    {
                        titleText2.text = "Summon " + string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonstersRandom(subtype, 3);
                        UserCardMonstersController.Instance.CreateUserCardMonstersForSummon(cardMonsters, PositionPanel);

                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardMonstersTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.SummonCardColonels))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonelsRandom(subtype, 3);
                        UserCardColonelsController.Instance.CreateUserCardColonelsForSummon(cardColonels, PositionPanel);

                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardColonelsTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.SummonCardGenerals))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGeneralsRandom(subtype, 3);
                        UserCardGeneralsController.Instance.CreateUserCardGeneralsForSummon(cardGenerals, PositionPanel);

                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardGeneralsTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.SummonCardAdmirals))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmiralsRandom(subtype, 3);
                        UserCardAdmiralsController.Instance.CreateUserCardAdmiralsForSummon(cardAdmirals, PositionPanel);

                        List<Items> items = new List<Items> { UserItemsService.Create().GetUserItemByName(ItemConstants.CardAdmiralsTicket) };
                        CurrencyManager.Instance.GetTicketsCurrency(
                            items,
                            CurrencyPanel
                        );

                        CreateTicketUI(items);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 1, items, (success) =>
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
                            FindObjectOfType<GachaSystem>().Summon(mainType, subtype, summonObject, 10, items, (success) =>
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
                    else if (mainType.Equals(AppConstants.Talisman))
                    {
                        List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset);
                        UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
                        listCount = talismans.Count;

                        totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.Puppet))
                    {
                        List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset);
                        UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
                        listCount = puppets.Count;

                        totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.Alchemy))
                    {
                        List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset);
                        UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
                        listCount = alchemies.Count;

                        totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.Forge))
                    {
                        List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset);
                        UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
                        listCount = forges.Count;

                        totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.CardLife))
                    {
                        List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset);
                        UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
                        listCount = cardLives.Count;

                        totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals(AppConstants.Artwork))
                    {
                        List<Artwork> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, subType, pageSize, offset);
                        UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
                        listCount = artworks.Count;

                        totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, subType);
                    }

                    if (listCount > 0)
                    {
                        totalPage = CalculateTotalPages(totalRecord, pageSize);
                        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
                    }

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
            if (mainType.Equals(AppConstants.Collaboration))
            {
                List<Collaboration> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
                listCount = collaborations.Count;

                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
                listCount = medals.Count;

                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
                listCount = titles.Count;

                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId);
            }

            if (listCount > 0)
            {
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            }
        }

    }
    void OnButtonClick(GameObject clickedButton, string type)
    {
        foreach (Transform child in TabButtonPanel)
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

        subType = type;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        if (buttonType.Equals("button1"))
        {
            ButtonLoader.Instance.ChangeButtonBackground(clickedButton, "Background_V4_166");
        }
        else if (buttonType.Equals("button2"))
        {
            ButtonLoader.Instance.ChangeButtonBackground(clickedButton, "Background_V4_211");
        }
        int totalRecord = 0;
        int listCount = 0;

        if (mainType.Equals(AppConstants.CardHero))
        {
            List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, type, pageSize, offset);
            UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            listCount = cardHeroes.Count;

            totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.Book))
        {
            List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, type, pageSize, offset);
            UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            listCount = books.Count;

            totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CardCaptain))
        {
            List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, type, pageSize, offset);
            UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            listCount = cardCaptains.Count;

            totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CollaborationEquipment))
        {
            List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, type, pageSize, offset);
            UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            listCount = collaborationEquipments.Count;

            totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, type);
        }
        // else if (mainType.Equals("Equipments"))
        // {
        //     List<Equipments> equipments = EquipmentsService.Create().GetEquipments(type, pageSize, offset);
        //     createEquipments(equipments);
        //     listCount = equipments.Count;

        //     totalRecord = EquipmentsService.Create().GetEquipmentsCount(type);
        // }
        else if (mainType.Equals(AppConstants.Pet))
        {
            List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, type, pageSize, offset);
            UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            listCount = pets.Count;

            totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.Skill))
        {
            List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, type, pageSize, offset);
            UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            listCount = skills.Count;

            totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.Symbol))
        {
            List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, type, pageSize, offset);
            UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            listCount = symbols.Count;

            totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CardMilitary))
        {
            List<CardMilitary> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, type, pageSize, offset);
            UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            listCount = cardMilitaries.Count;

            totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CardSpell))
        {
            List<CardSpell> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, type, pageSize, offset);
            UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            listCount = cardSpells.Count;

            totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.MagicFormationCircle))
        {
            List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, type, pageSize, offset);
            UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            listCount = magicFormationCircles.Count;

            totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.Relic))
        {
            List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, type, pageSize, offset);
            UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            listCount = relics.Count;

            totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CardMonster))
        {
            List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, type, pageSize, offset);
            UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            listCount = cardMonsters.Count;

            totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CardColonel))
        {
            List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, type, pageSize, offset);
            UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            listCount = cardColonels.Count;

            totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CardGeneral))
        {
            List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, type, pageSize, offset);
            UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            listCount = cardGenerals.Count;

            totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CardAdmiral))
        {
            List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, type, pageSize, offset);
            UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            listCount = cardAdmirals.Count;

            totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.SummonCardHeroes))
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
        else if (mainType.Equals(AppConstants.SummonBooks))
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
        else if (mainType.Equals(AppConstants.SummonCardCaptains))
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
        else if (mainType.Equals(AppConstants.SummonCardMilitaries))
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
        else if (mainType.Equals(AppConstants.SummonCardSpells))
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
        else if (mainType.Equals(AppConstants.SummonCardMonsters))
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
        else if (mainType.Equals(AppConstants.SummonCardColonels))
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
        else if (mainType.Equals(AppConstants.SummonCardGenerals))
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
        else if (mainType.Equals(AppConstants.SummonCardAdmirals))
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
        else if (mainType.Equals(AppConstants.Talisman))
        {
            List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, type, pageSize, offset);
            UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            listCount = talismans.Count;

            totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.Puppet))
        {
            List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, type, pageSize, offset);
            UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            listCount = puppets.Count;

            totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.Alchemy))
        {
            List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, type, pageSize, offset);
            UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            listCount = alchemies.Count;

            totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.Forge))
        {
            List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, type, pageSize, offset);
            UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            listCount = forges.Count;

            totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.CardLife))
        {
            List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, type, pageSize, offset);
            UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            listCount = cardLives.Count;

            totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals(AppConstants.Artwork))
        {
            List<Artwork> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, type, pageSize, offset);
            UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
            listCount = artworks.Count;

            totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, type);
        }

        if (listCount > 0)
        {
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        }
        // Debug.Log($"Button for type '{type}' clicked!");
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
        foreach (Transform child in TabButtonPanel)
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

            if (mainType.Equals(AppConstants.CardHero))
            {
                totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subType, pageSize, offset);
                UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {
                totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subType, pageSize, offset);
                UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {
                totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subType, pageSize, offset);
                UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {
                totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subType, pageSize, offset);
                UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {
                totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, subType, pageSize, offset);
                UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {
                totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, subType, pageSize, offset);
                UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {
                totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subType, pageSize, offset);
                UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {
                totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subType, pageSize, offset);
                UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {
                totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subType, pageSize, offset);
                UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {
                totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subType, pageSize, offset);
                UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {
                totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset);
                UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {
                totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset);
                UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset);
                UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset);
                UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset);
                UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset);
                UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subType, pageSize, offset);
                UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subType, pageSize, offset);
                UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subType, pageSize, offset);
                UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset);
                UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Artwork> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, subType, pageSize, offset);
                UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
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

            if (mainType.Equals(AppConstants.CardHero))
            {
                totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subType, pageSize, offset);
                UserCardHeroesController.Instance.CreateUserCardHeroes(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {
                totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subType, pageSize, offset);
                UserBooksController.Instance.CreateUserBooks(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {
                totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subType, pageSize, offset);
                UserCardCaptainsController.Instance.CreateUserCardCaptains(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {
                totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subType, pageSize, offset);
                UserCollaborationEquipmentController.Instance.CreateUserCollaborationEquipments(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset);
                UserCollaborationController.Instance.CreateUserCollaboration(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medals = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset);
                UserMedalsController.Instance.CreateUserMedals(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {
                totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, subType, pageSize, offset);
                UserCardMonstersController.Instance.CreateUserCardMonsters(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {
                totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, subType, pageSize, offset);
                UserPetsController.Instance.CreateUserPets(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {
                totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subType, pageSize, offset);
                UserSkillsController.Instance.CreateUserSkills(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {
                totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subType, pageSize, offset);
                UserSymbolsController.Instance.CreateUserSymbols(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset);
                UserTitlesController.Instance.CreateUserTitles(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {
                totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> cardMilitaries = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subType, pageSize, offset);
                UserCardMilitaryController.Instance.CreateUserCardMilitary(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {
                totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> cardSpells = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subType, pageSize, offset);
                UserCardSpellController.Instance.CreateUserCardSpell(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {
                totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset);
                UserMagicFormationCircleController.Instance.CreateUserMagicFormationCircle(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {
                totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset);
                UserRelicsController.Instance.CreateUserRelics(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset);
                UserTalismanController.Instance.CreateUserTalisman(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset);
                UserPuppetController.Instance.CreateUserPuppet(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset);
                UserAlchemyController.Instance.CreateUserAlchemy(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset);
                UserForgeController.Instance.CreateUserForge(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subType, pageSize, offset);
                UserCardColonelsController.Instance.CreateUserCardColonels(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subType, pageSize, offset);
                UserCardGeneralsController.Instance.CreateUserCardGenerals(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subType, pageSize, offset);
                UserCardAdmiralsController.Instance.CreateUserCardAdmirals(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset);
                UserCardLifeController.Instance.CreateUserCardLife(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                totalRecord = UserArtworkService.Create().GetUserArtworkCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Artwork> artworks = UserArtworkService.Create().GetUserArtwork(User.CurrentUserId, subType, pageSize, offset);
                UserArtworkController.Instance.CreateUserArtwork(artworks, DictionaryContentPanel);
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
    public void loadScence()
    {
        FindAnyObjectByType<SceneLoader>().LoadScene("TeamsScenes");
    }

}
