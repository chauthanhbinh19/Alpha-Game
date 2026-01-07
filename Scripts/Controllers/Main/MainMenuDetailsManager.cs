using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using System;

public class MainMenuDetailsManager : MonoBehaviour
{
    private GameObject MainMenuDetailPanel2Prefab;
    private Transform MainPanel;
    // private Transform RightButtonContent;
    private Transform DetailsPanel;
    private Transform LevelPanel;
    private Transform SkillsPanel;
    private Transform UpgradePanel;
    private Transform SpiritBeastPanel;
    private Transform RankPanel;
    private Transform DetailsContent;
    private Transform LevelElementContent;
    private Transform LevelMaterialContent;
    private Transform SkillsContent;
    private Transform UpgradeElementContent;
    private Transform UpgradeMaterialContent;
    private GameObject currentObject;
    private GameObject TabButton5;
    private Transform buttonGroupPanel1;
    private Transform buttonGroupPanel2;
    private Transform buttonGroupPanel3;
    private Transform setButtonGroupPanel;
    private RawImage cardBackground;
    // private double increasePerLevel = 0.01;
    // private double increasePerUpgrade = 1.1;
    // private TeamsService teamsService;
    // private UserItemsService userItemsService;
    public static MainMenuDetailsManager Instance { get; private set; }
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainMenuDetailPanel2Prefab = UIManager.Instance.Get("MainMenuDetailPanel2Prefab");

        TabButton5 = UIManager.Instance.Get("TabButton5");

        // teamsService = TeamsService.Create();
        // userItemsService = UserItemsService.Create();
    }
    public void PopupDetails(object data, Transform panel)
    {
        MainPanel = panel;
        currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        // RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        DetailsPanel = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel");
        LevelPanel = currentObject.transform.Find("DictionaryCards/Content/LevelPanel");
        SkillsPanel = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel");
        UpgradePanel = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel");
        SpiritBeastPanel = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel");
        RankPanel = currentObject.transform.Find("DictionaryCards/Content/RankPanel");
        DetailsContent = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");
        // LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        // LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        SkillsContent = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel/Scroll View/Viewport/Content");
        UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        UpgradeMaterialContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        buttonGroupPanel1 = currentObject.transform.Find("DictionaryCards/ButtonGroup1");
        buttonGroupPanel2 = currentObject.transform.Find("DictionaryCards/ButtonGroup2");
        buttonGroupPanel3 = currentObject.transform.Find("DictionaryCards/ButtonGroup3");
        setButtonGroupPanel = currentObject.transform.Find("DictionaryCards/SetButtonGroup/Viewport/Content");
        cardBackground = currentObject.transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        RawImage backgroundCircle1Image = currentObject.transform.Find("DictionaryCards/CircleImage1").GetComponent<RawImage>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        // ButtonLoader.Instance.buttonGroupPanel1 = buttonGroupPanel1;
        // ButtonLoader.Instance.buttonGroupPanel2 = buttonGroupPanel2;
        // ButtonLoader.Instance.buttonGroupPanel3 = buttonGroupPanel3;
        ButtonLoader.Instance.CreateSetButtonGroup(data, setButtonGroupPanel);

        backgroundCircle1Image.gameObject.AddComponent<RotateAnimation>();
        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes cardHero)
        {
            // Xử lý đối tượng Card
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HERO);
            UserCardHeroesController.Instance.ShowCardHeroDetails(cardHero, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardHero.Type}");
            cardBackground.texture = texture;
            cardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_HERO);
            });
        }
        else if (data is Books book)
        {
            // Xử lý đối tượng Book
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BOOK);
            UserBooksController.Instance.ShowBookDetails(book, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.BOOK_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.BOOK);
            });
        }
        else if (data is CardCaptains cardCaptain)
        {
            // Xử lý đối tượng Captain
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);
            UserCardCaptainsController.Instance.ShowCardCaptainDetails(cardCaptain, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardCaptain.Type}");
            cardBackground.texture = texture;
            cardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_CAPTAIN);
            });
        }
        else if (data is Pets pet)
        {
            // Xử lý đối tượng Pet
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.PET);
            UserPetsController.Instance.ShowPetDetails(pet, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.PET_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.PET);
            });
        }
        else if (data is CollaborationEquipments collaborationEquipmentsequipment)
        {
            // Xử lý đối tượng CollaborationEquipment
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLABORATION_EQUIPMENT);
            UserCollaborationEquipmentsController.Instance.ShowCollaborationEquipmentDetails(collaborationEquipmentsequipment, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.COLLABORATION_EQUIPMENT_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT);
            });
        }
        else if (data is CardMilitaries cardMilitary)
        {
            // Xử lý đối tượng Military
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MILITARY);
            UserCardMilitariesController.Instance.ShowCardMilitaryDetails(cardMilitary, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardMilitary.Type}");
            cardBackground.texture = texture;
            cardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_MILITARY);
            });
        }
        else if (data is CardSpells cardSpell)
        {
            // Xử lý đối tượng Spell
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_SPELL);
            UserCardSpellsController.Instance.ShowCardSpellDetails(cardSpell, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardSpell.Type}");
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_SPELL);
            });
        }
        else if (data is Collaborations collaboration)
        {
            // Xử lý đối tượng Collaboration
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLABORATION);
            UserCollaborationsController.Instance.ShowCollaborationDetails(collaboration, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.COLLABORATION_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.COLLABORATION);
            });
        }
        else if (data is CardMonsters cardMonster)
        {
            // Xử lý đối tượng Monster
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MONSTER);
            UserCardMonstersController.Instance.ShowCardMonsterDetails(cardMonster, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardMonster.Type}");
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_MONSTER);
            });
        }
        else if (data is Equipments equipment)
        {
            // Xử lý đối tượng Equipment
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.EQUIPMENT);
            UserEquipmentsController.Instance.ShowEquipmentDetails(equipment, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.EQUIPMENT_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.EQUIPMENT);
            });
        }
        else if (data is Medals medal)
        {
            // Xử lý đối tượng Medal
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.MEDAL);
            UserMedalsController.Instance.ShowMedalDetails(medal, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.MEDAL_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.MEDAL);
            });
        }
        else if (data is Skills skill)
        {
            // Xử lý đối tượng Skill
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SKILL);
            UserSkillsController.Instance.ShowSkillDetails(skill, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.SKILL_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.SKILL);
            });
        }
        else if (data is Symbols symbol)
        {
            // Xử lý đối tượng Symbol
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SYMBOL);
            UserSymbolsController.Instance.ShowSymbolDetails(symbol, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.SYMBOL_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.SYMBOL);
            });
        }
        else if (data is Titles title)
        {
            // Xử lý đối tượng Title
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TITLE);
            UserTitlesController.Instance.ShowTitleDetails(title, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.TITLE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.TITLE);
            });
        }
        else if (data is MagicFormationCircles magicFormationCircle)
        {
            // Xử lý đối tượng magic formation circle
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.MAGIC_FORMATION_CIRCLE);
            UserMagicFormationCirclesController.Instance.ShowMagicFormationCircleDetails(magicFormationCircle, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.MAGIC_FORMATION_CIRCLE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE);
            });
        }
        else if (data is Relics relic)
        {
            // Xử lý đối tượng relic
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RELIC);
            UserRelicsController.Instance.ShowRelicDetails(relic, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.RELIC_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.RELIC);
            });
        }
        else if (data is CardColonels cardColonel)
        {
            // Xử lý đối tượng colonels
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_COLONEL);
            UserCardColonelsController.Instance.ShowCardColonelDetails(cardColonel, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardColonel.Type}");
            cardBackground.texture = texture;
            cardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_COLONEL);
            });
        }
        else if (data is CardGenerals cardGeneral)
        {
            // Xử lý đối tượng Generals
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERAL);
            UserCardGeneralsController.Instance.ShowCardGeneralDetails(cardGeneral, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardGeneral.Type}");
            cardBackground.texture = texture;
            cardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_GENERAL);
            });
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            // Xử lý đối tượng admirals
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);
            UserCardAdmiralsController.Instance.ShowCardAdmiralDetails(cardAdmiral, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardAdmiral.Type}");
            cardBackground.texture = texture;
            cardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_ADMIRAL);
            });
        }
        // else if (data is Borders borders)
        // {
        //     // Xử lý đối tượng borders
        //     ShowBordersDetails(borders);
        // }
        else if (data is Achievements achievement)
        {
            // Xử lý đối tượng achievements
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ACHIEVEMENT);
            UserAchievementsController.Instance.ShowAchievementDetails(achievement, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.ACHIEVEMENT_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.ACHIEVEMENT);
            });
        }
        else if (data is Talismans talisman)
        {
            // Xử lý đối tượng talisman
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TALISMAN);
            UserTalismansController.Instance.ShowTalismanDetails(talisman, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.TALISMAN_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.TALISMAN);
            });
        }
        else if (data is Puppets puppet)
        {
            // Xử lý đối tượng puppet
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.PUPPET);
            UserPuppetsController.Instance.ShowPuppetDetails(puppet, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.PUPPET_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.PUPPET);
            });
        }
        else if (data is Alchemies alchemy)
        {
            // Xử lý đối tượng alchemy
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ALCHEMY);
            UserAlchemiesController.Instance.ShowAlchemyDetails(alchemy, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.ALCHEMY_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.ALCHEMY);
            });
        }
        else if (data is Forges forge)
        {
            // Xử lý đối tượng forge
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FORGE);
            UserForgesController.Instance.ShowForgeDetails(forge, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.FORGE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.FORGE);
            });
        }
        else if (data is CardLives cardLife)
        {
            // Xử lý đối tượng card life
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_LIFE);
            UserCardLivesController.Instance.ShowCardLifeDetails(cardLife, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.CARD_LIFE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_LIFE);
            });
        }
        else if (data is Artworks artwork)
        {
            // Xử lý đối tượng artwork
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ARTWORK);
            UserArtworksController.Instance.ShowArtworkDetails(artwork, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.ARTWORK_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.ARTWORK);
            });
        }
        else if (data is SpiritBeasts spiritBeast)
        {
            // Xử lý đối tượng spirit beast
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SPIRIT_BEAST);
            UserSpiritBeastsController.Instance.ShowSpiritBeastDetails(spiritBeast, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.SPIRIT_BEAST_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.SPIRIT_BEAST);
            });
        }
        else if (data is SpiritCards spiritCard)
        {
            // Xử lý đối tượng spirit card
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SPIRIT_CARD);
            UserSpiritCardsController.Instance.ShowSpiritCardDetails(spiritCard, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.SPIRIT_CARD_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.SPIRIT_CARD);
            });
        }
        else if (data is Cards card)
        {
            // Xử lý đối tượng card
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD);
            UserCardsController.Instance.ShowCardDetails(card, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.CARD_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD);
            });
        }
        else if (data is Architectures architecture)
        {
            // Xử lý đối tượng architecture
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ARCHITECTURE);
            UserArchitecturesController.Instance.ShowArchitectureDetails(architecture, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.ARCHITECTURE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.ARCHITECTURE);
            });
        }
        else if (data is Technologies technology)
        {
            // Xử lý đối tượng technology
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TECHNOLOGY);
            UserTechnologiesController.Instance.ShowTechnologyDetails(technology, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.TECHNOLOGY_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.TECHNOLOGY);
            });
        }
        else if (data is Vehicles vehicle)
        {
            // Xử lý đối tượng vehicle
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.VEHICLE);
            UserVehiclesController.Instance.ShowVehicleDetails(vehicle, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.VEHICLE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.VEHICLE);
            });
        }
        else if (data is Cores core)
        {
            // Xử lý đối tượng core
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CORE);
            UserCoresController.Instance.ShowCoreDetails(core, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.CORE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CORE);
            });
        }
        else if (data is Weapons weapon)
        {
            // Xử lý đối tượng weapon
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.WEAPON);
            UserWeaponsController.Instance.ShowWeaponDetails(weapon, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.WEAPON_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.WEAPON);
            });
        }
        else if (data is Robots robot)
        {
            // Xử lý đối tượng robot
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ROBOT);
            UserRobotsController.Instance.ShowRobotDetails(robot, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.ROBOT_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.ROBOT);
            });
        }
        else if (data is Badges badge)
        {
            // Xử lý đối tượng badge
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BADGE);
            UserBadgesController.Instance.ShowBadgeDetails(badge, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.BADGE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.BADGE);
            });
        }
        else if (data is MechaBeasts mechaBeast)
        {
            // Xử lý đối tượng mecha beast
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.MECHA_BEAST);
            UserMechaBeastsController.Instance.ShowMechaBeastDetails(mechaBeast, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.MECHA_BEAST_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.MECHA_BEAST);
            });
        }
        else if (data is Runes rune)
        {
            // Xử lý đối tượng rune
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RUNE);
            UserRunesController.Instance.ShowRuneDetails(rune, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.RUNE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.RUNE);
            });
        }
        else if (data is Furnitures furniture)
        {
            // Xử lý đối tượng rune
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RUNE);
            UserFurnituresController.Instance.ShowFurnitureDetails(furniture, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.FURNITURE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.FURNITURE);
            });
        }
        else if (data is Foods food)
        {
            // Xử lý đối tượng rune
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RUNE);
            UserFoodsController.Instance.ShowFoodDetails(food, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.FOOD_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.FOOD);
            });
        }
        else if (data is Beverages beverage)
        {
            // Xử lý đối tượng rune
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RUNE);
            UserBeveragesController.Instance.ShowBeverageDetails(beverage, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.BEVERAGE_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.BEVERAGE);
            });
        }
        else if (data is Buildings building)
        {
            // Xử lý đối tượng rune
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RUNE);
            UserBuildingsController.Instance.ShowBuildingDetails(building, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.BUILDING_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.BUILDING);
            });
        }
        else if (data is Plants plant)
        {
            // Xử lý đối tượng rune
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RUNE);
            UserPlantsController.Instance.ShowPlantDetails(plant, currentObject);
            Texture texture = Resources.Load<Texture>(ImageConstants.Background.PLANT_BACKGROUND_URL);
            cardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.PLANT);
            });
        }
        else
        {
            Debug.LogError("Không hỗ trợ loại dữ liệu này!");
        }

    }
    public bool UpOneLevelCondition(List<Items> items, int currentLevel, int userMaxLevel, int maxLevel, int experimentCondition, double totalExperiment)
    {
        bool status = false;
        if (currentLevel < userMaxLevel && currentLevel < maxLevel)
        {
            double requiredExp = experimentCondition - totalExperiment; // EXP cần để lên cấp
            bool canLevelUp = false;

            foreach (Items item in items)
            {
                double expPerBottle = EvaluateExperiment.GetItemExp(item.Name);

                if (expPerBottle > 0 && item.Quantity > 0) // Điều kiện 2: Phải có item hợp lệ
                {
                    double totalExpFromThisItem = expPerBottle * item.Quantity;

                    if (requiredExp <= totalExpFromThisItem)
                    {
                        totalExperiment += requiredExp;
                        item.Quantity -= (int)Math.Ceiling((double)requiredExp / expPerBottle);
                        canLevelUp = true;
                        break;
                    }
                    else
                    {
                        totalExperiment += totalExpFromThisItem;
                        item.Quantity = 0;
                    }
                }
            }

            if (canLevelUp)
            {
                totalExperiment -= experimentCondition;
                currentLevel++;
                experimentCondition = currentLevel * 100;
                status = true;
            }
        }
        if (status == true)
        {
            foreach (Items items1 in items)
            {
                _=UserItemsService.Create().UpdateUserItemQuantityAsync(items1);
            }
        }
        return status;
    }
    public bool UpMaxLevelCondition(List<Items> items, ref int currentLevel, int userMaxLevel, int maxLevel, int experimentCondition, double totalExperiment)
    {
        bool status = false;
        while (currentLevel < userMaxLevel && currentLevel < maxLevel)
        {
            double requiredExp = experimentCondition - totalExperiment; // EXP cần để lên cấp
            bool canLevelUp = false;

            foreach (Items item in items)
            {
                double expPerBottle = EvaluateExperiment.GetItemExp(item.Name);

                if (expPerBottle > 0 && item.Quantity > 0) // Điều kiện 2: Phải có item hợp lệ
                {
                    double totalExpFromThisItem = expPerBottle * item.Quantity;

                    if (requiredExp <= totalExpFromThisItem)
                    {
                        totalExperiment += requiredExp;
                        item.Quantity -= (int)Math.Ceiling((double)requiredExp / expPerBottle);
                        canLevelUp = true;
                        break;
                    }
                    else
                    {
                        totalExperiment += totalExpFromThisItem;
                        item.Quantity = 0;
                    }
                }
            }

            if (canLevelUp)
            {
                totalExperiment -= experimentCondition;
                currentLevel++;
                experimentCondition = currentLevel * 100;
                status = true;
            }
            else
            {
                break; // Không đủ EXP để lên cấp tiếp
            }
        }
        if (status == true)
        {
            // Cập nhật số lượng item còn lại trong cơ sở dữ liệu
            foreach (Items items1 in items)
            {
                _=UserItemsService.Create().UpdateUserItemQuantityAsync(items1);
            }
        }
        return status;
    }
    public bool BreakthroughCondition()
    {
        bool status = false;

        return status;
    }
    public void HideNonDetailsPanels()
    {
        DetailsPanel.gameObject.SetActive(true);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        SpiritBeastPanel.gameObject.SetActive(false);
        RankPanel.gameObject.SetActive(false);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        ButtonEvent.Instance.Close(SkillsContent);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
        // ButtonEvent.Instance.Close(SpiritBeastPanel);
    }
    public void HideNonLevelPanels()
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(true);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        SpiritBeastPanel.gameObject.SetActive(false);
        RankPanel.gameObject.SetActive(false);
        ButtonEvent.Instance.Close(DetailsContent);
        ButtonEvent.Instance.Close(SkillsContent);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        // ButtonEvent.Instance.Close(SpiritBeastPanel);
    }
    public void HideNonSkillsPanels()
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(true);
        UpgradePanel.gameObject.SetActive(false);
        SpiritBeastPanel.gameObject.SetActive(false);
        RankPanel.gameObject.SetActive(false);
        ButtonEvent.Instance.Close(DetailsContent);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
        // ButtonEvent.Instance.Close(SpiritBeastPanel);
    }
    public void HideNonUpgradePanels()
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(true);
        SpiritBeastPanel.gameObject.SetActive(false);
        RankPanel.gameObject.SetActive(false);
        ButtonEvent.Instance.Close(DetailsContent);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        ButtonEvent.Instance.Close(SkillsContent);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
        // ButtonEvent.Instance.Close(SpiritBeastPanel);
    }
    public void HideNonSpiritBeastPanels()
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        SpiritBeastPanel.gameObject.SetActive(true);
        RankPanel.gameObject.SetActive(false);
        ButtonEvent.Instance.Close(DetailsContent);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        ButtonEvent.Instance.Close(SkillsContent);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
        // ButtonEvent.Instance.Close(SpiritBeastPanel);
    }
    public void HideNonRankPanels()
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        SpiritBeastPanel.gameObject.SetActive(false);
        RankPanel.gameObject.SetActive(true);
        ButtonEvent.Instance.Close(DetailsContent);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        ButtonEvent.Instance.Close(SkillsContent);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
        // ButtonEvent.Instance.Close(SpiritBeastPanel);
    }
}
