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
    private Transform popupPanel;
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
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
        AssignButtonEvent("Button_23", SummonMainMenuPanel, () => CreateShopButton());
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
        titleText.text = LocalizationManager.Get("shop");
        CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(currentObject));
        HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Destroy(currentObject));
        Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");

        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetUserCurrency();
        FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);

        Transform tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        CreateButton(1, AppConstants.CardHeroes, Resources.Load<Texture2D>($"UI/Button/CardsGallery"), tempContent);
        CreateButton(2, AppConstants.Books, Resources.Load<Texture2D>($"UI/Button/BooksGallery"), tempContent);
        CreateButton(3, AppConstants.Pets, Resources.Load<Texture2D>($"UI/Button/PetsGallery"), tempContent);
        CreateButton(4, AppConstants.CardCaptains, Resources.Load<Texture2D>($"UI/Button/CaptainsGallery"), tempContent);
        CreateButton(5, AppConstants.CollaborationEquipments, Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsGallery"), tempContent);
        CreateButton(6, AppConstants.CardMilitaries, Resources.Load<Texture2D>($"UI/Button/MilitaryGallery"), tempContent);
        CreateButton(7, AppConstants.CardSpells, Resources.Load<Texture2D>($"UI/Button/SpellGallery"), tempContent);
        CreateButton(8, AppConstants.Collaborations, Resources.Load<Texture2D>($"UI/Button/CollaborationsGallery"), tempContent);
        CreateButton(9, AppConstants.CardMonsters, Resources.Load<Texture2D>($"UI/Button/MonstersGallery"), tempContent);
        CreateButton(10, AppConstants.Borders, Resources.Load<Texture2D>($"UI/Button/BorderGallery"), tempContent);
        CreateButton(11, AppConstants.Medals, Resources.Load<Texture2D>($"UI/Button/MedalsGallery"), tempContent);
        CreateButton(12, AppConstants.Skills, Resources.Load<Texture2D>($"UI/Button/SkillsGallery"), tempContent);
        CreateButton(13, AppConstants.Symbols, Resources.Load<Texture2D>($"UI/Button/SymbolsGallery"), tempContent);
        CreateButton(14, AppConstants.Titles, Resources.Load<Texture2D>($"UI/Button/TitlesGallery"), tempContent);
        CreateButton(15, AppConstants.MagicFormationCircles, Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleGallery"), tempContent);
        CreateButton(16, AppConstants.Relics, Resources.Load<Texture2D>($"UI/Button/RelicsGallery"), tempContent);
        CreateButton(17, AppConstants.Items, Resources.Load<Texture2D>($"UI/Button/ItemsGallery"), tempContent);
        CreateButton(18, AppConstants.Alchievements, Resources.Load<Texture2D>($"UI/Button/AchievementGallery"), tempContent);
        CreateButton(19, AppConstants.CardColonels, Resources.Load<Texture2D>($"UI/Button/teachings_of_conflict"), tempContent);
        CreateButton(20, AppConstants.CardGenerals, Resources.Load<Texture2D>($"UI/Button/teachings_of_contention"), tempContent);
        CreateButton(21, AppConstants.CardAdmirals, Resources.Load<Texture2D>($"UI/Button/teachings_of_diligence"), tempContent);
        CreateButton(22, AppConstants.Talismans, Resources.Load<Texture2D>($"UI/Button/TalismanGallery"), tempContent);
        CreateButton(23, AppConstants.Puppets, Resources.Load<Texture2D>($"UI/Button/PuppetGallery"), tempContent);
        CreateButton(24, AppConstants.Alchemies, Resources.Load<Texture2D>($"UI/Button/AlchemyGallery"), tempContent);
        CreateButton(25, AppConstants.Forges, Resources.Load<Texture2D>($"UI/Button/ForgeGallery"), tempContent);
        CreateButton(26, AppConstants.CardLives, Resources.Load<Texture2D>($"UI/Button/LifeGallery"), tempContent);

        AssignButtonEvent("Button_1", tempContent, () => GetType(AppConstants.CardHero));
        AssignButtonEvent("Button_2", tempContent, () => GetType(AppConstants.Book));
        AssignButtonEvent("Button_3", tempContent, () => GetType(AppConstants.Pet));
        AssignButtonEvent("Button_4", tempContent, () => GetType(AppConstants.CardCaptain));
        AssignButtonEvent("Button_5", tempContent, () => GetType(AppConstants.CollaborationEquipment));
        AssignButtonEvent("Button_6", tempContent, () => GetType(AppConstants.CardMilitary));
        AssignButtonEvent("Button_7", tempContent, () => GetType(AppConstants.CardSpell));
        AssignButtonEvent("Button_8", tempContent, () => GetType(AppConstants.Collaboration));
        AssignButtonEvent("Button_9", tempContent, () => GetType(AppConstants.CardMonster));
        AssignButtonEvent("Button_10", tempContent, () => GetType(AppConstants.Border));
        AssignButtonEvent("Button_11", tempContent, () => GetType(AppConstants.Medal));
        AssignButtonEvent("Button_12", tempContent, () => GetType(AppConstants.Skill));
        AssignButtonEvent("Button_13", tempContent, () => GetType(AppConstants.Symbol));
        AssignButtonEvent("Button_14", tempContent, () => GetType(AppConstants.Title));
        AssignButtonEvent("Button_15", tempContent, () => GetType(AppConstants.MagicFormationCircle));
        AssignButtonEvent("Button_16", tempContent, () => GetType(AppConstants.Relic));
        AssignButtonEvent("Button_17", tempContent, () => GetType(AppConstants.Item));
        AssignButtonEvent("Button_18", tempContent, () => GetType(AppConstants.Alchievement));
        AssignButtonEvent("Button_19", tempContent, () => GetType(AppConstants.CardColonel));
        AssignButtonEvent("Button_20", tempContent, () => GetType(AppConstants.CardGeneral));
        AssignButtonEvent("Button_21", tempContent, () => GetType(AppConstants.CardAdmiral));
        AssignButtonEvent("Button_22", tempContent, () => GetType(AppConstants.Talisman));
        AssignButtonEvent("Button_23", tempContent, () => GetType(AppConstants.Puppet));
        AssignButtonEvent("Button_24", tempContent, () => GetType(AppConstants.Alchemy));
        AssignButtonEvent("Button_25", tempContent, () => GetType(AppConstants.Forge));
        AssignButtonEvent("Button_26", tempContent, () => GetType(AppConstants.CardLife));
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
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void GetType(string type)
    {
        mainType = type; // Gán giá trị cho mainType
        GetButtonType(); // Gọi hàm xử lý
        titleText.text = LocalizationManager.Get(type); // Cập nhật tiêu đề
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
                    ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_166");
                    int totalRecord = 0;
                    if (mainType.Equals(AppConstants.CardHero))
                    {
                        List<CardHeroes> cardHeroes = CardHeroesService.Create().GetCardHeroesWithPrice(subtype, pageSize, offset);
                        CardHeroesController.Instance.CreateCardHeroesTrade(cardHeroes, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Book))
                    {
                        List<Books> books = BooksService.Create().GetBooksWithPrice(subtype, pageSize, offset);
                        BooksController.Instance.CreateBooksTrade(books, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = BooksService.Create().GetBookssWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardCaptain))
                    {
                        List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptainsWithPrice(subtype, pageSize, offset);
                        CardCaptainsController.Instance.CreateCardCaptainsTrade(cardCaptains, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CollaborationEquipment))
                    {
                        List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(subtype, pageSize, offset);
                        CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsTrade(collaborationEquipments, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Equipment))
                    {
                        List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subtype, pageSize, offset);
                        createEquipments(equipments);

                        totalRecord = EquipmentsService.Create().GetEquipmentsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Pet))
                    {
                        Pets petsManager = new Pets();
                        List<Pets> pets = PetsService.Create().GetPetsWithPrice(subtype, pageSize, offset);
                        PetsController.Instance.CreatePetsTrade(pets, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = PetsService.Create().GetPetsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Skill))
                    {
                        List<Skills> skills = SkillsService.Create().GetSkillsWithPrice(subtype, pageSize, offset);
                        SkillsController.Instance.CreateSkillsTrade(skills, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = SkillsService.Create().GetSkillsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Symbol))
                    {
                        List<Symbols> symbols = SymbolsService.Create().GetSymbolsWithPrice(subtype, pageSize, offset);
                        SymbolsController.Instance.CreateSymbolsTrade(symbols, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardMilitary))
                    {
                        List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitaryWithPrice(subtype, pageSize, offset);
                        CardMilitaryController.Instance.CreateCardMilitaryTrade(cardMilitaries, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardSpell))
                    {
                        List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpellWithPrice(subtype, pageSize, offset);
                        CardSpellController.Instance.CreateCardSpellTrade(cardSpells, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.MagicFormationCircle))
                    {
                        List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(subtype, pageSize, offset);
                        MagicFormationCircleController.Instance.CreateMagicFormationCircleTrade(magicFormationCircles, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Relic))
                    {
                        List<Relics> relics = RelicsService.Create().GetRelicsWithPrice(subtype, pageSize, offset);
                        RelicsController.Instance.CreateRelicsTrade(relics, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = RelicsService.Create().GetRelicsWithPriceCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardMonster))
                    {
                        List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonstersWithPrice(subtype, pageSize, offset);
                        CardMonstersController.Instance.CreateCardMonstersTrade(cardMonsters, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardColonel))
                    {
                        List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonelsWithPrice(subtype, pageSize, offset);
                        CardColonelsController.Instance.CreateCardColonelsTrade(cardColonels, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardColonelsService.Create().GetCardColonelsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardGeneral))
                    {
                        List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGeneralsWithPrice(subtype, pageSize, offset);
                        CardGeneralsController.Instance.CreateCardGeneralsTrade(cardGenerals, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardGeneralsService.Create().GetCardGeneralsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardAdmiral))
                    {
                        List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmiralsWithPrice(subtype, pageSize, offset);
                        CardAdmiralsController.Instance.CreateCardAdmiralsTrade(cardAdmirals, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardAdmiralsService.Create().GetCardAdmiralsWithPriceCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Talisman))
                    {
                        List<Talisman> talismans = TalismanService.Create().GetTalismanWithPrice(subType, pageSize, offset);
                        TalismanController.Instance.CreateTalismanTrade(talismans, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = TalismanService.Create().GetTalismanWithPriceCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Puppet))
                    {
                        List<Puppet> puppets = PuppetService.Create().GetPuppetWithPrice(subType, pageSize, offset);
                        PuppetController.Instance.CreatePuppetTrade(puppets, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = PuppetService.Create().GetPuppetWithPriceCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Alchemy))
                    {
                        List<Alchemy> alchemies = AlchemyService.Create().GetAlchemyWithPrice(subType, pageSize, offset);
                        AlchemyController.Instance.CreateAlchemyTrade(alchemies, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Forge))
                    {
                        List<Forge> forges = ForgeService.Create().GetForgeWithPrice(subType, pageSize, offset);
                        ForgeController.Instance.CreateForgeTrade(forges, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = ForgeService.Create().GetForgeWithPriceCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardLife))
                    {
                        List<CardLife> cardLives = CardLifeService.Create().GetCardLifeWithPrice(subType, pageSize, offset);
                        CardLifeController.Instance.CreateCardLifeTrade(cardLives, subtype, currentContent, currencyPanel, popupPanel);

                        totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(subType);
                    }

                    totalPage = CalculateTotalPages(totalRecord, pageSize);
                    PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

                }
                else
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_167");
                }
            }
        }
        else
        {
            int totalRecord = 0;
            if (mainType.Equals(AppConstants.Collaboration))
            {
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaborationWithPrice(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationTrade(collaborations, subType, currentContent, currencyPanel, popupPanel);

                totalRecord = CollaborationService.Create().GetCollaborationWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                List<Medals> medals = MedalsService.Create().GetMedalsWithPrice(pageSize, offset);
                MedalsController.Instance.CreateMedalsTrade(medals, subType, currentContent, currencyPanel, popupPanel);

                totalRecord = MedalsService.Create().GetMedalsWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                List<Titles> titles = TitlesService.Create().GetTitlesWithPrice(pageSize, offset);
                TitlesController.Instance.CreateTitlesTrade(titles, subType, currentContent, currencyPanel, popupPanel);

                totalRecord = TitlesService.Create().GetTitlesWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                List<Borders> borders = BordersService.Create().GetBordersWithPrice(pageSize, offset);
                BordersController.Instance.CreateBordersTrade(borders, subType, currentContent, currencyPanel, popupPanel);

                totalRecord = BordersService.Create().GetBordersWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.Alchievement))
            {
                List<Achievements> achievements = AchievementsService.Create().GetAchievementsWithPrice(pageSize, offset);
                AchievementsController.Instance.CreateAchievementsTrade(achievements, subType, currentContent, currencyPanel, popupPanel);

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
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_167"); // Giả sử bạn có texture trắng
            }
        }

        subType = type;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, "Background_V4_166");
        int totalRecord = 0;

        if (mainType.Equals(AppConstants.CardHero))
        {
            List<CardHeroes> cardHeroes = CardHeroesService.Create().GetCardHeroesWithPrice(type, pageSize, offset);
            CardHeroesController.Instance.CreateCardHeroesTrade(cardHeroes, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.Book))
        {
            List<Books> books = BooksService.Create().GetBooksWithPrice(type, pageSize, offset);
            BooksController.Instance.CreateBooksTrade(books, type, currentContent, currencyPanel, popupPanel);

            totalRecord = BooksService.Create().GetBookssWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.CardCaptain))
        {
            List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptainsWithPrice(type, pageSize, offset);
            CardCaptainsController.Instance.CreateCardCaptainsTrade(cardCaptains, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.CollaborationEquipment))
        {
            List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(type, pageSize, offset);
            CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsTrade(collaborationEquipments, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.Equipment))
        {
            List<Equipments> equipments = EquipmentsService.Create().GetEquipments(type, pageSize, offset);
            createEquipments(equipments);

            totalRecord = EquipmentsService.Create().GetEquipmentsCount(type);
        }
        else if (mainType.Equals(AppConstants.Pet))
        {
            List<Pets> pets = PetsService.Create().GetPetsWithPrice(type, pageSize, offset);
            PetsController.Instance.CreatePetsTrade(pets, type, currentContent, currencyPanel, popupPanel);

            totalRecord = PetsService.Create().GetPetsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.Skill))
        {
            List<Skills> skills = SkillsService.Create().GetSkillsWithPrice(type, pageSize, offset);
            SkillsController.Instance.CreateSkillsTrade(skills, type, currentContent, currencyPanel, popupPanel);

            totalRecord = SkillsService.Create().GetSkillsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.Symbol))
        {
            List<Symbols> symbols = SymbolsService.Create().GetSymbolsWithPrice(type, pageSize, offset);
            SymbolsController.Instance.CreateSymbolsTrade(symbols, type, currentContent, currencyPanel, popupPanel);

            totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.CardMilitary))
        {
            List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitaryWithPrice(type, pageSize, offset);
            CardMilitaryController.Instance.CreateCardMilitaryTrade(cardMilitaries, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.CardSpell))
        {
            List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpellWithPrice(type, pageSize, offset);
            CardSpellController.Instance.CreateCardSpellTrade(cardSpells, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MagicFormationCircle))
        {
            List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(type, pageSize, offset);
            MagicFormationCircleController.Instance.CreateMagicFormationCircleTrade(magicFormationCircles, type, currentContent, currencyPanel, popupPanel);

            totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(subType);
        }
        else if (mainType.Equals(AppConstants.Relic))
        {
            List<Relics> relics = RelicsService.Create().GetRelicsWithPrice(type, pageSize, offset);
            RelicsController.Instance.CreateRelicsTrade(relics, type, currentContent, currencyPanel, popupPanel);

            totalRecord = RelicsService.Create().GetRelicsWithPriceCount(subType);
        }
        else if (mainType.Equals(AppConstants.CardMonster))
        {
            List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonstersWithPrice(type, pageSize, offset);
            CardMonstersController.Instance.CreateCardMonstersTrade(cardMonsters, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.CardColonel))
        {
            List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonelsWithPrice(type, pageSize, offset);
            CardColonelsController.Instance.CreateCardColonelsTrade(cardColonels, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardColonelsService.Create().GetCardColonelsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.CardGeneral))
        {
            List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGeneralsWithPrice(type, pageSize, offset);
            CardGeneralsController.Instance.CreateCardGeneralsTrade(cardGenerals, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardGeneralsService.Create().GetCardGeneralsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.CardAdmiral))
        {
            List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmiralsWithPrice(type, pageSize, offset);
            CardAdmiralsController.Instance.CreateCardAdmiralsTrade(cardAdmirals, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardAdmiralsService.Create().GetCardAdmiralsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.Talisman))
        {
            List<Talisman> talismans = TalismanService.Create().GetTalismanWithPrice(type, pageSize, offset);
            TalismanController.Instance.CreateTalismanTrade(talismans, type, currentContent, currencyPanel, popupPanel);

            totalRecord = TalismanService.Create().GetTalismanWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.Puppet))
        {
            List<Puppet> puppets = PuppetService.Create().GetPuppetWithPrice(type, pageSize, offset);
            PuppetController.Instance.CreatePuppetTrade(puppets, type, currentContent, currencyPanel, popupPanel);

            totalRecord = PuppetService.Create().GetPuppetWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.Alchemy))
        {
            List<Alchemy> alchemies = AlchemyService.Create().GetAlchemyWithPrice(type, pageSize, offset);
            AlchemyController.Instance.CreateAlchemyTrade(alchemies, type, currentContent, currencyPanel, popupPanel);

            totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.Forge))
        {
            List<Forge> forges = ForgeService.Create().GetForgeWithPrice(type, pageSize, offset);
            ForgeController.Instance.CreateForgeTrade(forges, type, currentContent, currencyPanel, popupPanel);

            totalRecord = ForgeService.Create().GetForgeWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.CardLife))
        {
            List<CardLife> cardLives = CardLifeService.Create().GetCardLifeWithPrice(type, pageSize, offset);
            CardLifeController.Instance.CreateCardLifeTrade(cardLives, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(type);
        }

        totalPage = CalculateTotalPages(totalRecord, pageSize);
        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        // Debug.Log($"Button for type '{type}' clicked!");
    }
    private void createEquipments(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipment.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = equipmentObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(equipment, MainPanel));
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

            if (mainType.Equals(AppConstants.CardHero))
            {
                totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cardHeroes = CardHeroesService.Create().GetCardHeroesWithPrice(subType, pageSize, offset);
                CardHeroesController.Instance.CreateCardHeroesTrade(cardHeroes, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {
                totalRecord = BooksService.Create().GetBookssWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = BooksService.Create().GetBooksWithPrice(subType, pageSize, offset);
                BooksController.Instance.CreateBooksTrade(books, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {
                totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptainsWithPrice(subType, pageSize, offset);
                CardCaptainsController.Instance.CreateCardCaptainsTrade(cardCaptains, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {
                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(subType, pageSize, offset);
                CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsTrade(collaborationEquipments, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                totalRecord = CollaborationService.Create().GetCollaborationWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaborationWithPrice(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationTrade(collaborations, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset);
                createEquipments(equipments);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                totalRecord = MedalsService.Create().GetMedalsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medals = MedalsService.Create().GetMedalsWithPrice(pageSize, offset);
                MedalsController.Instance.CreateMedalsTrade(medals, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {
                totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonstersWithPrice(subType, pageSize, offset);
                CardMonstersController.Instance.CreateCardMonstersTrade(cardMonsters, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {
                totalRecord = PetsService.Create().GetPetsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = PetsService.Create().GetPetsWithPrice(subType, pageSize, offset);
                PetsController.Instance.CreatePetsTrade(pets, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {
                totalRecord = SkillsService.Create().GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = SkillsService.Create().GetSkillsWithPrice(subType, pageSize, offset);
                SkillsController.Instance.CreateSkillsTrade(skills, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {
                totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = SymbolsService.Create().GetSymbolsWithPrice(subType, pageSize, offset);
                SymbolsController.Instance.CreateSymbolsTrade(symbols, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                totalRecord = TitlesService.Create().GetTitlesWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = TitlesService.Create().GetTitlesWithPrice(pageSize, offset);
                TitlesController.Instance.CreateTitlesTrade(titles, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {
                totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitaryWithPrice(subType, pageSize, offset);
                CardMilitaryController.Instance.CreateCardMilitaryTrade(cardMilitaries, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {
                totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpellWithPrice(subType, pageSize, offset);
                CardSpellController.Instance.CreateCardSpellTrade(cardSpells, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {
                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(subType, pageSize, offset);
                MagicFormationCircleController.Instance.CreateMagicFormationCircleTrade(magicFormationCircles, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {
                totalRecord = RelicsService.Create().GetRelicsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = RelicsService.Create().GetRelicsWithPrice(subType, pageSize, offset);
                RelicsController.Instance.CreateRelicsTrade(relics, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                totalRecord = BordersService.Create().GetBordersWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Borders> borders = BordersService.Create().GetBordersWithPrice(pageSize, offset);
                BordersController.Instance.CreateBordersTrade(borders, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Alchievement))
            {
                totalRecord = AchievementsService.Create().GetAchievementsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Achievements> achievements = AchievementsService.Create().GetAchievementsWithPrice(pageSize, offset);
                AchievementsController.Instance.CreateAchievementsTrade(achievements, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                totalRecord = CardColonelsService.Create().GetCardColonelsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonelsWithPrice(subType, pageSize, offset);
                CardColonelsController.Instance.CreateCardColonelsTrade(cardColonels, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                totalRecord = CardGeneralsService.Create().GetCardGeneralsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGeneralsWithPrice(subType, pageSize, offset);
                CardGeneralsController.Instance.CreateCardGeneralsTrade(cardGenerals, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                totalRecord = CardAdmiralsService.Create().GetCardAdmiralsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmiralsWithPrice(subType, pageSize, offset);
                CardAdmiralsController.Instance.CreateCardAdmiralsTrade(cardAdmirals, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                totalRecord = TalismanService.Create().GetTalismanWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalismanWithPrice(subType, pageSize, offset);
                TalismanController.Instance.CreateTalismanTrade(talismans, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                totalRecord = PuppetService.Create().GetPuppetWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppetWithPrice(subType, pageSize, offset);
                PuppetController.Instance.CreatePuppetTrade(puppets, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemyWithPrice(subType, pageSize, offset);
                AlchemyController.Instance.CreateAlchemyTrade(alchemies, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                totalRecord = ForgeService.Create().GetForgeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = ForgeService.Create().GetForgeWithPrice(subType, pageSize, offset);
                ForgeController.Instance.CreateForgeTrade(forges, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLifeWithPrice(subType, pageSize, offset);
                CardLifeController.Instance.CreateCardLifeTrade(cardLives, subType, currentContent, currencyPanel, popupPanel);
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
                totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cardHeroes = CardHeroesService.Create().GetCardHeroesWithPrice(subType, pageSize, offset);
                CardHeroesController.Instance.CreateCardHeroesTrade(cardHeroes, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {
                totalRecord = BooksService.Create().GetBookssWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = BooksService.Create().GetBooksWithPrice(subType, pageSize, offset);
                BooksController.Instance.CreateBooksTrade(books, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {
                totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptainsWithPrice(subType, pageSize, offset);
                CardCaptainsController.Instance.CreateCardCaptainsTrade(cardCaptains, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {
                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(subType, pageSize, offset);
                CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsTrade(collaborationEquipments, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                totalRecord = CollaborationService.Create().GetCollaborationWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaborationWithPrice(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationTrade(collaborations, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset);
                createEquipments(equipments);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                totalRecord = MedalsService.Create().GetMedalsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medals = MedalsService.Create().GetMedalsWithPrice(pageSize, offset);
                MedalsController.Instance.CreateMedalsTrade(medals, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {
                totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonstersWithPrice(subType, pageSize, offset);
                CardMonstersController.Instance.CreateCardMonstersTrade(cardMonsters, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {
                totalRecord = PetsService.Create().GetPetsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = PetsService.Create().GetPetsWithPrice(subType, pageSize, offset);
                PetsController.Instance.CreatePetsTrade(pets, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {
                totalRecord = SkillsService.Create().GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = SkillsService.Create().GetSkillsWithPrice(subType, pageSize, offset);
                SkillsController.Instance.CreateSkillsTrade(skills, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {
                totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = SymbolsService.Create().GetSymbolsWithPrice(subType, pageSize, offset);
                SymbolsController.Instance.CreateSymbolsTrade(symbols, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                totalRecord = TitlesService.Create().GetTitlesWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = TitlesService.Create().GetTitlesWithPrice(pageSize, offset);
                TitlesController.Instance.CreateTitlesTrade(titles, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {
                totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitaryWithPrice(subType, pageSize, offset);
                CardMilitaryController.Instance.CreateCardMilitaryTrade(cardMilitaries, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {
                totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpellWithPrice(subType, pageSize, offset);
                CardSpellController.Instance.CreateCardSpellTrade(cardSpells, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {
                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(subType, pageSize, offset);
                MagicFormationCircleController.Instance.CreateMagicFormationCircleTrade(magicFormationCircles, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {
                totalRecord = RelicsService.Create().GetRelicsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = RelicsService.Create().GetRelicsWithPrice(subType, pageSize, offset);
                RelicsController.Instance.CreateRelicsTrade(relics, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                totalRecord = BordersService.Create().GetBordersWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Borders> borders = BordersService.Create().GetBordersWithPrice(pageSize, offset);
                BordersController.Instance.CreateBordersTrade(borders, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Alchievement))
            {
                totalRecord = AchievementsService.Create().GetAchievementsWithPriceCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Achievements> achievements = AchievementsService.Create().GetAchievementsWithPrice(pageSize, offset);
                AchievementsController.Instance.CreateAchievementsTrade(achievements, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                totalRecord = CardColonelsService.Create().GetCardColonelsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonelsWithPrice(subType, pageSize, offset);
                CardColonelsController.Instance.CreateCardColonelsTrade(cardColonels, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                totalRecord = CardGeneralsService.Create().GetCardGeneralsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGeneralsWithPrice(subType, pageSize, offset);
                CardGeneralsController.Instance.CreateCardGeneralsTrade(cardGenerals, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                totalRecord = CardAdmiralsService.Create().GetCardAdmiralsWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmiralsWithPrice(subType, pageSize, offset);
                CardAdmiralsController.Instance.CreateCardAdmiralsTrade(cardAdmirals, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                totalRecord = TalismanService.Create().GetTalismanWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalismanWithPrice(subType, pageSize, offset);
                TalismanController.Instance.CreateTalismanTrade(talismans, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                totalRecord = PuppetService.Create().GetPuppetWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppetWithPrice(subType, pageSize, offset);
                PuppetController.Instance.CreatePuppetTrade(puppets, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemyWithPrice(subType, pageSize, offset);
                AlchemyController.Instance.CreateAlchemyTrade(alchemies, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                Forge forgeManager = new Forge();
                totalRecord = ForgeService.Create().GetForgeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = ForgeService.Create().GetForgeWithPrice(subType, pageSize, offset);
                ForgeController.Instance.CreateForgeTrade(forges, subType, currentContent, currencyPanel, popupPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLifeWithPrice(subType, pageSize, offset);
                CardLifeController.Instance.CreateCardLifeTrade(cardLives, subType, currentContent, currencyPanel, popupPanel);
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
    // public void GetQuantity(int price, object obj)
    // {
    //     GameObject quantityObject = Instantiate(quantityPopupPrefab, popupPanel);

    //     Button increaseButton = quantityObject.transform.Find("IncreaseButton").GetComponent<Button>();
    //     Button decreaseButton = quantityObject.transform.Find("DecreaseButton").GetComponent<Button>();
    //     Button increase10Button = quantityObject.transform.Find("Increase10Button").GetComponent<Button>();
    //     Button decrease10Button = quantityObject.transform.Find("Decrease10Button").GetComponent<Button>();
    //     Button maxButton = quantityObject.transform.Find("MaxButton").GetComponent<Button>();
    //     Button minButton = quantityObject.transform.Find("MinButton").GetComponent<Button>();
    //     Button closeButton = quantityObject.transform.Find("CloseButton").GetComponent<Button>();
    //     Button confirmButton = quantityObject.transform.Find("Buy").GetComponent<Button>();
    //     TextMeshProUGUI quantityText = quantityObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
    //     RawImage currencyImage = quantityObject.transform.Find("Price/CurrencyImage").GetComponent<RawImage>();
    //     TextMeshProUGUI priceText = quantityObject.transform.Find("Price/PriceText").GetComponent<TextMeshProUGUI>();
    //     RawImage equipmentImage = quantityObject.transform.Find("Image").GetComponent<RawImage>();

    //     // Lấy thuộc tính `Id` và `Image` từ object
    //     var idProperty = obj.GetType().GetProperty("id");
    //     var imageProperty = obj.GetType().GetProperty("image");
    //     var currencyProperty = obj.GetType().GetProperty("currency");


    //     if (idProperty != null && imageProperty != null && currencyProperty != null)
    //     {
    //         int id = (int)idProperty.GetValue(obj);
    //         string image = (string)imageProperty.GetValue(obj);

    //         // Lấy đối tượng currency từ obj
    //         var currencyObject = currencyProperty.GetValue(obj);

    //         if (currencyObject != null)
    //         {
    //             // Lấy thuộc tính "image" từ currencyObject
    //             var currencyImageProperty = currencyObject.GetType().GetProperty("image");
    //             if (currencyImageProperty != null)
    //             {
    //                 string currencyImageValue = (string)currencyImageProperty.GetValue(currencyObject);

    //                 if (!string.IsNullOrEmpty(currencyImageValue))
    //                 {
    //                     string currencyFileNameWithoutExtension = currencyImageValue.Replace(".png", "");
    //                     Texture currencyTexture = Resources.Load<Texture>($"{currencyFileNameWithoutExtension}");
    //                     currencyImage.texture = currencyTexture;
    //                 }
    //             }
    //         }

    //         // Xử lý image của obj
    //         if (!string.IsNullOrEmpty(image))
    //         {
    //             string fileNameWithoutExtension = image.Replace(".png", "");
    //             Texture entityTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
    //             equipmentImage.texture = entityTexture;
    //         }

    //         priceText.text = price.ToString();
    //     }

    //     else
    //     {
    //         Debug.LogError("Object không có thuộc tính Id hoặc Image");
    //     }

    //     priceText.text = price.ToString();
    //     double originPrice = price;
    //     increaseButton.onClick.AddListener(() =>
    //     {
    //         int currentQuantity = int.Parse(quantityText.text);
    //         double price = double.Parse(priceText.text);
    //         currentQuantity++;
    //         price = originPrice * currentQuantity;
    //         quantityText.text = currentQuantity.ToString();
    //         priceText.text = price.ToString();
    //     });
    //     decreaseButton.onClick.AddListener(() =>
    //     {
    //         int currentQuantity = int.Parse(quantityText.text);
    //         double price = double.Parse(priceText.text);
    //         if (currentQuantity > 1)
    //         {
    //             currentQuantity--;
    //             price = originPrice * currentQuantity;
    //             quantityText.text = currentQuantity.ToString();
    //             priceText.text = price.ToString();
    //         }
    //     });
    //     increase10Button.onClick.AddListener(() =>
    //     {
    //         int currentQuantity = int.Parse(quantityText.text);
    //         double price = double.Parse(priceText.text);
    //         currentQuantity = currentQuantity + 10;
    //         price = originPrice * currentQuantity;
    //         quantityText.text = currentQuantity.ToString();
    //         priceText.text = price.ToString();
    //     });
    //     decrease10Button.onClick.AddListener(() =>
    //     {
    //         int currentQuantity = int.Parse(quantityText.text);
    //         double price = double.Parse(priceText.text);
    //         if (currentQuantity > 10)
    //         {
    //             currentQuantity = currentQuantity - 10;
    //             price = originPrice * currentQuantity;
    //             quantityText.text = currentQuantity.ToString();
    //             priceText.text = price.ToString();
    //         }
    //     });
    //     maxButton.onClick.AddListener(() =>
    //     {
    //         Currency userCurrency = new Currency();
    //         if (obj is Equipments equipment)
    //         {
    //             // userCurrency = currency.GetUserCurrencyById(equipment.c);
    //         }
    //         // double price = double.Parse(priceText.text);

    //         int max = (int)(userCurrency.quantity / price);
    //         double newprice = originPrice * max;
    //         quantityText.text = max.ToString();
    //         priceText.text = newprice.ToString();
    //     });
    //     minButton.onClick.AddListener(() =>
    //     {
    //         quantityText.text = "1";
    //         double price = double.Parse(priceText.text);
    //         price = originPrice * 1;
    //         priceText.text = price.ToString();
    //     });
    //     closeButton.onClick.AddListener(() => Close(popupPanel));
    //     confirmButton.onClick.AddListener(() =>
    //     {
    //         int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
    //         bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

    //         for (int i = 1; i <= quantity; i++) // Duyệt từ 1 đến giá trị trong quantityText
    //         {
    //             if (obj is Equipments equipment)
    //             {
    //                 UserEquipmentsService.Create().UpdateUserCurrency(equipment.id);
    //                 bool success = UserEquipmentsService.Create().BuyEquipment(equipment.id);
    //                 if (!success)
    //                 {
    //                     allSuccess = false;
    //                     break;
    //                 }
    //             }

    //         }

    //         // Hiển thị thông báo dựa trên kết quả
    //         if (allSuccess)
    //         {
    //             String fileNameWithoutExtension = "";
    //             Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
    //             List<Currency> currencies = new List<Currency>();
    //             string objType = "";
    //             if (obj is Equipments equipment)
    //             {
    //                 EquipmentsGalleryService.Create().InsertEquipmentsGallery(equipment.id);
    //                 FindObjectOfType<CurrencyManager>().GetEquipmentsCurrency(subType, CurrencyPanel);
    //                 fileNameWithoutExtension = equipment.image.Replace(".png", "");
    //             }
    //             Close(CurrencyPanel);
    //             FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    //             Close(popupPanel);
    //             // FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
    //             GameObject receivedNotificationObject = Instantiate(ReceivedNotification, popupPanel);

    //             ButtonEvent.Instance.AddCloseEvent(receivedNotificationObject);
    //             Transform itemContent = receivedNotificationObject.transform.Find("Scroll View/Viewport/Content");
    //             GameObject itemObject = Instantiate(ItemThird, itemContent);

    //             RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
    //             Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
    //             eImage.texture = equipmentTexture;

    //             TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
    //             eQuantity.text = quantity.ToString();

    //             if (objType.Equals("Achievements") || objType.Equals("Borders")
    //             || objType.Equals("Collaboration") || objType.Equals("CollaborationEquipment")
    //             || objType.Equals("Titles") || objType.Equals("Symbols") || objType.Equals("Medals")
    //             || objType.Equals("MagicFormationCircle") || objType.Equals("Talisman") || objType.Equals("Puppet")
    //             || objType.Equals("Alchemy") || objType.Equals("Forge") || objType.Equals("CardLife"))
    //             {
    //                 double currentPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
    //                 PowerManagerService.Create().UpdateUserStats(User.CurrentUserId);
    //                 double newPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
    //                 FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
    //             }
    //         }
    //         else
    //         {
    //             FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
    //         }
    //     });
    // }
}
