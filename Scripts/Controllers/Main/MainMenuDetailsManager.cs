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
    // private GameObject firstDetailsObject;
    // private GameObject elementDetailsObject;
    // private GameObject elementDetails2Object;
    // private GameObject descriptionDetailsObject;
    private GameObject TabButton5;
    // private Transform firstPopupPanel;
    // private Transform elementPopupPanel;
    // private Transform element2PopupPanel;
    // private Transform descriptionPopupPanel;
    private Transform buttonGroupPanel1;
    private Transform buttonGroupPanel2;
    private Transform buttonGroupPanel3;
    private Transform setButtonGroupPanel;
    private RawImage CardBackground;
    private string mainType;
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
        MainMenuDetailPanel2Prefab = UIManager.Instance.GetGameObject("MainMenuDetailPanel2Prefab");

        TabButton5 = UIManager.Instance.GetGameObject("TabButton5");

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
        setButtonGroupPanel = currentObject.transform.Find("DictionaryCards/SetButtonGroup");
        CardBackground = currentObject.transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        RawImage backgroundCircle1Image = currentObject.transform.Find("DictionaryCards/CircleImage1").GetComponent<RawImage>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        // ButtonLoader.Instance.buttonGroupPanel1 = buttonGroupPanel1;
        // ButtonLoader.Instance.buttonGroupPanel2 = buttonGroupPanel2;
        // ButtonLoader.Instance.buttonGroupPanel3 = buttonGroupPanel3;
        ButtonLoader.Instance.CreateSetButtonGroup(data, TabButton5, setButtonGroupPanel);

        backgroundCircle1Image.gameObject.AddComponent<RotateAnimation>();
        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes cardHeroes)
        {
            // Xử lý đối tượng Card
            mainType = "CardHeroes";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HERO);
            UserCardHeroesController.Instance.ShowCardHeroesDetails(cardHeroes, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardHeroes.Type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
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
            mainType = "Books";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BOOK);
            UserBooksController.Instance.ShowBooksDetails(book, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.BOOK);
            });
        }
        else if (data is CardCaptains cardCaptains)
        {
            // Xử lý đối tượng Captain
            mainType = "CardCaptains";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);
            UserCardCaptainsController.Instance.ShowCardCaptainsDetails(cardCaptains, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardCaptains.Type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
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
            mainType = "Pets";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.PET);
            UserPetsController.Instance.ShowPetsDetails(pet, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
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
            mainType = "CollaborationEquipments";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLABORATION_EQUIPMENT);
            UserCollaborationEquipmentController.Instance.ShowCollaborationEquipmentsDetails(collaborationEquipmentsequipment, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
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
            mainType = "CardMilitary";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MILITARY);
            UserCardMilitaryController.Instance.ShowCardMilitaryDetails(cardMilitary, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardMilitary.Type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
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
            mainType = "CardSpell";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_SPELL);
            UserCardSpellController.Instance.ShowCardSpellDetails(cardSpell, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
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
            mainType = "Collaborations";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLABORATION);
            UserCollaborationController.Instance.ShowCollaborationsDetails(collaboration, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.COLLABORATION);
            });
        }
        else if (data is CardMonsters cardMonsters)
        {
            // Xử lý đối tượng Monster
            mainType = "CardMonsters";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MONSTER);
            UserCardMonstersController.Instance.ShowCardMonstersDetails(cardMonsters, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
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
            mainType = "Equipments";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.EQUIPMENT);
            UserEquipmentsController.Instance.ShowEquipmentsDetails(equipment, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
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
            mainType = "Medals";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.MEDAL);
            UserMedalsController.Instance.ShowMedalsDetails(medal, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
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
            mainType = "Skills";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SKILL);
            UserSkillsController.Instance.ShowSkillsDetails(skill, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
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
            mainType = "Symbols";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SYMBOL);
            UserSymbolsController.Instance.ShowSymbolsDetails(symbol, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.SYMBOL);
            });
        }
        else if (data is Architectures title)
        {
            // Xử lý đối tượng Title
            mainType = "Titles";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TITLE);
            UserTitlesController.Instance.ShowTitlesDetails(title, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.TITLE);
            });
        }
        else if (data is MagicFormationCircles magicFormationCircle)
        {
            // Xử lý đối tượng Title
            mainType = "MagicFormationCircle";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.MAGIC_FORMATION_CIRCLE);
            UserMagicFormationCircleController.Instance.ShowMagicFormationCircleDetails(magicFormationCircle, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE);
            });
        }
        else if (data is Relics relics)
        {
            // Xử lý đối tượng Title
            mainType = "Relics";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RELIC);
            UserRelicsController.Instance.ShowRelicsDetails(relics, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.RELIC);
            });
        }
        else if (data is CardColonels cardColonels)
        {
            // Xử lý đối tượng colonels
            mainType = "CardColonels";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_COLONEL);
            UserCardColonelsController.Instance.ShowCardColonelsDetails(cardColonels, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardColonels.Type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_COLONEL);
            });
        }
        else if (data is CardGenerals cardGenerals)
        {
            // Xử lý đối tượng Generals
            mainType = "CardGenerals";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERAL);
            UserCardGeneralsController.Instance.ShowCardGeneralsDetails(cardGenerals, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardGenerals.Type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_GENERAL);
            });
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // Xử lý đối tượng admirals
            mainType = "CardAdmirals";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);
            UserCardAdmiralsController.Instance.ShowCardAdmiralsDetails(cardAdmirals, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardAdmirals.Type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
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
        else if (data is Achievements achievements)
        {
            // Xử lý đối tượng achievements
            mainType = "Achievements";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ACHIEVEMENT);
            UserAchievementsController.Instance.ShowAchievementsDetails(achievements, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.ACHIEVEMENT);
            });
        }
        else if (data is Talismans talisman)
        {
            // Xử lý đối tượng achievements
            mainType = "Talisman";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TALISMAN);
            UserTalismanController.Instance.ShowTalismanDetails(talisman, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.TALISMAN);
            });
        }
        else if (data is Puppets puppet)
        {
            // Xử lý đối tượng achievements
            mainType = "Puppet";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.PUPPET);
            UserPuppetController.Instance.ShowPuppetDetails(puppet, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.PUPPET);
            });
        }
        else if (data is Alchemies alchemy)
        {
            // Xử lý đối tượng achievements
            mainType = "Alchemy";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ALCHEMY);
            UserAlchemyController.Instance.ShowAlchemyDetails(alchemy, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.ALCHEMY);
            });
        }
        else if (data is Forges forge)
        {
            // Xử lý đối tượng achievements
            mainType = "Forge";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FORGE);
            UserForgeController.Instance.ShowForgeDetails(forge, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.FORGE);
            });
        }
        else if (data is CardLives cardLife)
        {
            // Xử lý đối tượng achievements
            mainType = "CardLife";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_LIFE);
            UserCardLifeController.Instance.ShowCardLifeDetails(cardLife, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.CARD_LIFE);
            });
        }
        else if (data is Artworks artwork)
        {
            // Xử lý đối tượng achievements
            mainType = "Artwork";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ARTWORK);
            UserArtworkController.Instance.ShowArtworkDetails(artwork, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.ARTWORK);
            });
        }
        else if (data is SpiritBeasts spiritBeast)
        {
            // Xử lý đối tượng achievements
            mainType = "SpiritBeast";
            titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SPIRIT_BEAST);
            UserSpiritBeastController.Instance.ShowSpiritBeastDetails(spiritBeast, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(AppConstants.MainType.SPIRIT_BEAST);
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

            foreach (Items items1 in items)
            {
                double expPerBottle = EvaluateExperiment.GetItemExp(items1.Name);

                if (expPerBottle > 0 && items1.Quantity > 0) // Điều kiện 2: Phải có item hợp lệ
                {
                    double totalExpFromThisItem = expPerBottle * items1.Quantity;

                    if (requiredExp <= totalExpFromThisItem)
                    {
                        totalExperiment += requiredExp;
                        items1.Quantity -= (int)Math.Ceiling((double)requiredExp / expPerBottle);
                        canLevelUp = true;
                        break;
                    }
                    else
                    {
                        totalExperiment += totalExpFromThisItem;
                        items1.Quantity = 0;
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
                UserItemsService.Create().UpdateUserItemsQuantity(items1);
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

            foreach (Items items1 in items)
            {
                double expPerBottle = EvaluateExperiment.GetItemExp(items1.Name);

                if (expPerBottle > 0 && items1.Quantity > 0) // Điều kiện 2: Phải có item hợp lệ
                {
                    double totalExpFromThisItem = expPerBottle * items1.Quantity;

                    if (requiredExp <= totalExpFromThisItem)
                    {
                        totalExperiment += requiredExp;
                        items1.Quantity -= (int)Math.Ceiling((double)requiredExp / expPerBottle);
                        canLevelUp = true;
                        break;
                    }
                    else
                    {
                        totalExperiment += totalExpFromThisItem;
                        items1.Quantity = 0;
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
                UserItemsService.Create().UpdateUserItemsQuantity(items1);
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
