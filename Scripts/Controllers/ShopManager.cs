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
        AssignButtonEvent("Button_36", SummonMainMenuPanel, () => CreateShopButton());
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

        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetUserCurrency();
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
        // 
        // List<Currency> currencies = new List<Currency>();
        // currencies = currency.GetUserCurrency();
        // FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);

        List<string> uniqueTypes = TypeManager.GetUniqueTypes(mainType);
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
                        List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroesWithPrice(subtype, pageSize, offset);
                        createCardHeroes(cards);

                        totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Books"))
                    {
                        List<Books> books = BooksService.Create().GetBooksWithPrice(subtype, pageSize, offset);
                        createBooks(books);

                        totalRecord = BooksService.Create().GetBookssWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardCaptains"))
                    {
                        List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptainsWithPrice(subtype, pageSize, offset);
                        createCardCaptains(captains);

                        totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CollaborationEquipments"))
                    {
                        List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(subtype, pageSize, offset);
                        createCollaborationEquipments(collaborationEquipments);

                        totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Equipments"))
                    {
                        List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subtype, pageSize, offset);
                        createEquipments(equipments);

                        totalRecord = EquipmentsService.Create().GetEquipmentsCount(subtype);
                    }
                    else if (mainType.Equals("Pets"))
                    {
                        Pets petsManager = new Pets();
                        List<Pets> pets = PetsService.Create().GetPetsWithPrice(subtype, pageSize, offset);
                        createPets(pets);

                        totalRecord = PetsService.Create().GetPetsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Skills"))
                    {
                        List<Skills> skills = SkillsService.Create().GetSkillsWithPrice(subtype, pageSize, offset);
                        createSkills(skills);

                        totalRecord = SkillsService.Create().GetSkillsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Symbols"))
                    {
                        List<Symbols> symbols = SymbolsService.Create().GetSymbolsWithPrice(subtype, pageSize, offset);
                        createSymbols(symbols);

                        totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardMilitary"))
                    {
                        List<CardMilitary> militaryList = CardMilitaryService.Create().GetCardMilitaryWithPrice(subtype, pageSize, offset);
                        createCardMilitary(militaryList);

                        totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(subType);
                    }
                    else if (mainType.Equals("CardSpell"))
                    {
                        List<CardSpell> spellList = CardSpellService.Create().GetCardSpellWithPrice(subtype, pageSize, offset);
                        createCardSpell(spellList);

                        totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(subType);
                    }
                    else if (mainType.Equals("MagicFormationCircle"))
                    {
                        List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(subtype, pageSize, offset);
                        createMagicFormationCircle(magicFormationCircles);

                        totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(subType);
                    }
                    else if (mainType.Equals("Relics"))
                    {
                        List<Relics> relicsList = RelicsService.Create().GetRelicsWithPrice(subtype, pageSize, offset);
                        createRelics(relicsList);

                        totalRecord = RelicsService.Create().GetRelicsWithPriceCount(subType);
                    }
                    else if (mainType.Equals("CardMonsters"))
                    {
                        List<CardMonsters> monstersList = CardMonstersService.Create().GetCardMonstersWithPrice(subtype, pageSize, offset);
                        createCardMonsters(monstersList);

                        totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardColonels"))
                    {
                        List<CardColonels> colonels = CardColonelsService.Create().GetCardColonelsWithPrice(subtype, pageSize, offset);
                        createCardColonels(colonels);

                        totalRecord = CardColonelsService.Create().GetCardColonelsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardGenerals"))
                    {
                        List<CardGenerals> relicsList = CardGeneralsService.Create().GetCardGeneralsWithPrice(subtype, pageSize, offset);
                        createCardGenerals(relicsList);

                        totalRecord = CardGeneralsService.Create().GetCardGeneralsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("CardAdmirals"))
                    {
                        List<CardAdmirals> relicsList = CardAdmiralsService.Create().GetCardAdmiralsWithPrice(subtype, pageSize, offset);
                        createCardAdmirals(relicsList);

                        totalRecord = CardAdmiralsService.Create().GetCardAdmiralsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals("Talisman"))
                    {
                        List<Talisman> talismans = TalismanService.Create().GetTalismanWithPrice(subType, pageSize, offset);
                        createTalisman(talismans);

                        totalRecord = TalismanService.Create().GetTalismanWithPriceCount(subType);
                    }
                    else if (mainType.Equals("Puppet"))
                    {
                        List<Puppet> puppets = PuppetService.Create().GetPuppetWithPrice(subType, pageSize, offset);
                        createPuppet(puppets);

                        totalRecord = PuppetService.Create().GetPuppetWithPriceCount(subType);
                    }
                    else if (mainType.Equals("Alchemy"))
                    {
                        List<Alchemy> alchemies = AlchemyService.Create().GetAlchemyWithPrice(subType, pageSize, offset);
                        createAlchemy(alchemies);

                        totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(subType);
                    }
                    else if (mainType.Equals("Forge"))
                    {
                        List<Forge> forges = ForgeService.Create().GetForgeWithPrice(subType, pageSize, offset);
                        createForge(forges);

                        totalRecord = ForgeService.Create().GetForgeWithPriceCount(subType);
                    }
                    else if (mainType.Equals("CardLife"))
                    {
                        List<CardLife> cardLives = CardLifeService.Create().GetCardLifeWithPrice(subType, pageSize, offset);
                        createCardLife(cardLives);

                        totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(subType);
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
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaborationWithPrice(pageSize, offset);
                createCollaboration(collaborations);

                totalRecord = CollaborationService.Create().GetCollaborationWithPriceCount();
            }
            else if (mainType.Equals("Medals"))
            {
                List<Medals> medalsList = MedalsService.Create().GetMedalsWithPrice(pageSize, offset);
                createMedals(medalsList);

                totalRecord = MedalsService.Create().GetMedalsWithPriceCount();
            }
            else if (mainType.Equals("Titles"))
            {
                List<Titles> titlesList = TitlesService.Create().GetTitlesWithPrice(pageSize, offset);
                createTitles(titlesList);

                totalRecord = TitlesService.Create().GetTitlesWithPriceCount();
            }
            else if (mainType.Equals("Borders"))
            {
                List<Borders> borders = BordersService.Create().GetBordersWithPrice(pageSize, offset);
                createBorders(borders);

                totalRecord = BordersService.Create().GetBordersWithPriceCount();
            }
            else if (mainType.Equals("Achievements"))
            {
                List<Achievements> achievements = AchievementsService.Create().GetAchievementsWithPrice(pageSize, offset);
                createAchievements(achievements);

                totalRecord = AchievementsService.Create().GetAchievementsWithPriceCount();
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
            List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroesWithPrice(type, pageSize, offset);
            createCardHeroes(cards);

            totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(type);
        }
        else if (mainType.Equals("Books"))
        {
            List<Books> books = BooksService.Create().GetBooksWithPrice(type, pageSize, offset);
            createBooks(books);

            totalRecord = BooksService.Create().GetBookssWithPriceCount(type);
        }
        else if (mainType.Equals("CardCaptains"))
        {
            List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptainsWithPrice(type, pageSize, offset);
            createCardCaptains(captains);

            totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(type);
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(type, pageSize, offset);
            createCollaborationEquipments(collaborationEquipments);

            totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(type);
        }
        else if (mainType.Equals("Equipments"))
        {
            List<Equipments> equipments = EquipmentsService.Create().GetEquipments(type, pageSize, offset);
            createEquipments(equipments);

            totalRecord = EquipmentsService.Create().GetEquipmentsCount(type);
        }
        else if (mainType.Equals("Pets"))
        {
            List<Pets> pets = PetsService.Create().GetPetsWithPrice(type, pageSize, offset);
            createPets(pets);

            totalRecord = PetsService.Create().GetPetsWithPriceCount(type);
        }
        else if (mainType.Equals("Skills"))
        {
            List<Skills> skills = SkillsService.Create().GetSkillsWithPrice(type, pageSize, offset);
            createSkills(skills);

            totalRecord = SkillsService.Create().GetSkillsWithPriceCount(type);
        }
        else if (mainType.Equals("Symbols"))
        {
            List<Symbols> symbols = SymbolsService.Create().GetSymbolsWithPrice(type, pageSize, offset);
            createSymbols(symbols);

            totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(type);
        }
        else if (mainType.Equals("CardMilitary"))
        {
            List<CardMilitary> militaryList = CardMilitaryService.Create().GetCardMilitaryWithPrice(type, pageSize, offset);
            createCardMilitary(militaryList);

            totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(type);
        }
        else if (mainType.Equals("CardSpell"))
        {
            List<CardSpell> spellList = CardSpellService.Create().GetCardSpellWithPrice(type, pageSize, offset);
            createCardSpell(spellList);

            totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(type);
        }
        else if (mainType.Equals("MagicFormationCircle"))
        {
            List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(type, pageSize, offset);
            createMagicFormationCircle(magicFormationCircles);

            totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(subType);
        }
        else if (mainType.Equals("Relics"))
        {
            List<Relics> relicsList = RelicsService.Create().GetRelicsWithPrice(type, pageSize, offset);
            createRelics(relicsList);

            totalRecord = RelicsService.Create().GetRelicsWithPriceCount(subType);
        }
        else if (mainType.Equals("CardMonsters"))
        {
            List<CardMonsters> monstersList = CardMonstersService.Create().GetCardMonstersWithPrice(type, pageSize, offset);
            createCardMonsters(monstersList);

            totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(type);
        }
        else if (mainType.Equals("CardColonels"))
        {
            List<CardColonels> colonels = CardColonelsService.Create().GetCardColonelsWithPrice(type, pageSize, offset);
            createCardColonels(colonels);

            totalRecord = CardColonelsService.Create().GetCardColonelsWithPriceCount(type);
        }
        else if (mainType.Equals("CardGenerals"))
        {
            List<CardGenerals> relicsList = CardGeneralsService.Create().GetCardGeneralsWithPrice(type, pageSize, offset);
            createCardGenerals(relicsList);

            totalRecord = CardGeneralsService.Create().GetCardGeneralsWithPriceCount(type);
        }
        else if (mainType.Equals("CardAdmirals"))
        {
            List<CardAdmirals> relicsList = CardAdmiralsService.Create().GetCardAdmiralsWithPrice(type, pageSize, offset);
            createCardAdmirals(relicsList);

            totalRecord = CardAdmiralsService.Create().GetCardAdmiralsWithPriceCount(type);
        }
        else if (mainType.Equals("Talisman"))
        {
            List<Talisman> talismans = TalismanService.Create().GetTalismanWithPrice(type, pageSize, offset);
            createTalisman(talismans);

            totalRecord = TalismanService.Create().GetTalismanWithPriceCount(type);
        }
        else if (mainType.Equals("Puppet"))
        {
            List<Puppet> puppets = PuppetService.Create().GetPuppetWithPrice(type, pageSize, offset);
            createPuppet(puppets);

            totalRecord = PuppetService.Create().GetPuppetWithPriceCount(type);
        }
        else if (mainType.Equals("Alchemy"))
        {
            List<Alchemy> alchemies = AlchemyService.Create().GetAlchemyWithPrice(type, pageSize, offset);
            createAlchemy(alchemies);

            totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(type);
        }
        else if (mainType.Equals("Forge"))
        {
            List<Forge> forges = ForgeService.Create().GetForgeWithPrice(type, pageSize, offset);
            createForge(forges);

            totalRecord = ForgeService.Create().GetForgeWithPriceCount(type);
        }
        else if (mainType.Equals("CardLife"))
        {
            List<CardLife> cardLives = CardLifeService.Create().GetCardLifeWithPrice(type, pageSize, offset);
            createCardLife(cardLives);

            totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(type);
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
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardHeroesCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetBooksCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardCaptainsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCollaborationsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCollaborationEquipmentsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetMedalsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardMonstersCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetPetsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetSymbolsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetTitlesCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardMilitaryCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardSpellCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetMagicFormationCircleCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetRelicsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetBordersCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetAchievementsCurrency();
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardColonelsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardGeneralsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardAdmiralsCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetTalismanCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetPuppetCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetAlchemyCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetForgeCurrency(subType);
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
        
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardLifeCurrency(subType);
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
                totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroesWithPrice(subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                totalRecord = BooksService.Create().GetBookssWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = BooksService.Create().GetBooksWithPrice(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> army = CardCaptainsService.Create().GetCardCaptainsWithPrice(subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaborations"))
            {
                totalRecord = CollaborationService.Create().GetCollaborationWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaboration = CollaborationService.Create().GetCollaborationWithPrice(pageSize, offset);
                createCollaboration(collaboration);
            }
            else if (mainType.Equals("Equipments"))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset);
                createEquipments(equipments);
            }
            else if (mainType.Equals("Medals"))
            {
                totalRecord = MedalsService.Create().GetMedalsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = MedalsService.Create().GetMedalsWithPrice(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> monstersList = CardMonstersService.Create().GetCardMonstersWithPrice(subType, pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                totalRecord = PetsService.Create().GetPetsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> petsList = PetsService.Create().GetPetsWithPrice(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                totalRecord = SkillsService.Create().GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skillsList = SkillsService.Create().GetSkillsWithPrice(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbolsList = SymbolsService.Create().GetSymbolsWithPrice(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                totalRecord = TitlesService.Create().GetTitlesWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titlesList = TitlesService.Create().GetTitlesWithPrice(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> militaryList = CardMilitaryService.Create().GetCardMilitaryWithPrice(subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> spellList = CardSpellService.Create().GetCardSpellWithPrice(subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                totalRecord = RelicsService.Create().GetRelicsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relicsList = RelicsService.Create().GetRelicsWithPrice(subType, pageSize, offset);
                createRelics(relicsList);
            }
            else if (mainType.Equals("Borders"))
            {
                totalRecord = BordersService.Create().GetBordersWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Borders> borders = BordersService.Create().GetBordersWithPrice(pageSize, offset);
                createBorders(borders);
            }
            else if (mainType.Equals("Achievements"))
            {
                totalRecord = AchievementsService.Create().GetAchievementsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Achievements> achievements = AchievementsService.Create().GetAchievementsWithPrice(pageSize, offset);
                createAchievements(achievements);
            }
            else if (mainType.Equals("Talisman"))
            {
                totalRecord = TalismanService.Create().GetTalismanWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalismanWithPrice(subType, pageSize, offset);
                createTalisman(talismans);
            }
            else if (mainType.Equals("Puppet"))
            {
                totalRecord = PuppetService.Create().GetPuppetWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppetWithPrice(subType, pageSize, offset);
                createPuppet(puppets);
            }
            else if (mainType.Equals("Alchemy"))
            {
                totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemyWithPrice(subType, pageSize, offset);
                createAlchemy(alchemies);
            }
            else if (mainType.Equals("Forge"))
            {
                totalRecord = ForgeService.Create().GetForgeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = ForgeService.Create().GetForgeWithPrice(subType, pageSize, offset);
                createForge(forges);
            }
            else if (mainType.Equals("CardLife"))
            {
                totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLifeWithPrice(subType, pageSize, offset);
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
                totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroesWithPrice(subType, pageSize, offset);
                createCardHeroes(cards);
            }
            else if (mainType.Equals("Books"))
            {
                totalRecord = BooksService.Create().GetBookssWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = BooksService.Create().GetBooksWithPrice(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> army = CardCaptainsService.Create().GetCardCaptainsWithPrice(subType, pageSize, offset);
                createCardCaptains(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaborations"))
            {
                totalRecord = CollaborationService.Create().GetCollaborationWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaboration = CollaborationService.Create().GetCollaborationWithPrice(pageSize, offset);
                createCollaboration(collaboration);
            }
            else if (mainType.Equals("Equipments"))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset);
                createEquipments(equipments);
            }
            else if (mainType.Equals("Medals"))
            {
                totalRecord = MedalsService.Create().GetMedalsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = MedalsService.Create().GetMedalsWithPrice(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monstersList = CardMonstersService.Create().GetCardMonstersWithPrice(subType, pageSize, offset);
                createCardMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                totalRecord = PetsService.Create().GetPetsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> petsList = PetsService.Create().GetPetsWithPrice(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                totalRecord = SkillsService.Create().GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skillsList = SkillsService.Create().GetSkillsWithPrice(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbolsList = SymbolsService.Create().GetSymbolsWithPrice(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                totalRecord = TitlesService.Create().GetTitlesWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titlesList = TitlesService.Create().GetTitlesWithPrice(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> militaryList = CardMilitaryService.Create().GetCardMilitaryWithPrice(subType, pageSize, offset);
                createCardMilitary(militaryList);
            }
            else if (mainType.Equals("CardSpell"))
            {
                totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> spellList = CardSpellService.Create().GetCardSpellWithPrice(subType, pageSize, offset);
                createCardSpell(spellList);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(subType, pageSize, offset);
                createMagicFormationCircle(magicFormationCircles);
            }
            else if (mainType.Equals("Relics"))
            {
                totalRecord = RelicsService.Create().GetRelicsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relicsList = RelicsService.Create().GetRelicsWithPrice(subType, pageSize, offset);
                createRelics(relicsList);
            }
            else if (mainType.Equals("Borders"))
            {
                totalRecord = BordersService.Create().GetBordersWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Borders> borders = BordersService.Create().GetBordersWithPrice(pageSize, offset);
                createBorders(borders);
            }
            else if (mainType.Equals("Achievements"))
            {
                totalRecord = AchievementsService.Create().GetAchievementsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Achievements> achievements = AchievementsService.Create().GetAchievementsWithPrice(pageSize, offset);
                createAchievements(achievements);
            }
            else if (mainType.Equals("Talisman"))
            {
                totalRecord = TalismanService.Create().GetTalismanWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalismanWithPrice(subType, pageSize, offset);
                createTalisman(talismans);
            }
            else if (mainType.Equals("Puppet"))
            {
                totalRecord = PuppetService.Create().GetPuppetWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppetWithPrice(subType, pageSize, offset);
                createPuppet(puppets);
            }
            else if (mainType.Equals("Alchemy"))
            {
                totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemyWithPrice(subType, pageSize, offset);
                createAlchemy(alchemies);
            }
            else if (mainType.Equals("Forge"))
            {
                Forge forgeManager = new Forge();
                totalRecord = ForgeService.Create().GetForgeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = ForgeService.Create().GetForgeWithPrice(subType, pageSize, offset);
                createForge(forges);
            }
            else if (mainType.Equals("CardLife"))
            {
                totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLifeWithPrice(subType, pageSize, offset);
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
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardHeroes.currency.id);
            }
            else if (obj is CardCaptains cardCaptains)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardCaptains.currency.id);
            }
            else if (obj is CardColonels cardColonels)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardColonels.currency.id);
            }
            else if (obj is CardGenerals cardGenerals)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardGenerals.currency.id);
            }
            else if (obj is CardAdmirals cardAdmirals)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardAdmirals.currency.id);
            }
            else if (obj is Books books)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(books.currency.id);
            }
            else if (obj is CardMonsters cardMonsters)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardMonsters.currency.id);
            }
            else if (obj is CardMilitary cardMilitary)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardMilitary.currency.id);
            }
            else if (obj is CardSpell cardSpell)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardSpell.currency.id);
            }
            else if (obj is Achievements achievements)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(achievements.currency.id);
            }
            else if (obj is Borders borders)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(borders.currency.id);
            }
            else if (obj is Collaboration collaboration)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(collaboration.currency.id);
            }
            else if (obj is CollaborationEquipment collaborationEquipment)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(collaborationEquipment.currency.id);
            }
            else if (obj is Titles titles)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(titles.currency.id);
            }
            else if (obj is Symbols symbols)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(symbols.currency.id);
            }
            else if (obj is Medals medals)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(medals.currency.id);
            }
            else if (obj is MagicFormationCircle magicFormationCircle)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(magicFormationCircle.currency.id);
            }
            else if (obj is Relics relics)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(relics.currency.id);
            }
            else if (obj is Pets pets)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(pets.currency.id);
            }
            else if (obj is Skills skill)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(skill.currency.id);
            }
            else if (obj is Talisman talisman)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(talisman.currency.id);
            }
            else if (obj is Puppet puppet)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(puppet.currency.id);
            }
            else if (obj is Alchemy alchemy)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(alchemy.currency.id);
            }
            else if (obj is Forge forge)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(forge.currency.id);
            }
            else if (obj is CardLife cardLife)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardLife.currency.id);
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
                    UserEquipmentsService.Create().UpdateUserCurrency(equipment.id);
                    bool success = UserEquipmentsService.Create().BuyEquipment(equipment.id);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardHeroes cardHeroes)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardHeroes.currency.id, price);
                    bool success = UserCardHeroesService.Create().InsertUserCardHeroes(cardHeroes);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardCaptains cardCaptains)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardCaptains.currency.id, price);
                    bool success = UserCardCaptainsService.Create().InsertUserCardCaptains(cardCaptains);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardColonels cardColonels)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardColonels.currency.id, price);
                    bool success = UserCardColonelsService.Create().InsertUserCardColonels(cardColonels);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardGenerals cardGenerals)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardGenerals.currency.id, price);
                    bool success = UserCardGeneralsService.Create().InsertUserCardGenerals(cardGenerals);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardAdmirals cardAdmirals)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardAdmirals.currency.id, price);
                    bool success = UserCardAdmiralsService.Create().InsertUserCardAdmirals(cardAdmirals);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Books books)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(books.currency.id, price);
                    bool success = UserBooksService.Create().InsertUserBooks(books);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardMonsters cardMonsters)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardMonsters.currency.id, price);
                    bool success = UserCardMonstersService.Create().InsertUserCardMonsters(cardMonsters);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardMilitary cardMilitary)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardMilitary.currency.id, price);
                    bool success = UserCardMilitaryService.Create().InsertUserCardMilitary(cardMilitary);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardSpell cardSpell)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardSpell.currency.id, price);
                    bool success = UserCardSpellService.Create().InsertUserCardSpell(cardSpell);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Achievements achievements)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(achievements.currency.id, price);
                    bool success = UserAchievementsService.Create().InsertUserAchievements(achievements);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Borders borders)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(borders.currency.id, price);
                    bool success = UserBordersService.Create().InsertUserBorders(borders);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Collaboration collaboration)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(collaboration.currency.id, price);
                    bool success = UserCollaborationService.Create().InsertUserCollaborations(collaboration);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CollaborationEquipment collaborationEquipment)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(collaborationEquipment.currency.id, price);
                    bool success = UserCollaborationEquipmentService.Create().InsertUserCollaborationEquipments(collaborationEquipment);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Titles titles)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(titles.currency.id, price);
                    bool success = UserTitlesService.Create().InsertUserTitles(titles);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Symbols symbols)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(symbols.currency.id, price);
                    bool success = UserSymbolsService.Create().InsertUserSymbols(symbols);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Medals medals)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(medals.currency.id, price);
                    bool success = UserMedalsService.Create().InsertUserMedals(medals);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is MagicFormationCircle magicFormationCircle)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(magicFormationCircle.currency.id, price);
                    bool success = UserMagicFormationCircleService.Create().InsertUserMagicFormationCircle(magicFormationCircle);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Relics relics)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(relics.currency.id, price);
                    bool success = UserRelicsService.Create().InsertUserReclis(relics);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Pets pets)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(pets.currency.id, price);
                    bool success = UserPetsService.Create().InsertUserPets(pets);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Skills skill)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(skill.currency.id, price);
                    bool success = UserSkillsService.Create().InsertUserSkills(skill);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Talisman talisman)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(talisman.currency.id, price);
                    bool success = UserTalismanService.Create().InsertUserTalisman(talisman);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Puppet puppet)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(puppet.currency.id, price);
                    bool success = UserPuppetService.Create().InsertUserPuppet(puppet);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Alchemy alchemy)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(alchemy.currency.id, price);
                    bool success = UserAlchemyService.Create().InsertUserAlchemy(alchemy);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is Forge forge)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(forge.currency.id, price);
                    bool success = UserForgeService.Create().InsertUserForge(forge);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
                else if (obj is CardLife cardLife)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardLife.currency.id, price);
                    bool success = UserCardLifeService.Create().InsertUserCardLife(cardLife);
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
                    EquipmentsGalleryService.Create().InsertEquipmentsGallery(equipment.id);
                    FindObjectOfType<CurrencyManager>().GetEquipmentsCurrency(subType, CurrencyPanel);
                    fileNameWithoutExtension = equipment.image.Replace(".png", "");
                }
                else if (obj is CardHeroes cardHeroes)
                {
                    CardHeroesGalleryService.Create().InsertCardHeroesGallery(cardHeroes.id);
                    currencies = UserCurrencyService.Create().GetCardHeroesCurrency(subType);
                    fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
                }
                else if (obj is CardCaptains cardCaptains)
                {
                    CardCaptainsGalleryService.Create().InsertCardCaptainsGallery(cardCaptains.id);
                    currencies = UserCurrencyService.Create().GetCardCaptainsCurrency(subType);
                    fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
                }
                else if (obj is CardColonels cardColonels)
                {
                    CardColonelsGalleryService.Create().InsertCardColonelsGallery(cardColonels.id);
                    currencies = UserCurrencyService.Create().GetCardColonelsCurrency(subType);
                    fileNameWithoutExtension = cardColonels.image.Replace(".png", "");
                }
                else if (obj is CardGenerals cardGenerals)
                {
                    CardGeneralsGalleryService.Create().InsertCardGeneralsGallery(cardGenerals.id);
                    currencies = UserCurrencyService.Create().GetCardGeneralsCurrency(subType);
                    fileNameWithoutExtension = cardGenerals.image.Replace(".png", "");
                }
                else if (obj is CardAdmirals cardAdmirals)
                {
                    CardAdmiralsGalleryService.Create().InsertCardAdmiralsGallery(cardAdmirals.id);
                    currencies = UserCurrencyService.Create().GetCardAdmiralsCurrency(subType);
                    fileNameWithoutExtension = cardAdmirals.image.Replace(".png", "");
                }
                else if (obj is Books books)
                {
                    BooksGalleryService.Create().InsertBooksGallery(books.id);
                    currencies = UserCurrencyService.Create().GetBooksCurrency(subType);
                    fileNameWithoutExtension = books.image.Replace(".png", "");
                }
                else if (obj is CardMonsters cardMonsters)
                {
                    CardMonstersGalleryService.Create().InsertCardMonstersGallery(cardMonsters.id);
                    currencies = UserCurrencyService.Create().GetCardMonstersCurrency(subType);
                    fileNameWithoutExtension = cardMonsters.image.Replace(".png", "");
                }
                else if (obj is CardMilitary cardMilitary)
                {
                    CardMilitaryGalleryService.Create().InsertCardMilitaryGallery(cardMilitary.id);
                    currencies = UserCurrencyService.Create().GetCardMilitaryCurrency(subType);
                    fileNameWithoutExtension = cardMilitary.image.Replace(".png", "");
                }
                else if (obj is CardSpell cardSpell)
                {
                    CardSpellGalleryService.Create().InsertCardSpellGallery(cardSpell.id);
                    currencies = UserCurrencyService.Create().GetCardMilitaryCurrency(subType);
                    fileNameWithoutExtension = cardSpell.image.Replace(".png", "");
                }
                else if (obj is Achievements achievements)
                {
                    // achievements.InsertUserAchievements(achievements);
                    currencies = UserCurrencyService.Create().GetAchievementsCurrency();
                    fileNameWithoutExtension = achievements.image.Replace(".png", "");
                    objType = "Achievements";
                }
                else if (obj is Borders borders)
                {
                    BordersGalleryService.Create().InsertBordersGallery(borders.id);
                    currencies = UserCurrencyService.Create().GetBooksCurrency(subType);
                    fileNameWithoutExtension = borders.image.Replace(".png", "");
                    objType = "Borders";
                }
                else if (obj is Collaboration collaboration)
                {
                    CollaborationGalleryService.Create().InsertCollaborationsGallery(collaboration.id);
                    currencies = UserCurrencyService.Create().GetCardMilitaryCurrency(subType);
                    fileNameWithoutExtension = collaboration.image.Replace(".png", "");
                    objType = "Collaboration";
                }
                else if (obj is CollaborationEquipment collaborationEquipment)
                {
                    CollaborationEquipmentGalleryService.Create().InsertCollaborationEquipmentsGallery(collaborationEquipment.id);
                    currencies = UserCurrencyService.Create().GetCollaborationEquipmentsCurrency(subType);
                    fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", "");
                    objType = "CollaborationEquipment";
                }
                else if (obj is Titles titles)
                {
                    TitlesGalleryService.Create().InsertTitlesGallery(titles.id);
                    currencies = UserCurrencyService.Create().GetTitlesCurrency(subType);
                    fileNameWithoutExtension = titles.image.Replace(".png", "");
                    objType = "Titles";
                }
                else if (obj is Symbols symbols)
                {
                    SymbolsGalleryService.Create().InsertSymbolsGallery(symbols.id);
                    currencies = UserCurrencyService.Create().GetSymbolsCurrency(subType);
                    fileNameWithoutExtension = symbols.image.Replace(".png", "");
                    objType = "Symbols";
                }
                else if (obj is Medals medals)
                {
                    MedalsGalleryService.Create().InsertMedalsGallery(medals.id);
                    currencies = UserCurrencyService.Create().GetMedalsCurrency(subType);
                    fileNameWithoutExtension = medals.image.Replace(".png", "");
                    objType = "Medals";
                }
                else if (obj is MagicFormationCircle magicFormationCircle)
                {
                    MagicFormationCircleGalleryService.Create().InsertMagicFormationCircleGallery(magicFormationCircle.id);
                    currencies = UserCurrencyService.Create().GetMagicFormationCircleCurrency(subType);
                    fileNameWithoutExtension = magicFormationCircle.image.Replace(".png", "");
                }
                else if (obj is Relics relics)
                {
                    RelicsGalleryService.Create().InsertRelicsGallery(relics.id);
                    currencies = UserCurrencyService.Create().GetRelicsCurrency(subType);
                    fileNameWithoutExtension = relics.image.Replace(".png", "");
                }
                else if (obj is Pets pets)
                {
                    PetsGalleryService.Create().InsertPetsGallery(pets.id);
                    currencies = UserCurrencyService.Create().GetPetsCurrency(subType);
                    fileNameWithoutExtension = pets.image.Replace(".png", "");
                }
                else if (obj is Skills skill)
                {
                    SkillsGalleryService.Create().InsertSkillsGallery(skill.id);
                    currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
                    fileNameWithoutExtension = skill.image.Replace(".png", "");
                }
                else if (obj is Talisman talisman)
                {
                    TalismanGalleryService.Create().InsertTalismanGallery(talisman.id);
                    currencies = UserCurrencyService.Create().GetTalismanCurrency(subType);
                    fileNameWithoutExtension = talisman.image.Replace(".png", "");
                }
                else if (obj is Puppet puppet)
                {
                    PuppetGalleryService.Create().InsertPuppetGallery(puppet.id);
                    currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
                    fileNameWithoutExtension = puppet.image.Replace(".png", "");
                }
                else if (obj is Alchemy alchemy)
                {
                    AlchemyGalleryService.Create().InsertAlchemyGallery(alchemy.id);
                    currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
                    fileNameWithoutExtension = alchemy.image.Replace(".png", "");
                }
                else if (obj is Forge forge)
                {
                    ForgeGalleryService.Create().InsertForgeGallery(forge.id);
                    currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
                    fileNameWithoutExtension = forge.image.Replace(".png", "");
                }
                else if (obj is CardLife cardLife)
                {
                    CardLifeGalleryService.Create().InsertCardLifeGallery(cardLife.id);
                    currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
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
                    double currentPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
                    PowerManagerService.Create().UpdateUserStats(User.CurrentUserId);
                    double newPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
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
