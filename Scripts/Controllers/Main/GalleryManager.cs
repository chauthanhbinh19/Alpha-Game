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
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
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
    private string type;
    private string rare;
    public void CreateGallery(Transform GalleryMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        rare = AppConstants.All;
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
        AssignButtonEvent("Button_27", () => GetType(AppConstants.SpiritBeast));
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
        RightScrollViewContentPanel = equipmentObject.transform.Find("RightScrollView/Viewport/Content");
        LeftScrollViewContentPanel = equipmentObject.transform.Find("Scroll View/Viewport/ButtonContent");
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

        List<string> uniqueRaries = QualityEvaluator.rarities;
        if (uniqueRaries.Count > 0)
        {
            for (int i = 0; i < uniqueRaries.Count; i++)
            {
                string selectedRare = uniqueRaries[i];
                string rareTemp = selectedRare;
                GameObject button = Instantiate(buttonPrefab, RightScrollViewContentPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = LocalizationManager.Get(selectedRare);

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnRareTabButtonClick(button, rareTemp));

                if (i == 0)
                {
                    rare = selectedRare;
                    ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_84_2");
                    LoadCurrentPage();
                }
                else
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_84_1");
                }
            }
        }

        List<string> uniqueTypes = TypeManager.GetUniqueTypes(mainType);
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subType = uniqueTypes[i];
                GameObject button = Instantiate(buttonPrefab, LeftScrollViewContentPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subType.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, subType));

                if (i == 0)
                {
                    type = subType;
                    ButtonLoader.Instance.ChangeButtonBackground(button, "Background_V4_166");
                    LoadCurrentPage();
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
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset, rare);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

                totalRecord = CollaborationService.Create().GetCollaborationCount(rare);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset, rare);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

                totalRecord = MedalsService.Create().GetMedalsCount(rare);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset, rare);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

                totalRecord = TitlesService.Create().GetTitlesCount(rare);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset, rare);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

                totalRecord = BordersService.Create().GetBordersCount(rare);
            }
            else if (mainType.Equals(AppConstants.SpiritBeast))
            {
                List<SpiritBeast> spiritBeasts = SpiritBeastService.Create().GetSpiritBeast(pageSize, offset, rare);
                SpiritBeastController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);

                totalRecord = SpiritBeastService.Create().GetSpiritBeastCount(rare);
            }

            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        }

    }
    void OnButtonClick(GameObject clickedButton, string subType)
    {
        foreach (Transform child in LeftScrollViewContentPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_167"); // Giả sử bạn có texture trắng
            }
        }

        type = subType;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, "Background_V4_166");

        if (RightScrollViewContentPanel.childCount > 0)
        {
            for (int i = 0; i < RightScrollViewContentPanel.childCount; i++)
            {
                Transform child = RightScrollViewContentPanel.GetChild(i);
                Button rareButton = child.GetComponent<Button>();

                if (rareButton != null)
                {
                    if (i == 0)
                    {
                        rare = QualityEvaluator.rarities[0]; // hoặc AppConstants.All
                        ButtonLoader.Instance.ChangeButtonBackground(child.gameObject, "Background_V4_84_2");
                    }
                    else
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(child.gameObject, "Background_V4_84_1");
                    }
                }
            }
        }

        LoadCurrentPage();
    }
    public void OnRareTabButtonClick(GameObject clickedButton, string selectedRare)
    {
        foreach (Transform child in RightScrollViewContentPanel)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_84_1");
            }
        }

        rare = selectedRare;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, "Background_V4_84_2");
        LoadCurrentPage();
    }
    public void LoadCurrentPage()
    {
        int totalRecord = 0;

        if (mainType.Equals(AppConstants.CardHero))
        {

            List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(type, pageSize, offset, rare);
            CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);

            totalRecord = CardHeroesService.Create().GetCardHeroesCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Book))
        {

            List<Books> books = BooksService.Create().GetBooks(type, pageSize, offset, rare);
            BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = BooksService.Create().GetBooksCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardCaptain))
        {

            List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptains(type, pageSize, offset, rare);
            CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

            totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CollaborationEquipment))
        {

            List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(type, pageSize, offset, rare);
            CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Equipment))
        {

            List<Equipments> equipments = EquipmentsService.Create().GetEquipments(type, pageSize, offset, rare);
            EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = EquipmentsService.Create().GetEquipmentsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Pet))
        {

            List<Pets> pets = PetsService.Create().GetPets(type, pageSize, offset, rare);
            PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = PetsService.Create().GetPetsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Skill))
        {

            List<Skills> skills = SkillsService.Create().GetSkills(type, pageSize, offset, rare);
            SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = SkillsService.Create().GetSkillsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Symbol))
        {

            List<Symbols> symbols = SymbolsService.Create().GetSymbols(type, pageSize, offset, rare);
            SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = SymbolsService.Create().GetSymbolsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardMilitary))
        {

            List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(type, pageSize, offset, rare);
            CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardSpell))
        {

            List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpell(type, pageSize, offset, rare);
            CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = CardSpellService.Create().GetCardSpellCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Collaboration))
        {
            List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset, rare);
            CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

            totalRecord = CollaborationService.Create().GetCollaborationCount(rare);
        }
        else if (mainType.Equals(AppConstants.Medal))
        {
            List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset, rare);
            MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

            totalRecord = MedalsService.Create().GetMedalsCount(rare);
        }
        else if (mainType.Equals(AppConstants.Title))
        {
            List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset, rare);
            TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = TitlesService.Create().GetTitlesCount(rare);
        }
        else if (mainType.Equals(AppConstants.Border))
        {
            List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset, rare);
            BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = BordersService.Create().GetBordersCount(rare);
        }
        else if (mainType.Equals(AppConstants.MagicFormationCircle))
        {

            List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(type, pageSize, offset, rare);
            MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Relic))
        {

            List<Relics> relics = RelicsService.Create().GetRelics(type, pageSize, offset, rare);
            RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = RelicsService.Create().GetRelicsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardMonster))
        {

            List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(type, pageSize, offset, rare);
            CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

            totalRecord = CardMonstersService.Create().GetCardMonstersCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardColonel))
        {

            List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(type, pageSize, offset, rare);
            CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = CardColonelsService.Create().GetCardColonelsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardGeneral))
        {

            List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(type, pageSize, offset, rare);
            CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardAdmiral))
        {

            List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(type, pageSize, offset, rare);
            CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Talisman))
        {
            List<Talisman> talismans = TalismanService.Create().GetTalisman(type, pageSize, offset, rare);
            TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = TalismanService.Create().GetTalismanCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Puppet))
        {
            List<Puppet> puppets = PuppetService.Create().GetPuppet(type, pageSize, offset, rare);
            PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = PuppetService.Create().GetPuppetCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Alchemy))
        {
            List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(type, pageSize, offset, rare);
            AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = AlchemyService.Create().GetAlchemyCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Forge))
        {
            List<Forge> forges = ForgeService.Create().GetForge(type, pageSize, offset, rare);
            ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = ForgeService.Create().GetForgeCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardLife))
        {
            List<CardLife> cardLives = CardLifeService.Create().GetCardLife(type, pageSize, offset, rare);
            CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = CardLifeService.Create().GetCardLifeCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Artwork))
        {
            List<Artwork> artworks = ArtworkService.Create().GetArtwork(type, pageSize, offset, rare);
            ArtworkController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

            totalRecord = ArtworkService.Create().GetArtworkCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.SpiritBeast))
        {
            List<SpiritBeast> spiritBeasts = SpiritBeastService.Create().GetSpiritBeast(pageSize, offset, rare);
            SpiritBeastController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = SpiritBeastService.Create().GetSpiritBeastCount(rare);
        }

        totalPage = CalculateTotalPages(totalRecord, pageSize);
        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
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
        foreach (Transform child in LeftScrollViewContentPanel)
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

                totalRecord = CardHeroesService.Create().GetCardHeroesCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(subType, pageSize, offset, rare);
                CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {

                totalRecord = BooksService.Create().GetBooksCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = BooksService.Create().GetBooks(subType, pageSize, offset, rare);
                BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {

                totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptains(subType, pageSize, offset, rare);
                CardCaptainsController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {

                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(subType, pageSize, offset, rare);
                CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                totalRecord = CollaborationService.Create().GetCollaborationCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset, rare);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset, rare);
                EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                totalRecord = MedalsService.Create().GetMedalsCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset, rare);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {

                totalRecord = CardMonstersService.Create().GetCardMonstersCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(subType, pageSize, offset, rare);
                CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {

                totalRecord = PetsService.Create().GetPetsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = PetsService.Create().GetPets(subType, pageSize, offset, rare);
                PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {

                totalRecord = SkillsService.Create().GetSkillsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = SkillsService.Create().GetSkills(subType, pageSize, offset, rare);
                SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {

                totalRecord = SymbolsService.Create().GetSymbolsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = SymbolsService.Create().GetSymbols(subType, pageSize, offset, rare);
                SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {

                totalRecord = TitlesService.Create().GetTitlesCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset, rare);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {

                totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(subType, pageSize, offset, rare);
                CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {

                totalRecord = CardSpellService.Create().GetCardSpellCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> cardSpells = CardSpellService.Create().GetCardSpell(subType, pageSize, offset, rare);
                CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {

                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(subType, pageSize, offset, rare);
                MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {

                totalRecord = RelicsService.Create().GetRelicsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = RelicsService.Create().GetRelics(subType, pageSize, offset, rare);
                RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(subType, pageSize, offset, rare);
                CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(subType, pageSize, offset, rare);
                CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(subType, pageSize, offset, rare);
                CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                totalRecord = BordersService.Create().GetBordersCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset, rare);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                totalRecord = TalismanService.Create().GetTalismanCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalisman(subType, pageSize, offset, rare);
                TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                totalRecord = PuppetService.Create().GetPuppetCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppet(subType, pageSize, offset, rare);
                PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                totalRecord = AlchemyService.Create().GetAlchemyCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(subType, pageSize, offset, rare);
                AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                totalRecord = ForgeService.Create().GetForgeCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = ForgeService.Create().GetForge(subType, pageSize, offset, rare);
                ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                totalRecord = CardLifeService.Create().GetCardLifeCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLife(subType, pageSize, offset, rare);
                CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                totalRecord = ArtworkService.Create().GetArtworkCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Artwork> artworks = ArtworkService.Create().GetArtwork(subType, pageSize, offset, rare);
                ArtworkController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.SpiritBeast))
            {

                totalRecord = SpiritBeastService.Create().GetSpiritBeastCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<SpiritBeast> spiritBeasts = SpiritBeastService.Create().GetSpiritBeast(pageSize, offset, rare);
                SpiritBeastController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);
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

                totalRecord = CardHeroesService.Create().GetCardHeroesCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(subType, pageSize, offset, rare);
                CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {

                totalRecord = BooksService.Create().GetBooksCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = BooksService.Create().GetBooks(subType, pageSize, offset, rare);
                BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {

                totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptains(subType, pageSize, offset, rare);
                CardCaptainsController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {

                totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(subType, pageSize, offset, rare);
                CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                totalRecord = CollaborationService.Create().GetCollaborationCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset, rare);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                totalRecord = EquipmentsService.Create().GetEquipmentsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = EquipmentsService.Create().GetEquipments(subType, pageSize, offset, rare);
                EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                totalRecord = MedalsService.Create().GetMedalsCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset, rare);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {

                totalRecord = CardMonstersService.Create().GetCardMonstersCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monsters = CardMonstersService.Create().GetCardMonsters(subType, pageSize, offset, rare);
                CardMonstersController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {

                totalRecord = PetsService.Create().GetPetsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = PetsService.Create().GetPets(subType, pageSize, offset, rare);
                PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {

                totalRecord = SkillsService.Create().GetSkillsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = SkillsService.Create().GetSkills(subType, pageSize, offset, rare);
                SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {

                totalRecord = SymbolsService.Create().GetSymbolsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = SymbolsService.Create().GetSymbols(subType, pageSize, offset, rare);
                SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {

                totalRecord = TitlesService.Create().GetTitlesCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset, rare);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {

                totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> militaries = CardMilitaryService.Create().GetCardMilitary(subType, pageSize, offset, rare);
                CardMilitaryController.Instance.CreateCardMilitaryGallery(militaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {

                totalRecord = CardSpellService.Create().GetCardSpellCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> spells = CardSpellService.Create().GetCardSpell(subType, pageSize, offset, rare);
                CardSpellController.Instance.CreateCardSpellGallery(spells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {

                totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(subType, pageSize, offset, rare);
                MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {

                totalRecord = RelicsService.Create().GetRelicsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = RelicsService.Create().GetRelics(subType, pageSize, offset, rare);
                RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                totalRecord = CardColonelsService.Create().GetCardColonelsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(subType, pageSize, offset, rare);
                CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(subType, pageSize, offset, rare);
                CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(subType, pageSize, offset, rare);
                CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                totalRecord = BordersService.Create().GetBordersCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset, rare);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                totalRecord = TalismanService.Create().GetTalismanCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = TalismanService.Create().GetTalisman(subType, pageSize, offset, rare);
                TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                totalRecord = PuppetService.Create().GetPuppetCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = PuppetService.Create().GetPuppet(subType, pageSize, offset, rare);
                PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                totalRecord = AlchemyService.Create().GetAlchemyCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = AlchemyService.Create().GetAlchemy(subType, pageSize, offset, rare);
                AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                totalRecord = ForgeService.Create().GetForgeCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = ForgeService.Create().GetForge(subType, pageSize, offset, rare);
                ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                totalRecord = CardLifeService.Create().GetCardLifeCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = CardLifeService.Create().GetCardLife(subType, pageSize, offset, rare);
                CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                totalRecord = ArtworkService.Create().GetArtworkCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Artwork> artworks = ArtworkService.Create().GetArtwork(subType, pageSize, offset, rare);
                ArtworkController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.SpiritBeast))
            {

                totalRecord = SpiritBeastService.Create().GetSpiritBeastCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<SpiritBeast> spiritBeasts = SpiritBeastService.Create().GetSpiritBeast(pageSize, offset, rare);
                SpiritBeastController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);
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
