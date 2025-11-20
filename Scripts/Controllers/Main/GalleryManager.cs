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
    private Material UI_Blue_Gradient_Radius_Mat_MaskPercent_45;
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
    public void CreateGallery(Transform GalleryMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        rare = AppConstants.Rare.ALL;
        galleryMenuPanel = GalleryMenuPanel;
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        DictionaryPanel = UIManager.Instance.GetGameObject("DictionaryPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        UI_Blue_Gradient_Radius_Mat_MaskPercent_45 = MaterialManager.Instance.GetBlueMaterial("UI_Blue_Gradient_Radius_Mat_MaskPercent_45");

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
        // GetCardsType();
    }
    void AssignButtonEvent(string buttonName, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = galleryMenuPanel.Find(buttonName);
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
        mainType = type; // Gán giá trị cho mainType
        GetButtonType(); // Gọi hàm xử lý
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
        topBackgroundImage.material = UI_Blue_Gradient_Radius_Mat_MaskPercent_45;
        TextMeshProUGUI subTitleText = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
        subTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.GALLERY);

        Transform CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");
        List<Currencies> currencies = new List<Currencies>();
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
                // Tạo một nút mới từ prefab
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
                List<Collaborations> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset, rare);
                CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

                totalRecord = CollaborationService.Create().GetCollaborationCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.MEDAL))
            {
                List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset, rare);
                MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

                totalRecord = MedalsService.Create().GetMedalsCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.TITLE))
            {
                List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset, rare);
                TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

                totalRecord = TitlesService.Create().GetTitlesCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.BORDER))
            {
                List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset, rare);
                BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

                totalRecord = BordersService.Create().GetBordersCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
            {
                List<SpiritBeasts> spiritBeasts = SpiritBeastService.Create().GetSpiritBeast(pageSize, offset, rare);
                SpiritBeastController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);

                totalRecord = SpiritBeastService.Create().GetSpiritBeastCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.AVATAR))
            {
                List<Achievements> avatars = AvatarsService.Create().GetAvatars(pageSize, offset, rare);
                AvatarsController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

                totalRecord = AvatarsService.Create().GetAvatarsCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
            {
                List<Achievements> achievements = AchievementsService.Create().GetAchievement(pageSize, offset, rare);
                AchievementsController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

                totalRecord = AvatarsService.Create().GetAvatarsCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.CARD))
            {
                List<Cards> cards = CardsService.Create().GetCards(pageSize, offset, rare);
                CardsController.Instance.CreateCardsGallery(cards, DictionaryContentPanel);

                totalRecord = CardsService.Create().GetCardsCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
            {
                List<Architectures> architectures = ArchitecturesService.Create().GetArchitectures(pageSize, offset, rare);
                ArchitecturesController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

                totalRecord = ArchitecturesService.Create().GetArchitecturesCount(rare);
            }
            else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
            {
                List<Technologies> technologies = TechnologiesService.Create().GetTechnologies(pageSize, offset, rare);
                TechnologiesController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

                totalRecord = TechnologiesService.Create().GetTechnologiesCount(rare);
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
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL); // Giả sử bạn có texture trắng
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

            List<CardHeroes> cards = CardHeroesService.Create().GetCardHeroes(type, pageSize, offset, rare);
            CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);

            totalRecord = CardHeroesService.Create().GetCardHeroesCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {

            List<Books> books = BooksService.Create().GetBooks(type, pageSize, offset, rare);
            BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = BooksService.Create().GetBooksCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {

            List<CardCaptains> captains = CardCaptainsService.Create().GetCardCaptains(type, pageSize, offset, rare);
            CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

            totalRecord = CardCaptainsService.Create().GetCardCaptainsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {

            List<CollaborationEquipments> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipments(type, pageSize, offset, rare);
            CollaborationEquipmentController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {

            List<Equipments> equipments = EquipmentsService.Create().GetEquipments(type, pageSize, offset, rare);
            EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = EquipmentsService.Create().GetEquipmentsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {

            List<Pets> pets = PetsService.Create().GetPets(type, pageSize, offset, rare);
            PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = PetsService.Create().GetPetsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {

            List<Skills> skills = SkillsService.Create().GetSkills(type, pageSize, offset, rare);
            SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = SkillsService.Create().GetSkillsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {

            List<Symbols> symbols = SymbolsService.Create().GetSymbols(type, pageSize, offset, rare);
            SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = SymbolsService.Create().GetSymbolsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {

            List<CardMilitaries> cardMilitaries = CardMilitaryService.Create().GetCardMilitary(type, pageSize, offset, rare);
            CardMilitaryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = CardMilitaryService.Create().GetCardMilitaryCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {

            List<CardSpells> cardSpells = CardSpellService.Create().GetCardSpell(type, pageSize, offset, rare);
            CardSpellController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = CardSpellService.Create().GetCardSpellCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            List<Collaborations> collaborations = CollaborationService.Create().GetCollaboration(pageSize, offset, rare);
            CollaborationController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

            totalRecord = CollaborationService.Create().GetCollaborationCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            List<Medals> medalsList = MedalsService.Create().GetMedals(pageSize, offset, rare);
            MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

            totalRecord = MedalsService.Create().GetMedalsCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            List<Titles> titles = TitlesService.Create().GetTitles(pageSize, offset, rare);
            TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = TitlesService.Create().GetTitlesCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            List<Borders> borders = BordersService.Create().GetBorders(pageSize, offset, rare);
            BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = BordersService.Create().GetBordersCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {

            List<MagicFormationCircles> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircle(type, pageSize, offset, rare);
            MagicFormationCircleController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {

            List<Relics> relics = RelicsService.Create().GetRelics(type, pageSize, offset, rare);
            RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = RelicsService.Create().GetRelicsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {

            List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonsters(type, pageSize, offset, rare);
            CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

            totalRecord = CardMonstersService.Create().GetCardMonstersCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {

            List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonels(type, pageSize, offset, rare);
            CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = CardColonelsService.Create().GetCardColonelsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {

            List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGenerals(type, pageSize, offset, rare);
            CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = CardGeneralsService.Create().GetCardGeneralsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {

            List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmirals(type, pageSize, offset, rare);
            CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = CardAdmiralsService.Create().GetCardAdmiralsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            List<Talismans> talismans = TalismanService.Create().GetTalisman(type, pageSize, offset, rare);
            TalismanController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = TalismanService.Create().GetTalismanCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            List<Puppets> puppets = PuppetService.Create().GetPuppet(type, pageSize, offset, rare);
            PuppetController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = PuppetService.Create().GetPuppetCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            List<Alchemies> alchemies = AlchemyService.Create().GetAlchemy(type, pageSize, offset, rare);
            AlchemyController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = AlchemyService.Create().GetAlchemyCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            List<Forges> forges = ForgeService.Create().GetForge(type, pageSize, offset, rare);
            ForgeController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = ForgeService.Create().GetForgeCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            List<CardLives> cardLives = CardLifeService.Create().GetCardLife(type, pageSize, offset, rare);
            CardLifeController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = CardLifeService.Create().GetCardLifeCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            List<Artworks> artworks = ArtworkService.Create().GetArtwork(type, pageSize, offset, rare);
            ArtworkController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

            totalRecord = ArtworkService.Create().GetArtworkCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            List<SpiritBeasts> spiritBeasts = SpiritBeastService.Create().GetSpiritBeast(pageSize, offset, rare);
            SpiritBeastController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = SpiritBeastService.Create().GetSpiritBeastCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            List<Achievements> avatars = AvatarsService.Create().GetAvatars(pageSize, offset, rare);
            AvatarsController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

            totalRecord = AvatarsService.Create().GetAvatarsCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            List<SpiritCards> spiritCards = SpiritCardService.Create().GetSpiritCard(type, pageSize, offset, rare);
            SpiritCardController.Instance.CreateSpiritCardGallery(spiritCards, DictionaryContentPanel);

            totalRecord = SpiritCardService.Create().GetSpiritCardCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            List<Achievements> achievements = AchievementsService.Create().GetAchievement(pageSize, offset, rare);
            AchievementsController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

            totalRecord = AvatarsService.Create().GetAvatarsCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD))
        {
            List<Cards> cards = CardsService.Create().GetCards(pageSize, offset, rare);
            CardsController.Instance.CreateCardsGallery(cards, DictionaryContentPanel);

            totalRecord = CardsService.Create().GetCardsCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            List<Architectures> architectures = ArchitecturesService.Create().GetArchitectures(pageSize, offset, rare);
            ArchitecturesController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

            totalRecord = ArchitecturesService.Create().GetArchitecturesCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            List<Technologies> technologies = TechnologiesService.Create().GetTechnologies(pageSize, offset, rare);
            TechnologiesController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

            totalRecord = TechnologiesService.Create().GetTechnologiesCount(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            List<Vehicles> vehicles = VehiclesService.Create().GetVehicles(type, pageSize, offset, rare);
            VehiclesController.Instance.CreateVehicleGallery(vehicles, DictionaryContentPanel);

            totalRecord = VehiclesService.Create().GetVehicleCount(type, rare);
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
