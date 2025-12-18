using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Threading.Tasks;

public class CollectionManager : MonoBehaviour
{
    private Transform collectionMenuPanel;
    private GameObject TypeButtonPrefab;
    private GameObject DictionaryPanelPrefab;
    private GameObject RareButtonPrefab;
    private Transform MainPanel;
    private Transform DictionaryContentPanel;
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
    private Material UI_Green_Gradient_Radius_Mat_MaskPercent_70;
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
    public void CreateCollection(Transform CollectionMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        rare = AppConstants.Rare.ALL;
        collectionMenuPanel = CollectionMenuPanel;
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        RareButtonPrefab = UIManager.Instance.Get("RareButtonPrefab");
        DictionaryPanelPrefab = UIManager.Instance.Get("DictionaryPanelPrefab");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        UI_Green_Gradient_Radius_Mat_MaskPercent_70 = MaterialManager.Instance.Get("UI_Green_Gradient_Radius_Mat_MaskPercent_70");

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
        AssignButtonEvent("Button_41", () => GetType(AppConstants.MainType.FURNITURE));
        AssignButtonEvent("Button_42", () => GetType(AppConstants.MainType.FOOD));
        AssignButtonEvent("Button_43", () => GetType(AppConstants.MainType.BEVERAGE));
        AssignButtonEvent("Button_44", () => GetType(AppConstants.MainType.BUILDING));
        AssignButtonEvent("Button_45", () => GetType(AppConstants.MainType.PLANT));
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
        _ = GetButtonTypeAsync();
        titleText.text = LocalizationManager.Get(type);
    }
    public async Task GetButtonTypeAsync()
    {
        // DictionaryPanel.SetActive(true);
        GameObject mainMenuObject = Instantiate(DictionaryPanelPrefab, MainPanel);
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

        Image topBackgroundImage = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TopBackground").GetComponent<Image>();
        topBackgroundImage.material = UI_Green_Gradient_Radius_Mat_MaskPercent_70;
        TextMeshProUGUI subTitleText = mainMenuObject.transform.Find("DictionaryCards/TitleGroup/TitleText").GetComponent<TextMeshProUGUI>();
        subTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLECTION);

        Transform CurrencyPanel = mainMenuObject.transform.Find("DictionaryCards/Currency");
        IUserCurrenciesRepository userCurrencyRepository = new UserCurrenciesRepository();
        UserCurrenciesService userCurrencyService = new UserCurrenciesService(userCurrencyRepository);
        List<Currencies> currencies = new List<Currencies>();
        currencies = await userCurrencyService.GetUserCurrencyAsync(User.CurrentUserId);
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
                string subType = uniqueTypes[i];
                GameObject button = Instantiate(TypeButtonPrefab, LeftScrollViewContentPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
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
                    _ = LoadCurrentPageAsync();
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

        _ = LoadCurrentPageAsync();
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
        _ = LoadCurrentPageAsync();
    }
    public async Task LoadCurrentPageAsync()
    {
        int totalRecord = 0;

        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            var cardHeroesGalleryService = CardHeroesGalleryService.Create();
            List<CardHeroes> cardHeroes = await cardHeroesGalleryService.GetCardHeroesCollectionAsync(type, pageSize, offset, rare);
            CardHeroesGalleryController.Instance.CreateCardHeroesGallery(cardHeroes, DictionaryContentPanel);

            totalRecord = await cardHeroesGalleryService.GetCardHeroesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            var booksGalleryService = BooksGalleryService.Create();
            List<Books> books = await booksGalleryService.GetBooksCollectionAsync(type, pageSize, offset, rare);
            BooksGalleryController.Instance.CreateBooksGallery(books, DictionaryContentPanel);

            totalRecord = await booksGalleryService.GetBooksCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
            List<CardCaptains> cardCaptains = await cardCaptainsGalleryService.GetCardCaptainsCollectionAsync(type, pageSize, offset, rare);
            CardCaptainsGalleryController.Instance.CreateCardCaptainsGallery(cardCaptains, DictionaryContentPanel);

            totalRecord = await cardCaptainsGalleryService.GetCardCaptainsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            var collaborationEquipmentGalleryService = CollaborationEquipmentsGalleryService.Create();
            List<CollaborationEquipments> collaborationEquipments = await collaborationEquipmentGalleryService.GetCollaborationEquipmentsCollectionAsync(type, pageSize, offset, rare);
            CollaborationEquipmentsGalleryController.Instance.CreateCollaborationEquipmentsGallery(collaborationEquipments, DictionaryContentPanel);

            totalRecord = await collaborationEquipmentGalleryService.GetCollaborationEquipmentsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            var equipmentsGalleryService = EquipmentsGalleryService.Create();
            List<Equipments> equipments = await equipmentsGalleryService.GetEquipmentsCollectionAsync(type, pageSize, offset, rare);
            EquipmentsGalleryController.Instance.CreateEquipmentsGallery(equipments, DictionaryContentPanel);

            totalRecord = await equipmentsGalleryService.GetEquipmentsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            var petsGalleryService = PetsGalleryService.Create();
            List<Pets> pets = await petsGalleryService.GetPetsCollectionAsync(type, pageSize, offset, rare);
            PetsGalleryController.Instance.CreatePetsGallery(pets, DictionaryContentPanel);

            totalRecord = await petsGalleryService.GetPetsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            var skillsGalleryService = SkillsGalleryService.Create();
            List<Skills> skills = await skillsGalleryService.GetSkillsCollectionAsync(type, pageSize, offset, rare);
            SkillsGalleryController.Instance.CreateSkillsGallery(skills, DictionaryContentPanel);

            totalRecord = await skillsGalleryService.GetSkillsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            var symbolsGalleryService = SymbolsGalleryService.Create();
            List<Symbols> symbols = await symbolsGalleryService.GetSymbolsCollectionAsync(type, pageSize, offset, rare);
            SymbolsGalleryController.Instance.CreateSymbolsGallery(symbols, DictionaryContentPanel);

            totalRecord = await symbolsGalleryService.GetSymbolsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            var cardMilitaryGalleryService = CardMilitariesGalleryService.Create();
            List<CardMilitaries> cardMilitaries = await cardMilitaryGalleryService.GetCardMilitariesCollectionAsync(type, pageSize, offset, rare);
            CardMilitariesGalleryController.Instance.CreateCardMilitaryGallery(cardMilitaries, DictionaryContentPanel);

            totalRecord = await cardMilitaryGalleryService.GetCardMilitariesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            var cardSpellGalleryService = CardSpellsGalleryService.Create();
            List<CardSpells> cardSpells = await cardSpellGalleryService.GetCardSpellsCollectionAsync(type, pageSize, offset, rare);
            CardSpellsGalleryController.Instance.CreateCardSpellGallery(cardSpells, DictionaryContentPanel);

            totalRecord = await cardSpellGalleryService.GetCardSpellsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            var collaborationGalleryService = CollaborationsGalleryService.Create();
            List<Collaborations> collaborations = await collaborationGalleryService.GetCollaborationsCollectionAsync(pageSize, offset, rare);
            CollaborationsGalleryController.Instance.CreateCollaborationGallery(collaborations, DictionaryContentPanel);

            totalRecord = await collaborationGalleryService.GetCollaborationsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            var medalsGalleryService = MedalsGalleryService.Create();
            List<Medals> medals = await medalsGalleryService.GetMedalsCollectionAsync(pageSize, offset, rare);
            MedalsGalleryController.Instance.CreateMedalsGallery(medals, DictionaryContentPanel);

            totalRecord = await medalsGalleryService.GetMedalsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            var titlesGalleryService = TitlesGalleryService.Create();
            List<Titles> titles = await titlesGalleryService.GetTitlesCollectionAsync(pageSize, offset, rare);
            TitlesGalleryController.Instance.CreateTitlesGallery(titles, DictionaryContentPanel);

            totalRecord = await titlesGalleryService.GetTitlesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            var bordersGalleryService = BordersGalleryService.Create();
            List<Borders> borders = await bordersGalleryService.GetBordersCollectionAsync(pageSize, offset, rare);
            BordersGalleryController.Instance.CreateBordersGallery(borders, DictionaryContentPanel);

            totalRecord = await bordersGalleryService.GetBordersCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            var magicFormationCircleGalleryService = MagicFormationCirclesGalleryService.Create();
            List<MagicFormationCircles> magicFormationCircles = await magicFormationCircleGalleryService.GetMagicFormationCirclesCollectionAsync(type, pageSize, offset, rare);
            MagicFormationCirclesGalleryController.Instance.CreateMagicFormationCircleGallery(magicFormationCircles, DictionaryContentPanel);

            totalRecord = await magicFormationCircleGalleryService.GetMagicFormationCirclesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            var relicsGalleryService = RelicsGalleryService.Create();
            List<Relics> relics = await relicsGalleryService.GetRelicsCollectionAsync(type, pageSize, offset, rare);
            RelicsGalleryController.Instance.CreateRelicsGallery(relics, DictionaryContentPanel);

            totalRecord = await relicsGalleryService.GetRelicsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            var cardMonstersGalleryService = CardMonstersGalleryService.Create();
            List<CardMonsters> monsters = await cardMonstersGalleryService.GetCardMonstersCollectionAsync(type, pageSize, offset, rare);
            CardMonstersGalleryController.Instance.CreateCardMonstersGallery(monsters, DictionaryContentPanel);

            totalRecord = await cardMonstersGalleryService.GetCardMonstersCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            var cardColonelsGalleryService = CardColonelsGalleryService.Create();
            List<CardColonels> cardColonels = await cardColonelsGalleryService.GetCardColonelsCollectionAsync(type, pageSize, offset, rare);
            CardColonelsGalleryController.Instance.CreateCardColonelsGallery(cardColonels, DictionaryContentPanel);

            totalRecord = await cardColonelsGalleryService.GetCardColonelsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            var cardGeneralsGalleryService = CardGeneralsGalleryService.Create();
            List<CardGenerals> cardGenerals = await cardGeneralsGalleryService.GetCardGeneralsCollectionAsync(type, pageSize, offset, rare);
            CardGeneralsGalleryController.Instance.CreateCardGeneralsGallery(cardGenerals, DictionaryContentPanel);

            totalRecord = await cardGeneralsGalleryService.GetCardGeneralsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            var cardAdmiralsGalleryService = CardAdmiralsGalleryService.Create();
            List<CardAdmirals> cardAdmirals = await cardAdmiralsGalleryService.GetCardAdmiralsCollectionAsync(type, pageSize, offset, rare);
            CardAdmiralsGalleryController.Instance.CreateCardAdmiralsGallery(cardAdmirals, DictionaryContentPanel);

            totalRecord = await cardAdmiralsGalleryService.GetCardAdmiralsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            var talismanGalleryService = TalismansGalleryService.Create();
            List<Talismans> talismans = await talismanGalleryService.GetTalismansCollectionAsync(type, pageSize, offset, rare);
            TalismansGalleryController.Instance.CreateTalismanGallery(talismans, DictionaryContentPanel);

            totalRecord = await talismanGalleryService.GetTalismansCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            var puppetGalleryService = PuppetsGalleryService.Create();
            List<Puppets> puppets = await puppetGalleryService.GetPuppetsCollectionAsync(type, pageSize, offset, rare);
            PuppetsGalleryController.Instance.CreatePuppetGallery(puppets, DictionaryContentPanel);

            totalRecord = await puppetGalleryService.GetPuppetsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            var alchemyGalleryService = AlchemiesGalleryService.Create();
            List<Alchemies> alchemies = await alchemyGalleryService.GetAlchemyCollectionAsync(type, pageSize, offset, rare);
            AlchemiesGalleryController.Instance.CreateAlchemyGallery(alchemies, DictionaryContentPanel);

            totalRecord = await alchemyGalleryService.GetAlchemyCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            var forgeGalleryService = ForgesGalleryService.Create();
            List<Forges> forges = await forgeGalleryService.GetForgesCollectionAsync(type, pageSize, offset, rare);
            ForgesGalleryController.Instance.CreateForgeGallery(forges, DictionaryContentPanel);

            totalRecord = await forgeGalleryService.GetForgesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            var cardLifeGalleryService = CardLivesGalleryService.Create();
            List<CardLives> cardLives = await cardLifeGalleryService.GetCardLivesCollectionAsync(type, pageSize, offset, rare);
            CardLivesGalleryController.Instance.CreateCardLifeGallery(cardLives, DictionaryContentPanel);

            totalRecord = await cardLifeGalleryService.GetCardLivesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            var artworkGalleryService = ArtworksGalleryService.Create();
            List<Artworks> artworks = await artworkGalleryService.GetArtworksCollectionAsync(type, pageSize, offset, rare);
            ArtworksGalleryController.Instance.CreateArtworkGallery(artworks, DictionaryContentPanel);

            totalRecord = await artworkGalleryService.GetArtworksCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            var spiritBeastGalleryService = SpiritBeastsGalleryService.Create();
            List<SpiritBeasts> spiritBeasts = await spiritBeastGalleryService.GetSpiritBeastsCollectionAsync(pageSize, offset, rare);
            SpiritBeastsGalleryController.Instance.CreateSpiritBeastGallery(spiritBeasts, DictionaryContentPanel);

            totalRecord = await spiritBeastGalleryService.GetSpiritBeastsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            var avatarsGalleryService = AvatarsGalleryService.Create();
            List<Avatars> avatars = await avatarsGalleryService.GetAvatarsCollectionAsync(pageSize, offset, rare);
            AvatarsGalleryController.Instance.CreateAvatarsGallery(avatars, DictionaryContentPanel);

            totalRecord = await avatarsGalleryService.GetAvatarsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            var spiritCardGalleryService = SpiritCardsGalleryService.Create();
            List<SpiritCards> spiritCards = await spiritCardGalleryService.GetSpiritCardsCollectionAsync(type, pageSize, offset, rare);
            SpiritCardsGalleryController.Instance.CreateSpiritCardGallery(spiritCards, DictionaryContentPanel);

            totalRecord = await spiritCardGalleryService.GetSpiritCardsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {
            var achievementsGalleryService = AchievementsGalleryService.Create();
            List<Achievements> achievements = await achievementsGalleryService.GetAchievementCollectionAsync(pageSize, offset, rare);
            AchievementsGalleryController.Instance.CreateAchievementsGallery(achievements, DictionaryContentPanel);

            totalRecord = await achievementsGalleryService.GetAchievementsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD))
        {
            var cardsGalleryService = CardsGalleryService.Create();
            List<Cards> cards = await cardsGalleryService.GetCardsCollectionAsync(pageSize, offset, rare);
            CardsGalleryController.Instance.CreateCardsGallery(cards, DictionaryContentPanel);

            totalRecord = await cardsGalleryService.GetCardsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            var architecturesGalleryService = ArchitecturesGalleryService.Create();
            List<Architectures> architectures = await architecturesGalleryService.GetArchitecturesCollectionAsync(pageSize, offset, rare);
            ArchitecturesGalleryController.Instance.CreateArchitecturesGallery(architectures, DictionaryContentPanel);

            totalRecord = await architecturesGalleryService.GetArchitecturesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            var technologiesGalleryService = TechnologiesGalleryService.Create();
            List<Technologies> technologies = await technologiesGalleryService.GetTechnologiesCollectionAsync(pageSize, offset, rare);
            TechnologiesGalleryController.Instance.CreateTechnologiesGallery(technologies, DictionaryContentPanel);

            totalRecord = await technologiesGalleryService.GetTechnologiesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            var vehiclesGalleryService = VehiclesGalleryService.Create();
            List<Vehicles> vehicles = await vehiclesGalleryService.GetVehiclesCollectionAsync(type, pageSize, offset, rare);
            VehiclesGalleryController.Instance.CreateVehicleGallery(vehicles, DictionaryContentPanel);

            totalRecord = await vehiclesGalleryService.GetVehiclesCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            var coresGalleryService = CoresGalleryService.Create();
            List<Cores> cores = await coresGalleryService.GetCoresCollectionAsync(pageSize, offset, rare);
            CoresGalleryController.Instance.CreateCoresGallery(cores, DictionaryContentPanel);

            totalRecord = await coresGalleryService.GetCoresCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            var weaponsGalleryService = WeaponsGalleryService.Create();
            List<Weapons> weapons = await weaponsGalleryService.GetWeaponsCollectionAsync(pageSize, offset, rare);
            WeaponsGalleryController.Instance.CreateWeaponsGallery(weapons, DictionaryContentPanel);

            totalRecord = await weaponsGalleryService.GetWeaponsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            var robotsGalleryService = RobotsGalleryService.Create();
            List<Robots> robots = await robotsGalleryService.GetRobotsCollectionAsync(pageSize, offset, rare);
            RobotsGalleryController.Instance.CreateRobotsGallery(robots, DictionaryContentPanel);

            totalRecord = await robotsGalleryService.GetRobotsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            var badgesGalleryService = BadgesGalleryService.Create();
            List<Badges> badges = await badgesGalleryService.GetBadgesCollectionAsync(pageSize, offset, rare);
            BadgesGalleryController.Instance.CreateBadgesGallery(badges, DictionaryContentPanel);

            totalRecord = await badgesGalleryService.GetBadgesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            var mechaBeastsGalleryService = MechaBeastsGalleryService.Create();
            List<MechaBeasts> mechaBeasts = await mechaBeastsGalleryService.GetMechaBeastsCollectionAsync(pageSize, offset, rare);
            MechaBeastsGalleryController.Instance.CreateMechaBeastsGallery(mechaBeasts, DictionaryContentPanel);

            totalRecord = await mechaBeastsGalleryService.GetMechaBeastsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            var runesGalleryService = RunesGalleryService.Create();
            List<Runes> runes = await runesGalleryService.GetRunesCollectionAsync(pageSize, offset, rare);
            RunesGalleryController.Instance.CreateRunesGallery(runes, DictionaryContentPanel);

            totalRecord = await runesGalleryService.GetRunesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            var furnituresGalleryService = FurnituresGalleryService.Create();
            List<Furnitures> furnitures = await furnituresGalleryService.GetFurnituresCollectionAsync(type, pageSize, offset, rare);
            FurnituresGalleryController.Instance.CreateFurnitureGallery(furnitures, DictionaryContentPanel);

            totalRecord = await furnituresGalleryService.GetFurnituresCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            var foodsGalleryService = FoodsGalleryService.Create();
            List<Foods> foods = await foodsGalleryService.GetFoodsCollectionAsync(pageSize, offset, rare);
            FoodsGalleryController.Instance.CreateFoodsGallery(foods, DictionaryContentPanel);

            totalRecord = await foodsGalleryService.GetFoodsCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            var beveragesGalleryService = BeveragesGalleryService.Create();
            List<Beverages> beverages = await beveragesGalleryService.GetBeveragesCollectionAsync(pageSize, offset, rare);
            BeveragesGalleryController.Instance.CreateBeveragesGallery(beverages, DictionaryContentPanel);

            totalRecord = await beveragesGalleryService.GetBeveragesCountAsync(rare);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            var buildingsGalleryService = BuildingsGalleryService.Create();
            List<Buildings> buildings = await buildingsGalleryService.GetBuildingsCollectionAsync(type, pageSize, offset, rare);
            BuildingsGalleryController.Instance.CreateBuildingGallery(buildings, DictionaryContentPanel);

            totalRecord = await buildingsGalleryService.GetBuildingsCountAsync(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            var plantsGalleryService = PlantsGalleryService.Create();
            List<Plants> plants = await plantsGalleryService.GetPlantsCollectionAsync(pageSize, offset, rare);
            PlantsGalleryController.Instance.CreatePlantsGallery(plants, DictionaryContentPanel);

            totalRecord = await plantsGalleryService.GetPlantsCountAsync(rare);
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
            _ = LoadCurrentPageAsync();

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
            _ = LoadCurrentPageAsync();

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
