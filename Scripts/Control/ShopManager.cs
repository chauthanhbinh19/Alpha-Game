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
    private Transform currencyPanel;
    private GameObject ShopButtonPrefab;
    private GameObject ShopManagerPrefab;
    private GameObject currentObject;
    private GameObject ShopPrefab;
    private GameObject buttonPrefab;
    private GameObject equipmentsShopPrefab;
    private GameObject MainMenuDetailPanelPrefab;
    private GameObject ElementDetailsPrefab;
    private GameObject NumberDetailPrefab;
    private Transform popupPanel;
    private GameObject quantityPopupPrefab;
    private GameObject ReceivedNotification;
    private GameObject ItemThird;
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
        NumberDetailPrefab = UIManager.Instance.GetGameObject("NumberDetailPrefab");
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
        quantityPopupPrefab = UIManager.Instance.GetGameObject("quantityPopupPrefab");
        ReceivedNotification = UIManager.Instance.GetGameObject("ReceivedNotification");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
        AssignButtonEvent("Button_31", SummonMainMenuPanel, () => CreateShopButton());
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
        CreateButton(19, "Card Colonels", Resources.Load<Texture2D>($"UI/Button/teachings_of_conflict"), tempContent);
        CreateButton(20, "Card Generals", Resources.Load<Texture2D>($"UI/Button/teachings_of_contention"), tempContent);
        CreateButton(21, "Card Admirals", Resources.Load<Texture2D>($"UI/Button/teachings_of_diligence"), tempContent);
        CreateButton(22, "Talisman", Resources.Load<Texture2D>($"UI/Button/TalismanGallery"), tempContent);
        CreateButton(23, "Puppet", Resources.Load<Texture2D>($"UI/Button/PuppetGallery"), tempContent);
        CreateButton(24, "Alchemy", Resources.Load<Texture2D>($"UI/Button/AlchemyGallery"), tempContent);
        CreateButton(25, "Forge", Resources.Load<Texture2D>($"UI/Button/ForgeGallery"), tempContent);
        CreateButton(26, "Card Life", Resources.Load<Texture2D>($"UI/Button/LifeGallery"), tempContent);

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
        AssignButtonEvent("Button_19", tempContent, () => GetType("CardColonels"));
        AssignButtonEvent("Button_20", tempContent, () => GetType("CardGenerals"));
        AssignButtonEvent("Button_21", tempContent, () => GetType("CardAdmirals"));
        AssignButtonEvent("Button_22", tempContent, () => GetType("Talisman"));
        AssignButtonEvent("Button_23", tempContent, () => GetType("Puppet"));
        AssignButtonEvent("Button_24", tempContent, () => GetType("Alchemy"));
        AssignButtonEvent("Button_25", tempContent, () => GetType("Forge"));
        AssignButtonEvent("Button_26", tempContent, () => GetType("CardLife"));
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
        titleText.text = string.Concat(type.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " Shop"; // Cập nhật tiêu đề
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
        else if (mainType.Equals("Talisman"))
        {
            return Talisman.GetUniqueTalismanTypes();
        }
        else if (mainType.Equals("Puppet"))
        {
            return Puppet.GetUniquePuppetTypes();
        }
        else if (mainType.Equals("Alchemy"))
        {
            return Alchemy.GetUniqueAlchemyTypes();
        }
        else if (mainType.Equals("Forge"))
        {
            return Forge.GetUniqueForgeTypes();
        }
        else if (mainType.Equals("CardLife"))
        {
            return CardLife.GetUniqueCardLifeTypes();
        }
        return new List<string>();
    }
    public void GetButtonType()
    {
        // DictionaryPanel.SetActive(true);
        GameObject equipmentObject = Instantiate(ShopPrefab, MainPanel);
        currentContent = equipmentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TabButtonPanel = equipmentObject.transform.Find("Scroll View/Viewport/Content");
        currencyPanel = equipmentObject.transform.Find("DictionaryCards/Currency");
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

        // Transform CurrencyPanel = equipmentObject.transform.Find("DictionaryCards/Currency");
        // Currency currency = new Currency();
        // List<Currency> currencies = new List<Currency>();
        // currencies = currency.GetUserCurrency();
        // FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);

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
                    else if (mainType.Equals("CardColonels"))
                    {
                        CardColonels colonelsManager = new CardColonels();
                        List<CardColonels> colonels = colonelsManager.GetCardColonelsWithPrice(subtype, pageSize, offset);
                        createCardColonels(colonels);

                        totalRecord = colonelsManager.GetCardColonelsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardGenerals"))
                    {
                        CardGenerals generalsManager = new CardGenerals();
                        List<CardGenerals> relicsList = generalsManager.GetCardGeneralsWithPrice(subtype, pageSize, offset);
                        createCardGenerals(relicsList);

                        totalRecord = generalsManager.GetCardGeneralsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardAdmirals"))
                    {
                        CardAdmirals admiralsManager = new CardAdmirals();
                        List<CardAdmirals> relicsList = admiralsManager.GetCardAdmiralsWithPrice(subtype, pageSize, offset);
                        createCardAdmirals(relicsList);

                        totalRecord = admiralsManager.GetCardAdmiralsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Talisman"))
                    {
                        Talisman talismanManager = new Talisman();
                        List<Talisman> talismans = talismanManager.GetTalismanWithPrice(subType, pageSize, offset);
                        createTalisman(talismans);

                        totalRecord = talismanManager.GetTalismanWithPriceCount(subType);
                    }
                    else if (mainType.Equals("Puppet"))
                    {
                        Puppet puppetManager = new Puppet();
                        List<Puppet> puppets = puppetManager.GetPuppetWithPrice(subType, pageSize, offset);
                        createPuppet(puppets);

                        totalRecord = puppetManager.GetPuppetWithPriceCount(subType);
                    }
                    else if (mainType.Equals("Alchemy"))
                    {
                        Alchemy alchemyManager = new Alchemy();
                        List<Alchemy> alchemies = alchemyManager.GetAlchemyWithPrice(subType, pageSize, offset);
                        createAlchemy(alchemies);

                        totalRecord = alchemyManager.GetAlchemyWithPriceCount(subType);
                    }
                    else if (mainType.Equals("Forge"))
                    {
                        Forge forgeManager = new Forge();
                        List<Forge> forges = forgeManager.GetForgeWithPrice(subType, pageSize, offset);
                        createForge(forges);

                        totalRecord = forgeManager.GetForgeWithPriceCount(subType);
                    }
                    else if (mainType.Equals("CardLife"))
                    {
                        CardLife cardLifeManager = new CardLife();
                        List<CardLife> cardLives = cardLifeManager.GetCardLifeWithPrice(subType, pageSize, offset);
                        createCardLife(cardLives);

                        totalRecord = cardLifeManager.GetCardLifeWithPriceCount(subType);
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
        else if (mainType.Equals("CardColonels"))
        {
            CardColonels colonelsManager = new CardColonels();
            List<CardColonels> colonels = colonelsManager.GetCardColonelsWithPrice(type, pageSize, offset);
            createCardColonels(colonels);

            totalRecord = colonelsManager.GetCardColonelsWithPriceCount(type);
        }
        else if (mainType.Equals("CardGenerals"))
        {
            CardGenerals generalsManager = new CardGenerals();
            List<CardGenerals> relicsList = generalsManager.GetCardGeneralsWithPrice(type, pageSize, offset);
            createCardGenerals(relicsList);

            totalRecord = generalsManager.GetCardGeneralsWithPriceCount(type);
        }
        else if (mainType.Equals("CardAdmirals"))
        {
            CardAdmirals admiralsManager = new CardAdmirals();
            List<CardAdmirals> relicsList = admiralsManager.GetCardAdmiralsWithPrice(type, pageSize, offset);
            createCardAdmirals(relicsList);

            totalRecord = admiralsManager.GetCardAdmiralsWithPriceCount(type);
        }
        else if (mainType.Equals("Talisman"))
        {
            Talisman talismanManager = new Talisman();
            List<Talisman> talismans = talismanManager.GetTalismanWithPrice(type, pageSize, offset);
            createTalisman(talismans);

            totalRecord = talismanManager.GetTalismanWithPriceCount(type);
        }
        else if (mainType.Equals("Puppet"))
        {
            Puppet puppetManager = new Puppet();
            List<Puppet> puppets = puppetManager.GetPuppetWithPrice(type, pageSize, offset);
            createPuppet(puppets);

            totalRecord = puppetManager.GetPuppetWithPriceCount(type);
        }
        else if (mainType.Equals("Alchemy"))
        {
            Alchemy alchemyManager = new Alchemy();
            List<Alchemy> alchemies = alchemyManager.GetAlchemyWithPrice(type, pageSize, offset);
            createAlchemy(alchemies);

            totalRecord = alchemyManager.GetAlchemyWithPriceCount(type);
        }
        else if (mainType.Equals("Forge"))
        {
            Forge forgeManager = new Forge();
            List<Forge> forges = forgeManager.GetForgeWithPrice(type, pageSize, offset);
            createForge(forges);

            totalRecord = forgeManager.GetForgeWithPriceCount(type);
        }
        else if (mainType.Equals("CardLife"))
        {
            CardLife cardLifeManager = new CardLife();
            List<CardLife> cardLives = cardLifeManager.GetCardLifeWithPrice(type, pageSize, offset);
            createCardLife(cardLives);

            totalRecord = cardLifeManager.GetCardLifeWithPriceCount(type);
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
            RawImage FrameImage = cardObject.transform.Find("Frame").GetComponent<RawImage>();
            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(card, MainPanel));
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

            Button buy = cardObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(card.currency.quantity, card);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardHeroesCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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

            RawImage FrameImage = bookObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(book, MainPanel));
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

            Button buy = bookObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(book.currency.quantity, book);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetBooksCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = captainsObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(captain, MainPanel));
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

            Button buy = captainsObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(captain.currency.quantity, captain);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardCaptainsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = collaborationObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(collaboration, MainPanel));
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

            Button buy = collaborationObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(collaboration.currency.quantity, collaboration);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCollaborationsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = collaborationEquipmentObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(collaborationEquipment, MainPanel));
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

            Button buy = collaborationEquipmentObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(collaborationEquipment.currency.quantity, collaborationEquipment);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCollaborationEquipmentsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = equipmentObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(equipment, MainPanel));
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

            // Button buy = equipmentObject.transform.Find("Buy").GetComponent<Button>();
            // buy.onClick.AddListener(() =>
            // {
            //     GetQuantity(equipment.currency.quantity, e);
            // });
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
            RawImage FrameImage = medalObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(medal, MainPanel));
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

            Button buy = medalObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(medal.currency.quantity, medal);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetMedalsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = monstersObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(monster, MainPanel));
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

            Button buy = monstersObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(monster.currency.quantity, monster);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardMonstersCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = petsObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(pet, MainPanel));
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

            Button buy = petsObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(pet.currency.quantity, pet);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetPetsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = skillObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(skill, MainPanel));
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

            Button buy = skillObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(skill.currency.quantity, skill);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetSkillsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = symbolObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(symbol, MainPanel));
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

            Button buy = symbolObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(symbol.currency.quantity, symbol);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetSymbolsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = titleObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(title, MainPanel));
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

            Button buy = titleObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(title.currency.quantity, title);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetTitlesCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = militaryObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(military, MainPanel));
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

            Button buy = militaryObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(military.currency.quantity, military);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardMilitaryCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = spellObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(spell, MainPanel));
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

            Button buy = spellObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(spell.currency.quantity, spell);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardSpellCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = magicFormationCircleObject.transform.Find("Frame").GetComponent<RawImage>();
            // RawImage frameImage = magicFormationCircleObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(magicFormationCircle, MainPanel));
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

            Button buy = magicFormationCircleObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(magicFormationCircle.currency.quantity, magicFormationCircle);
            });

        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetMagicFormationCircleCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = relicObject.transform.Find("Frame").GetComponent<RawImage>();
            // RawImage frameImage = relicObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(relic, MainPanel));
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

            Button buy = relicObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(relic.currency.quantity, relic);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetRelicsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = borderObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(border, MainPanel));
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

            Button buy = borderObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(border.currency.quantity, border);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetBordersCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            RawImage FrameImage = achievementObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(achievement, MainPanel));
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

            Button buy = achievementObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(achievement.currency.quantity, achievement);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetAchievementsCurrency();
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    private void createCardColonels(List<CardColonels> cardColonels)
    {
        foreach (var achievement in cardColonels)
        {
            GameObject achievementObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = achievementObject.transform.Find("Title").GetComponent<Text>();
            Title.text = achievement.name.Replace("_", " ");

            RawImage Image = achievementObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = achievement.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = achievementObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(achievement, MainPanel));
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

            Button buy = achievementObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(achievement.currency.quantity, achievement);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardColonelsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    private void createCardGenerals(List<CardGenerals> cardGenerals)
    {
        foreach (var achievement in cardGenerals)
        {
            GameObject achievementObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = achievementObject.transform.Find("Title").GetComponent<Text>();
            Title.text = achievement.name.Replace("_", " ");

            RawImage Image = achievementObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = achievement.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = achievementObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(achievement, MainPanel));
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

            Button buy = achievementObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(achievement.currency.quantity, achievement);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardGeneralsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    private void createCardAdmirals(List<CardAdmirals> cardAdmirals)
    {
        foreach (var achievement in cardAdmirals)
        {
            GameObject achievementObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = achievementObject.transform.Find("Title").GetComponent<Text>();
            Title.text = achievement.name.Replace("_", " ");

            RawImage Image = achievementObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = achievement.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = achievementObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(achievement, MainPanel));
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

            Button buy = achievementObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(achievement.currency.quantity, achievement);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardAdmiralsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    private void createTalisman(List<Talisman> talismans)
    {
        foreach (var talisman in talismans)
        {
            GameObject talismanObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = talismanObject.transform.Find("Title").GetComponent<Text>();
            Title.text = talisman.name.Replace("_", " ");

            RawImage Image = talismanObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = talisman.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = talismanObject.transform.Find("Frame").GetComponent<RawImage>();
            // RawImage frameImage = talismanObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(talisman, MainPanel));
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

            // RawImage rareImage = talismanObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{talisman.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = talismanObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = talisman.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = talismanObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = talisman.currency.quantity.ToString();

            Button buy = talismanObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(talisman.currency.quantity, talisman);
            });

        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetTalismanCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    private void createPuppet(List<Puppet> puppets)
    {
        foreach (var puppet in puppets)
        {
            GameObject puppetObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = puppetObject.transform.Find("Title").GetComponent<Text>();
            Title.text = puppet.name.Replace("_", " ");

            RawImage Image = puppetObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = puppet.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = puppetObject.transform.Find("Frame").GetComponent<RawImage>();
            // RawImage frameImage = puppetObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(puppet, MainPanel));
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

            // RawImage rareImage = puppetObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{puppet.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = puppetObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = puppet.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = puppetObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = puppet.currency.quantity.ToString();

            Button buy = puppetObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(puppet.currency.quantity, puppet);
            });

        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetPuppetCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    private void createAlchemy(List<Alchemy> alchemies)
    {
        foreach (var alchemy in alchemies)
        {
            GameObject alchemyObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = alchemyObject.transform.Find("Title").GetComponent<Text>();
            Title.text = alchemy.name.Replace("_", " ");

            RawImage Image = alchemyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = alchemy.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = alchemyObject.transform.Find("Frame").GetComponent<RawImage>();
            // RawImage frameImage = alchemyObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(alchemy, MainPanel));
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

            // RawImage rareImage = alchemyObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{alchemy.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = alchemyObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = alchemy.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = alchemyObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = alchemy.currency.quantity.ToString();

            Button buy = alchemyObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(alchemy.currency.quantity, alchemy);
            });

        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetAlchemyCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    private void createForge(List<Forge> forges)
    {
        foreach (var forge in forges)
        {
            GameObject forgeObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = forgeObject.transform.Find("Title").GetComponent<Text>();
            Title.text = forge.name.Replace("_", " ");

            RawImage Image = forgeObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = forge.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = forgeObject.transform.Find("Frame").GetComponent<RawImage>();
            // RawImage frameImage = forgeObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(forge, MainPanel));
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

            // RawImage rareImage = forgeObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{forge.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = forgeObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = forge.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = forgeObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = forge.currency.quantity.ToString();

            Button buy = forgeObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(forge.currency.quantity, forge);
            });

        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetForgeCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    private void createCardLife(List<CardLife> cards)
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
            RawImage FrameImage = cardObject.transform.Find("Frame").GetComponent<RawImage>();
            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(card, MainPanel));
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

            Button buy = cardObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(card.currency.quantity, card);
            });
        }
        Currency currency = new Currency();
        List<Currency> currencies = new List<Currency>();
        currencies = currency.GetCardLifeCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            else if (mainType.Equals("Talisman"))
            {
                Talisman talismanManager = new Talisman();
                totalRecord = talismanManager.GetTalismanWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = talismanManager.GetTalismanWithPrice(subType, pageSize, offset);
                createTalisman(talismans);
            }
            else if (mainType.Equals("Puppet"))
            {
                Puppet puppetManager = new Puppet();
                totalRecord = puppetManager.GetPuppetWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = puppetManager.GetPuppetWithPrice(subType, pageSize, offset);
                createPuppet(puppets);
            }
            else if (mainType.Equals("Alchemy"))
            {
                Alchemy alchemyManager = new Alchemy();
                totalRecord = alchemyManager.GetAlchemyWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = alchemyManager.GetAlchemyWithPrice(subType, pageSize, offset);
                createAlchemy(alchemies);
            }
            else if (mainType.Equals("Forge"))
            {
                Forge forgeManager = new Forge();
                totalRecord = forgeManager.GetForgeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = forgeManager.GetForgeWithPrice(subType, pageSize, offset);
                createForge(forges);
            }
            else if (mainType.Equals("CardLife"))
            {
                CardLife cardLifeManager = new CardLife();
                totalRecord = cardLifeManager.GetCardLifeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = cardLifeManager.GetCardLifeWithPrice(subType, pageSize, offset);
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
            else if (mainType.Equals("Talisman"))
            {
                Talisman talismanManager = new Talisman();
                totalRecord = talismanManager.GetTalismanWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = talismanManager.GetTalismanWithPrice(subType, pageSize, offset);
                createTalisman(talismans);
            }
            else if (mainType.Equals("Puppet"))
            {
                Puppet puppetManager = new Puppet();
                totalRecord = puppetManager.GetPuppetWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = puppetManager.GetPuppetWithPrice(subType, pageSize, offset);
                createPuppet(puppets);
            }
            else if (mainType.Equals("Alchemy"))
            {
                Alchemy alchemyManager = new Alchemy();
                totalRecord = alchemyManager.GetAlchemyWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = alchemyManager.GetAlchemyWithPrice(subType, pageSize, offset);
                createAlchemy(alchemies);
            }
            else if (mainType.Equals("Forge"))
            {
                Forge forgeManager = new Forge();
                totalRecord = forgeManager.GetForgeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = forgeManager.GetForgeWithPrice(subType, pageSize, offset);
                createForge(forges);
            }
            else if (mainType.Equals("CardLife"))
            {
                CardLife cardLifeManager = new CardLife();
                totalRecord = cardLifeManager.GetCardLifeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = cardLifeManager.GetCardLifeWithPrice(subType, pageSize, offset);
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
    public void GetQuantity(int price, object obj)
    {
        GameObject quantityObject = Instantiate(quantityPopupPrefab, popupPanel);

        Button increaseButton = quantityObject.transform.Find("IncreaseButton").GetComponent<Button>();
        Button decreaseButton = quantityObject.transform.Find("DecreaseButton").GetComponent<Button>();
        Button increase10Button = quantityObject.transform.Find("Increase10Button").GetComponent<Button>();
        Button decrease10Button = quantityObject.transform.Find("Decrease10Button").GetComponent<Button>();
        Button maxButton = quantityObject.transform.Find("MaxButton").GetComponent<Button>();
        Button minButton = quantityObject.transform.Find("MinButton").GetComponent<Button>();
        Button closeButton = quantityObject.transform.Find("CloseButton").GetComponent<Button>();
        Button confirmButton = quantityObject.transform.Find("Buy").GetComponent<Button>();
        TextMeshProUGUI quantityText = quantityObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        RawImage currencyImage = quantityObject.transform.Find("Price/CurrencyImage").GetComponent<RawImage>();
        TextMeshProUGUI priceText = quantityObject.transform.Find("Price/PriceText").GetComponent<TextMeshProUGUI>();
        RawImage equipmentImage = quantityObject.transform.Find("Image").GetComponent<RawImage>();

        // Lấy thuộc tính `Id` và `Image` từ object
        var idProperty = obj.GetType().GetProperty("id");
        var imageProperty = obj.GetType().GetProperty("image");
        var currencyProperty = obj.GetType().GetProperty("currency");
        Currency currency = new Currency();

        if (idProperty != null && imageProperty != null && currencyProperty != null)
        {
            int id = (int)idProperty.GetValue(obj);
            string image = (string)imageProperty.GetValue(obj);

            // Lấy đối tượng currency từ obj
            var currencyObject = currencyProperty.GetValue(obj);

            if (currencyObject != null)
            {
                // Lấy thuộc tính "image" từ currencyObject
                var currencyImageProperty = currencyObject.GetType().GetProperty("image");
                if (currencyImageProperty != null)
                {
                    string currencyImageValue = (string)currencyImageProperty.GetValue(currencyObject);

                    if (!string.IsNullOrEmpty(currencyImageValue))
                    {
                        string currencyFileNameWithoutExtension = currencyImageValue.Replace(".png", "");
                        Texture currencyTexture = Resources.Load<Texture>($"{currencyFileNameWithoutExtension}");
                        currencyImage.texture = currencyTexture;
                    }
                }
            }

            // Xử lý image của obj
            if (!string.IsNullOrEmpty(image))
            {
                string fileNameWithoutExtension = image.Replace(".png", "");
                Texture entityTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                equipmentImage.texture = entityTexture;
            }

            priceText.text = price.ToString();
        }

        else
        {
            Debug.LogError("Object không có thuộc tính Id hoặc Image");
        }

        priceText.text = price.ToString();
        double originPrice = price;
        increaseButton.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            currentQuantity++;
            price = originPrice * currentQuantity;
            quantityText.text = currentQuantity.ToString();
            priceText.text = price.ToString();
        });
        decreaseButton.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            if (currentQuantity > 1)
            {
                currentQuantity--;
                price = originPrice * currentQuantity;
                quantityText.text = currentQuantity.ToString();
                priceText.text = price.ToString();
            }
        });
        increase10Button.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            currentQuantity = currentQuantity + 10;
            price = originPrice * currentQuantity;
            quantityText.text = currentQuantity.ToString();
            priceText.text = price.ToString();
        });
        decrease10Button.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            if (currentQuantity > 10)
            {
                currentQuantity = currentQuantity - 10;
                price = originPrice * currentQuantity;
                quantityText.text = currentQuantity.ToString();
                priceText.text = price.ToString();
            }
        });
        maxButton.onClick.AddListener(() =>
        {
            Currency userCurrency = new Currency();
            if (obj is Equipments equipment)
            {
                // userCurrency = currency.GetUserCurrencyById(equipment.c);
            }
            else if (obj is CardHeroes cardHeroes)
            {
                userCurrency = currency.GetUserCurrencyById(cardHeroes.currency.id);
            }
            else if (obj is CardCaptains cardCaptains)
            {
                userCurrency = currency.GetUserCurrencyById(cardCaptains.currency.id);
            }
            else if (obj is CardColonels cardColonels)
            {
                userCurrency = currency.GetUserCurrencyById(cardColonels.currency.id);
            }
            else if (obj is CardGenerals cardGenerals)
            {
                userCurrency = currency.GetUserCurrencyById(cardGenerals.currency.id);
            }
            else if (obj is CardAdmirals cardAdmirals)
            {
                userCurrency = currency.GetUserCurrencyById(cardAdmirals.currency.id);
            }
            else if (obj is Books books)
            {
                userCurrency = currency.GetUserCurrencyById(books.currency.id);
            }
            else if (obj is CardMonsters cardMonsters)
            {
                userCurrency = currency.GetUserCurrencyById(cardMonsters.currency.id);
            }
            else if (obj is CardMilitary cardMilitary)
            {
                userCurrency = currency.GetUserCurrencyById(cardMilitary.currency.id);
            }
            else if (obj is CardSpell cardSpell)
            {
                userCurrency = currency.GetUserCurrencyById(cardSpell.currency.id);
            }
            else if (obj is Achievements achievements)
            {
                userCurrency = currency.GetUserCurrencyById(achievements.currency.id);
            }
            else if (obj is Borders borders)
            {
                userCurrency = currency.GetUserCurrencyById(borders.currency.id);
            }
            else if (obj is Collaboration collaboration)
            {
                userCurrency = currency.GetUserCurrencyById(collaboration.currency.id);
            }
            else if (obj is CollaborationEquipment collaborationEquipment)
            {
                userCurrency = currency.GetUserCurrencyById(collaborationEquipment.currency.id);
            }
            else if (obj is Titles titles)
            {
                userCurrency = currency.GetUserCurrencyById(titles.currency.id);
            }
            else if (obj is Symbols symbols)
            {
                userCurrency = currency.GetUserCurrencyById(symbols.currency.id);
            }
            else if (obj is Medals medals)
            {
                userCurrency = currency.GetUserCurrencyById(medals.currency.id);
            }
            else if (obj is MagicFormationCircle magicFormationCircle)
            {
                userCurrency = currency.GetUserCurrencyById(magicFormationCircle.currency.id);
            }
            else if (obj is Relics relics)
            {
                userCurrency = currency.GetUserCurrencyById(relics.currency.id);
            }
            else if (obj is Pets pets)
            {
                userCurrency = currency.GetUserCurrencyById(pets.currency.id);
            }
            else if (obj is Skills skill)
            {
                userCurrency = currency.GetUserCurrencyById(skill.currency.id);
            }
            else if (obj is Talisman talisman)
            {
                userCurrency = currency.GetUserCurrencyById(talisman.currency.id);
            }
            else if (obj is Puppet puppet)
            {
                userCurrency = currency.GetUserCurrencyById(puppet.currency.id);
            }
            else if (obj is Alchemy alchemy)
            {
                userCurrency = currency.GetUserCurrencyById(alchemy.currency.id);
            }
            else if (obj is Forge forge)
            {
                userCurrency = currency.GetUserCurrencyById(forge.currency.id);
            }
            // double price = double.Parse(priceText.text);

            int max = (int)(userCurrency.quantity / price);
            double newprice = originPrice * max;
            quantityText.text = max.ToString();
            priceText.text = newprice.ToString();
        });
        minButton.onClick.AddListener(() =>
        {
            quantityText.text = "1";
            double price = double.Parse(priceText.text);
            price = originPrice * 1;
            priceText.text = price.ToString();
        });
        closeButton.onClick.AddListener(() => Close(popupPanel));
        confirmButton.onClick.AddListener(() =>
        {
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            for (int i = 1; i <= quantity; i++) // Duyệt từ 1 đến giá trị trong quantityText
            {
                if (obj is Equipments equipment)
                {
                    equipment.UpdateUserCurrency(equipment.id);
                    bool success = equipment.BuyEquipment((int)idProperty.GetValue(obj));
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardHeroes cardHeroes)
                {
                    currency.UpdateUserCurrency(cardHeroes.currency.id, price);
                    bool success = cardHeroes.InsertUserCardHeroes(cardHeroes);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardCaptains cardCaptains)
                {
                    currency.UpdateUserCurrency(cardCaptains.currency.id, price);
                    bool success = cardCaptains.InsertUserCardCaptains(cardCaptains);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardColonels cardColonels)
                {
                    currency.UpdateUserCurrency(cardColonels.currency.id, price);
                    bool success = cardColonels.InsertUserCardColonels(cardColonels);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardGenerals cardGenerals)
                {
                    currency.UpdateUserCurrency(cardGenerals.currency.id, price);
                    bool success = cardGenerals.InsertUserCardGenerals(cardGenerals);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardAdmirals cardAdmirals)
                {
                    currency.UpdateUserCurrency(cardAdmirals.currency.id, price);
                    bool success = cardAdmirals.InsertUserCardAdmirals(cardAdmirals);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Books books)
                {
                    currency.UpdateUserCurrency(books.currency.id, price);
                    bool success = books.InsertUserBooks(books);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardMonsters cardMonsters)
                {
                    currency.UpdateUserCurrency(cardMonsters.currency.id, price);
                    bool success = cardMonsters.InsertUserCardMonsters(cardMonsters);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardMilitary cardMilitary)
                {
                    currency.UpdateUserCurrency(cardMilitary.currency.id, price);
                    bool success = cardMilitary.InsertUserCardMilitary(cardMilitary);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardSpell cardSpell)
                {
                    currency.UpdateUserCurrency(cardSpell.currency.id, price);
                    bool success = cardSpell.InsertUserCardSpell(cardSpell);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Achievements achievements)
                {
                    currency.UpdateUserCurrency(achievements.currency.id, price);
                    bool success = achievements.InsertUserAchievements(achievements);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Borders borders)
                {
                    currency.UpdateUserCurrency(borders.currency.id, price);
                    bool success = borders.InsertUserBorders(borders);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Collaboration collaboration)
                {
                    currency.UpdateUserCurrency(collaboration.currency.id, price);
                    bool success = collaboration.InsertUserCollaborations(collaboration);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CollaborationEquipment collaborationEquipment)
                {
                    currency.UpdateUserCurrency(collaborationEquipment.currency.id, price);
                    bool success = collaborationEquipment.InsertUserCollaborationEquipments(collaborationEquipment);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Titles titles)
                {
                    currency.UpdateUserCurrency(titles.currency.id, price);
                    bool success = titles.InsertUserTitles(titles);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Symbols symbols)
                {
                    currency.UpdateUserCurrency(symbols.currency.id, price);
                    bool success = symbols.InsertUserSymbols(symbols);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Medals medals)
                {
                    currency.UpdateUserCurrency(medals.currency.id, price);
                    bool success = medals.InsertUserMedals(medals);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is MagicFormationCircle magicFormationCircle)
                {
                    currency.UpdateUserCurrency(magicFormationCircle.currency.id, price);
                    bool success = magicFormationCircle.InsertUserMacgicFormationCircle(magicFormationCircle);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Relics relics)
                {
                    currency.UpdateUserCurrency(relics.currency.id, price);
                    bool success = relics.InsertUserReclis(relics);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Pets pets)
                {
                    currency.UpdateUserCurrency(pets.currency.id, price);
                    bool success = pets.InsertUserPets(pets);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Skills skill)
                {
                    currency.UpdateUserCurrency(skill.currency.id, price);
                    bool success = skill.InsertUserSkills(skill);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Talisman talisman)
                {
                    currency.UpdateUserCurrency(talisman.currency.id, price);
                    bool success = talisman.InsertUserTalisman(talisman);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Puppet puppet)
                {
                    currency.UpdateUserCurrency(puppet.currency.id, price);
                    bool success = puppet.InsertUserPuppet(puppet);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Alchemy alchemy)
                {
                    currency.UpdateUserCurrency(alchemy.currency.id, price);
                    bool success = alchemy.InsertUserAlchemy(alchemy);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Forge forge)
                {
                    currency.UpdateUserCurrency(forge.currency.id, price);
                    bool success = forge.InsertUserForge(forge);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardLife cardLife)
                {
                    currency.UpdateUserCurrency(cardLife.currency.id, price);
                    bool success = cardLife.InsertUserCardLife(cardLife);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
            }

            // Hiển thị thông báo dựa trên kết quả
            if (allSuccess)
            {
                String fileNameWithoutExtension = "";
                Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
                List<Currency> currencies = new List<Currency>();
                string objType = "";
                if (obj is Equipments equipment)
                {
                    equipment.InsertEquipmentsGallery(equipment.id);
                    FindObjectOfType<CurrencyManager>().GetEquipmentsCurrency(subType, CurrencyPanel);
                    fileNameWithoutExtension = equipment.image.Replace(".png", "");
                }
                else if (obj is CardHeroes cardHeroes)
                {
                    cardHeroes.InsertCardHeroesGallery(cardHeroes.id);
                    currencies = currency.GetCardHeroesCurrency(subType);
                    fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
                }
                else if (obj is CardCaptains cardCaptains)
                {
                    cardCaptains.InsertCardCaptainsGallery(cardCaptains.id);
                    currencies = currency.GetCardCaptainsCurrency(subType);
                    fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
                }
                else if (obj is CardColonels cardColonels)
                {
                    cardColonels.InsertCardColonelsGallery(cardColonels.id);
                    currencies = currency.GetCardColonelsCurrency(subType);
                    fileNameWithoutExtension = cardColonels.image.Replace(".png", "");
                }
                else if (obj is CardGenerals cardGenerals)
                {
                    cardGenerals.InsertCardGeneralsGallery(cardGenerals.id);
                    currencies = currency.GetCardGeneralsCurrency(subType);
                    fileNameWithoutExtension = cardGenerals.image.Replace(".png", "");
                }
                else if (obj is CardAdmirals cardAdmirals)
                {
                    cardAdmirals.InsertCardAdmiralsGallery(cardAdmirals.id);
                    currencies = currency.GetCardAdmiralsCurrency(subType);
                    fileNameWithoutExtension = cardAdmirals.image.Replace(".png", "");
                }
                else if (obj is Books books)
                {
                    books.InsertBooksGallery(books.id);
                    currencies = currency.GetBooksCurrency(subType);
                    fileNameWithoutExtension = books.image.Replace(".png", "");
                }
                else if (obj is CardMonsters cardMonsters)
                {
                    cardMonsters.InsertCardMonstersGallery(cardMonsters.id);
                    currencies = currency.GetCardMonstersCurrency(subType);
                    fileNameWithoutExtension = cardMonsters.image.Replace(".png", "");
                }
                else if (obj is CardMilitary cardMilitary)
                {
                    cardMilitary.InsertCardMilitaryGallery(cardMilitary.id);
                    currencies = currency.GetCardMilitaryCurrency(subType);
                    fileNameWithoutExtension = cardMilitary.image.Replace(".png", "");
                }
                else if (obj is CardSpell cardSpell)
                {
                    cardSpell.InsertCardSpellGallery(cardSpell.id);
                    currencies = currency.GetCardMilitaryCurrency(subType);
                    fileNameWithoutExtension = cardSpell.image.Replace(".png", "");
                }
                else if (obj is Achievements achievements)
                {
                    // achievements.InsertUserAchievements(achievements);
                    currencies = currency.GetAchievementsCurrency();
                    fileNameWithoutExtension = achievements.image.Replace(".png", "");
                    objType = "Achievements";
                }
                else if (obj is Borders borders)
                {
                    borders.InsertBordersGallery(borders.id);
                    currencies = currency.GetBooksCurrency(subType);
                    fileNameWithoutExtension = borders.image.Replace(".png", "");
                    objType = "Borders";
                }
                else if (obj is Collaboration collaboration)
                {
                    collaboration.InsertCollaborationsGallery(collaboration.id);
                    currencies = currency.GetCardMilitaryCurrency(subType);
                    fileNameWithoutExtension = collaboration.image.Replace(".png", "");
                    objType = "Collaboration";
                }
                else if (obj is CollaborationEquipment collaborationEquipment)
                {
                    collaborationEquipment.InsertCollaborationEquipmentsGallery(collaborationEquipment.id);
                    currencies = currency.GetCollaborationEquipmentsCurrency(subType);
                    fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", "");
                    objType = "CollaborationEquipment";
                }
                else if (obj is Titles titles)
                {
                    titles.InsertTitlesGallery(titles.id);
                    currencies = currency.GetTitlesCurrency(subType);
                    fileNameWithoutExtension = titles.image.Replace(".png", "");
                    objType = "Titles";
                }
                else if (obj is Symbols symbols)
                {
                    symbols.InsertSymbolsGallery(symbols.id);
                    currencies = currency.GetSymbolsCurrency(subType);
                    fileNameWithoutExtension = symbols.image.Replace(".png", "");
                    objType = "Symbols";
                }
                else if (obj is Medals medals)
                {
                    medals.InsertMedalsGallery(medals.id);
                    currencies = currency.GetMedalsCurrency(subType);
                    fileNameWithoutExtension = medals.image.Replace(".png", "");
                    objType = "Medals";
                }
                else if (obj is MagicFormationCircle magicFormationCircle)
                {
                    magicFormationCircle.InsertMagicFormationCircleGallery(magicFormationCircle.id);
                    currencies = currency.GetMagicFormationCircleCurrency(subType);
                    fileNameWithoutExtension = magicFormationCircle.image.Replace(".png", "");
                }
                else if (obj is Relics relics)
                {
                    relics.InsertRelicsGallery(relics.id);
                    currencies = currency.GetRelicsCurrency(subType);
                    fileNameWithoutExtension = relics.image.Replace(".png", "");
                }
                else if (obj is Pets pets)
                {
                    pets.InsertPetsGallery(pets.id);
                    currencies = currency.GetPetsCurrency(subType);
                    fileNameWithoutExtension = pets.image.Replace(".png", "");
                }
                else if (obj is Skills skill)
                {
                    skill.InsertSkillsGallery(skill.id);
                    currencies = currency.GetSkillsCurrency(subType);
                    fileNameWithoutExtension = skill.image.Replace(".png", "");
                }
                else if (obj is Talisman talisman)
                {
                    talisman.InsertTalismanGallery(talisman.id);
                    currencies = currency.GetTalismanCurrency(subType);
                    fileNameWithoutExtension = talisman.image.Replace(".png", "");
                }
                else if (obj is Puppet puppet)
                {
                    puppet.InsertPuppetGallery(puppet.id);
                    currencies = currency.GetSkillsCurrency(subType);
                    fileNameWithoutExtension = puppet.image.Replace(".png", "");
                }
                else if (obj is Alchemy alchemy)
                {
                    alchemy.InsertAlchemyGallery(alchemy.id);
                    currencies = currency.GetSkillsCurrency(subType);
                    fileNameWithoutExtension = alchemy.image.Replace(".png", "");
                }
                else if (obj is Forge forge)
                {
                    forge.InsertForgeGallery(forge.id);
                    currencies = currency.GetSkillsCurrency(subType);
                    fileNameWithoutExtension = forge.image.Replace(".png", "");
                }
                else if (obj is CardLife cardLife)
                {
                    cardLife.InsertCardLifeGallery(cardLife.id);
                    currencies = currency.GetSkillsCurrency(subType);
                    fileNameWithoutExtension = cardLife.image.Replace(".png", "");
                }
                Close(CurrencyPanel);
                FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
                Close(popupPanel);
                // FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
                GameObject receivedNotificationObject = Instantiate(ReceivedNotification, popupPanel);

                AddCloseEvent(receivedNotificationObject);
                Transform itemContent = receivedNotificationObject.transform.Find("Scroll View/Viewport/Content");
                GameObject itemObject = Instantiate(ItemThird, itemContent);

                RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                eQuantity.text = quantity.ToString();

                if (objType.Equals("Achievements") || objType.Equals("Borders")
                || objType.Equals("Collaboration") || objType.Equals("CollaborationEquipment")
                || objType.Equals("Titles") || objType.Equals("Symbols") || objType.Equals("Medals")
                || objType.Equals("MagicFormationCircle") || objType.Equals("Talisman") || objType.Equals("Puppet")
                || objType.Equals("Alchemy") || objType.Equals("Forge") || objType.Equals("CardLife"))
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    powerManager.UpdateUserStats(User.CurrentUserId);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
            }
            else
            {
                FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
            }
        });
    }
    private void AddCloseEvent(GameObject obj)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) =>
        {
            Destroy(obj);
        });
        trigger.triggers.Add(entry);
    }
}
