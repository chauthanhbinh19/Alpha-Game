using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class MainMenuManagement : MonoBehaviour
{
    private Transform mainMenuPanel;
    private GameObject buttonPrefab;
    private GameObject DictionaryPanel;
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private Transform DictionaryContentPanel;
    private Button CloseButton;
    private Button HomeButton;
    private Button SummonButton;
    private Button Summon10Button;
    private GameObject equipmentsPrefab;
    private Transform TabButtonPanel;
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
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        mainMenuPanel = UIManager.Instance.GetTransform("mainMenuButtonPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        DictionaryPanel = UIManager.Instance.GetGameObject("DictionaryPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        SummonPanel = UIManager.Instance.GetGameObject("SummonPanelPrefab");
        PositionPrefab = UIManager.Instance.GetGameObject("PositionPrefab");
        SummonMainMenuPanel = UIManager.Instance.GetTransform("summonPanel");

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
        AssignButtonEvent("Button_15", mainMenuPanel, () => GetType("MagicFormationCircle"));
        AssignButtonEvent("Button_16", mainMenuPanel, () => GetType("Relics"));
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
        titleText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())); // Cập nhật tiêu đề
    }
    public List<string> GetUniqueTypes()
    {
        if (mainType.Equals("CardHeroes"))
        {
            return CardHeroes.GetUniqueCardHeroTypes();
        }
        else if (mainType.Equals("Books"))
        {
            return Books.GetUniqueBookTypes();
        }
        else if (mainType.Equals("CardCaptains"))
        {
            return CardCaptains.GetUniqueCardCaptainsTypes();
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            return CollaborationEquipment.GetUniqueCollaborationEquipmentTypes();
        }
        else if (mainType.Equals("Equipments"))
        {
            return Equipments.GetUniqueEquipmentsTypes();
        }
        else if (mainType.Equals("Pets"))
        {
            return Pets.GetUniquePetsTypes();
        }
        else if (mainType.Equals("Skills"))
        {
            return Skills.GetUniqueSkillsTypes();
        }
        else if (mainType.Equals("Symbols"))
        {
            return Symbols.GetUniqueSymbolsTypes();
        }
        else if (mainType.Equals("CardMilitary"))
        {
            return CardMilitary.GetUniqueCardMilitaryTypes();
        }
        else if (mainType.Equals("CardSpell"))
        {
            return CardSpell.GetUniqueCardSpellTypes();
        }
        else if (mainType.Equals("MagicFormationCircle"))
        {
            return MagicFormationCircle.GetUniqueMagicFormationCircleTypes();
        }
        else if (mainType.Equals("Relics"))
        {
            return Relics.GetUniqueRelicsTypes();
        }
        else if (mainType.Equals("CardColonels"))
        {
            return CardColonels.GetUniqueCardColonelsTypes();
        }
        else if (mainType.Equals("CardGenerals"))
        {
            return CardGenerals.GetUniqueCardGeneralsTypes();
        }
        else if (mainType.Equals("CardAdmirals"))
        {
            return CardAdmirals.GetUniqueCardAdmiralsTypes();
        }
        else if (mainType.Equals("SummonCardHeroes"))
        {
            return CardHeroes.GetUniqueCardHeroTypes();
        }
        else if (mainType.Equals("SummonBooks"))
        {
            return Books.GetUniqueBookTypes();
        }
        else if (mainType.Equals("SummonCardCaptains"))
        {
            return CardCaptains.GetUniqueCardCaptainsTypes();
        }
        else if (mainType.Equals("SummonCardMilitary"))
        {
            return CardMilitary.GetUniqueCardMilitaryTypes();
        }
        else if (mainType.Equals("SummonCardSpell"))
        {
            return CardSpell.GetUniqueCardSpellTypes();
        }
        return new List<string>();
    }
    public void GetButtonType()
    {
        // MainMenuPanel.SetActive(true);
        if (mainType.Equals("SummonCardHeroes") || mainType.Equals("SummonBooks") || mainType.Equals("SummonCardCaptains") ||
        mainType.Equals("SummonCardMonsters") || mainType.Equals("SummonCardMilitary") || mainType.Equals("SummonCardSpell"))
        {
            GameObject summonObject = Instantiate(SummonPanel, MainPanel);
            DictionaryContentPanel = summonObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            TabButtonPanel = summonObject.transform.Find("Scroll View/Viewport/ButtonContent");
            PositionPanel = summonObject.transform.Find("DictionaryCards/Position");
            titleText = summonObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
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
            else if (mainType.Equals("SummonBooks"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_51");
                dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_48");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals("SummonCardCaptains"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_50");
                dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_63");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals("SummonCardMonsters"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_49");
                dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_69");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals("SummonCardMilitary"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_48");
                dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_85");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
            else if (mainType.Equals("SummonCardSpell"))
            {
                Texture texture = Resources.Load<Texture>("UI/Background1/Background_V1_47");
                dictionaryBackground.texture = texture;
                Texture rawTexture = Resources.Load<Texture>("UI/Background4/Background_V4_94");
                rawImage1.texture = rawTexture;
                rawImage2.texture = rawTexture;
                background2.texture = texture;
            }
        }
        else
        {
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
        List<string> uniqueTypes = GetUniqueTypes();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, subtype));

                if (i == 0)
                {
                    subType = subtype;
                    ChangeButtonBackground(button, "Background_V4_166");
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

                    if (!mainType.Equals("SummonCardHeroes") && !mainType.Equals("SummonBooks") && !mainType.Equals("SummonCardCaptains") &&
                    !mainType.Equals("SummonCardMonsters") && !mainType.Equals("SummonCardMilitary") && !mainType.Equals("SummonCardSpell"))
                    {
                        totalPage = CalculateTotalPages(totalRecord, pageSize);
                        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
                    }

                }
                else
                {
                    ChangeButtonBackground(button, "Background_V4_167");
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

            if (!mainType.Equals("SummonCardMonsters"))
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
                ChangeButtonBackground(button.gameObject, "Background_V4_167"); // Giả sử bạn có texture trắng
            }
        }

        subType = type;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ChangeButtonBackground(clickedButton, "Background_V4_166");
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

        if (!mainType.Equals("SummonCardHeroes") && !mainType.Equals("SummonBooks") && !mainType.Equals("SummonCardCaptains") &&
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
        foreach (var spell in cardColonels)
        {
            GameObject spellObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = spell.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spell.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
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
        foreach (var spell in cardGenerals)
        {
            GameObject spellObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = spell.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spell.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
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
        foreach (var spell in cardAdmirals)
        {
            GameObject spellObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = spell.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spell.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
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
}
