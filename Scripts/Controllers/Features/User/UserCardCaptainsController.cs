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

public class UserCardCaptainsController : MonoBehaviour
{
    public static UserCardCaptainsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardCaptainButtonPrefab;
    private GameObject PositionPrefab;
    private GameObject MainMenuDetailPanel2Prefab;
    private GameObject PopupSpiritBeastPanelPrefab;
    private GameObject EquipmentsWearingPrefab;
    private GameObject PopupSpiritBeastObject;
    private GameObject TempCurrentObject;
    private GameObject SkillPanelPrefab;
    private GameObject SkillGroupPrefab;
    private GameObject Skill1Prefab;
    private GameObject Skill2Prefab;
    private GameObject PopupSkillsPanelPrefab;
    private GameObject PopupSkillDetailPrefab;
    private GameObject SkillPanelObject;
    private const int MAX_LEVEL = 10000;
    // private const int PAGE_SIZE = 100;
    // private int Offset;
    // private int CurrentPage;
    // private int TotalPage;
    // private string StatusToggle;
    // private string Search;
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        CardCaptainButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.CARD_CAPTAIN_BUTTON_PREFAB);
        PositionPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.POSITION_PREFAB);
        MainMenuDetailPanel2Prefab = UIManager.Instance.Get(AppConstants.Prefab.General.MAIN_MENU_DETAIL_PANEL_2_PREFAB);
        PopupSpiritBeastPanelPrefab = UIManager.Instance.Get("PopupSpiritBeastPanelPrefab");
        EquipmentsWearingPrefab = UIManager.Instance.Get(AppConstants.Prefab.Equipment.EQUIPMENTS_WEARING_PREFAB);
        SkillPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Skill.SKILL_PANEL_PREFAB);
        SkillGroupPrefab = UIManager.Instance.Get(AppConstants.Prefab.Skill.SKILL_GROUP_PREFAB);
        Skill1Prefab = UIManager.Instance.Get(AppConstants.Prefab.Skill.SKILL_1_PREFAB);
        Skill2Prefab = UIManager.Instance.Get(AppConstants.Prefab.Skill.SKILL_2_PREFAB);
        PopupSkillsPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Skill.POPUP_SKILLS_PANEL_PREFAB);
        PopupSkillDetailPrefab = UIManager.Instance.Get(AppConstants.Prefab.Skill.POPUP_SKILL_DETAIL_PREFAB);
    }
    public void CreateUserCardCaptains(List<CardCaptains> cardCaptains, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        foreach (var cardCaptain in cardCaptains)
        {
            GameObject cardCaptainObject = Instantiate(CardCaptainButtonPrefab, contentPanel);
            Transform transform = cardCaptainObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardCaptain.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardCaptain.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardCaptain.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);

            TextMeshProUGUI typePanel = transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardCaptain.Type.ToString().Replace("_", " ");

            Image rareBackground = transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardCaptain.Rarity));

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardCaptain.Rarity));
            rareText.text = cardCaptain.Rarity;

            Transform teamPanel = transform.Find("Team");
            if (cardCaptain.Team.TeamNumber != 0)
            {
                teamPanel.gameObject.SetActive(true);
                RawImage teamBackgroundImage = transform.Find("Team/Background").GetComponent<RawImage>();
                TextMeshProUGUI teamTitleText = transform.Find("Team/TitleText").GetComponent<TextMeshProUGUI>();
                Texture teamBackgroundTexture = TextureHelper.LoadTextureCached(ImageConstants.Team.TEAM_BACKGROUND_2);
                teamBackgroundImage.texture = teamBackgroundTexture;
                teamTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TEAM) + " " + cardCaptain.Team.TeamNumber.ToString();
            }
            else
            {
                teamPanel.gameObject.SetActive(false);
            }

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ShowCardCaptainDetails(cardCaptain);
            });
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(250, 360);
            gridLayout.spacing = new Vector2(23, 10);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateUserCardCaptainsForSummon(List<CardCaptains> cardCaptains, Transform PositionPanel)
    {
        foreach (var cardCaptain in cardCaptains)
        {
            GameObject cardCaptainObject = Instantiate(PositionPrefab, PositionPanel);
            Transform transform = cardCaptainObject.transform;

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardCaptain.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            // GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
            // if (gridLayout != null)
            // {
            //     gridLayout.cellSize = new Vector2(200, 250);
            // }
        }
    }
    public void ShowCardCaptainDetails(CardCaptains cardCaptain, int buttonType = 1)
    {
        GameObject currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        Transform transform = currentObject.transform;
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Transform setButtonGroupPanel = transform.Find("DictionaryCards/SetButtonGroup/Viewport/Content");
        RawImage cardBackground = transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        RawImage backgroundCircle1Image = transform.Find("DictionaryCards/CircleImage1").GetComponent<RawImage>();
        ButtonLoader.Instance.CreateSetButtonGroup(cardCaptain, setButtonGroupPanel);
        backgroundCircle1Image.gameObject.AddComponent<RotateAnimation>();

        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);
        Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.CARD_CAPTAIN_BACKGROUND_URL);
        cardBackground.texture = texture;
        closeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
            await MainMenuManager.Instance.GetTypeAsync(AppConstants.MainType.CARD_CAPTAIN);
        });
        TempCurrentObject = currentObject;
        CreateDetailsUI(cardCaptain, currentObject);
    }
    public void CreateDetailsUI(CardCaptains cardCaptain, GameObject currentObject)
    {
        RefreshDetailsUI(cardCaptain, currentObject);

        BindButton(cardCaptain, currentObject);
    }
    public void RefreshDetailsUI(CardCaptains cardCaptain, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardCaptain.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI idText = transform.Find("DictionaryCards/Name/IdText").GetComponent<TextMeshProUGUI>();
        idText.text = "ID: " + cardCaptain.Id;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/Name/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardCaptain.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/Power/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardCaptain.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardCaptain.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/DetailsPanel/Group2/Rare/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardCaptain.Rarity}");
        rareImage.texture = rareTexture;

        Transform starGridLayout = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/GridLayout");
        TextureHelper.SetupStars(starGridLayout, cardCaptain.Star);

        RawImage mainClassImage = transform.Find("DictionaryCards/Class/MainClassImage").GetComponent<RawImage>();
        RawImage subClassImage = transform.Find("DictionaryCards/Class/SubClassImage").GetComponent<RawImage>();
        TextMeshProUGUI mainClassText = transform.Find("DictionaryCards/Class/MainClassText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subClassText = transform.Find("DictionaryCards/Class/SubClassText").GetComponent<TextMeshProUGUI>();

        mainClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(cardCaptain.Class.MainImage));
        subClassImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(cardCaptain.Class.SubImage));
        mainClassText.text = cardCaptain.Class.MainType;
        subClassText.text = cardCaptain.Class.SubType;

        SetupStat(transform, "Health", AppConstants.StatFields.HEALTH, AppDisplayConstants.StatFieldsShort.HEALTH, cardCaptain.Health);
        SetupStat(transform, "PhysicalAttack", AppConstants.StatFields.PHYSICAL_ATTACK, AppDisplayConstants.StatFieldsShort.PHYSICAL_ATTACK, cardCaptain.PhysicalAttack);
        SetupStat(transform, "PhysicalDefense", AppConstants.StatFields.PHYSICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.PHYSICAL_DEFENSE, cardCaptain.PhysicalDefense);
        SetupStat(transform, "MagicalAttack", AppConstants.StatFields.MAGICAL_ATTACK, AppDisplayConstants.StatFieldsShort.MAGICAL_ATTACK, cardCaptain.MagicalAttack);
        SetupStat(transform, "MagicalDefense", AppConstants.StatFields.MAGICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MAGICAL_DEFENSE, cardCaptain.MagicalDefense);
        SetupStat(transform, "ChemicalAttack", AppConstants.StatFields.CHEMICAL_ATTACK, AppDisplayConstants.StatFieldsShort.CHEMICAL_ATTACK, cardCaptain.ChemicalAttack);
        SetupStat(transform, "ChemicalDefense", AppConstants.StatFields.CHEMICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.CHEMICAL_DEFENSE, cardCaptain.ChemicalDefense);
        SetupStat(transform, "AtomicAttack", AppConstants.StatFields.ATOMIC_ATTACK, AppDisplayConstants.StatFieldsShort.ATOMIC_ATTACK, cardCaptain.AtomicAttack);
        SetupStat(transform, "AtomicDefense", AppConstants.StatFields.ATOMIC_DEFENSE, AppDisplayConstants.StatFieldsShort.ATOMIC_DEFENSE, cardCaptain.AtomicDefense);
        SetupStat(transform, "MentalAttack", AppConstants.StatFields.MENTAL_ATTACK, AppDisplayConstants.StatFieldsShort.MENTAL_ATTACK, cardCaptain.MentalAttack);
        SetupStat(transform, "MentalDefense", AppConstants.StatFields.MENTAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MENTAL_DEFENSE, cardCaptain.MentalDefense);
        SetupStat(transform, "Speed", AppConstants.StatFields.SPEED, AppDisplayConstants.StatFieldsShort.SPEED, cardCaptain.Speed);
    }
    public void BindButton(CardCaptains cardCaptain, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        Button detailButton = transform.Find("DictionaryCards/DetailsPanel/Group4/Stats/DetailButton").GetComponent<Button>();
        detailButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StatsManager.Instance.CreateStatsManager(cardCaptain);
        });

        Button upgradeLevelButton = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/UpgradeLevelButton").GetComponent<Button>();
        upgradeLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ItemExperienceDTO itemExperienceDTO = await UserItemsService.Create().GetUserItemExperienceByCodeNameAsync(User.CurrentUserId, ItemConstants.Experiment.EXP_CARD_HEROES);
            LevelController.Instance.CreateLevelPanel(cardCaptain, itemExperienceDTO, MAX_LEVEL, level => Math.Max(level, 1) * 500d);
        });

        Button upgradeStarButton = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/UpgradeStarButton").GetComponent<Button>();
        upgradeStarButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StarController.Instance.CreateStarPanel(cardCaptain, MAX_LEVEL, level => Math.Max(level, 1) * 2d);
        });

        Button moduleButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Module").GetComponent<Button>();
        moduleButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ModuleManager.Instance.CreateModule(cardCaptain);
        });

        Button upgradeButton = transform.Find("DictionaryCards/DetailsPanel/Group3/Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            UpgradeManager.Instance.CreateUpgrade(cardCaptain);
        });
    }
    public void RefreshCurrentDetailsUI(CardCaptains cardCaptain)
    {
        if (TempCurrentObject == null)
            return;

        RefreshDetailsUI(
            cardCaptain,
            TempCurrentObject);
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
        if (obj is CardCaptains cardCaptain)
        {
            var skills = await UserSkillsService.Create().GetUserCardCaptainsSkillsAsync(User.CurrentUserId, cardCaptain.Id);
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
                await CreateSkillPanelAsync(cardCaptain.Id);
            });
        }
    }
    public async Task CreateSkillPanelAsync(string cardId)
    {
        SkillPanelObject = Instantiate(SkillPanelPrefab, MainPanel);
        Transform transform = SkillPanelObject.transform;
        Transform skillGroupContent = transform.Find("DictionaryCards/SkillGroup");
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(SkillPanelObject);
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
        var skills = await UserSkillsService.Create().GetUserCardCaptainsSkillsAsync(User.CurrentUserId, cardId);
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

        var skills = await UserSkillsService.Create().GetUserCardCaptainsSkillsAsync(User.CurrentUserId, cardId);
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
                Destroy(SkillPanelObject);
                if (oldSkill != null)
                {
                    await UserSkillsService.Create().DeleteUserCardCaptainSkillsAsync(User.CurrentUserId, cardId, oldSkill.Id, position);
                    await UserSkillsService.Create().InsertUserCardCaptainSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
                }
                else
                {
                    await UserSkillsService.Create().InsertUserCardCaptainSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
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
            Destroy(SkillPanelObject);
            await UserSkillsService.Create().DeleteUserCardCaptainSkillsAsync(User.CurrentUserId, cardId, skill.Id, position);
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
    public int CalculateTotalPages(int totalRecords, int PAGE_SIZE)
    {
        if (PAGE_SIZE <= 0) return 0; // Đảm bảo PAGE_SIZE không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / PAGE_SIZE);
    }
    public void GetRank(object obj, GameObject currentObject)
    {
    }
}
