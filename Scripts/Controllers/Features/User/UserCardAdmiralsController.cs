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

public class UserCardAdmiralsController : MonoBehaviour
{
    public static UserCardAdmiralsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardAdmiralButtonPrefab;
    private GameObject PositionPrefab;
    private GameObject MainMenuDetailPanel2Prefab;
    private TeamsService teamsService;
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
    private const int MAX_LEVEL = 10000;
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
        CardAdmiralButtonPrefab = UIManager.Instance.Get("CardAdmiralButtonPrefab");
        PositionPrefab = UIManager.Instance.Get("PositionPrefab");
        MainMenuDetailPanel2Prefab = UIManager.Instance.Get("MainMenuDetailPanel2Prefab");
        PopupSpiritBeastPanelPrefab = UIManager.Instance.Get("PopupSpiritBeastPanelPrefab");
        EquipmentsWearingPrefab = UIManager.Instance.Get("EquipmentsWearingPrefab");
        SkillPanelPrefab = UIManager.Instance.Get("SkillPanelPrefab");
        SkillGroupPrefab = UIManager.Instance.Get("SkillGroupPrefab");
        Skill1Prefab = UIManager.Instance.Get("Skill1Prefab");
        Skill2Prefab = UIManager.Instance.Get("Skill2Prefab");
        PopupSkillsPanelPrefab = UIManager.Instance.Get("PopupSkillsPanelPrefab");
        PopupSkillDetailPrefab = UIManager.Instance.Get("PopupSkillDetailPrefab");
        teamsService = TeamsService.Create();
        search ="";
    }
    public void CreateUserCardAdmirals(List<CardAdmirals> cardAdmirals, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        foreach (var cardAdmiral in cardAdmirals)
        {
            GameObject cardAdmiralObject = Instantiate(CardAdmiralButtonPrefab, contentPanel);
            Transform transform = cardAdmiralObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardAdmiral.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardAdmiral.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardAdmiral.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);

            TextMeshProUGUI typePanel = transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardAdmiral.Type.ToString().Replace("_", " ");

            Image rareBackground = transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardAdmiral.Rare));

            Transform teamPanel = transform.Find("Team");
            if(cardAdmiral.Team.TeamNumber != 0)
            {
                teamPanel.gameObject.SetActive(true);
                RawImage teamBackgroundImage = transform.Find("Team/Background").GetComponent<RawImage>();
                TextMeshProUGUI teamTitleText = transform.Find("Team/TitleText").GetComponent<TextMeshProUGUI>();
                Texture teamBackgroundTexture = TextureHelper.LoadTextureCached(ImageConstants.Team.TEAM_BACKGROUND_6);
                teamBackgroundImage.texture = teamBackgroundTexture;
                teamTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TEAM) + " " + cardAdmiral.Team.TeamNumber.ToString();
            }
            else
            {
                teamPanel.gameObject.SetActive(false);
            }

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ShowCardAdmiralDetails(cardAdmiral);
            });

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardAdmiral.Rare));
            rareText.text = cardAdmiral.Rare;
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(250, 360);
            gridLayout.spacing = new Vector2(23, 10);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateUserCardAdmiralsForSummon(List<CardAdmirals> cardAdmirals, Transform PositionPanel)
    {
        foreach (var cardAdmiral in cardAdmirals)
        {
            GameObject cardAdmiralObject = Instantiate(PositionPrefab, PositionPanel);
            Transform transform = cardAdmiralObject.transform;

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardAdmiral.Image);
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
    public void ShowCardAdmiralDetails(CardAdmirals cardAdmiral, int buttonType = 1)
    {
        GameObject currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        Transform transform = currentObject.transform;
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Transform setButtonGroupPanel = transform.Find("DictionaryCards/SetButtonGroup/Viewport/Content");
        RawImage cardBackground = transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        RawImage backgroundCircle1Image = transform.Find("DictionaryCards/CircleImage1").GetComponent<RawImage>();
        ButtonLoader.Instance.CreateSetButtonGroup(cardAdmiral, setButtonGroupPanel);
        backgroundCircle1Image.gameObject.AddComponent<RotateAnimation>();

        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);
        Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.CARD_ADMIRAL_BACKGROUND_URL);
        cardBackground.texture = texture;
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
            MainMenuManager.Instance.GetType(AppConstants.MainType.CARD_ADMIRAL);
        });
        tempCurrentObject = currentObject;
        CreateDetailsUI(cardAdmiral, currentObject);
    }
    public void CreateDetailsUI(CardAdmirals cardAdmiral, GameObject currentObject)
    {
        RefreshDetailsUI(cardAdmiral, currentObject);

        BindButton(cardAdmiral, currentObject);
    }
    public void RefreshDetailsUI(CardAdmirals cardAdmiral, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardAdmiral.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI idText = transform.Find("DictionaryCards/Name/IdText").GetComponent<TextMeshProUGUI>();
        idText.text = "ID: " + cardAdmiral.Id;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/Name/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardAdmiral.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/Power/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardAdmiral.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardAdmiral.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/DetailsPanel/Group2/Rare/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardAdmiral.Rare}");
        rareImage.texture = rareTexture;

        Transform starGridLayout = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/GridLayout");
        TextureHelper.SetupStars(starGridLayout, cardAdmiral.Star);

        RawImage mainClassImage = transform.Find("DictionaryCards/Class/MainClassImage").GetComponent<RawImage>();
        RawImage subClassImage = transform.Find("DictionaryCards/Class/SubClassImage").GetComponent<RawImage>();
        TextMeshProUGUI mainClassText = transform.Find("DictionaryCards/Class/MainClassText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subClassText = transform.Find("DictionaryCards/Class/SubClassText").GetComponent<TextMeshProUGUI>();

        mainClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(cardAdmiral.Class.MainImage));
        subClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(cardAdmiral.Class.SubImage));
        mainClassText.text = cardAdmiral.Class.MainType;
        subClassText.text = cardAdmiral.Class.SubType;

        SetupStat(transform, "Health", AppConstants.StatFields.HEALTH, AppDisplayConstants.StatFieldsShort.HEALTH, cardAdmiral.Health);
        SetupStat(transform, "PhysicalAttack", AppConstants.StatFields.PHYSICAL_ATTACK, AppDisplayConstants.StatFieldsShort.PHYSICAL_ATTACK, cardAdmiral.PhysicalAttack);
        SetupStat(transform, "PhysicalDefense", AppConstants.StatFields.PHYSICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.PHYSICAL_DEFENSE, cardAdmiral.PhysicalDefense);
        SetupStat(transform, "MagicalAttack", AppConstants.StatFields.MAGICAL_ATTACK, AppDisplayConstants.StatFieldsShort.MAGICAL_ATTACK, cardAdmiral.MagicalAttack);
        SetupStat(transform, "MagicalDefense", AppConstants.StatFields.MAGICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MAGICAL_DEFENSE, cardAdmiral.MagicalDefense);
        SetupStat(transform, "ChemicalAttack", AppConstants.StatFields.CHEMICAL_ATTACK, AppDisplayConstants.StatFieldsShort.CHEMICAL_ATTACK, cardAdmiral.ChemicalAttack);
        SetupStat(transform, "ChemicalDefense", AppConstants.StatFields.CHEMICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.CHEMICAL_DEFENSE, cardAdmiral.ChemicalDefense);
        SetupStat(transform, "AtomicAttack", AppConstants.StatFields.ATOMIC_ATTACK, AppDisplayConstants.StatFieldsShort.ATOMIC_ATTACK, cardAdmiral.AtomicAttack);
        SetupStat(transform, "AtomicDefense", AppConstants.StatFields.ATOMIC_DEFENSE, AppDisplayConstants.StatFieldsShort.ATOMIC_DEFENSE, cardAdmiral.AtomicDefense);
        SetupStat(transform, "MentalAttack", AppConstants.StatFields.MENTAL_ATTACK, AppDisplayConstants.StatFieldsShort.MENTAL_ATTACK, cardAdmiral.MentalAttack);
        SetupStat(transform, "MentalDefense", AppConstants.StatFields.MENTAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MENTAL_DEFENSE, cardAdmiral.MentalDefense);
        SetupStat(transform, "Speed", AppConstants.StatFields.SPEED, AppDisplayConstants.StatFieldsShort.SPEED, cardAdmiral.Speed);
    }
    public void BindButton(CardAdmirals cardAdmiral, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        Button detailButton = transform.Find("DictionaryCards/DetailsPanel/Group4/Stats/DetailButton").GetComponent<Button>();
        detailButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StatsManager.Instance.CreateStatsManager(cardAdmiral);
        });

        Button upgradeLevelButton = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/UpgradeLevelButton").GetComponent<Button>();
        upgradeLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ItemExperienceDTO itemExperienceDTO = await UserItemsService.Create().GetUserItemExperienceByCodeNameAsync(ItemConstants.Experiment.EXP_CARD_HEROES);
            LevelController.Instance.CreateLevelPanel(cardAdmiral, itemExperienceDTO, MAX_LEVEL, level => Math.Max(level, 1) * 500d);
        });

        Button upgradeStarButton = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/UpgradeStarButton").GetComponent<Button>();
        upgradeStarButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StarController.Instance.CreateStarPanel(cardAdmiral, MAX_LEVEL, level => Math.Max(level, 1) * 2d);
        });

        Button moduleButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Module").GetComponent<Button>();
        moduleButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ModuleManager.Instance.CreateModule(cardAdmiral);
        });

        Button upgradeButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            UpgradeManager.Instance.CreateUpgrade(cardAdmiral);
        });
    }
    public void RefreshCurrentDetailsUI(CardAdmirals cardAdmiral)
    {
        if (tempCurrentObject == null)
            return;

        RefreshDetailsUI(
            cardAdmiral,
            tempCurrentObject);
    }
    private void SetupStat(Transform root, string statObjectName, string statField, string statDisplayName, double value, bool isPercent = false)
    {
        Transform statTransform = root.Find($"DictionaryCards/DetailsPanel/Group4/Stats/GridLayout/{statObjectName}");

        RawImage iconImage = statTransform.Find("IconImage").GetComponent<RawImage>();
        TextMeshProUGUI titleText = statTransform.Find("StatTitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI valueText = statTransform.Find("StatText").GetComponent<TextMeshProUGUI>();

        TextureHelper.CreatePropertyRuneUI(statField, iconImage);

        titleText.text = LocalizationManager.Get(statDisplayName);
        titleText.enableWordWrapping = false;

        if (isPercent)
        {
            valueText.text = NumberFormatterHelper.FormatNumberExtended(value, true) + " %";
        }
        else
        {
            valueText.text = NumberFormatterHelper.FormatNumberExtended(value, true);
        }
    }
    public async Task GetSkillsAsync(object obj, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        Transform skillContent = transform.Find("DictionaryCards/Content/SkillsPanel/Scroll View/Viewport/Content");
        Button setUpButton = transform.Find("DictionaryCards/Content/SkillsPanel/SetUpButton").GetComponent<Button>();
        if (obj is CardAdmirals cardAdmiral)
        {
            var skills = await UserSkillsService.Create().GetUserCardAdmiralsSkillsAsync(User.CurrentUserId, cardAdmiral.Id);
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
                await CreateSkillPanelAsync(cardAdmiral.Id);
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
        var skills = await UserSkillsService.Create().GetUserCardAdmiralsSkillsAsync(User.CurrentUserId, cardId);
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

        var skills = await UserSkillsService.Create().GetUserCardAdmiralsSkillsAsync(User.CurrentUserId, cardId);
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
            equipButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(skillPopupObject);
                Destroy(skillPanelObject);
                if (oldSkill != null)
                {
                    await UserSkillsService.Create().DeleteUserCardAdmiralSkillsAsync(User.CurrentUserId, cardId, oldSkill.Id, position);
                    await UserSkillsService.Create().InsertUserCardAdmiralSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
                }
                else
                {
                    await UserSkillsService.Create().InsertUserCardAdmiralSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
                }
                await CreateSkillPanelAsync(cardId);
            });
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
            await UserSkillsService.Create().DeleteUserCardAdmiralSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
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
    public async Task GetSpiritBeastAsync(object obj, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        RawImage background1Image = transform.Find("DictionaryCards/Content/SpiritBeastPanel/Background1").GetComponent<RawImage>();
        Button addButton = transform.Find("DictionaryCards/Content/SpiritBeastPanel/AddButton").GetComponent<Button>();
        Button removeButton = transform.Find("DictionaryCards/Content/SpiritBeastPanel/RemoveButton").GetComponent<Button>();

        offset = 0;
        currentPage = 1;

        background1Image.gameObject.AddComponent<RotateAnimation>();

        if (obj is CardAdmirals cardAdmiral)
        {
            var userCardSpiritBeast = await UserSpiritBeastsService.Create().GetUserCardAdmiralSpiritBeastAsync(User.CurrentUserId, cardAdmiral);
            RawImage spiritBeastImage = transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = userCardSpiritBeast.Image != null
                ? ImageHelper.RemoveImageExtension(userCardSpiritBeast.Image)
                : "UI/Background4/Background_V4_352";
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            spiritBeastImage.texture = texture;

            CreateDetailsUI(cardAdmiral, currentObject);
            addButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreatePopupEquipmentsAsync(obj, currentObject);
            });
            removeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await UserSpiritBeastsService.Create().DeleteUserCardAdmiralSpiritBeastAsync(User.CurrentUserId, cardAdmiral, userCardSpiritBeast);
                string fileNameWithoutExtension = "UI/Background4/Background_V4_352";
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                spiritBeastImage.texture = texture;

                var card = await UserCardAdmiralsService.Create().GetUserCardAdmiralByIdAsync(User.CurrentUserId, cardAdmiral.Id);
                ShowCardAdmiralDetails(card, 5);
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
        spiritBeasts = await UserSpiritBeastsService.Create().GetAllUserCardAdmiralsSpiritBeastAsync(User.CurrentUserId, PAGE_SIZE, offset, statusToggle);

        int totalRecord = await UserSpiritBeastsService.Create().GetUserSpiritBeastsCountAsync(User.CurrentUserId, search, AppConstants.Rare.ALL);
        totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);

        pageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        CreatePopupEquipmentsUI(data, spiritBeasts, contentPanel, currentObject);
        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();
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
                if (data is CardAdmirals cardAdmiral)
                {
                    await UserSpiritBeastsService.Create().InsertOrUpdateUserCardAdmiralSpiritBeastAsync(User.CurrentUserId, cardAdmiral, spiritBeast);

                    RawImage spiritBeastImage = tempCurrentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
                    var userCardSpiritBeast = await UserSpiritBeastsService.Create().GetUserCardAdmiralSpiritBeastAsync(User.CurrentUserId, cardAdmiral);
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(userCardSpiritBeast.Image);
                    Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                    spiritBeastImage.texture = texture;

                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    var card = await UserCardAdmiralsService.Create().GetUserCardAdmiralByIdAsync(User.CurrentUserId, cardAdmiral.Id);
                    ShowCardAdmiralDetails(card, 5);
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
            List<SpiritBeasts> spiritBeasts = await UserSpiritBeastsService.Create().GetAllUserCardAdmiralsSpiritBeastAsync(User.CurrentUserId, PAGE_SIZE, offset, statusToggle);
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
            List<SpiritBeasts> spiritBeasts = await UserSpiritBeastsService.Create().GetAllUserCardAdmiralsSpiritBeastAsync(User.CurrentUserId, PAGE_SIZE, offset, statusToggle);
            CreatePopupEquipmentsUI(data, spiritBeasts, content, currentObject);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void GetRank(object obj, GameObject currentObject)
    {
    }
}
