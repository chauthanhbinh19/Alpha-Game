using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Threading.Tasks;
using System.Threading;

public class TeamsManager : MonoBehaviour
{
    public static TeamsManager Instance { get; private set; }
    private Transform MainPanel;
    private Transform PositionPanel;
    private GameObject CardThirdPrefab;
    private GameObject PopupTeamFirstPrefab;
    private GameObject PopupTeamSecondPrefab;
    private GameObject TeamsPanelPrefab;
    private GameObject TeamsPositionPrefab;
    private GameObject TeamTypePrefab;
    private GameObject TeamSlotSecondPrefab;
    private GameObject TeamSlotFirstPrefab;
    private GameObject PopupCardPanelPrefab;
    private GameObject CardSelectButtonPrefab;
    private GameObject PopupWarningPanelPrefab;
    private GameObject PopupTeamEmblemPanelPrefab;
    private GameObject EmblemButtonPrefab;
    private GameObject RareButtonPrefab;
    private GameObject PositionPrefab;
    private GameObject PopupTeamFirstObject;
    private GameObject PopupTeamSecondObject;
    private GameObject PopupCardPanelObject;
    private Button CloseButton;
    private Button HomeButton;
    private TextMeshProUGUI PowerText;
    // private int offset;
    private string SelectedOptionName;
    private string TeamId;
    private int TeamNumber;
    private int TeamPositionIndex;
    private int TeamSlotIndex;
    private string Position;
    private int TeamLimit;
    private int TeamOffset;
    private Text TitleText;
    private string MainType;
    // private string subType;
    private int MaxMembersInTeamPosition = 10;
    private Transform ChoseTeam;
    List<CardDragHandler> CardDragHandlers = new List<CardDragHandler>();
    UserCardHeroesService UserCardHeroesService;
    UserCardCaptainsService UserCardCaptainsService;
    UserCardColonelsService UserCardColonelsService;
    UserCardGeneralsService UserCardGeneralsService;
    UserCardAdmiralsService UserCardAdmiralsService;
    UserCardMonstersService UserCardMonstersService;
    UserCardMilitariesService UserCardMilitariesService;
    UserCardSpellsService UserCardSpellsService;
    TeamsService TeamsService;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private string Search = "";
    private string Type = AppConstants.Type.ALL;
    private string Rare = AppConstants.Rare.ALL;
    private int Offset = 0;
    private int CurrentPage = 1;
    private int TotalPage;
    private const int PAGE_SIZE = 100;
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
        CardThirdPrefab = UIManager.Instance.Get("CardThirdPrefab");
        PopupTeamFirstPrefab = UIManager.Instance.Get("PopupTeamFirstPanelPrefab");
        PopupTeamSecondPrefab = UIManager.Instance.Get("PopupTeamSecondPanelPrefab");
        TeamsPanelPrefab = UIManager.Instance.Get("TeamsPanelPrefab");
        TeamsPositionPrefab = UIManager.Instance.Get("TeamsPositionPrefab");
        TeamTypePrefab = UIManager.Instance.Get("TeamTypePrefab");
        TeamSlotFirstPrefab = UIManager.Instance.Get("TeamSlotFirstPrefab");
        TeamSlotSecondPrefab = UIManager.Instance.Get("TeamSlotSecondPrefab");
        PopupCardPanelPrefab = UIManager.Instance.Get("PopupCardPanelPrefab");
        CardSelectButtonPrefab = UIManager.Instance.Get("CardSelectButtonPrefab");
        PopupWarningPanelPrefab = UIManager.Instance.Get("PopupWarningPanelPrefab");
        PopupTeamEmblemPanelPrefab = UIManager.Instance.Get("PopupTeamEmblemPanelPrefab");
        EmblemButtonPrefab = UIManager.Instance.Get("EmblemButtonPrefab");
        RareButtonPrefab = UIManager.Instance.Get("RareButtonPrefab");
        PositionPrefab = UIManager.Instance.Get("PositionPrefab");

        UserCardHeroesService = UserCardHeroesService.Create();
        UserCardCaptainsService = UserCardCaptainsService.Create();
        UserCardColonelsService = UserCardColonelsService.Create();
        UserCardGeneralsService = UserCardGeneralsService.Create();
        UserCardAdmiralsService = UserCardAdmiralsService.Create();
        UserCardMonstersService = UserCardMonstersService.Create();
        UserCardMilitariesService = UserCardMilitariesService.Create();
        UserCardSpellsService = UserCardSpellsService.Create();
        TeamsService = TeamsService.Create();
    }
    public async Task CreateTeamsAsync()
    {
        GameObject teamObject = Instantiate(TeamsPanelPrefab, MainPanel);
        Transform transform = teamObject.transform;
        Transform tempLeftContent = transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = transform.Find("ScrollViewRight/Viewport/Content");
        Transform positionTeamsPanel = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TextMeshProUGUI teamsTitleText = transform.Find("DictionaryCards/TeamsTitleText").GetComponent<TextMeshProUGUI>();
        CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            foreach (Transform child in MainPanel)
            {
                Destroy(child.gameObject);
            }
        });
        // HomeButton = teamsObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        // HomeButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Close(MainPanel);
        // });

        teamsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TEAM);
        await CreateTeamsPositionAsync(positionTeamsPanel);
    }
    public async Task CreateTeamsPositionAsync(Transform positionTeamsPanel)
    {
        ButtonEvent.Instance.Close(positionTeamsPanel);

        // 1. Lấy danh sách teams
        var userTeams = await TeamsService.Create().GetUserTeamsAsync(User.CurrentUserId);
        if (userTeams == null || userTeams.Count == 0) return;

        // --- PHẦN CHỈNH SỬA: Kiểm soát số lượng request đồng thời ---
        // Giới hạn tối đa 5 teams được xử lý cùng lúc (tổng cộng khoảng 40 request song song)
        // Con số này an toàn cho hầu hết các server Unity/Mobile.
        using (var semaphore = new SemaphoreSlim(5))
        {
            var dataTasks = userTeams.Select(async team =>
            {
                await semaphore.WaitAsync();
                try
                {
                    // Tạo các task đếm card
                    var tasks = new List<Task<int>>
                    {
                    UserCardHeroesService.GetUserCardHeroesTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    UserCardCaptainsService.GetUserCardCaptainsTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    UserCardColonelsService.GetUserCardColonelsTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    UserCardGeneralsService.GetUserCardGeneralsTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    UserCardAdmiralsService.GetUserCardAdmiralsTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    UserCardMonstersService.GetUserCardMonstersTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    UserCardMilitariesService.GetUserCardMilitariesTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    UserCardSpellsService.GetUserCardSpellsTeamsCountAsync(User.CurrentUserId, team.TeamId)
                    };

                    // Đợi 8 task của riêng team này hoàn thành
                    await Task.WhenAll(tasks);

                    return new
                    {
                        Info = team,
                        Tasks = tasks // Giữ nguyên List<Task<int>> để logic UI bên dưới không bị lỗi .Result
                    };
                }
                finally
                {
                    semaphore.Release();
                }
            });

            // Chạy tất cả các nhóm data
            var teamDataResults = await Task.WhenAll(dataTasks);
            var teamDataList = teamDataResults.ToList();

            // --- HẾT PHẦN CHỈNH SỬA ---

            // 3. Khởi tạo UI (Giữ nguyên logic bên dưới của bạn)
            string[] titles = {
            AppDisplayConstants.MainType.CARD_HEROES, AppDisplayConstants.MainType.CARD_CAPTAINS,
            AppDisplayConstants.MainType.CARD_COLONELS, AppDisplayConstants.MainType.CARD_GENERALS,
            AppDisplayConstants.MainType.CARD_ADMIRALS, AppDisplayConstants.MainType.CARD_MONSTERS,
            AppDisplayConstants.MainType.CARD_MILITARIES, AppDisplayConstants.MainType.CARD_SPELLS
        };

            string[] backgrounds = {
            "UI/Background4/Item_Background_9", "UI/Background4/Item_Background_10",
            "UI/Background4/Item_Background_9", "UI/Background4/Item_Background_10",
            "UI/Background4/Item_Background_9", "UI/Background4/Item_Background_10",
            "UI/Background4/Item_Background_9", "UI/Background4/Item_Background_10"
        };

            foreach (var teamData in teamDataList)
            {
                var team = teamData.Info;
                GameObject teamPositionObject = Instantiate(TeamsPositionPrefab, positionTeamsPanel);
                Transform transform = teamPositionObject.transform;

                transform.Find("TeamNumberText").GetComponent<TextMeshProUGUI>().text = team.TeamNumber.ToString();
                transform.Find("AvatarImage").GetComponent<RawImage>().texture = TextureHelper.LoadTextureCached(team.TeamAvatar);
                transform.Find("BorderImage").GetComponent<RawImage>().texture = TextureHelper.LoadTextureCached(team.TeamBorder);

                Transform cardContent = transform.Find("Content");

                for (int i = 0; i < 8; i++)
                {
                    // .Result ở đây sẽ an toàn vì chúng ta đã await Task.WhenAll(tasks) ở trên rồi
                    int count = teamData.Tasks[i].Result;
                    CreateCardTypeItem(cardContent, titles[i], count, backgrounds[i]);
                }

                string tempTeamId = team.TeamId;
                int tempTeamPositionIndex = team.TeamNumber;
                transform.Find("ChangeCardButton").GetComponent<Button>().onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    this.TeamId = tempTeamId;
                    this.TeamNumber = tempTeamPositionIndex;
                    CreatePopupTeamFirstPanel();
                });
            }
        }
    }
    // Hàm phụ giúp tạo các item con bên trong Team Content
    private void CreateCardTypeItem(Transform parent, string titleKey, int quantity, string bgPath)
    {
        GameObject teamTypeObject = Instantiate(TeamTypePrefab, parent);
        Transform transform = teamTypeObject.transform;

        transform.Find("QuantityText").GetComponent<TextMeshProUGUI>().text = quantity.ToString();
        transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = LocalizationManager.Get(titleKey);
        transform.Find("Background1").GetComponent<RawImage>().texture = TextureHelper.LoadTextureCached(bgPath);
    }
    public void CreatePopupTeamFirstPanel()
    {
        PopupTeamFirstObject = Instantiate(PopupTeamFirstPrefab, MainPanel);
        Transform transform = PopupTeamFirstObject.transform;
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
        // ScrollRect scrollRect = teamObject.transform.Find("DictionaryCards/ScrollViewPosition").GetComponent<ScrollRect>();
        Transform teamSlotPanel = transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        Transform tempLeftContent = transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = transform.Find("ScrollViewRight/Viewport/Content");
        CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(PopupTeamFirstObject);
            await CreateTeamsAsync();
        });
        HomeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        TextMeshProUGUI numberText = transform.Find("DictionaryCards/TeamPanel/NumberText").GetComponent<TextMeshProUGUI>();
        numberText.text = TeamNumber.ToString();

        _ = CreatePositionAsync(teamSlotPanel);
    }
    public async Task CreatePositionAsync(Transform slotPanel)
    {
        ButtonEvent.Instance.Close(slotPanel);

        // var taskCardHero = userCardHeroesService.GetUserCardHeroesTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        // var taskCardCaptain = userCardCaptainsService.GetUserCardCaptainsTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        // var taskCardColonel = userCardColonelsService.GetUserCardColonelsTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        // var taskCardGeneral = userCardGeneralsService.GetUserCardGeneralsTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        // var taskCardAdmiral = userCardAdmiralsService.GetUserCardAdmiralsTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        // var taskCardMonster = userCardMonstersService.GetUserCardMonstersTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        // var taskCardMilitary = userCardMilitariesService.GetUserCardMilitariesTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        // var taskCardSpell = userCardSpellsService.GetUserCardSpellsTeamWithoutPositionAsync(User.CurrentUserId, teamId);

        // await Task.WhenAll(taskCardHero, taskCardCaptain, taskCardColonel, taskCardGeneral, taskCardAdmiral, taskCardMonster, taskCardMilitary, taskCardSpell);

        // List<CardHeroes> cardHeroList = await taskCardHero;
        // List<CardCaptains> cardCaptainList = await taskCardCaptain;
        // List<CardColonels> cardColonelList = await taskCardColonel;
        // List<CardGenerals> cardGeneralList = await taskCardGeneral;
        // List<CardAdmirals> cardAdmiralList = await taskCardAdmiral;
        // List<CardMonsters> cardMonsterList = await taskCardMonster;
        // List<CardMilitaries> cardMilitaryList = await taskCardMilitary;
        // List<CardSpells> cardSpellList = await taskCardSpell;

        List<CardHeroes> cardHeroList = await UserCardHeroesService.GetUserCardHeroesTeamWithoutPositionAsync(User.CurrentUserId, TeamId);
        List<CardCaptains> cardCaptainList = await UserCardCaptainsService.GetUserCardCaptainsTeamWithoutPositionAsync(User.CurrentUserId, TeamId);
        List<CardColonels> cardColonelList = await UserCardColonelsService.GetUserCardColonelsTeamWithoutPositionAsync(User.CurrentUserId, TeamId);
        List<CardGenerals> cardGeneralList = await UserCardGeneralsService.GetUserCardGeneralsTeamWithoutPositionAsync(User.CurrentUserId, TeamId);
        List<CardAdmirals> cardAdmiralList = await UserCardAdmiralsService.GetUserCardAdmiralsTeamWithoutPositionAsync(User.CurrentUserId, TeamId);
        List<CardMonsters> cardMonsterList = await UserCardMonstersService.GetUserCardMonstersTeamWithoutPositionAsync(User.CurrentUserId, TeamId);
        List<CardMilitaries> cardMilitaryList = await UserCardMilitariesService.GetUserCardMilitariesTeamWithoutPositionAsync(User.CurrentUserId, TeamId);
        List<CardSpells> cardSpellList = await UserCardSpellsService.GetUserCardSpellsTeamWithoutPositionAsync(User.CurrentUserId, TeamId);

        for (int i = 1; i <= 10; i++)
        {
            int tempPositionIndex = i;
            GameObject teamSlotFirstObject = Instantiate(TeamSlotFirstPrefab, slotPanel);
            Transform transform = teamSlotFirstObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            RawImage circleImage = transform.Find("CircleImage").GetComponent<RawImage>();
            circleImage.gameObject.AddComponent<RotateAnimation>();
            Button emblemButton = transform.Find("EmblemButton").GetComponent<Button>();

            var emblemData = BuildEmblemPopupData(
                cardHeroList,
                cardCaptainList,
                cardColonelList,
                cardGeneralList,
                cardAdmiralList,
                cardMonsterList,
                cardMilitaryList,
                cardSpellList
            );

            emblemButton.onClick.AddListener(() =>
            {
                TeamPositionIndex = tempPositionIndex;
                CreateEmblemPanel(emblemData);
            });

            Button cardHeroButton = transform.Find("ButtonGroup/CardHeroButton").GetComponent<Button>();
            Button cardCaptainButton = transform.Find("ButtonGroup/CardCaptainButton").GetComponent<Button>();
            Button cardColonelButton = transform.Find("ButtonGroup/CardColonelButton").GetComponent<Button>();
            Button cardGeneralButton = transform.Find("ButtonGroup/CardGeneralButton").GetComponent<Button>();
            Button cardAdmiralButton = transform.Find("ButtonGroup/CardAdmiralButton").GetComponent<Button>();
            Button cardMonsterButton = transform.Find("ButtonGroup/CardMonsterButton").GetComponent<Button>();
            Button cardMilitaryButton = transform.Find("ButtonGroup/CardMilitaryButton").GetComponent<Button>();
            Button cardSpellButton = transform.Find("ButtonGroup/CardSpellButton").GetComponent<Button>();

            cardHeroButton.onClick.AddListener(async () => await OnCardClickAsync(tempPositionIndex, AppConstants.MainType.CARD_HERO));
            cardCaptainButton.onClick.AddListener(async () => await OnCardClickAsync(tempPositionIndex, AppConstants.MainType.CARD_CAPTAIN));
            cardColonelButton.onClick.AddListener(async () => await OnCardClickAsync(tempPositionIndex, AppConstants.MainType.CARD_COLONEL));
            cardGeneralButton.onClick.AddListener(async () => await OnCardClickAsync(tempPositionIndex, AppConstants.MainType.CARD_GENERAL));
            cardAdmiralButton.onClick.AddListener(async () => await OnCardClickAsync(tempPositionIndex, AppConstants.MainType.CARD_ADMIRAL));
            cardMonsterButton.onClick.AddListener(async () => await OnCardClickAsync(tempPositionIndex, AppConstants.MainType.CARD_MONSTER));
            cardMilitaryButton.onClick.AddListener(async () => await OnCardClickAsync(tempPositionIndex, AppConstants.MainType.CARD_MILITARY));
            cardSpellButton.onClick.AddListener(async () => await OnCardClickAsync(tempPositionIndex, AppConstants.MainType.CARD_SPELL));

            titleText.text = "Slot " + i.ToString();
            quantityText.text = i.ToString();

            // Tối ưu bằng cách lấy Group Transform một lần
            Transform process = transform.Find("Process");
            string slotPrefix = $"{i}-";

            // 2. Cập nhật thủ công từng thanh để tránh dùng 'dynamic' gây lỗi CS0656
            UpdateBarManual(process.Find("CardHeroBar"), cardHeroList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.Card.CARD_HERO_COLOR);
            UpdateBarManual(process.Find("CardCaptainBar"), cardCaptainList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.Card.CARD_CAPTAIN_COLOR);
            UpdateBarManual(process.Find("CardColonelBar"), cardColonelList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.Card.CARD_COLONEL_COLOR);
            UpdateBarManual(process.Find("CardGeneralBar"), cardGeneralList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.Card.CARD_GENERAL_COLOR);
            UpdateBarManual(process.Find("CardAdmiralBar"), cardAdmiralList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.Card.CARD_ADMIRAL_COLOR);
            UpdateBarManual(process.Find("CardMonsterBar"), cardMonsterList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.Card.CARD_MONSTER_COLOR);
            UpdateBarManual(process.Find("CardMilitaryBar"), cardMilitaryList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.Card.CARD_MILITARY_COLOR);
            UpdateBarManual(process.Find("CardSpellBar"), cardSpellList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.Card.CARD_SPELL_COLOR);
        }
    }
    // Hàm hỗ trợ để không phải viết lặp đi lặp lại GetComponent
    // Hàm này chỉ nhận giá trị đã tính toán xong, không cần biết kiểu dữ liệu Card là gì
    private void UpdateBarManual(Transform barTransform, int count, string colorHex)
    {
        Slider slider = barTransform.Find("Slider").GetComponent<Slider>();

        slider.maxValue = 10;
        slider.value = count;

        Image fillImage = slider.fillRect.GetComponent<Image>();

        if (count == 0)
        {
            fillImage.gameObject.SetActive(false); // ẩn fill
        }
        else
        {
            fillImage.gameObject.SetActive(true);  // hiện lại khi > 0
            fillImage.color = ColorHelper.HexToColor(colorHex);
        }

        barTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>().text = count.ToString();
    }
    public async Task OnCardClickAsync(int position, string type)
    {
        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        MainType = type;
        TeamPositionIndex = position;
        await CreatePopupTeamSecondPanelAsync();
    }
    private List<EmblemDTO> BuildEmblemPopupData(
    List<CardHeroes> cardHeroes,
    List<CardCaptains> cardCaptains,
    List<CardColonels> cardColonels,
    List<CardGenerals> cardGenerals,
    List<CardAdmirals> cardAdmirals,
    List<CardMonsters> cardMonsters,
    List<CardMilitaries> cardMilitaries,
    List<CardSpells> cardSpells)
    {
        var result = new List<EmblemDTO>();

        void AddEmblems(IEnumerable<Emblems> emblems, string cardType)
        {
            if (emblems == null) return;

            var grouped = emblems.GroupBy(e => e.Id);

            foreach (var g in grouped)
            {
                var first = g.First();

                result.Add(new EmblemDTO
                {
                    CardType = cardType,
                    EmblemId = g.Key,
                    EmblemType = first.Type,
                    Name = first.Name,
                    Image = first.Image,
                    Count = g.Count()
                });
            }
        }

        AddEmblems(cardHeroes?.SelectMany(c => c.Emblems ?? new List<Emblems>()), AppConstants.MainType.CARD_HERO);
        AddEmblems(cardCaptains?.SelectMany(c => c.Emblems ?? new List<Emblems>()), AppConstants.MainType.CARD_CAPTAIN);
        AddEmblems(cardColonels?.SelectMany(c => c.Emblems ?? new List<Emblems>()), AppConstants.MainType.CARD_COLONEL);
        AddEmblems(cardGenerals?.SelectMany(c => c.Emblems ?? new List<Emblems>()), AppConstants.MainType.CARD_GENERAL);
        AddEmblems(cardAdmirals?.SelectMany(c => c.Emblems ?? new List<Emblems>()), AppConstants.MainType.CARD_ADMIRAL);
        AddEmblems(cardMonsters?.SelectMany(c => c.Emblems ?? new List<Emblems>()), AppConstants.MainType.CARD_MONSTER);
        AddEmblems(cardMilitaries?.SelectMany(c => c.Emblems ?? new List<Emblems>()), AppConstants.MainType.CARD_MILITARY);
        AddEmblems(cardSpells?.SelectMany(c => c.Emblems ?? new List<Emblems>()), AppConstants.MainType.CARD_SPELL);

        return result;
    }
    public void CreateEmblemPanel(List<EmblemDTO> data)
    {
        GameObject popupTeamEmblemPanelObject = Instantiate(PopupTeamEmblemPanelPrefab, MainPanel);
        Transform transform = popupTeamEmblemPanelObject.transform;

        Transform contentPanel = transform.Find("Scroll View/Viewport/Content");
        CloseButton = transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupTeamEmblemPanelObject);
        });

        Button cardHeroButton = transform.Find("ButtonGroup/CardHeroButton").GetComponent<Button>();
        Button cardCaptainButton = transform.Find("ButtonGroup/CardCaptainButton").GetComponent<Button>();
        Button cardColonelButton = transform.Find("ButtonGroup/CardColonelButton").GetComponent<Button>();
        Button cardGeneralButton = transform.Find("ButtonGroup/CardGeneralButton").GetComponent<Button>();
        Button cardAdmiralButton = transform.Find("ButtonGroup/CardAdmiralButton").GetComponent<Button>();
        Button cardMonsterButton = transform.Find("ButtonGroup/CardMonsterButton").GetComponent<Button>();
        Button cardMilitaryButton = transform.Find("ButtonGroup/CardMilitaryButton").GetComponent<Button>();
        Button cardSpellButton = transform.Find("ButtonGroup/CardSpellButton").GetComponent<Button>();

        // Gom các Transform hiển thị kết quả vào Dictionary
        Dictionary<string, Transform> resultTransforms = new Dictionary<string, Transform>
        {
            { AppConstants.Emblem.FACTION_A, transform.Find("ResultGroup/EmblemA") },
            { AppConstants.Emblem.FACTION_B, transform.Find("ResultGroup/EmblemB") },
            { AppConstants.Emblem.FACTION_C, transform.Find("ResultGroup/EmblemC") },
            { AppConstants.Emblem.FACTION_D, transform.Find("ResultGroup/EmblemD") },
            { AppConstants.Emblem.FACTION_E, transform.Find("ResultGroup/EmblemE") }
        };

        List<Button> allButtons = new List<Button>
        {
            cardHeroButton,
            cardCaptainButton,
            cardColonelButton,
            cardGeneralButton,
            cardAdmiralButton,
            cardMonsterButton,
            cardMilitaryButton,
            cardSpellButton
        };

        Texture defaultBackground = TextureHelper.LoadTexture2DCached("UI/Background4/Item_Background_11");
        Texture activeBackground = TextureHelper.LoadTexture2DCached("UI/Background4/Item_Background_12");

        void SetActiveButton(Button clickedBtn)
        {
            foreach (var button in allButtons)
            {
                var image = button.transform.Find("Background4").GetComponent<RawImage>();
                image.texture = (button == clickedBtn) ? activeBackground : defaultBackground;
            }
        }

        // Gộp logic click
        void Bind(Button btn, string type)
        {
            btn.onClick.AddListener(async () =>
            {
                SetActiveButton(btn);
                await RenderEmblemsAsync(data, type, contentPanel, resultTransforms);
            });
        }

        // Gán sự kiện
        cardHeroButton.onClick.AddListener(() => SetActiveButton(cardHeroButton));
        cardCaptainButton.onClick.AddListener(() => SetActiveButton(cardCaptainButton));
        cardColonelButton.onClick.AddListener(() => SetActiveButton(cardColonelButton));
        cardGeneralButton.onClick.AddListener(() => SetActiveButton(cardGeneralButton));
        cardAdmiralButton.onClick.AddListener(() => SetActiveButton(cardAdmiralButton));
        cardMonsterButton.onClick.AddListener(() => SetActiveButton(cardMonsterButton));
        cardMilitaryButton.onClick.AddListener(() => SetActiveButton(cardMilitaryButton));
        cardSpellButton.onClick.AddListener(() => SetActiveButton(cardSpellButton));

        Bind(cardHeroButton, AppConstants.MainType.CARD_HERO);
        Bind(cardCaptainButton, AppConstants.MainType.CARD_CAPTAIN);
        Bind(cardColonelButton, AppConstants.MainType.CARD_COLONEL);
        Bind(cardGeneralButton, AppConstants.MainType.CARD_GENERAL);
        Bind(cardAdmiralButton, AppConstants.MainType.CARD_ADMIRAL);
        Bind(cardMonsterButton, AppConstants.MainType.CARD_MONSTER);
        Bind(cardMilitaryButton, AppConstants.MainType.CARD_MILITARY);
        Bind(cardSpellButton, AppConstants.MainType.CARD_SPELL);

        // Default chọn Hero
        SetActiveButton(cardHeroButton);
        _ = RenderEmblemsAsync(data, AppConstants.MainType.CARD_HERO, contentPanel, resultTransforms);
    }
    public async Task RenderEmblemsAsync(List<EmblemDTO> emblemDTOs, string cardType, Transform contentPanel, Dictionary<string, Transform> resultTransforms)
    {
        // Clear cũ
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        // Set up mặc định
        foreach (var kvp in resultTransforms)
        {
            string key = kvp.Key;
            Transform parent = kvp.Value;

            if (parent == null)
            {
                Debug.LogWarning($"Transform của {key} bị null");
                continue;
            }

            var image = parent.Find("Image")?.GetComponent<RawImage>();
            var titleText = parent.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
            var quantityText = parent.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();

            if (image != null) image.texture = TextureHelper.LoadTexture2DCached("UI/Background4/Background_V4_502");
            if (titleText != null) titleText.text = "?????";
            if (quantityText != null) quantityText.text = "";
        }

        var emblems = await TeamsService.Create().GetUserTeamEmblemsAsync(User.CurrentUserId, TeamId, TeamPositionIndex, cardType);
        List<EmblemDTO> dtoList = emblems.Select(te => new EmblemDTO
        {
            TeamId = te.TeamId,
            CardType = te.CardType,
            EmblemId = te.EmblemId,
            Position = te.Position,
            Count = te.EmblemQuantity,

            // chưa có dữ liệu → để tạm
            Name = te.Emblem.Name,
            Image = te.Emblem.Image,
            EmblemType = te.Emblem.Type

        }).ToList();

        foreach (var dto in dtoList)
        {
            if (resultTransforms.TryGetValue(dto.EmblemType, out var targetTransform))
            {
                UpdateResultUI(targetTransform, dto);
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy slot cho type: {dto.EmblemType}");
            }
        }

        var selectedNames = new HashSet<string>(
            dtoList
                .Where(x => !string.IsNullOrEmpty(x.Name))
                .Select(x => x.Name)
        );

        int GetFactionOrder(string type)
        {
            return type switch
            {
                AppConstants.Emblem.FACTION_A => 0,
                AppConstants.Emblem.FACTION_B => 1,
                AppConstants.Emblem.FACTION_C => 2,
                AppConstants.Emblem.FACTION_D => 3,
                AppConstants.Emblem.FACTION_E => 4,
                _ => 999 // fallback nếu có type lạ
            };
        }
        // Filter theo loại card
        var emblemFiltered = emblemDTOs
            .Where(x => x.CardType == cardType)
            .OrderBy(x => GetFactionOrder(x.EmblemType))   // sort theo type
            .ThenByDescending(x => x.Count);              // sort theo count

        foreach (var emblem in emblemFiltered)
        {
            var emblemButtonObject = Instantiate(EmblemButtonPrefab, contentPanel);
            Transform transform = emblemButtonObject.transform;

            var block = transform.Find("Block")?.gameObject;

            var selectButton = transform.Find("SelectButton").GetComponent<Button>();
            var emblemImage = transform.Find("Image").GetComponent<RawImage>();
            var quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            var titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            var emblemBackground = transform.Find("EmblemBackground").GetComponent<Image>();
            var emblemTypeText = transform.Find("EmblemTypeText").GetComponent<TextMeshProUGUI>();
            emblemTypeText.text = emblem.EmblemType;

            emblemBackground.color = ColorHelper.GetEmblemColor(emblem.EmblemType);

            emblemImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(emblem.Image));
            quantityText.text = "x" + emblem.Count;
            titleText.text = emblem.Name;

            // Check trùng name
            bool isSelected = selectedNames.Contains(emblem.Name);

            if (block != null)
                block.SetActive(isSelected);

            // Nếu đã chọn thì disable luôn button (optional)
            selectButton.interactable = !isSelected;
            // Xử lý sự kiện khi nhấn Select
            selectButton.onClick.AddListener(async () =>
            {
                // 1. Gọi API Insert (Giả sử bạn đã có biến user_id ở đâu đó trong class)
                bool isSuccess = await TeamsService.Create().InsertUserTeamEmblemsAsync(User.CurrentUserId, TeamId, TeamPositionIndex, emblem);

                if (isSuccess)
                {
                    // 2. Tìm Transform tương ứng dựa trên EmblemType (ví dụ: "Faction_A")
                    if (resultTransforms.TryGetValue(emblem.EmblemType, out Transform targetSlot))
                    {
                        UpdateResultUI(targetSlot, emblem);
                    }
                    // Update UI ngay sau khi chọn
                    if (block != null)
                        block.SetActive(true);

                    selectButton.interactable = false;
                }
            });
        }
    }
    private void UpdateResultUI(Transform transform, EmblemDTO emblemDTO)
    {
        // Tìm các thành phần UI bên trong slot (EmblemA, B...)
        // Lưu ý: Tên path (Image, TitleText...) phải khớp với cấu trúc trong Prefab của slot đó
        var image = transform.Find("Image")?.GetComponent<RawImage>();
        var titleText = transform.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
        var quantityText = transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();

        if (image != null) image.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(emblemDTO.Image));
        if (titleText != null) titleText.text = emblemDTO.Name;
        if (quantityText != null) quantityText.text = "x" + emblemDTO.Count;

        // Có thể thêm hiệu ứng feedback ở đây
        // Debug.Log($"Đã cập nhật {emblemDTO.Name} vào {transform.name}");
    }
    public async Task CreatePopupTeamSecondPanelAsync()
    {
        PopupTeamSecondObject = Instantiate(PopupTeamSecondPrefab, MainPanel);
        Transform transform = PopupTeamSecondObject.transform;
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
        PositionPanel = transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        Transform tempLeftContent = transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = transform.Find("ScrollViewRight/Viewport/Content");
        CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(PopupTeamSecondObject);
            CreatePopupTeamFirstPanel();
        });
        HomeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI teamNumberText = transform.Find("DictionaryCards/TeamPanel/NumberText").GetComponent<TextMeshProUGUI>();
        teamNumberText.text = TeamNumber.ToString();
        TextMeshProUGUI positionNumberText = transform.Find("DictionaryCards/PositionPanel/NumberText").GetComponent<TextMeshProUGUI>();
        positionNumberText.text = TeamPositionIndex.ToString();
        TextMeshProUGUI cardTypeText = transform.Find("DictionaryCards/CardTypePanel/TypeText").GetComponent<TextMeshProUGUI>();
        TMP_Dropdown dropdownType = transform.Find("DictionaryCards/DropdownType").GetComponent<TMP_Dropdown>();
        TextMeshProUGUI teamTitleText = transform.Find("DictionaryCards/TeamTitleText").GetComponent<TextMeshProUGUI>();
        PowerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();

        var cardList = await GetUserCardsTeamByTypeAsync(TeamPositionIndex.ToString(), cardTypeText);
        // var cardList = new List<ICard>();
        CreateSlotAsync(cardList, PositionPanel);
    }
    private async Task<List<ICard>> GetUserCardsTeamByTypeAsync(string position, TextMeshProUGUI cardTypeText)
    {
        try
        {
            switch (MainType)
            {
                case AppConstants.MainType.CARD_HERO:
                    cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HERO);
                    return (await UserCardHeroesService
                        .GetUserCardHeroesTeamAsync(User.CurrentUserId, TeamId, position))
                        .Cast<ICard>().ToList();

                case AppConstants.MainType.CARD_CAPTAIN:
                    cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);
                    return (await UserCardCaptainsService
                        .GetUserCardCaptainsTeamAsync(User.CurrentUserId, TeamId, position))
                        .Cast<ICard>().ToList();

                case AppConstants.MainType.CARD_COLONEL:
                    cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_COLONEL);
                    return (await UserCardColonelsService
                        .GetUserCardColonelsTeamAsync(User.CurrentUserId, TeamId, position))
                        .Cast<ICard>().ToList();

                case AppConstants.MainType.CARD_GENERAL:
                    cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERAL);
                    return (await UserCardGeneralsService
                        .GetUserCardGeneralsTeamAsync(User.CurrentUserId, TeamId, position))
                        .Cast<ICard>().ToList();

                case AppConstants.MainType.CARD_ADMIRAL:
                    cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);
                    return (await UserCardAdmiralsService
                        .GetUserCardAdmiralsTeamAsync(User.CurrentUserId, TeamId, position))
                        .Cast<ICard>().ToList();

                case AppConstants.MainType.CARD_MONSTER:
                    cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MONSTER);
                    return (await UserCardMonstersService
                        .GetUserCardMonstersTeamAsync(User.CurrentUserId, TeamId, position))
                        .Cast<ICard>().ToList();

                case AppConstants.MainType.CARD_MILITARY:
                    cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MILITARY);
                    return (await UserCardMilitariesService
                        .GetUserCardMilitariesTeamAsync(User.CurrentUserId, TeamId, position))
                        .Cast<ICard>().ToList();

                case AppConstants.MainType.CARD_SPELL:
                    cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_SPELL);
                    return (await UserCardSpellsService
                        .GetUserCardSpellsTeamAsync(User.CurrentUserId, TeamId, position))
                        .Cast<ICard>().ToList();

                default:
                    return new List<ICard>();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return new List<ICard>();
        }
    }
    private async Task<List<ICard>> GetUserCardsByTypeAsync(string mainType, TextMeshProUGUI cardTypeText)
    {
        int totalRecord = 0;
        switch (mainType)
        {
            case AppConstants.MainType.CARD_HERO:
                cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HERO);
                totalRecord = await UserCardHeroesService.Create().GetUserCardHeroesCountAsync(User.CurrentUserId, Search, Type, Rare);
                TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
                PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
                return (await UserCardHeroesService
                    .GetUserCardHeroesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare))
                    .Where(card => GetX(card.Position) != TeamPositionIndex)
                    .Cast<ICard>().ToList();

            case AppConstants.MainType.CARD_CAPTAIN:
                cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);
                totalRecord = await UserCardCaptainsService.Create().GetUserCardCaptainsCountAsync(User.CurrentUserId, Search, Type, Rare);
                TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
                PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
                return (await UserCardCaptainsService
                    .GetUserCardCaptainsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare))
                    .Where(card => GetX(card.Position) != TeamPositionIndex)
                    .Cast<ICard>().ToList();

            case AppConstants.MainType.CARD_COLONEL:
                cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_COLONEL);
                totalRecord = await UserCardColonelsService.Create().GetUserCardColonelsCountAsync(User.CurrentUserId, Search, Type, Rare);
                TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
                PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
                return (await UserCardColonelsService
                    .GetUserCardColonelsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare))
                    .Where(card => GetX(card.Position) != TeamPositionIndex)
                    .Cast<ICard>().ToList();

            case AppConstants.MainType.CARD_GENERAL:
                cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERAL);
                totalRecord = await UserCardGeneralsService.Create().GetUserCardGeneralsCountAsync(User.CurrentUserId, Search, Type, Rare);
                TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
                PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
                return (await UserCardGeneralsService
                    .GetUserCardGeneralsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare))
                    .Where(card => GetX(card.Position) != TeamPositionIndex)
                    .Cast<ICard>().ToList();

            case AppConstants.MainType.CARD_ADMIRAL:
                cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);
                totalRecord = await UserCardAdmiralsService.Create().GetUserCardAdmiralsCountAsync(User.CurrentUserId, Search, Type, Rare);
                TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
                PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
                return (await UserCardAdmiralsService
                    .GetUserCardAdmiralsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare))
                    .Where(card => GetX(card.Position) != TeamPositionIndex)
                    .Cast<ICard>().ToList();

            case AppConstants.MainType.CARD_MONSTER:
                cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MONSTER);
                totalRecord = await UserCardMonstersService.Create().GetUserCardMonstersCountAsync(User.CurrentUserId, Search, Type, Rare);
                TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
                PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
                return (await UserCardMonstersService
                    .GetUserCardMonstersAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare))
                    .Where(card => GetX(card.Position) != TeamPositionIndex)
                    .Cast<ICard>().ToList();

            case AppConstants.MainType.CARD_MILITARY:
                cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MILITARY);
                totalRecord = await UserCardMilitariesService.Create().GetUserCardMilitariesCountAsync(User.CurrentUserId, Search, Type, Rare);
                TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
                PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
                return (await UserCardMilitariesService
                    .GetUserCardMilitariesAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare))
                    .Where(card => GetX(card.Position) != TeamPositionIndex)
                    .Cast<ICard>().ToList();

            case AppConstants.MainType.CARD_SPELL:
                cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_SPELL);
                totalRecord = await UserCardSpellsService.Create().GetUserCardSpellsCountAsync(User.CurrentUserId, Search, Type, Rare);
                TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
                PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
                return (await UserCardSpellsService
                    .GetUserCardSpellsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare))
                    .Where(card => GetX(card.Position) != TeamPositionIndex)
                    .Cast<ICard>().ToList();

            default:
                return new List<ICard>();
        }

    }
    private void CreateSlotAsync(List<ICard> cardList, Transform positionPanel)
    {
        for (int i = 1; i <= 10; i++)
        {
            GameObject teamSlotSecondObject = Instantiate(TeamSlotSecondPrefab, positionPanel);
            Transform transform = teamSlotSecondObject.transform;

            RawImage cardImage = transform.Find("CardImage").GetComponent<RawImage>();
            TextMeshProUGUI positionText = transform.Find("PositionText").GetComponent<TextMeshProUGUI>();

            Button selectButton = transform.Find("ButtonGroup/SelectButton").GetComponent<Button>();
            Button removeButton = transform.Find("ButtonGroup/RemoveButton").GetComponent<Button>();
            Button changeButton = transform.Find("ButtonGroup/ChangeButton").GetComponent<Button>();

            selectButton.onClick.RemoveAllListeners();
            removeButton.onClick.RemoveAllListeners();
            changeButton.onClick.RemoveAllListeners();

            int tempSlotIndex = i;
            positionText.text = tempSlotIndex.ToString();

            var card = cardList.FirstOrDefault(c => GetY(c.Position) == tempSlotIndex);

            if (card != null)
            {
                cardImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(card.Image));

                string cardId = card.Id;

                selectButton.onClick.AddListener(async () =>
                {
                    TeamSlotIndex = tempSlotIndex;
                    await OnSelectCardClickAsync(card);
                });

                removeButton.onClick.AddListener(async () =>
                {
                    await OnRemoveCardClickAsync(cardId, card);
                    Destroy(PopupTeamSecondObject);
                    await CreatePopupTeamSecondPanelAsync();
                });
            }
            else
            {
                cardImage.texture = TextureHelper.LoadTexture2DCached("UI/Background4/Background_V4_505");
                removeButton.gameObject.SetActive(false);

                selectButton.onClick.AddListener(async () =>
                {
                    TeamSlotIndex = tempSlotIndex;
                    await OnSelectCardClickAsync();
                });
            }
        }
    }
    private int GetX(string position)
    {
        if (string.IsNullOrEmpty(position)) return -1;

        var parts = position.Split('-');
        if (parts.Length < 2) return -1;

        return int.TryParse(parts[0], out int x) ? x : -1;
    }
    private int GetY(string position)
    {
        if (string.IsNullOrEmpty(position)) return -1;

        var parts = position.Split('-');
        if (parts.Length < 2) return -1;

        return int.TryParse(parts[1], out int y) ? y : -1;
    }
    public async Task OnSelectCardClickAsync(ICard card = null)
    {
        PopupCardPanelObject = Instantiate(PopupCardPanelPrefab, MainPanel);
        Transform transform = PopupCardPanelObject.transform;
        Transform contentPanel = transform.Find("Scroll View/Viewport/Content");
        TMP_Dropdown rareDropdown = transform.Find("InputGroup/RareDropdown").GetComponent<TMP_Dropdown>();
        TMP_Dropdown typeDropdown = transform.Find("InputGroup/TypeDropdown").GetComponent<TMP_Dropdown>();
        TMP_InputField searchInputField = transform.Find("InputGroup/Search").GetComponent<TMP_InputField>();
        Button searchButton = transform.Find("InputGroup/SearchButton").GetComponent<Button>();
        PageText = transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = transform.Find("Pagination/Previous").GetComponent<Button>();
        TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        CloseButton = transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(PopupCardPanelObject);
        });

        searchButton.onClick.AddListener(async () =>
        {
            Offset = 0;
            CurrentPage = 1;
            string searchText = searchInputField.text;
            Search = searchText;
            var cardList = await GetUserCardsByTypeAsync(MainType, titleText);
            CreateUserCards(cardList, contentPanel);
        });

        List<string> uniqueRaries = QualityEvaluatorHelper.rarities;
        if (uniqueRaries.Count > 0)
        {
            rareDropdown.ClearOptions();
            rareDropdown.AddOptions(uniqueRaries);

            // Quan trọng: clear listener cũ trước
            rareDropdown.onValueChanged.RemoveAllListeners();

            // Gán sự kiện
            rareDropdown.onValueChanged.AddListener(async (index) =>
            {
                Offset = 0;
                CurrentPage = 1;
                // Lấy text đang chọn
                string selectedRare = rareDropdown.options[index].text;
                Rare = selectedRare;

                // Gọi async (fire & forget an toàn)
                var cardList = await GetUserCardsByTypeAsync(MainType, titleText);
                CreateUserCards(cardList, contentPanel);
            });

            rareDropdown.value = 0;
            rareDropdown.RefreshShownValue();
        }

        List<string> uniqueTypes = await TypeManager.GetUniqueTypesAsync(MainType);
        uniqueTypes.Insert(0, AppConstants.Type.ALL);
        if (uniqueTypes.Count > 0)
        {
            typeDropdown.ClearOptions();
            typeDropdown.AddOptions(uniqueTypes);

            // Quan trọng: clear listener cũ trước
            typeDropdown.onValueChanged.RemoveAllListeners();

            // Gán sự kiện
            typeDropdown.onValueChanged.AddListener(async (index) =>
            {
                Offset = 0;
                CurrentPage = 1;
                // Lấy text đang chọn
                string selectedType = typeDropdown.options[index].text;
                Type = selectedType;

                // Gọi async (fire & forget an toàn)
                var cardList = await GetUserCardsByTypeAsync(MainType, titleText);
                CreateUserCards(cardList, contentPanel);
            });

            typeDropdown.value = 0;
            typeDropdown.RefreshShownValue();
        }

        NextButton.onClick.AddListener(async () =>
        {
            if (CurrentPage < TotalPage)
            {
                ButtonEvent.Instance.Close(contentPanel);
                CurrentPage = CurrentPage + 1;
                Offset = Offset + PAGE_SIZE;
                // Gọi async (fire & forget an toàn)
                var cardList = await GetUserCardsByTypeAsync(MainType, titleText);
                CreateUserCards(cardList, contentPanel);

            }
        });

        PreviousButton.onClick.AddListener(async () =>
        {
            if (CurrentPage > 1)
            {
                ButtonEvent.Instance.Close(contentPanel);
                CurrentPage = CurrentPage - 1;
                Offset = Offset - PAGE_SIZE;
                // Gọi async (fire & forget an toàn)
                var cardList = await GetUserCardsByTypeAsync(MainType, titleText);
                CreateUserCards(cardList, contentPanel);

            }
        });

        var cardList = await GetUserCardsByTypeAsync(MainType, titleText);
        CreateUserCards(cardList, contentPanel, card);
    }
    public async Task OnRemoveCardClickAsync(string cardId, ICard oldCard)
    {
        double currentPower = User.CurrentUserPower;
        if (MainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            await UserCardHeroesService.UpdateTeamCardHeroAsync(null, null, cardId);
            double updatedPower = currentPower - oldCard.Power;

            await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
            User.CurrentUserPower = updatedPower;

            FindObjectOfType<PowerController>().ShowPower(currentPower, oldCard.Power, 0);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            await UserCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, cardId);
            double updatedPower = currentPower - oldCard.Power;

            await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
            User.CurrentUserPower = updatedPower;

            FindObjectOfType<PowerController>().ShowPower(currentPower, oldCard.Power, 0);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            await UserCardColonelsService.UpdateTeamCardColonelAsync(null, null, cardId);
            double updatedPower = currentPower - oldCard.Power;

            await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
            User.CurrentUserPower = updatedPower;

            FindObjectOfType<PowerController>().ShowPower(currentPower, oldCard.Power, 0);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            await UserCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, cardId);
            double updatedPower = currentPower - oldCard.Power;

            await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
            User.CurrentUserPower = updatedPower;

            FindObjectOfType<PowerController>().ShowPower(currentPower, oldCard.Power, 0);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            await UserCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, cardId);
            double updatedPower = currentPower - oldCard.Power;

            await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
            User.CurrentUserPower = updatedPower;

            FindObjectOfType<PowerController>().ShowPower(currentPower, oldCard.Power, 0);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            await UserCardMonstersService.UpdateTeamCardMonsterAsync(null, null, cardId);
            double updatedPower = currentPower - oldCard.Power;

            await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
            User.CurrentUserPower = updatedPower;

            FindObjectOfType<PowerController>().ShowPower(currentPower, oldCard.Power, 0);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            await UserCardMilitariesService.UpdateTeamCardMilitaryAsync(null, null, cardId);
            double updatedPower = currentPower - oldCard.Power;

            await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
            User.CurrentUserPower = updatedPower;

            FindObjectOfType<PowerController>().ShowPower(currentPower, oldCard.Power, 0);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            await UserCardSpellsService.UpdateTeamCardSpellAsync(null, null, cardId);
            double updatedPower = currentPower - oldCard.Power;

            await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
            User.CurrentUserPower = updatedPower;

            FindObjectOfType<PowerController>().ShowPower(currentPower, oldCard.Power, 0);
        }
        await TeamsService.Create().DeleteUserTeamEmblemsAsync(User.CurrentUserId, TeamId, TeamPositionIndex, MainType);
    }
    public void CreateUserCards(List<ICard> cards, Transform contentPanel, ICard oldCard = null)
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        // var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        // if (oldAnim != null) Destroy(oldAnim);

        foreach (var card in cards)
        {
            GameObject cardSelectButtonObject = Instantiate(CardSelectButtonPrefab, contentPanel);
            Transform transform = cardSelectButtonObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = card.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(card.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = card.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HERO);

            TextMeshProUGUI typePanel = transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = card.Type.ToString().Replace("_", " ");

            Image rareBackground = transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(card.Rarity));

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(card.Rarity));
            rareText.text = card.Rarity;

            Transform teamPanel = transform.Find("TeamPanel");
            if (card.Team.TeamNumber != 0)
            {
                teamPanel.gameObject.SetActive(true);
                // RawImage teamBackgroundImage = transform.Find("Team/Background").GetComponent<RawImage>();
                // TextMeshProUGUI teamTitleText = transform.Find("Team/TitleText").GetComponent<TextMeshProUGUI>();
                // Texture teamBackgroundTexture = TextureHelper.LoadTextureCached(ImageConstants.Team.TEAM_BACKGROUND_1);
                // teamBackgroundImage.texture = teamBackgroundTexture;
                // teamTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TEAM) + " " + card.Team.TeamNumber.ToString();
            }
            else
            {
                teamPanel.gameObject.SetActive(false);
            }

            // Button button = transform.GetComponent<Button>();
            // button.onClick.AddListener(() =>
            // {
            //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            //     MainMenuDetailsManager.Instance.PopupDetails(card, MainPanel);
            // });

            Button selectButton = transform.Find("SelectButton").GetComponent<Button>();
            selectButton.onClick.AddListener(async () =>
            {
                if (oldCard != null)
                {
                    CreatePopWarningPanel(card, oldCard);
                }
                else
                {
                    await OnSelectCardIntoTeamClickAsync(card, oldCard);
                    Destroy(PopupCardPanelObject);
                    Destroy(PopupTeamSecondObject);
                    await CreatePopupTeamSecondPanelAsync();
                    await TeamsService.Create().DeleteUserTeamEmblemsAsync(User.CurrentUserId, TeamId, TeamPositionIndex, MainType);
                }
            });
        }
        // GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        // if (gridLayout != null)
        // {
        //     gridLayout.cellSize = new Vector2(250, 360);
        //     gridLayout.spacing = new Vector2(23, 10);
        // }
        // contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreatePopWarningPanel(ICard card, ICard oldCard)
    {
        GameObject popupWarningPanelObject = Instantiate(PopupWarningPanelPrefab, MainPanel);
        Transform transform = popupWarningPanelObject.transform;
        Button confirmButton = transform.Find("ConfirmButton").GetComponent<Button>();
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        confirmButton.onClick.AddListener(async () =>
        {
            await OnSelectCardIntoTeamClickAsync(card, oldCard);
            Destroy(PopupCardPanelObject);
            Destroy(PopupTeamSecondObject);
            await CreatePopupTeamSecondPanelAsync();
            await TeamsService.Create().DeleteUserTeamEmblemsAsync(User.CurrentUserId, TeamId, TeamPositionIndex, MainType);
        });
        closeButton.onClick.AddListener(() =>
        {
            Destroy(popupWarningPanelObject);
        });
    }
    public async Task OnSelectCardIntoTeamClickAsync(ICard newCard, ICard oldCard = null)
    {
        double currentPower = User.CurrentUserPower;
        string newPosition = TeamPositionIndex.ToString() + "-" + TeamSlotIndex.ToString();
        if (newCard is Equipments cardHero)
        {
            if (oldCard == null)
            {
                await UserCardHeroesService.UpdateTeamCardHeroAsync(TeamId, newPosition, cardHero.Id);
                double updatedPower = currentPower + cardHero.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardHero.Power, 1);
            }
            else
            {
                await UserCardHeroesService.UpdateTeamCardHeroAsync(null, null, oldCard.Id);
                await UserCardHeroesService.UpdateTeamCardHeroAsync(TeamId, newPosition, cardHero.Id);
                if (cardHero.Power >= oldCard.Power)
                {
                    double diffPower = cardHero.Power - oldCard.Power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = oldCard.Power - cardHero.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
        }
        else if (newCard is CardCaptains cardCaptain)
        {
            if (oldCard == null)
            {
                await UserCardCaptainsService.UpdateTeamCardCaptainAsync(TeamId, newPosition, cardCaptain.Id);
                double updatedPower = currentPower + cardCaptain.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardCaptain.Power, 1);
            }
            else
            {
                await UserCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, oldCard.Id);
                await UserCardCaptainsService.UpdateTeamCardCaptainAsync(TeamId, newPosition, cardCaptain.Id);
                if (cardCaptain.Power >= oldCard.Power)
                {
                    double diffPower = cardCaptain.Power - oldCard.Power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = oldCard.Power - cardCaptain.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
        }
        else if (newCard is CardColonels cardColonel)
        {
            if (oldCard == null)
            {
                await UserCardColonelsService.UpdateTeamCardColonelAsync(TeamId, newPosition, cardColonel.Id);
                double updatedPower = currentPower + cardColonel.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardColonel.Power, 1);
            }
            else
            {
                await UserCardColonelsService.UpdateTeamCardColonelAsync(null, null, oldCard.Id);
                await UserCardColonelsService.UpdateTeamCardColonelAsync(TeamId, newPosition, cardColonel.Id);
                if (cardColonel.Power >= oldCard.Power)
                {
                    double diffPower = cardColonel.Power - oldCard.Power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = oldCard.Power - cardColonel.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
        }
        else if (newCard is CardGenerals cardGeneral)
        {
            if (oldCard == null)
            {
                await UserCardGeneralsService.UpdateTeamCardGeneralAsync(TeamId, newPosition, cardGeneral.Id);
                double updatedPower = currentPower + cardGeneral.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardGeneral.Power, 1);
            }
            else
            {
                await UserCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, oldCard.Id);
                await UserCardGeneralsService.UpdateTeamCardGeneralAsync(TeamId, newPosition, cardGeneral.Id);
                if (cardGeneral.Power >= oldCard.Power)
                {
                    double diffPower = cardGeneral.Power - oldCard.Power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = oldCard.Power - cardGeneral.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
        }
        else if (newCard is CardAdmirals cardAdmiral)
        {
            if (oldCard == null)
            {
                await UserCardAdmiralsService.UpdateTeamCardAdmiralAsync(TeamId, newPosition, cardAdmiral.Id);
                double updatedPower = currentPower + cardAdmiral.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardAdmiral.Power, 1);
            }
            else
            {
                await UserCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, oldCard.Id);
                await UserCardAdmiralsService.UpdateTeamCardAdmiralAsync(TeamId, newPosition, cardAdmiral.Id);
                if (cardAdmiral.Power >= oldCard.Power)
                {
                    double diffPower = cardAdmiral.Power - oldCard.Power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = oldCard.Power - cardAdmiral.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
        }
        else if (newCard is CardMonsters cardMonster)
        {
            if (oldCard == null)
            {
                await UserCardMonstersService.UpdateTeamCardMonsterAsync(TeamId, newPosition, cardMonster.Id);
                double updatedPower = currentPower + cardMonster.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardMonster.Power, 1);
            }
            else
            {
                await UserCardMonstersService.UpdateTeamCardMonsterAsync(null, null, oldCard.Id);
                await UserCardMonstersService.UpdateTeamCardMonsterAsync(TeamId, newPosition, cardMonster.Id);
                if (cardMonster.Power >= oldCard.Power)
                {
                    double diffPower = cardMonster.Power - oldCard.Power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = oldCard.Power - cardMonster.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
        }
        else if (newCard is CardMilitaries cardMilitary)
        {
            if (oldCard == null)
            {
                await UserCardMilitariesService.UpdateTeamCardMilitaryAsync(TeamId, newPosition, cardMilitary.Id);
                double updatedPower = currentPower + cardMilitary.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardMilitary.Power, 1);
            }
            else
            {
                await UserCardMilitariesService.UpdateTeamCardMilitaryAsync(null, null, oldCard.Id);
                await UserCardMilitariesService.UpdateTeamCardMilitaryAsync(TeamId, newPosition, cardMilitary.Id);
                if (cardMilitary.Power >= oldCard.Power)
                {
                    double diffPower = cardMilitary.Power - oldCard.Power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = oldCard.Power - cardMilitary.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
        }
        else if (newCard is CardSpells cardSpell)
        {
            if (oldCard == null)
            {
                await UserCardSpellsService.UpdateTeamCardSpellAsync(TeamId, newPosition, cardSpell.Id);
                double updatedPower = currentPower + cardSpell.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardSpell.Power, 1);
            }
            else
            {
                await UserCardSpellsService.UpdateTeamCardSpellAsync(null, null, oldCard.Id);
                await UserCardSpellsService.UpdateTeamCardSpellAsync(TeamId, newPosition, cardSpell.Id);
                if (cardSpell.Power >= oldCard.Power)
                {
                    double diffPower = cardSpell.Power - oldCard.Power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = oldCard.Power - cardSpell.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
        }
    }
    public async Task LoadCardDataByTypeAsync(string type, string selectedOptionName, int team_limit, int team_offset, Transform choseTeam)
    {
        List<object> cardObjects = null;

        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                List<CardHeroes> cardHeroList = await UserCardHeroesService.GetUserCardHeroesAsync(User.CurrentUserId, Search, selectedOptionName, team_limit, team_offset, Rare);
                cardObjects = cardHeroList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_CAPTAIN:
                List<CardCaptains> cardCaptainList = await UserCardCaptainsService.GetUserCardCaptainsAsync(User.CurrentUserId, Search, selectedOptionName, team_limit, team_offset, Rare);
                cardObjects = cardCaptainList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_COLONEL:
                List<CardColonels> cardColonelList = await UserCardColonelsService.GetUserCardColonelsAsync(User.CurrentUserId, Search, selectedOptionName, team_limit, team_offset, Rare);
                cardObjects = cardColonelList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_GENERAL:
                List<CardGenerals> cardGeneralList = await UserCardGeneralsService.GetUserCardGeneralsAsync(User.CurrentUserId, Search, selectedOptionName, team_limit, team_offset, Rare);
                cardObjects = cardGeneralList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_ADMIRAL:
                List<CardAdmirals> cardAdmiralList = await UserCardAdmiralsService.GetUserCardAdmiralsAsync(User.CurrentUserId, Search, selectedOptionName, team_limit, team_offset, Rare);
                cardObjects = cardAdmiralList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_MONSTER:
                List<CardMonsters> cardMonsterList = await UserCardMonstersService.GetUserCardMonstersAsync(User.CurrentUserId, Search, selectedOptionName, team_limit, team_offset, Rare);
                cardObjects = cardMonsterList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_MILITARY:
                List<CardMilitaries> cardMilitaryList = await UserCardMilitariesService.GetUserCardMilitariesAsync(User.CurrentUserId, Search, selectedOptionName, team_limit, team_offset, Rare);
                cardObjects = cardMilitaryList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_SPELL:
                List<CardSpells> cardSpellList = await UserCardSpellsService.GetUserCardSpellsAsync(User.CurrentUserId, Search, selectedOptionName, team_limit, team_offset, Rare);
                cardObjects = cardSpellList.Cast<object>().ToList();
                break;

            default:
                Debug.LogWarning("Unknown type: " + type);
                break;
        }

        if (cardObjects != null)
        {
            CreateCardTeams(cardObjects, choseTeam);
        }
    }
    public async Task CreatePositionAsync(Transform positionPanel, GameObject teamsObject)
    {
        foreach (Transform child in positionPanel)
        {
            Destroy(child.gameObject); // Hoặc DestroyImmediate(child.gameObject) nếu cần xóa ngay lập tức
        }
        var backgroundTexture = "Background_V4_408";
        Texture defaultBackgroundTexture = TextureHelper.LoadTextureCached($"UI/Background4/{backgroundTexture}");
        if (MainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            double totalPower = 0;
            List<CardHeroes> cardHeroList = await UserCardHeroesService.GetUserCardHeroesTeamAsync(User.CurrentUserId, TeamId, Position);
            cardHeroList = cardHeroList
                .Where(cardHero => cardHero.TeamId.Equals(TeamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < MaxMembersInTeamPosition; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(false);
                PositionBackground.gameObject.SetActive(false);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardHeroes matchingCardHero = cardHeroList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.Position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardHero != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(matchingCardHero.Image);
                    Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardHero.Power;
                }
                else
                {
                    image.texture = defaultBackgroundTexture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    image.texture = null;
                    await UserCardHeroesService.UpdateTeamCardHeroAsync(null, null, matchingCardHero.Id);
                    double newPower = await TeamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardHero.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(MainType, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.TeamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.PositionId = tempPositionId.ToString();
                    if (matchingCardHero != null)
                    {
                        dropHandler.CardId = matchingCardHero.Id;
                        dropHandler.CardPower = matchingCardHero.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            PowerText.text = NumberFormatterHelper.FormatNumber(totalPower);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            double totalPower = 0;
            List<CardCaptains> cardCaptainList = await UserCardCaptainsService.GetUserCardCaptainsTeamAsync(User.CurrentUserId, TeamId, Position);
            cardCaptainList = cardCaptainList
                .Where(cardHero => cardHero.TeamId.Equals(TeamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < MaxMembersInTeamPosition; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(false);
                PositionBackground.gameObject.SetActive(false);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardCaptains matchingCardCaptain = cardCaptainList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.Position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardCaptain != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(matchingCardCaptain.Image);
                    Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardCaptain.Power;
                }
                else
                {
                    image.texture = defaultBackgroundTexture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    image.texture = null;
                    await UserCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, matchingCardCaptain.Id);
                    double newPower = await TeamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardCaptain.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(MainType, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.TeamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.PositionId = tempPositionId.ToString();
                    if (matchingCardCaptain != null)
                    {
                        dropHandler.CardId = matchingCardCaptain.Id;
                        dropHandler.CardPower = matchingCardCaptain.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            PowerText.text = NumberFormatterHelper.FormatNumber(totalPower);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            double totalPower = 0;
            List<CardColonels> cardColonelList = await UserCardColonelsService.GetUserCardColonelsTeamAsync(User.CurrentUserId, TeamId, Position);
            cardColonelList = cardColonelList
                .Where(cardHero => cardHero.TeamId.Equals(TeamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < MaxMembersInTeamPosition; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(false);
                PositionBackground.gameObject.SetActive(false);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardColonels matchingCardColonel = cardColonelList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.Position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardColonel != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(matchingCardColonel.Image);
                    Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardColonel.Power;
                }
                else
                {
                    image.texture = defaultBackgroundTexture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    image.texture = null;
                    await UserCardColonelsService.UpdateTeamCardColonelAsync(null, null, matchingCardColonel.Id);
                    double newPower = await TeamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardColonel.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(MainType, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.TeamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.PositionId = tempPositionId.ToString();
                    if (matchingCardColonel != null)
                    {
                        dropHandler.CardId = matchingCardColonel.Id;
                        dropHandler.CardPower = matchingCardColonel.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            PowerText.text = NumberFormatterHelper.FormatNumber(totalPower);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            double totalPower = 0;
            List<CardGenerals> cardGeneralList = await UserCardGeneralsService.GetUserCardGeneralsTeamAsync(User.CurrentUserId, TeamId, Position);
            cardGeneralList = cardGeneralList
                .Where(cardHero => cardHero.TeamId.Equals(TeamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < MaxMembersInTeamPosition; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(false);
                PositionBackground.gameObject.SetActive(false);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardGenerals matchingCardGeneral = cardGeneralList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.Position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardGeneral != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(matchingCardGeneral.Image);
                    Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardGeneral.Power;
                }
                else
                {
                    image.texture = defaultBackgroundTexture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    image.texture = null;
                    await UserCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, matchingCardGeneral.Id);
                    double newPower = await TeamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardGeneral.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(MainType, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.TeamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.PositionId = tempPositionId.ToString();
                    if (matchingCardGeneral != null)
                    {
                        dropHandler.CardId = matchingCardGeneral.Id;
                        dropHandler.CardPower = matchingCardGeneral.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            PowerText.text = NumberFormatterHelper.FormatNumber(totalPower);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            double totalPower = 0;
            List<CardAdmirals> cardAdmiralList = await UserCardAdmiralsService.GetUserCardAdmiralsTeamAsync(User.CurrentUserId, TeamId, Position);
            cardAdmiralList = cardAdmiralList
                .Where(cardHero => cardHero.TeamId.Equals(TeamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < MaxMembersInTeamPosition; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(false);
                PositionBackground.gameObject.SetActive(false);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardAdmirals matchingCardAdmiral = cardAdmiralList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.Position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardAdmiral != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(matchingCardAdmiral.Image);
                    Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardAdmiral.Power;
                }
                else
                {
                    image.texture = defaultBackgroundTexture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    image.texture = null;
                    await UserCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, matchingCardAdmiral.Id);
                    double newPower = await TeamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardAdmiral.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(MainType, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.TeamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.PositionId = tempPositionId.ToString();
                    if (matchingCardAdmiral != null)
                    {
                        dropHandler.CardId = matchingCardAdmiral.Id;
                        dropHandler.CardPower = matchingCardAdmiral.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            PowerText.text = NumberFormatterHelper.FormatNumber(totalPower);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            double totalPower = 0;
            List<CardMonsters> cardMonsterList = await UserCardMonstersService.GetUserCardMonstersTeamAsync(User.CurrentUserId, TeamId, Position);
            cardMonsterList = cardMonsterList
                .Where(cardHero => cardHero.TeamId.Equals(TeamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < MaxMembersInTeamPosition; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(false);
                PositionBackground.gameObject.SetActive(false);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardMonsters matchingCardMonster = cardMonsterList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.Position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardMonster != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(matchingCardMonster.Image);
                    Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardMonster.Power;
                }
                else
                {
                    image.texture = defaultBackgroundTexture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    image.texture = null;
                    await UserCardMonstersService.UpdateTeamCardMonsterAsync(null, null, matchingCardMonster.Id);
                    double newPower = await TeamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardMonster.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(MainType, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.TeamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.PositionId = tempPositionId.ToString();
                    if (matchingCardMonster != null)
                    {
                        dropHandler.CardId = matchingCardMonster.Id;
                        dropHandler.CardPower = matchingCardMonster.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            PowerText.text = NumberFormatterHelper.FormatNumber(totalPower);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            double totalPower = 0;
            List<CardMilitaries> cardMilitaryList = await UserCardMilitariesService.GetUserCardMilitariesTeamAsync(User.CurrentUserId, TeamId, Position);
            cardMilitaryList = cardMilitaryList
                .Where(cardHero => cardHero.TeamId.Equals(TeamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < MaxMembersInTeamPosition; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(false);
                PositionBackground.gameObject.SetActive(false);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardMilitaries matchingCardMilitary = cardMilitaryList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.Position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardMilitary != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(matchingCardMilitary.Image);
                    Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardMilitary.Power;
                }
                else
                {
                    image.texture = defaultBackgroundTexture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    image.texture = null;
                    await UserCardMilitariesService.UpdateTeamCardMilitaryAsync(null, null, matchingCardMilitary.Id);
                    double newPower = await TeamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardMilitary.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(MainType, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.TeamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.PositionId = tempPositionId.ToString();
                    if (matchingCardMilitary != null)
                    {
                        dropHandler.CardId = matchingCardMilitary.Id;
                        dropHandler.CardPower = matchingCardMilitary.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            PowerText.text = NumberFormatterHelper.FormatNumber(totalPower);
        }
        else if (MainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            double totalPower = 0;
            List<CardSpells> cardSpellList = await UserCardSpellsService.GetUserCardSpellsTeamAsync(User.CurrentUserId, TeamId, Position);
            cardSpellList = cardSpellList
                .Where(cardHero => cardHero.TeamId.Equals(TeamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < MaxMembersInTeamPosition; i++)
            {
                GameObject positionObject = Instantiate(PositionPrefab, positionPanel);
                RawImage image = positionObject.transform.Find("Image").GetComponent<RawImage>();
                RawImage PositionBackground = positionObject.transform.Find("PositionBackground").GetComponent<RawImage>();
                Button positionButton = positionObject.transform.Find("PositionButton").GetComponent<Button>();
                positionButton.gameObject.SetActive(false);
                PositionBackground.gameObject.SetActive(false);
                RawImage LeaveBackground = positionObject.transform.Find("LeaveBackground").GetComponent<RawImage>();
                Button leaveButton = positionObject.transform.Find("LeaveButton").GetComponent<Button>();
                TMP_Text buttonText = positionButton.GetComponentInChildren<TMP_Text>();

                // Tìm cardHeroes có position trùng với vị trí i
                CardSpells matchingCardSpell = cardSpellList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.Position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardSpell != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(matchingCardSpell.Image);
                    Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardSpell.Power;
                }
                else
                {
                    image.texture = defaultBackgroundTexture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    image.texture = null;
                    await UserCardSpellsService.UpdateTeamCardSpellAsync(null, null, matchingCardSpell.Id);
                    double newPower = await TeamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardSpell.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(MainType, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.TeamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.PositionId = tempPositionId.ToString();
                    if (matchingCardSpell != null)
                    {
                        dropHandler.CardId = matchingCardSpell.Id;
                        dropHandler.CardPower = matchingCardSpell.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            PowerText.text = NumberFormatterHelper.FormatNumber(totalPower);
        }
    }
    public void CreateCardTeams(List<object> obj, Transform panel)
    {
        foreach (Transform child in panel)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in obj)
        {
            if (item is CardHeroes cardHero)
            {
                GameObject cardObject = Instantiate(CardThirdPrefab, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardHero.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatterHelper.FormatNumber(cardHero.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardHero.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardHero.Rarity}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardHero.TeamId != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardHero;
                        dragHandler.team_id = TeamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = Position;
                        dragHandler.positionPanel = PositionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_HERO, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        };

                        CardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_HERO, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                            // CreatePosition("CardHeroes", team, positionPanel, typePanel, level, teamsObject);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardCaptains cardCaptain)
            {
                GameObject cardObject = Instantiate(CardThirdPrefab, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardCaptain.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatterHelper.FormatNumber(cardCaptain.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardCaptain.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardCaptain.Rarity}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardCaptain.TeamId != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardCaptain;
                        dragHandler.team_id = TeamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = Position;
                        dragHandler.positionPanel = PositionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_CAPTAIN, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        };

                        CardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_CAPTAIN, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardColonels cardColonel)
            {
                GameObject cardObject = Instantiate(CardThirdPrefab, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardColonel.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatterHelper.FormatNumber(cardColonel.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardColonel.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardColonel.Rarity}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardColonel.TeamId != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardColonel;
                        dragHandler.team_id = TeamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = Position;
                        dragHandler.positionPanel = PositionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_COLONEL, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        };

                        CardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_COLONEL, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardGenerals cardGeneral)
            {
                GameObject cardObject = Instantiate(CardThirdPrefab, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardGeneral.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatterHelper.FormatNumber(cardGeneral.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardGeneral.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardGeneral.Rarity}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardGeneral.TeamId != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardGeneral;
                        dragHandler.team_id = TeamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = Position;
                        dragHandler.positionPanel = PositionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_GENERAL, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        };

                        CardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_GENERAL, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardAdmirals cardAdmiral)
            {
                GameObject cardObject = Instantiate(CardThirdPrefab, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardAdmiral.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatterHelper.FormatNumber(cardAdmiral.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardAdmiral.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardAdmiral.Rarity}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardAdmiral.TeamId != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardAdmiral;
                        dragHandler.team_id = TeamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = Position;
                        dragHandler.positionPanel = PositionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_ADMIRAL, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        };

                        CardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_ADMIRAL, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardMonsters cardMonster)
            {
                GameObject cardObject = Instantiate(CardThirdPrefab, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardMonster.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatterHelper.FormatNumber(cardMonster.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMonster.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardMonster.Rarity}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardMonster.TeamId != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardMonster;
                        dragHandler.team_id = TeamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = Position;
                        dragHandler.positionPanel = PositionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_MONSTER, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        };

                        CardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_MONSTER, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardMilitaries cardMilitary)
            {
                GameObject cardObject = Instantiate(CardThirdPrefab, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardMilitary.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatterHelper.FormatNumber(cardMilitary.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMilitary.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardMilitary.Rarity}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardMilitary.TeamId != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardMilitary;
                        dragHandler.team_id = TeamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = Position;
                        dragHandler.positionPanel = PositionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_MILITARY, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        };

                        CardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_MILITARY, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardSpells cardSpell)
            {
                GameObject cardObject = Instantiate(CardThirdPrefab, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardSpell.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatterHelper.FormatNumber(cardSpell.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardSpell.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardSpell.Rarity}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardSpell.TeamId != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardSpell;
                        dragHandler.team_id = TeamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = Position;
                        dragHandler.positionPanel = PositionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_SPELL, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        };

                        CardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_SPELL, SelectedOptionName, TeamLimit, TeamOffset, ChoseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
        }
    }
}
