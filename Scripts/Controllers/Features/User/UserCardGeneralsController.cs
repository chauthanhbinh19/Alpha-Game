using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserCardGeneralsController : MonoBehaviour
{
    public static UserCardGeneralsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardGeneralButtonPrefab;
    private GameObject PositionPrefab;
    private GameObject ElementDetails2Prefab;
    private const double INCREASE_PER_LEVEL = 0.01;
    private const double INCREASE_PER_UPGRADE = 1.1;
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
    private const int PAGE_SIZE = 100;
    private int offset;
    private int currentPage;
    private int totalPage;
    private string statusToggle;
    private string search;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        CardGeneralButtonPrefab = UIManager.Instance.Get("CardGeneralButtonPrefab");
        PositionPrefab = UIManager.Instance.Get("PositionPrefab");
        ElementDetails2Prefab = UIManager.Instance.Get("ElementDetails2Prefab");
        PopupSpiritBeastPanelPrefab = UIManager.Instance.Get("PopupSpiritBeastPanelPrefab");
        EquipmentsWearingPrefab = UIManager.Instance.Get("EquipmentsWearingPrefab");
        SkillPanelPrefab = UIManager.Instance.Get("SkillPanelPrefab");
        SkillGroupPrefab = UIManager.Instance.Get("SkillGroupPrefab");
        Skill1Prefab = UIManager.Instance.Get("Skill1Prefab");
        Skill2Prefab = UIManager.Instance.Get("Skill2Prefab");
        PopupSkillsPanelPrefab = UIManager.Instance.Get("PopupSkillsPanelPrefab");
        PopupSkillDetailPrefab = UIManager.Instance.Get("PopupSkillDetailPrefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
        search = "";
    }
    public void CreateUserCardGenerals(List<CardGenerals> cardGenerals, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        foreach (var cardGeneral in cardGenerals)
        {
            GameObject cardGeneralObject = Instantiate(CardGeneralButtonPrefab, contentPanel);
            Transform transform = cardGeneralObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardGeneral.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardGeneral.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardGeneral.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERAL);

            TextMeshProUGUI typePanel = transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardGeneral.Type.ToString().Replace("_", " ");

            Image rareBackground = transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardGeneral.Rare));

            Transform teamPanel = transform.Find("Team");
            if(cardGeneral.Team.TeamNumber != 0)
            {
                teamPanel.gameObject.SetActive(true);
                RawImage teamBackgroundImage = transform.Find("Team/Background").GetComponent<RawImage>();
                TextMeshProUGUI teamTitleText = transform.Find("Team/TitleText").GetComponent<TextMeshProUGUI>();
                Texture teamBackgroundTexture = TextureHelper.LoadTextureCached(ImageConstants.Team.TEAM_BACKGROUND_4);
                teamBackgroundImage.texture = teamBackgroundTexture;
                teamTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TEAM) + " " + cardGeneral.Team.TeamNumber.ToString();
            }
            else
            {
                teamPanel.gameObject.SetActive(false);
            }

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                MainMenuDetailsManager.Instance.PopupDetails(cardGeneral, MainPanel);
            });

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardGeneral.Rare));
            rareText.text = cardGeneral.Rare;
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(250, 360);
            gridLayout.spacing = new Vector2(23, 10);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateUserCardGeneralsForSummon(List<CardGenerals> cardGenerals, Transform PositionPanel)
    {
        foreach (var general in cardGenerals)
        {
            GameObject cardGeneralObject = Instantiate(PositionPrefab, PositionPanel);
            Transform transform = cardGeneralObject.transform;

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(general.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
        }
    }
    public void ShowCardGeneralDetails(CardGenerals cardGeneral, GameObject currentObject, int buttonType = 1)
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
            GetDetails(cardGeneral, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            _=GetLevelAsync(cardGeneral, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            _=GetSkillsAsync(cardGeneral, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            _=GetUpgradeAsync(cardGeneral, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", RightButtonContent, () =>
        {
            _=GetSpiritBeastAsync(cardGeneral, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_5", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", RightButtonContent, () =>
        {
            GetRank(cardGeneral, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_6", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(cardGeneral, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                _=GetLevelAsync(cardGeneral, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                _=GetSkillsAsync(cardGeneral, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                _=GetUpgradeAsync(cardGeneral, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            case 5:
                _=GetSpiritBeastAsync(cardGeneral, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_5", RightButtonContent);
                break;
            case 6:
                GetRank(cardGeneral, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_6", RightButtonContent);
                break;
            default:
                GetDetails(cardGeneral, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void CreateDetailsUI(CardGenerals cardGeneral, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardGeneral.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardGeneral.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardGeneral.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = cardGenerals.level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardGeneral.Rare}");
        rareImage.texture = rareTexture;
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is CardGenerals cardGeneral)
        {
            CreateDetailsUI(cardGeneral, currentObject);
            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardGeneral.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, cardGeneral, currentObject);
        }
    }
    public async Task GetLevelAsync(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Transform transform = currentObject.transform;
        Button up1LevelButton = transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CardGenerals cardGeneral)
        {
            PropertyInfo[] properties = cardGeneral.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, cardGeneral, INCREASE_PER_LEVEL, currentObject);
            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForLevelAsync(AppConstants.MainType.CARD_GENERAL);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                CardGenerals currentCardGeneral = new CardGenerals();
                currentCardGeneral = await UserCardGeneralsService.Create().GetUserCardGeneralByIdAsync(User.CurrentUserId, cardGeneral.Id);
                double totalExperiment = currentCardGeneral.Experiment;
                int currentLevel = currentCardGeneral.Level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardGenerals newCardGeneral = new CardGenerals();

                    newCardGeneral = await UnitLevelHelper.GetNewLevelPowerAsync(cardGeneral, INCREASE_PER_LEVEL);
                    await UserCardGeneralsService.Create().UpdateCardGeneralLevelAsync(newCardGeneral, currentLevel + 1);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    await GetLevelAsync(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
            upMaxLevelButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                CardGenerals currentCardGeneral = await UserCardGeneralsService.Create().GetUserCardGeneralByIdAsync(User.CurrentUserId, cardGeneral.Id);
                double totalExperiment = currentCardGeneral.Experiment;
                int currentLevel = currentCardGeneral.Level;
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

                    CardGenerals newCardGeneral = await UnitLevelHelper.GetNewLevelPowerAsync(cardGeneral, levelsGained * INCREASE_PER_LEVEL);
                    await UserCardGeneralsService.Create().UpdateCardGeneralLevelAsync(newCardGeneral, currentLevel);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    await GetLevelAsync(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
        }
    }
    public async Task GetSkillsAsync(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonSkillsPanels();
        Transform transform = currentObject.transform;
        Transform skillContent = transform.Find("DictionaryCards/Content/SkillsPanel/Scroll View/Viewport/Content");
        Button setUpButton = transform.Find("DictionaryCards/Content/SkillsPanel/SetUpButton").GetComponent<Button>();
        if (obj is CardGenerals cardGeneral)
        {
            var skills = await UserSkillsService.Create().GetUserCardGeneralsSkillsAsync(User.CurrentUserId, cardGeneral.Id);
            skills = skills.Where(x => x.Position != 0).ToList();
            foreach (var skill in skills)
            {
                GameObject skillObject = Instantiate(Skill1Prefab, skillContent);
                Transform skillTransform = skillObject.transform;
                RawImage skillImage = skillTransform.Find("SkillImage").GetComponent<RawImage>();
                Texture skillImageTexure = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(skill.Image)}");
                skillImage.texture = skillImageTexure;

                TextMeshProUGUI skillTitleText = skillTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                skillTitleText.text = skill.Name;

                RawImage skillBackgroundImage = skillTransform.Find("Background").GetComponent<RawImage>();
                string skillBackground = SkillHelper.GetBackgroundForSkill(skill.Type);
                Texture skillBackgroundImageTexture = TextureHelper.LoadTextureCached($"{skillBackground}");
                skillBackgroundImage.texture = skillBackgroundImageTexture;
            }
            setUpButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreateSkillPanelAsync(cardGeneral.Id);
            });
        }
    }
    public async Task CreateSkillPanelAsync(string cardId)
    {
        skillPanelObject = Instantiate(SkillPanelPrefab, MainPanel);
        Transform transform = skillPanelObject.transform;
        Transform skillGroupContent = transform.Find("DictionaryCards/SkillGroup");
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(skillPanelObject);
        });
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        int activeSkillPosition = 1;
        int passiveSkill1Position = 2;
        int passiveSkill2Position = 3;

        List<string> uniqueTypes = await TypeManager.GetUniqueTypesAsync(AppConstants.MainType.SKILL);
        var skills = await UserSkillsService.Create().GetUserCardGeneralsSkillsAsync(User.CurrentUserId, cardId);
        for (int i = 0; i < uniqueTypes.Count; i++)
        {
            string currentType = uniqueTypes[i];
            GameObject skillGroupObject = Instantiate(SkillGroupPrefab, skillGroupContent);
            Transform skillGroupTransform = skillGroupObject.transform;

            TextMeshProUGUI skillTitleText = skillGroupTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            skillTitleText.text = currentType;

            RawImage skillBackgroundImage = skillGroupTransform.Find("Background4").GetComponent<RawImage>();
            string skillBackground = SkillHelper.GetBackgroundForSkill(currentType);
            Texture skillBackgroundImageTexture = TextureHelper.LoadTextureCached($"{skillBackground}");
            skillBackgroundImage.texture = skillBackgroundImageTexture;

            Button activeSkillButton = skillGroupTransform.Find("ActiveSkillButton").GetComponent<Button>();
            Button passiveSkillButton1 = skillGroupTransform.Find("PassiveSkillButton1").GetComponent<Button>();
            Button passiveSkillButton2 = skillGroupTransform.Find("PassiveSkillButton2").GetComponent<Button>();

            var tempSkills = skills.Where(x => x.Type.Equals(currentType)).ToList();
            var activeSkill = tempSkills.FirstOrDefault(x => x.Position == activeSkillPosition);
            var passiveSkill1 = tempSkills.FirstOrDefault(x => x.Position == passiveSkill1Position);
            var passiveSkill2 = tempSkills.FirstOrDefault(x => x.Position == passiveSkill2Position);

            activeSkillButton.onClick.RemoveAllListeners();
            passiveSkillButton1.onClick.RemoveAllListeners();
            passiveSkillButton2.onClick.RemoveAllListeners();

            if (activeSkill != null)
            {
                RawImage activeSkillImage = activeSkillButton.GetComponent<RawImage>();
                Texture activeSkillImageTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(activeSkill.Image)}");
                activeSkillImage.texture = activeSkillImageTexture;
                activeSkillButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    // MainMenuDetailsManager.Instance.PopupDetails(activeSkill, MainPanel);
                    CreatePopupSkillDetail(activeSkillPosition, cardId, currentType, AppConstants.Status.ACTIVE, activeSkill);
                });
            }
            else
            {
                activeSkillButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await CreateSkillPopupAsync(activeSkillPosition, cardId, currentType, AppConstants.Status.ACTIVE);
                });
            }

            if (passiveSkill1 != null)
            {
                RawImage passiveSkill1Image = passiveSkillButton1.GetComponent<RawImage>();
                Texture passiveSkill1ImageTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(passiveSkill1.Image)}");
                passiveSkill1Image.texture = passiveSkill1ImageTexture;
                passiveSkillButton1.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    // MainMenuDetailsManager.Instance.PopupDetails(passiveSkill1, MainPanel);
                    CreatePopupSkillDetail(passiveSkill1Position, cardId, currentType, AppConstants.Status.PASSIVE, passiveSkill1);
                });
            }
            else
            {
                passiveSkillButton1.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await CreateSkillPopupAsync(passiveSkill1Position, cardId, currentType, AppConstants.Status.PASSIVE);
                });
            }

            if (passiveSkill2 != null)
            {
                RawImage passiveSkill2Image = passiveSkillButton2.GetComponent<RawImage>();
                Texture passiveSkill2ImageTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(passiveSkill2.Image)}");
                passiveSkill2Image.texture = passiveSkill2ImageTexture;
                passiveSkillButton2.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    // MainMenuDetailsManager.Instance.PopupDetails(passiveSkill2, MainPanel);
                    CreatePopupSkillDetail(passiveSkill2Position, cardId, currentType, AppConstants.Status.PASSIVE, passiveSkill2);
                });
            }
            else
            {
                passiveSkillButton2.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await CreateSkillPopupAsync(passiveSkill2Position, cardId, currentType, AppConstants.Status.PASSIVE);
                });
            }
        }
    }
    public async Task CreateSkillPopupAsync(int position, string cardId, string type, string skillType, Skills oldSkill = null)
    {
        GameObject skillPopupObject = Instantiate(PopupSkillsPanelPrefab, MainPanel);
        Transform transform = skillPopupObject.transform;
        Transform skillContent = transform.Find("Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(skillPopupObject);
        });

        var skills = await UserSkillsService.Create().GetUserCardGeneralsSkillsAsync(User.CurrentUserId, cardId);
        skills = skills.Where(x => x.Type.Equals(type)
                                && x.Position != 1
                                && x.Position != 2
                                && x.Position != 3
                                && x.SkillType.Equals(skillType)).ToList();

        foreach (var skill in skills)
        {
            GameObject skillObject = Instantiate(Skill2Prefab, skillContent);
            Transform skillTransform = skillObject.transform;
            RawImage skillImage = skillTransform.Find("SkillImage").GetComponent<RawImage>();
            Texture skillImageTexure = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(skill.Image)}");
            skillImage.texture = skillImageTexure;

            TextMeshProUGUI skillTitleText = skillTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            skillTitleText.text = skill.Name;

            RawImage skillBackgroundImage = skillTransform.Find("Background").GetComponent<RawImage>();
            string skillBackground = SkillHelper.GetBackgroundForSkill(skill.Type);
            Texture skillBackgroundImageTexture = TextureHelper.LoadTextureCached($"{skillBackground}");
            skillBackgroundImage.texture = skillBackgroundImageTexture;

            Button equipButton = skillTransform.Find("EquipButton").GetComponent<Button>();
            equipButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(skillPopupObject);
                Destroy(skillPanelObject);
                if (oldSkill != null)
                {
                    await UserSkillsService.Create().DeleteUserCardGeneralSkillsAsync(User.CurrentUserId, cardId, oldSkill.Id, position);
                    await UserSkillsService.Create().InsertUserCardGeneralSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
                }
                else
                {
                    await UserSkillsService.Create().InsertUserCardGeneralSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
                }
                await CreateSkillPanelAsync(cardId);
            }));
        }
    }
    public void CreatePopupSkillDetail(int position, string cardId, string type, string skillType, Skills skill)
    {
        GameObject popupSkillDetailObject = Instantiate(PopupSkillDetailPrefab, MainPanel);
        Transform transform = popupSkillDetailObject.transform;
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupSkillDetailObject);
        });

        RawImage skillImage = transform.Find("SkillImage").GetComponent<RawImage>();
        Texture skillImageTexure = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(skill.Image)}");
        skillImage.texture = skillImageTexure;

        TextMeshProUGUI skillTitleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        skillTitleText.text = skill.Name;

        Button removeButton = transform.Find("RemoveButton").GetComponent<Button>();
        removeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupSkillDetailObject);
            Destroy(skillPanelObject);
            await UserSkillsService.Create().DeleteUserCardGeneralSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
            await CreateSkillPanelAsync(cardId);
        });

        Button swapButton = transform.Find("SwapButton").GetComponent<Button>();
        swapButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupSkillDetailObject);       
            await CreateSkillPopupAsync(position, cardId, type, skillType, skill);
        });
    }
    public async Task GetUpgradeAsync(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonUpgradePanels();
        Transform transform = currentObject.transform;
        Button breakthroughButton = transform.Find("DictionaryCards/Content/UpgradePanel/BreakthroughButton").GetComponent<Button>();
        Transform UpgradeElementContent = transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        Transform UpgradeMaterialContent = transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CardGenerals cardGeneral)
        {
            PropertyInfo[] properties = cardGeneral.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardGeneral, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, INCREASE_PER_UPGRADE, currentObject);
            }
            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForBreakthourghAsync(AppConstants.MainType.CARD_GENERAL);
            string fileNameWithoutExtension = "";
            foreach (Items item in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);
                Transform itemTransform = itemObject.transform;

                RawImage eImage = itemTransform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageHelper.RemoveImageExtension(item.Image);
                Texture equipmentTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = item.Quantity.ToString() + "/" + (cardGeneral.Star + 1).ToString();
            }
            GameObject cardGeneralObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);
            Transform cardGeneralTransform = cardGeneralObject.transform;

            RawImage cardGeneralImage = cardGeneralTransform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardGeneral.Image);
            Texture cardGeneralTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            cardGeneralImage.texture = cardGeneralTexture;

            TextMeshProUGUI cardGeneralQuantity = cardGeneralTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardGeneralQuantity.text = cardGeneral.Quantity.ToString() + "/" + (cardGeneral.Star + 1).ToString();

            UIManager.Instance.CreateStarUI(cardGeneral.Star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                double requiredQuantity = cardGeneral.Star + 1;
                double totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCardGeneral = cardGeneral.Quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items item in items)
                {
                    totalItemQuantity += item.Quantity;
                }
                bool hasEnoughItem = totalItemQuantity + cardGeneral.Quantity >= requiredQuantity;

                if (hasEnoughCardGeneral || hasEnoughItem)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardGeneral.Quantity >= requiredQuantity)
                    {
                        cardGeneral.Quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        double remainingRequired = requiredQuantity - cardGeneral.Quantity;
                        cardGeneral.Quantity = 0; // Dùng hết thẻ bài

                        foreach (Items item in items)
                        {
                            if (remainingRequired <= 0) break; // Đã đủ vật phẩm để nâng cấp

                            if (item.Quantity >= remainingRequired)
                            {
                                item.Quantity -= remainingRequired;
                                remainingRequired = 0;
                            }
                            else
                            {
                                remainingRequired -= item.Quantity;
                                item.Quantity = 0; // Dùng hết vật phẩm này
                            }
                        }
                    }

                    foreach (Items item in items)
                    {
                        await userItemsService.UpdateUserItemQuantityAsync(item);
                    }
                    // Cập nhật cấp sao (Star)
                    CardGenerals newCardGeneral = new CardGenerals();

                    newCardGeneral = await UnitBreakthroughHelper.GetNewBreakthroughPowerAsync(cardGeneral, INCREASE_PER_UPGRADE);
                    await UserCardGeneralsService.Create().UpdateCardGeneralBreakthroughAsync(newCardGeneral, cardGeneral.Star + 1, cardGeneral.Quantity);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    await  CardGeneralsGalleryService.Create().UpdateStarCardGeneralGalleryAsync(cardGeneral.Id, cardGeneral.Star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    await GetUpgradeAsync(obj, currentObject);
                    UIManager.Instance.CreateStarUI(cardGeneral.Star, currentObject);
                }
                else
                {
                    Debug.Log(MessageConstants.ITEM_NOT_ENOUGH);
                }
            });
        }
    }
    public async Task GetSpiritBeastAsync(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonSpiritBeastPanels();
        Transform transform = currentObject.transform;
        RawImage background1Image = transform.Find("DictionaryCards/Content/SpiritBeastPanel/Background1").GetComponent<RawImage>();
        Button addButton = transform.Find("DictionaryCards/Content/SpiritBeastPanel/AddButton").GetComponent<Button>();
        Button removeButton = transform.Find("DictionaryCards/Content/SpiritBeastPanel/RemoveButton").GetComponent<Button>();

        offset = 0;
        currentPage = 1;

        background1Image.gameObject.AddComponent<RotateAnimation>();

        if (obj is CardGenerals cardGeneral)
        {
            var userCardSpiritBeast = await UserSpiritBeastsService.Create().GetUserCardGeneralSpiritBeastAsync(User.CurrentUserId, cardGeneral);
            RawImage spiritBeastImage = transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = userCardSpiritBeast.Image != null
                ? ImageHelper.RemoveImageExtension(userCardSpiritBeast.Image)
                : "UI/Background4/Background_V4_352";
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            spiritBeastImage.texture = texture;

            CreateDetailsUI(cardGeneral, currentObject);
            addButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreatePopupEquipmentsAsync(obj, currentObject);
            });
            removeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await UserSpiritBeastsService.Create().DeleteUserCardGeneralSpiritBeastAsync(User.CurrentUserId, cardGeneral, userCardSpiritBeast);
                string fileNameWithoutExtension = "UI/Background4/Background_V4_352";
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                spiritBeastImage.texture = texture;
            });
        }
    }
    public async Task CreatePopupEquipmentsAsync(object data, GameObject currentObject, string statusToggle = "NOT EQUIP")
    {
        popupSpiritBeastObject = Instantiate(PopupSpiritBeastPanelPrefab, MainPanel);
        Transform transform = popupSpiritBeastObject.transform;
        Transform contentPanel = transform.Find("Scroll View/Viewport/Content");
        Text pageText = transform.Find("Pagination/Page").GetComponent<Text>();
        Toggle toggle = transform.Find("Toggle").GetComponent<Toggle>();
        toggle.isOn = (statusToggle == "ALL");
        toggle.onValueChanged.AddListener(async (bool isOn) =>
        {
            string newStatusToggle = isOn ? "ALL" : "NOT EQUIP";
            Destroy(popupSpiritBeastObject);
            await CreatePopupEquipmentsAsync(data, currentObject, newStatusToggle); // Gọi lại nhưng giữ statusToggle mới
        });
        Button nextButton = transform.Find("Pagination/Next").GetComponent<Button>();
        Button previousButton = transform.Find("Pagination/Previous").GetComponent<Button>();
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupSpiritBeastObject);
        });
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        spiritBeasts = await UserSpiritBeastsService.Create().GetAllUserCardGeneralsSpiritBeastAsync(User.CurrentUserId, PAGE_SIZE, offset, statusToggle);

        int totalRecord = await UserSpiritBeastsService.Create().GetUserSpiritBeastsCountAsync(User.CurrentUserId, search, AppConstants.Rare.ALL);
        totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);

        pageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        CreatePopupEquipmentsUI(data, spiritBeasts, contentPanel, currentObject);
        nextButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            await ChangeNextPageAsync(data, pageText, contentPanel, currentObject);
        });
        previousButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            await ChangePreviousPageAsync(data, pageText, contentPanel, currentObject);
        });
    }
    public int CalculateTotalPages(int totalRecords, int PAGE_SIZE)
    {
        if (PAGE_SIZE <= 0) return 0; // Đảm bảo PAGE_SIZE không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / PAGE_SIZE);
    }
    public void CreatePopupEquipmentsUI(object data, List<SpiritBeasts> spiritBeasts, Transform content, GameObject currentObject)
    {
        foreach (var spiritBeast in spiritBeasts)
        {
            GameObject spiritBeastObject = Instantiate(EquipmentsWearingPrefab, content);
            Transform transform = spiritBeastObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = spiritBeast.Name.Replace("_", " ");

            TextMeshProUGUI powerText = transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
            powerText.text = spiritBeast.Power.ToString();

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spiritBeast.Image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{spiritBeast.Rare}");
            rareImage.texture = rareTexture;

            Button equipButton = transform.Find("EquipButton").GetComponent<Button>();
            equipButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupSpiritBeastObject);
                if (data is CardGenerals cardGeneral)
                {
                    await UserSpiritBeastsService.Create().InsertOrUpdateUserCardGeneralSpiritBeastAsync(User.CurrentUserId, cardGeneral, spiritBeast);

                    RawImage spiritBeastImage = tempCurrentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
                    var userCardSpiritBeast = await UserSpiritBeastsService.Create().GetUserCardGeneralSpiritBeastAsync(User.CurrentUserId, cardGeneral);
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(userCardSpiritBeast.Image);
                    Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                    spiritBeastImage.texture = texture;

                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
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
    public async Task ChangeNextPageAsync(object data, Text PageText, Transform content, GameObject currentObject)
    {
        if (currentPage < totalPage)
        {
            ButtonEvent.Instance.Close(content);
            int totalRecord = 0;

            totalRecord = await UserSpiritBeastsService.Create().GetUserSpiritBeastsCountAsync(User.CurrentUserId, search, AppConstants.Rare.ALL);
            totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
            currentPage = currentPage + 1;
            offset = offset + PAGE_SIZE;
            List<SpiritBeasts> spiritBeasts = await UserSpiritBeastsService.Create().GetAllUserCardGeneralsSpiritBeastAsync(User.CurrentUserId, PAGE_SIZE, offset, statusToggle);
            CreatePopupEquipmentsUI(data, spiritBeasts, content, currentObject);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public async Task ChangePreviousPageAsync(object data, Text PageText, Transform content, GameObject currentObject)
    {
        if (currentPage > 1)
        {
            ButtonEvent.Instance.Close(content);
            int totalRecord = 0;

            totalRecord = await UserSpiritBeastsService.Create().GetUserSpiritBeastsCountAsync(User.CurrentUserId, search, AppConstants.Rare.ALL);
            totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
            currentPage = currentPage - 1;
            offset = offset - PAGE_SIZE;
            List<SpiritBeasts> spiritBeasts = await UserSpiritBeastsService.Create().GetAllUserCardGeneralsSpiritBeastAsync(User.CurrentUserId, PAGE_SIZE, offset, statusToggle);
            CreatePopupEquipmentsUI(data, spiritBeasts, content, currentObject);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void GetRank(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonRankPanels();
    }
}
