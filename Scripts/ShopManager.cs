using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform SummonMainMenuPanel;
    private Transform TabButtonPanel;
    private Transform currentContent;
    private GameObject ShopButtonPrefab;
    private GameObject ShopManagerPrefab;
    private GameObject currentObject;
    private GameObject ShopPrefab;
    private GameObject buttonPrefab;
    private GameObject equipmentsShopPrefab;
    private GameObject MainMenuDetailPanelPrefab;
    private GameObject ElementDetailsPrefab;
    private Button CloseButton;
    private Button HomeButton;
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
    // Start is called before the first frame update
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        SummonMainMenuPanel = UIManager.Instance.GetTransform("summonPanel");
        ShopButtonPrefab = UIManager.Instance.GetGameObject("ShopButtonPrefab");
        ShopManagerPrefab = UIManager.Instance.GetGameObject("ShopManagerPrefab");
        ShopPrefab = UIManager.Instance.GetGameObject("ShopPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        equipmentsShopPrefab = UIManager.Instance.GetGameObject("equipmentsShopPrefab");
        MainMenuDetailPanelPrefab = UIManager.Instance.GetGameObject("MainMenuDetailPanelPrefab");
        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
        AssignButtonEvent("Button_25", SummonMainMenuPanel, () => CreateShopButton());
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
    public void CreateShopButton()
    {
        currentObject = Instantiate(ShopManagerPrefab, MainPanel);
        titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        titleText.text = "Shop";
        CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(currentObject));
        HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Destroy(currentObject));
        Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetUserCurrency();
        FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);

        Transform tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        CreateButton(1, "Card Heroes", Resources.Load<Texture2D>($"UI/Button/CardsGallery"), tempContent);
        CreateButton(2, "Books", Resources.Load<Texture2D>($"UI/Button/BooksGallery"), tempContent);
        CreateButton(3, "Pets", Resources.Load<Texture2D>($"UI/Button/PetsGallery"), tempContent);
        CreateButton(4, "Card Captains", Resources.Load<Texture2D>($"UI/Button/CaptainsGallery"), tempContent);
        CreateButton(5, "Collaboration Equipments", Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsGallery"), tempContent);
        CreateButton(6, "Card Military", Resources.Load<Texture2D>($"UI/Button/MilitaryGallery"), tempContent);
        CreateButton(7, "Card Spell", Resources.Load<Texture2D>($"UI/Button/SpellGallery"), tempContent);
        CreateButton(8, "Collaborations", Resources.Load<Texture2D>($"UI/Button/CollaborationsGallery"), tempContent);
        CreateButton(9, "Card Monsters", Resources.Load<Texture2D>($"UI/Button/MonstersGallery"), tempContent);
        CreateButton(10, "Borders", Resources.Load<Texture2D>($"UI/Button/BorderGallery"), tempContent);
        CreateButton(11, "Medals", Resources.Load<Texture2D>($"UI/Button/MedalsGallery"), tempContent);
        CreateButton(12, "Skills", Resources.Load<Texture2D>($"UI/Button/SkillsGallery"), tempContent);
        CreateButton(13, "Symbols", Resources.Load<Texture2D>($"UI/Button/SymbolsGallery"), tempContent);
        CreateButton(14, "Titles", Resources.Load<Texture2D>($"UI/Button/TitlesGallery"), tempContent);
        CreateButton(15, "Magic Formation Circle", Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleGallery"), tempContent);
        CreateButton(16, "Relics", Resources.Load<Texture2D>($"UI/Button/RelicsGallery"), tempContent);
        CreateButton(17, "Items", Resources.Load<Texture2D>($"UI/Button/ItemsGallery"), tempContent);
        CreateButton(18, "Achievements", Resources.Load<Texture2D>($"UI/Button/AchievementGallery"), tempContent);

        AssignButtonEvent("Button_1", tempContent, () => GetType("CardHeroes"));
        AssignButtonEvent("Button_2", tempContent, () => GetType("Books"));
        AssignButtonEvent("Button_3", tempContent, () => GetType("Pets"));
        AssignButtonEvent("Button_4", tempContent, () => GetType("CardCaptains"));
        AssignButtonEvent("Button_5", tempContent, () => GetType("CollaborationEquipments"));
        AssignButtonEvent("Button_6", tempContent, () => GetType("CardMilitary"));
        AssignButtonEvent("Button_7", tempContent, () => GetType("CardSpell"));
        AssignButtonEvent("Button_8", tempContent, () => GetType("Collaborations"));
        AssignButtonEvent("Button_9", tempContent, () => GetType("CardMonsters"));
        AssignButtonEvent("Button_10", tempContent, () => GetType("Borders"));
        AssignButtonEvent("Button_11", tempContent, () => GetType("Medals"));
        AssignButtonEvent("Button_12", tempContent, () => GetType("Skills"));
        AssignButtonEvent("Button_13", tempContent, () => GetType("Symbols"));
        AssignButtonEvent("Button_14", tempContent, () => GetType("Titles"));
        AssignButtonEvent("Button_15", tempContent, () => GetType("MagicFormationCircle"));
        AssignButtonEvent("Button_16", tempContent, () => GetType("Relics"));
        AssignButtonEvent("Button_17", tempContent, () => GetType("Items"));
        AssignButtonEvent("Button_18", tempContent, () => GetType("Achievements"));
    }
    private void CreateButton(int index, string itemName, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ShopButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        Text nameText = newButton.transform.Find("ItemName").GetComponent<Text>();
        if (nameText != null)
        {
            nameText.text = itemName;
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
        return new List<string>();
    }
    public void GetButtonType()
    {
        // DictionaryPanel.SetActive(true);
        GameObject equipmentObject = Instantiate(ShopPrefab, MainPanel);
        currentContent = equipmentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TabButtonPanel = equipmentObject.transform.Find("Scroll View/Viewport/Content");
        PageText = equipmentObject.transform.Find("Pagination/Page").GetComponent<Text>();
        NextButton = equipmentObject.transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = equipmentObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = equipmentObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = equipmentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(equipmentObject));
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
                        List<CardHeroes> cards = cardsManager.GetCardHeroesWithPrice(subtype, pageSize, offset);
                        createCardHeroes(cards);

                        totalRecord = cardsManager.GetCardHeroesWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Books"))
                    {
                        Books booksManager = new Books();
                        List<Books> books = booksManager.GetBooksWithPrice(subtype, pageSize, offset);
                        createBooks(books);

                        totalRecord = booksManager.GetBookssWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardCaptains"))
                    {
                        CardCaptains captainsManager = new CardCaptains();
                        List<CardCaptains> captains = captainsManager.GetCardCaptainsWithPrice(subtype, pageSize, offset);
                        createCardCaptains(captains);

                        totalRecord = captainsManager.GetCardCaptainsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CollaborationEquipments"))
                    {
                        CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                        List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipmentsWithPrice(subtype, pageSize, offset);
                        createCollaborationEquipments(collaborationEquipments);

                        totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentsWithPriceCount(subtype);
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
                        List<Pets> pets = petsManager.GetPetsWithPrice(subtype, pageSize, offset);
                        createPets(pets);

                        totalRecord = petsManager.GetPetsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Skills"))
                    {
                        Skills skillsManager = new Skills();
                        List<Skills> skills = skillsManager.GetSkillsWithPrice(subtype, pageSize, offset);
                        createSkills(skills);

                        totalRecord = skillsManager.GetSkillsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Symbols"))
                    {
                        Symbols symbolsManager = new Symbols();
                        List<Symbols> symbols = symbolsManager.GetSymbolsWithPrice(subtype, pageSize, offset);
                        createSymbols(symbols);

                        totalRecord = symbolsManager.GetSkillsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardMilitary"))
                    {
                        CardMilitary militaryManager = new CardMilitary();
                        List<CardMilitary> militaryList = militaryManager.GetCardMilitaryWithPrice(subtype, pageSize, offset);
                        createCardMilitary(militaryList);

                        totalRecord = militaryManager.GetCardMilitaryWithPriceCount(subType);
                    }
                    else if (mainType.Equals("CardSpell"))
                    {
                        CardSpell spellManager = new CardSpell();
                        List<CardSpell> spellList = spellManager.GetCardSpellWithPrice(subtype, pageSize, offset);
                        createCardSpell(spellList);

                        totalRecord = spellManager.GetCardSpellWithPriceCount(subType);
                    }
                    else if (mainType.Equals("MagicFormationCircle"))
                    {
                        MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                        List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircleWithPrice(subtype, pageSize, offset);
                        createMagicFormationCircle(magicFormationCircles);

                        totalRecord = magicFormationCircleManager.GetMagicFormationCircleWithPriceCount(subType);
                    }
                    else if (mainType.Equals("Relics"))
                    {
                        Relics relicsManager = new Relics();
                        List<Relics> relicsList = relicsManager.GetRelicsWithPrice(subtype, pageSize, offset);
                        createRelics(relicsList);

                        totalRecord = relicsManager.GetRelicsWithPriceCount(subType);
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
                List<Collaboration> collaborations = collaborationManager.GetCollaborationWithPrice(pageSize, offset);
                createCollaboration(collaborations);

                totalRecord = collaborationManager.GetCollaborationWithPriceCount();
            }
            else if (mainType.Equals("Medals"))
            {
                Medals medalsManager = new Medals();
                List<Medals> medalsList = medalsManager.GetMedalsWithPrice(pageSize, offset);
                createMedals(medalsList);

                totalRecord = medalsManager.GetMedalsWithPriceCount();
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                List<CardMonsters> monstersList = monstersManager.GetCardMonstersWithPrice(pageSize, offset);
                createCardMonsters(monstersList);

                totalRecord = monstersManager.GetCardMonstersWithPriceCount();
            }
            else if (mainType.Equals("Titles"))
            {
                Titles titleManager = new Titles();
                List<Titles> titlesList = titleManager.GetTitlesWithPrice(pageSize, offset);
                createTitles(titlesList);

                totalRecord = titleManager.GetTitlesWithPriceCount();
            }
            else if (mainType.Equals("Borders"))
            {
                Borders bordersManager = new Borders();
                List<Borders> borders = bordersManager.GetBordersWithPrice(pageSize, offset);
                createBorders(borders);

                totalRecord = bordersManager.GetBordersWithPriceCount();
            }
            else if (mainType.Equals("Achievements"))
            {
                Achievements achievementsManager = new Achievements();
                List<Achievements> achievements = achievementsManager.GetAchievementsWithPrice(pageSize, offset);
                createAchievements(achievements);

                totalRecord = achievementsManager.GetAchievementsWithPriceCount();
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
            List<CardHeroes> cards = cardsManager.GetCardHeroesWithPrice(type, pageSize, offset);
            createCardHeroes(cards);

            totalRecord = cardsManager.GetCardHeroesWithPriceCount(type);
        }
        else if (mainType.Equals("Books"))
        {
            Books booksManager = new Books();
            List<Books> books = booksManager.GetBooksWithPrice(type, pageSize, offset);
            createBooks(books);

            totalRecord = booksManager.GetBookssWithPriceCount(type);
        }
        else if (mainType.Equals("CardCaptains"))
        {
            CardCaptains captainsManager = new CardCaptains();
            List<CardCaptains> captains = captainsManager.GetCardCaptainsWithPrice(type, pageSize, offset);
            createCardCaptains(captains);

            totalRecord = captainsManager.GetCardCaptainsWithPriceCount(type);
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
            List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipmentsWithPrice(type, pageSize, offset);
            createCollaborationEquipments(collaborationEquipments);

            totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentsWithPriceCount(type);
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
            List<Pets> pets = petsManager.GetPetsWithPrice(type, pageSize, offset);
            createPets(pets);

            totalRecord = petsManager.GetPetsWithPriceCount(type);
        }
        else if (mainType.Equals("Skills"))
        {
            Skills skillsManager = new Skills();
            List<Skills> skills = skillsManager.GetSkillsWithPrice(type, pageSize, offset);
            createSkills(skills);

            totalRecord = skillsManager.GetSkillsWithPriceCount(type);
        }
        else if (mainType.Equals("Symbols"))
        {
            Symbols symbolsManager = new Symbols();
            List<Symbols> symbols = symbolsManager.GetSymbolsWithPrice(type, pageSize, offset);
            createSymbols(symbols);

            totalRecord = symbolsManager.GetSkillsWithPriceCount(type);
        }
        else if (mainType.Equals("CardMilitary"))
        {
            CardMilitary militaryManager = new CardMilitary();
            List<CardMilitary> militaryList = militaryManager.GetCardMilitaryWithPrice(type, pageSize, offset);
            createCardMilitary(militaryList);

            totalRecord = militaryManager.GetCardMilitaryWithPriceCount(type);
        }
        else if (mainType.Equals("CardSpell"))
        {
            CardSpell spellManager = new CardSpell();
            List<CardSpell> spellList = spellManager.GetCardSpellWithPrice(type, pageSize, offset);
            createCardSpell(spellList);

            totalRecord = spellManager.GetCardSpellWithPriceCount(type);
        }
        else if (mainType.Equals("MagicFormationCircle"))
        {
            MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
            List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircleWithPrice(type, pageSize, offset);
            createMagicFormationCircle(magicFormationCircles);

            totalRecord = magicFormationCircleManager.GetMagicFormationCircleWithPriceCount(subType);
        }
        else if (mainType.Equals("Relics"))
        {
            Relics relicsManager = new Relics();
            List<Relics> relicsList = relicsManager.GetRelicsWithPrice(type, pageSize, offset);
            createRelics(relicsList);

            totalRecord = relicsManager.GetRelicsWithPriceCount(subType);
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
            GameObject cardObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = cardObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = card.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = cardObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = card.currency.quantity.ToString();
        }
    }
    private void createBooks(List<Books> books)
    {
        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = bookObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = bookObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = book.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = bookObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = book.currency.quantity.ToString();
        }
    }
    private void createCardCaptains(List<CardCaptains> captainsList)
    {
        foreach (var captain in captainsList)
        {
            GameObject captainsObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = captainsObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captain.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = captainsObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = captain.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = captainsObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = captain.currency.quantity.ToString();
        }
    }
    private void createCollaboration(List<Collaboration> collaborationList)
    {
        foreach (var collaboration in collaborationList)
        {
            GameObject collaborationObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = collaborationObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = collaborationObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = collaboration.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = collaborationObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = collaboration.currency.quantity.ToString();
        }
    }
    private void createCollaborationEquipments(List<CollaborationEquipment> collaborationEquipmentList)
    {
        foreach (var collaborationEquipment in collaborationEquipmentList)
        {
            GameObject collaborationEquipmentObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = collaborationEquipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = collaborationEquipmentObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = collaborationEquipment.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = collaborationEquipmentObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = collaborationEquipment.currency.quantity.ToString();
        }
    }
    private void createEquipments(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
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
        // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        // if (gridLayout != null)
        // {
        //     gridLayout.cellSize = new Vector2(200, 230);
        // }
    }
    private void createMedals(List<Medals> medalsList)
    {
        foreach (var medal in medalsList)
        {
            GameObject medalObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = medalObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = medalObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = medal.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = medalObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = medal.currency.quantity.ToString();
        }
    }
    private void createCardMonsters(List<CardMonsters> monstersList)
    {
        foreach (var monster in monstersList)
        {
            GameObject monstersObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = monstersObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monster.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = monstersObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = monster.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = monstersObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = monster.currency.quantity.ToString();
        }
    }
    private void createPets(List<Pets> petsList)
    {
        foreach (var pet in petsList)
        {
            GameObject petsObject;
            if (pet.type.Equals("Legendary_Dragon") || pet.type.Equals("Naruto_Bijuu") || pet.type.Equals("Naruto_Susanoo") || pet.type.Equals("One_Piece_Ship") || pet.type.Equals("Prime_Monster"))
            {
                petsObject = Instantiate(equipmentsShopPrefab, currentContent);
                RawImage Background = petsObject.transform.Find("Background").GetComponent<RawImage>();
                Background.gameObject.SetActive(true);

                // GridLayoutGroup gridLayout = currentContent.GetComponent<GridLayoutGroup>();
                // if (gridLayout != null)
                // {
                //     gridLayout.cellSize = new Vector2(280, 280);
                // }
            }
            else
            {
                petsObject = Instantiate(equipmentsShopPrefab, currentContent);

                // GridLayoutGroup gridLayout = currentContent.GetComponent<GridLayoutGroup>();
                // if (gridLayout != null)
                // {
                //     gridLayout.cellSize = new Vector2(200, 230);
                // }
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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // if (pet.type.Equals("Prime_Monster"))
            // {
            //     Image.SetNativeSize();
            //     Image.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            // }

            // RawImage rareImage = petsObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = petsObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = pet.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = petsObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = pet.currency.quantity.ToString();

        }
    }
    private void createSkills(List<Skills> skillsList)
    {
        foreach (var skill in skillsList)
        {
            GameObject skillObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            // RawImage rareImage = skillObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skill.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = skillObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = skill.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = skillObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = skill.currency.quantity.ToString();
        }
    }
    private void createSymbols(List<Symbols> symbolsList)
    {
        foreach (var symbol in symbolsList)
        {
            GameObject symbolObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = symbolObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{symbol.rare}");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = symbolObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = symbol.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = symbolObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = symbol.currency.quantity.ToString();
        }
    }
    private void createTitles(List<Titles> titlesList)
    {
        foreach (var title in titlesList)
        {
            GameObject titleObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = titleObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{title.rare}");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = titleObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = title.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = titleObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = title.currency.quantity.ToString();
        }
    }
    private void createCardMilitary(List<CardMilitary> militaryList)
    {
        foreach (var military in militaryList)
        {
            GameObject militaryObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = militaryObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.rare}");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = militaryObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = military.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = militaryObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = military.currency.quantity.ToString();
        }
    }
    private void createCardSpell(List<CardSpell> spellList)
    {
        foreach (var spell in spellList)
        {
            GameObject spellObject = Instantiate(equipmentsShopPrefab, currentContent);

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
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = spellObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = spell.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = spellObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = spell.currency.quantity.ToString();
        }
    }
    private void createMagicFormationCircle(List<MagicFormationCircle> magicFormationCircles)
    {
        foreach (var magicFormationCircle in magicFormationCircles)
        {
            GameObject magicFormationCircleObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = magicFormationCircleObject.transform.Find("Title").GetComponent<Text>();
            Title.text = magicFormationCircle.name.Replace("_", " ");

            RawImage Image = magicFormationCircleObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = magicFormationCircle.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // RawImage frameImage = magicFormationCircleObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(magicFormationCircle));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = magicFormationCircleObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{magicFormationCircle.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = magicFormationCircleObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = magicFormationCircle.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = magicFormationCircleObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = magicFormationCircle.currency.quantity.ToString();

        }
    }
    private void createRelics(List<Relics> relics)
    {
        foreach (var relic in relics)
        {
            GameObject relicObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = relicObject.transform.Find("Title").GetComponent<Text>();
            Title.text = relic.name.Replace("_", " ");

            RawImage Image = relicObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = relic.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // RawImage frameImage = relicObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(relic));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = relicObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relic.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = relicObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = relic.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = relicObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = relic.currency.quantity.ToString();

        }
    }
    private void createBorders(List<Borders> borders)
    {
        foreach (var border in borders)
        {
            GameObject borderObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = borderObject.transform.Find("Title").GetComponent<Text>();
            Title.text = border.name.Replace("_", " ");

            RawImage Image = borderObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = border.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(border));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = medalObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = borderObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = border.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = borderObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = border.currency.quantity.ToString();
        }
    }
    private void createAchievements(List<Achievements> achievements)
    {
        foreach (var achievement in achievements)
        {
            GameObject achievementObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = achievementObject.transform.Find("Title").GetComponent<Text>();
            Title.text = achievement.name.Replace("_", " ");

            RawImage Image = achievementObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = achievement.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupDetails(achievement));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = medalObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = achievementObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = achievement.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = achievementObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = achievement.currency.quantity.ToString();
        }
    }
    public void ClearAllPrefabs()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        foreach (Transform child in currentContent)
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
                totalRecord = cardsManager.GetCardHeroesWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cards = cardsManager.GetCardHeroesWithPrice(subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                Books booksManager = new Books();
                totalRecord = booksManager.GetBookssWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = booksManager.GetBooksWithPrice(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                CardCaptains captainsManager = new CardCaptains();
                totalRecord = captainsManager.GetCardCaptainsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> army = captainsManager.GetCardCaptainsWithPrice(subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipmentsWithPrice(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaborations"))
            {
                Collaboration collaborationManager = new Collaboration();
                totalRecord = collaborationManager.GetCollaborationWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaboration = collaborationManager.GetCollaborationWithPrice(pageSize, offset);
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
                totalRecord = medalsManager.GetMedalsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = medalsManager.GetMedalsWithPrice(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                totalRecord = monstersManager.GetCardMonstersWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> monstersList = monstersManager.GetCardMonstersWithPrice(pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                Pets petsManager = new Pets();
                totalRecord = petsManager.GetPetsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> petsList = petsManager.GetPetsWithPrice(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                Skills skillsManager = new Skills();
                totalRecord = skillsManager.GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skillsList = skillsManager.GetSkillsWithPrice(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                Symbols symbolsManager = new Symbols();
                totalRecord = symbolsManager.GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbolsList = symbolsManager.GetSymbolsWithPrice(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                Titles symbolsManager = new Titles();
                totalRecord = symbolsManager.GetTitlesWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titlesList = symbolsManager.GetTitlesWithPrice(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                CardMilitary militaryManager = new CardMilitary();
                totalRecord = militaryManager.GetCardMilitaryWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> militaryList = militaryManager.GetCardMilitaryWithPrice(subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                CardSpell spellManager = new CardSpell();
                totalRecord = spellManager.GetCardSpellWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> spellList = spellManager.GetCardSpellWithPrice(subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                totalRecord = magicFormationCircleManager.GetMagicFormationCircleWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircleWithPrice(subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                Relics relicsManager = new Relics();
                totalRecord = relicsManager.GetRelicsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relicsList = relicsManager.GetRelicsWithPrice(subType, pageSize, offset);
                createRelics(relicsList);
            }
            else if (mainType.Equals("Borders"))
            {
                Borders bordersManager = new Borders();
                totalRecord = bordersManager.GetBordersWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Borders> borders = bordersManager.GetBordersWithPrice(pageSize, offset);
                createBorders(borders);
            }
            else if (mainType.Equals("Achievements"))
            {
                Achievements achievementsManager = new Achievements();
                totalRecord = achievementsManager.GetAchievementsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Achievements> achievements = achievementsManager.GetAchievementsWithPrice(pageSize, offset);
                createAchievements(achievements);
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
                totalRecord = cardsManager.GetCardHeroesWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cards = cardsManager.GetCardHeroesWithPrice(subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                Books booksManager = new Books();
                totalRecord = booksManager.GetBookssWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = booksManager.GetBooksWithPrice(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                CardCaptains captainsManager = new CardCaptains();
                totalRecord = captainsManager.GetCardCaptainsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> army = captainsManager.GetCardCaptainsWithPrice(subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipmentsWithPrice(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaborations"))
            {
                Collaboration collaborationManager = new Collaboration();
                totalRecord = collaborationManager.GetCollaborationWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaboration = collaborationManager.GetCollaborationWithPrice(pageSize, offset);
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
                totalRecord = medalsManager.GetMedalsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = medalsManager.GetMedalsWithPrice(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                CardMonsters monstersManager = new CardMonsters();
                totalRecord = monstersManager.GetCardMonstersWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monstersList = monstersManager.GetCardMonstersWithPrice(pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                Pets petsManager = new Pets();
                totalRecord = petsManager.GetPetsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> petsList = petsManager.GetPetsWithPrice(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                Skills skillsManager = new Skills();
                totalRecord = skillsManager.GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skillsList = skillsManager.GetSkillsWithPrice(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                Symbols symbolsManager = new Symbols();
                totalRecord = symbolsManager.GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbolsList = symbolsManager.GetSymbolsWithPrice(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                Titles symbolsManager = new Titles();
                totalRecord = symbolsManager.GetTitlesWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titlesList = symbolsManager.GetTitlesWithPrice(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                CardMilitary militaryManager = new CardMilitary();
                totalRecord = militaryManager.GetCardMilitaryWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> militaryList = militaryManager.GetCardMilitaryWithPrice(subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                CardSpell spellManager = new CardSpell();
                totalRecord = spellManager.GetCardSpellWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> spellList = spellManager.GetCardSpellWithPrice(subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                MagicFormationCircle magicFormationCircleManager = new MagicFormationCircle();
                totalRecord = magicFormationCircleManager.GetMagicFormationCircleWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleManager.GetMagicFormationCircleWithPrice(subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                Relics relicsManager = new Relics();
                totalRecord = relicsManager.GetRelicsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relicsList = relicsManager.GetRelicsWithPrice(subType, pageSize, offset);
                createRelics(relicsList);
            }
            else if (mainType.Equals("Borders"))
            {
                Borders bordersManager = new Borders();
                totalRecord = bordersManager.GetBordersWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Borders> borders = bordersManager.GetBordersWithPrice(pageSize, offset);
                createBorders(borders);
            }
            else if (mainType.Equals("Achievements"))
            {
                Achievements achievementsManager = new Achievements();
                totalRecord = achievementsManager.GetAchievementsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Achievements> achievements = achievementsManager.GetAchievementsWithPrice(pageSize, offset);
                createAchievements(achievements);
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
            ShowCardHeroDetails(card);
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
    private void ShowCardHeroDetails(CardHeroes card)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
    private void ShowBookDetails(Books book)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
    private void ShowCardCaptainDetails(CardCaptains captains)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
        Transform elementPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/NumberDetail/ElementDetails/Scroll View/Viewport/Content");
        Transform descriptionPopupPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content/DescriptionDetail");

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
            if (!property.Name.Equals("id") && !property.Name.Equals("sequence") && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block") && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name") && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type") && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
