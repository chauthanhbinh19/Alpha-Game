using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Reflection;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class GalleryManager : MonoBehaviour
{
    private Transform galleryMenuPanel;
    private GameObject buttonPrefab;
    private GameObject DictionaryPanel;
    private GameObject RareButtonPrefab;
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
        RareButtonPrefab = UIManager.Instance.GetGeneralButton("RareButtonPrefab");
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
        AssignButtonEvent("Button_35", () => GetType(AppConstants.MainType.CORE));
        AssignButtonEvent("Button_36", () => GetType(AppConstants.MainType.WEAPON));
        AssignButtonEvent("Button_37", () => GetType(AppConstants.MainType.ROBOT));
        AssignButtonEvent("Button_38", () => GetType(AppConstants.MainType.BADGE));
        AssignButtonEvent("Button_39", () => GetType(AppConstants.MainType.MECHA_BEAST));
        AssignButtonEvent("Button_40", () => GetType(AppConstants.MainType.RUNE));
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
        _=GetButtonTypeAsync(); // Gọi hàm xử lý
        titleText.text = LocalizationManager.Get(type);
    }
    public async Task GetButtonTypeAsync()
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
        currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
        FindObjectOfType<CurrenciesManager>().GetMainCurrency(currencies, CurrencyPanel);

        List<string> uniqueRaries = QualityEvaluator.rarities;
        if (uniqueRaries.Count > 0)
        {
            for (int i = 0; i < uniqueRaries.Count; i++)
            {
                string selectedRare = uniqueRaries[i];
                string rareTemp = selectedRare;
                GameObject button = Instantiate(RareButtonPrefab, RightScrollViewContentPanel);

                TextMeshProUGUI buttonText = button.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
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
                    button.transform.Find("Active").gameObject.SetActive(true);
                    button.transform.Find("Unactive").gameObject.SetActive(false);
                    // LoadCurrentPage();
                }
                else
                {
                    button.transform.Find("Active").gameObject.SetActive(false);
                    button.transform.Find("Unactive").gameObject.SetActive(true);
                }
            }
        }

        List<string> uniqueTypes = await TypeManager.GetUniqueTypesAsync(mainType);
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
                    _=LoadCurrentPageAsync();
                }
                else
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
            }
        }
        else
        {
            _=LoadCurrentPageAsync();
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
                        rare = QualityEvaluator.rarities[0];
                        rareButton.transform.Find("Active").gameObject.SetActive(true);
                        rareButton.transform.Find("Unactive").gameObject.SetActive(false);
                    }
                    else
                    {
                        rareButton.transform.Find("Active").gameObject.SetActive(false);
                        rareButton.transform.Find("Unactive").gameObject.SetActive(true);
                    }
                }
            }
        }

        _=LoadCurrentPageAsync();
    }
    public void OnRareTabButtonClick(GameObject clickedButton, string selectedRare)
    {
        foreach (Transform child in RightScrollViewContentPanel)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.transform.Find("Active").gameObject.SetActive(false);
                button.transform.Find("Unactive").gameObject.SetActive(true);
            }
        }

        rare = selectedRare;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        clickedButton.transform.Find("Active").gameObject.SetActive(true);
        clickedButton.transform.Find("Unactive").gameObject.SetActive(false);
        _=LoadCurrentPageAsync();
    }
    public async Task LoadCurrentPageAsync()
    {
        int totalRecord = 0;

        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {

            List<CardHeroes> cards = await CardHeroesService.Create().GetCardHeroesAsync(type, pageSize, offset, rare);
            CardHeroesController.Instance.CreateCardHeroesGallery(cards, DictionaryContentPanel);

            totalRecord = await CardHeroesService.Create().GetCardHeroesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {

            List<Books> books = await BooksService.Create().GetBooksAsync(type, pageSize, offset, rare);
            BooksController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = await BooksService.Create().GetBooksCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {

            List<CardCaptains> captains = await CardCaptainsService.Create().GetCardCaptainsAsync(type, pageSize, offset, rare);
            CardCaptainsController.Instance.CreateCardCaptainsGallery(captains, DictionaryContentPanel);

            totalRecord = await CardCaptainsService.Create().GetCardCaptainsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {

            List<CollaborationEquipments> collaborationEquipments = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsAsync(type, pageSize, offset, rare);
            CollaborationEquipmentsController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {

            List<Equipments> equipments = await EquipmentsService.Create().GetEquipmentsAsync(type, pageSize, offset, rare);
            EquipmentsController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = await EquipmentsService.Create().GetEquipmentsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {

            List<Pets> pets = await PetsService.Create().GetPetsAsync(type, pageSize, offset, rare);
            PetsController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = await PetsService.Create().GetPetsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {

            List<Skills> skills = await SkillsService.Create().GetSkillsAsync(type, pageSize, offset, rare);
            SkillsController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = await SkillsService.Create().GetSkillsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {

            List<Symbols> symbols = await SymbolsService.Create().GetSymbolsAsync(type, pageSize, offset, rare);
            SymbolsController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = await SymbolsService.Create().GetSymbolsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {

            List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create().GetCardMilitariesAsync(type, pageSize, offset, rare);
            CardMilitariesController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = await CardMilitariesService.Create().GetCardMilitariesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {

            List<CardSpells> cardSpells = await CardSpellsService.Create().GetCardSpellsAsync(type, pageSize, offset, rare);
            CardSpellsController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = await CardSpellsService.Create().GetCardSpellsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            List<Collaborations> collaborations = await CollaborationsService.Create().GetCollaborationsAsync(pageSize, offset, rare);
            CollaborationsController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

            totalRecord = await CollaborationsService.Create().GetCollaborationsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            List<Medals> medalsList = await MedalsService.Create().GetMedalsAsync(pageSize, offset, rare);
            MedalsController.Instance.CreateMedalsGallery(medalsList, DictionaryContentPanel);

            totalRecord = await MedalsService.Create().GetMedalsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            List<Titles> titles = await TitlesService.Create().GetTitlesAsync(pageSize, offset, rare);
            TitlesController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = await TitlesService.Create().GetTitlesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            List<Borders> borders = await BordersService.Create().GetBordersAsync(pageSize, offset, rare);
            BordersController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = await BordersService.Create().GetBordersCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {

            List<MagicFormationCircles> magicFormationCircles = await MagicFormationCirclesService.Create().GetMagicFormationCirclesAsync(type, pageSize, offset, rare);
            MagicFormationCirclesController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = await MagicFormationCirclesService.Create().GetMagicFormationCirclesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {

            List<Relics> relics = await RelicsService.Create().GetRelicsAsync(type, pageSize, offset, rare);
            RelicsController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = await RelicsService.Create().GetRelicsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {

            List<CardMonsters> cardMonsters = await CardMonstersService.Create().GetCardMonstersAsync(type, pageSize, offset, rare);
            CardMonstersController.Instance.CreateCardMonstersGallery(cardMonsters, DictionaryContentPanel);

            totalRecord = await CardMonstersService.Create().GetCardMonstersCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {

            List<CardColonels> cardColonels = await CardColonelsService.Create().GetCardColonelsAsync(type, pageSize, offset, rare);
            CardColonelsController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = await CardColonelsService.Create().GetCardColonelsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {

            List<CardGenerals> cardGenerals = await CardGeneralsService.Create().GetCardGeneralsAsync(type, pageSize, offset, rare);
            CardGeneralsController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = await CardGeneralsService.Create().GetCardGeneralsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {

            List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create().GetCardAdmiralsAsync(type, pageSize, offset, rare);
            CardAdmiralsController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = await CardAdmiralsService.Create().GetCardAdmiralsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            List<Talismans> talismans = await TalismansService.Create().GetTalismansAsync(type, pageSize, offset, rare);
            TalismansController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = await TalismansService.Create().GetTalismansCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            List<Puppets> puppets = await PuppetsService.Create().GetPuppetsAsync(type, pageSize, offset, rare);
            PuppetsController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = await PuppetsService.Create().GetPuppetsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            List<Alchemies> alchemies = await AlchemiesService.Create().GetAlchemiesAsync(type, pageSize, offset, rare);
            AlchemiesController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = await AlchemiesService.Create().GetAlchemiesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            List<Forges> forges = await ForgesService.Create().GetForgesAsync(type, pageSize, offset, rare);
            ForgesController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = await ForgesService.Create().GetForgesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            List<CardLives> cardLives = await CardLivesService.Create().GetCardLivesAsync(type, pageSize, offset, rare);
            CardLivesController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = await CardLivesService.Create().GetCardLivesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            List<Artworks> artworks = await ArtworksService.Create().GetArtworksAsync(type, pageSize, offset, rare);
            ArtworksController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

            totalRecord = await ArtworksService.Create().GetArtworksCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            List<SpiritBeasts> spiritBeasts = await SpiritBeastsService.Create().GetSpiritBeastsAsync(pageSize, offset, rare);
            SpiritBeastsController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = await SpiritBeastsService.Create().GetSpiritBeastsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            List<Avatars> avatars = await AvatarsService.Create().GetAvatarsAsync(pageSize, offset, rare);
            AvatarsController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

            totalRecord = await AvatarsService.Create().GetAvatarsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            List<SpiritCards> spiritCards = await SpiritCardsService.Create().GetSpiritCardsAsync(type, pageSize, offset, rare);
            SpiritCardsController.Instance.CreateSpiritCardGallery(spiritCards, DictionaryContentPanel);

            totalRecord = await SpiritCardsService.Create().GetSpiritCardsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            List<Achievements> achievements = await AchievementsService.Create().GetAchievementsAsync(pageSize, offset, rare);
            AchievementsController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

            totalRecord = await AvatarsService.Create().GetAvatarsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD))
        {
            List<Cards> cards = await CardsService.Create().GetCardsAsync(pageSize, offset, rare);
            CardsController.Instance.CreateCardsGallery(cards, DictionaryContentPanel);

            totalRecord = await CardsService.Create().GetCardsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            List<Architectures> architectures = await ArchitecturesService.Create().GetArchitecturesAsync(pageSize, offset, rare);
            ArchitecturesController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

            totalRecord = await ArchitecturesService.Create().GetArchitecturesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            List<Technologies> technologies = await TechnologiesService.Create().GetTechnologiesAsync(pageSize, offset, rare);
            TechnologiesController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

            totalRecord = await TechnologiesService.Create().GetTechnologiesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            List<Vehicles> vehicles = await VehiclesService.Create().GetVehiclesAsync(type, pageSize, offset, rare);
            VehiclesController.Instance.CreateVehicleGallery(vehicles, DictionaryContentPanel);

            totalRecord = await VehiclesService.Create().GetVehiclesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            List<Cores> cores = await CoresService.Create().GetCoresAsync(pageSize, offset, rare);
            CoresController.Instance.CreateCoresGallery(cores, DictionaryContentPanel);

            totalRecord = await CoresService.Create().GetCoresCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            List<Weapons> weapons = await WeaponsService.Create().GetWeaponsAsync(pageSize, offset, rare);
            WeaponsController.Instance.CreateWeaponsGallery(weapons, DictionaryContentPanel);

            totalRecord = await WeaponsService.Create().GetWeaponsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            List<Robots> robots = await RobotsService.Create().GetRobotsAsync(pageSize, offset, rare);
            RobotsController.Instance.CreateRobotsGallery(robots, DictionaryContentPanel);

            totalRecord = await RobotsService.Create().GetRobotsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            List<Badges> badges = await BadgesService.Create().GetBadgesAsync(pageSize, offset, rare);
            BadgesController.Instance.CreateBadgesGallery(badges, DictionaryContentPanel);

            totalRecord = await BadgesService.Create().GetBadgesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            List<MechaBeasts> mechaBeasts = await MechaBeastsService.Create().GetMechaBeastsAsync(pageSize, offset, rare);
            MechaBeastsController.Instance.CreateMechaBeastsGallery(mechaBeasts, DictionaryContentPanel);

            totalRecord = await MechaBeastsService.Create().GetMechaBeastsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            List<Runes> runes = await RunesService.Create().GetRunesAsync(pageSize, offset, rare);
            RunesController.Instance.CreateRunesGallery(runes, DictionaryContentPanel);

            totalRecord = await RunesService.Create().GetRunesCountAsync(rare);
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
            _=LoadCurrentPageAsync();

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
            _=LoadCurrentPageAsync();
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
