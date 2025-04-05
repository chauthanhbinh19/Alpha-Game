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
    private GameObject buttonPrefab;
    private GameObject DictionaryPanel;
    private GameObject PopupMenuPanelPrefab;
    private GameObject ArenaPanelPrefab;
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private GameObject cardsPrefab3;
    private Transform DictionaryContentPanel;
    private Transform positionPanel;
    private Button CloseButton;
    private Button HomeButton;
    private Button SummonButton;
    private Button Summon10Button;
    private GameObject equipmentsPrefab;
    private Transform TabButtonPanel;
    private GameObject buttonPrefab2;
    private GameObject buttonPrefab3;
    private GameObject SummonPanel;
    private GameObject PositionPrefab;
    private GameObject TeamsPrefab;
    private GameObject TypePrefab;
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
    private TextMeshProUGUI teamMembersText;
    private TextMeshProUGUI powerText;
    private string buttonType;
    private string selectedOptionName;
    private int team_id;
    private int team_limit;
    private int team_offset;
    private Transform choseTeam;
    List<CardDragHandler> cardDragHandlers = new List<CardDragHandler>();
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        mainMenuPanel = UIManager.Instance.GetTransform("mainMenuButtonPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        buttonPrefab2 = UIManager.Instance.GetGameObject("TabButton2");
        buttonPrefab3 = UIManager.Instance.GetGameObject("TabButton3");
        DictionaryPanel = UIManager.Instance.GetGameObject("DictionaryPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        cardsPrefab3 = UIManager.Instance.GetGameObject("CardsThirdPrefab");
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        SummonPanel = UIManager.Instance.GetGameObject("SummonPanelPrefab");
        PositionPrefab = UIManager.Instance.GetGameObject("PositionPrefab");
        SummonMainMenuPanel = UIManager.Instance.GetTransform("summonPanel");
        TeamsPrefab = UIManager.Instance.GetGameObject("TeamsPrefab");
        TypePrefab = UIManager.Instance.GetGameObject("TypePrefab");
        PopupMenuPanelPrefab = UIManager.Instance.GetGameObject("PopupMenuPanelPrefab");
        ArenaPanelPrefab = UIManager.Instance.GetGameObject("ArenaPanelPrefab");

        AssignButtonEvent("Button_1", mainMenuPanel, () => GetType("CardHeroes"));
        AssignButtonEvent("Button_2", mainMenuPanel, () => GetType("Books"));
        AssignButtonEvent("Button_3", mainMenuPanel, () => GetType("Pets"));
        AssignButtonEvent("Button_4", mainMenuPanel, () => GetType("CardCaptains"));
        AssignButtonEvent("Button_5", mainMenuPanel, () => GetType("CollaborationEquipments"));
        AssignButtonEvent("Button_6", mainMenuPanel, () => GetType("CardMilitary"));
        AssignButtonEvent("Button_7", mainMenuPanel, () => GetType("CardSpell"));
        AssignButtonEvent("Button_8", mainMenuPanel, () => GetType("Collaborations"));
        AssignButtonEvent("Button_9", mainMenuPanel, () => GetType("CardMonsters"));
        // AssignButtonEvent("Button_10", mainMenuPanel, () => GetType("Equipments"));
        AssignButtonEvent("Button_11", mainMenuPanel, () => GetType("Medals"));
        AssignButtonEvent("Button_12", mainMenuPanel, () => GetType("Skills"));
        AssignButtonEvent("Button_13", mainMenuPanel, () => GetType("Symbols"));
        AssignButtonEvent("Button_14", mainMenuPanel, () => GetType("Titles"));
        AssignButtonEvent("Button_15", SummonMainMenuPanel, () => GetType("MagicFormationCircle"));
        AssignButtonEvent("Button_16", SummonMainMenuPanel, () => GetType("Relics"));
        AssignButtonEvent("Button_17", mainMenuPanel, () => GetType("Bag"));
        AssignButtonEvent("Button_18", mainMenuPanel, () => GetType("Teams"));
        AssignButtonEvent("Button_19", mainMenuPanel, () => GetType("CardColonels"));
        AssignButtonEvent("Button_20", mainMenuPanel, () => GetType("CardGenerals"));
        AssignButtonEvent("Button_21", mainMenuPanel, () => GetType("CardAdmirals"));

        AssignButtonEvent("Button_22", SummonMainMenuPanel, () => GetType("SummonCardHeroes"));
        AssignButtonEvent("Button_23", SummonMainMenuPanel, () => GetType("SummonBooks"));
        AssignButtonEvent("Button_24", SummonMainMenuPanel, () => GetType("SummonCardCaptains"));
        AssignButtonEvent("Button_25", SummonMainMenuPanel, () => GetType("SummonCardMonsters"));
        AssignButtonEvent("Button_26", SummonMainMenuPanel, () => GetType("SummonCardMilitary"));
        AssignButtonEvent("Button_27", SummonMainMenuPanel, () => GetType("SummonCardSpell"));
        AssignButtonEvent("Button_28", SummonMainMenuPanel, () => GetType("SummonCardColonels"));
        AssignButtonEvent("Button_29", SummonMainMenuPanel, () => GetType("SummonCardGenerals"));
        AssignButtonEvent("Button_30", SummonMainMenuPanel, () => GetType("SummonCardAdmirals"));
        AssignButtonEvent("Button_32", SummonMainMenuPanel, () => GetType("Gallery"));
        AssignButtonEvent("Button_33", SummonMainMenuPanel, () => GetType("Collection"));
        AssignButtonEvent("Button_34", SummonMainMenuPanel, () => GetType("Equipments"));
        AssignButtonEvent("Button_35", SummonMainMenuPanel, () => GetType("Arena"));
        // GetCardsType();
    }

    void Update()
    {

    }
    private void CreateButton(int index, string itemName, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(buttonPrefab3, panel);
        newButton.name = "Button_" + index;

        // Gán tên cho itemName
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = itemName;
        }
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
        && !mainType.Equals("Equipments"))
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
    public List<string> GetUniqueTypes(string type)
    {
        if (type.Equals("CardHeroes"))
        {
            return CardHeroes.GetUniqueCardHeroTypes();
        }
        else if (type.Equals("Books"))
        {
            return Books.GetUniqueBookTypes();
        }
        else if (type.Equals("CardCaptains"))
        {
            return CardCaptains.GetUniqueCardCaptainsTypes();
        }
        else if (type.Equals("CollaborationEquipments"))
        {
            return CollaborationEquipment.GetUniqueCollaborationEquipmentTypes();
        }
        else if (type.Equals("Equipments"))
        {
            return Equipments.GetUniqueEquipmentsTypes();
        }
        else if (type.Equals("Pets"))
        {
            return Pets.GetUniquePetsTypes();
        }
        else if (type.Equals("Skills"))
        {
            return Skills.GetUniqueSkillsTypes();
        }
        else if (type.Equals("Symbols"))
        {
            return Symbols.GetUniqueSymbolsTypes();
        }
        else if (type.Equals("CardMilitary"))
        {
            return CardMilitary.GetUniqueCardMilitaryTypes();
        }
        else if (type.Equals("CardSpell"))
        {
            return CardSpell.GetUniqueCardSpellTypes();
        }
        else if (type.Equals("MagicFormationCircle"))
        {
            return MagicFormationCircle.GetUniqueMagicFormationCircleTypes();
        }
        else if (type.Equals("Relics"))
        {
            return Relics.GetUniqueRelicsTypes();
        }
        else if (type.Equals("CardColonels"))
        {
            return CardColonels.GetUniqueCardColonelsTypes();
        }
        else if (type.Equals("CardGenerals"))
        {
            return CardGenerals.GetUniqueCardGeneralsTypes();
        }
        else if (type.Equals("CardAdmirals"))
        {
            return CardAdmirals.GetUniqueCardAdmiralsTypes();
        }
        else if (type.Equals("SummonCardHeroes"))
        {
            return CardHeroes.GetUniqueCardHeroTypes();
        }
        else if (type.Equals("SummonBooks"))
        {
            return Books.GetUniqueBookTypes();
        }
        else if (type.Equals("SummonCardCaptains"))
        {
            return CardCaptains.GetUniqueCardCaptainsTypes();
        }
        else if (type.Equals("SummonCardMilitary"))
        {
            return CardMilitary.GetUniqueCardMilitaryTypes();
        }
        else if (type.Equals("SummonCardSpell"))
        {
            return CardSpell.GetUniqueCardSpellTypes();
        }
        else if (type.Equals("SummonCardColonels"))
        {
            return CardColonels.GetUniqueCardColonelsTypes();
        }
        else if (type.Equals("SummonCardGenerals"))
        {
            return CardGenerals.GetUniqueCardGeneralsTypes();
        }
        else if (type.Equals("SummonCardAdmirals"))
        {
            return CardAdmirals.GetUniqueCardAdmiralsTypes();
        }
        return new List<string>();
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
        else if (mainType.Equals("Teams"))
        {
            createTeams();
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
            Currency currency = new Currency();
            List<Currency> currencies = new List<Currency>();
            currencies = currency.GetUserCurrency();
            FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);
        }
        List<string> uniqueTypes = GetUniqueTypes(mainType);
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
                    if (mainType.Equals("CardHeroes"))
                    {
                        CardHeroes cardsManager = new CardHeroes();
                        List<CardHeroes> cards = cardsManager.GetUserCardHeroes(subtype, pageSize, offset);
                        createCardHeroes(cards);

                        totalRecord = cardsManager.GetUserCardHeroesCount(subtype);
                    }
                    else if (mainType.Equals("Books"))
                    {
                        Books booksManager = new Books();
                        List<Books> books = booksManager.GetUserBooks(subtype, pageSize, offset);
                        createBooks(books);

                        totalRecord = booksManager.GetUserBooksCount(subtype);
                    }
                    else if (mainType.Equals("CardCaptains"))
                    {
                        CardCaptains cardCaptainsManager = new CardCaptains();
                        List<CardCaptains> captains = cardCaptainsManager.GetUserCardCaptains(subtype, pageSize, offset);
                        createCardCaptains(captains);

                        totalRecord = cardCaptainsManager.GetUserCardCaptainsCount(subtype);
                    }
                    else if (mainType.Equals("CollaborationEquipments"))
                    {
                        CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                        List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetUserCollaborationEquipments(subtype, pageSize, offset);
                        createCollaborationEquipments(collaborationEquipments);

                        totalRecord = collaborationEquipmentManager.GetUserCollaborationEquipmentCount(subtype);
                    }
                    else if (mainType.Equals("Equipments"))
                    {
                        Equipments equipmentsManager = new Equipments();
                        List<Equipments> equipments = equipmentsManager.GetEquipments(subtype, pageSize, offset);
                        createEquipments(equipments);

                        totalRecord = equipmentsManager.GetEquipmentsCount(subtype);
                    }
                    else if (mainType.Equals("Pets"))
                    {
                        Pets petsManager = new Pets();
                        List<Pets> pets = petsManager.GetUserPets(subtype, pageSize, offset);
                        createPets(pets);

                        totalRecord = petsManager.GetUserPetsCount(subtype);
                    }
                    else if (mainType.Equals("Skills"))
                    {
                        Skills skillsManager = new Skills();
                        List<Skills> skills = skillsManager.GetUserSkills(subtype, pageSize, offset);
                        createSkills(skills);

                        totalRecord = skillsManager.GetUserSkillsCount(subtype);
                    }
                    else if (mainType.Equals("Symbols"))
                    {
                        Symbols symbolsManager = new Symbols();
                        List<Symbols> symbols = symbolsManager.GetUserSymbols(subtype, pageSize, offset);
                        createSymbols(symbols);

                        totalRecord = symbolsManager.GetUserSymbolsCount(subtype);
                    }
                    else if (mainType.Equals("CardMilitary"))
                    {
                        CardMilitary militaryManager = new CardMilitary();
                        List<CardMilitary> militaryList = militaryManager.GetUserCardMilitary(subtype, pageSize, offset);
                        createCardMilitary(militaryList);

                        totalRecord = militaryManager.GetUserCardMilitaryCount(subType);
                    }
                    else if (mainType.Equals("CardSpell"))
                    {
                        CardSpell spellManager = new CardSpell();
                        List<CardSpell> spellList = spellManager.GetUserCardSpell(subtype, pageSize, offset);
                        createCardSpell(spellList);

                        totalRecord = spellManager.GetUserCardSpellCount(subType);
                    }
                    else if (mainType.Equals("MagicFormationCircle"))
                    {
                        MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                        List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetUserMagicFormationCircle(subType, pageSize, offset);
                        createMagicFormationCircle(magicFormationCircles);

                        totalRecord = magicFormationCircleManager.GetUserMagicFormationCircleCount(subType);
                    }
                    else if (mainType.Equals("Relics"))
                    {
                        Relics relicsManager = new Relics();
                        List<Relics> relicsList = relicsManager.GetUserRelics(subType, pageSize, offset);
                        createRelics(relicsList);

                        totalRecord = relicsManager.GetUserRelicsCount(subType);
                    }
                    else if (mainType.Equals("CardColonels"))
                    {
                        CardColonels colonelsManager = new CardColonels();
                        List<CardColonels> colonels = colonelsManager.GetUserCardColonels(subtype, pageSize, offset);
                        createCardColonels(colonels);

                        totalRecord = colonelsManager.GetUserCardColonelsCount(subType);
                    }
                    else if (mainType.Equals("CardGenerals"))
                    {
                        CardGenerals generalsManager = new CardGenerals();
                        List<CardGenerals> relicsList = generalsManager.GetUserCardGenerals(subtype, pageSize, offset);
                        createCardGenerals(relicsList);

                        totalRecord = generalsManager.GetUserCardGeneralsCount(subType);
                    }
                    else if (mainType.Equals("CardAdmirals"))
                    {
                        CardAdmirals admiralsManager = new CardAdmirals();
                        List<CardAdmirals> relicsList = admiralsManager.GetUserCardAdmirals(subtype, pageSize, offset);
                        createCardAdmirals(relicsList);

                        totalRecord = admiralsManager.GetUserCardAdmiralsCount(subType);
                    }
                    else if (mainType.Equals("SummonCardHeroes"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        CardHeroes cardsManager = new CardHeroes();
                        List<CardHeroes> cards = cardsManager.GetCardHeroesRandom(subtype, 3);
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
                        Books booksManager = new Books();
                        List<Books> books = booksManager.GetBooksRandom(subtype, 3);
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
                        CardCaptains captainsManager = new CardCaptains();
                        List<CardCaptains> captains = captainsManager.GetCardCaptainsRandom(subtype, 3);
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
                        CardMilitary militaryManager = new CardMilitary();
                        List<CardMilitary> military = militaryManager.GetCardMilitaryRandom(subtype, 3);
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
                        CardSpell militaryManager = new CardSpell();
                        List<CardSpell> spell = militaryManager.GetCardSpellRandom(subtype, 3);
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
                    else if (mainType.Equals("SummonCardColonels"))
                    {
                        titleText2.text = "Summon " + string.Concat(subtype.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                        CardColonels colonelsManager = new CardColonels();
                        List<CardColonels> colonels = colonelsManager.GetCardColonelsRandom(subtype, 3);
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
                        CardGenerals generalsManager = new CardGenerals();
                        List<CardGenerals> relicsList = generalsManager.GetCardGeneralsRandom(subtype, 3);
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
                        CardAdmirals admiralsManager = new CardAdmirals();
                        List<CardAdmirals> relicsList = admiralsManager.GetCardAdmiralsRandom(subtype, 3);
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

                    if (!mainType.Equals("SummonCardHeroes") && !mainType.Equals("SummonBooks") && !mainType.Equals("SummonCardCaptains") &&
                    !mainType.Equals("SummonCardColonels") && !mainType.Equals("SummonCardGenerals") && !mainType.Equals("SummonCardAdmirals") &&
                    !mainType.Equals("SummonCardMonsters") && !mainType.Equals("SummonCardMilitary") && !mainType.Equals("SummonCardSpell"))
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
            if (mainType.Equals("Collaboration"))
            {
                Collaboration collaborationManager = new Collaboration();
                List<Collaboration> collaborations = collaborationManager.GetUserCollaboration(pageSize, offset);
                createCollaboration(collaborations);

                totalRecord = collaborationManager.GetUserCollaborationCount();
            }
            else if (mainType.Equals("Medals"))
            {
                Medals medalsManager = new Medals();
                List<Medals> medalsList = medalsManager.GetUserMedals(pageSize, offset);
                createMedals(medalsList);

                totalRecord = medalsManager.GetUserMedalsCount();
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                List<CardMonsters> monstersList = monstersManager.GetUserCardMonsters(pageSize, offset);
                createCardMonsters(monstersList);

                totalRecord = monstersManager.GetUserCardMonstersCount();
            }
            else if (mainType.Equals("Titles"))
            {
                Titles titleManager = new Titles();
                List<Titles> titlesList = titleManager.GetUserTitles(pageSize, offset);
                createTitles(titlesList);

                totalRecord = titleManager.GetUserTitlesCount();
            }
            else if (mainType.Equals("SummonCardMonsters"))
            {
                titleText2.text = "Summon " + string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
                CardMonsters monstersManager = new CardMonsters();
                List<CardMonsters> monsters = monstersManager.GetCardMonstersRandom(3);
                createCardMonstersForSummon(monsters);

                SummonButton.onClick.AddListener(() =>
                {
                    FindObjectOfType<GachaSystem>().Summon("monsters", "none", SummonAreaPanel, 1);
                });
                Summon10Button.onClick.AddListener(() =>
                {
                    FindObjectOfType<GachaSystem>().Summon("monsters", "none", SummonAreaPanel, 10);
                });
            }

            if (!mainType.Equals("SummonCardMonsters") && !mainType.Equals("Teams")
            && !mainType.Equals("Gallery") && !mainType.Equals("Collection")
            && !mainType.Equals("Equipments") && !mainType.Equals("Arena"))
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
            CardHeroes cardsManager = new CardHeroes();
            List<CardHeroes> cards = cardsManager.GetUserCardHeroes(type, pageSize, offset);
            createCardHeroes(cards);

            totalRecord = cardsManager.GetUserCardHeroesCount(type);
        }
        else if (mainType.Equals("Books"))
        {
            Books booksManager = new Books();
            List<Books> books = booksManager.GetUserBooks(type, pageSize, offset);
            createBooks(books);

            totalRecord = booksManager.GetUserBooksCount(type);
        }
        else if (mainType.Equals("CardCaptains"))
        {
            CardCaptains captainsManager = new CardCaptains();
            List<CardCaptains> captains = captainsManager.GetUserCardCaptains(type, pageSize, offset);
            createCardCaptains(captains);

            totalRecord = captainsManager.GetUserCardCaptainsCount(type);
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
            List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetUserCollaborationEquipments(type, pageSize, offset);
            createCollaborationEquipments(collaborationEquipments);

            totalRecord = collaborationEquipmentManager.GetUserCollaborationEquipmentCount(type);
        }
        else if (mainType.Equals("Equipments"))
        {
            Equipments equipmentsManager = new Equipments();
            List<Equipments> equipments = equipmentsManager.GetEquipments(type, pageSize, offset);
            createEquipments(equipments);

            totalRecord = equipmentsManager.GetEquipmentsCount(type);
        }
        else if (mainType.Equals("Pets"))
        {
            Pets petsManager = new Pets();
            List<Pets> pets = petsManager.GetUserPets(type, pageSize, offset);
            createPets(pets);

            totalRecord = petsManager.GetUserPetsCount(type);
        }
        else if (mainType.Equals("Skills"))
        {
            Skills skillsManager = new Skills();
            List<Skills> skills = skillsManager.GetUserSkills(type, pageSize, offset);
            createSkills(skills);

            totalRecord = skillsManager.GetUserSkillsCount(type);
        }
        else if (mainType.Equals("Symbols"))
        {
            Symbols symbolsManager = new Symbols();
            List<Symbols> symbols = symbolsManager.GetUserSymbols(type, pageSize, offset);
            createSymbols(symbols);

            totalRecord = symbolsManager.GetUserSymbolsCount(type);
        }
        else if (mainType.Equals("CardMilitary"))
        {
            CardMilitary militaryManager = new CardMilitary();
            List<CardMilitary> militaryList = militaryManager.GetUserCardMilitary(type, pageSize, offset);
            createCardMilitary(militaryList);

            totalRecord = militaryManager.GetUserCardMilitaryCount(type);
        }
        else if (mainType.Equals("CardSpell"))
        {
            CardSpell spellManager = new CardSpell();
            List<CardSpell> spellList = spellManager.GetUserCardSpell(type, pageSize, offset);
            createCardSpell(spellList);

            totalRecord = spellManager.GetUserCardSpellCount(type);
        }
        else if (mainType.Equals("MagicFormationCircle"))
        {
            MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
            List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircleCollection(type, pageSize, offset);
            createMagicFormationCircle(magicFormationCircles);

            totalRecord = magicFormationCircleManager.GetMagicFormationCircleCount(type);
        }
        else if (mainType.Equals("Relics"))
        {
            Relics relicsManager = new Relics();
            List<Relics> relicsList = relicsManager.GetUserRelics(type, pageSize, offset);
            createRelics(relicsList);

            totalRecord = relicsManager.GetUserRelicsCount(type);
        }
        else if (mainType.Equals("CardColonels"))
        {
            CardColonels colonelsManager = new CardColonels();
            List<CardColonels> colonels = colonelsManager.GetUserCardColonels(type, pageSize, offset);
            createCardColonels(colonels);

            totalRecord = colonelsManager.GetUserCardColonelsCount(type);
        }
        else if (mainType.Equals("CardGenerals"))
        {
            CardGenerals generalsManager = new CardGenerals();
            List<CardGenerals> relicsList = generalsManager.GetUserCardGenerals(type, pageSize, offset);
            createCardGenerals(relicsList);

            totalRecord = generalsManager.GetUserCardGeneralsCount(type);
        }
        else if (mainType.Equals("CardAdmirals"))
        {
            CardAdmirals admiralsManager = new CardAdmirals();
            List<CardAdmirals> relicsList = admiralsManager.GetUserCardAdmirals(type, pageSize, offset);
            createCardAdmirals(relicsList);

            totalRecord = admiralsManager.GetUserCardAdmiralsCount(type);
        }
        else if (mainType.Equals("SummonCardHeroes"))
        {
            titleText2.text = "Summon " + string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Cards";
            CardHeroes cardsManager = new CardHeroes();
            List<CardHeroes> cards = cardsManager.GetCardHeroesRandom(type, 3);
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
            Books booksManager = new Books();
            List<Books> books = booksManager.GetBooksRandom(type, 3);
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
            CardCaptains captainsManager = new CardCaptains();
            List<CardCaptains> captains = captainsManager.GetCardCaptainsRandom(type, 3);
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
            CardMilitary militaryManager = new CardMilitary();
            List<CardMilitary> military = militaryManager.GetCardMilitaryRandom(type, 3);
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
            CardSpell militaryManager = new CardSpell();
            List<CardSpell> spell = militaryManager.GetCardSpellRandom(type, 3);
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
            CardColonels colonelsManager = new CardColonels();
            List<CardColonels> colonels = colonelsManager.GetCardColonelsRandom(type, 3);
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
            CardGenerals generalsManager = new CardGenerals();
            List<CardGenerals> relicsList = generalsManager.GetCardGeneralsRandom(type, 3);
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
            CardAdmirals admiralsManager = new CardAdmirals();
            List<CardAdmirals> relicsList = admiralsManager.GetCardAdmiralsRandom(type, 3);
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
    private void createTeams()
    {
        GameObject teamsObject = Instantiate(TeamsPrefab, MainPanel);
        Transform tempLeftContent = teamsObject.transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = teamsObject.transform.Find("ScrollViewRight/Viewport/Content");
        titleText = teamsObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        ScrollRect scrollRect = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition").GetComponent<ScrollRect>();
        positionPanel = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        Transform typeContent = teamsObject.transform.Find("DictionaryCards/ScrollViewType/Viewport/Content");
        CloseButton = teamsObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            foreach (Transform child in MainPanel)
            {
                Destroy(child.gameObject);
            }
        });
        HomeButton = teamsObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        RawImage arrowUp = teamsObject.transform.Find("DictionaryCards/ScrollViewArrowUp").GetComponent<RawImage>();
        RawImage arrowDown = teamsObject.transform.Find("DictionaryCards/ScrollViewArrowDown").GetComponent<RawImage>();
        TMP_Dropdown dropdownType = teamsObject.transform.Find("DictionaryCards/DropdownType").GetComponent<TMP_Dropdown>();
        teamMembersText = teamsObject.transform.Find("DictionaryCards/TeamMembersText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI typeText = teamsObject.transform.Find("DictionaryCards/TypeText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI teamTitleText = teamsObject.transform.Find("DictionaryCards/TeamTitleText").GetComponent<TextMeshProUGUI>();
        powerText = teamsObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        choseTeam = teamsObject.transform.Find("DictionaryCards/ChoseTeam");
        Button nextButton = teamsObject.transform.Find("DictionaryCards/NextButton").GetComponent<Button>();
        Button previousButton = teamsObject.transform.Find("DictionaryCards/PreviousButton").GetComponent<Button>();
        Text pageText = teamsObject.transform.Find("Pagination/Page").GetComponent<Text>();

        int team = 1;
        team_limit = 10;
        team_offset = 0;
        int page = 1;
        team_id = 1;
        string type = "CardHeroes";
        User user = new User();
        user = user.GetUserById(User.CurrentUserId);
        CardHeroes cardHeroes = new CardHeroes();
        List<CardHeroes> cardHeroesList = cardHeroes.GetUserCardHeroes("Adamas", team_limit, team_offset);
        // Gọi script quản lý cuộn
        ScrollManager scrollManager = teamsObject.AddComponent<ScrollManager>();
        scrollManager.Initialize(scrollRect, arrowUp, arrowDown);
        // Thêm sự kiện OnScroll vào ScrollRect
        scrollRect.onValueChanged.AddListener((Vector2 position) =>
        {
            scrollManager.UpdateArrows(); // Cập nhật mũi tên khi cuộn
        });
        CreateButton(1, "Card Heroes", tempLeftContent);
        CreateButton(2, "Card Captains", tempLeftContent);
        CreateButton(3, "Card Colonels", tempLeftContent);
        CreateButton(4, "Card Generals", tempLeftContent);
        CreateButton(5, "Card Admirals", tempLeftContent);
        CreateButton(6, "Card Monsters", tempLeftContent);
        CreateButton(7, "Card Military", tempLeftContent);
        CreateButton(8, "Card Spell", tempLeftContent);
        GetTeamsType(type, dropdownType, choseTeam, pageText, team_limit, newOffset =>
        {
            team_offset = newOffset;
        }, newCurrentPage =>
        {
            page = newCurrentPage;
        });
        typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
        List<object> cardObjects = cardHeroesList.Cast<object>().ToList();
        CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);
        createCardTeams(cardObjects, choseTeam);
        selectedOptionName = dropdownType.options[dropdownType.value].text;
        int totalRecord = cardHeroes.GetUserCardHeroesCount(selectedOptionName);
        totalPage = CalculateTotalPages(totalRecord, team_limit);
        pageText.text = page.ToString() + "/" + totalPage.ToString();

        AssignButtonEvent("Button_1", tempLeftContent, () =>
        {
            GetTeamsType("CardHeroes", dropdownType, choseTeam, pageText, team_limit, newOffset =>
            {
                team_offset = newOffset;
            }, newCurrentPage =>
            {
                page = newCurrentPage;
            });
            type = "CardHeroes";
            typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            CardHeroes c = new CardHeroes();
            CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);

            selectedOptionName = dropdownType.options[dropdownType.value].text;
            List<CardHeroes> cardHeroesList2 = c.GetUserCardHeroes(selectedOptionName, team_limit, team_offset);
            cardObjects = cardHeroesList2.Cast<object>().ToList();
            createCardTeams(cardObjects, choseTeam);

            totalRecord = c.GetUserCardHeroesCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
            pageText.text = page.ToString() + "/" + totalPage.ToString();
        });
        AssignButtonEvent("Button_2", tempLeftContent, () =>
        {
            GetTeamsType("CardCaptains", dropdownType, choseTeam, pageText, team_limit, newOffset =>
            {
                team_offset = newOffset;
            }, newCurrentPage =>
            {
                page = newCurrentPage;
            });
            type = "CardCaptains";
            typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            CardCaptains c = new CardCaptains();
            CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);

            selectedOptionName = dropdownType.options[dropdownType.value].text;
            List<CardCaptains> cardHeroesList2 = c.GetUserCardCaptains(selectedOptionName, team_limit, team_offset);
            cardObjects = cardHeroesList2.Cast<object>().ToList();
            createCardTeams(cardObjects, choseTeam);

            totalRecord = c.GetUserCardCaptainsCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
            pageText.text = page.ToString() + "/" + totalPage.ToString();
        });
        AssignButtonEvent("Button_3", tempLeftContent, () =>
        {
            GetTeamsType("CardColonels", dropdownType, choseTeam, pageText, team_limit, newOffset =>
            {
                team_offset = newOffset;
            }, newCurrentPage =>
            {
                page = newCurrentPage;
            });
            type = "CardColonels";
            typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            CardColonels c = new CardColonels();
            CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);

            selectedOptionName = dropdownType.options[dropdownType.value].text;
            List<CardColonels> cardHeroesList2 = c.GetUserCardColonels(selectedOptionName, team_limit, team_offset);
            cardObjects = cardHeroesList2.Cast<object>().ToList();
            createCardTeams(cardObjects, choseTeam);

            totalRecord = c.GetUserCardColonelsCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
            pageText.text = page.ToString() + "/" + totalPage.ToString();
        });
        AssignButtonEvent("Button_4", tempLeftContent, () =>
        {
            GetTeamsType("CardGenerals", dropdownType, choseTeam, pageText, team_limit, newOffset =>
            {
                team_offset = newOffset;
            }, newCurrentPage =>
            {
                page = newCurrentPage;
            });
            type = "CardGenerals";
            typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            CardGenerals c = new CardGenerals();
            CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);

            selectedOptionName = dropdownType.options[dropdownType.value].text;
            List<CardGenerals> cardHeroesList2 = c.GetUserCardGenerals(selectedOptionName, team_limit, team_offset);
            cardObjects = cardHeroesList2.Cast<object>().ToList();
            createCardTeams(cardObjects, choseTeam);

            totalRecord = c.GetUserCardGeneralsCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
            pageText.text = page.ToString() + "/" + totalPage.ToString();
        });
        AssignButtonEvent("Button_5", tempLeftContent, () =>
        {
            GetTeamsType("CardAdmirals", dropdownType, choseTeam, pageText, team_limit, newOffset =>
            {
                team_offset = newOffset;
            }, newCurrentPage =>
            {
                page = newCurrentPage;
            });
            type = "CardAdmirals";
            typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            CardAdmirals c = new CardAdmirals();
            CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);

            selectedOptionName = dropdownType.options[dropdownType.value].text;
            List<CardAdmirals> cardHeroesList2 = c.GetUserCardAdmirals(selectedOptionName, team_limit, team_offset);
            cardObjects = cardHeroesList2.Cast<object>().ToList();
            createCardTeams(cardObjects, choseTeam);

            totalRecord = c.GetUserCardAdmiralsCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
            pageText.text = page.ToString() + "/" + totalPage.ToString();
        });
        AssignButtonEvent("Button_6", tempLeftContent, () =>
        {
            GetTeamsType("CardMonsters", dropdownType, choseTeam, pageText, team_limit, newOffset =>
            {
                team_offset = newOffset;
            }, newCurrentPage =>
            {
                page = newCurrentPage;
            });
            type = "CardMonsters";
            typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            CardMonsters c = new CardMonsters();
            CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);

            // selectedOptionName = dropdownType.options[dropdownType.value].text;
            List<CardMonsters> cardHeroesList2 = c.GetUserCardMonsters(team_limit, team_offset);
            cardObjects = cardHeroesList2.Cast<object>().ToList();
            createCardTeams(cardObjects, choseTeam);

            totalRecord = c.GetUserCardMonstersCount();
            totalPage = CalculateTotalPages(totalRecord, team_limit);
            pageText.text = page.ToString() + "/" + totalPage.ToString();
        });
        AssignButtonEvent("Button_7", tempLeftContent, () =>
        {
            GetTeamsType("CardMilitary", dropdownType, choseTeam, pageText, team_limit, newOffset =>
            {
                team_offset = newOffset;
            }, newCurrentPage =>
            {
                page = newCurrentPage;
            });
            type = "CardMilitary";
            typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            CardMilitary c = new CardMilitary();
            CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);

            selectedOptionName = dropdownType.options[dropdownType.value].text;
            List<CardMilitary> cardHeroesList2 = c.GetUserCardMilitary(selectedOptionName, team_limit, team_offset);
            cardObjects = cardHeroesList2.Cast<object>().ToList();
            createCardTeams(cardObjects, choseTeam);

            totalRecord = c.GetUserCardMilitaryCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
            pageText.text = page.ToString() + "/" + totalPage.ToString();
        });
        AssignButtonEvent("Button_8", tempLeftContent, () =>
        {
            GetTeamsType("CardSpell", dropdownType, choseTeam, pageText, team_limit, newOffset =>
            {
                team_offset = newOffset;
            }, newCurrentPage =>
            {
                page = newCurrentPage;
            });
            type = "CardSpell";
            typeText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
            CardSpell c = new CardSpell();
            CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);

            selectedOptionName = dropdownType.options[dropdownType.value].text;
            List<CardSpell> cardHeroesList2 = c.GetUserCardSpell(selectedOptionName, team_limit, team_offset);
            cardObjects = cardHeroesList2.Cast<object>().ToList();
            createCardTeams(cardObjects, choseTeam);

            totalRecord = c.GetUserCardSpellCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
            pageText.text = page.ToString() + "/" + totalPage.ToString();
        });

        Teams teams = new Teams();
        List<Teams> teamList = teams.GetUserTeams();
        foreach (Teams t in teamList)
        {
            GameObject button = Instantiate(buttonPrefab, tempRightContent);
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = "Team " + t.team_id;
            ChangeButtonBackground(button, "Background_V4_167");
            if (t.team_id == 1)
            {
                ChangeButtonBackground(button, "Background_V4_166");
            }
            int teamIndex = t.team_id;
            Button btn = button.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                team = teamIndex;
                teamTitleText.text = "Team " + teamIndex;
                team_id = teamIndex;
                GetTeamsButton(button, tempRightContent);
                CreatePosition(type, team, positionPanel, typeContent, user.level, teamsObject);
                UpdateTeamForAllCards(teamIndex);
            });
        }
        nextButton.onClick.AddListener(() =>
        {
            if (page < totalPage)
            {
                team_offset = team_offset + team_limit;
                page = page + 1;
                LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                pageText.text = page.ToString() + "/" + totalPage.ToString();
            }
        });
        previousButton.onClick.AddListener(() =>
        {
            if (page > 1)
            {
                team_offset = team_offset - team_limit;
                page = page - 1;
                LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                pageText.text = page.ToString() + "/" + totalPage.ToString();
            }
        });
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
                CardHeroes cardsManager = new CardHeroes();
                totalRecord = cardsManager.GetUserCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cards = cardsManager.GetUserCardHeroes(subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                Books booksManager = new Books();
                totalRecord = booksManager.GetUserBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = booksManager.GetUserBooks(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                CardCaptains captainsManager = new CardCaptains();
                totalRecord = captainsManager.GetUserCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> army = captainsManager.GetUserCardCaptains(subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                totalRecord = collaborationEquipmentManager.GetUserCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetUserCollaborationEquipments(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaboration"))
            {
                Collaboration collaborationManager = new Collaboration();
                totalRecord = collaborationManager.GetUserCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaboration = collaborationManager.GetUserCollaboration(pageSize, offset);
                createCollaboration(collaboration);
            }
            else if (mainType.Equals("Equipments"))
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetEquipments(subType, pageSize, offset);
                createEquipments(equipments);
            }
            else if (mainType.Equals("Medals"))
            {
                Medals medalsManager = new Medals();
                totalRecord = medalsManager.GetUserMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = medalsManager.GetUserMedals(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                totalRecord = monstersManager.GetUserCardMonstersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> monstersList = monstersManager.GetUserCardMonsters(pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                Pets petsManager = new Pets();
                totalRecord = petsManager.GetUserPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> petsList = petsManager.GetUserPets(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                Skills skillsManager = new Skills();
                totalRecord = skillsManager.GetUserSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skillsList = skillsManager.GetUserSkills(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                Symbols symbolsManager = new Symbols();
                totalRecord = symbolsManager.GetUserSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbolsList = symbolsManager.GetUserSymbols(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                Titles symbolsManager = new Titles();
                totalRecord = symbolsManager.GetUserTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titlesList = symbolsManager.GetUserTitles(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                CardMilitary militaryManager = new CardMilitary();
                totalRecord = militaryManager.GetUserCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> militaryList = militaryManager.GetUserCardMilitary(subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                CardSpell spellManager = new CardSpell();
                totalRecord = spellManager.GetUserCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> spellList = spellManager.GetUserCardSpell(subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                totalRecord = magicFormationCircleManager.GetUserMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetUserMagicFormationCircle(subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                Relics relicsManager = new Relics();
                totalRecord = relicsManager.GetUserRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relicsList = relicsManager.GetUserRelics(subType, pageSize, offset);
                createRelics(relicsList);
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
                CardHeroes cardsManager = new CardHeroes();
                totalRecord = cardsManager.GetUserCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cards = cardsManager.GetUserCardHeroes(subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                Books booksManager = new Books();
                totalRecord = booksManager.GetUserBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = booksManager.GetUserBooks(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                CardCaptains captainsManager = new CardCaptains();
                totalRecord = captainsManager.GetUserCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> army = captainsManager.GetUserCardCaptains(subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                totalRecord = collaborationEquipmentManager.GetUserCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetUserCollaborationEquipments(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaboration"))
            {
                Collaboration collaborationManager = new Collaboration();
                totalRecord = collaborationManager.GetUserCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaboration = collaborationManager.GetUserCollaboration(pageSize, offset);
                createCollaboration(collaboration);
            }
            else if (mainType.Equals("Equipments"))
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetEquipments(subType, pageSize, offset);
                createEquipments(equipments);
            }
            else if (mainType.Equals("Medals"))
            {
                Medals medalsManager = new Medals();
                totalRecord = medalsManager.GetUserMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = medalsManager.GetUserMedals(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                totalRecord = monstersManager.GetUserCardMonstersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monstersList = monstersManager.GetUserCardMonsters(pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                Pets petsManager = new Pets();
                totalRecord = petsManager.GetUserPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> petsList = petsManager.GetUserPets(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                Skills skillsManager = new Skills();
                totalRecord = skillsManager.GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skillsList = skillsManager.GetUserSkills(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                Symbols symbolsManager = new Symbols();
                totalRecord = symbolsManager.GetUserSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbolsList = symbolsManager.GetUserSymbols(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                Titles symbolsManager = new Titles();
                totalRecord = symbolsManager.GetUserTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titlesList = symbolsManager.GetUserTitles(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                CardMilitary militaryManager = new CardMilitary();
                totalRecord = militaryManager.GetUserCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> militaryList = militaryManager.GetUserCardMilitary(subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                CardSpell spellManager = new CardSpell();
                totalRecord = spellManager.GetUserCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> spellList = spellManager.GetUserCardSpell(subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                totalRecord = magicFormationCircleManager.GetUserMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetUserMagicFormationCircle(subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                Relics relicsManager = new Relics();
                totalRecord = relicsManager.GetUserRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relicsList = relicsManager.GetUserRelics(subType, pageSize, offset);
                createRelics(relicsList);
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
    public void GetTeamsType(string type, TMP_Dropdown dropdownType, Transform panel, Text pageText, int team_limit, Action<int> onOffsetUpdated, Action<int> onCurrentPageUpdated)
    {
        List<string> uniqueTypes = GetUniqueTypes(type);
        // Xóa các callback cũ của dropdown
        dropdownType.onValueChanged.RemoveAllListeners();
        DropdownManager.PopulateDropdown(dropdownType, uniqueTypes, index =>
    {
        selectedOptionName = dropdownType.options[index].text;
        int team_offset = 0;
        int page = 1;

        if (type.Equals("CardHeroes"))
        {
            CardHeroes cardHeroes = new CardHeroes();
            List<CardHeroes> cardHeroesList = cardHeroes.GetUserCardHeroes(selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardHeroesList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = cardHeroes.GetUserCardHeroesCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardCaptains"))
        {
            CardCaptains cardCaptains = new CardCaptains();
            List<CardCaptains> cardCaptainsList = cardCaptains.GetUserCardCaptains(selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardCaptainsList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = cardCaptains.GetUserCardCaptainsCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardColonels"))
        {
            CardColonels cardColonels = new CardColonels();
            List<CardColonels> cardColonelsList = cardColonels.GetUserCardColonels(selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardColonelsList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = cardColonels.GetUserCardColonelsCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardGenerals"))
        {
            CardGenerals cardGenerals = new CardGenerals();
            List<CardGenerals> cardGeneralsList = cardGenerals.GetUserCardGenerals(selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardGeneralsList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = cardGenerals.GetUserCardGeneralsCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardAdmirals"))
        {
            CardAdmirals cardAdmirals = new CardAdmirals();
            List<CardAdmirals> cardAdmiralsList = cardAdmirals.GetUserCardAdmirals(selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardAdmiralsList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = cardAdmirals.GetUserCardAdmiralsCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardMonsters"))
        {
            CardMonsters cardMonsters = new CardMonsters();
            List<CardMonsters> cardMonstersList = cardMonsters.GetUserCardMonsters(team_limit, team_offset);
            List<object> cardObjects = cardMonstersList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = cardMonsters.GetUserCardMonstersCount();
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardMilitary"))
        {
            CardMilitary cardMilitary = new CardMilitary();
            List<CardMilitary> cardMilitaryList = cardMilitary.GetUserCardMilitary(selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardMilitaryList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = cardMilitary.GetUserCardMilitaryCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardSpell"))
        {
            CardSpell cardSpell = new CardSpell();
            List<CardSpell> cardSpellList = cardSpell.GetUserCardSpell(selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardSpellList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = cardSpell.GetUserCardSpellCount(selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        onOffsetUpdated?.Invoke(team_offset);
        onCurrentPageUpdated?.Invoke(page);
        pageText.text = currentPage.ToString() + "/" + totalPage.ToString();
    });
    }
    public void GetTeamsButton(GameObject clickedButton, Transform panel)
    {
        foreach (Transform child in panel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ChangeButtonBackground(button.gameObject, "Background_V4_167");
            }
        }
        ChangeButtonBackground(clickedButton, "Background_V4_166");
    }
    private void LoadCardDataByType(string type, string selectedOptionName, int team_limit, int team_offset, Transform choseTeam)
    {
        List<object> cardObjects = null;

        switch (type)
        {
            case "CardHeroes":
                CardHeroes cardHeroes = new CardHeroes();
                List<CardHeroes> cardHeroesList = cardHeroes.GetUserCardHeroes(selectedOptionName, team_limit, team_offset);
                cardObjects = cardHeroesList.Cast<object>().ToList();
                break;

            case "CardCaptains":
                CardCaptains cardCaptains = new CardCaptains();
                List<CardCaptains> cardCaptainsList = cardCaptains.GetUserCardCaptains(selectedOptionName, team_limit, team_offset);
                cardObjects = cardCaptainsList.Cast<object>().ToList();
                break;

            case "CardColonels":
                CardColonels cardColonels = new CardColonels();
                List<CardColonels> cardColonelsList = cardColonels.GetUserCardColonels(selectedOptionName, team_limit, team_offset);
                cardObjects = cardColonelsList.Cast<object>().ToList();
                break;

            case "CardGenerals":
                CardGenerals cardGenerals = new CardGenerals();
                List<CardGenerals> cardGeneralsList = cardGenerals.GetUserCardGenerals(selectedOptionName, team_limit, team_offset);
                cardObjects = cardGeneralsList.Cast<object>().ToList();
                break;

            case "CardAdmirals":
                CardAdmirals cardAdmirals = new CardAdmirals();
                List<CardAdmirals> cardAdmiralsList = cardAdmirals.GetUserCardAdmirals(selectedOptionName, team_limit, team_offset);
                cardObjects = cardAdmiralsList.Cast<object>().ToList();
                break;

            case "CardMonsters":
                CardMonsters cardMonsters = new CardMonsters();
                List<CardMonsters> cardMonstersList = cardMonsters.GetUserCardMonsters(team_limit, team_offset);
                cardObjects = cardMonstersList.Cast<object>().ToList();
                break;

            case "CardMilitary":
                CardMilitary cardMilitary = new CardMilitary();
                List<CardMilitary> cardMilitaryList = cardMilitary.GetUserCardMilitary(selectedOptionName, team_limit, team_offset);
                cardObjects = cardMilitaryList.Cast<object>().ToList();
                break;

            case "CardSpell":
                CardSpell cardSpell = new CardSpell();
                List<CardSpell> cardSpellList = cardSpell.GetUserCardSpell(selectedOptionName, team_limit, team_offset);
                cardObjects = cardSpellList.Cast<object>().ToList();
                break;

            default:
                Debug.LogWarning("Unknown type: " + type);
                break;
        }

        if (cardObjects != null)
        {
            createCardTeams(cardObjects, choseTeam);
        }
    }
    public void CreatePosition(string type, int team, Transform positionPanel, Transform typePanel, int level, GameObject teamsObject)
    {
        int maxLevelForTeam = team * 100;
        int minLevelForTeam = maxLevelForTeam - 99;
        Teams teams = new Teams();
        foreach (Transform child in positionPanel)
        {
            Destroy(child.gameObject); // Hoặc DestroyImmediate(child.gameObject) nếu cần xóa ngay lập tức
        }
        foreach (Transform child in typePanel)
        {
            Destroy(child.gameObject);
        }
        if (type.Equals("CardHeroes"))
        {
            double totalPower = 0;
            CardHeroes c = new CardHeroes();
            List<CardHeroes> cardHeroesList = c.GetUserCardHeroesTeam(team);
            cardHeroesList = cardHeroesList
                .Where(cardHero => cardHero.team_id == team) // Lọc theo team_id
                .ToList();
            var result = c.GetUniqueCardHeroTypesTeam(team);
            foreach (var item in result)
            {
                GameObject typeObject = Instantiate(TypePrefab, typePanel);
                TextMeshProUGUI titleText = typeObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                titleText.text = item.Key;
                TextMeshProUGUI quantityText = typeObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                int total = 0;
                if (item.Value < 2)
                {
                    total = 2;
                }
                else if (item.Value >= 2 && item.Value < 4)
                {
                    total = 4;
                }
                else if (item.Value >= 4 && item.Value < 6)
                {
                    total = 6;
                }
                else if (item.Value >= 6 && item.Value < 8)
                {
                    total = 8;
                }
                else if (item.Value >= 8 && item.Value < 10)
                {
                    total = 10;
                }
                else if (item.Value >= 10 && item.Value < 20)
                {
                    total = 20;
                }
                else if (item.Value >= 20 && item.Value < 50)
                {
                    total = 50;
                }
                else if (item.Value >= 50 && item.Value < 100)
                {
                    total = 100;
                }
                quantityText.text = item.Value.ToString() + "/" + total.ToString();
            }
            int iterations = Mathf.Clamp(level - minLevelForTeam + 1, 0, 100);
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(true);
                PositionBackground.gameObject.SetActive(true);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardHeroes matchingCardHero = cardHeroesList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string numberPart = new string(cardHero.position.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numberPart, out int positionIndex))
                    {
                        return positionIndex - 1 == i; // Kiểm tra nếu vị trí tương ứng
                    }
                    return false;
                });
                string positionText = "";
                if (matchingCardHero != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardHero.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    positionText = matchingCardHero.position;
                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        buttonText.text = "Front";
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        buttonText.text = "Back";
                    }
                    count = count + 1;
                    totalPower = totalPower + matchingCardHero.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                positionButton.onClick.AddListener(() =>
                {
                    CardHeroes cardHeroes = new CardHeroes();

                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        positionText = "B" + positionText.Substring(1); // Đổi 'F' thành 'B'
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        positionText = "F" + positionText.Substring(1); // Đổi 'B' thành 'F'
                    }

                    if (buttonText.text == "Back")
                    {
                        buttonText.text = "Front";
                    }
                    else if (buttonText.text == "Front")
                    {
                        buttonText.text = "Back";
                    }
                    cardHeroes.UpdateTeamFactCardHeroes(team, positionText, matchingCardHero.id);

                });

                leaveButton.onClick.AddListener(() =>
                {
                    CardHeroes cardHeroes = new CardHeroes();
                    image.texture = null;
                    double currentPower = teams.GetTeamsPower();
                    cardHeroes.UpdateTeamFactCardHeroes(null, null, matchingCardHero.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardHero.all_power, 0);
                    CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    dropHandler.position_id = i + 1;
                    if (matchingCardHero != null)
                    {
                        dropHandler.card_id = matchingCardHero.id;
                        dropHandler.card_all_power = matchingCardHero.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    };
                }
            }
            teamMembersText.text = count.ToString() + "/" + iterations;
            powerText.text = totalPower.ToString();
        }
        else if (type.Equals("CardCaptains"))
        {
            double totalPower = 0;
            CardCaptains c = new CardCaptains();
            List<CardCaptains> cardCaptainsList = c.GetUserCardCaptainsTeam(team);
            cardCaptainsList = cardCaptainsList
                .Where(cardHero => cardHero.team_id == team) // Lọc theo team_id
                .ToList();
            var result = c.GetUniqueCardCaptainTypesTeam(team);
            foreach (var item in result)
            {
                GameObject typeObject = Instantiate(TypePrefab, typePanel);
                TextMeshProUGUI titleText = typeObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                titleText.text = item.Key;
                TextMeshProUGUI quantityText = typeObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                int total = 0;
                if (item.Value < 2)
                {
                    total = 2;
                }
                else if (item.Value >= 2 && item.Value < 4)
                {
                    total = 4;
                }
                else if (item.Value >= 4 && item.Value < 6)
                {
                    total = 6;
                }
                else if (item.Value >= 6 && item.Value < 8)
                {
                    total = 8;
                }
                else if (item.Value >= 8 && item.Value < 10)
                {
                    total = 10;
                }
                else if (item.Value >= 10 && item.Value < 20)
                {
                    total = 20;
                }
                else if (item.Value >= 20 && item.Value < 50)
                {
                    total = 50;
                }
                else if (item.Value >= 50 && item.Value < 100)
                {
                    total = 100;
                }
                quantityText.text = item.Value.ToString() + "/" + total.ToString();
            }
            int iterations = Mathf.Clamp(Mathf.CeilToInt((float)(level - minLevelForTeam + 1) / 5), 0, 20);
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(true);
                PositionBackground.gameObject.SetActive(true);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardCaptains matchingCardCaptain = cardCaptainsList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string numberPart = new string(cardHero.position.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numberPart, out int positionIndex))
                    {
                        return positionIndex - 1 == i; // Kiểm tra nếu vị trí tương ứng
                    }
                    return false;
                });
                string positionText = "";
                if (matchingCardCaptain != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardCaptain.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    positionText = matchingCardCaptain.position;
                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        buttonText.text = "Front";
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        buttonText.text = "Back";
                    }
                    count = count + 1;
                    totalPower = totalPower + matchingCardCaptain.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                positionButton.onClick.AddListener(() =>
                {
                    CardCaptains cardCaptains = new CardCaptains();

                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        positionText = "B" + positionText.Substring(1); // Đổi 'F' thành 'B'
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        positionText = "F" + positionText.Substring(1); // Đổi 'B' thành 'F'
                    }

                    if (buttonText.text == "Back")
                    {
                        buttonText.text = "Front";
                    }
                    else if (buttonText.text == "Front")
                    {
                        buttonText.text = "Back";
                    }
                    cardCaptains.UpdateTeamFactCardCaptains(team, positionText, matchingCardCaptain.id);

                });

                leaveButton.onClick.AddListener(() =>
                {
                    CardCaptains cardCaptains = new CardCaptains();
                    image.texture = null;
                    double currentPower = teams.GetTeamsPower();
                    cardCaptains.UpdateTeamFactCardCaptains(null, null, matchingCardCaptain.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardCaptain.all_power, 0);
                    CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    dropHandler.position_id = i + 1;
                    if (matchingCardCaptain != null)
                    {
                        dropHandler.card_id = matchingCardCaptain.id;
                        dropHandler.card_all_power = matchingCardCaptain.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    };
                }
            }
            teamMembersText.text = count.ToString() + "/" + iterations;
            powerText.text = totalPower.ToString();
        }
        else if (type.Equals("CardColonels"))
        {
            double totalPower = 0;
            CardColonels c = new CardColonels();
            List<CardColonels> cardColonelsList = c.GetUserCardColonelsTeam(team);
            cardColonelsList = cardColonelsList
                .Where(cardHero => cardHero.team_id == team) // Lọc theo team_id
                .ToList();
            var result = c.GetUniqueCardColonelTypesTeam(team);
            foreach (var item in result)
            {
                GameObject typeObject = Instantiate(TypePrefab, typePanel);
                TextMeshProUGUI titleText = typeObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                titleText.text = item.Key;
                TextMeshProUGUI quantityText = typeObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                int total = 0;
                if (item.Value < 2)
                {
                    total = 2;
                }
                else if (item.Value >= 2 && item.Value < 4)
                {
                    total = 4;
                }
                else if (item.Value >= 4 && item.Value < 6)
                {
                    total = 6;
                }
                else if (item.Value >= 6 && item.Value < 8)
                {
                    total = 8;
                }
                else if (item.Value >= 8 && item.Value < 10)
                {
                    total = 10;
                }
                else if (item.Value >= 10 && item.Value < 20)
                {
                    total = 20;
                }
                else if (item.Value >= 20 && item.Value < 50)
                {
                    total = 50;
                }
                else if (item.Value >= 50 && item.Value < 100)
                {
                    total = 100;
                }
                quantityText.text = item.Value.ToString() + "/" + total.ToString();
            }
            int iterations = Mathf.Clamp(Mathf.CeilToInt((float)(level - minLevelForTeam + 1) / 5), 0, 20);
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(true);
                PositionBackground.gameObject.SetActive(true);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardColonels matchingCardColonel = cardColonelsList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string numberPart = new string(cardHero.position.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numberPart, out int positionIndex))
                    {
                        return positionIndex - 1 == i; // Kiểm tra nếu vị trí tương ứng
                    }
                    return false;
                });
                string positionText = "";
                if (matchingCardColonel != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardColonel.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    positionText = matchingCardColonel.position;
                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        buttonText.text = "Front";
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        buttonText.text = "Back";
                    }
                    count = count + 1;
                    totalPower = totalPower + matchingCardColonel.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                positionButton.onClick.AddListener(() =>
                {
                    CardColonels cardColonels = new CardColonels();

                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        positionText = "B" + positionText.Substring(1); // Đổi 'F' thành 'B'
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        positionText = "F" + positionText.Substring(1); // Đổi 'B' thành 'F'
                    }

                    if (buttonText.text == "Back")
                    {
                        buttonText.text = "Front";
                    }
                    else if (buttonText.text == "Front")
                    {
                        buttonText.text = "Back";
                    }
                    cardColonels.UpdateTeamFactCardColonels(team, positionText, matchingCardColonel.id);

                });

                leaveButton.onClick.AddListener(() =>
                {
                    CardColonels cardColonels = new CardColonels();
                    image.texture = null;
                    double currentPower = teams.GetTeamsPower();
                    cardColonels.UpdateTeamFactCardColonels(null, null, matchingCardColonel.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardColonel.all_power, 0);
                    CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    dropHandler.position_id = i + 1;
                    if (matchingCardColonel != null)
                    {
                        dropHandler.card_id = matchingCardColonel.id;
                        dropHandler.card_all_power = matchingCardColonel.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    };
                }
            }
            teamMembersText.text = count.ToString() + "/" + iterations;
            powerText.text = totalPower.ToString();
        }
        else if (type.Equals("CardGenerals"))
        {
            double totalPower = 0;
            CardGenerals c = new CardGenerals();
            List<CardGenerals> cardGeneralsList = c.GetUserCardGeneralsTeam(team);
            cardGeneralsList = cardGeneralsList
                .Where(cardHero => cardHero.team_id == team) // Lọc theo team_id
                .ToList();
            var result = c.GetUniqueCardGeneralTypesTeam(team);
            foreach (var item in result)
            {
                GameObject typeObject = Instantiate(TypePrefab, typePanel);
                TextMeshProUGUI titleText = typeObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                titleText.text = item.Key;
                TextMeshProUGUI quantityText = typeObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                int total = 0;
                if (item.Value < 2)
                {
                    total = 2;
                }
                else if (item.Value >= 2 && item.Value < 4)
                {
                    total = 4;
                }
                else if (item.Value >= 4 && item.Value < 6)
                {
                    total = 6;
                }
                else if (item.Value >= 6 && item.Value < 8)
                {
                    total = 8;
                }
                else if (item.Value >= 8 && item.Value < 10)
                {
                    total = 10;
                }
                else if (item.Value >= 10 && item.Value < 20)
                {
                    total = 20;
                }
                else if (item.Value >= 20 && item.Value < 50)
                {
                    total = 50;
                }
                else if (item.Value >= 50 && item.Value < 100)
                {
                    total = 100;
                }
                quantityText.text = item.Value.ToString() + "/" + total.ToString();
            }
            int iterations = Mathf.Clamp(Mathf.CeilToInt((float)(level - minLevelForTeam + 1) / 5), 0, 20);
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(true);
                PositionBackground.gameObject.SetActive(true);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardGenerals matchingCardGeneral = cardGeneralsList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string numberPart = new string(cardHero.position.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numberPart, out int positionIndex))
                    {
                        return positionIndex - 1 == i; // Kiểm tra nếu vị trí tương ứng
                    }
                    return false;
                });
                string positionText = "";
                if (matchingCardGeneral != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardGeneral.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    positionText = matchingCardGeneral.position;
                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        buttonText.text = "Front";
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        buttonText.text = "Back";
                    }
                    count = count + 1;
                    totalPower = totalPower + matchingCardGeneral.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                positionButton.onClick.AddListener(() =>
                {
                    CardGenerals cardGenerals = new CardGenerals();

                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        positionText = "B" + positionText.Substring(1); // Đổi 'F' thành 'B'
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        positionText = "F" + positionText.Substring(1); // Đổi 'B' thành 'F'
                    }

                    if (buttonText.text == "Back")
                    {
                        buttonText.text = "Front";
                    }
                    else if (buttonText.text == "Front")
                    {
                        buttonText.text = "Back";
                    }
                    cardGenerals.UpdateTeamFactCardGenerals(team, positionText, matchingCardGeneral.id);

                });

                leaveButton.onClick.AddListener(() =>
                {
                    CardGenerals cardGenerals = new CardGenerals();
                    image.texture = null;
                    double currentPower = teams.GetTeamsPower();
                    cardGenerals.UpdateTeamFactCardGenerals(null, null, matchingCardGeneral.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardGeneral.all_power, 0);
                    CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    dropHandler.position_id = i + 1;
                    if (matchingCardGeneral != null)
                    {
                        dropHandler.card_id = matchingCardGeneral.id;
                        dropHandler.card_all_power = matchingCardGeneral.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    };
                }
            }
            teamMembersText.text = count.ToString() + "/" + iterations;
            powerText.text = totalPower.ToString();
        }
        else if (type.Equals("CardAdmirals"))
        {
            double totalPower = 0;
            CardAdmirals c = new CardAdmirals();
            List<CardAdmirals> cardAdmiralsList = c.GetUserCardAdmiralsTeam(team);
            cardAdmiralsList = cardAdmiralsList
                .Where(cardHero => cardHero.team_id == team) // Lọc theo team_id
                .ToList();
            var result = c.GetUniqueCardAdmiralTypesTeam(team);
            foreach (var item in result)
            {
                GameObject typeObject = Instantiate(TypePrefab, typePanel);
                TextMeshProUGUI titleText = typeObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                titleText.text = item.Key;
                TextMeshProUGUI quantityText = typeObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                int total = 0;
                if (item.Value < 2)
                {
                    total = 2;
                }
                else if (item.Value >= 2 && item.Value < 4)
                {
                    total = 4;
                }
                else if (item.Value >= 4 && item.Value < 6)
                {
                    total = 6;
                }
                else if (item.Value >= 6 && item.Value < 8)
                {
                    total = 8;
                }
                else if (item.Value >= 8 && item.Value < 10)
                {
                    total = 10;
                }
                else if (item.Value >= 10 && item.Value < 20)
                {
                    total = 20;
                }
                else if (item.Value >= 20 && item.Value < 50)
                {
                    total = 50;
                }
                else if (item.Value >= 50 && item.Value < 100)
                {
                    total = 100;
                }
                quantityText.text = item.Value.ToString() + "/" + total.ToString();
            }
            int iterations = Mathf.Clamp(Mathf.CeilToInt((float)(level - minLevelForTeam + 1) / 5), 0, 20);
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(true);
                PositionBackground.gameObject.SetActive(true);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardAdmirals matchingCardAdmiral = cardAdmiralsList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string numberPart = new string(cardHero.position.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numberPart, out int positionIndex))
                    {
                        return positionIndex - 1 == i; // Kiểm tra nếu vị trí tương ứng
                    }
                    return false;
                });
                string positionText = "";
                if (matchingCardAdmiral != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardAdmiral.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    positionText = matchingCardAdmiral.position;
                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        buttonText.text = "Front";
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        buttonText.text = "Back";
                    }
                    count = count + 1;
                    totalPower = totalPower + matchingCardAdmiral.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                positionButton.onClick.AddListener(() =>
                {
                    CardAdmirals cardAdmirals = new CardAdmirals();

                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        positionText = "B" + positionText.Substring(1); // Đổi 'F' thành 'B'
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        positionText = "F" + positionText.Substring(1); // Đổi 'B' thành 'F'
                    }

                    if (buttonText.text == "Back")
                    {
                        buttonText.text = "Front";
                    }
                    else if (buttonText.text == "Front")
                    {
                        buttonText.text = "Back";
                    }
                    cardAdmirals.UpdateTeamFactCardAdmirals(team, positionText, matchingCardAdmiral.id);

                });

                leaveButton.onClick.AddListener(() =>
                {
                    CardAdmirals cardAdmirals = new CardAdmirals();
                    image.texture = null;
                    double currentPower = teams.GetTeamsPower();
                    cardAdmirals.UpdateTeamFactCardAdmirals(null, null, matchingCardAdmiral.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardAdmiral.all_power, 0);
                    CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    dropHandler.position_id = i + 1;
                    if (matchingCardAdmiral != null)
                    {
                        dropHandler.card_id = matchingCardAdmiral.id;
                        dropHandler.card_all_power = matchingCardAdmiral.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    };
                }
            }
            teamMembersText.text = count.ToString() + "/" + iterations;
            powerText.text = totalPower.ToString();
        }
        else if (type.Equals("CardMonsters"))
        {
            double totalPower = 0;
            CardMonsters c = new CardMonsters();
            List<CardMonsters> cardMonstersList = c.GetUserCardMonstersTeam(team);
            cardMonstersList = cardMonstersList
                .Where(cardHero => cardHero.team_id == team) // Lọc theo team_id
                .ToList();
            var result = c.GetUniqueCardMonsterTypesTeam(team);
            foreach (var item in result)
            {
                GameObject typeObject = Instantiate(TypePrefab, typePanel);
                TextMeshProUGUI titleText = typeObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                titleText.text = item.Key;
                TextMeshProUGUI quantityText = typeObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                int total = 0;
                if (item.Value < 2)
                {
                    total = 2;
                }
                else if (item.Value >= 2 && item.Value < 4)
                {
                    total = 4;
                }
                else if (item.Value >= 4 && item.Value < 6)
                {
                    total = 6;
                }
                else if (item.Value >= 6 && item.Value < 8)
                {
                    total = 8;
                }
                else if (item.Value >= 8 && item.Value < 10)
                {
                    total = 10;
                }
                else if (item.Value >= 10 && item.Value < 20)
                {
                    total = 20;
                }
                else if (item.Value >= 20 && item.Value < 50)
                {
                    total = 50;
                }
                else if (item.Value >= 50 && item.Value < 100)
                {
                    total = 100;
                }
                quantityText.text = item.Value.ToString() + "/" + total.ToString();
            }
            int iterations = Mathf.Clamp(Mathf.CeilToInt((float)(level - minLevelForTeam + 1) / 2), 0, 50);
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(true);
                PositionBackground.gameObject.SetActive(true);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardMonsters matchingCardMonster = cardMonstersList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string numberPart = new string(cardHero.position.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numberPart, out int positionIndex))
                    {
                        return positionIndex - 1 == i; // Kiểm tra nếu vị trí tương ứng
                    }
                    return false;
                });
                string positionText = "";
                if (matchingCardMonster != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardMonster.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    positionText = matchingCardMonster.position;
                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        buttonText.text = "Front";
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        buttonText.text = "Back";
                    }
                    count = count + 1;
                    totalPower = totalPower + matchingCardMonster.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                positionButton.onClick.AddListener(() =>
                {
                    CardMonsters cardMonsters = new CardMonsters();

                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        positionText = "B" + positionText.Substring(1); // Đổi 'F' thành 'B'
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        positionText = "F" + positionText.Substring(1); // Đổi 'B' thành 'F'
                    }

                    if (buttonText.text == "Back")
                    {
                        buttonText.text = "Front";
                    }
                    else if (buttonText.text == "Front")
                    {
                        buttonText.text = "Back";
                    }
                    cardMonsters.UpdateTeamFactCardMonsters(team, positionText, matchingCardMonster.id);

                });

                leaveButton.onClick.AddListener(() =>
                {
                    CardMonsters cardMonsters = new CardMonsters();
                    image.texture = null;
                    double currentPower = teams.GetTeamsPower();
                    cardMonsters.UpdateTeamFactCardMonsters(null, null, matchingCardMonster.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardMonster.all_power, 0);
                    CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    dropHandler.position_id = i + 1;
                    if (matchingCardMonster != null)
                    {
                        dropHandler.card_id = matchingCardMonster.id;
                        dropHandler.card_all_power = matchingCardMonster.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    };
                }
            }
            teamMembersText.text = count.ToString() + "/" + iterations;
            powerText.text = totalPower.ToString();
        }
        else if (type.Equals("CardMilitary"))
        {
            double totalPower = 0;
            CardMilitary c = new CardMilitary();
            List<CardMilitary> cardMilitaryList = c.GetUserCardMilitaryTeam(team);
            cardMilitaryList = cardMilitaryList
                .Where(cardHero => cardHero.team_id == team) // Lọc theo team_id
                .ToList();
            var result = c.GetUniqueCardMilitaryTypesTeam(team);
            foreach (var item in result)
            {
                GameObject typeObject = Instantiate(TypePrefab, typePanel);
                TextMeshProUGUI titleText = typeObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                titleText.text = item.Key;
                TextMeshProUGUI quantityText = typeObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                int total = 0;
                if (item.Value < 2)
                {
                    total = 2;
                }
                else if (item.Value >= 2 && item.Value < 4)
                {
                    total = 4;
                }
                else if (item.Value >= 4 && item.Value < 6)
                {
                    total = 6;
                }
                else if (item.Value >= 6 && item.Value < 8)
                {
                    total = 8;
                }
                else if (item.Value >= 8 && item.Value < 10)
                {
                    total = 10;
                }
                else if (item.Value >= 10 && item.Value < 20)
                {
                    total = 20;
                }
                else if (item.Value >= 20 && item.Value < 50)
                {
                    total = 50;
                }
                else if (item.Value >= 50 && item.Value < 100)
                {
                    total = 100;
                }
                quantityText.text = item.Value.ToString() + "/" + total.ToString();
            }
            int iterations = Mathf.Clamp(Mathf.CeilToInt((float)(level - minLevelForTeam + 1) / 5), 0, 20);
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(true);
                PositionBackground.gameObject.SetActive(true);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardMilitary matchingCardMilitary = cardMilitaryList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string numberPart = new string(cardHero.position.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numberPart, out int positionIndex))
                    {
                        return positionIndex - 1 == i; // Kiểm tra nếu vị trí tương ứng
                    }
                    return false;
                });
                string positionText = "";
                if (matchingCardMilitary != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardMilitary.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    positionText = matchingCardMilitary.position;
                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        buttonText.text = "Front";
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        buttonText.text = "Back";
                    }
                    count = count + 1;
                    totalPower = totalPower + matchingCardMilitary.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                positionButton.onClick.AddListener(() =>
                {
                    CardMilitary CardMilitary = new CardMilitary();

                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        positionText = "B" + positionText.Substring(1); // Đổi 'F' thành 'B'
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        positionText = "F" + positionText.Substring(1); // Đổi 'B' thành 'F'
                    }

                    if (buttonText.text == "Back")
                    {
                        buttonText.text = "Front";
                    }
                    else if (buttonText.text == "Front")
                    {
                        buttonText.text = "Back";
                    }
                    CardMilitary.UpdateTeamFactCardMilitary(team, positionText, matchingCardMilitary.id);

                });

                leaveButton.onClick.AddListener(() =>
                {
                    CardMilitary cardMilitary = new CardMilitary();
                    image.texture = null;
                    double currentPower = teams.GetTeamsPower();
                    cardMilitary.UpdateTeamFactCardMilitary(null, null, matchingCardMilitary.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardMilitary.all_power, 0);
                    CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    dropHandler.position_id = i + 1;
                    if (matchingCardMilitary != null)
                    {
                        dropHandler.card_id = matchingCardMilitary.id;
                        dropHandler.card_all_power = matchingCardMilitary.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    };
                }
            }
            teamMembersText.text = count.ToString() + "/" + iterations;
            powerText.text = totalPower.ToString();
        }
        else if (type.Equals("CardSpell"))
        {
            double totalPower = 0;
            CardSpell c = new CardSpell();
            List<CardSpell> cardSpellList = c.GetUserCardSpellTeam(team);
            cardSpellList = cardSpellList
                .Where(cardHero => cardHero.team_id == team) // Lọc theo team_id
                .ToList();
            var result = c.GetUniqueCardSpellTypesTeam(team);
            foreach (var item in result)
            {
                GameObject typeObject = Instantiate(TypePrefab, typePanel);
                TextMeshProUGUI titleText = typeObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                titleText.text = item.Key;
                TextMeshProUGUI quantityText = typeObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                int total = 0;
                if (item.Value < 2)
                {
                    total = 2;
                }
                else if (item.Value >= 2 && item.Value < 4)
                {
                    total = 4;
                }
                else if (item.Value >= 4 && item.Value < 6)
                {
                    total = 6;
                }
                else if (item.Value >= 6 && item.Value < 8)
                {
                    total = 8;
                }
                else if (item.Value >= 8 && item.Value < 10)
                {
                    total = 10;
                }
                else if (item.Value >= 10 && item.Value < 20)
                {
                    total = 20;
                }
                else if (item.Value >= 20 && item.Value < 50)
                {
                    total = 50;
                }
                else if (item.Value >= 50 && item.Value < 100)
                {
                    total = 100;
                }
                quantityText.text = item.Value.ToString() + "/" + total.ToString();
            }
            int iterations = Mathf.Clamp(Mathf.CeilToInt((float)(level - minLevelForTeam + 1) / 5), 0, 20);
            int count = 0;
            for (int i = 0; i < iterations; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(true);
                PositionBackground.gameObject.SetActive(true);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardSpell matchingCardSpell = cardSpellList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string numberPart = new string(cardHero.position.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numberPart, out int positionIndex))
                    {
                        return positionIndex - 1 == i; // Kiểm tra nếu vị trí tương ứng
                    }
                    return false;
                });
                string positionText = "";
                if (matchingCardSpell != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardSpell.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    positionText = matchingCardSpell.position;
                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        buttonText.text = "Front";
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        buttonText.text = "Back";
                    }
                    count = count + 1;
                    totalPower = totalPower + matchingCardSpell.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                positionButton.onClick.AddListener(() =>
                {
                    CardSpell cardSpell = new CardSpell();

                    // Kiểm tra và thay đổi ký tự đầu tiên
                    if (positionText.StartsWith("F"))
                    {
                        positionText = "B" + positionText.Substring(1); // Đổi 'F' thành 'B'
                    }
                    else if (positionText.StartsWith("B"))
                    {
                        positionText = "F" + positionText.Substring(1); // Đổi 'B' thành 'F'
                    }

                    if (buttonText.text == "Back")
                    {
                        buttonText.text = "Front";
                    }
                    else if (buttonText.text == "Front")
                    {
                        buttonText.text = "Back";
                    }
                    cardSpell.UpdateTeamFactCardSpell(team, positionText, matchingCardSpell.id);

                });

                leaveButton.onClick.AddListener(() =>
                {
                    CardSpell cardSpell = new CardSpell();
                    image.texture = null;
                    double currentPower = teams.GetTeamsPower();
                    cardSpell.UpdateTeamFactCardSpell(null, null, matchingCardSpell.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardSpell.all_power, 0);
                    CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    LoadCardDataByType(type, selectedOptionName, team_limit, team_offset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    dropHandler.position_id = i + 1;
                    if (matchingCardSpell != null)
                    {
                        dropHandler.card_id = matchingCardSpell.id;
                        dropHandler.card_all_power = matchingCardSpell.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(type, team, positionPanel, typePanel, level, teamsObject);
                    };
                }
            }
            teamMembersText.text = count.ToString() + "/" + iterations;
            powerText.text = totalPower.ToString();
        }
    }
    private void createCardTeams(List<object> obj, Transform panel)
    {
        foreach (Transform child in panel)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in obj)
        {
            if (item is CardHeroes cardHeroes)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardHeroes.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardHeroes.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardHeroes.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardHeroes.team_id != -1)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardHeroes;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardHeroes", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardHeroes", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardCaptains cardCaptains)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardCaptains.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardCaptains.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardCaptains.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardCaptains.team_id != -1)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardCaptains;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardCaptains", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardCaptains", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardColonels cardColonels)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardColonels.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardColonels.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardColonels.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardColonels.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardColonels.team_id != -1)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardColonels;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardColonels", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardColonels", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardGenerals cardGenerals)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardGenerals.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardGenerals.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardGenerals.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardGenerals.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardGenerals.team_id != -1)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardGenerals;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardGenerals", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardGenerals", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardAdmirals cardAdmirals)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardAdmirals.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardAdmirals.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardAdmirals.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardAdmirals.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardAdmirals.team_id != -1)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardAdmirals;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardAdmirals", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardAdmirals", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardMonsters cardMonsters)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardMonsters.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardMonsters.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardMonsters.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMonsters.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardMonsters.team_id != -1)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardMonsters;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardMonsters", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardMonsters", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardMilitary cardMilitary)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardMilitary.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardMilitary.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardMilitary.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMilitary.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardMilitary.team_id != -1)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardMilitary;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardMilitary", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardMilitary", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardSpell cardSpell)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardSpell.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardSpell.power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardSpell.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardSpell.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardSpell.team_id != -1)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardSpell;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardSpell", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardSpell", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
        }
    }
    public void UpdateTeamForAllCards(int newTeamId)
    {
        foreach (var dragHandler in cardDragHandlers)
        {
            dragHandler.team_id = newTeamId;
        }
    }
    public void InsertCardToTeam(object obj, int position_id, int card_id, int team_id, double card_all_power)
    {
        string position = "F" + position_id;
        Teams teams = new Teams();
        if (obj is CardHeroes cardHeroes)
        {
            double currentPower = teams.GetTeamsPower();
            if (card_id != 0)
            {
                cardHeroes.UpdateTeamFactCardHeroes(null, null, card_id);
                cardHeroes.UpdateTeamFactCardHeroes(team_id, position, cardHeroes.id);
                if (cardHeroes.all_power >= card_all_power)
                {
                    double newPower = cardHeroes.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardHeroes.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardHeroes.UpdateTeamFactCardHeroes(team_id, position, cardHeroes.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.all_power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptains)
        {
            double currentPower = teams.GetTeamsPower();
            if (card_id != 0)
            {
                cardCaptains.UpdateTeamFactCardCaptains(null, null, card_id);
                cardCaptains.UpdateTeamFactCardCaptains(team_id, position, cardCaptains.id);
                if (cardCaptains.all_power >= card_all_power)
                {
                    double newPower = cardCaptains.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardCaptains.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardCaptains.UpdateTeamFactCardCaptains(team_id, position, cardCaptains.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.all_power, 1);
            }
        }
        else if (obj is CardColonels cardColonels)
        {
            double currentPower = teams.GetTeamsPower();
            if (card_id != 0)
            {
                cardColonels.UpdateTeamFactCardColonels(null, null, card_id);
                cardColonels.UpdateTeamFactCardColonels(team_id, position, cardColonels.id);
                if (cardColonels.all_power >= card_all_power)
                {
                    double newPower = cardColonels.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardColonels.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardColonels.UpdateTeamFactCardColonels(team_id, position, cardColonels.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.all_power, 1);
            }
        }
        else if (obj is CardGenerals cardGenerals)
        {
            double currentPower = teams.GetTeamsPower();
            if (card_id != 0)
            {
                cardGenerals.UpdateTeamFactCardGenerals(null, null, card_id);
                cardGenerals.UpdateTeamFactCardGenerals(team_id, position, cardGenerals.id);
                if (cardGenerals.all_power >= card_all_power)
                {
                    double newPower = cardGenerals.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardGenerals.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardGenerals.UpdateTeamFactCardGenerals(team_id, position, cardGenerals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.all_power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            double currentPower = teams.GetTeamsPower();
            if (card_id != 0)
            {
                cardAdmirals.UpdateTeamFactCardAdmirals(null, null, card_id);
                cardAdmirals.UpdateTeamFactCardAdmirals(team_id, position, cardAdmirals.id);
                if (cardAdmirals.all_power >= card_all_power)
                {
                    double newPower = cardAdmirals.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardAdmirals.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardAdmirals.UpdateTeamFactCardAdmirals(team_id, position, cardAdmirals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.all_power, 1);
            }
        }
        else if (obj is CardMonsters cardMonsters)
        {
            double currentPower = teams.GetTeamsPower();
            if (card_id != 0)
            {
                cardMonsters.UpdateTeamFactCardMonsters(null, null, card_id);
                cardMonsters.UpdateTeamFactCardMonsters(team_id, position, cardMonsters.id);
                if (cardMonsters.all_power >= card_all_power)
                {
                    double newPower = cardMonsters.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardMonsters.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardMonsters.UpdateTeamFactCardMonsters(team_id, position, cardMonsters.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.all_power, 1);
            }
        }
        else if (obj is CardMilitary cardMilitary)
        {
            double currentPower = teams.GetTeamsPower();
            if (card_id != 0)
            {
                cardMilitary.UpdateTeamFactCardMilitary(null, null, card_id);
                cardMilitary.UpdateTeamFactCardMilitary(team_id, position, cardMilitary.id);
                if (cardMilitary.all_power >= card_all_power)
                {
                    double newPower = cardMilitary.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardMilitary.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardMilitary.UpdateTeamFactCardMilitary(team_id, position, cardMilitary.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.all_power, 1);
            }
        }
        else if (obj is CardSpell cardSpell)
        {
            double currentPower = teams.GetTeamsPower();
            if (card_id != 0)
            {
                cardSpell.UpdateTeamFactCardSpell(null, null, card_id);
                cardSpell.UpdateTeamFactCardSpell(team_id, position, cardSpell.id);
                if (cardSpell.all_power >= card_all_power)
                {
                    double newPower = cardSpell.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardSpell.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardSpell.UpdateTeamFactCardSpell(team_id, position, cardSpell.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.all_power, 1);
            }
        }
    }

}
