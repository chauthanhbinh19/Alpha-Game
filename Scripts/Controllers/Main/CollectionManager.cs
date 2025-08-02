using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Reflection;
using UnityEngine.EventSystems;

public class CollectionManager : MonoBehaviour
{
    private Transform collectionMenuPanel;
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
    public List<Button> rareTabButtons;
    void Start()
    {

    }
    public void CreateCollection(Transform CollectionMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        rare = AppConstants.All;
        collectionMenuPanel = CollectionMenuPanel;
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
        Transform buttonTransform = collectionMenuPanel.Find(buttonName);
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
        mainType = type;
        GetButtonType();
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
        IUserCurrencyRepository userCurrencyRepository = new UserCurrencyRepository();
        UserCurrencyService userCurrencyService = new UserCurrencyService(userCurrencyRepository);
        List<Currency> currencies = new List<Currency>();
        currencies = userCurrencyService.GetUserCurrency();
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
                var collaborationGalleryService = CollaborationGalleryService.Create();
                List<Collaboration> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset, rare);
                CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

                totalRecord = collaborationGalleryService.GetCollaborationCount(rare);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                var medalsGalleryService = MedalsGalleryService.Create();
                List<Medals> medals = medalsGalleryService.GetMedalsCollection(pageSize, offset, rare);
                MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);

                totalRecord = medalsGalleryService.GetMedalsCount(rare);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                var titlesGalleryService = TitlesGalleryService.Create();
                List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset, rare);
                TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

                totalRecord = titlesGalleryService.GetTitlesCount(rare);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                var bordersGalleryService = BordersGalleryService.Create();
                List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset, rare);
                BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

                totalRecord = bordersGalleryService.GetBordersCount(rare);
            }
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        }

    }
    void OnButtonClick(GameObject clickedButton, string subType)
    {
        foreach (Transform child in LeftScrollViewContentPanel)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_167");
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
            var cardHeroesGalleryService = CardHeroesGalleryService.Create();
            List<CardHeroes> cardHeroes = cardHeroesGalleryService.GetCardHeroesCollection(type, pageSize, offset, rare);
            CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);

            totalRecord = cardHeroesGalleryService.GetCardHeroesCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Book))
        {
            var booksGalleryService = BooksGalleryService.Create();
            List<Books> books = booksGalleryService.GetBooksCollection(type, pageSize, offset, rare);
            BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = booksGalleryService.GetBooksCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardCaptain))
        {
            var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
            List<CardCaptains> cardCaptains = cardCaptainsGalleryService.GetCardCaptainsCollection(type, pageSize, offset, rare);
            CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);

            totalRecord = cardCaptainsGalleryService.GetCardCaptainsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CollaborationEquipment))
        {
            var collaborationEquipmentGalleryService = CollaborationEquipmentGalleryService.Create();
            List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollection(type, pageSize, offset, rare);
            CollaborationEquipmentGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = collaborationEquipmentGalleryService.GetCollaborationEquipmentCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Equipment))
        {
            var equipmentsGalleryService = EquipmentsGalleryService.Create();
            List<Equipments> equipments = equipmentsGalleryService.GetEquipmentsCollection(type, pageSize, offset, rare);
            EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = equipmentsGalleryService.GetEquipmentsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Pet))
        {
            var petsGalleryService = PetsGalleryService.Create();
            List<Pets> pets = petsGalleryService.GetPetsCollection(type, pageSize, offset, rare);
            PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = petsGalleryService.GetPetsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Skill))
        {
            var skillsGalleryService = SkillsGalleryService.Create();
            List<Skills> skills = skillsGalleryService.GetSkillsCollection(type, pageSize, offset, rare);
            SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = skillsGalleryService.GetSkillsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Symbol))
        {
            var symbolsGalleryService = SymbolsGalleryService.Create();
            List<Symbols> symbols = symbolsGalleryService.GetSymbolsCollection(type, pageSize, offset, rare);
            SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = symbolsGalleryService.GetSymbolsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardMilitary))
        {
            var cardMilitaryGalleryService = CardMilitaryGalleryService.Create();
            List<CardMilitary> cardMilitaries = cardMilitaryGalleryService.GetCardMilitaryCollection(type, pageSize, offset, rare);
            CardMilitaryGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = cardMilitaryGalleryService.GetCardMilitaryCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardSpell))
        {
            var cardSpellGalleryService = CardSpellGalleryService.Create();
            List<CardSpell> cardSpells = cardSpellGalleryService.GetCardSpellCollection(type, pageSize, offset, rare);
            CardSpellGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = cardSpellGalleryService.GetCardSpellCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Collaboration))
        {
            var collaborationGalleryService = CollaborationGalleryService.Create();
            List<Collaboration> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset, rare);
            CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

            totalRecord = collaborationGalleryService.GetCollaborationCount(rare);
        }
        else if (mainType.Equals(AppConstants.Medal))
        {
            var medalsGalleryService = MedalsGalleryService.Create();
            List<Medals> medals = medalsGalleryService.GetMedalsCollection(pageSize, offset, rare);
            MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);

            totalRecord = medalsGalleryService.GetMedalsCount(rare);
        }
        else if (mainType.Equals(AppConstants.Title))
        {
            var titlesGalleryService = TitlesGalleryService.Create();
            List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset, rare);
            TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = titlesGalleryService.GetTitlesCount(rare);
        }
        else if (mainType.Equals(AppConstants.Border))
        {
            var bordersGalleryService = BordersGalleryService.Create();
            List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset, rare);
            BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = bordersGalleryService.GetBordersCount(rare);
        }
        else if (mainType.Equals(AppConstants.MagicFormationCircle))
        {
            var magicFormationCircleGalleryService = MagicFormationCircleGalleryService.Create();
            List<MagicFormationCircle> magicFormationCircles = magicFormationCircleGalleryService.GetMagicFormationCircleCollection(type, pageSize, offset, rare);
            MagicFormationCircleGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = magicFormationCircleGalleryService.GetMagicFormationCircleCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Relic))
        {
            var relicsGalleryService = RelicsGalleryService.Create();
            List<Relics> relics = relicsGalleryService.GetRelicsCollection(type, pageSize, offset, rare);
            RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = relicsGalleryService.GetRelicsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardMonster))
        {
            var cardMonstersGalleryService = CardMonstersGalleryService.Create();
            List<CardMonsters> monsters = cardMonstersGalleryService.GetCardMonstersCollection(type, pageSize, offset, rare);
            CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);

            totalRecord = cardMonstersGalleryService.GetCardMonstersCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardColonel))
        {
            var cardColonelsGalleryService = CardColonelsGalleryService.Create();
            List<CardColonels> cardColonels = cardColonelsGalleryService.GetCardColonelsCollection(type, pageSize, offset, rare);
            CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = cardColonelsGalleryService.GetCardColonelsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardGeneral))
        {
            var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
            List<CardGenerals> cardGenerals = cardGeneralsGalleryService.GetCardGeneralsCollection(type, pageSize, offset, rare);
            CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = cardGeneralsGalleryService.GetCardGeneralsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardAdmiral))
        {
            var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
            List<CardAdmirals> cardAdmirals = cardAdmiralsGalleryService.GetCardAdmiralsCollection(type, pageSize, offset, rare);
            CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = cardAdmiralsGalleryService.GetCardAdmiralsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Talisman))
        {
            var talismanGalleryService = TalismanGalleryService.Create();
            List<Talisman> talismans = talismanGalleryService.GetTalismanCollection(type, pageSize, offset, rare);
            TalismanGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = talismanGalleryService.GetTalismanCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Puppet))
        {
            var puppetGalleryService = PuppetGalleryService.Create();
            List<Puppet> puppets = puppetGalleryService.GetPuppetCollection(type, pageSize, offset, rare);
            PuppetGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = puppetGalleryService.GetPuppetCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Alchemy))
        {
            var alchemyGalleryService = AlchemyGalleryService.Create();
            List<Alchemy> alchemies = alchemyGalleryService.GetAlchemyCollection(type, pageSize, offset, rare);
            AlchemyGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = alchemyGalleryService.GetAlchemyCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Forge))
        {
            var forgeGalleryService = ForgeGalleryService.Create();
            List<Forge> forges = forgeGalleryService.GetForgeCollection(type, pageSize, offset, rare);
            ForgeGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = forgeGalleryService.GetForgeCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.CardLife))
        {
            var cardLifeGalleryService = CardLifeGalleryService.Create();
            List<CardLife> cardLives = cardLifeGalleryService.GetCardLifeCollection(type, pageSize, offset, rare);
            CardLifeGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = cardLifeGalleryService.GetCardLifeCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.Artwork))
        {
            var artworkGalleryService = ArtworkGalleryService.Create();
            List<Artwork> artworks = artworkGalleryService.GetArtworkCollection(type, pageSize, offset, rare);
            ArtworkGalleryController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

            totalRecord = artworkGalleryService.GetArtworkCount(type, rare);
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
                var cardHeroesGalleryService = CardHeroesGalleryService.Create();
                totalRecord = cardHeroesGalleryService.GetCardHeroesCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cardHeroes = cardHeroesGalleryService.GetCardHeroesCollection(subType, pageSize, offset, rare);
                CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {
                var booksGalleryService = BooksGalleryService.Create();
                totalRecord = booksGalleryService.GetBooksCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = booksGalleryService.GetBooksCollection(subType, pageSize, offset, rare);
                BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {
                var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
                totalRecord = cardCaptainsGalleryService.GetCardCaptainsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = cardCaptainsGalleryService.GetCardCaptainsCollection(subType, pageSize, offset, rare);
                CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {
                var collaborationEquipmentGalleryService = CollaborationEquipmentGalleryService.Create();
                totalRecord = collaborationEquipmentGalleryService.GetCollaborationEquipmentCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollection(subType, pageSize, offset, rare);
                CollaborationEquipmentGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                var collaborationGalleryService = CollaborationGalleryService.Create();
                totalRecord = collaborationGalleryService.GetCollaborationCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset, rare);
                CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                var equipmentsGalleryService = EquipmentsGalleryService.Create();
                totalRecord = equipmentsGalleryService.GetEquipmentsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentsGalleryService.GetEquipmentsCollection(subType, pageSize, offset, rare);
                EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                var medalGalleryService = MedalsGalleryService.Create();
                totalRecord = medalGalleryService.GetMedalsCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medals = medalGalleryService.GetMedalsCollection(pageSize, offset, rare);
                MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {
                var cardMonstersGalleryService = CardMonstersGalleryService.Create();
                totalRecord = cardMonstersGalleryService.GetCardMonstersCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> monsters = cardMonstersGalleryService.GetCardMonstersCollection(subType, pageSize, offset, rare);
                CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {
                var petsGalleryService = PetsGalleryService.Create();
                totalRecord = petsGalleryService.GetPetsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = petsGalleryService.GetPetsCollection(subType, pageSize, offset, rare);
                PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {
                var skillGalleryService = SkillsGalleryService.Create();
                totalRecord = skillGalleryService.GetSkillsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = skillGalleryService.GetSkillsCollection(subType, pageSize, offset, rare);
                SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {
                var symbolGalleryService = SymbolsGalleryService.Create();
                totalRecord = symbolGalleryService.GetSymbolsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = symbolGalleryService.GetSymbolsCollection(subType, pageSize, offset, rare);
                SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                var titlesGalleryService = TitlesGalleryService.Create();
                totalRecord = titlesGalleryService.GetTitlesCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset, rare);
                TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {
                var cardMilitaryGalleryService = CardMilitaryGalleryService.Create();
                totalRecord = cardMilitaryGalleryService.GetCardMilitaryCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> cardMilitaries = cardMilitaryGalleryService.GetCardMilitaryCollection(subType, pageSize, offset, rare);
                CardMilitaryGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {
                var cardSpellGalleryService = CardSpellGalleryService.Create();
                totalRecord = cardSpellGalleryService.GetCardSpellCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> cardSpells = cardSpellGalleryService.GetCardSpellCollection(subType, pageSize, offset, rare);
                CardSpellGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {
                var magicFormationCircleGalleryService = MagicFormationCircleGalleryService.Create();
                totalRecord = magicFormationCircleGalleryService.GetMagicFormationCircleCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleGalleryService.GetMagicFormationCircleCollection(subType, pageSize, offset, rare);
                MagicFormationCircleGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {
                var relicsGalleryService = RelicsGalleryService.Create();
                totalRecord = relicsGalleryService.GetRelicsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = relicsGalleryService.GetRelicsCollection(subType, pageSize, offset, rare);
                RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                var cardColonelsGalleryService = CardColonelsGalleryService.Create();
                totalRecord = cardColonelsGalleryService.GetCardColonelsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = cardColonelsGalleryService.GetCardColonelsCollection(subType, pageSize, offset, rare);
                CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
                totalRecord = cardGeneralsGalleryService.GetCardGeneralsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = cardGeneralsGalleryService.GetCardGeneralsCollection(subType, pageSize, offset, rare);
                CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
                totalRecord = cardAdmiralsGalleryService.GetCardAdmiralsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = cardAdmiralsGalleryService.GetCardAdmiralsCollection(subType, pageSize, offset, rare);
                CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                var bordersGalleryService = BordersGalleryService.Create();
                totalRecord = bordersGalleryService.GetBordersCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset, rare);
                BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                var talismanGalleryService = TalismanGalleryService.Create();
                totalRecord = talismanGalleryService.GetTalismanCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = talismanGalleryService.GetTalismanCollection(subType, pageSize, offset, rare);
                TalismanGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                var puppetGalleryService = PuppetGalleryService.Create();
                totalRecord = puppetGalleryService.GetPuppetCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = puppetGalleryService.GetPuppetCollection(subType, pageSize, offset, rare);
                PuppetGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                var alchemyGalleryService = AlchemyGalleryService.Create();
                totalRecord = alchemyGalleryService.GetAlchemyCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = alchemyGalleryService.GetAlchemyCollection(subType, pageSize, offset, rare);
                AlchemyGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                var forgeGalleryService = ForgeGalleryService.Create();
                totalRecord = forgeGalleryService.GetForgeCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = forgeGalleryService.GetForgeCollection(subType, pageSize, offset, rare);
                ForgeGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                var cardLifeGalleryService = CardLifeGalleryService.Create();
                totalRecord = cardLifeGalleryService.GetCardLifeCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = cardLifeGalleryService.GetCardLifeCollection(subType, pageSize, offset, rare);
                CardLifeGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                var artworkGalleryService = ArtworkGalleryService.Create();
                totalRecord = artworkGalleryService.GetArtworkCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Artwork> artworks = artworkGalleryService.GetArtworkCollection(subType, pageSize, offset, rare);
                ArtworkGalleryController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);
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
                var cardHeroesGalleryService = CardHeroesGalleryService.Create();
                totalRecord = cardHeroesGalleryService.GetCardHeroesCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cardHeroes = cardHeroesGalleryService.GetCardHeroesCollection(subType, pageSize, offset, rare);
                CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {
                var booksGalleryService = BooksGalleryService.Create();
                totalRecord = booksGalleryService.GetBooksCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = booksGalleryService.GetBooksCollection(subType, pageSize, offset, rare);
                BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {
                var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
                totalRecord = cardCaptainsGalleryService.GetCardCaptainsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = cardCaptainsGalleryService.GetCardCaptainsCollection(subType, pageSize, offset, rare);
                CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {
                var collaborationEquipmentGalleryService = CollaborationEquipmentGalleryService.Create();
                totalRecord = collaborationEquipmentGalleryService.GetCollaborationEquipmentCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollection(subType, pageSize, offset, rare);
                CollaborationEquipmentGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                var collaborationGalleryService = CollaborationGalleryService.Create();
                totalRecord = collaborationGalleryService.GetCollaborationCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset, rare);
                CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                var equipmentsGalleryService = EquipmentsGalleryService.Create();
                totalRecord = equipmentsGalleryService.GetEquipmentsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentsGalleryService.GetEquipmentsCollection(subType, pageSize, offset, rare);
                EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                var medalGalleryService = MedalsGalleryService.Create();
                totalRecord = medalGalleryService.GetMedalsCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medals = medalGalleryService.GetMedalsCollection(pageSize, offset, rare);
                MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {
                var cardMonstersGalleryService = CardMonstersGalleryService.Create();
                totalRecord = cardMonstersGalleryService.GetCardMonstersCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monsters = cardMonstersGalleryService.GetCardMonstersCollection(subType, pageSize, offset, rare);
                CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {
                var petsGalleryService = PetsGalleryService.Create();
                totalRecord = petsGalleryService.GetPetsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = petsGalleryService.GetPetsCollection(subType, pageSize, offset, rare);
                PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {
                var skillGalleryService = SkillsGalleryService.Create();
                totalRecord = skillGalleryService.GetSkillsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = skillGalleryService.GetSkillsCollection(subType, pageSize, offset, rare);
                SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {
                var symbolGalleryService = SymbolsGalleryService.Create();
                totalRecord = symbolGalleryService.GetSymbolsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = symbolGalleryService.GetSymbolsCollection(subType, pageSize, offset, rare);
                SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                var titlesGalleryService = TitlesGalleryService.Create();
                totalRecord = titlesGalleryService.GetTitlesCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset, rare);
                TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {
                var cardMilitaryGalleryService = CardMilitaryGalleryService.Create();
                totalRecord = cardMilitaryGalleryService.GetCardMilitaryCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> cardMilitaries = cardMilitaryGalleryService.GetCardMilitaryCollection(subType, pageSize, offset, rare);
                CardMilitaryGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {
                var cardSpellGalleryService = CardSpellGalleryService.Create();
                totalRecord = cardSpellGalleryService.GetCardSpellCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> cardSpells = cardSpellGalleryService.GetCardSpellCollection(subType, pageSize, offset, rare);
                CardSpellGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {
                var magicFormationCircleGalleryService = MagicFormationCircleGalleryService.Create();
                totalRecord = magicFormationCircleGalleryService.GetMagicFormationCircleCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleGalleryService.GetMagicFormationCircleCollection(subType, pageSize, offset, rare);
                MagicFormationCircleGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {
                var relicsGalleryService = RelicsGalleryService.Create();
                totalRecord = relicsGalleryService.GetRelicsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = relicsGalleryService.GetRelicsCollection(subType, pageSize, offset, rare);
                RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                var cardColonelsGalleryService = CardColonelsGalleryService.Create();
                totalRecord = cardColonelsGalleryService.GetCardColonelsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = cardColonelsGalleryService.GetCardColonelsCollection(subType, pageSize, offset, rare);
                CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
                totalRecord = cardGeneralsGalleryService.GetCardGeneralsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = cardGeneralsGalleryService.GetCardGeneralsCollection(subType, pageSize, offset, rare);
                CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
                totalRecord = cardAdmiralsGalleryService.GetCardAdmiralsCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = cardAdmiralsGalleryService.GetCardAdmiralsCollection(subType, pageSize, offset, rare);
                CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                var bordersGalleryService = BordersGalleryService.Create();
                totalRecord = bordersGalleryService.GetBordersCount(rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset, rare);
                BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                var talismanGalleryService = TalismanGalleryService.Create();
                totalRecord = talismanGalleryService.GetTalismanCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = talismanGalleryService.GetTalismanCollection(subType, pageSize, offset, rare);
                TalismanGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                var puppetGalleryService = PuppetGalleryService.Create();
                totalRecord = puppetGalleryService.GetPuppetCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = puppetGalleryService.GetPuppetCollection(subType, pageSize, offset, rare);
                PuppetGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                var alchemyGalleryService = AlchemyGalleryService.Create();
                totalRecord = alchemyGalleryService.GetAlchemyCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = alchemyGalleryService.GetAlchemyCollection(subType, pageSize, offset, rare);
                AlchemyGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                var forgeGalleryService = ForgeGalleryService.Create();
                totalRecord = forgeGalleryService.GetForgeCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = forgeGalleryService.GetForgeCollection(subType, pageSize, offset, rare);
                ForgeGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                var cardLifeGalleryService = CardLifeGalleryService.Create();
                totalRecord = cardLifeGalleryService.GetCardLifeCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = cardLifeGalleryService.GetCardLifeCollection(subType, pageSize, offset, rare);
                CardLifeGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                var artworkGalleryService = ArtworkGalleryService.Create();
                totalRecord = artworkGalleryService.GetArtworkCount(subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Artwork> artworks = artworkGalleryService.GetArtworkCollection(subType, pageSize, offset, rare);
                ArtworkGalleryController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);
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
