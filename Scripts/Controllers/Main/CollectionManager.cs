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
    private Material UI_Green_Gradient_Radius_Mat_MaskPercent_45;
    private Button CloseButton;
    private Button HomeButton;
    //Variable for pagination
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private TextMeshProUGUI PageText;
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
        rare = AppConstants.Rare.ALL;
        collectionMenuPanel = CollectionMenuPanel;
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        DictionaryPanel = UIManager.Instance.GetGameObject("DictionaryPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        UI_Green_Gradient_Radius_Mat_MaskPercent_45 = MaterialManager.Instance.GetGreenMaterial("UI_Green_Gradient_Radius_Mat_MaskPercent_45");

        AssignButtonEvent("Button_1", () => GetType(AppConstants.MainType.CARD_HERO));
        AssignButtonEvent("Button_2", () => GetType(AppConstants.MainType.BOOK));
        AssignButtonEvent("Button_3", () => GetType(AppConstants.MainType.PET));
        AssignButtonEvent("Button_4", () => GetType(AppConstants.MainType.CARD_CAPTAIN));
        AssignButtonEvent("Button_5", () => GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT));
        AssignButtonEvent("Button_6", () => GetType(AppConstants.MainType.CARD_MILITARY));
        AssignButtonEvent("Button_7", () => GetType(AppConstants.MainType.CARD_SPELL));
        AssignButtonEvent("Button_8", () => GetType(AppConstants.MainType.COLLABORATION));
        AssignButtonEvent("Button_9", () => GetType(AppConstants.MainType.CARD_MONSTER));
        AssignButtonEvent("Button_10", () => GetType(AppConstants.MainType.EQUIPMENT));
        AssignButtonEvent("Button_11", () => GetType(AppConstants.MainType.MEDAL));
        AssignButtonEvent("Button_12", () => GetType(AppConstants.MainType.SKILL));
        AssignButtonEvent("Button_13", () => GetType(AppConstants.MainType.SYMBOL));
        AssignButtonEvent("Button_14", () => GetType(AppConstants.MainType.TITLE));
        AssignButtonEvent("Button_15", () => GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE));
        AssignButtonEvent("Button_16", () => GetType(AppConstants.MainType.RELIC));
        AssignButtonEvent("Button_17", () => GetType(AppConstants.MainType.CARD_COLONEL));
        AssignButtonEvent("Button_18", () => GetType(AppConstants.MainType.CARD_GENERAL));
        AssignButtonEvent("Button_19", () => GetType(AppConstants.MainType.CARD_ADMIRAL));
        AssignButtonEvent("Button_20", () => GetType(AppConstants.MainType.BORDER));
        AssignButtonEvent("Button_21", () => GetType(AppConstants.MainType.TALISMAN));
        AssignButtonEvent("Button_22", () => GetType(AppConstants.MainType.PUPPET));
        AssignButtonEvent("Button_23", () => GetType(AppConstants.MainType.ALCHEMY));
        AssignButtonEvent("Button_24", () => GetType(AppConstants.MainType.FORGE));
        AssignButtonEvent("Button_25", () => GetType(AppConstants.MainType.CARD_LIFE));
        AssignButtonEvent("Button_26", () => GetType(AppConstants.MainType.ARTWORK));
        AssignButtonEvent("Button_27", () => GetType(AppConstants.MainType.SPIRIT_BEAST));
        AssignButtonEvent("Button_28", () => GetType(AppConstants.MainType.AVATAR));
        AssignButtonEvent("Button_29", () => GetType(AppConstants.MainType.SPIRIT_CARD));
        AssignButtonEvent("Button_30", () => GetType(AppConstants.MainType.ACHIEVEMENT));
        AssignButtonEvent("Button_31", () => GetType(AppConstants.MainType.CARD));
        AssignButtonEvent("Button_32", () => GetType(AppConstants.MainType.ARCHITECTURE));
        AssignButtonEvent("Button_33", () => GetType(AppConstants.MainType.TECHNOLOGY));
        AssignButtonEvent("Button_34", () => GetType(AppConstants.MainType.VEHICLE));
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
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    action();
                });
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
        GameObject mainMenuObject = Instantiate(DictionaryPanel, MainPanel);
        DictionaryContentPanel = mainMenuObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
        RightScrollViewContentPanel = mainMenuObject.transform.Find("RightScrollView/Viewport/Content");
        LeftScrollViewContentPanel = mainMenuObject.transform.Find("Scroll View/Viewport/ButtonContent");
        PageText = mainMenuObject.transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = mainMenuObject.transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = mainMenuObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = mainMenuObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = mainMenuObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePanel();
            Destroy(mainMenuObject);
        });
        HomeButton = mainMenuObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(MainPanel);
        });
        NextButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            ChangeNextPage();
        });
        PreviousButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            ChangePreviousPage();
        });

        RawImage topBackgroundImage = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TopBackground").GetComponent<RawImage>();
        topBackgroundImage.material = UI_Green_Gradient_Radius_Mat_MaskPercent_45;
        TextMeshProUGUI subTitleText = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
        subTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLECTION);

        Transform CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");
        IUserCurrencyRepository userCurrencyRepository = new UserCurrencyRepository();
        UserCurrencyService userCurrencyService = new UserCurrencyService(userCurrencyRepository);
        List<Currencies> currencies = new List<Currencies>();
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
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    OnRareTabButtonClick(button, rareTemp);
                });

                if (i == 0)
                {
                    rare = selectedRare;
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.RARE_BUTTON_AFTER_CLICK_URL);
                    // LoadCurrentPage();
                }
                else
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.RARE_BUTTON_BEFORE_CLICK_URL);
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
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    OnButtonClick(button, subType);
                });

                if (i == 0)
                {
                    type = subType;
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    LoadCurrentPage();
                }
                else
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
            }
        }
        else
        {
            int totalRecord = 0;
            if (mainType.Equals(AppConstants.MainType.COLLABORATION))
            {
                var collaborationGalleryService = CollaborationGalleryService.Create();
                List<Collaborations> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset, rare);
                CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

                totalRecord = collaborationGalleryService.GetCollaborationCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.MEDAL))
            {
                var medalsGalleryService = MedalsGalleryService.Create();
                List<Medals> medals = medalsGalleryService.GetMedalsCollection(pageSize, offset, rare);
                MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);

                totalRecord = medalsGalleryService.GetMedalsCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.TITLE))
            {
                var titlesGalleryService = TitlesGalleryService.Create();
                List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset, rare);
                TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

                totalRecord = titlesGalleryService.GetTitlesCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.BORDER))
            {
                var bordersGalleryService = BordersGalleryService.Create();
                List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset, rare);
                BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

                totalRecord = bordersGalleryService.GetBordersCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
            {
                var spiritBeastGalleryService = SpiritBeastGalleryService.Create();
                List<SpiritBeasts> spiritBeasts = spiritBeastGalleryService.GetSpiritBeastCollection(pageSize, offset, rare);
                SpiritBeastGalleryController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);

                totalRecord = spiritBeastGalleryService.GetSpiritBeastCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.AVATAR))
            {
                var avatarsGalleryService = AvatarsGalleryService.Create();
                List<Achievements> avatars = avatarsGalleryService.GetAvatarsCollection(pageSize, offset, rare);
                AvatarsGalleryController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

                totalRecord = avatarsGalleryService.GetAvatarsCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
            {
                var achievementsGalleryService = AchievementsGalleryService.Create();
                List<Achievements> achievements = achievementsGalleryService.GetAchievementCollection(pageSize, offset, rare);
                AchievementsGalleryController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

                totalRecord = achievementsGalleryService.GetAchievementsCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD))
            {
                var cardsGalleryService = CardsGalleryService.Create();
                List<Cards> cards = cardsGalleryService.GetCardsCollection(pageSize, offset, rare);
                CardsGalleryController.Instance.CreateCardsGallery(cards, DictionaryContentPanel);

                totalRecord = cardsGalleryService.GetCardsCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
            {
                var architecturesGalleryService = ArchitecturesGalleryService.Create();
                List<Architectures> architectures = architecturesGalleryService.GetArchitecturesCollection(pageSize, offset, rare);
                ArchitecturesGalleryController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

                totalRecord = architecturesGalleryService.GetArchitecturesCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
            {
                var technologiesGalleryService = TechnologiesGalleryService.Create();
                List<Technologies> technologies = technologiesGalleryService.GetTechnologiesCollection(pageSize, offset, rare);
                TechnologiesGalleryController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

                totalRecord = technologiesGalleryService.GetTechnologiesCount(rare);
            }
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        }
        LoadAnimation();
    }
    void OnButtonClick(GameObject clickedButton, string subType)
    {
        foreach (Transform child in LeftScrollViewContentPanel)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
            }
        }

        type = subType;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);

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
                        rare = QualityEvaluator.rarities[0]; // hoặc AppConstants.MainType.All
                        ButtonLoader.Instance.ChangeButtonBackground(child.gameObject, ImageConstants.Button.RARE_BUTTON_AFTER_CLICK_URL);
                    }
                    else
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(child.gameObject, ImageConstants.Button.RARE_BUTTON_BEFORE_CLICK_URL);
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
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.RARE_BUTTON_BEFORE_CLICK_URL);
            }
        }

        rare = selectedRare;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.RARE_BUTTON_AFTER_CLICK_URL);
        LoadCurrentPage();
    }
    public void LoadCurrentPage()
    {
        int totalRecord = 0;

        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            var cardHeroesGalleryService = CardHeroesGalleryService.Create();
            List<CardHeroes> cardHeroes = cardHeroesGalleryService.GetCardHeroesCollection(type, pageSize, offset, rare);
            CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);

            totalRecord = cardHeroesGalleryService.GetCardHeroesCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            var booksGalleryService = BooksGalleryService.Create();
            List<Books> books = booksGalleryService.GetBooksCollection(type, pageSize, offset, rare);
            BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = booksGalleryService.GetBooksCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
            List<CardCaptains> cardCaptains = cardCaptainsGalleryService.GetCardCaptainsCollection(type, pageSize, offset, rare);
            CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);

            totalRecord = cardCaptainsGalleryService.GetCardCaptainsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            var collaborationEquipmentGalleryService = CollaborationEquipmentGalleryService.Create();
            List<CollaborationEquipments> collaborationEquipments = collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollection(type, pageSize, offset, rare);
            CollaborationEquipmentGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = collaborationEquipmentGalleryService.GetCollaborationEquipmentCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            var equipmentsGalleryService = EquipmentsGalleryService.Create();
            List<Equipments> equipments = equipmentsGalleryService.GetEquipmentsCollection(type, pageSize, offset, rare);
            EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = equipmentsGalleryService.GetEquipmentsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            var petsGalleryService = PetsGalleryService.Create();
            List<Pets> pets = petsGalleryService.GetPetsCollection(type, pageSize, offset, rare);
            PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = petsGalleryService.GetPetsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            var skillsGalleryService = SkillsGalleryService.Create();
            List<Skills> skills = skillsGalleryService.GetSkillsCollection(type, pageSize, offset, rare);
            SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = skillsGalleryService.GetSkillsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            var symbolsGalleryService = SymbolsGalleryService.Create();
            List<Symbols> symbols = symbolsGalleryService.GetSymbolsCollection(type, pageSize, offset, rare);
            SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = symbolsGalleryService.GetSymbolsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            var cardMilitaryGalleryService = CardMilitaryGalleryService.Create();
            List<CardMilitaries> cardMilitaries = cardMilitaryGalleryService.GetCardMilitaryCollection(type, pageSize, offset, rare);
            CardMilitaryGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = cardMilitaryGalleryService.GetCardMilitaryCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            var cardSpellGalleryService = CardSpellGalleryService.Create();
            List<CardSpells> cardSpells = cardSpellGalleryService.GetCardSpellCollection(type, pageSize, offset, rare);
            CardSpellGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = cardSpellGalleryService.GetCardSpellCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            var collaborationGalleryService = CollaborationGalleryService.Create();
            List<Collaborations> collaborations = collaborationGalleryService.GetCollaborationCollection(pageSize, offset, rare);
            CollaborationGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

            totalRecord = collaborationGalleryService.GetCollaborationCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            var medalsGalleryService = MedalsGalleryService.Create();
            List<Medals> medals = medalsGalleryService.GetMedalsCollection(pageSize, offset, rare);
            MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);

            totalRecord = medalsGalleryService.GetMedalsCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            var titlesGalleryService = TitlesGalleryService.Create();
            List<Titles> titles = titlesGalleryService.GetTitlesCollection(pageSize, offset, rare);
            TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = titlesGalleryService.GetTitlesCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            var bordersGalleryService = BordersGalleryService.Create();
            List<Borders> borders = bordersGalleryService.GetBordersCollection(pageSize, offset, rare);
            BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = bordersGalleryService.GetBordersCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            var magicFormationCircleGalleryService = MagicFormationCircleGalleryService.Create();
            List<MagicFormationCircles> magicFormationCircles = magicFormationCircleGalleryService.GetMagicFormationCircleCollection(type, pageSize, offset, rare);
            MagicFormationCircleGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = magicFormationCircleGalleryService.GetMagicFormationCircleCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            var relicsGalleryService = RelicsGalleryService.Create();
            List<Relics> relics = relicsGalleryService.GetRelicsCollection(type, pageSize, offset, rare);
            RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = relicsGalleryService.GetRelicsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            var cardMonstersGalleryService = CardMonstersGalleryService.Create();
            List<CardMonsters> monsters = cardMonstersGalleryService.GetCardMonstersCollection(type, pageSize, offset, rare);
            CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);

            totalRecord = cardMonstersGalleryService.GetCardMonstersCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            var cardColonelsGalleryService = CardColonelsGalleryService.Create();
            List<CardColonels> cardColonels = cardColonelsGalleryService.GetCardColonelsCollection(type, pageSize, offset, rare);
            CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = cardColonelsGalleryService.GetCardColonelsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
            List<CardGenerals> cardGenerals = cardGeneralsGalleryService.GetCardGeneralsCollection(type, pageSize, offset, rare);
            CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = cardGeneralsGalleryService.GetCardGeneralsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
            List<CardAdmirals> cardAdmirals = cardAdmiralsGalleryService.GetCardAdmiralsCollection(type, pageSize, offset, rare);
            CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = cardAdmiralsGalleryService.GetCardAdmiralsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            var talismanGalleryService = TalismanGalleryService.Create();
            List<Talismans> talismans = talismanGalleryService.GetTalismanCollection(type, pageSize, offset, rare);
            TalismanGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = talismanGalleryService.GetTalismanCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            var puppetGalleryService = PuppetGalleryService.Create();
            List<Puppets> puppets = puppetGalleryService.GetPuppetCollection(type, pageSize, offset, rare);
            PuppetGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = puppetGalleryService.GetPuppetCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            var alchemyGalleryService = AlchemyGalleryService.Create();
            List<Alchemies> alchemies = alchemyGalleryService.GetAlchemyCollection(type, pageSize, offset, rare);
            AlchemyGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = alchemyGalleryService.GetAlchemyCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            var forgeGalleryService = ForgeGalleryService.Create();
            List<Forges> forges = forgeGalleryService.GetForgeCollection(type, pageSize, offset, rare);
            ForgeGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = forgeGalleryService.GetForgeCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            var cardLifeGalleryService = CardLifeGalleryService.Create();
            List<CardLives> cardLives = cardLifeGalleryService.GetCardLifeCollection(type, pageSize, offset, rare);
            CardLifeGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = cardLifeGalleryService.GetCardLifeCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            var artworkGalleryService = ArtworkGalleryService.Create();
            List<Artworks> artworks = artworkGalleryService.GetArtworkCollection(type, pageSize, offset, rare);
            ArtworkGalleryController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

            totalRecord = artworkGalleryService.GetArtworkCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            var spiritBeastGalleryService = SpiritBeastGalleryService.Create();
            List<SpiritBeasts> spiritBeasts = spiritBeastGalleryService.GetSpiritBeastCollection(pageSize, offset, rare);
            SpiritBeastGalleryController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = spiritBeastGalleryService.GetSpiritBeastCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            var avatarsGalleryService = AvatarsGalleryService.Create();
            List<Achievements> avatars = avatarsGalleryService.GetAvatarsCollection(pageSize, offset, rare);
            AvatarsGalleryController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

            totalRecord = avatarsGalleryService.GetAvatarsCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            var spiritCardGalleryService = SpiritCardGalleryService.Create();
            List<SpiritCards> spiritCards = spiritCardGalleryService.GetSpiritCardCollection(type, pageSize, offset, rare);
            SpiritCardGalleryController.Instance.CreateSpiritCardGallery(spiritCards, DictionaryContentPanel);

            totalRecord = spiritCardGalleryService.GetSpiritCardCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            var achievementsGalleryService = AchievementsGalleryService.Create();
            List<Achievements> achievements = achievementsGalleryService.GetAchievementCollection(pageSize, offset, rare);
            AchievementsGalleryController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

            totalRecord = achievementsGalleryService.GetAchievementsCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD))
        {
            var cardsGalleryService = CardsGalleryService.Create();
            List<Cards> cards = cardsGalleryService.GetCardsCollection(pageSize, offset, rare);
            CardsGalleryController.Instance.CreateCardsGallery(cards, DictionaryContentPanel);

            totalRecord = cardsGalleryService.GetCardsCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            var architecturesGalleryService = ArchitecturesGalleryService.Create();
            List<Architectures> architectures = architecturesGalleryService.GetArchitecturesCollection(pageSize, offset, rare);
            ArchitecturesGalleryController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

            totalRecord = architecturesGalleryService.GetArchitecturesCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            var technologiesGalleryService = TechnologiesGalleryService.Create();
            List<Technologies> technologies = technologiesGalleryService.GetTechnologiesCollection(pageSize, offset, rare);
            TechnologiesGalleryController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

            totalRecord = technologiesGalleryService.GetTechnologiesCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            var vehiclesGalleryService = VehicleGalleryService.Create();
            List<Vehicles> vehicles = vehiclesGalleryService.GetVehicleCollection(type, pageSize, offset, rare);
            VehicleGalleryController.Instance.CreateVehicleGallery(vehicles, DictionaryContentPanel);

            totalRecord = vehiclesGalleryService.GetVehicleCount(type, rare);
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
            currentPage = currentPage + 1;
            offset = offset + pageSize;
            LoadCurrentPage();

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage()
    {
        if (currentPage > 1)
        {
            ClearAllPrefabs();
            currentPage = currentPage - 1;
            offset = offset - pageSize;
            LoadCurrentPage();

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ClosePanel()
    {
        ClearAllButton();
        ClearAllPrefabs();
        offset = 0;
        currentPage = 1;
        // foreach (Transform child in MainPanel)
        // {
        //     Destroy(child.gameObject);
        // }
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
    public void LoadAnimation()
    {
        LeftScrollViewContentPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
        RightScrollViewContentPanel.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
}
