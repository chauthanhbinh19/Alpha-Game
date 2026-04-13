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
    private Transform positionPanel;
    private GameObject CardThirdPrefab;
    private GameObject PopupTeamFirstPrefab;
    private GameObject PopupTeamSecondPrefab;
    private GameObject TeamsPanelPrefab;
    private GameObject TeamsPositionPrefab;
    private GameObject TeamTypePrefab;
    private GameObject TeamSlotSecondPrefab;
    private GameObject TeamSlotFirstPrefab;
    private GameObject RareButtonPrefab;
    private GameObject PositionPrefab;
    private Button closeButton;
    private Button homeButton;
    private TextMeshProUGUI powerText;
    // private int offset;
    private int currentPage;
    private int totalPage;
    private const int PAGE_SIZE = 100;
    private string selectedOptionName;
    private string teamId;
    private int teamNumber;
    private string position;
    private int teamLimit;
    private int teamOffset;
    private Text titleText;
    private string mainType;
    private string subType;
    private int maxMembersInTeamPosition = 10;
    private Transform choseTeam;
    List<CardDragHandler> cardDragHandlers = new List<CardDragHandler>();
    UserCardHeroesService userCardHeroesService;
    UserCardCaptainsService userCardCaptainsService;
    UserCardColonelsService userCardColonelsService;
    UserCardGeneralsService userCardGeneralsService;
    UserCardAdmiralsService userCardAdmiralsService;
    UserCardMonstersService userCardMonstersService;
    UserCardMilitariesService userCardMilitaryService;
    UserCardSpellsService userCardSpellService;
    TeamsService teamsService;
    private string search;
    private string rare;
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
        RareButtonPrefab = UIManager.Instance.Get("RareButtonPrefab");
        PositionPrefab = UIManager.Instance.Get("PositionPrefab");

        userCardHeroesService = UserCardHeroesService.Create();
        userCardCaptainsService = UserCardCaptainsService.Create();
        userCardColonelsService = UserCardColonelsService.Create();
        userCardGeneralsService = UserCardGeneralsService.Create();
        userCardAdmiralsService = UserCardAdmiralsService.Create();
        userCardMonstersService = UserCardMonstersService.Create();
        userCardMilitaryService = UserCardMilitariesService.Create();
        userCardSpellService = UserCardSpellsService.Create();
        teamsService = TeamsService.Create();
        rare = AppConstants.Rare.ALL;
    }

    public async Task CreateTeamsAsync()
    {
        GameObject teamObject = Instantiate(TeamsPanelPrefab, MainPanel);
        Transform transform = teamObject.transform;
        Transform tempLeftContent = transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = transform.Find("ScrollViewRight/Viewport/Content");
        Transform positionTeamsPanel = transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        TextMeshProUGUI teamsTitleText = transform.Find("DictionaryCards/TeamsTitleText").GetComponent<TextMeshProUGUI>();
        closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
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
                    userCardHeroesService.GetUserCardHeroesTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    userCardCaptainsService.GetUserCardCaptainsTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    userCardColonelsService.GetUserCardColonelsTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    userCardGeneralsService.GetUserCardGeneralsTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    userCardAdmiralsService.GetUserCardAdmiralsTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    userCardMonstersService.GetUserCardMonstersTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    userCardMilitaryService.GetUserCardMilitariesTeamsCountAsync(User.CurrentUserId, team.TeamId),
                    userCardSpellService.GetUserCardSpellsTeamsCountAsync(User.CurrentUserId, team.TeamId)
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
            "UI/Background4/Background_V4_438", "UI/Background4/Background_V4_439",
            "UI/Background4/Background_V4_438", "UI/Background4/Background_V4_439",
            "UI/Background4/Background_V4_438", "UI/Background4/Background_V4_439",
            "UI/Background4/Background_V4_438", "UI/Background4/Background_V4_439"
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
                int tempTeamNumber = team.TeamNumber;
                transform.Find("ChangeCardButton").GetComponent<Button>().onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    this.teamId = tempTeamId;
                    this.teamNumber = tempTeamNumber;
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
        GameObject teamObject = Instantiate(PopupTeamFirstPrefab, MainPanel);
        titleText = teamObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        // ScrollRect scrollRect = teamObject.transform.Find("DictionaryCards/ScrollViewPosition").GetComponent<ScrollRect>();
        Transform teamSlotPanel = teamObject.transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        Transform tempLeftContent = teamObject.transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = teamObject.transform.Find("ScrollViewRight/Viewport/Content");
        closeButton = teamObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(teamObject);
            await CreateTeamsAsync();
        });
        homeButton = teamObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        TextMeshProUGUI numberText = teamObject.transform.Find("DictionaryCards/TeamPanel/NumberText").GetComponent<TextMeshProUGUI>();
        numberText.text = teamNumber.ToString();

        _ = CreateSlotAsync(teamSlotPanel);
    }
    public async Task CreateSlotAsync(Transform slotPanel)
    {
        ButtonEvent.Instance.Close(slotPanel);

        var taskCardHero = userCardHeroesService.GetUserCardHeroesTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        var taskCardCaptain = userCardCaptainsService.GetUserCardCaptainsTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        var taskCardColonel = userCardColonelsService.GetUserCardColonelsTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        var taskCardGeneral = userCardGeneralsService.GetUserCardGeneralsTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        var taskCardAdmiral = userCardAdmiralsService.GetUserCardAdmiralsTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        var taskCardMonster = userCardMonstersService.GetUserCardMonstersTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        var taskCardMilitary = userCardMilitaryService.GetUserCardMilitariesTeamWithoutPositionAsync(User.CurrentUserId, teamId);
        var taskCardSpell = userCardSpellService.GetUserCardSpellsTeamWithoutPositionAsync(User.CurrentUserId, teamId);

        await Task.WhenAll(taskCardHero, taskCardCaptain, taskCardColonel, taskCardGeneral, taskCardAdmiral, taskCardMonster, taskCardMilitary, taskCardSpell);

        List<CardHeroes> cardHeroList = await taskCardHero;
        List<CardCaptains> cardCaptainList = await taskCardCaptain;
        List<CardColonels> cardColonelList = await taskCardColonel;
        List<CardGenerals> cardGeneralList = await taskCardGeneral;
        List<CardAdmirals> cardAdmiralList = await taskCardAdmiral;
        List<CardMonsters> cardMonsterList = await taskCardMonster;
        List<CardMilitaries> cardMilitaryList = await taskCardMilitary;
        List<CardSpells> cardSpellList = await taskCardSpell;

        for (int i = 1; i <= 10; i++)
        {
            GameObject teamSlotComponentObject = Instantiate(TeamSlotFirstPrefab, slotPanel);
            Transform transform = teamSlotComponentObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

            Button cardHeroButton = transform.Find("ButtonGroup/CardHeroButton").GetComponent<Button>();
            Button cardCaptainButton = transform.Find("ButtonGroup/CardCaptainButton").GetComponent<Button>();
            Button cardColonelButton = transform.Find("ButtonGroup/CardColonelButton").GetComponent<Button>();
            Button cardGeneralButton = transform.Find("ButtonGroup/CardGeneralButton").GetComponent<Button>();
            Button cardAdmiralButton = transform.Find("ButtonGroup/CardAdmiralButton").GetComponent<Button>();
            Button cardMonsterButton = transform.Find("ButtonGroup/CardMonsterButton").GetComponent<Button>();
            Button cardMilitaryButton = transform.Find("ButtonGroup/CardMilitaryButton").GetComponent<Button>();
            Button cardSpellButton = transform.Find("ButtonGroup/CardSpellButton").GetComponent<Button>();

            int slotIndex = i;
            cardHeroButton.onClick.AddListener(() => OnCardClick(slotIndex, AppConstants.MainType.CARD_HERO));
            cardCaptainButton.onClick.AddListener(() => OnCardClick(slotIndex, AppConstants.MainType.CARD_CAPTAIN));
            cardColonelButton.onClick.AddListener(() => OnCardClick(slotIndex, AppConstants.MainType.CARD_COLONEL));
            cardGeneralButton.onClick.AddListener(() => OnCardClick(slotIndex, AppConstants.MainType.CARD_GENERAL));
            cardAdmiralButton.onClick.AddListener(() => OnCardClick(slotIndex, AppConstants.MainType.CARD_ADMIRAL));
            cardMonsterButton.onClick.AddListener(() => OnCardClick(slotIndex, AppConstants.MainType.CARD_MONSTER));
            cardMilitaryButton.onClick.AddListener(() => OnCardClick(slotIndex, AppConstants.MainType.CARD_MILITARY));
            cardSpellButton.onClick.AddListener(() => OnCardClick(slotIndex, AppConstants.MainType.CARD_SPELL));

            titleText.text = "Slot " + i.ToString();
            quantityText.text = i.ToString();

            // Tối ưu bằng cách lấy Group Transform một lần
            Transform process = transform.Find("Process");
            string slotPrefix = $"{i}-";

            // 2. Cập nhật thủ công từng thanh để tránh dùng 'dynamic' gây lỗi CS0656
            UpdateBarManual(process.Find("CardHeroBar"), cardHeroList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.CARD_HERO_COLOR);
            UpdateBarManual(process.Find("CardCaptainBar"), cardCaptainList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.CARD_CAPTAIN_COLOR);
            UpdateBarManual(process.Find("CardColonelBar"), cardColonelList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.CARD_COLONEL_COLOR);
            UpdateBarManual(process.Find("CardGeneralBar"), cardGeneralList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.CARD_GENERAL_COLOR);
            UpdateBarManual(process.Find("CardAdmiralBar"), cardAdmiralList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.CARD_ADMIRAL_COLOR);
            UpdateBarManual(process.Find("CardMonsterBar"), cardMonsterList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.CARD_MONSTER_COLOR);
            UpdateBarManual(process.Find("CardMilitaryBar"), cardMilitaryList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.CARD_MILITARY_COLOR);
            UpdateBarManual(process.Find("CardSpellBar"), cardSpellList.Count(c => c.Position?.StartsWith(slotPrefix) == true), ColorConstants.CARD_SPELL_COLOR);
        }
    }
    // Hàm hỗ trợ để không phải viết lặp đi lặp lại GetComponent
    // Hàm này chỉ nhận giá trị đã tính toán xong, không cần biết kiểu dữ liệu Card là gì
    private void UpdateBarManual(Transform barTransform, int count, string colorHex)
    {
        Slider slider = barTransform.Find("Slider").GetComponent<Slider>();
        slider.value = count;
        slider.fillRect.GetComponent<Image>().color = ColorHelper.HexToColor(colorHex);
        barTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>().text = count.ToString();
    }
    public void OnCardClick(int position, string type)
    {
        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        mainType = type;
        _ = CreatePopupTeamSecondPanelAsync(position, type);
    }
    public async Task CreatePopupTeamSecondPanelAsync(int position, string cardType)
    {
        GameObject teamObject = Instantiate(PopupTeamSecondPrefab, MainPanel);
        Transform transform = teamObject.transform;
        titleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
        ScrollRect scrollRect = transform.Find("DictionaryCards/ScrollViewPosition").GetComponent<ScrollRect>();
        positionPanel = transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        Transform tempLeftContent = transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = transform.Find("ScrollViewRight/Viewport/Content");
        closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(teamObject);
            CreatePopupTeamFirstPanel();
        });
        homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI teamNumberText = teamObject.transform.Find("DictionaryCards/TeamPanel/NumberText").GetComponent<TextMeshProUGUI>();
        teamNumberText.text = teamNumber.ToString();
        TextMeshProUGUI positionNumberText = teamObject.transform.Find("DictionaryCards/PositionPanel/NumberText").GetComponent<TextMeshProUGUI>();
        positionNumberText.text = position.ToString();
        TextMeshProUGUI cardTypeText = teamObject.transform.Find("DictionaryCards/CardTypePanel/TypeText").GetComponent<TextMeshProUGUI>();
        TMP_Dropdown dropdownType = transform.Find("DictionaryCards/DropdownType").GetComponent<TMP_Dropdown>();
        TextMeshProUGUI teamTitleText = transform.Find("DictionaryCards/TeamTitleText").GetComponent<TextMeshProUGUI>();
        powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();

        teamLimit = 10;
        teamOffset = 0;

        var cardListObject = new List<object>();

        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            List<CardHeroes> cardHeroList = await userCardHeroesService.GetUserCardHeroesTeamAsync(User.CurrentUserId, teamId, position.ToString());
            cardListObject = cardHeroList.Cast<object>().ToList();
            cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HERO);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> cardCaptainList = await userCardCaptainsService.GetUserCardCaptainsTeamAsync(User.CurrentUserId, teamId, position.ToString());
            cardListObject = cardCaptainList.Cast<object>().ToList();
            cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonelList = await userCardColonelsService.GetUserCardColonelsTeamAsync(User.CurrentUserId, teamId, position.ToString());
            cardListObject = cardColonelList.Cast<object>().ToList();
            cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_COLONEL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGeneralList = await userCardGeneralsService.GetUserCardGeneralsTeamAsync(User.CurrentUserId, teamId, position.ToString());
            cardListObject = cardGeneralList.Cast<object>().ToList();
            cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERAL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmiralList = await userCardAdmiralsService.GetUserCardAdmiralsTeamAsync(User.CurrentUserId, teamId, position.ToString());
            cardListObject = cardAdmiralList.Cast<object>().ToList();
            cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonsterList = await userCardMonstersService.GetUserCardMonstersTeamAsync(User.CurrentUserId, teamId, position.ToString());
            cardListObject = cardMonsterList.Cast<object>().ToList();
            cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MONSTER);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaryList = await userCardMilitaryService.GetUserCardMilitariesTeamAsync(User.CurrentUserId, teamId, position.ToString());
            cardListObject = cardMilitaryList.Cast<object>().ToList();
            cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MILITARY);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpellList = await userCardSpellService.GetUserCardSpellsTeamAsync(User.CurrentUserId, teamId, position.ToString());
            cardListObject = cardSpellList.Cast<object>().ToList();
            cardTypeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_SPELL);
        }

        for (int i = 1; i <= 10; i++)
        {
            GameObject teamSlotSecondObject = Instantiate(TeamSlotSecondPrefab, positionPanel);
            Transform teamSlotSecondTransform = teamSlotSecondObject.transform;

            RawImage cardImage = teamSlotSecondTransform.Find("CardImage").GetComponent<RawImage>();
            TextMeshProUGUI positionText = teamSlotSecondTransform.Find("PositionText").GetComponent<TextMeshProUGUI>();

            int tempPosition = i;
            positionText.text = tempPosition.ToString();

            var card = cardListObject.FirstOrDefault(c =>
            {
                var pos = c.GetType().GetProperty("Position")?.GetValue(c)?.ToString();
                return GetY(pos) == i;
            });

            if (card != null)
            {
                var imagePath = card.GetType().GetProperty("Image")?.GetValue(card)?.ToString();

                Texture texture = Resources.Load<Texture>(imagePath);

                if (texture != null)
                {
                    cardImage.texture = texture;
                }
            }
        }
    }
    int GetY(string position)
    {
        if (string.IsNullOrEmpty(position)) return -1;

        var parts = position.Split('-');
        if (parts.Length < 2) return -1;

        return int.TryParse(parts[1], out int y) ? y : -1;
    }
    public void CreateButton(int index, string itemName, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(RareButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán tên cho itemName
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = itemName;
        }
    }
    public async Task GetTeamsTypeAsync(string type, TMP_Dropdown dropdownType, Transform panel, Text pageText, int team_limit, Action<int> onOffsetUpdated, Action<int> onCurrentPageUpdated)
    {
        List<string> uniqueTypes = await TypeManager.GetUniqueTypesAsync(type);
        // Xóa các callback cũ của dropdown
        dropdownType.onValueChanged.RemoveAllListeners();
        DropdownManager.PopulateDropdown(dropdownType, uniqueTypes, async index =>
    {
        selectedOptionName = dropdownType.options[index].text;
        int team_offset = 0;
        int page = 1;

        if (type.Equals(AppConstants.MainType.CARD_HERO))
        {
            List<CardHeroes> cardHeroList = await userCardHeroesService.GetUserCardHeroesAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardHeroList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = await userCardHeroesService.GetUserCardHeroesCountAsync(User.CurrentUserId, search, selectedOptionName, rare);
            totalPage = PageHelper.CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> cardCaptainList = await userCardCaptainsService.GetUserCardCaptainsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardCaptainList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = await userCardCaptainsService.GetUserCardCaptainsCountAsync(User.CurrentUserId, search, selectedOptionName, rare);
            totalPage = PageHelper.CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonelList = await userCardColonelsService.GetUserCardColonelsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardColonelList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = await userCardColonelsService.GetUserCardColonelsCountAsync(User.CurrentUserId, search, selectedOptionName, rare);
            totalPage = PageHelper.CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGeneralList = await userCardGeneralsService.GetUserCardGeneralsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardGeneralList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = await userCardGeneralsService.GetUserCardGeneralsCountAsync(User.CurrentUserId, search, selectedOptionName, rare);
            totalPage = PageHelper.CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmiralList = await userCardAdmiralsService.GetUserCardAdmiralsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardAdmiralList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = await userCardAdmiralsService.GetUserCardAdmiralsCountAsync(User.CurrentUserId, search, selectedOptionName, rare);
            totalPage = PageHelper.CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonsterList = await userCardMonstersService.GetUserCardMonstersAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardMonsterList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = await userCardMonstersService.GetUserCardMonstersCountAsync(User.CurrentUserId, search, selectedOptionName, rare);
            totalPage = PageHelper.CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaryList = await userCardMilitaryService.GetUserCardMilitariesAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardMilitaryList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = await userCardMilitaryService.GetUserCardMilitariesCountAsync(User.CurrentUserId, search, selectedOptionName, rare);
            totalPage = PageHelper.CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpellList = await userCardSpellService.GetUserCardSpellsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardSpellList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = await userCardSpellService.GetUserCardSpellsCountAsync(User.CurrentUserId, search, selectedOptionName, rare);
            totalPage = PageHelper.CalculateTotalPages(totalRecord, team_limit);
        }
        onOffsetUpdated?.Invoke(team_offset);
        onCurrentPageUpdated?.Invoke(page);
        pageText.text = currentPage.ToString() + "/" + totalPage.ToString();
    });
    }
    public void GetTeamsButton(GameObject clickedButton, Transform panel)
    {
        foreach (Transform child in panel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ChangeButtonBackground(button.gameObject, "Background_V4_167");
            }
        }
        ChangeButtonBackground(clickedButton, "Background_V4_166");
    }
    public async Task LoadCardDataByTypeAsync(string type, string selectedOptionName, int team_limit, int team_offset, Transform choseTeam)
    {
        List<object> cardObjects = null;

        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                List<CardHeroes> cardHeroList = await userCardHeroesService.GetUserCardHeroesAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardHeroList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_CAPTAIN:
                List<CardCaptains> cardCaptainList = await userCardCaptainsService.GetUserCardCaptainsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardCaptainList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_COLONEL:
                List<CardColonels> cardColonelList = await userCardColonelsService.GetUserCardColonelsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardColonelList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_GENERAL:
                List<CardGenerals> cardGeneralList = await userCardGeneralsService.GetUserCardGeneralsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardGeneralList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_ADMIRAL:
                List<CardAdmirals> cardAdmiralList = await userCardAdmiralsService.GetUserCardAdmiralsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardAdmiralList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_MONSTER:
                List<CardMonsters> cardMonsterList = await userCardMonstersService.GetUserCardMonstersAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardMonsterList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_MILITARY:
                List<CardMilitaries> cardMilitaryList = await userCardMilitaryService.GetUserCardMilitariesAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardMilitaryList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_SPELL:
                List<CardSpells> cardSpellList = await userCardSpellService.GetUserCardSpellsAsync(User.CurrentUserId, search, selectedOptionName, team_limit, team_offset, rare);
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
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            double totalPower = 0;
            List<CardHeroes> cardHeroList = await userCardHeroesService.GetUserCardHeroesTeamAsync(User.CurrentUserId, teamId, position);
            cardHeroList = cardHeroList
                .Where(cardHero => cardHero.TeamId.Equals(teamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < maxMembersInTeamPosition; i++)
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardHero.Image);
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
                    await userCardHeroesService.UpdateTeamCardHeroAsync(null, null, matchingCardHero.Id);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardHero.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.position_id = tempPositionId.ToString();
                    if (matchingCardHero != null)
                    {
                        dropHandler.card_id = matchingCardHero.Id;
                        dropHandler.card_power = matchingCardHero.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            double totalPower = 0;
            List<CardCaptains> cardCaptainList = await userCardCaptainsService.GetUserCardCaptainsTeamAsync(User.CurrentUserId, teamId, position);
            cardCaptainList = cardCaptainList
                .Where(cardHero => cardHero.TeamId.Equals(teamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < maxMembersInTeamPosition; i++)
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardCaptain.Image);
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
                    await userCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, matchingCardCaptain.Id);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardCaptain.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.position_id = tempPositionId.ToString();
                    if (matchingCardCaptain != null)
                    {
                        dropHandler.card_id = matchingCardCaptain.Id;
                        dropHandler.card_power = matchingCardCaptain.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            double totalPower = 0;
            List<CardColonels> cardColonelList = await userCardColonelsService.GetUserCardColonelsTeamAsync(User.CurrentUserId, teamId, position);
            cardColonelList = cardColonelList
                .Where(cardHero => cardHero.TeamId.Equals(teamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < maxMembersInTeamPosition; i++)
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardColonel.Image);
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
                    await userCardColonelsService.UpdateTeamCardColonelAsync(null, null, matchingCardColonel.Id);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardColonel.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.position_id = tempPositionId.ToString();
                    if (matchingCardColonel != null)
                    {
                        dropHandler.card_id = matchingCardColonel.Id;
                        dropHandler.card_power = matchingCardColonel.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            double totalPower = 0;
            List<CardGenerals> cardGeneralList = await userCardGeneralsService.GetUserCardGeneralsTeamAsync(User.CurrentUserId, teamId, position);
            cardGeneralList = cardGeneralList
                .Where(cardHero => cardHero.TeamId.Equals(teamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < maxMembersInTeamPosition; i++)
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardGeneral.Image);
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
                    await userCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, matchingCardGeneral.Id);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardGeneral.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.position_id = tempPositionId.ToString();
                    if (matchingCardGeneral != null)
                    {
                        dropHandler.card_id = matchingCardGeneral.Id;
                        dropHandler.card_power = matchingCardGeneral.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            double totalPower = 0;
            List<CardAdmirals> cardAdmiralList = await userCardAdmiralsService.GetUserCardAdmiralsTeamAsync(User.CurrentUserId, teamId, position);
            cardAdmiralList = cardAdmiralList
                .Where(cardHero => cardHero.TeamId.Equals(teamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < maxMembersInTeamPosition; i++)
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardAdmiral.Image);
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
                    await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, matchingCardAdmiral.Id);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardAdmiral.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.position_id = tempPositionId.ToString();
                    if (matchingCardAdmiral != null)
                    {
                        dropHandler.card_id = matchingCardAdmiral.Id;
                        dropHandler.card_power = matchingCardAdmiral.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            double totalPower = 0;
            List<CardMonsters> cardMonsterList = await userCardMonstersService.GetUserCardMonstersTeamAsync(User.CurrentUserId, teamId, position);
            cardMonsterList = cardMonsterList
                .Where(cardHero => cardHero.TeamId.Equals(teamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < maxMembersInTeamPosition; i++)
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardMonster.Image);
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
                    await userCardMonstersService.UpdateTeamCardMonsterAsync(null, null, matchingCardMonster.Id);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardMonster.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.position_id = tempPositionId.ToString();
                    if (matchingCardMonster != null)
                    {
                        dropHandler.card_id = matchingCardMonster.Id;
                        dropHandler.card_power = matchingCardMonster.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            double totalPower = 0;
            List<CardMilitaries> cardMilitaryList = await userCardMilitaryService.GetUserCardMilitariesTeamAsync(User.CurrentUserId, teamId, position);
            cardMilitaryList = cardMilitaryList
                .Where(cardHero => cardHero.TeamId.Equals(teamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < maxMembersInTeamPosition; i++)
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardMilitary.Image);
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
                    await userCardMilitaryService.UpdateTeamCardMilitaryAsync(null, null, matchingCardMilitary.Id);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardMilitary.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.position_id = tempPositionId.ToString();
                    if (matchingCardMilitary != null)
                    {
                        dropHandler.card_id = matchingCardMilitary.Id;
                        dropHandler.card_power = matchingCardMilitary.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            double totalPower = 0;
            List<CardSpells> cardSpellList = await userCardSpellService.GetUserCardSpellsTeamAsync(User.CurrentUserId, teamId, position);
            cardSpellList = cardSpellList
                .Where(cardHero => cardHero.TeamId.Equals(teamId)) // Lọc theo team_id
                .ToList();
            int count = 0;
            for (int i = 0; i < maxMembersInTeamPosition; i++)
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardSpell.Image);
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
                    await userCardSpellService.UpdateTeamCardSpellAsync(null, null, matchingCardSpell.Id);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, matchingCardSpell.Power, 0);
                    await CreatePositionAsync(positionPanel, teamsObject);
                    await LoadCardDataByTypeAsync(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                });

                // Kiểm tra và gán CardDropHandler
                if (positionObject.GetComponent<CardDropHandler>() == null)
                {
                    CardDropHandler dropHandler = positionObject.AddComponent<CardDropHandler>();
                    dropHandler.teamsObject = teamsObject;
                    int tempPositionId = i + 1;
                    dropHandler.position_id = tempPositionId.ToString();
                    if (matchingCardSpell != null)
                    {
                        dropHandler.card_id = matchingCardSpell.Id;
                        dropHandler.card_power = matchingCardSpell.Power;
                    }
                    dropHandler.OnDropEnd = async () =>
                    {
                        await CreatePositionAsync(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
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
                Power.text = NumberFormatter.FormatNumber(cardHero.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardHero.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardHero.Rare}");
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_HERO, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_HERO, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardCaptain.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardCaptain.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardCaptain.Rare}");
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_CAPTAIN, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_CAPTAIN, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardColonel.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardColonel.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardColonel.Rare}");
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_COLONEL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_COLONEL, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardGeneral.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardGeneral.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardGeneral.Rare}");
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_GENERAL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_GENERAL, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardAdmiral.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardAdmiral.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardAdmiral.Rare}");
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_ADMIRAL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_ADMIRAL, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardMonster.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMonster.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardMonster.Rare}");
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_MONSTER, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_MONSTER, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardMilitary.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardMilitary.Rare}");
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_MILITARY, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_MILITARY, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardSpell.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardSpell.Image);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardSpell.Rare}");
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = async () =>
                        {
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_SPELL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(async (data) =>
                        {
                            dragHandler.OnCardClicked();
                            await LoadCardDataByTypeAsync(AppConstants.MainType.CARD_SPELL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
        }
    }
    public void UpdateTeamForAllCards(string newTeamId)
    {
        foreach (var dragHandler in cardDragHandlers)
        {
            dragHandler.team_id = newTeamId;
        }
    }
    public async Task InsertCardToTeamAsync(object obj, string position_id, string card_id, string team_id, double card_power)
    {
        string position = position_id;
        double currentPower = User.CurrentUserPower;
        if (obj is CardHeroes cardHero)
        {
            if (!string.IsNullOrEmpty(card_id))
            {
                await userCardHeroesService.UpdateTeamCardHeroAsync(null, null, card_id);
                await userCardHeroesService.UpdateTeamCardHeroAsync(team_id, position, cardHero.Id);
                if (cardHero.Power >= card_power)
                {
                    double diffPower = cardHero.Power - card_power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = card_power - cardHero.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardHeroesService.UpdateTeamCardHeroAsync(team_id, position, cardHero.Id);
                double updatedPower = currentPower + cardHero.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardHero.Power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptain)
        {
            if (!string.IsNullOrEmpty(card_id))
            {
                await userCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, card_id);
                await userCardCaptainsService.UpdateTeamCardCaptainAsync(team_id, position, cardCaptain.Id);
                if (cardCaptain.Power >= card_power)
                {
                    double diffPower = cardCaptain.Power - card_power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = card_power - cardCaptain.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardCaptainsService.UpdateTeamCardCaptainAsync(team_id, position, cardCaptain.Id);
                double updatedPower = currentPower + cardCaptain.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardCaptain.Power, 1);
            }
        }
        else if (obj is CardColonels CardColonel)
        {
            if (!string.IsNullOrEmpty(card_id))
            {
                await userCardColonelsService.UpdateTeamCardColonelAsync(null, null, card_id);
                await userCardColonelsService.UpdateTeamCardColonelAsync(team_id, position, CardColonel.Id);
                if (CardColonel.Power >= card_power)
                {
                    double diffPower = CardColonel.Power - card_power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = card_power - CardColonel.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardColonelsService.UpdateTeamCardColonelAsync(team_id, position, CardColonel.Id);
                double updatedPower = currentPower + CardColonel.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, CardColonel.Power, 1);
            }
        }
        else if (obj is CardGenerals cardGeneral)
        {
            if (!string.IsNullOrEmpty(card_id))
            {
                await userCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, card_id);
                await userCardGeneralsService.UpdateTeamCardGeneralAsync(team_id, position, cardGeneral.Id);
                if (cardGeneral.Power >= card_power)
                {
                    double diffPower = cardGeneral.Power - card_power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = card_power - cardGeneral.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardGeneralsService.UpdateTeamCardGeneralAsync(team_id, position, cardGeneral.Id);
                double updatedPower = currentPower + cardGeneral.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardGeneral.Power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmiral)
        {
            if (!string.IsNullOrEmpty(card_id))
            {
                await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, card_id);
                await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(team_id, position, cardAdmiral.Id);
                if (cardAdmiral.Power >= card_power)
                {
                    double diffPower = cardAdmiral.Power - card_power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = card_power - cardAdmiral.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(team_id, position, cardAdmiral.Id);
                double updatedPower = currentPower + cardAdmiral.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardAdmiral.Power, 1);
            }
        }
        else if (obj is CardMonsters cardMonster)
        {
            if (!string.IsNullOrEmpty(card_id))
            {
                await userCardMonstersService.UpdateTeamCardMonsterAsync(null, null, card_id);
                await userCardMonstersService.UpdateTeamCardMonsterAsync(team_id, position, cardMonster.Id);
                if (cardMonster.Power >= card_power)
                {
                    double diffPower = cardMonster.Power - card_power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = card_power - cardMonster.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardMonstersService.UpdateTeamCardMonsterAsync(team_id, position, cardMonster.Id);
                double updatedPower = currentPower + cardMonster.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardMonster.Power, 1);
            }
        }
        else if (obj is CardMilitaries cardMilitary)
        {
            if (!string.IsNullOrEmpty(card_id))
            {
                await userCardMilitaryService.UpdateTeamCardMilitaryAsync(null, null, card_id);
                await userCardMilitaryService.UpdateTeamCardMilitaryAsync(team_id, position, cardMilitary.Id);
                if (cardMilitary.Power >= card_power)
                {
                    double diffPower = cardMilitary.Power - card_power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = card_power - cardMilitary.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardMilitaryService.UpdateTeamCardMilitaryAsync(team_id, position, cardMilitary.Id);
                double updatedPower = currentPower + cardMilitary.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardMilitary.Power, 1);
            }
        }
        else if (obj is CardSpells cardSpell)
        {
            if (!string.IsNullOrEmpty(card_id))
            {
                await userCardSpellService.UpdateTeamCardSpellAsync(null, null, card_id);
                await userCardSpellService.UpdateTeamCardSpellAsync(team_id, position, cardSpell.Id);
                if (cardSpell.Power >= card_power)
                {
                    double diffPower = cardSpell.Power - card_power;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = card_power - cardSpell.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardSpellService.UpdateTeamCardSpellAsync(team_id, position, cardSpell.Id);
                double updatedPower = currentPower + cardSpell.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardSpell.Power, 1);
            }
        }
    }
    // public void Close(Transform content)
    // {
    //     // offset = 0;
    //     currentPage = 1;
    //     foreach (Transform child in content)
    //     {
    //         Destroy(child.gameObject);
    //     }
    // }
    private void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = TextureHelper.LoadTextureCached($"UI/Background4/{image}");
            if (texture != null)
            {
                buttonImage.texture = texture;
            }
            else
            {
                Debug.LogError($"Texture '{image}' not found in Resources.");
            }
        }
        else
        {
            Debug.LogError("Button does not have a RawImage component.");
        }
    }
}
