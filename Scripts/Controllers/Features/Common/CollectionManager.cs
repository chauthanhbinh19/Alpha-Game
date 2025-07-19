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
    void Start()
    {

    }
    public void CreateCollection(Transform CollectionMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
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
        IUserCurrencyRepository userCurrencyRepository = new UserCurrencyRepository();
        UserCurrencyService userCurrencyService = new UserCurrencyService(userCurrencyRepository);
        List<Currency> currencies = new List<Currency>();
        currencies = userCurrencyService.GetUserCurrency();
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
                        var cardHeroesGalleryService = CardHeroesGalleryService.Create();
                        List<CardHeroes> cardHeroes = cardHeroesGalleryService.GetCardHeroesCollection(subtype, pageSize, offset);
                        CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);

                        totalRecord = cardHeroesGalleryService.GetCardHeroesCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Book))
                    {
                        var booksGalleryService = BooksGalleryService.Create();
                        List<Books> books = booksGalleryService.GetBooksCollection(subtype, pageSize, offset);
                        BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

                        totalRecord = booksGalleryService.GetBooksCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardCaptain))
                    {
                        var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
                        List<CardCaptains> cardCaptains = cardCaptainsGalleryService.GetCardCaptainsCollection(subtype, pageSize, offset);
                        CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);

                        totalRecord = cardCaptainsGalleryService.GetCardCaptainsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CollaborationEquipment))
                    {
                        var collaborationEquipmentGalleryService = CollaborationEquipmentGalleryService.Create();
                        List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollection(subtype, pageSize, offset);
                        CollaborationEquipmentGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

                        totalRecord = collaborationEquipmentGalleryService.GetCollaborationEquipmentCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Equipment))
                    {
                        var equipmentsGalleryService = EquipmentsGalleryService.Create();
                        List<Equipments> equipments = equipmentsGalleryService.GetEquipmentsCollection(subtype, pageSize, offset);
                        EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

                        totalRecord = equipmentsGalleryService.GetEquipmentsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Pet))
                    {
                        var petsGalleryService = PetsGalleryService.Create();
                        List<Pets> pets = petsGalleryService.GetPetsCollection(subtype, pageSize, offset);
                        PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

                        totalRecord = petsGalleryService.GetPetsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Skill))
                    {
                        var skillsGalleryService = SkillsGalleryService.Create();
                        List<Skills> skills = skillsGalleryService.GetSkillsCollection(subtype, pageSize, offset);
                        SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

                        totalRecord = skillsGalleryService.GetSkillsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Symbol))
                    {
                        var symbolsGalleryService = SymbolsGalleryService.Create();
                        List<Symbols> symbols = symbolsGalleryService.GetSymbolsCollection(subtype, pageSize, offset);
                        SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

                        totalRecord = symbolsGalleryService.GetSymbolsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardMilitary))
                    {
                        var cardMilitaryGalleryService = CardMilitaryGalleryService.Create();
                        List<CardMilitary> cardMilitaries = cardMilitaryGalleryService.GetCardMilitaryCollection(subtype, pageSize, offset);
                        CardMilitaryGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

                        totalRecord = cardMilitaryGalleryService.GetCardMilitaryCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardSpell))
                    {
                        var cardSpellGalleryService = CardSpellGalleryService.Create();
                        List<CardSpell> cardSpells = cardSpellGalleryService.GetCardSpellCollection(subtype, pageSize, offset);
                        CardSpellGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

                        totalRecord = cardSpellGalleryService.GetCardSpellCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.MagicFormationCircle))
                    {
                        var magicFormationCircleGalleryService = MagicFormationCircleGalleryService.Create();
                        List<MagicFormationCircle> magicFormationCircles = magicFormationCircleGalleryService.GetMagicFormationCircleCollection(subtype, pageSize, offset);
                        MagicFormationCircleGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

                        totalRecord = magicFormationCircleGalleryService.GetMagicFormationCircleCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Relic))
                    {
                        var relicsGalleryService = RelicsGalleryService.Create();
                        List<Relics> relics = relicsGalleryService.GetRelicsCollection(subtype, pageSize, offset);
                        RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

                        totalRecord = relicsGalleryService.GetRelicsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardMonster))
                    {
                        var cardMonstersGalleryService = CardMonstersGalleryService.Create();
                        List<CardMonsters> monsters = cardMonstersGalleryService.GetCardMonstersCollection(subtype, pageSize, offset);
                        CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);

                        totalRecord = cardMonstersGalleryService.GetCardMonstersCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardColonel))
                    {
                        var cardColonelsGalleryService = CardColonelsGalleryService.Create();
                        List<CardColonels> cardColonels = cardColonelsGalleryService.GetCardColonelsCollection(subtype, pageSize, offset);
                        CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

                        totalRecord = cardColonelsGalleryService.GetCardColonelsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardGeneral))
                    {
                        var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
                        List<CardGenerals> cardGenerals = cardGeneralsGalleryService.GetCardGeneralsCollection(subtype, pageSize, offset);
                        CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

                        totalRecord = cardGeneralsGalleryService.GetCardGeneralsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardAdmiral))
                    {
                        var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
                        List<CardAdmirals> cardAdmirals = cardAdmiralsGalleryService.GetCardAdmiralsCollection(subtype, pageSize, offset);
                        CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

                        totalRecord = cardAdmiralsGalleryService.GetCardAdmiralsCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Talisman))
                    {
                        var talismanGalleryService = TalismanGalleryService.Create();
                        List<Talisman> talismans = talismanGalleryService.GetTalismanCollection(subtype, pageSize, offset);
                        TalismanGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

                        totalRecord = talismanGalleryService.GetTalismanCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Puppet))
                    {
                        var puppetGalleryService = PuppetGalleryService.Create();
                        List<Puppet> puppets = puppetGalleryService.GetPuppetCollection(subtype, pageSize, offset);
                        PuppetGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

                        totalRecord = puppetGalleryService.GetPuppetCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Alchemy))
                    {
                        var alchemyGalleryService = AlchemyGalleryService.Create();
                        List<Alchemy> alchemies = alchemyGalleryService.GetAlchemyCollection(subtype, pageSize, offset);
                        AlchemyGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

                        totalRecord = alchemyGalleryService.GetAlchemyCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Forge))
                    {
                        var forgeGalleryService = ForgeGalleryService.Create();
                        List<Forge> forges = forgeGalleryService.GetForgeCollection(subtype, pageSize, offset);
                        ForgeGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

                        totalRecord = forgeGalleryService.GetForgeCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.CardLife))
                    {
                        var cardLifeGalleryService = CardLifeGalleryService.Create();
                        List<CardLife> cardLives = cardLifeGalleryService.GetCardLifeCollection(subtype, pageSize, offset);
                        CardLifeGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

                        totalRecord = cardLifeGalleryService.GetCardLifeCount(subtype);
                    }
                    else if (mainType.Equals(AppConstants.Artwork))
                    {
                        var artworkGalleryService = ArtworkGalleryService.Create();
                        List<Artwork> artworks = artworkGalleryService.GetArtworkCollection(subtype, pageSize, offset);
                        ArtworkGalleryController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

                        totalRecord = artworkGalleryService.GetArtworkCount(subtype);
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
                var collaborationGalleryService = CollaborationGalleryService.Create();
                List<Collaboration> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset);
                CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

                totalRecord = collaborationGalleryService.GetCollaborationCount();
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                var medalsGalleryService = MedalsGalleryService.Create();
                List<Medals> medals = medalsGalleryService.GetMedalsCollection(pageSize, offset);
                MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);

                totalRecord = medalsGalleryService.GetMedalsCount();
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                var titlesGalleryService = TitlesGalleryService.Create();
                List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset);
                TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

                totalRecord = titlesGalleryService.GetTitlesCount();
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                var bordersGalleryService = BordersGalleryService.Create();
                List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset);
                BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

                totalRecord = bordersGalleryService.GetBordersCount();
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
            var cardHeroesGalleryService = CardHeroesGalleryService.Create();
            List<CardHeroes> cardHeroes = cardHeroesGalleryService.GetCardHeroesCollection(type, pageSize, offset);
            CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);

            totalRecord = cardHeroesGalleryService.GetCardHeroesCount(type);
        }
        else if (mainType.Equals(AppConstants.Book))
        {
            var booksGalleryService = BooksGalleryService.Create();
            List<Books> books = booksGalleryService.GetBooksCollection(type, pageSize, offset);
            BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = booksGalleryService.GetBooksCount(type);
        }
        else if (mainType.Equals(AppConstants.CardCaptain))
        {
            var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
            List<CardCaptains> cardCaptains = cardCaptainsGalleryService.GetCardCaptainsCollection(type, pageSize, offset);
            CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);

            totalRecord = cardCaptainsGalleryService.GetCardCaptainsCount(type);
        }
        else if (mainType.Equals(AppConstants.CollaborationEquipment))
        {
            var collaborationEquipmentGalleryService = CollaborationEquipmentGalleryService.Create();
            List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollection(type, pageSize, offset);
            CollaborationEquipmentGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = collaborationEquipmentGalleryService.GetCollaborationEquipmentCount(type);
        }
        else if (mainType.Equals(AppConstants.Equipment))
        {
            var equipmentsGalleryService = EquipmentsGalleryService.Create();
            List<Equipments> equipments = equipmentsGalleryService.GetEquipmentsCollection(type, pageSize, offset);
            EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = equipmentsGalleryService.GetEquipmentsCount(type);
        }
        else if (mainType.Equals(AppConstants.Pet))
        {
            var petsGalleryService = PetsGalleryService.Create();
            List<Pets> pets = petsGalleryService.GetPetsCollection(type, pageSize, offset);
            PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = petsGalleryService.GetPetsCount(type);
        }
        else if (mainType.Equals(AppConstants.Skill))
        {
            var skillsGalleryService = SkillsGalleryService.Create();
            List<Skills> skills = skillsGalleryService.GetSkillsCollection(type, pageSize, offset);
            SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = skillsGalleryService.GetSkillsCount(type);
        }
        else if (mainType.Equals(AppConstants.Symbol))
        {
            var symbolsGalleryService = SymbolsGalleryService.Create();
            List<Symbols> symbols = symbolsGalleryService.GetSymbolsCollection(type, pageSize, offset);
            SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = symbolsGalleryService.GetSymbolsCount(type);
        }
        else if (mainType.Equals(AppConstants.CardMilitary))
        {
            var cardMilitaryGalleryService = CardMilitaryGalleryService.Create();
            List<CardMilitary> cardMilitaries = cardMilitaryGalleryService.GetCardMilitaryCollection(type, pageSize, offset);
            CardMilitaryGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = cardMilitaryGalleryService.GetCardMilitaryCount(type);
        }
        else if (mainType.Equals(AppConstants.CardSpell))
        {
            var cardSpellGalleryService = CardSpellGalleryService.Create();
            List<CardSpell> cardSpells = cardSpellGalleryService.GetCardSpellCollection(type, pageSize, offset);
            CardSpellGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = cardSpellGalleryService.GetCardSpellCount(type);
        }
        else if (mainType.Equals(AppConstants.MagicFormationCircle))
        {
            var magicFormationCircleGalleryService = MagicFormationCircleGalleryService.Create();
            List<MagicFormationCircle> magicFormationCircles = magicFormationCircleGalleryService.GetMagicFormationCircleCollection(type, pageSize, offset);
            MagicFormationCircleGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = magicFormationCircleGalleryService.GetMagicFormationCircleCount(type);
        }
        else if (mainType.Equals(AppConstants.Relic))
        {
            var relicsGalleryService = RelicsGalleryService.Create();
            List<Relics> relics = relicsGalleryService.GetRelicsCollection(type, pageSize, offset);
            RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = relicsGalleryService.GetRelicsCount(type);
        }
        else if (mainType.Equals(AppConstants.CardMonster))
        {
            var cardMonstersGalleryService = CardMonstersGalleryService.Create();
            List<CardMonsters> monsters = cardMonstersGalleryService.GetCardMonstersCollection(type, pageSize, offset);
            CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);

            totalRecord = cardMonstersGalleryService.GetCardMonstersCount(type);
        }
        else if (mainType.Equals(AppConstants.CardColonel))
        {
            var cardColonelsGalleryService = CardColonelsGalleryService.Create();
            List<CardColonels> cardColonels = cardColonelsGalleryService.GetCardColonelsCollection(type, pageSize, offset);
            CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = cardColonelsGalleryService.GetCardColonelsCount(type);
        }
        else if (mainType.Equals(AppConstants.CardGeneral))
        {
            var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
            List<CardGenerals> cardGenerals = cardGeneralsGalleryService.GetCardGeneralsCollection(type, pageSize, offset);
            CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = cardGeneralsGalleryService.GetCardGeneralsCount(type);
        }
        else if (mainType.Equals(AppConstants.CardAdmiral))
        {
            var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
            List<CardAdmirals> cardAdmirals = cardAdmiralsGalleryService.GetCardAdmiralsCollection(type, pageSize, offset);
            CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = cardAdmiralsGalleryService.GetCardAdmiralsCount(type);
        }
        else if (mainType.Equals(AppConstants.Talisman))
        {
            var talismanGalleryService = TalismanGalleryService.Create();
            List<Talisman> talismans = talismanGalleryService.GetTalismanCollection(type, pageSize, offset);
            TalismanGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = talismanGalleryService.GetTalismanCount(type);
        }
        else if (mainType.Equals(AppConstants.Puppet))
        {
            var puppetGalleryService = PuppetGalleryService.Create();
            List<Puppet> puppets = puppetGalleryService.GetPuppetCollection(type, pageSize, offset);
            PuppetGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = puppetGalleryService.GetPuppetCount(type);
        }
        else if (mainType.Equals(AppConstants.Alchemy))
        {
            var alchemyGalleryService = AlchemyGalleryService.Create();
            List<Alchemy> alchemies = alchemyGalleryService.GetAlchemyCollection(type, pageSize, offset);
            AlchemyGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = alchemyGalleryService.GetAlchemyCount(type);
        }
        else if (mainType.Equals(AppConstants.Forge))
        {
            var forgeGalleryService = ForgeGalleryService.Create();
            List<Forge> forges = forgeGalleryService.GetForgeCollection(type, pageSize, offset);
            ForgeGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = forgeGalleryService.GetForgeCount(type);
        }
        else if (mainType.Equals(AppConstants.CardLife))
        {
            var cardLifeGalleryService = CardLifeGalleryService.Create();
            List<CardLife> cardLives = cardLifeGalleryService.GetCardLifeCollection(type, pageSize, offset);
            CardLifeGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = cardLifeGalleryService.GetCardLifeCount(type);
        }
        else if (mainType.Equals(AppConstants.Artwork))
        {
            var artworkGalleryService = ArtworkGalleryService.Create();
            List<Artwork> artworks = artworkGalleryService.GetArtworkCollection(type, pageSize, offset);
            ArtworkGalleryController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

            totalRecord = artworkGalleryService.GetArtworkCount(type);
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
                var cardHeroesGalleryService = CardHeroesGalleryService.Create();
                totalRecord = cardHeroesGalleryService.GetCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardHeroes> cardHeroes = cardHeroesGalleryService.GetCardHeroesCollection(subType, pageSize, offset);
                CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {
                var booksGalleryService = BooksGalleryService.Create();
                totalRecord = booksGalleryService.GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = booksGalleryService.GetBooksCollection(subType, pageSize, offset);
                BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {
                var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
                totalRecord = cardCaptainsGalleryService.GetCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardCaptains> cardCaptains = cardCaptainsGalleryService.GetCardCaptainsCollection(subType, pageSize, offset);
                CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {
                var collaborationEquipmentGalleryService = CollaborationEquipmentGalleryService.Create();
                totalRecord = collaborationEquipmentGalleryService.GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollection(subType, pageSize, offset);
                CollaborationEquipmentGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                var collaborationGalleryService = CollaborationGalleryService.Create();
                totalRecord = collaborationGalleryService.GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset);
                CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                var equipmentsGalleryService = EquipmentsGalleryService.Create();
                totalRecord = equipmentsGalleryService.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentsGalleryService.GetEquipmentsCollection(subType, pageSize, offset);
                EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                var medalGalleryService = MedalsGalleryService.Create();
                totalRecord = medalGalleryService.GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medals = medalGalleryService.GetMedalsCollection(pageSize, offset);
                MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {
                var cardMonstersGalleryService = CardMonstersGalleryService.Create();
                totalRecord = cardMonstersGalleryService.GetCardMonstersCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMonsters> monsters = cardMonstersGalleryService.GetCardMonstersCollection(subType, pageSize, offset);
                CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {
                var petsGalleryService = PetsGalleryService.Create();
                totalRecord = petsGalleryService.GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> pets = petsGalleryService.GetPetsCollection(subType, pageSize, offset);
                PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {
                var skillGalleryService = SkillsGalleryService.Create();
                totalRecord = skillGalleryService.GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skills = skillGalleryService.GetSkillsCollection(subType, pageSize, offset);
                SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {
                var symbolGalleryService = SymbolsGalleryService.Create();
                totalRecord = symbolGalleryService.GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbols = symbolGalleryService.GetSymbolsCollection(subType, pageSize, offset);
                SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                var titlesGalleryService = TitlesGalleryService.Create();
                totalRecord = titlesGalleryService.GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset);
                TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {
                var cardMilitaryGalleryService = CardMilitaryGalleryService.Create();
                totalRecord = cardMilitaryGalleryService.GetCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardMilitary> cardMilitaries = cardMilitaryGalleryService.GetCardMilitaryCollection(subType, pageSize, offset);
                CardMilitaryGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {
                var cardSpellGalleryService = CardSpellGalleryService.Create();
                totalRecord = cardSpellGalleryService.GetCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardSpell> cardSpells = cardSpellGalleryService.GetCardSpellCollection(subType, pageSize, offset);
                CardSpellGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {
                var magicFormationCircleGalleryService = MagicFormationCircleGalleryService.Create();
                totalRecord = magicFormationCircleGalleryService.GetMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleGalleryService.GetMagicFormationCircleCollection(subType, pageSize, offset);
                MagicFormationCircleGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {
                var relicsGalleryService = RelicsGalleryService.Create();
                totalRecord = relicsGalleryService.GetRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Relics> relics = relicsGalleryService.GetRelicsCollection(subType, pageSize, offset);
                RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                var cardColonelsGalleryService = CardColonelsGalleryService.Create();
                totalRecord = cardColonelsGalleryService.GetCardColonelsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardColonels> cardColonels = cardColonelsGalleryService.GetCardColonelsCollection(subType, pageSize, offset);
                CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
                totalRecord = cardGeneralsGalleryService.GetCardGeneralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardGenerals> cardGenerals = cardGeneralsGalleryService.GetCardGeneralsCollection(subType, pageSize, offset);
                CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
                totalRecord = cardAdmiralsGalleryService.GetCardAdmiralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardAdmirals> cardAdmirals = cardAdmiralsGalleryService.GetCardAdmiralsCollection(subType, pageSize, offset);
                CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                var bordersGalleryService = BordersGalleryService.Create();
                totalRecord = bordersGalleryService.GetBordersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset);
                BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                var talismanGalleryService = TalismanGalleryService.Create();
                totalRecord = talismanGalleryService.GetTalismanCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Talisman> talismans = talismanGalleryService.GetTalismanCollection(subType, pageSize, offset);
                TalismanGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                var puppetGalleryService = PuppetGalleryService.Create();
                totalRecord = puppetGalleryService.GetPuppetCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Puppet> puppets = puppetGalleryService.GetPuppetCollection(subType, pageSize, offset);
                PuppetGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                var alchemyGalleryService = AlchemyGalleryService.Create();
                totalRecord = alchemyGalleryService.GetAlchemyCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Alchemy> alchemies = alchemyGalleryService.GetAlchemyCollection(subType, pageSize, offset);
                AlchemyGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                var forgeGalleryService = ForgeGalleryService.Create();
                totalRecord = forgeGalleryService.GetForgeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Forge> forges = forgeGalleryService.GetForgeCollection(subType, pageSize, offset);
                ForgeGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                var cardLifeGalleryService = CardLifeGalleryService.Create();
                totalRecord = cardLifeGalleryService.GetCardLifeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CardLife> cardLives = cardLifeGalleryService.GetCardLifeCollection(subType, pageSize, offset);
                CardLifeGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                var artworkGalleryService = ArtworkGalleryService.Create();
                totalRecord = artworkGalleryService.GetArtworkCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Artwork> artworks = artworkGalleryService.GetArtworkCollection(subType, pageSize, offset);
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
                totalRecord = cardHeroesGalleryService.GetCardHeroesCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardHeroes> cardHeroes = cardHeroesGalleryService.GetCardHeroesCollection(subType, pageSize, offset);
                CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Book))
            {
                var booksGalleryService = BooksGalleryService.Create();
                totalRecord = booksGalleryService.GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = booksGalleryService.GetBooksCollection(subType, pageSize, offset);
                BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardCaptain))
            {
                var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
                totalRecord = cardCaptainsGalleryService.GetCardCaptainsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardCaptains> cardCaptains = cardCaptainsGalleryService.GetCardCaptainsCollection(subType, pageSize, offset);
                CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CollaborationEquipment))
            {
                var collaborationEquipmentGalleryService = CollaborationEquipmentGalleryService.Create();
                totalRecord = collaborationEquipmentGalleryService.GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollection(subType, pageSize, offset);
                CollaborationEquipmentGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Collaboration))
            {
                var collaborationGalleryService = CollaborationGalleryService.Create();
                totalRecord = collaborationGalleryService.GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset);
                CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Equipment))
            {
                var equipmentsGalleryService = EquipmentsGalleryService.Create();
                totalRecord = equipmentsGalleryService.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentsGalleryService.GetEquipmentsCollection(subType, pageSize, offset);
                EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Medal))
            {
                var medalGalleryService = MedalsGalleryService.Create();
                totalRecord = medalGalleryService.GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medals = medalGalleryService.GetMedalsCollection(pageSize, offset);
                MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMonster))
            {
                var cardMonstersGalleryService = CardMonstersGalleryService.Create();
                totalRecord = cardMonstersGalleryService.GetCardMonstersCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMonsters> monsters = cardMonstersGalleryService.GetCardMonstersCollection(subType, pageSize, offset);
                CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Pet))
            {
                var petsGalleryService = PetsGalleryService.Create();
                totalRecord = petsGalleryService.GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> pets = petsGalleryService.GetPetsCollection(subType, pageSize, offset);
                PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Skill))
            {
                var skillGalleryService = SkillsGalleryService.Create();
                totalRecord = skillGalleryService.GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skills = skillGalleryService.GetSkillsCollection(subType, pageSize, offset);
                SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Symbol))
            {
                var symbolGalleryService = SymbolsGalleryService.Create();
                totalRecord = symbolGalleryService.GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbols = symbolGalleryService.GetSymbolsCollection(subType, pageSize, offset);
                SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Title))
            {
                var titlesGalleryService = TitlesGalleryService.Create();
                totalRecord = titlesGalleryService.GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset);
                TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardMilitary))
            {
                var cardMilitaryGalleryService = CardMilitaryGalleryService.Create();
                totalRecord = cardMilitaryGalleryService.GetCardMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardMilitary> cardMilitaries = cardMilitaryGalleryService.GetCardMilitaryCollection(subType, pageSize, offset);
                CardMilitaryGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardSpell))
            {
                var cardSpellGalleryService = CardSpellGalleryService.Create();
                totalRecord = cardSpellGalleryService.GetCardSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardSpell> cardSpells = cardSpellGalleryService.GetCardSpellCollection(subType, pageSize, offset);
                CardSpellGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.MagicFormationCircle))
            {
                var magicFormationCircleGalleryService = MagicFormationCircleGalleryService.Create();
                totalRecord = magicFormationCircleGalleryService.GetMagicFormationCircleCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<MagicFormationCircle> magicFormationCircles = magicFormationCircleGalleryService.GetMagicFormationCircleCollection(subType, pageSize, offset);
                MagicFormationCircleGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Relic))
            {
                var relicsGalleryService = RelicsGalleryService.Create();
                totalRecord = relicsGalleryService.GetRelicsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Relics> relics = relicsGalleryService.GetRelicsCollection(subType, pageSize, offset);
                RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardColonel))
            {
                var cardColonelsGalleryService = CardColonelsGalleryService.Create();
                totalRecord = cardColonelsGalleryService.GetCardColonelsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardColonels> cardColonels = cardColonelsGalleryService.GetCardColonelsCollection(subType, pageSize, offset);
                CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardGeneral))
            {
                var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
                totalRecord = cardGeneralsGalleryService.GetCardGeneralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardGenerals> cardGenerals = cardGeneralsGalleryService.GetCardGeneralsCollection(subType, pageSize, offset);
                CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardAdmiral))
            {
                var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
                totalRecord = cardAdmiralsGalleryService.GetCardAdmiralsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardAdmirals> cardAdmirals = cardAdmiralsGalleryService.GetCardAdmiralsCollection(subType, pageSize, offset);
                CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Border))
            {
                var bordersGalleryService = BordersGalleryService.Create();
                totalRecord = bordersGalleryService.GetBordersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset);
                BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Talisman))
            {
                var talismanGalleryService = TalismanGalleryService.Create();
                totalRecord = talismanGalleryService.GetTalismanCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Talisman> talismans = talismanGalleryService.GetTalismanCollection(subType, pageSize, offset);
                TalismanGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Puppet))
            {
                var puppetGalleryService = PuppetGalleryService.Create();
                totalRecord = puppetGalleryService.GetPuppetCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Puppet> puppets = puppetGalleryService.GetPuppetCollection(subType, pageSize, offset);
                PuppetGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Alchemy))
            {
                var alchemyGalleryService = AlchemyGalleryService.Create();
                totalRecord = alchemyGalleryService.GetAlchemyCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Alchemy> alchemies = alchemyGalleryService.GetAlchemyCollection(subType, pageSize, offset);
                AlchemyGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Forge))
            {
                var forgeGalleryService = ForgeGalleryService.Create();
                totalRecord = forgeGalleryService.GetForgeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Forge> forges = forgeGalleryService.GetForgeCollection(subType, pageSize, offset);
                ForgeGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.CardLife))
            {
                var cardLifeGalleryService = CardLifeGalleryService.Create();
                totalRecord = cardLifeGalleryService.GetCardLifeCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CardLife> cardLives = cardLifeGalleryService.GetCardLifeCollection(subType, pageSize, offset);
                CardLifeGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);
            }
            else if (mainType.Equals(AppConstants.Artwork))
            {
                var artworkGalleryService = ArtworkGalleryService.Create();
                totalRecord = artworkGalleryService.GetArtworkCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Artwork> artworks = artworkGalleryService.GetArtworkCollection(subType, pageSize, offset);
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
