using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserCardMonstersController : MonoBehaviour
{
    public static UserCardMonstersController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private GameObject PositionPrefab;
    private GameObject ElementDetails2Prefab;
    private double increasePerLevel = 0.01;
    private double increasePerUpgrade = 1.1;
    private TeamsService teamsService;
    private UserItemsService userItemsService;
    private GameObject PopupSpiritBeastPanelPrefab;
    private GameObject EquipmentsWearingPrefab;
    private GameObject popupSpiritBeastObject;
    private GameObject tempCurrentObject;
    private GameObject SkillPanelPrefab;
    private GameObject SkillGroupPrefab;
    private GameObject Skill1Prefab;
    private GameObject Skill2Prefab;
    private GameObject PopupSkillsPanelPrefab;
    private GameObject PopupSkillDetailPrefab;
    private GameObject skillPanelObject;
    private int pageSize;
    private int offset;
    private int currentPage;
    private int totalPage;
    private string statusToggle;
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

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        PositionPrefab = UIManager.Instance.GetGameObject("PositionPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
        PopupSpiritBeastPanelPrefab = UIManager.Instance.GetGameObject("PopupSpiritBeastPanelPrefab");
        EquipmentsWearingPrefab = UIManager.Instance.GetGameObject("EquipmentsWearingPrefab");
        SkillPanelPrefab = UIManager.Instance.GetGameObject("SkillPanelPrefab");
        SkillGroupPrefab = UIManager.Instance.GetGameObject("SkillGroupPrefab");
        Skill1Prefab = UIManager.Instance.GetGameObject("Skill1Prefab");
        Skill2Prefab = UIManager.Instance.GetGameObject("Skill2Prefab");
        PopupSkillsPanelPrefab = UIManager.Instance.GetGameObject("PopupSkillsPanelPrefab");
        PopupSkillDetailPrefab = UIManager.Instance.GetGameObject("PopupSkillDetailPrefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void CreateUserCardMonsters(List<CardMonsters> monstersList, Transform DictionaryContentPanel)
    {
        foreach (var monster in monstersList)
        {
            GameObject monstersObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = monstersObject.transform.Find("Title").GetComponent<Text>();
            Title.text = monster.name.Replace("_", " ");

            RawImage Image = monstersObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(monster.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = monstersObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                MainMenuDetailsManager.Instance.PopupDetails(monster, MainPanel);
            });

            RawImage rareImage = monstersObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monster.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateUserCardMonstersForSummon(List<CardMonsters> monsters, Transform PositionPanel)
    {
        foreach (var monster in monsters)
        {
            GameObject monsterObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = monsterObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(monster.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            // if (gridLayout != null)
            // {
            //     gridLayout.cellSize = new Vector2(200, 250);
            // }
        }
    }
    public void ShowCardMonstersDetails(CardMonsters cardMonsters, GameObject currentObject, int buttonType = 1)
    {
        tempCurrentObject = currentObject;
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(3, "Skills", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);
        ButtonLoader.Instance.CreateButton(5, "Spirit Beast", RightButtonContent);
        ButtonLoader.Instance.CreateButton(6, "Rank", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardMonsters, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardMonsters, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardMonsters, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardMonsters, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", RightButtonContent, () =>
        {
            GetSpiritBeast(cardMonsters, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_5", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", RightButtonContent, () =>
        {
            GetRank(cardMonsters, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_6", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(cardMonsters, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                GetLevel(cardMonsters, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(cardMonsters, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                GetUpgrade(cardMonsters, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            case 5:
                GetSpiritBeast(cardMonsters, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_5", RightButtonContent);
                break;
            case 6:
                GetRank(cardMonsters, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_6", RightButtonContent);
                break;
            default:
                GetDetails(cardMonsters, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void CreateDetailsUI(CardMonsters cardMonsters, GameObject currentObject)
    {
        RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMonsters.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardMonsters.name;

        TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardMonsters.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = cardMonsters.level.ToString();

        RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMonsters.rare}");
        rareImage.texture = rareTexture;
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is CardMonsters cardMonsters)
        {
            CreateDetailsUI(cardMonsters, currentObject);
            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardMonsters.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, cardMonsters, currentObject);
        }
    }
    public void GetLevel(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CardMonsters cardMonsters)
        {
            PropertyInfo[] properties = cardMonsters.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, cardMonsters, increasePerLevel, currentObject);
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForLevel(AppConstants.MainType.CARD_MONSTER);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                CardMonsters currentCard = new CardMonsters();
                currentCard = UserCardMonstersService.Create().GetUserCardMonstersById(User.CurrentUserId, cardMonsters.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardMonsters newCard = new CardMonsters();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCardMonstersService.Create().GetNewLevelPower(cardMonsters, increasePerLevel);
                    UserCardMonstersService.Create().UpdateCardMonstersLevel(newCard, currentLevel + 1);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    GetLevel(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                CardMonsters currentCard = UserCardMonstersService.Create().GetUserCardMonstersById(User.CurrentUserId, cardMonsters.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = MainMenuDetailsManager.Instance.UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    CardMonsters newCard = UserCardMonstersService.Create().GetNewLevelPower(cardMonsters, levelsGained * increasePerLevel);
                    UserCardMonstersService.Create().UpdateCardMonstersLevel(newCard, currentLevel);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    GetLevel(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
        }
    }
    public void GetSkills(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonSkillsPanels();
        Transform skillContent = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel/Scroll View/Viewport/Content");
        Button setUpButton = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel/SetUpButton").GetComponent<Button>();
        if (obj is CardMonsters cardMonsters)
        {
            var skills = UserSkillsService.Create().GetUserCardMonstersSkills(User.CurrentUserId, cardMonsters.id);
            skills = skills.Where(x => x.position != 0).ToList();
            foreach (var skill in skills)
            {
                GameObject skillObject = Instantiate(Skill1Prefab, skillContent);
                RawImage skillImage = skillObject.transform.Find("SkillImage").GetComponent<RawImage>();
                Texture skillImageTexure = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(skill.image)}");
                skillImage.texture = skillImageTexure;

                TextMeshProUGUI skillTitleText = skillObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                skillTitleText.text = skill.name;

                RawImage skillBackgroundImage = skillObject.transform.Find("Background").GetComponent<RawImage>();
                string skillBackground = EvaluateSkill.GetBackgroundForSkill(skill.type);
                Texture skillBackgroundImageTexture = Resources.Load<Texture>($"{skillBackground}");
                skillBackgroundImage.texture = skillBackgroundImageTexture;
            }
            setUpButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                CreateSkillPanel(cardMonsters.id);
            });
        }
    }
    public void CreateSkillPanel(string cardId)
    {
        skillPanelObject = Instantiate(SkillPanelPrefab, MainPanel);
        Transform skillGroupContent = skillPanelObject.transform.Find("DictionaryCards/SkillGroup");
        Button CloseButton = skillPanelObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = skillPanelObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(skillPanelObject);
        });
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ButtonEvent.Instance.Close(MainPanel);
        });

        int activeSkillPosition = 1;
        int passiveSkill1Position = 2;
        int passiveSkill2Position = 3;

        List<string> uniqueTypes = TypeManager.GetUniqueTypes(AppConstants.MainType.SKILL);
        for (int i = 0; i < uniqueTypes.Count; i++)
        {
            string currentType = uniqueTypes[i];
            GameObject skillGroupObject = Instantiate(SkillGroupPrefab, skillGroupContent);

            TextMeshProUGUI skillTitleText = skillGroupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            skillTitleText.text = currentType;

            RawImage skillBackgroundImage = skillGroupObject.transform.Find("Background4").GetComponent<RawImage>();
            string skillBackground = EvaluateSkill.GetBackgroundForSkill(currentType);
            Texture skillBackgroundImageTexture = Resources.Load<Texture>($"{skillBackground}");
            skillBackgroundImage.texture = skillBackgroundImageTexture;

            Button activeSkillButton = skillGroupObject.transform.Find("ActiveSkillButton").GetComponent<Button>();
            Button passiveSkillButton1 = skillGroupObject.transform.Find("PassiveSkillButton1").GetComponent<Button>();
            Button passiveSkillButton2 = skillGroupObject.transform.Find("PassiveSkillButton2").GetComponent<Button>();

            var skills = UserSkillsService.Create().GetUserCardMonstersSkills(User.CurrentUserId, cardId);
            skills = skills.Where(x => x.type.Equals(currentType)).ToList();
            var activeSkill = skills.FirstOrDefault(x => x.position == activeSkillPosition);
            var passiveSkill1 = skills.FirstOrDefault(x => x.position == passiveSkill1Position);
            var passiveSkill2 = skills.FirstOrDefault(x => x.position == passiveSkill2Position);

            activeSkillButton.onClick.RemoveAllListeners();
            passiveSkillButton1.onClick.RemoveAllListeners();
            passiveSkillButton2.onClick.RemoveAllListeners();

            if (activeSkill != null)
            {
                RawImage activeSkillImage = activeSkillButton.GetComponent<RawImage>();
                Texture activeSkillImageTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(activeSkill.image)}");
                activeSkillImage.texture = activeSkillImageTexture;
                activeSkillButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    // MainMenuDetailsManager.Instance.PopupDetails(activeSkill, MainPanel);
                    CreatePopupSkillDetail(activeSkillPosition, cardId, currentType, AppConstants.Status.ACTIVE, activeSkill);
                });
            }
            else
            {
                activeSkillButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    CreateSkillPopup(activeSkillPosition, cardId, currentType, AppConstants.Status.ACTIVE);
                });
            }

            if (passiveSkill1 != null)
            {
                RawImage passiveSkill1Image = passiveSkillButton1.GetComponent<RawImage>();
                Texture passiveSkill1ImageTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(passiveSkill1.image)}");
                passiveSkill1Image.texture = passiveSkill1ImageTexture;
                passiveSkillButton1.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    // MainMenuDetailsManager.Instance.PopupDetails(passiveSkill1, MainPanel);
                    CreatePopupSkillDetail(passiveSkill1Position, cardId, currentType, AppConstants.Status.PASSIVE, passiveSkill1);
                });
            }
            else
            {
                passiveSkillButton1.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    CreateSkillPopup(passiveSkill1Position, cardId, currentType, AppConstants.Status.PASSIVE);
                });
            }

            if (passiveSkill2 != null)
            {
                RawImage passiveSkill2Image = passiveSkillButton2.GetComponent<RawImage>();
                Texture passiveSkill2ImageTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(passiveSkill2.image)}");
                passiveSkill2Image.texture = passiveSkill2ImageTexture;
                passiveSkillButton2.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    // MainMenuDetailsManager.Instance.PopupDetails(passiveSkill2, MainPanel);
                    CreatePopupSkillDetail(passiveSkill2Position, cardId, currentType, AppConstants.Status.PASSIVE, passiveSkill2);
                });
            }
            else
            {
                passiveSkillButton2.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    CreateSkillPopup(passiveSkill2Position, cardId, currentType, AppConstants.Status.PASSIVE);
                });
            }
        }
    }
    public void CreateSkillPopup(int position, string cardId, string type, string skillType, Skills oldSkill = null)
    {
        GameObject skillPopupObject = Instantiate(PopupSkillsPanelPrefab, MainPanel);
        Transform skillContent = skillPopupObject.transform.Find("Scroll View/Viewport/Content");
        Button closeButton = skillPopupObject.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(skillPopupObject);
        });

        var skills = UserSkillsService.Create().GetUserCardMonstersSkills(User.CurrentUserId, cardId);
        skills = skills.Where(x => x.type.Equals(type)
                                && x.position != 1
                                && x.position != 2
                                && x.position != 3
                                && x.skill_type.Equals(skillType)).ToList();

        foreach (var skill in skills)
        {
            GameObject skillObject = Instantiate(Skill2Prefab, skillContent);
            RawImage skillImage = skillObject.transform.Find("SkillImage").GetComponent<RawImage>();
            Texture skillImageTexure = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(skill.image)}");
            skillImage.texture = skillImageTexure;

            TextMeshProUGUI skillTitleText = skillObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            skillTitleText.text = skill.name;

            RawImage skillBackgroundImage = skillObject.transform.Find("Background").GetComponent<RawImage>();
            string skillBackground = EvaluateSkill.GetBackgroundForSkill(skill.type);
            Texture skillBackgroundImageTexture = Resources.Load<Texture>($"{skillBackground}");
            skillBackgroundImage.texture = skillBackgroundImageTexture;

            Button equipButton = skillObject.transform.Find("EquipButton").GetComponent<Button>();
            equipButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(skillPopupObject);
                Destroy(skillPanelObject);
                if (oldSkill != null)
                {
                    UserSkillsService.Create().DeleteUserCardMonstersSkills(User.CurrentUserId, cardId, oldSkill.id, position);
                    UserSkillsService.Create().InsertUserCardMonstersSkills(User.CurrentUserId, cardId, skill.id, position);
                }
                else
                {
                    UserSkillsService.Create().InsertUserCardMonstersSkills(User.CurrentUserId, cardId, skill.id, position);
                }
                CreateSkillPanel(cardId);
            });
        }
    }
    public void CreatePopupSkillDetail(int position, string cardId, string type, string skillType, Skills skill)
    {
        GameObject popupSkillDetailObject = Instantiate(PopupSkillDetailPrefab, MainPanel);
        Button closeButton = popupSkillDetailObject.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(popupSkillDetailObject);
        });

        RawImage skillImage = popupSkillDetailObject.transform.Find("SkillImage").GetComponent<RawImage>();
        Texture skillImageTexure = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(skill.image)}");
        skillImage.texture = skillImageTexure;

        TextMeshProUGUI skillTitleText = popupSkillDetailObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        skillTitleText.text = skill.name;

        Button removeButton = popupSkillDetailObject.transform.Find("RemoveButton").GetComponent<Button>();
        removeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(popupSkillDetailObject);
            Destroy(skillPanelObject);
            UserSkillsService.Create().DeleteUserCardMonstersSkills(User.CurrentUserId, cardId, skill.id, position);
            CreateSkillPanel(cardId);
        });

        Button swapButton = popupSkillDetailObject.transform.Find("SwapButton").GetComponent<Button>();
        swapButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(popupSkillDetailObject);       
            CreateSkillPopup(position, cardId, type, skillType, skill);
        });
    }
    public void GetUpgrade(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonUpgradePanels();
        Button breakthroughButton = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/BreakthroughButton").GetComponent<Button>();
        Transform UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        Transform UpgradeMaterialContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CardMonsters cardMonsters)
        {
            PropertyInfo[] properties = cardMonsters.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardMonsters, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForBreakthourgh(AppConstants.MainType.CARD_MONSTER);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(items1.image);
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardMonsters.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMonsters.image);
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardMonsters.quantity.ToString() + "/" + (cardMonsters.star + 1).ToString();

            UIManager.Instance.CreateStarUI(cardMonsters.star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                int requiredQuantity = cardMonsters.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardMonsters.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardMonsters.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardMonsters.quantity >= requiredQuantity)
                    {
                        cardMonsters.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardMonsters.quantity;
                        cardMonsters.quantity = 0; // Dùng hết thẻ bài

                        foreach (Items items1 in items)
                        {
                            if (remainingRequired <= 0) break; // Đã đủ vật phẩm để nâng cấp

                            if (items1.quantity >= remainingRequired)
                            {
                                items1.quantity -= remainingRequired;
                                remainingRequired = 0;
                            }
                            else
                            {
                                remainingRequired -= items1.quantity;
                                items1.quantity = 0; // Dùng hết vật phẩm này
                            }
                        }
                    }

                    foreach (Items items1 in items)
                    {
                        userItemsService.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardMonsters newMonster = new CardMonsters();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newMonster = UserCardMonstersService.Create().GetNewBreakthroughPower(cardMonsters, increasePerUpgrade);
                    UserCardMonstersService.Create().UpdateCardMonstersBreakthrough(newMonster, cardMonsters.star + 1, cardMonsters.quantity);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    CardMonstersGalleryService.Create().UpdateStarCardMonstersGallery(cardMonsters.id, cardMonsters.star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    GetUpgrade(obj, currentObject);
                    UIManager.Instance.CreateStarUI(cardMonsters.star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp CardMonsters!");
                }
            });
        }
    }
    public void GetSpiritBeast(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonSpiritBeastPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Background1").GetComponent<RawImage>();
        Button addButton = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/AddButton").GetComponent<Button>();
        Button removeButton = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/RemoveButton").GetComponent<Button>();

        pageSize = 100;
        offset = 0;
        currentPage = 1;

        background1Image.gameObject.AddComponent<RotateAnimation>();

        if (obj is CardMonsters cardMonsters)
        {
            var userCardSpiritBeast = UserSpiritBeastService.Create().GetUserCardMonstersSpiritBeast(User.CurrentUserId, cardMonsters);
            RawImage spiritBeastImage = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = userCardSpiritBeast.image != null
                ? ImageExtensionHandler.RemoveImageExtension(userCardSpiritBeast.image)
                : "UI/Background4/Background_V4_352";
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            spiritBeastImage.texture = texture;

            CreateDetailsUI(cardMonsters, currentObject);
            addButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                CreatePopupEquipments(obj, currentObject);
            });

            removeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                UserSpiritBeastService.Create().DeleteUserCardMonstersSpiritBeast(User.CurrentUserId, cardMonsters, userCardSpiritBeast);
                string fileNameWithoutExtension = "UI/Background4/Background_V4_352";
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                spiritBeastImage.texture = texture;

                var card = UserCardMonstersService.Create().GetUserCardMonstersById(User.CurrentUserId, cardMonsters.id);
                ShowCardMonstersDetails(card, currentObject, 5);
            });
        }
    }
    public void CreatePopupEquipments(object data, GameObject currentObject, string statusToggle = "NOT EQUIP")
    {
        popupSpiritBeastObject = Instantiate(PopupSpiritBeastPanelPrefab, MainPanel);
        Transform contentPanel = popupSpiritBeastObject.transform.Find("Scroll View/Viewport/Content");
        Text PageText = popupSpiritBeastObject.transform.Find("Pagination/Page").GetComponent<Text>();
        Toggle toggle = popupSpiritBeastObject.transform.Find("Toggle").GetComponent<Toggle>();
        toggle.isOn = (statusToggle == "ALL");
        toggle.onValueChanged.AddListener((bool isOn) =>
        {
            string newStatusToggle = isOn ? "ALL" : "NOT EQUIP";
            Destroy(popupSpiritBeastObject);
            CreatePopupEquipments(data, currentObject, newStatusToggle); // Gọi lại nhưng giữ statusToggle mới
        });
        Button NextButton = popupSpiritBeastObject.transform.Find("Pagination/Next").GetComponent<Button>();
        Button PreviousButton = popupSpiritBeastObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        Button CloseButton = popupSpiritBeastObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.RemoveAllListeners();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(popupSpiritBeastObject);
        });
        Equipments equipments = new Equipments();
        List<SpiritBeast> spiritBeasts = new List<SpiritBeast>();
        spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardMonstersSpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);

        int totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, AppConstants.Rare.All);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        CreatePopupEquipmentsUI(data, spiritBeasts, contentPanel, currentObject);
        NextButton.onClick.RemoveAllListeners();
        PreviousButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
            ChangeNextPage(data, PageText, contentPanel, currentObject);
        });
        PreviousButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
            ChangePreviousPage(data, PageText, contentPanel, currentObject);
        });
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void CreatePopupEquipmentsUI(object data, List<SpiritBeast> spiritBeasts, Transform content, GameObject currentObject)
    {
        foreach (var spiritBeast in spiritBeasts)
        {
            GameObject equipmentObject = Instantiate(EquipmentsWearingPrefab, content);

            TextMeshProUGUI Title = equipmentObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = spiritBeast.name.Replace("_", " ");

            TextMeshProUGUI Power = equipmentObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
            Power.text = spiritBeast.power.ToString();

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spiritBeast.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spiritBeast.rare}");
            rareImage.texture = rareTexture;

            Button EquipButton = equipmentObject.transform.Find("EquipButton").GetComponent<Button>();
            EquipButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Destroy(popupSpiritBeastObject);
                if (data is CardMonsters cardMonsters)
                {
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    UserSpiritBeastService.Create().InsertOrUpdateUserCardMonstersSpiritBeast(User.CurrentUserId, cardMonsters, spiritBeast);

                    RawImage spiritBeastImage = tempCurrentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
                    var userCardSpiritBeast = UserSpiritBeastService.Create().GetUserCardMonstersSpiritBeast(User.CurrentUserId, cardMonsters);
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(userCardSpiritBeast.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    spiritBeastImage.texture = texture;

                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    var card = UserCardMonstersService.Create().GetUserCardMonstersById(User.CurrentUserId, cardMonsters.id);
                    ShowCardMonstersDetails(card, currentObject, 5);
                }

                Destroy(popupSpiritBeastObject);
            });
        }
        GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(340, 130);
        }
    }
    public void ChangeNextPage(object data, Text PageText, Transform content, GameObject currentObject)
    {
        if (currentPage < totalPage)
        {
            ButtonEvent.Instance.Close(content);
            int totalRecord = 0;

            totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, AppConstants.Rare.All);
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            currentPage = currentPage + 1;
            offset = offset + pageSize;
            List<SpiritBeast> spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardMonstersSpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);
            CreatePopupEquipmentsUI(data, spiritBeasts, content, currentObject);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage(object data, Text PageText, Transform content, GameObject currentObject)
    {
        if (currentPage > 1)
        {
            ButtonEvent.Instance.Close(content);
            int totalRecord = 0;

            totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, AppConstants.Rare.All);
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            currentPage = currentPage - 1;
            offset = offset - pageSize;
            List<SpiritBeast> spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardMonstersSpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);
            CreatePopupEquipmentsUI(data, spiritBeasts, content, currentObject);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void GetRank(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonRankPanels();
    }
}
