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

        AssignButtonEvent("Button_1", () => GetType(AppConstants.CardHero));
        AssignButtonEvent("Button_2", () => GetType(AppConstants.Book));
        AssignButtonEvent("Button_3", () => GetType(AppConstants.Pet));
        AssignButtonEvent("Button_4", () => GetType(AppConstants.CardCaptain));
        AssignButtonEvent("Button_5", () => GetType(AppConstants.CollaborationEquipment));
        AssignButtonEvent("Button_6", () => GetType(AppConstants.CardMilitary));
        AssignButtonEvent("Button_7", () => GetType(AppConstants.CardSpell));
        AssignButtonEvent("Button_8", () => GetType(AppConstants.Collaboration));
        AssignButtonEvent("Button_9", () => GetType(AppConstants.CardMonster));
        AssignButtonEvent("Button_10", () => GetType(AppConstants.Equipment));
        AssignButtonEvent("Button_11", () => GetType(AppConstants.Medal));
        AssignButtonEvent("Button_12", () => GetType(AppConstants.Skill));
        AssignButtonEvent("Button_13", () => GetType(AppConstants.Symbol));
        AssignButtonEvent("Button_14", () => GetType(AppConstants.Title));
        AssignButtonEvent("Button_15", () => GetType(AppConstants.MagicFormationCircle));
        AssignButtonEvent("Button_16", () => GetType(AppConstants.Relic));
        AssignButtonEvent("Button_17", () => GetType(AppConstants.CardColonel));
        AssignButtonEvent("Button_18", () => GetType(AppConstants.CardGeneral));
        AssignButtonEvent("Button_19", () => GetType(AppConstants.CardAdmiral));
        AssignButtonEvent("Button_20", () => GetType(AppConstants.Border));
        AssignButtonEvent("Button_21", () => GetType(AppConstants.Talisman));
        AssignButtonEvent("Button_22", () => GetType(AppConstants.Puppet));
        AssignButtonEvent("Button_23", () => GetType(AppConstants.Alchemy));
        AssignButtonEvent("Button_24", () => GetType(AppConstants.Forge));
        AssignButtonEvent("Button_25", () => GetType(AppConstants.CardLife));
        AssignButtonEvent("Button_26", () => GetType(AppConstants.Artwork));
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
        titleText.text = LocalizationManager.Get(type);
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
                    if (mainType.Equals(AppConstants.CardHero))
                    {

                        List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(subtype, pageSize, offset);
                        CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);

                        totalRecord = CardHeroesService.Create().GetCardHeroesCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Book))
                    {

                        List<Books> books = BooksService.Create().GetBooks(subtype, pageSize, offset);
                        BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

                        totalRecord = BooksService.Create().GetBooksCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardCaptain))
                    {

                        List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptains(subtype, pageSize, offset);
                        CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

                        totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CollaborationEquipment))
                    {

                        List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(subtype, pageSize, offset);
                        CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

                        totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Equipment))
                    {

                        List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subtype, pageSize, offset);
                        EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

                        totalRecord = EquipmentsService.Create().GetEquipmentsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Pet))
                    {

                        List<Pets> pets = PetsService.Create().GetPets(subtype, pageSize, offset);
                        PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

                        totalRecord = PetsService.Create().GetPetsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Skill))
                    {

                        List<Skills> skills = SkillsService.Create().GetSkills(subtype, pageSize, offset);
                        SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

                        totalRecord = SkillsService.Create().GetSkillsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Symbol))
                    {

                        List<Symbols> symbols = SymbolsService.Create().GetSymbols(subtype, pageSize, offset);
                        SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

                        totalRecord = SymbolsService.Create().GetSymbolsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardMilitary))
                    {

                        List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(subtype, pageSize, offset);
                        CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

                        totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardSpell))
                    {

                        List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpell(subtype, pageSize, offset);
                        CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

                        totalRecord = CardSpellService.Create().GetCardSpellCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.MagicFormationCircle))
                    {

                        List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(subtype, pageSize, offset);
                        MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

                        totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Relic))
                    {

                        List<Relics> relics = RelicsService.Create().GetRelics(subtype, pageSize, offset);
                        RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

                        totalRecord = RelicsService.Create().GetRelicsCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardMonster))
                    {

                        List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(subtype, pageSize, offset);
                        CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

                        totalRecord = CardMonstersService.Create().GetCardMonstersCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardColonel))
                    {

                        List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(subtype, pageSize, offset);
                        CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

                        totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardGeneral))
                    {

                        List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(subtype, pageSize, offset);
                        CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

                        totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardAdmiral))
                    {

                        List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(subtype, pageSize, offset);
                        CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

                        totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Talisman))
                    {
                        List<Talisman> talismans = TalismanService.Create().GetTalisman(subType, pageSize, offset);
                        TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

                        totalRecord = TalismanService.Create().GetTalismanCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Puppet))
                    {
                        List<Puppet> puppets = PuppetService.Create().GetPuppet(subType, pageSize, offset);
                        PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

                        totalRecord = PuppetService.Create().GetPuppetCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Alchemy))
                    {
                        List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(subType, pageSize, offset);
                        AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

                        totalRecord = AlchemyService.Create().GetAlchemyCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Forge))
                    {
                        List<Forge> forges = ForgeService.Create().GetForge(subType, pageSize, offset);
                        ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

                        totalRecord = ForgeService.Create().GetForgeCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.CardLife))
                    {
                        List<CardLife> cardLives = CardLifeService.Create().GetCardLife(subType, pageSize, offset);
                        CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

                        totalRecord = CardLifeService.Create().GetCardLifeCount(subType);
                    }
                    else if (mainType.Equals(AppConstants.Artwork))
                    {
                        List<Artwork> artworks = ArtworkService.Create().GetArtwork(subType, pageSize, offset);
                        ArtworkController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

                        totalRecord = ArtworkService.Create().GetArtworkCount(subType);
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
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

                totalRecord = CollaborationService.Create().GetCollaborationCount();
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

                totalRecord = MedalsService.Create().GetMedalsCount();
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

                totalRecord = TitlesService.Create().GetTitlesCount();
            }
            else if (mainType.Equals(AppConstants.Border))
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

        if (mainType.Equals(AppConstants.CardHero))
        {

            List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(type, pageSize, offset);
            CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);

            totalRecord = CardHeroesService.Create().GetCardHeroesCount(type);
        }
        else if (mainType.Equals(AppConstants.Book))
        {

            List<Books> books = BooksService.Create().GetBooks(type, pageSize, offset);
            BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = BooksService.Create().GetBooksCount(type);
        }
        else if (mainType.Equals(AppConstants.CardCaptain))
        {

            List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptains(type, pageSize, offset);
            CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

            totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(type);
        }
        else if (mainType.Equals(AppConstants.CollaborationEquipment))
        {

            List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(type, pageSize, offset);
            CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(type);
        }
        else if (mainType.Equals(AppConstants.Equipment))
        {

            List<Equipments> equipments = EquipmentsService.Create().GetEquipments(type, pageSize, offset);
            EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = EquipmentsService.Create().GetEquipmentsCount(type);
        }
        else if (mainType.Equals(AppConstants.Pet))
        {

            List<Pets> pets = PetsService.Create().GetPets(type, pageSize, offset);
            PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = PetsService.Create().GetPetsCount(type);
        }
        else if (mainType.Equals(AppConstants.Skill))
        {

            List<Skills> skills = SkillsService.Create().GetSkills(type, pageSize, offset);
            SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = SkillsService.Create().GetSkillsCount(type);
        }
        else if (mainType.Equals(AppConstants.Symbol))
        {

            List<Symbols> symbols = SymbolsService.Create().GetSymbols(type, pageSize, offset);
            SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = SymbolsService.Create().GetSymbolsCount(type);
        }
        else if (mainType.Equals(AppConstants.CardMilitary))
        {

            List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(type, pageSize, offset);
            CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(type);
        }
        else if (mainType.Equals(AppConstants.CardSpell))
        {

            List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpell(type, pageSize, offset);
            CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = CardSpellService.Create().GetCardSpellCount(type);
        }
        else if (mainType.Equals(AppConstants.MagicFormationCircle))
        {

            List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(type, pageSize, offset);
            MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType);
        }
        else if (mainType.Equals(AppConstants.Relic))
        {

            List<Relics> relics = RelicsService.Create().GetRelics(type, pageSize, offset);
            RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = RelicsService.Create().GetRelicsCount(subType);
        }
        else if (mainType.Equals(AppConstants.CardMonster))
        {

            List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(type, pageSize, offset);
            CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

            totalRecord = CardMonstersService.Create().GetCardMonstersCount(type);
        }
        else if (mainType.Equals(AppConstants.CardColonel))
        {

            List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(type, pageSize, offset);
            CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType);
        }
        else if (mainType.Equals(AppConstants.CardGeneral))
        {

            List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(type, pageSize, offset);
            CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType);
        }
        else if (mainType.Equals(AppConstants.CardAdmiral))
        {

            List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(type, pageSize, offset);
            CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(type);
        }
        else if (mainType.Equals(AppConstants.Talisman))
        {
            List<Talisman> talismans = TalismanService.Create().GetTalisman(type, pageSize, offset);
            TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = TalismanService.Create().GetTalismanCount(type);
        }
        else if (mainType.Equals(AppConstants.Puppet))
        {
            List<Puppet> puppets = PuppetService.Create().GetPuppet(type, pageSize, offset);
            PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = PuppetService.Create().GetPuppetCount(type);
        }
        else if (mainType.Equals(AppConstants.Alchemy))
        {
            List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(type, pageSize, offset);
            AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = AlchemyService.Create().GetAlchemyCount(type);
        }
        else if (mainType.Equals(AppConstants.Forge))
        {
            List<Forge> forges = ForgeService.Create().GetForge(type, pageSize, offset);
            ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = ForgeService.Create().GetForgeCount(type);
        }
        else if (mainType.Equals(AppConstants.CardLife))
        {
            List<CardLife> cardLives = CardLifeService.Create().GetCardLife(type, pageSize, offset);
            CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = CardLifeService.Create().GetCardLifeCount(type);
        }
        else if (mainType.Equals(AppConstants.Artwork))
        {
            List<Artwork> artworks = ArtworkService.Create().GetArtwork(type, pageSize, offset);
            ArtworkController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

            totalRecord = ArtworkService.Create().GetArtworkCount(type);
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

            if (mainType.Equals(AppConstants.CardHero))
            {

                totalRecord = CardHeroesService.Create().GetCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(subType, pageSize, offset);
                CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {

                totalRecord = BooksService.Create().GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = BooksService.Create().GetBooks(subType, pageSize, offset);
                BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {

                totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptains(subType, pageSize, offset);
                CardCaptainsController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {

                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(subType, pageSize, offset);
                CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                totalRecord = CollaborationService.Create().GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset);
                EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                totalRecord = MedalsService.Create().GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {

                totalRecord = CardMonstersService.Create().GetCardMonstersCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(subType, pageSize, offset);
                CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {

                totalRecord = PetsService.Create().GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = PetsService.Create().GetPets(subType, pageSize, offset);
                PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {

                totalRecord = SkillsService.Create().GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = SkillsService.Create().GetSkills(subType, pageSize, offset);
                SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {

                totalRecord = SymbolsService.Create().GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = SymbolsService.Create().GetSymbols(subType, pageSize, offset);
                SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {

                totalRecord = TitlesService.Create().GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {

                totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(subType, pageSize, offset);
                CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {

                totalRecord = CardSpellService.Create().GetCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpell(subType, pageSize, offset);
                CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {

                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(subType, pageSize, offset);
                MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {

                totalRecord = RelicsService.Create().GetRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = RelicsService.Create().GetRelics(subType, pageSize, offset);
                RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(subType, pageSize, offset);
                CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(subType, pageSize, offset);
                CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(subType, pageSize, offset);
                CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                totalRecord = BordersService.Create().GetBordersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                totalRecord = TalismanService.Create().GetTalismanCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalisman(subType, pageSize, offset);
                TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                totalRecord = PuppetService.Create().GetPuppetCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppet(subType, pageSize, offset);
                PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                totalRecord = AlchemyService.Create().GetAlchemyCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(subType, pageSize, offset);
                AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                totalRecord = ForgeService.Create().GetForgeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = ForgeService.Create().GetForge(subType, pageSize, offset);
                ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                totalRecord = CardLifeService.Create().GetCardLifeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLife(subType, pageSize, offset);
                CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                totalRecord = ArtworkService.Create().GetArtworkCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Artwork> artworks = ArtworkService.Create().GetArtwork(subType, pageSize, offset);
                ArtworkController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);
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

                totalRecord = CardHeroesService.Create().GetCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(subType, pageSize, offset);
                CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {

                totalRecord = BooksService.Create().GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = BooksService.Create().GetBooks(subType, pageSize, offset);
                BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {

                totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptains(subType, pageSize, offset);
                CardCaptainsController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {

                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(subType, pageSize, offset);
                CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                totalRecord = CollaborationService.Create().GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset);
                EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                totalRecord = MedalsService.Create().GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {

                totalRecord = CardMonstersService.Create().GetCardMonstersCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monsters = CardMonstersService.Create().GetCardMonsters(subType, pageSize, offset);
                CardMonstersController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {

                totalRecord = PetsService.Create().GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = PetsService.Create().GetPets(subType, pageSize, offset);
                PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {

                totalRecord = SkillsService.Create().GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = SkillsService.Create().GetSkills(subType, pageSize, offset);
                SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {

                totalRecord = SymbolsService.Create().GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = SymbolsService.Create().GetSymbols(subType, pageSize, offset);
                SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {

                totalRecord = TitlesService.Create().GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {

                totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> militaries = CardMilitaryService.Create().GetCardMilitary(subType, pageSize, offset);
                CardMilitaryController.Instance.CreateCardMilitaryGallery(militaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {

                totalRecord = CardSpellService.Create().GetCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> spells = CardSpellService.Create().GetCardSpell(subType, pageSize, offset);
                CardSpellController.Instance.CreateCardSpellGallery(spells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {

                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(subType, pageSize, offset);
                MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {

                totalRecord = RelicsService.Create().GetRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = RelicsService.Create().GetRelics(subType, pageSize, offset);
                RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(subType, pageSize, offset);
                CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(subType, pageSize, offset);
                CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(subType, pageSize, offset);
                CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                totalRecord = BordersService.Create().GetBordersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                totalRecord = TalismanService.Create().GetTalismanCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalisman(subType, pageSize, offset);
                TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                totalRecord = PuppetService.Create().GetPuppetCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppet(subType, pageSize, offset);
                PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                totalRecord = AlchemyService.Create().GetAlchemyCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(subType, pageSize, offset);
                AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                totalRecord = ForgeService.Create().GetForgeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = ForgeService.Create().GetForge(subType, pageSize, offset);
                ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                totalRecord = CardLifeService.Create().GetCardLifeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLife(subType, pageSize, offset);
                CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                totalRecord = ArtworkService.Create().GetArtworkCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Artwork> artworks = ArtworkService.Create().GetArtwork(subType, pageSize, offset);
                ArtworkController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);
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
