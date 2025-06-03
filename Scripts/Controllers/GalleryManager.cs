using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Reflection;
using UnityEngine.EventSystems;

public class GalleryManager : MonoBehaviour
{
    private Transform galleryMenuPanel;
    private GameObject buttonPrefab;
    private GameObject DictionaryPanel;
    private Transform MainPanel;
    private Transform DictionaryContentPanel;
    private Transform TabButtonPanel;
    private Button CloseButton;
    private Button HomeButton;
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
    public void CreateGallery(Transform GalleryMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        galleryMenuPanel = GalleryMenuPanel;
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        DictionaryPanel = UIManager.Instance.GetGameObject("DictionaryPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");

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
        AssignButtonEvent("Button_20", () => GetType("Borders"));
        AssignButtonEvent("Button_21", () => GetType("Talisman"));
        AssignButtonEvent("Button_22", () => GetType("Puppet"));
        AssignButtonEvent("Button_23", () => GetType("Alchemy"));
        AssignButtonEvent("Button_24", () => GetType("Forge"));
        AssignButtonEvent("Button_25", () => GetType("CardLife"));
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
        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetUserCurrency();
        FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);

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
                    if (mainType.Equals("CardHeroes"))
                    {
                        
                        List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(subtype, pageSize, offset);
                        CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);

                        totalRecord = CardHeroesService.Create().GetCardHeroesCount(subtype);
                    }
                    else if (mainType.Equals("Books"))
                    {
                        
                        List<Books> books = BooksService.Create().GetBooks(subtype, pageSize, offset);
                        BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

                        totalRecord = BooksService.Create().GetBooksCount(subtype);
                    }
                    else if (mainType.Equals("CardCaptains"))
                    {
                        
                        List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptains(subtype, pageSize, offset);
                        CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

                        totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(subtype);
                    }
                    else if (mainType.Equals("CollaborationEquipments"))
                    {
                        
                        List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(subtype, pageSize, offset);
                        CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

                        totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(subtype);
                    }
                    else if (mainType.Equals("Equipments"))
                    {
                        
                        List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subtype, pageSize, offset);
                        EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

                        totalRecord = EquipmentsService.Create().GetEquipmentsCount(subtype);
                    }
                    else if (mainType.Equals("Pets"))
                    {
                        
                        List<Pets> pets = PetsService.Create().GetPets(subtype, pageSize, offset);
                        PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

                        totalRecord = PetsService.Create().GetPetsCount(subtype);
                    }
                    else if (mainType.Equals("Skills"))
                    {
                        
                        List<Skills> skills = SkillsService.Create().GetSkills(subtype, pageSize, offset);
                        SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

                        totalRecord = SkillsService.Create().GetSkillsCount(subtype);
                    }
                    else if (mainType.Equals("Symbols"))
                    {
                        
                        List<Symbols> symbols = SymbolsService.Create().GetSymbols(subtype, pageSize, offset);
                        SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

                        totalRecord = SymbolsService.Create().GetSymbolsCount(subtype);
                    }
                    else if (mainType.Equals("CardMilitary"))
                    {
                        
                        List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(subtype, pageSize, offset);
                        CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

                        totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(subType);
                    }
                    else if (mainType.Equals("CardSpell"))
                    {
                        
                        List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpell(subtype, pageSize, offset);
                        CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

                        totalRecord = CardSpellService.Create().GetCardSpellCount(subType);
                    }
                    else if (mainType.Equals("MagicFormationCircle"))
                    {
                        
                        List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(subtype, pageSize, offset);
                        MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

                        totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType);
                    }
                    else if (mainType.Equals("Relics"))
                    {
                        
                        List<Relics> relics = RelicsService.Create().GetRelics(subtype, pageSize, offset);
                        RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

                        totalRecord = RelicsService.Create().GetRelicsCount(subType);
                    }
                    else if (mainType.Equals("CardMonsters"))
                    {
                        
                        List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(subtype, pageSize, offset);
                        CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

                        totalRecord = CardMonstersService.Create().GetCardMonstersCount(subType);
                    }
                    else if (mainType.Equals("CardColonels"))
                    {
                        
                        List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(subtype, pageSize, offset);
                        CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

                        totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType);
                    }
                    else if (mainType.Equals("CardGenerals"))
                    {
                        
                        List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(subtype, pageSize, offset);
                        CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

                        totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType);
                    }
                    else if (mainType.Equals("CardAdmirals"))
                    {
                        
                        List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(subtype, pageSize, offset);
                        CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

                        totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(subType);
                    }
                    else if (mainType.Equals("Talisman"))
                    {
                        List<Talisman> talismans = TalismanService.Create().GetTalisman(subType, pageSize, offset);
                        TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

                        totalRecord = TalismanService.Create().GetTalismanCount(subType);
                    }
                    else if (mainType.Equals("Puppet"))
                    {
                        List<Puppet> puppets = PuppetService.Create().GetPuppet(subType, pageSize, offset);
                        PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

                        totalRecord = PuppetService.Create().GetPuppetCount(subType);
                    }
                    else if (mainType.Equals("Alchemy"))
                    {
                        List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(subType, pageSize, offset);
                        AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

                        totalRecord = AlchemyService.Create().GetAlchemyCount(subType);
                    }
                    else if (mainType.Equals("Forge"))
                    {
                        List<Forge> forges = ForgeService.Create().GetForge(subType, pageSize, offset);
                        ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

                        totalRecord = ForgeService.Create().GetForgeCount(subType);
                    }
                    else if (mainType.Equals("CardLife"))
                    {
                        List<CardLife> cardLives = CardLifeService.Create().GetCardLife(subType, pageSize, offset);
                        CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

                        totalRecord = CardLifeService.Create().GetCardLifeCount(subType);
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
            if (mainType.Equals("Collaborations"))
            {
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

                totalRecord = CollaborationService.Create().GetCollaborationCount();
            }
            else if (mainType.Equals("Medals"))
            {
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

                totalRecord = MedalsService.Create().GetMedalsCount();
            }
            else if (mainType.Equals("Titles"))
            {
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

                totalRecord = TitlesService.Create().GetTitlesCount();
            }
            else if (mainType.Equals("Borders"))
            {
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

                totalRecord = BordersService.Create().GetBordersCount();
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

        if (mainType.Equals("CardHeroes"))
        {
            
            List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(type, pageSize, offset);
            CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);

            totalRecord = CardHeroesService.Create().GetCardHeroesCount(type);
        }
        else if (mainType.Equals("Books"))
        {
            
            List<Books> books = BooksService.Create().GetBooks(type, pageSize, offset);
            BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = BooksService.Create().GetBooksCount(type);
        }
        else if (mainType.Equals("CardCaptains"))
        {
            
            List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptains(type, pageSize, offset);
            CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

            totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(type);
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            
            List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(type, pageSize, offset);
            CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(type);
        }
        else if (mainType.Equals("Equipments"))
        {
            
            List<Equipments> equipments = EquipmentsService.Create().GetEquipments(type, pageSize, offset);
            EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = EquipmentsService.Create().GetEquipmentsCount(type);
        }
        else if (mainType.Equals("Pets"))
        {
            
            List<Pets> pets = PetsService.Create().GetPets(type, pageSize, offset);
            PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = PetsService.Create().GetPetsCount(type);
        }
        else if (mainType.Equals("Skills"))
        {
            
            List<Skills> skills = SkillsService.Create().GetSkills(type, pageSize, offset);
            SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = SkillsService.Create().GetSkillsCount(type);
        }
        else if (mainType.Equals("Symbols"))
        {
            
            List<Symbols> symbols = SymbolsService.Create().GetSymbols(type, pageSize, offset);
            SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = SymbolsService.Create().GetSymbolsCount(type);
        }
        else if (mainType.Equals("CardMilitary"))
        {
            
            List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(type, pageSize, offset);
            CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(type);
        }
        else if (mainType.Equals("CardSpell"))
        {
            
            List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpell(type, pageSize, offset);
            CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = CardSpellService.Create().GetCardSpellCount(type);
        }
        else if (mainType.Equals("MagicFormationCircle"))
        {
            
            List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(type, pageSize, offset);
            MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType);
        }
        else if (mainType.Equals("Relics"))
        {
            
            List<Relics> relics = RelicsService.Create().GetRelics(type, pageSize, offset);
            RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = RelicsService.Create().GetRelicsCount(subType);
        }
        else if (mainType.Equals("CardMonsters"))
        {
            
            List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(type, pageSize, offset);
            CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

            totalRecord = CardMonstersService.Create().GetCardMonstersCount(type);
        }
        else if (mainType.Equals("CardColonels"))
        {
            
            List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(type, pageSize, offset);
            CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType);
        }
        else if (mainType.Equals("CardGenerals"))
        {
            
            List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(type, pageSize, offset);
            CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType);
        }
        else if (mainType.Equals("CardAdmirals"))
        {
            
            List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(type, pageSize, offset);
            CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(type);
        }
        else if (mainType.Equals("Talisman"))
        {
            List<Talisman> talismans = TalismanService.Create().GetTalisman(type, pageSize, offset);
            TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = TalismanService.Create().GetTalismanCount(type);
        }
        else if (mainType.Equals("Puppet"))
        {
            List<Puppet> puppets = PuppetService.Create().GetPuppet(type, pageSize, offset);
            PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = PuppetService.Create().GetPuppetCount(type);
        }
        else if (mainType.Equals("Alchemy"))
        {
            List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(type, pageSize, offset);
            AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = AlchemyService.Create().GetAlchemyCount(type);
        }
        else if (mainType.Equals("Forge"))
        {
            List<Forge> forges = ForgeService.Create().GetForge(type, pageSize, offset);
            ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = ForgeService.Create().GetForgeCount(type);
        }
        else if (mainType.Equals("CardLife"))
        {
            List<CardLife> cardLives = CardLifeService.Create().GetCardLife(type, pageSize, offset);
            CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = CardLifeService.Create().GetCardLifeCount(type);
        }

        totalPage = CalculateTotalPages(totalRecord, pageSize);
        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        // Debug.Log($"Button for type '{type}' clicked!");
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
                
                totalRecord = CardHeroesService.Create().GetCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(subType, pageSize, offset);
                CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);
            }
            else if (mainType.Equals("Books"))
            {
                
                totalRecord = BooksService.Create().GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = BooksService.Create().GetBooks(subType, pageSize, offset);
                BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                
                totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptains(subType, pageSize, offset);
                CardCaptainsController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                
                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(subType, pageSize, offset);
                CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals("Collaborations"))
            {
                totalRecord = CollaborationService.Create().GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals("Equipments"))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset);
                EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals("Medals"))
            {
                totalRecord = MedalsService.Create().GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                
                totalRecord = CardMonstersService.Create().GetCardMonstersCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(subType, pageSize, offset);
                CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals("Pets"))
            {
                
                totalRecord = PetsService.Create().GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = PetsService.Create().GetPets(subType, pageSize, offset);
                PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals("Skills"))
            {
                
                totalRecord = SkillsService.Create().GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = SkillsService.Create().GetSkills(subType, pageSize, offset);
                SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals("Symbols"))
            {
                
                totalRecord = SymbolsService.Create().GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = SymbolsService.Create().GetSymbols(subType, pageSize, offset);
                SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals("Titles"))
            {
                
                totalRecord = TitlesService.Create().GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                
                totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(subType, pageSize, offset);
                CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardSpell"))
            {
                
                totalRecord = CardSpellService.Create().GetCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpell(subType, pageSize, offset);
                CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                
                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(subType, pageSize, offset);
                MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals("Relics"))
            {
                
                totalRecord = RelicsService.Create().GetRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = RelicsService.Create().GetRelics(subType, pageSize, offset);
                RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardColonels"))
            {
                totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(subType, pageSize, offset);
                CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardGenerals"))
            {
                totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(subType, pageSize, offset);
                CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardAdmirals"))
            {
                totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(subType, pageSize, offset);
                CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals("Borders"))
            {
                totalRecord = BordersService.Create().GetBordersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals("Talisman"))
            {
                totalRecord = TalismanService.Create().GetTalismanCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalisman(subType, pageSize, offset);
                TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals("Puppet"))
            {
                totalRecord = PuppetService.Create().GetPuppetCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppet(subType, pageSize, offset);
                PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals("Alchemy"))
            {
                totalRecord = AlchemyService.Create().GetAlchemyCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(subType, pageSize, offset);
                AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals("Forge"))
            {
                totalRecord = ForgeService.Create().GetForgeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = ForgeService.Create().GetForge(subType, pageSize, offset);
                ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardLife"))
            {
                totalRecord = CardLifeService.Create().GetCardLifeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLife(subType, pageSize, offset);
                CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
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
                
                totalRecord = CardHeroesService.Create().GetCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(subType, pageSize, offset);
                CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);
            }
            else if (mainType.Equals("Books"))
            {
                
                totalRecord = BooksService.Create().GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = BooksService.Create().GetBooks(subType, pageSize, offset);
                BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardCaptains"))
            {
                
                totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptains(subType, pageSize, offset);
                CardCaptainsController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                
                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(subType, pageSize, offset);
                CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals("Collaborations"))
            {
                totalRecord = CollaborationService.Create().GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals("Equipments"))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset);
                EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals("Medals"))
            {
                totalRecord = MedalsService.Create().GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardMonsters"))
            {
                
                totalRecord = CardMonstersService.Create().GetCardMonstersCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monsters = CardMonstersService.Create().GetCardMonsters(subType, pageSize, offset);
                CardMonstersController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);
            }
            else if (mainType.Equals("Pets"))
            {
                
                totalRecord = PetsService.Create().GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = PetsService.Create().GetPets(subType, pageSize, offset);
                PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals("Skills"))
            {
                
                totalRecord = SkillsService.Create().GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = SkillsService.Create().GetSkills(subType, pageSize, offset);
                SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals("Symbols"))
            {
                
                totalRecord = SymbolsService.Create().GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = SymbolsService.Create().GetSymbols(subType, pageSize, offset);
                SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals("Titles"))
            {
                
                totalRecord = TitlesService.Create().GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardMilitary"))
            {
                
                totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> militaries = CardMilitaryService.Create().GetCardMilitary(subType, pageSize, offset);
                CardMilitaryController.Instance.CreateCardMilitaryGallery(militaries, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardSpell"))
            {
                
                totalRecord = CardSpellService.Create().GetCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> spells = CardSpellService.Create().GetCardSpell(subType, pageSize, offset);
                CardSpellController.Instance.CreateCardSpellGallery(spells, DictionaryContentPanel);
            }
            else if (mainType.Equals("MagicFormationCircle"))
            {
                
                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(subType, pageSize, offset);
                MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals("Relics"))
            {
                
                totalRecord = RelicsService.Create().GetRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = RelicsService.Create().GetRelics(subType, pageSize, offset);
                RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardColonels"))
            {
                totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(subType, pageSize, offset);
                CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardGenerals"))
            {
                totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(subType, pageSize, offset);
                CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardAdmirals"))
            {
                totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(subType, pageSize, offset);
                CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals("Borders"))
            {
                totalRecord = BordersService.Create().GetBordersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals("Talisman"))
            {
                totalRecord = TalismanService.Create().GetTalismanCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalisman(subType, pageSize, offset);
                TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals("Puppet"))
            {
                totalRecord = PuppetService.Create().GetPuppetCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppet(subType, pageSize, offset);
                PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals("Alchemy"))
            {
                totalRecord = AlchemyService.Create().GetAlchemyCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(subType, pageSize, offset);
                AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals("Forge"))
            {
                totalRecord = ForgeService.Create().GetForgeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = ForgeService.Create().GetForge(subType, pageSize, offset);
                ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals("CardLife"))
            {
                totalRecord = CardLifeService.Create().GetCardLifeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLife(subType, pageSize, offset);
                CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
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
