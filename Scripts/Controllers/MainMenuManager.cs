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
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private Transform DictionaryContentPanel;
    private Button CloseButton;
    private Button HomeButton;
    private Button SummonButton;
    private Button Summon10Button;
    private GameObject equipmentsPrefab;
    private Transform TabButtonPanel;
    private GameObject buttonPrefab2;
    
    private GameObject SummonPanel;
    private GameObject PositionPrefab;
    private Transform PositionPanel;
    private Transform SummonAreaPanel;
    private Transform SummonMainMenuPanel;
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
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        SummonPanel = UIManager.Instance.GetGameObject("SummonPanelPrefab");
        PositionPrefab = UIManager.Instance.GetGameObject("PositionPrefab");
        SummonMainMenuPanel = UIManager.Instance.GetTransform("summonPanel");
        PopupMenuPanelPrefab = UIManager.Instance.GetGameObject("PopupMenuPanelPrefab");
        ArenaPanelPrefab = UIManager.Instance.GetGameObject("ArenaPanelPrefab");
        AnimePanelPrefab = UIManager.Instance.GetGameObject("AnimePanelPrefab");

        AssignButtonEvent("Button_1", mainMenuCampaignPanel, () => loadScence());

        AssignButtonEvent("Button_1", mainMenuPanel, () => GetType("CardHeroes"));
        AssignButtonEvent("Button_2", mainMenuPanel, () => GetType("Books"));
        AssignButtonEvent("Button_3", mainMenuPanel, () => GetType("Pets"));
        AssignButtonEvent("Button_4", mainMenuPanel, () => GetType("CardCaptains"));
        AssignButtonEvent("Button_5", mainMenuPanel, () => GetType("CardColonels"));
        AssignButtonEvent("Button_6", mainMenuPanel, () => GetType("CardGenerals"));
        AssignButtonEvent("Button_7", mainMenuPanel, () => GetType("CardAdmirals"));
        AssignButtonEvent("Button_8", mainMenuPanel, () => GetType("CollaborationEquipments"));
        AssignButtonEvent("Button_9", mainMenuPanel, () => GetType("CardMilitary"));
        AssignButtonEvent("Button_10", mainMenuPanel, () => GetType("CardSpell"));
        AssignButtonEvent("Button_11", mainMenuPanel, () => GetType("Collaborations"));
        AssignButtonEvent("Button_12", mainMenuPanel, () => GetType("CardMonsters"));
        // AssignButtonEvent("Button_13", mainMenuPanel, () => GetType("Equipments"));
        AssignButtonEvent("Button_14", mainMenuPanel, () => GetType("Medals"));
        AssignButtonEvent("Button_15", mainMenuPanel, () => GetType("Skills"));
        AssignButtonEvent("Button_16", mainMenuPanel, () => GetType("Symbols"));
        AssignButtonEvent("Button_17", mainMenuPanel, () => GetType("Titles"));
        AssignButtonEvent("Button_18", mainMenuPanel, () => GetType("Bag"));
        AssignButtonEvent("Button_19", mainMenuPanel, () => GetType("Teams"));
        AssignButtonEvent("Button_20", SummonMainMenuPanel, () => GetType("MagicFormationCircle"));
        AssignButtonEvent("Button_21", SummonMainMenuPanel, () => GetType("Relics"));
        AssignButtonEvent("Button_22", SummonMainMenuPanel, () => GetType("Talisman"));
        AssignButtonEvent("Button_23", SummonMainMenuPanel, () => GetType("Puppet"));
        AssignButtonEvent("Button_24", SummonMainMenuPanel, () => GetType("Alchemy"));
        AssignButtonEvent("Button_25", SummonMainMenuPanel, () => GetType("Forge"));
        AssignButtonEvent("Button_26", SummonMainMenuPanel, () => GetType("CardLife"));

        AssignButtonEvent("Button_27", SummonMainMenuPanel, () => GetType("SummonCardHeroes"));
        AssignButtonEvent("Button_28", SummonMainMenuPanel, () => GetType("SummonBooks"));
        AssignButtonEvent("Button_29", SummonMainMenuPanel, () => GetType("SummonCardCaptains"));
        AssignButtonEvent("Button_30", SummonMainMenuPanel, () => GetType("SummonCardMonsters"));
        AssignButtonEvent("Button_31", SummonMainMenuPanel, () => GetType("SummonCardMilitary"));
        AssignButtonEvent("Button_32", SummonMainMenuPanel, () => GetType("SummonCardSpell"));
        AssignButtonEvent("Button_33", SummonMainMenuPanel, () => GetType("SummonCardColonels"));
        AssignButtonEvent("Button_34", SummonMainMenuPanel, () => GetType("SummonCardGenerals"));
        AssignButtonEvent("Button_35", SummonMainMenuPanel, () => GetType("SummonCardAdmirals"));

        AssignButtonEvent("Button_37", SummonMainMenuPanel, () => GetType("Gallery"));
        AssignButtonEvent("Button_38", SummonMainMenuPanel, () => GetType("Collection"));
        AssignButtonEvent("Button_39", SummonMainMenuPanel, () => GetType("Equipments"));
        AssignButtonEvent("Button_40", SummonMainMenuPanel, () => GetType("Anime"));
        AssignButtonEvent("Button_41", SummonMainMenuPanel, () => GetType("Arena"));
        AssignButtonEvent("Button_42", SummonMainMenuPanel, () => GetType("Guild"));
        AssignButtonEvent("Button_43", SummonMainMenuPanel, () => GetType("Tower"));
        AssignButtonEvent("Button_44", SummonMainMenuPanel, () => GetType("Event"));
        // GetCardsType();
    }

    void Update()
    {

    }
    void AssignButtonEvent(string buttonName, Transform panel, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = panel.Find(buttonName);
        if (buttonTransform != null)
        {
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(action);
            }
        }
        else
        {
            Debug.LogWarning($"Button {buttonName} not found!");
        }
    }
    public void GetType(string type)
    {
        mainType = type; // Gán giá trị cho mainType
        GetButtonType(); // Gọi hàm xử lý
        if (!mainType.Equals("Gallery") && !mainType.Equals("Collection")
        && !mainType.Equals("Equipments") && !mainType.Equals("Teams"))
        {
            if (titleText != null)
            {
                titleText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            }
            else
            {
                Debug.LogError("titleText is null!");
            }

        }
    }
    public void GetButtonType()
    {
        // MainMenuPanel.SetActive(true);
        if (mainType.Equals("SummonCardHeroes") || mainType.Equals("SummonBooks") || mainType.Equals("SummonCardCaptains") ||
        mainType.Equals("SummonCardColonels") || mainType.Equals("SummonCardGenerals") || mainType.Equals("SummonCardAdmirals") ||
        mainType.Equals("SummonCardMonsters") || mainType.Equals("SummonCardMilitary") || mainType.Equals("SummonCardSpell"))
        {
            buttonType = "button2";
            GameObject summonObject = Instantiate(SummonPanel, MainPanel);
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

            RawImage dictionaryBackground = summonObject.transform.Find("DictionaryBackground").GetComponent<RawImage>();
            RawImage rawImage1 = summonObject.transform.Find("DictionaryCards/RawImage1").GetComponent<RawImage>();
            RawImage rawImage2 = summonObject.transform.Find("DictionaryCards/RawImage2").GetComponent<RawImage>();
            RawImage background2 = summonObject.transform.Find("DictionaryCards/Background2").GetComponent<RawImage>();
            if (mainType.Equals("SummonCardHeroes"))
            {

            }
            else if (mainType.Equals("SummonBooks") || mainType.Equals("SummonCardColonels"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_51");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_48");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals("SummonCardCaptains") || mainType.Equals("SummonCardGenerals"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_50");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_63");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals("SummonCardMonsters") || mainType.Equals("SummonCardAdmirals"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_49");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_69");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals("SummonCardMilitary"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_48");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_85");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals("SummonCardSpell"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_47");
                // dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_94");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
        }
        else if (mainType.Equals("Gallery"))
        {
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = "Gallery";
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            FindObjectOfType<ButtonLoader>().CreateGalleryButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals("Collection"))
        {
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = "Collection";
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            FindObjectOfType<ButtonLoader>().CreateCollectionButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals("Equipments"))
        {
            GameObject popupObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
            TextMeshProUGUI TitleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TitleText.text = "Equipments";
            CloseButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            FindObjectOfType<ButtonLoader>().CreateEquipmentsButton(popupObject.transform.Find("Content"));
        }
        else if (mainType.Equals("Anime"))
        {
            GameObject popupObject = Instantiate(AnimePanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            FindObjectOfType<ButtonLoader>().CreateAnimeButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals("Arena"))
        {
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            FindObjectOfType<ButtonLoader>().CreateArenaButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals("Guild"))
        {
            // GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            // titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            // CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // CloseButton.onClick.AddListener(() => Close(MainPanel));
            // HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            // HomeButton.onClick.AddListener(() => Close(MainPanel));
            // FindObjectOfType<ButtonLoader>().CreateArenaButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals("Tower"))
        {
            GameObject popupObject = Instantiate(ArenaPanelPrefab, MainPanel);
            titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
            CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Close(MainPanel));
            HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
            FindObjectOfType<ButtonLoader>().CreateTowerButton(popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content"));
        }
        else if (mainType.Equals("Event"))
        {
            
        }
        else if (mainType.Equals("Teams"))
        {
            FindAnyObjectByType<TeamsManager>().CreateTeams();
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

            Transform CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");
            
            List<Currency> currencies = new List<Currency>();
            currencies = UserCurrencyService.Create().GetUserCurrency();
            FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);
        }
        List<string> uniqueTypes = TypeManager.GetUniqueTypes(mainType);
        if (uniqueTypes.Count > 0 && !mainType.Equals("Equipments"))
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
                        ChangeButtonBackground(button, "Background_V4_166");
                    }
                    else if (buttonType.Equals("button2"))
                    {
                        ChangeButtonBackground(button, "Background_V4_211");
                    }
                    int totalRecord = 0;
                    int listCount = 0;
                    if (mainType.Equals("CardHeroes"))
                    {
                        List<CardHeroes> cards = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subtype, pageSize, offset);
                        createCardHeroes(cards);
                        listCount = cards.Count;
                        Debug.Log(User.CurrentUserId);
                        totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals("Books"))
                    {
                        List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subtype, pageSize, offset);
                        createBooks(books);
                        listCount = books.Count;

                        totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals("CardCaptains"))
                    {
                        List<CardCaptains> captains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subtype, pageSize, offset);
                        createCardCaptains(captains);
                        listCount = captains.Count;

                        totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals("CollaborationEquipments"))
                    {
                        List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subtype, pageSize, offset);
                        createCollaborationEquipments(collaborationEquipments);
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
                    else if (mainType.Equals("Pets"))
                    {
                        List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, subtype, pageSize, offset);
                        createPets(pets);
                        listCount = pets.Count;

                        totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals("Skills"))
                    {
                        List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subtype, pageSize, offset);
                        createSkills(skills);
                        listCount = skills.Count;

                        totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals("Symbols"))
                    {
                        List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subtype, pageSize, offset);
                        createSymbols(symbols);
                        listCount = symbols.Count;

                        totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals("CardMilitary"))
                    {
                        List<CardMilitary> militaryList = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subtype, pageSize, offset);
                        createCardMilitary(militaryList);
                        listCount = militaryList.Count;

                        totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("CardSpell"))
                    {
                        List<CardSpell> spellList = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subtype, pageSize, offset);
                        createCardSpell(spellList);
                        listCount = spellList.Count;

                        totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("MagicFormationCircle"))
                    {
                        List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset);
                        createMagicFormationCircle(magicFormationCircles);
                        listCount = magicFormationCircles.Count;

                        totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("Relics"))
                    {
                        List<Relics> relicsList = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset);
                        createRelics(relicsList);
                        listCount = relicsList.Count;

                        totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("CardMonsters"))
                    {
                        List<CardMonsters> monstersList = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, subType, pageSize, offset);
                        createCardMonsters(monstersList);
                        listCount = monstersList.Count;

                        totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subtype);
                    }
                    else if (mainType.Equals("CardColonels"))
                    {
                        List<CardColonels> colonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subtype, pageSize, offset);
                        createCardColonels(colonels);
                        listCount = colonels.Count;

                        totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("CardGenerals"))
                    {
                        List<CardGenerals> generals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subtype, pageSize, offset);
                        createCardGenerals(generals);
                        listCount = generals.Count;

                        totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("CardAdmirals"))
                    {
                        List<CardAdmirals> admirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subtype, pageSize, offset);
                        createCardAdmirals(admirals);
                        listCount = admirals.Count;

                        totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("SummonCardHeroes"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroesRandom(subtype, 3);
                        createCardHeroesForSummon(cards);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("cards", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("cards", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("SummonBooks"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
                        List<Books> books = BooksService.Create().GetBooksRandom(subtype, 3);
                        createBooksForSummon(books);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("books", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("books", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("SummonCardCaptains"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptainsRandom(subtype, 3);
                        createCardCaptainsForSummon(captains);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("captains", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("captains", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("SummonCardMilitary"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardMilitary> military = CardMilitaryService.Create().GetCardMilitaryRandom(subtype, 3);
                        createCardMilitaryForSummon(military);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("military", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("military", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("SummonCardSpell"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardSpell> spell = CardSpellService.Create().GetCardSpellRandom(subtype, 3);
                        createCardSpellForSummon(spell);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("spell", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("spell", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("SummonCardMonsters"))
                    {
                        titleText2.text = "Summon " + string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardMonsters> monsters = CardMonstersService.Create().GetCardMonstersRandom(subtype, 3);
                        createCardMonstersForSummon(monsters);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("monsters", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("monsters", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("SummonCardColonels"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardColonels> colonels = CardColonelsService.Create().GetCardColonelsRandom(subtype, 3);
                        createCardColonelsForSummon(colonels);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("colonels", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("colonels", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("SummonCardGenerals"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardGenerals> relicsList = CardGeneralsService.Create().GetCardGeneralsRandom(subtype, 3);
                        createCardGeneralsForSummon(relicsList);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("generals", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("generals", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("SummonCardAdmirals"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        List<CardAdmirals> relicsList = CardAdmiralsService.Create().GetCardAdmiralsRandom(subtype, 3);
                        createCardAdmiralsForSummon(relicsList);

                        SummonButton.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("admirals", subtype, SummonAreaPanel, 1);
                        });
                        Summon10Button.onClick.AddListener(() =>
                        {
                            FindObjectOfType<GachaSystem>().Summon("admirals", subtype, SummonAreaPanel, 10);
                        });
                    }
                    else if (mainType.Equals("Talisman"))
                    {
                        List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset);
                        createTalisman(talismans);
                        listCount = talismans.Count;

                        totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("Puppet"))
                    {
                        List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset);
                        createPuppet(puppets);
                        listCount = puppets.Count;

                        totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("Alchemy"))
                    {
                        List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset);
                        createAlchemy(alchemies);
                        listCount = alchemies.Count;

                        totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("Forge"))
                    {
                        List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset);
                        createForge(forges);
                        listCount = forges.Count;

                        totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType);
                    }
                    else if (mainType.Equals("CardLife"))
                    {
                        List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset);
                        createCardLife(cardLives);
                        listCount = cardLives.Count;

                        totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType);
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
                        ChangeButtonBackground(button, "Background_V4_167");
                    }
                    else if (buttonType.Equals("button2"))
                    {
                        ChangeButtonBackground(button, "Background_V4_210");
                    }
                }
            }
        }
        else
        {
            int totalRecord = 0;
            int listCount = 0;
            if (mainType.Equals("Collaboration"))
            {
                List<Collaboration> collaborations = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset);
                createCollaboration(collaborations);
                listCount = collaborations.Count;

                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId);
            }
            else if (mainType.Equals("Medals"))
            {
                List<Medals> medalsList = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset);
                createMedals(medalsList);
                listCount = medalsList.Count;

                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId);
            }
            else if (mainType.Equals("Titles"))
            {
                List<Titles> titlesList = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset);
                createTitles(titlesList);
                listCount = titlesList.Count;

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
                    ChangeButtonBackground(button.gameObject, "Background_V4_167");
                }
                else if (buttonType.Equals("button2"))
                {
                    ChangeButtonBackground(button.gameObject, "Background_V4_210");
                }
            }
        }

        subType = type;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        if (buttonType.Equals("button1"))
        {
            ChangeButtonBackground(clickedButton, "Background_V4_166");
        }
        else if (buttonType.Equals("button2"))
        {
            ChangeButtonBackground(clickedButton, "Background_V4_211");
        }
        int totalRecord = 0;

        if (mainType.Equals("CardHeroes"))
        {
            List<CardHeroes> cards = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, type, pageSize, offset);
            createCardHeroes(cards);

            totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("Books"))
        {
            List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, type, pageSize, offset);
            createBooks(books);

            totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CardCaptains"))
        {
            List<CardCaptains> captains = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, type, pageSize, offset);
            createCardCaptains(captains);

            totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, type, pageSize, offset);
            createCollaborationEquipments(collaborationEquipments);

            totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, type);
        }
        // else if (mainType.Equals("Equipments"))
        // {
        //     List<Equipments> equipments = EquipmentsService.Create().GetUserEquipments(User.CurrentUserId, type, pageSize, offset);
        //     createEquipments(equipments);

        //     totalRecord = EquipmentsService.Create().GetEquipmentsCount(type);
        // }
        else if (mainType.Equals("Pets"))
        {
            List<Pets> pets = UserPetsService.Create().GetUserPets(User.CurrentUserId, type, pageSize, offset);
            createPets(pets);

            totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("Skills"))
        {
            List<Skills> skills = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, type, pageSize, offset);
            createSkills(skills);

            totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("Symbols"))
        {
            List<Symbols> symbols = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, type, pageSize, offset);
            createSymbols(symbols);

            totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CardMilitary"))
        {
            List<CardMilitary> militaryList = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, type, pageSize, offset);
            createCardMilitary(militaryList);

            totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CardSpell"))
        {
            List<CardSpell> spellList = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, type, pageSize, offset);
            createCardSpell(spellList);

            totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("MagicFormationCircle"))
        {
            List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, type, pageSize, offset);
            createMagicFormationCircle(magicFormationCircles);

            totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("Relics"))
        {
            List<Relics> relicsList = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, type, pageSize, offset);
            createRelics(relicsList);

            totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CardMonsters"))
        {
            List<CardMonsters> monstersList = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId, type, pageSize, offset);
            createCardMonsters(monstersList);

            totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CardColonels"))
        {
            List<CardColonels> colonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, type, pageSize, offset);
            createCardColonels(colonels);

            totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CardGenerals"))
        {
            List<CardGenerals> generals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, type, pageSize, offset);
            createCardGenerals(generals);

            totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CardAdmirals"))
        {
            List<CardAdmirals> admirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, type, pageSize, offset);
            createCardAdmirals(admirals);

            totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("SummonCardHeroes"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroesRandom(type, 3);
            createCardHeroesForSummon(cards);

            SummonButton.onClick.RemoveAllListeners();
            Summon10Button.onClick.RemoveAllListeners();

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("cards", type, SummonAreaPanel, 1);
            });
            Summon10Button.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("cards", type, SummonAreaPanel, 10);
            });
        }
        else if (mainType.Equals("SummonBooks"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            List<Books> books = BooksService.Create().GetBooksRandom(type, 3);
            createBooksForSummon(books);

            SummonButton.onClick.RemoveAllListeners();
            Summon10Button.onClick.RemoveAllListeners();

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("books", type, SummonAreaPanel, 1);
            });
            Summon10Button.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("books", type, SummonAreaPanel, 10);
            });
        }
        else if (mainType.Equals("SummonCardCaptains"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptainsRandom(type, 3);
            createCardCaptainsForSummon(captains);

            SummonButton.onClick.RemoveAllListeners();
            Summon10Button.onClick.RemoveAllListeners();

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("captains", type, SummonAreaPanel, 1);
            });
            Summon10Button.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("captains", type, SummonAreaPanel, 10);
            });
        }
        else if (mainType.Equals("SummonCardMilitary"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardMilitary> military = CardMilitaryService.Create().GetCardMilitaryRandom(type, 3);
            createCardMilitaryForSummon(military);

            SummonButton.onClick.RemoveAllListeners();
            Summon10Button.onClick.RemoveAllListeners();

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("military", type, SummonAreaPanel, 1);
            });
            Summon10Button.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("military", type, SummonAreaPanel, 10);
            });
        }
        else if (mainType.Equals("SummonCardSpell"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardSpell> spell = CardSpellService.Create().GetCardSpellRandom(type, 3);
            createCardSpellForSummon(spell);

            SummonButton.onClick.RemoveAllListeners();
            Summon10Button.onClick.RemoveAllListeners();

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("spell", type, SummonAreaPanel, 1);
            });
            Summon10Button.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("spell", type, SummonAreaPanel, 10);
            });
        }
        else if (mainType.Equals("SummonCardColonels"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardColonels> colonels = CardColonelsService.Create().GetCardColonelsRandom(type, 3);
            createCardColonelsForSummon(colonels);

            SummonButton.onClick.RemoveAllListeners();
            Summon10Button.onClick.RemoveAllListeners();

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("colonels", type, SummonAreaPanel, 1);
            });
            Summon10Button.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("colonels", type, SummonAreaPanel, 10);
            });
        }
        else if (mainType.Equals("SummonCardGenerals"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardGenerals> relicsList = CardGeneralsService.Create().GetCardGeneralsRandom(type, 3);
            createCardGeneralsForSummon(relicsList);

            SummonButton.onClick.RemoveAllListeners();
            Summon10Button.onClick.RemoveAllListeners();

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("generals", type, SummonAreaPanel, 1);
            });
            Summon10Button.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("generals", type, SummonAreaPanel, 10);
            });
        }
        else if (mainType.Equals("SummonCardAdmirals"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            List<CardAdmirals> relicsList = CardAdmiralsService.Create().GetCardAdmiralsRandom(type, 3);
            createCardAdmiralsForSummon(relicsList);

            SummonButton.onClick.RemoveAllListeners();
            Summon10Button.onClick.RemoveAllListeners();

            SummonButton.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("admirals", type, SummonAreaPanel, 1);
            });
            Summon10Button.onClick.AddListener(() =>
            {
                FindObjectOfType<GachaSystem>().Summon("admirals", type, SummonAreaPanel, 10);
            });
        }
        else if (mainType.Equals("Talisman"))
        {
            List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, type, pageSize, offset);
            createTalisman(talismans);

            totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("Puppet"))
        {
            List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, type, pageSize, offset);
            createPuppet(puppets);

            totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("Alchemy"))
        {
            List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, type, pageSize, offset);
            createAlchemy(alchemies);

            totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("Forge"))
        {
            List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, type, pageSize, offset);
            createForge(forges);

            totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, type);
        }
        else if (mainType.Equals("CardLife"))
        {
            List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, type, pageSize, offset);
            createCardLife(cardLives);

            totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, type);
        }


        if (!mainType.Equals("SummonCardHeroes") && !mainType.Equals("SummonBooks") && !mainType.Equals("SummonCardCaptains") &&
        !mainType.Equals("SummonCardColonels") && !mainType.Equals("SummonCardGenerals") && !mainType.Equals("SummonCardAdmirals") &&
        !mainType.Equals("SummonCardMonsters") && !mainType.Equals("SummonCardMilitary") && !mainType.Equals("SummonCardSpell"))
        {
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        }
        // Debug.Log($"Button for type '{type}' clicked!");
    }
    private void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"UI/Background4/{image}");
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
    private void createCardHeroes(List<CardHeroes> cards)
    {
        foreach (var card in cards)
        {
            GameObject cardObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
            Title.text = card.name.Replace("_", " ");

            RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
            rareImage.texture = rareTexture;

            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(card, MainPanel));
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

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 250);
            }
        }
    }
    private void createBooks(List<Books> books)
    {
        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = bookObject.transform.Find("Title").GetComponent<Text>();
            Title.text = book.name.Replace("_", " ");

            RawImage Image = bookObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = book.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = bookObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
            rareImage.texture = rareTexture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(book, MainPanel));
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
            // Đặt kích thước gốc
            Image.SetNativeSize();

            // Thay đổi tỉ lệ
            if (texture.width < 1400 && texture.height < 1400 && texture.width > 700 && texture.height > 700)
            {
                Image.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            }
            else if (texture.width > 1000 && texture.height <= 2100 && texture.width < 2000 && texture.height > 1000)
            {
                Image.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
            }
            else if (texture.width <= 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
            }
            else if (texture.width <= 700 && texture.height <= 1100)
            {
                Image.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            }
            else if (texture.width > 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.35f, 0.45f, 0.35f);
            }
            else
            {
                Image.transform.localScale = new Vector3(0.17f, 0.17f, 0.17f);
            }


            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(280, 300);
            }
        }
    }
    private void createCardCaptains(List<CardCaptains> captainsList)
    {
        foreach (var captain in captainsList)
        {
            GameObject captainsObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = captainsObject.transform.Find("Title").GetComponent<Text>();
            Title.text = captain.name.Replace("_", " ");

            RawImage Image = captainsObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = captain.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = captainsObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captain.rare}");
            rareImage.texture = rareTexture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(captain, MainPanel));
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

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 250);
            }
        }
    }
    private void createCollaboration(List<Collaboration> collaborationList)
    {
        foreach (var collaboration in collaborationList)
        {
            GameObject collaborationObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = collaborationObject.transform.Find("Title").GetComponent<Text>();
            Title.text = collaboration.name.Replace("_", " ");

            RawImage Image = collaborationObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = collaboration.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = collaborationObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(collaboration, MainPanel));
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

            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(280, 230);
            }
        }
    }
    private void createCollaborationEquipments(List<CollaborationEquipment> collaborationEquipmentList)
    {
        foreach (var collaborationEquipment in collaborationEquipmentList)
        {
            GameObject collaborationEquipmentObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = collaborationEquipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = collaborationEquipment.name.Replace("_", " ");

            RawImage Image = collaborationEquipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(collaborationEquipment, MainPanel));
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

            RawImage rareImage = collaborationEquipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
            rareImage.texture = rareTexture;

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
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
    private void createMedals(List<Medals> medalsList)
    {
        foreach (var medal in medalsList)
        {
            GameObject medalObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = medalObject.transform.Find("Title").GetComponent<Text>();
            Title.text = medal.name.Replace("_", " ");

            RawImage Image = medalObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = medal.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(medal, MainPanel));
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

            RawImage rareImage = medalObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

            RawImage rareBackgroundImage = medalObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);
        }
    }
    private void createCardMonsters(List<CardMonsters> monstersList)
    {
        foreach (var monster in monstersList)
        {
            GameObject monstersObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = monstersObject.transform.Find("Title").GetComponent<Text>();
            Title.text = monster.name.Replace("_", " ");

            RawImage Image = monstersObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = monster.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(monster, MainPanel));
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

            RawImage rareImage = monstersObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monster.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createPets(List<Pets> petsList)
    {
        foreach (var pet in petsList)
        {
            GameObject petsObject;
            if (pet.type.Equals("Legendary_Dragon") || pet.type.Equals("Naruto_Bijuu") || pet.type.Equals("Naruto_Susanoo") || pet.type.Equals("One_Piece_Ship") || pet.type.Equals("Prime_Monster"))
            {
                petsObject = Instantiate(cardsPrefab, DictionaryContentPanel);
                RawImage Background = petsObject.transform.Find("Background").GetComponent<RawImage>();
                Background.gameObject.SetActive(true);

                GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
                if (gridLayout != null)
                {
                    gridLayout.cellSize = new Vector2(280, 280);
                }
            }
            else
            {
                petsObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

                GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
                if (gridLayout != null)
                {
                    gridLayout.cellSize = new Vector2(200, 230);
                }
            }

            Text Title = petsObject.transform.Find("Title").GetComponent<Text>();
            Title.text = pet.name.Replace("_", " ");

            RawImage Image = petsObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = pet.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(pet, MainPanel));
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

            if (pet.type.Equals("Prime_Monster"))
            {
                Image.SetNativeSize();
                Image.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            RawImage rareImage = petsObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

        }
    }
    private void createSkills(List<Skills> skillsList)
    {
        foreach (var skill in skillsList)
        {
            GameObject skillObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = skillObject.transform.Find("Title").GetComponent<Text>();
            Title.text = skill.name.Replace("_", " ");

            RawImage Image = skillObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = skill.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(skill, MainPanel));
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

            RawImage rareImage = skillObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skill.rare}");
            rareImage.texture = rareTexture;

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    private void createSymbols(List<Symbols> symbolsList)
    {
        foreach (var symbol in symbolsList)
        {
            GameObject symbolObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = symbolObject.transform.Find("Title").GetComponent<Text>();
            Title.text = symbol.name.Replace("_", " ");

            RawImage Image = symbolObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = symbol.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(symbol, MainPanel));
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

            RawImage rareImage = symbolObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{symbol.rare}");
            rareImage.texture = rareTexture;

            RawImage rareBackgroundImage = symbolObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    private void createTitles(List<Titles> titlesList)
    {
        foreach (var title in titlesList)
        {
            GameObject titleObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = titleObject.transform.Find("Title").GetComponent<Text>();
            Title.text = title.name.Replace("_", " ");

            RawImage Image = titleObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = title.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(title, MainPanel));
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

            RawImage rareImage = titleObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{title.rare}");
            rareImage.texture = rareTexture;

            RawImage rareBackgroundImage = titleObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    private void createCardMilitary(List<CardMilitary> militaryList)
    {
        foreach (var military in militaryList)
        {
            GameObject militaryObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = militaryObject.transform.Find("Title").GetComponent<Text>();
            Title.text = military.name.Replace("_", " ");

            RawImage Image = militaryObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = military.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(military, MainPanel));
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

            RawImage rareImage = militaryObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.rare}");
            rareImage.texture = rareTexture;

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 250);
            }
        }
    }
    private void createCardSpell(List<CardSpell> spellList)
    {
        foreach (var spell in spellList)
        {
            GameObject spellObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = spell.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spell.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(spell, MainPanel));
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

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
            rareImage.texture = rareTexture;

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 250);
            }
        }
    }
    private void createMagicFormationCircle(List<MagicFormationCircle> magicFormationCircles)
    {
        foreach (var magicFormationCircle in magicFormationCircles)
        {
            GameObject magicFormationCircleObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = magicFormationCircleObject.transform.Find("Title").GetComponent<Text>();
            Title.text = magicFormationCircle.name.Replace("_", " ");

            RawImage Image = magicFormationCircleObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = magicFormationCircle.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(magicFormationCircle, MainPanel));
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

            RawImage frameImage = magicFormationCircleObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = magicFormationCircleObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{magicFormationCircle.rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createRelics(List<Relics> relics)
    {
        foreach (var relic in relics)
        {
            GameObject relicObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = relicObject.transform.Find("Title").GetComponent<Text>();
            Title.text = relic.name.Replace("_", " ");

            RawImage Image = relicObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = relic.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(relic, MainPanel));
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

            RawImage frameImage = relicObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = relicObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relic.rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createCardColonels(List<CardColonels> cardColonels)
    {
        foreach (var colonels in cardColonels)
        {
            GameObject spellObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = colonels.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = colonels.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(colonels, MainPanel));
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

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{colonels.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createCardGenerals(List<CardGenerals> cardGenerals)
    {
        foreach (var generals in cardGenerals)
        {
            GameObject spellObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = generals.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = generals.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(generals, MainPanel));
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

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{generals.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createCardAdmirals(List<CardAdmirals> cardAdmirals)
    {
        foreach (var admirals in cardAdmirals)
        {
            GameObject spellObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = admirals.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = admirals.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(admirals, MainPanel));
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

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{admirals.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createCardHeroesForSummon(List<CardHeroes> cards)
    {
        foreach (var card in cards)
        {
            GameObject cardObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            // if (gridLayout != null)
            // {
            //     gridLayout.cellSize = new Vector2(200, 250);
            // }
        }
    }
    private void createBooksForSummon(List<Books> books)
    {
        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = bookObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = book.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            Image.SetNativeSize();

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            // rectTransform.sizeDelta = new Vector2(300f, 375f);
            if (texture.width < 1400 && texture.height < 1400 && texture.width > 700 && texture.height > 700)
            {
                Image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (texture.width > 1000 && texture.height <= 2100 && texture.width < 2000 && texture.height > 1000)
            {
                Image.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
            else if (texture.width <= 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            else if (texture.width <= 700 && texture.height <= 1100)
            {
                Image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (texture.width > 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else
            {
                Image.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            GridLayoutGroup gridLayout = PositionPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.spacing = new Vector2(100, 0);
            }
        }
    }
    private void createCardCaptainsForSummon(List<CardCaptains> captains)
    {
        foreach (var captain in captains)
        {
            GameObject captainObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = captainObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = captain.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            // if (gridLayout != null)
            // {
            //     gridLayout.cellSize = new Vector2(200, 250);
            // }
        }
    }
    private void createCardMonstersForSummon(List<CardMonsters> monsters)
    {
        foreach (var monster in monsters)
        {
            GameObject monsterObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = monsterObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = monster.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            // if (gridLayout != null)
            // {
            //     gridLayout.cellSize = new Vector2(200, 250);
            // }
        }
    }
    private void createCardMilitaryForSummon(List<CardMilitary> militaries)
    {
        foreach (var military in militaries)
        {
            GameObject militaryObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = militaryObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = military.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            // if (gridLayout != null)
            // {
            //     gridLayout.cellSize = new Vector2(200, 250);
            // }
        }
    }
    private void createCardSpellForSummon(List<CardSpell> spells)
    {
        foreach (var spell in spells)
        {
            GameObject spellObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spell.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            // if (gridLayout != null)
            // {
            //     gridLayout.cellSize = new Vector2(200, 250);
            // }
        }
    }
    private void createCardColonelsForSummon(List<CardColonels> colonels)
    {
        foreach (var captain in colonels)
        {
            GameObject captainObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = captainObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = captain.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
        }
    }
    private void createCardGeneralsForSummon(List<CardGenerals> generals)
    {
        foreach (var captain in generals)
        {
            GameObject captainObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = captainObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = captain.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
        }
    }
    private void createCardAdmiralsForSummon(List<CardAdmirals> admirals)
    {
        foreach (var captain in admirals)
        {
            GameObject captainObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = captainObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = captain.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
        }
    }
    private void createTalisman(List<Talisman> talismans)
    {
        foreach (var talisman in talismans)
        {
            GameObject talismanObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = talismanObject.transform.Find("Title").GetComponent<Text>();
            Title.text = talisman.name.Replace("_", " ");

            RawImage Image = talismanObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = talisman.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(talisman, MainPanel));
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

            RawImage frameImage = talismanObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = talismanObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{talisman.rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createPuppet(List<Puppet> puppets)
    {
        foreach (var puppet in puppets)
        {
            GameObject puppetObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = puppetObject.transform.Find("Title").GetComponent<Text>();
            Title.text = puppet.name.Replace("_", " ");

            RawImage Image = puppetObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = puppet.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(puppet, MainPanel));
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

            RawImage frameImage = puppetObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = puppetObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{puppet.rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createAlchemy(List<Alchemy> alchemies)
    {
        foreach (var alchemy in alchemies)
        {
            GameObject alchemyObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = alchemyObject.transform.Find("Title").GetComponent<Text>();
            Title.text = alchemy.name.Replace("_", " ");

            RawImage Image = alchemyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = alchemy.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(alchemy, MainPanel));
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

            RawImage frameImage = alchemyObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = alchemyObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{alchemy.rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createForge(List<Forge> forges)
    {
        foreach (var forge in forges)
        {
            GameObject forgeObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = forgeObject.transform.Find("Title").GetComponent<Text>();
            Title.text = forge.name.Replace("_", " ");

            RawImage Image = forgeObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = forge.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(forge, MainPanel));
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

            RawImage frameImage = forgeObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = forgeObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{forge.rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    private void createCardLife(List<CardLife> cards)
    {
        foreach (var card in cards)
        {
            GameObject cardObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
            Title.text = card.name.Replace("_", " ");

            RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
            rareImage.texture = rareTexture;

            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(card, MainPanel));
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

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 250);
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

            if (mainType.Equals("CardHeroes"))
            {
                totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cards = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> army = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaboration"))
            {
                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaboration = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset);
                createCollaboration(collaboration);
            }
            // else if (mainType.Equals("Equipments"))
            // {
            //     Equipments equipmentManager = new Equipments();
            //     totalRecord = equipmentManager.GetEquipmentsCount(subType);
            //     totalPage = CalculateTotalPages(totalRecord, pageSize);
            //     currentPage = currentPage + 1;
            //     offset = offset + pageSize;
            //     List<Equipments> equipments = equipmentManager.GetEquipments(subType, pageSize, offset);
            //     createEquipments(equipments);
            // }
            else if (mainType.Equals("Medals"))
            {
                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> monstersList = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId,subType, pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> petsList = UserPetsService.Create().GetUserPets(User.CurrentUserId, subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skillsList = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbolsList = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titlesList = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> militaryList = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> spellList = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relicsList = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset);
                createRelics(relicsList);
            }
            else if (mainType.Equals("Talisman"))
            {
                totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset);
                createTalisman(talismans);
            }
            else if (mainType.Equals("Puppet"))
            {
                totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset);
                createPuppet(puppets);
            }
            else if (mainType.Equals("Alchemy"))
            {
                totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset);
                createAlchemy(alchemies);
            }
            else if (mainType.Equals("Forge"))
            {
                totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset);
                createForge(forges);
            }
            else if (mainType.Equals("CardColonels"))
            {
                totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subType, pageSize, offset);
                createCardColonels(cardColonels);
            }
            else if (mainType.Equals("CardGenerals"))
            {
                totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subType, pageSize, offset);
                createCardGenerals(cardGenerals);
            }
            else if (mainType.Equals("CardAdmirals"))
            {
                totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subType, pageSize, offset);
                createCardAdmirals(cardAdmirals);
            }
            else if (mainType.Equals("CardLife"))
            {
                totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset);
                createCardLife(cardLives);
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

            if (mainType.Equals("CardHeroes"))
            {
                totalRecord = UserCardHeroesService.Create().GetUserCardHeroesCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cards = UserCardHeroesService.Create().GetUserCardHeroes(User.CurrentUserId, subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                totalRecord = UserBooksService.Create().GetUserBooksCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = UserBooksService.Create().GetUserBooks(User.CurrentUserId, subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                totalRecord = UserCardCaptainsService.Create().GetUserCardCaptainsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> army = UserCardCaptainsService.Create().GetUserCardCaptains(User.CurrentUserId, subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                totalRecord = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipments(User.CurrentUserId, subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaboration"))
            {
                totalRecord = UserCollaborationService.Create().GetUserCollaborationCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaboration = UserCollaborationService.Create().GetUserCollaboration(User.CurrentUserId, pageSize, offset);
                createCollaboration(collaboration);
            }
            // else if (mainType.Equals("Equipments"))
            // {
            //     totalRecord = UserEquipmentsService.Create().GetEquipmentsCount(subType);
            //     totalPage = CalculateTotalPages(totalRecord, pageSize);
            //     currentPage = currentPage - 1;
            //     offset = offset - pageSize;
            //     List<Equipments> equipments = equipmentManager.GetEquipments(subType, pageSize, offset);
            //     createEquipments(equipments);
            // }
            else if (mainType.Equals("Medals"))
            {
                totalRecord = UserMedalsService.Create().GetUserMedalsCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = UserMedalsService.Create().GetUserMedals(User.CurrentUserId, pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                totalRecord = UserCardMonstersService.Create().GetUserCardMonstersCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monstersList = UserCardMonstersService.Create().GetUserCardMonsters(User.CurrentUserId,subType, pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                totalRecord = UserPetsService.Create().GetUserPetsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> petsList = UserPetsService.Create().GetUserPets(User.CurrentUserId, subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                totalRecord = UserSkillsService.Create().GetUserSkillsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skillsList = UserSkillsService.Create().GetUserSkills(User.CurrentUserId, subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                totalRecord = UserSymbolsService.Create().GetUserSymbolsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbolsList = UserSymbolsService.Create().GetUserSymbols(User.CurrentUserId, subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                totalRecord = UserTitlesService.Create().GetUserTitlesCount(User.CurrentUserId);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titlesList = UserTitlesService.Create().GetUserTitles(User.CurrentUserId, pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                totalRecord = UserCardMilitaryService.Create().GetUserCardMilitaryCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> militaryList = UserCardMilitaryService.Create().GetUserCardMilitary(User.CurrentUserId, subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                totalRecord = UserCardSpellService.Create().GetUserCardSpellCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> spellList = UserCardSpellService.Create().GetUserCardSpell(User.CurrentUserId, subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                totalRecord = UserMagicFormationCircleService.Create().GetUserMagicFormationCircleCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = UserMagicFormationCircleService.Create().GetUserMagicFormationCircle(User.CurrentUserId, subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                totalRecord = UserRelicsService.Create().GetUserRelicsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relicsList = UserRelicsService.Create().GetUserRelics(User.CurrentUserId, subType, pageSize, offset);
                createRelics(relicsList);
            }
            else if (mainType.Equals("Talisman"))
            {
                totalRecord = UserTalismanService.Create().GetUserTalismanCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = UserTalismanService.Create().GetUserTalisman(User.CurrentUserId, subType, pageSize, offset);
                createTalisman(talismans);
            }
            else if (mainType.Equals("Puppet"))
            {
                totalRecord = UserPuppetService.Create().GetUserPuppetCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = UserPuppetService.Create().GetUserPuppet(User.CurrentUserId, subType, pageSize, offset);
                createPuppet(puppets);
            }
            else if (mainType.Equals("Alchemy"))
            {
                totalRecord = UserAlchemyService.Create().GetUserAlchemyCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = UserAlchemyService.Create().GetUserAlchemy(User.CurrentUserId, subType, pageSize, offset);
                createAlchemy(alchemies);
            }
            else if (mainType.Equals("Forge"))
            {
                totalRecord = UserForgeService.Create().GetUserForgeCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = UserForgeService.Create().GetUserForge(User.CurrentUserId, subType, pageSize, offset);
                createForge(forges);
            }
            else if (mainType.Equals("CardColonels"))
            {
                totalRecord = UserCardColonelsService.Create().GetUserCardColonelsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = UserCardColonelsService.Create().GetUserCardColonels(User.CurrentUserId, subType, pageSize, offset);
                createCardColonels(cardColonels);
            }
            else if (mainType.Equals("CardGenerals"))
            {
                totalRecord = UserCardGeneralsService.Create().GetUserCardGeneralsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetUserCardGenerals(User.CurrentUserId, subType, pageSize, offset);
                createCardGenerals(cardGenerals);
            }
            else if (mainType.Equals("CardAdmirals"))
            {
                totalRecord = UserCardAdmiralsService.Create().GetUserCardAdmiralsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetUserCardAdmirals(User.CurrentUserId, subType, pageSize, offset);
                createCardAdmirals(cardAdmirals);
            }
            else if (mainType.Equals("CardLife"))
            {
                totalRecord = UserCardLifeService.Create().GetUserCardLifeCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = UserCardLifeService.Create().GetUserCardLife(User.CurrentUserId, subType, pageSize, offset);
                createCardLife(cardLives);
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
    void AddClickListener(EventTrigger trigger, System.Action callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { callback(); });
        trigger.triggers.Add(entry);
    }
    public void loadScence(){
        FindAnyObjectByType<SceneLoader>().LoadScene("TeamsScenes");
    }

}
