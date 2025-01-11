using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Reflection;
using UnityEngine.EventSystems;

public class GalleryManagement : MonoBehaviour
{
    private Transform galleryMenuPanel;
    private GameObject buttonPrefab;
    private GameObject DictionaryPanel;
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private Transform DictionaryContentPanel;
    private Transform TabButtonPanel;
    private Button CloseButton;
    private Button HomeButton;
    private GameObject equipmentsPrefab;
    private GameObject MainMenuDetailPanelPrefab;
    private GameObject ElementDetailsPrefab;
    private GameObject NumberDetailPrefab;
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
        galleryMenuPanel = UIManager.Instance.GetTransform("galleryMenuPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        DictionaryPanel = UIManager.Instance.GetGameObject("DictionaryPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        MainMenuDetailPanelPrefab = UIManager.Instance.GetGameObject("MainMenuDetailPanelPrefab");
        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
        NumberDetailPrefab = UIManager.Instance.GetGameObject("NumberDetailPrefab");

        AssignButtonEvent("Button_1", () => GetType("CardHeroes"));
        AssignButtonEvent("Button_2", () => GetType("Books"));
        AssignButtonEvent("Button_3", () => GetType("Pets"));
        AssignButtonEvent("Button_4", () => GetType("CardCaptains"));
        AssignButtonEvent("Button_5", () => GetType("CollaborationEquipments"));
        AssignButtonEvent("Button_6", () => GetType("CardMilitary"));
        AssignButtonEvent("Button_7", () => GetType("CardSpell"));
        AssignButtonEvent("Button_8", () => GetType("Collaborations"));
        AssignButtonEvent("Button_9", () => GetType("CardMonsters"));
        AssignButtonEvent("Button_10", () => GetType("Equipments"));
        AssignButtonEvent("Button_11", () => GetType("Medals"));
        AssignButtonEvent("Button_12", () => GetType("Skills"));
        AssignButtonEvent("Button_13", () => GetType("Symbols"));
        AssignButtonEvent("Button_14", () => GetType("Titles"));
        AssignButtonEvent("Button_15", () => GetType("MagicFormationCircle"));
        AssignButtonEvent("Button_16", () => GetType("Relics"));
        AssignButtonEvent("Button_17", () => GetType("CardColonels"));
        AssignButtonEvent("Button_18", () => GetType("CardGenerals"));
        AssignButtonEvent("Button_19", () => GetType("CardAdmirals"));
        // GetCardsType();
    }

    void Update()
    {

    }
    void AssignButtonEvent(string buttonName, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = galleryMenuPanel.Find(buttonName);
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
        titleText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
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
        return new List<string>();
    }
    public void GetButtonType()
    {
        // DictionaryPanel.SetActive(true);
        GameObject equipmentObject = Instantiate(DictionaryPanel, MainPanel);
        DictionaryContentPanel = equipmentObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
        TabButtonPanel = equipmentObject.transform.Find("Scroll View/Viewport/ButtonContent");
        PageText = equipmentObject.transform.Find("Pagination/Page").GetComponent<Text>();
        NextButton = equipmentObject.transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = equipmentObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = equipmentObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = equipmentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(ClosePanel);
        HomeButton = equipmentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        NextButton.onClick.AddListener(ChangeNextPage);
        PreviousButton.onClick.AddListener(ChangePreviousPage);

        Transform CurrencyPanel = equipmentObject.transform.Find("DictionaryCards/Currency");
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetUserCurrency();
        FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);

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
                        List<CardHeroes> cards = cardsManager.GetCardHeroes(subtype, pageSize, offset);
                        createCardHeroes(cards);

                        totalRecord = cardsManager.GetCardHeroesCount(subtype);
                    }
                    else if (mainType.Equals("Books"))
                    {
                        Books booksManager = new Books();
                        List<Books> books = booksManager.GetBooks(subtype, pageSize, offset);
                        createBooks(books);

                        totalRecord = booksManager.GetBooksCount(subtype);
                    }
                    else if (mainType.Equals("CardCaptains"))
                    {
                        CardCaptains captainsManager = new CardCaptains();
                        List<CardCaptains> captains = captainsManager.GetCardCaptains(subtype, pageSize, offset);
                        createCardCaptains(captains);

                        totalRecord = captainsManager.GetCardCaptainsCount(subtype);
                    }
                    else if (mainType.Equals("CollaborationEquipments"))
                    {
                        CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                        List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipments(subtype, pageSize, offset);
                        createCollaborationEquipments(collaborationEquipments);

                        totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentCount(subtype);
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
                        List<Pets> pets = petsManager.GetPets(subtype, pageSize, offset);
                        createPets(pets);

                        totalRecord = petsManager.GetPetsCount(subtype);
                    }
                    else if (mainType.Equals("Skills"))
                    {
                        Skills skillsManager = new Skills();
                        List<Skills> skills = skillsManager.GetSkills(subtype, pageSize, offset);
                        createSkills(skills);

                        totalRecord = skillsManager.GetSkillsCount(subtype);
                    }
                    else if (mainType.Equals("Symbols"))
                    {
                        Symbols symbolsManager = new Symbols();
                        List<Symbols> symbols = symbolsManager.GetSymbols(subtype, pageSize, offset);
                        createSymbols(symbols);

                        totalRecord = symbolsManager.GetSymbolsCount(subtype);
                    }
                    else if (mainType.Equals("CardMilitary"))
                    {
                        CardMilitary militaryManager = new CardMilitary();
                        List<CardMilitary> militaryList = militaryManager.GetCardMilitary(subtype, pageSize, offset);
                        createCardMilitary(militaryList);

                        totalRecord = militaryManager.GetCardMilitaryCount(subType);
                    }
                    else if (mainType.Equals("CardSpell"))
                    {
                        CardSpell spellManager = new CardSpell();
                        List<CardSpell> spellList = spellManager.GetCardSpell(subtype, pageSize, offset);
                        createCardSpell(spellList);

                        totalRecord = spellManager.GetCardSpellCount(subType);
                    }
                    else if (mainType.Equals("MagicFormationCircle"))
                    {
                        MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                        List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircle(subtype, pageSize, offset);
                        createMagicFormationCircle(magicFormationCircles);

                        totalRecord = magicFormationCircleManager.GetMagicFormationCircleCount(subType);
                    }
                    else if (mainType.Equals("Relics"))
                    {
                        Relics relicsManager = new Relics();
                        List<Relics> relicsList = relicsManager.GetRelics(subtype, pageSize, offset);
                        createRelics(relicsList);

                        totalRecord = relicsManager.GetRelicsCount(subType);
                    }
                    else if (mainType.Equals("CardColonels"))
                    {
                        CardColonels colonelsManager = new CardColonels();
                        List<CardColonels> colonels = colonelsManager.GetCardColonels(subtype, pageSize, offset);
                        createCardColonels(colonels);

                        totalRecord = colonelsManager.GetCardColonelsCount(subType);
                    }
                    else if (mainType.Equals("CardGenerals"))
                    {
                        CardGenerals generalsManager = new CardGenerals();
                        List<CardGenerals> relicsList = generalsManager.GetCardGenerals(subtype, pageSize, offset);
                        createCardGenerals(relicsList);

                        totalRecord = generalsManager.GetCardGeneralsCount(subType);
                    }
                    else if (mainType.Equals("CardAdmirals"))
                    {
                        CardAdmirals admiralsManager = new CardAdmirals();
                        List<CardAdmirals> relicsList = admiralsManager.GetCardAdmirals(subtype, pageSize, offset);
                        createCardAdmirals(relicsList);

                        totalRecord = admiralsManager.GetCardAdmiralsCount(subType);
                    }

                    totalPage = CalculateTotalPages(totalRecord, pageSize);
                    PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

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
            if (mainType.Equals("Collaborations"))
            {
                Collaboration collaborationManager = new Collaboration();
                List<Collaboration> collaborations = collaborationManager.GetCollaboration(pageSize, offset);
                createCollaboration(collaborations);

                totalRecord = collaborationManager.GetCollaborationCount();
            }
            else if (mainType.Equals("Medals"))
            {
                Medals medalsManager = new Medals();
                List<Medals> medalsList = medalsManager.GetMedals(pageSize, offset);
                createMedals(medalsList);

                totalRecord = medalsManager.GetMedalsCount();
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                List<CardMonsters> monstersList = monstersManager.GetCardMonsters(pageSize, offset);
                createCardMonsters(monstersList);

                totalRecord = monstersManager.GetCardMonstersCount();
            }
            else if (mainType.Equals("Titles"))
            {
                Titles titleManager = new Titles();
                List<Titles> titlesList = titleManager.GetTitles(pageSize, offset);
                createTitles(titlesList);

                totalRecord = titleManager.GetTitlesCount();
            }

            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
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
            List<CardHeroes> cards = cardsManager.GetCardHeroes(type, pageSize, offset);
            createCardHeroes(cards);

            totalRecord = cardsManager.GetCardHeroesCount(type);
        }
        else if (mainType.Equals("Books"))
        {
            Books booksManager = new Books();
            List<Books> books = booksManager.GetBooks(type, pageSize, offset);
            createBooks(books);

            totalRecord = booksManager.GetBooksCount(type);
        }
        else if (mainType.Equals("CardCaptains"))
        {
            CardCaptains captainsManager = new CardCaptains();
            List<CardCaptains> captains = captainsManager.GetCardCaptains(type, pageSize, offset);
            createCardCaptains(captains);

            totalRecord = captainsManager.GetCardCaptainsCount(type);
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
            List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipments(type, pageSize, offset);
            createCollaborationEquipments(collaborationEquipments);

            totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentCount(type);
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
            List<Pets> pets = petsManager.GetPets(type, pageSize, offset);
            createPets(pets);

            totalRecord = petsManager.GetPetsCount(type);
        }
        else if (mainType.Equals("Skills"))
        {
            Skills skillsManager = new Skills();
            List<Skills> skills = skillsManager.GetSkills(type, pageSize, offset);
            createSkills(skills);

            totalRecord = skillsManager.GetSkillsCount(type);
        }
        else if (mainType.Equals("Symbols"))
        {
            Symbols symbolsManager = new Symbols();
            List<Symbols> symbols = symbolsManager.GetSymbols(type, pageSize, offset);
            createSymbols(symbols);

            totalRecord = symbolsManager.GetSymbolsCount(type);
        }
        else if (mainType.Equals("CardMilitary"))
        {
            CardMilitary militaryManager = new CardMilitary();
            List<CardMilitary> militaryList = militaryManager.GetCardMilitary(type, pageSize, offset);
            createCardMilitary(militaryList);

            totalRecord = militaryManager.GetCardMilitaryCount(type);
        }
        else if (mainType.Equals("CardSpell"))
        {
            CardSpell spellManager = new CardSpell();
            List<CardSpell> spellList = spellManager.GetCardSpell(type, pageSize, offset);
            createCardSpell(spellList);

            totalRecord = spellManager.GetCardSpellCount(type);
        }
        else if (mainType.Equals("MagicFormationCircle"))
        {
            MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
            List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircle(type, pageSize, offset);
            createMagicFormationCircle(magicFormationCircles);

            totalRecord = magicFormationCircleManager.GetMagicFormationCircleCount(subType);
        }
        else if (mainType.Equals("Relics"))
        {
            Relics relicsManager = new Relics();
            List<Relics> relicsList = relicsManager.GetRelics(type, pageSize, offset);
            createRelics(relicsList);

            totalRecord = relicsManager.GetRelicsCount(subType);
        }
        else if (mainType.Equals("CardColonels"))
        {
            CardColonels colonelsManager = new CardColonels();
            List<CardColonels> colonels = colonelsManager.GetCardColonels(type, pageSize, offset);
            createCardColonels(colonels);

            totalRecord = colonelsManager.GetCardColonelsCount(subType);
        }
        else if (mainType.Equals("CardGenerals"))
        {
            CardGenerals generalsManager = new CardGenerals();
            List<CardGenerals> relicsList = generalsManager.GetCardGenerals(type, pageSize, offset);
            createCardGenerals(relicsList);

            totalRecord = generalsManager.GetCardGeneralsCount(subType);
        }
        else if (mainType.Equals("CardAdmirals"))
        {
            CardAdmirals admiralsManager = new CardAdmirals();
            List<CardAdmirals> relicsList = admiralsManager.GetCardAdmirals(type, pageSize, offset);
            createCardAdmirals(relicsList);

            totalRecord = admiralsManager.GetCardAdmiralsCount(subType);
        }

        totalPage = CalculateTotalPages(totalRecord, pageSize);
        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
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
            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(card));
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

            RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
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

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(book));
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

            RawImage rareImage = bookObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
            rareImage.texture = rareTexture;
            // Đặt kích thước gốc
            Image.SetNativeSize();

            // Thay đổi tỉ lệ
            if (texture.width < 1400 && texture.height < 1400 && texture.width > 700 && texture.height > 700)
            {
                Image.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
            }
            else if (texture.width > 1000 && texture.height <= 2100 && texture.width < 2000 && texture.height > 1000)
            {
                Image.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
            }
            else if (texture.width <= 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.60f, 0.6f, 0.6f);
            }
            else if (texture.width <= 700 && texture.height <= 1100)
            {
                Image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (texture.width > 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.3f, 0.4f, 0.3f);
            }
            else
            {
                Image.transform.localScale = new Vector3(0.17f, 0.17f, 0.17f);
            }
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(280, 300);
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
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(captain));
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

            RawImage rareImage = captainsObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captain.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
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
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(collaboration));
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

            RawImage rareImage = collaborationObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(280, 230);
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
            AddClickListener(eventTrigger, () => PopupDetails(collaborationEquipment));
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
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
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
            AddClickListener(eventTrigger, () => PopupDetails(equipment));
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
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
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
            AddClickListener(eventTrigger, () => PopupDetails(medal));
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
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
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
            AddClickListener(eventTrigger, () => PopupDetails(monster));
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
            AddClickListener(eventTrigger, () => PopupDetails(pet));
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
            AddClickListener(eventTrigger, () => PopupDetails(skill));
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
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
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
            AddClickListener(eventTrigger, () => PopupDetails(symbol));
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
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
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
            AddClickListener(eventTrigger, () => PopupDetails(title));
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
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
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
            AddClickListener(eventTrigger, () => PopupDetails(military));
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
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
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
            AddClickListener(eventTrigger, () => PopupDetails(spell));
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
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(spell));
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
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(spell));
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
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(spell));
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
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
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
            EventTrigger eventTrigger = frameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = frameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(magicFormationCircle));
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
            EventTrigger eventTrigger = frameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = frameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(relic));
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
    public void ClearAllPrefabs()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        foreach (Transform child in DictionaryContentPanel)
        {
            Destroy(child.gameObject);
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
                totalRecord = cardsManager.GetCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cards = cardsManager.GetCardHeroes(subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                Books booksManager = new Books();
                totalRecord = booksManager.GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = booksManager.GetBooks(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                CardCaptains captainsManager = new CardCaptains();
                totalRecord = captainsManager.GetCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> army = captainsManager.GetCardCaptains(subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipments(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaborations"))
            {
                Collaboration collaborationManager = new Collaboration();
                totalRecord = collaborationManager.GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaboration = collaborationManager.GetCollaboration(pageSize, offset);
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
                totalRecord = medalsManager.GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = medalsManager.GetMedals(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                totalRecord = monstersManager.GetCardMonstersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> monstersList = monstersManager.GetCardMonsters(pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                Pets petsManager = new Pets();
                totalRecord = petsManager.GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> petsList = petsManager.GetPets(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                Skills skillsManager = new Skills();
                totalRecord = skillsManager.GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skillsList = skillsManager.GetSkills(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                Symbols symbolsManager = new Symbols();
                totalRecord = symbolsManager.GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbolsList = symbolsManager.GetSymbols(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                Titles symbolsManager = new Titles();
                totalRecord = symbolsManager.GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titlesList = symbolsManager.GetTitles(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                CardMilitary militaryManager = new CardMilitary();
                totalRecord = militaryManager.GetCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> militaryList = militaryManager.GetCardMilitary(subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                CardSpell spellManager = new CardSpell();
                totalRecord = spellManager.GetCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> spellList = spellManager.GetCardSpell(subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                totalRecord = magicFormationCircleManager.GetMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircle(subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                Relics relicsManager = new Relics();
                totalRecord = relicsManager.GetRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relicsList = relicsManager.GetRelics(subType, pageSize, offset);
                createRelics(relicsList);
            }
            else if (mainType.Equals("CardColonels"))
            {
                CardColonels relicsManager = new CardColonels();
                totalRecord = relicsManager.GetCardColonelsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> relicsList = relicsManager.GetCardColonels(subType, pageSize, offset);
                createCardColonels(relicsList);
            }
            else if (mainType.Equals("CardGenerals"))
            {
                CardGenerals relicsManager = new CardGenerals();
                totalRecord = relicsManager.GetCardGeneralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> relicsList = relicsManager.GetCardGenerals(subType, pageSize, offset);
                createCardGenerals(relicsList);
            }
            else if (mainType.Equals("CardAdmirals"))
            {
                CardAdmirals relicsManager = new CardAdmirals();
                totalRecord = relicsManager.GetCardAdmiralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> relicsList = relicsManager.GetCardAdmirals(subType, pageSize, offset);
                createCardAdmirals(relicsList);
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
                totalRecord = cardsManager.GetCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cards = cardsManager.GetCardHeroes(subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                Books booksManager = new Books();
                totalRecord = booksManager.GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = booksManager.GetBooks(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                CardCaptains captainsManager = new CardCaptains();
                totalRecord = captainsManager.GetCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> army = captainsManager.GetCardCaptains(subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipments(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaborations"))
            {
                Collaboration collaborationManager = new Collaboration();
                totalRecord = collaborationManager.GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaboration = collaborationManager.GetCollaboration(pageSize, offset);
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
                totalRecord = medalsManager.GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = medalsManager.GetMedals(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                totalRecord = monstersManager.GetCardMonstersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monstersList = monstersManager.GetCardMonsters(pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                Pets petsManager = new Pets();
                totalRecord = petsManager.GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> petsList = petsManager.GetPets(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                Skills skillsManager = new Skills();
                totalRecord = skillsManager.GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skillsList = skillsManager.GetSkills(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                Symbols symbolsManager = new Symbols();
                totalRecord = symbolsManager.GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbolsList = symbolsManager.GetSymbols(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                Titles symbolsManager = new Titles();
                totalRecord = symbolsManager.GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titlesList = symbolsManager.GetTitles(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                CardMilitary militaryManager = new CardMilitary();
                totalRecord = militaryManager.GetCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> militaryList = militaryManager.GetCardMilitary(subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                CardSpell spellManager = new CardSpell();
                totalRecord = spellManager.GetCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> spellList = spellManager.GetCardSpell(subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                totalRecord = magicFormationCircleManager.GetMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircle(subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                Relics relicsManager = new Relics();
                totalRecord = relicsManager.GetRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relicsList = relicsManager.GetRelics(subType, pageSize, offset);
                createRelics(relicsList);
            }
            else if (mainType.Equals("CardColonels"))
            {
                CardColonels relicsManager = new CardColonels();
                totalRecord = relicsManager.GetCardColonelsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> relicsList = relicsManager.GetCardColonels(subType, pageSize, offset);
                createCardColonels(relicsList);
            }
            else if (mainType.Equals("CardGenerals"))
            {
                CardGenerals relicsManager = new CardGenerals();
                totalRecord = relicsManager.GetCardGeneralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> relicsList = relicsManager.GetCardGenerals(subType, pageSize, offset);
                createCardGenerals(relicsList);
            }
            else if (mainType.Equals("CardAdmirals"))
            {
                CardAdmirals relicsManager = new CardAdmirals();
                totalRecord = relicsManager.GetCardAdmiralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> relicsList = relicsManager.GetCardAdmirals(subType, pageSize, offset);
                createCardAdmirals(relicsList);
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
    public void PopupDetails(object data)
    {
        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes card)
        {
            // Xử lý đối tượng Card
            ShowCardHeroesDetails(card);
        }
        else if (data is Books book)
        {
            // Xử lý đối tượng Book
            ShowBookDetails(book);
        }
        else if (data is CardCaptains captain)
        {
            // Xử lý đối tượng Captain
            ShowCardCaptainDetails(captain);
        }
        else if (data is Pets pet)
        {
            // Xử lý đối tượng Pet
            ShowPetDetails(pet);
        }
        else if (data is CollaborationEquipment collaborationEquipmentsequipment)
        {
            // Xử lý đối tượng CollaborationEquipment
            ShowCollaborationEquipmentDetails(collaborationEquipmentsequipment);
        }
        else if (data is CardMilitary military)
        {
            // Xử lý đối tượng Military
            ShowCardMilitaryDetails(military);
        }
        else if (data is CardSpell spell)
        {
            // Xử lý đối tượng Spell
            ShowCardSpellDetails(spell);
        }
        else if (data is Collaboration collaboration)
        {
            // Xử lý đối tượng Collaboration
            ShowCollaborationDetails(collaboration);
        }
        else if (data is CardMonsters monster)
        {
            // Xử lý đối tượng Monster
            ShowCardMonsterDetails(monster);
        }
        else if (data is Equipments equipment)
        {
            // Xử lý đối tượng Equipment
            ShowEquipmentDetails(equipment);
        }
        else if (data is Medals medal)
        {
            // Xử lý đối tượng Medal
            ShowMedalDetails(medal);
        }
        else if (data is Skills skill)
        {
            // Xử lý đối tượng Skill
            ShowSkillDetails(skill);
        }
        else if (data is Symbols symbol)
        {
            // Xử lý đối tượng Symbol
            ShowSymbolDetails(symbol);
        }
        else if (data is Titles title)
        {
            // Xử lý đối tượng Title
            ShowTitleDetails(title);
        }
        else if (data is MagicFormationCircle magicFormationCircle)
        {
            // Xử lý đối tượng Title
            ShowMagicFormationCircleDetails(magicFormationCircle);
        }
        else if (data is Relics relics)
        {
            // Xử lý đối tượng Title
            ShowRelicsDetails(relics);
        }
        else if (data is CardColonels colonels)
        {
            // Xử lý đối tượng colonels
            ShowCardColonelsDetails(colonels);
        }
        else if (data is CardGenerals generals)
        {
            // Xử lý đối tượng Generals
            ShowCardGeneralsDetails(generals);
        }
        else if (data is CardAdmirals admirals)
        {
            // Xử lý đối tượng admirals
            ShowCardAdmiralsDetails(admirals);
        }
        else
        {
            Debug.LogError("Không hỗ trợ loại dữ liệu này!");
        }

    }
    // Hàm thêm sự kiện click vào EventTrigger
    void AddClickListener(EventTrigger trigger, System.Action callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { callback(); });
        trigger.triggers.Add(entry);
    }
    void ClosePopup(GameObject popup)
    {
        Destroy(popup); // Hủy popupObject khi nút CloseButton được nhấn
    }
    private void ShowCardHeroesDetails(CardHeroes card)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = card.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = card.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = card.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = card.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = card.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(card, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                    GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                    if (gridLayout != null)
                    {
                        gridLayout.cellSize = new Vector2(670, 800);
                    }
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowBookDetails(Books book)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = book.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 700f; // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = book.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = book.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = book.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = book.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(book, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                    GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                    if (gridLayout != null)
                    {
                        gridLayout.cellSize = new Vector2(670, 800);
                    }
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardCaptainDetails(CardCaptains captains)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = captains.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = captains.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = captains.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = captains.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captains.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = captains.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(captains, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowPetDetails(Pets pet)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = pet.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            if (pet.type.Equals("Legendary_Dragon") || pet.type.Equals("Naruto_Bijuu") || pet.type.Equals("Naruto_Susanoo") || pet.type.Equals("One_Piece_Ship") || pet.type.Equals("Prime_Monster"))
            {
                newHeight = 700f;
            }
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = pet.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = pet.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = pet.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{pet.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = pet.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(pet, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCollaborationEquipmentDetails(CollaborationEquipment collaborationEquipment)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = collaborationEquipment.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = collaborationEquipment.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = collaborationEquipment.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(collaborationEquipment, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardMilitaryDetails(CardMilitary military)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = military.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = military.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = military.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = military.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = military.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(military, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardSpellDetails(CardSpell spell)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = spell.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = spell.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = spell.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = spell.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = spell.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(spell, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCollaborationDetails(Collaboration collaboration)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = collaboration.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = collaboration.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = collaboration.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = collaboration.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaboration.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaboration.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(collaboration, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardMonsterDetails(CardMonsters monsters)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = monsters.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = monsters.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = monsters.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = monsters.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monsters.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = monsters.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(monsters, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowEquipmentDetails(Equipments equipments)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = equipments.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = equipments.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = equipments.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = equipments.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipments.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));
        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = equipments.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(equipments, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowMedalDetails(Medals medals)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = medals.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = medals.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = medals.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = medals.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{medals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = medals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(medals, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowSkillDetails(Skills skills)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = skills.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = skills.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = skills.power.ToString();

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skills.rare}");
        rareImage.texture = rareTexture;

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = skills.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(skills, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowSymbolDetails(Symbols symbols)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = symbols.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = symbols.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = symbols.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{symbols.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = symbols.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(symbols, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowTitleDetails(Titles titles)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = titles.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = titles.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = titles.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{titles.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = titles.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(titles, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowMagicFormationCircleDetails(MagicFormationCircle magicFormationCircle)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = magicFormationCircle.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = magicFormationCircle.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = magicFormationCircle.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{magicFormationCircle.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = magicFormationCircle.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(magicFormationCircle, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowRelicsDetails(Relics relics)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = relics.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = relics.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = relics.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relics.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = relics.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(relics, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardColonelsDetails(CardColonels colonels)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = colonels.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = colonels.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = colonels.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{colonels.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = colonels.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(colonels, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardGeneralsDetails(CardGenerals generals)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = generals.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = generals.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = generals.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{generals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = generals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(generals, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardAdmiralsDetails(CardAdmirals admirals)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = admirals.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = admirals.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = admirals.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{admirals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = admirals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(admirals, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
            {
                if (property.Name.Equals("description"))
                {
                    // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                    GameObject descriptionTextObject = new GameObject("DescriptionText");
                    descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                    // Thêm component TextMeshProUGUI vào đối tượng mới
                    TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                    // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                    descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                    descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                    descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                    // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                    // Đổi màu chữ bằng mã hex #844000
                    Color color;
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
}
