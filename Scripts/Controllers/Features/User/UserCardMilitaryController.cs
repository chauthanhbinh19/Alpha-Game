using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserCardMilitaryController : MonoBehaviour
{
    public static UserCardMilitaryController Instance { get; private set; }
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
    public void CreateUserCardMilitary(List<CardMilitaries> militaryList, Transform DictionaryContentPanel)
    {
        foreach (var military in militaryList)
        {
            GameObject militaryObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = militaryObject.transform.Find("Title").GetComponent<Text>();
            Title.text = military.Name.Replace("_", " ");

            RawImage Image = militaryObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(military.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = militaryObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
                MainMenuDetailsManager.Instance.PopupDetails(military, MainPanel);
            });

            RawImage rareImage = militaryObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.Rare}");
            rareImage.texture = rareTexture;

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 250);
            }
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateUserCardMilitaryForSummon(List<CardMilitaries> militaries, Transform PositionPanel)
    {
        foreach (var military in militaries)
        {
            GameObject militaryObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = militaryObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(military.Image);
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
    public void ShowCardMilitaryDetails(CardMilitaries cardMilitary, GameObject currentObject, int buttonType = 1)
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
            GetDetails(cardMilitary, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardMilitary, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardMilitary, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardMilitary, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", RightButtonContent, () =>
        {
            GetSpiritBeast(cardMilitary, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_5", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", RightButtonContent, () =>
        {
            GetRank(cardMilitary, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_6", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(cardMilitary, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                GetLevel(cardMilitary, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(cardMilitary, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                GetUpgrade(cardMilitary, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            case 5:
                GetSpiritBeast(cardMilitary, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_5", RightButtonContent);
                break;
            case 6:
                GetRank(cardMilitary, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_6", RightButtonContent);
                break;
            default:
                GetDetails(cardMilitary, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void CreateDetailsUI(CardMilitaries cardMilitary, GameObject currentObject)
    {
        RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardMilitary.Name;

        TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardMilitary.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = cardMilitary.level.ToString();

        RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMilitary.Rare}");
        rareImage.texture = rareTexture;
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is CardMilitaries cardMilitary)
        {
            CreateDetailsUI(cardMilitary, currentObject);
            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardMilitary.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, cardMilitary, currentObject);
        }
    }
    public void GetLevel(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CardMilitaries cardMilitary)
        {
            PropertyInfo[] properties = cardMilitary.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, cardMilitary, increasePerLevel, currentObject);
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForLevel(AppConstants.MainType.CARD_MILITARY);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
                CardMilitaries currentCard = new CardMilitaries();
                currentCard = UserCardMilitaryService.Create().GetUserCardMilitaryById(User.CurrentUserId, cardMilitary.Id);
                int totalExperiment = currentCard.Experiment;
                int currentLevel = currentCard.Level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardMilitaries newCard = new CardMilitaries();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCardMilitaryService.Create().GetNewLevelPower(cardMilitary, increasePerLevel);
                    UserCardMilitaryService.Create().UpdateCardMilitaryLevel(newCard, currentLevel + 1);
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
                CardMilitaries currentCard = UserCardMilitaryService.Create().GetUserCardMilitaryById(User.CurrentUserId, cardMilitary.Id);
                int totalExperiment = currentCard.Experiment;
                int currentLevel = currentCard.Level;
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
                    CardMilitaries newCard = UserCardMilitaryService.Create().GetNewLevelPower(cardMilitary, levelsGained * increasePerLevel);
                    UserCardMilitaryService.Create().UpdateCardMilitaryLevel(newCard, currentLevel);
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
        if (obj is CardMilitaries cardMilitary)
        {
            var skills = UserSkillsService.Create().GetUserCardMilitarySkills(User.CurrentUserId, cardMilitary.Id);
            skills = skills.Where(x => x.Position != 0).ToList();
            foreach (var skill in skills)
            {
                GameObject skillObject = Instantiate(Skill1Prefab, skillContent);
                RawImage skillImage = skillObject.transform.Find("SkillImage").GetComponent<RawImage>();
                Texture skillImageTexure = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(skill.Image)}");
                skillImage.texture = skillImageTexure;

                TextMeshProUGUI skillTitleText = skillObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                skillTitleText.text = skill.Name;

                RawImage skillBackgroundImage = skillObject.transform.Find("Background").GetComponent<RawImage>();
                string skillBackground = EvaluateSkill.GetBackgroundForSkill(skill.Type);
                Texture skillBackgroundImageTexture = Resources.Load<Texture>($"{skillBackground}");
                skillBackgroundImage.texture = skillBackgroundImageTexture;
            }
            setUpButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
                CreateSkillPanel(cardMilitary.Id);
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

            var skills = UserSkillsService.Create().GetUserCardMilitarySkills(User.CurrentUserId, cardId);
            skills = skills.Where(x => x.Type.Equals(currentType)).ToList();
            var activeSkill = skills.FirstOrDefault(x => x.Position == activeSkillPosition);
            var passiveSkill1 = skills.FirstOrDefault(x => x.Position == passiveSkill1Position);
            var passiveSkill2 = skills.FirstOrDefault(x => x.Position == passiveSkill2Position);

            activeSkillButton.onClick.RemoveAllListeners();
            passiveSkillButton1.onClick.RemoveAllListeners();
            passiveSkillButton2.onClick.RemoveAllListeners();

            if (activeSkill != null)
            {
                RawImage activeSkillImage = activeSkillButton.GetComponent<RawImage>();
                Texture activeSkillImageTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(activeSkill.Image)}");
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
                Texture passiveSkill1ImageTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(passiveSkill1.Image)}");
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
                Texture passiveSkill2ImageTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(passiveSkill2.Image)}");
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

        var skills = UserSkillsService.Create().GetUserCardMilitarySkills(User.CurrentUserId, cardId);
        skills = skills.Where(x => x.Type.Equals(type)
                                && x.Position != 1
                                && x.Position != 2
                                && x.Position != 3
                                && x.SkillType.Equals(skillType)).ToList();

        foreach (var skill in skills)
        {
            GameObject skillObject = Instantiate(Skill2Prefab, skillContent);
            RawImage skillImage = skillObject.transform.Find("SkillImage").GetComponent<RawImage>();
            Texture skillImageTexure = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(skill.Image)}");
            skillImage.texture = skillImageTexure;

            TextMeshProUGUI skillTitleText = skillObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            skillTitleText.text = skill.Name;

            RawImage skillBackgroundImage = skillObject.transform.Find("Background").GetComponent<RawImage>();
            string skillBackground = EvaluateSkill.GetBackgroundForSkill(skill.Type);
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
                    UserSkillsService.Create().DeleteUserCardMilitarySkills(User.CurrentUserId, cardId, oldSkill.Id, position);
                    UserSkillsService.Create().InsertUserCardMilitarySkills(User.CurrentUserId, cardId, skill.Id, position);
                }
                else
                {
                    UserSkillsService.Create().InsertUserCardMilitarySkills(User.CurrentUserId, cardId, skill.Id, position);
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
        Texture skillImageTexure = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(skill.Image)}");
        skillImage.texture = skillImageTexure;

        TextMeshProUGUI skillTitleText = popupSkillDetailObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        skillTitleText.text = skill.Name;

        Button removeButton = popupSkillDetailObject.transform.Find("RemoveButton").GetComponent<Button>();
        removeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(popupSkillDetailObject);
            Destroy(skillPanelObject);
            UserSkillsService.Create().DeleteUserCardMilitarySkills(User.CurrentUserId, cardId, skill.Id, position);
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
        if (obj is CardMilitaries cardMilitary)
        {
            PropertyInfo[] properties = cardMilitary.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardMilitary, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForBreakthourgh(AppConstants.MainType.CARD_MILITARY);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(items1.Image);
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.Quantity.ToString() + "/" + (cardMilitary.Star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image);
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardMilitary.Quantity.ToString() + "/" + (cardMilitary.Star + 1).ToString();

            UIManager.Instance.CreateStarUI(cardMilitary.Star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                int requiredQuantity = cardMilitary.Star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardMilitary.Quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.Quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardMilitary.Quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardMilitary.Quantity >= requiredQuantity)
                    {
                        cardMilitary.Quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardMilitary.Quantity;
                        cardMilitary.Quantity = 0; // Dùng hết thẻ bài

                        foreach (Items items1 in items)
                        {
                            if (remainingRequired <= 0) break; // Đã đủ vật phẩm để nâng cấp

                            if (items1.Quantity >= remainingRequired)
                            {
                                items1.Quantity -= remainingRequired;
                                remainingRequired = 0;
                            }
                            else
                            {
                                remainingRequired -= items1.Quantity;
                                items1.Quantity = 0; // Dùng hết vật phẩm này
                            }
                        }
                    }

                    foreach (Items items1 in items)
                    {
                        userItemsService.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardMilitaries newCard = new CardMilitaries();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCardMilitaryService.Create().GetNewBreakthroughPower(cardMilitary, increasePerUpgrade);
                    UserCardMilitaryService.Create().UpdateCardMilitaryBreakthrough(newCard, cardMilitary.Star + 1, cardMilitary.Quantity);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    CardMilitaryGalleryService.Create().UpdateStarCardMilitaryGallery(cardMilitary.Id, cardMilitary.Star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    GetUpgrade(obj, currentObject);
                    UIManager.Instance.CreateStarUI(cardMilitary.Star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
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

        if (obj is CardMilitaries cardMilitary)
        {
            var userCardSpiritBeast = UserSpiritBeastService.Create().GetUserCardMilitarySpiritBeast(User.CurrentUserId, cardMilitary);
            RawImage spiritBeastImage = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = userCardSpiritBeast.Image != null
                ? ImageExtensionHandler.RemoveImageExtension(userCardSpiritBeast.Image)
                : "UI/Background4/Background_V4_352";
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            spiritBeastImage.texture = texture;

            CreateDetailsUI(cardMilitary, currentObject);
            addButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                CreatePopupEquipments(obj, currentObject);
            });
            removeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                UserSpiritBeastService.Create().DeleteUserCardMilitarySpiritBeast(User.CurrentUserId, cardMilitary, userCardSpiritBeast);
                string fileNameWithoutExtension = "UI/Background4/Background_V4_352";
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                spiritBeastImage.texture = texture;

                var card = UserCardMilitaryService.Create().GetUserCardMilitaryById(User.CurrentUserId, cardMilitary.Id);
                ShowCardMilitaryDetails(card, currentObject, 5);
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
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardMilitarySpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);

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
    public void CreatePopupEquipmentsUI(object data, List<SpiritBeasts> spiritBeasts, Transform content, GameObject currentObject)
    {
        foreach (var spiritBeast in spiritBeasts)
        {
            GameObject equipmentObject = Instantiate(EquipmentsWearingPrefab, content);

            TextMeshProUGUI Title = equipmentObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = spiritBeast.Name.Replace("_", " ");

            TextMeshProUGUI Power = equipmentObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
            Power.text = spiritBeast.Power.ToString();

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spiritBeast.Image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spiritBeast.Rare}");
            rareImage.texture = rareTexture;

            Button EquipButton = equipmentObject.transform.Find("EquipButton").GetComponent<Button>();
            EquipButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK);
                Destroy(popupSpiritBeastObject);
                if (data is CardMilitaries cardMilitary)
                {
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    UserSpiritBeastService.Create().InsertOrUpdateUserCardMilitarySpiritBeast(User.CurrentUserId, cardMilitary, spiritBeast);

                    RawImage spiritBeastImage = tempCurrentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
                    var userCardSpiritBeast = UserSpiritBeastService.Create().GetUserCardMilitarySpiritBeast(User.CurrentUserId, cardMilitary);
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(userCardSpiritBeast.Image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    spiritBeastImage.texture = texture;

                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    var card = UserCardMilitaryService.Create().GetUserCardMilitaryById(User.CurrentUserId, cardMilitary.Id);
                    ShowCardMilitaryDetails(card, currentObject, 5);
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
            List<SpiritBeasts> spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardMilitarySpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);
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
            List<SpiritBeasts> spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardMilitarySpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);
            CreatePopupEquipmentsUI(data, spiritBeasts, content, currentObject);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void GetRank(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonRankPanels();
    }
}
