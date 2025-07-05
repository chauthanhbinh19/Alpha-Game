using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TeamsManager : MonoBehaviour
{
    public static TeamsManager Instance { get; private set; }
    private Transform MainPanel;
    private Transform positionPanel;
    private GameObject cardsPrefab3;
    private GameObject PopupTeamsPrefab;
    private GameObject TeamsPanelPrefab;
    private GameObject TeamsPositionPrefab;
    private GameObject TypePrefab;
    private GameObject buttonPrefab3;
    private GameObject PositionPrefab;
    private Button CloseButton;
    private Button HomeButton;
    private GameObject buttonPrefab;
    private TextMeshProUGUI powerText;
    // private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private string selectedOptionName;
    private string team_id;
    private string position;
    private int team_limit;
    private int team_offset;
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
    UserCardMilitaryService userCardMilitaryService;
    UserCardSpellService userCardSpellService;
    TeamsService teamsService;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        cardsPrefab3 = UIManager.Instance.GetGameObject("CardsThirdPrefab");
        PopupTeamsPrefab = UIManager.Instance.GetGameObject("PopupTeamsPrefab");
        TeamsPanelPrefab = UIManager.Instance.GetGameObject("TeamsPanelPrefab");
        TeamsPositionPrefab = UIManager.Instance.GetGameObject("TeamsPositionPrefab");
        TypePrefab = UIManager.Instance.GetGameObject("TypePrefab");
        buttonPrefab3 = UIManager.Instance.GetGameObject("TabButton3");
        PositionPrefab = UIManager.Instance.GetGameObject("PositionPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");

        userCardHeroesService = UserCardHeroesService.Create();
        userCardCaptainsService = UserCardCaptainsService.Create();
        userCardColonelsService = UserCardColonelsService.Create();
        userCardGeneralsService = UserCardGeneralsService.Create();
        userCardAdmiralsService = UserCardAdmiralsService.Create();
        userCardMonstersService = UserCardMonstersService.Create();
        userCardMilitaryService = UserCardMilitaryService.Create();
        userCardSpellService = UserCardSpellService.Create();
        teamsService = TeamsService.Create();
    }
    public void CreateTeams()
    {
        GameObject teamsObject = Instantiate(TeamsPanelPrefab, MainPanel);
        Transform tempLeftContent = teamsObject.transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = teamsObject.transform.Find("ScrollViewRight/Viewport/Content");
        Transform positionTeamsPanel = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        TextMeshProUGUI teamsTitleText = teamsObject.transform.Find("DictionaryCards/TeamsTitleText").GetComponent<TextMeshProUGUI>();
        CloseButton = teamsObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            foreach (Transform child in MainPanel)
            {
                Destroy(child.gameObject);
            }
        });
        HomeButton = teamsObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));

        mainType = "CardHeroes";
        team_id = "1";
        teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        CreateTeamsPosition(positionTeamsPanel);
        CreateButton(1, "Card Heroes", tempLeftContent);
        CreateButton(2, "Card Captains", tempLeftContent);
        CreateButton(3, "Card Colonels", tempLeftContent);
        CreateButton(4, "Card Generals", tempLeftContent);
        CreateButton(5, "Card Admirals", tempLeftContent);
        CreateButton(6, "Card Monsters", tempLeftContent);
        CreateButton(7, "Card Military", tempLeftContent);
        CreateButton(8, "Card Spell", tempLeftContent);
        ButtonEvent.Instance.AssignButtonEvent("Button_1", tempLeftContent, () =>
        {
            mainType = "CardHeroes";
            CreateTeamsPosition(positionTeamsPanel);
            teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", tempLeftContent, () =>
        {
            mainType = "CardCaptains";
            CreateTeamsPosition(positionTeamsPanel);
            teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", tempLeftContent, () =>
        {
            mainType = "CardColonels";
            CreateTeamsPosition(positionTeamsPanel);
            teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", tempLeftContent, () =>
        {
            mainType = "CardGenerals";
            CreateTeamsPosition(positionTeamsPanel);
            teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", tempLeftContent, () =>
        {
            mainType = "CardAdmirals";
            CreateTeamsPosition(positionTeamsPanel);
            teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", tempLeftContent, () =>
        {
            mainType = "CardMonsters";
            CreateTeamsPosition(positionTeamsPanel);
            teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", tempLeftContent, () =>
        {
            mainType = "CardMilitary";
            CreateTeamsPosition(positionTeamsPanel);
            teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", tempLeftContent, () =>
        {
            mainType = "CardSpell";
            CreateTeamsPosition(positionTeamsPanel);
            teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
        });
        for (int i = 0; i <= 20; i++)
        {
            GameObject button = Instantiate(buttonPrefab, tempRightContent);
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = "Team " + (i + 1);
            ChangeButtonBackground(button, "Background_V4_167");
            if (i == 0)
            {
                ChangeButtonBackground(button, "Background_V4_166");
            }
            int teamIndex = i + 1;
            Button btn = button.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                team_id = teamIndex.ToString();
                teamsTitleText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString())) + " - Team " + team_id.ToString();
                GetTeamsButton(button, tempRightContent);
                CreateTeamsPosition(positionTeamsPanel);
            });
        }
    }
    public void CreateTeamsPosition(Transform positionTeamsPanel)
    {
        Close(positionTeamsPanel);
        for (int i = 1; i <= 10; i++)
        {
            GameObject cardTeam = Instantiate(TeamsPositionPrefab, positionTeamsPanel);
            RawImage cardImage = cardTeam.transform.Find("CardImage").GetComponent<RawImage>();
            TextMeshProUGUI teamsPositionText = cardTeam.transform.Find("PositionText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI teamsContentText = cardTeam.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
            teamsPositionText.text = i.ToString();
            int positionNumber = 0;
            if (mainType.Equals("CardHeroes"))
            {
                positionNumber = userCardHeroesService.GetUserCardHeroesTeamsPositionCount(User.CurrentUserId, team_id, i.ToString());
            }
            else if (mainType.Equals("CardCaptains"))
            {
                positionNumber = userCardCaptainsService.GetUserCardCaptainsTeamsPositionCount(User.CurrentUserId, team_id, i.ToString());
            }
            else if (mainType.Equals("CardColonels"))
            {
                positionNumber = userCardColonelsService.GetUserCardColonelsTeamsPositionCount(User.CurrentUserId, team_id, i.ToString());
            }
            else if (mainType.Equals("CardGenerals"))
            {
                positionNumber = userCardGeneralsService.GetUserCardGeneralsTeamsPositionCount(User.CurrentUserId, team_id, i.ToString());
            }
            else if (mainType.Equals("CardAdmirals"))
            {
                positionNumber = userCardAdmiralsService.GetUserCardAdmiralsTeamsPositionCount(User.CurrentUserId, team_id, i.ToString());
            }
            else if (mainType.Equals("CardMonsters"))
            {
                positionNumber = userCardMonstersService.GetUserCardMonstersTeamsPositionCount(User.CurrentUserId, team_id, i.ToString());
            }
            else if (mainType.Equals("CardMilitary"))
            {
                positionNumber = userCardMilitaryService.GetUserCardMilitaryTeamsPositionCount(User.CurrentUserId, team_id, i.ToString());
            }
            else if (mainType.Equals("CardSpell"))
            {
                positionNumber = userCardSpellService.GetUserCardSpellTeamsPositionCount(User.CurrentUserId, team_id, i.ToString());
            }
            teamsContentText.text = positionNumber.ToString() + "/10";

            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = cardImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = cardImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            int index = i;
            ButtonEvent.Instance.AddClickListener(eventTrigger, () =>
            {
                position = index.ToString();
                CreatePopupTeams();
            });
        }
    }
    public void CreatePopupTeams()
    {
        GameObject teamsObject = Instantiate(PopupTeamsPrefab, MainPanel);
        titleText = teamsObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        ScrollRect scrollRect = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition").GetComponent<ScrollRect>();
        positionPanel = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        CloseButton = teamsObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            Close(MainPanel);
            CreateTeams();
        });
        HomeButton = teamsObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        // RawImage arrowUp = teamsObject.transform.Find("DictionaryCards/ScrollViewArrowUp").GetComponent<RawImage>();
        // RawImage arrowDown = teamsObject.transform.Find("DictionaryCards/ScrollViewArrowDown").GetComponent<RawImage>();
        TMP_Dropdown dropdownType = teamsObject.transform.Find("DictionaryCards/DropdownType").GetComponent<TMP_Dropdown>();
        TextMeshProUGUI typeText = teamsObject.transform.Find("DictionaryCards/TypeText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI teamTitleText = teamsObject.transform.Find("DictionaryCards/TeamTitleText").GetComponent<TextMeshProUGUI>();
        powerText = teamsObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        choseTeam = teamsObject.transform.Find("DictionaryCards/ChoseTeam");
        Button nextButton = teamsObject.transform.Find("DictionaryCards/NextButton").GetComponent<Button>();
        Button previousButton = teamsObject.transform.Find("DictionaryCards/PreviousButton").GetComponent<Button>();
        Text pageText = teamsObject.transform.Find("Pagination/Page").GetComponent<Text>();

        team_limit = 10;
        team_offset = 0;
        int page = 1;
        team_id = "1";
        User user = new User();
        user = UserService.Create().GetUserById(User.CurrentUserId);
        CardHeroes cardHeroes = new CardHeroes();
        List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroes(User.CurrentUserId, "Adamas", team_limit, team_offset);
        // Gọi script quản lý cuộn
        // ScrollManager scrollManager = teamsObject.AddComponent<ScrollManager>();
        // scrollManager.Initialize(scrollRect, arrowUp, arrowDown);
        // Thêm sự kiện OnScroll vào ScrollRect
        // scrollRect.onValueChanged.AddListener((Vector2 position) =>
        // {
        //     scrollManager.UpdateArrows(); // Cập nhật mũi tên khi cuộn
        // });
        GetTeamsType(mainType, dropdownType, choseTeam, pageText, team_limit, newOffset =>
        {
            team_offset = newOffset;
        }, newCurrentPage =>
        {
            page = newCurrentPage;
        });
        typeText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));
        List<object> cardObjects = cardHeroesList.Cast<object>().ToList();
        CreatePosition(positionPanel, user.level, teamsObject);
        createCardTeams(cardObjects, choseTeam);
        selectedOptionName = dropdownType.options[dropdownType.value].text;
        int totalRecord = userCardHeroesService.GetUserCardHeroesCount(User.CurrentUserId, selectedOptionName);
        totalPage = CalculateTotalPages(totalRecord, team_limit);
        pageText.text = page.ToString() + "/" + totalPage.ToString();

        nextButton.onClick.AddListener(() =>
        {
            if (page < totalPage)
            {
                team_offset = team_offset + team_limit;
                page = page + 1;
                LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
                pageText.text = page.ToString() + "/" + totalPage.ToString();
            }
        });
        previousButton.onClick.AddListener(() =>
        {
            if (page > 1)
            {
                team_offset = team_offset - team_limit;
                page = page - 1;
                LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
                pageText.text = page.ToString() + "/" + totalPage.ToString();
            }
        });
    }
    private void CreateButton(int index, string itemName, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(buttonPrefab3, panel);
        newButton.name = "Button_" + index;

        // Gán tên cho itemName
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = itemName;
        }
    }
    public void GetTeamsType(string type, TMP_Dropdown dropdownType, Transform panel, Text pageText, int team_limit, Action<int> onOffsetUpdated, Action<int> onCurrentPageUpdated)
    {
        List<string> uniqueTypes = TypeManager.GetUniqueTypes(type);
        // Xóa các callback cũ của dropdown
        dropdownType.onValueChanged.RemoveAllListeners();
        DropdownManager.PopulateDropdown(dropdownType, uniqueTypes, index =>
    {
        selectedOptionName = dropdownType.options[index].text;
        int team_offset = 0;
        int page = 1;

        if (type.Equals("CardHeroes"))
        {
            List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroes(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardHeroesList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = userCardHeroesService.GetUserCardHeroesCount(User.CurrentUserId, selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardCaptains"))
        {
            List<CardCaptains> cardCaptainsList = userCardCaptainsService.GetUserCardCaptains(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardCaptainsList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = userCardCaptainsService.GetUserCardCaptainsCount(User.CurrentUserId, selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardColonels"))
        {
            List<CardColonels> cardColonelsList = userCardColonelsService.GetUserCardColonels(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardColonelsList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = userCardColonelsService.GetUserCardColonelsCount(User.CurrentUserId, selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardGenerals"))
        {
            List<CardGenerals> cardGeneralsList = userCardGeneralsService.GetUserCardGenerals(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardGeneralsList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = userCardGeneralsService.GetUserCardGeneralsCount(User.CurrentUserId, selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardAdmirals"))
        {
            List<CardAdmirals> cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmirals(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardAdmiralsList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = userCardAdmiralsService.GetUserCardAdmiralsCount(User.CurrentUserId, selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardMonsters"))
        {
            List<CardMonsters> cardMonstersList = userCardMonstersService.GetUserCardMonsters(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardMonstersList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = userCardMonstersService.GetUserCardMonstersCount(User.CurrentUserId, selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardMilitary"))
        {
            List<CardMilitary> cardMilitaryList = userCardMilitaryService.GetUserCardMilitary(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardMilitaryList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = userCardMilitaryService.GetUserCardMilitaryCount(User.CurrentUserId, selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals("CardSpell"))
        {
            List<CardSpell> cardSpellList = userCardSpellService.GetUserCardSpell(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
            List<object> cardObjects = cardSpellList.Cast<object>().ToList();
            createCardTeams(cardObjects, panel);
            int totalRecord = userCardSpellService.GetUserCardSpellCount(User.CurrentUserId, selectedOptionName);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
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
    private void LoadCardDataByType(string type, string selectedOptionName, int team_limit, int team_offset, Transform choseTeam)
    {
        List<object> cardObjects = null;

        switch (type)
        {
            case "CardHeroes":
                List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroes(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
                cardObjects = cardHeroesList.Cast<object>().ToList();
                break;

            case "CardCaptains":
                List<CardCaptains> cardCaptainsList = userCardCaptainsService.GetUserCardCaptains(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
                cardObjects = cardCaptainsList.Cast<object>().ToList();
                break;

            case "CardColonels":
                List<CardColonels> cardColonelsList = userCardColonelsService.GetUserCardColonels(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
                cardObjects = cardColonelsList.Cast<object>().ToList();
                break;

            case "CardGenerals":
                List<CardGenerals> cardGeneralsList = userCardGeneralsService.GetUserCardGenerals(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
                cardObjects = cardGeneralsList.Cast<object>().ToList();
                break;

            case "CardAdmirals":
                List<CardAdmirals> cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmirals(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
                cardObjects = cardAdmiralsList.Cast<object>().ToList();
                break;

            case "CardMonsters":
                List<CardMonsters> cardMonstersList = userCardMonstersService.GetUserCardMonsters(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
                cardObjects = cardMonstersList.Cast<object>().ToList();
                break;

            case "CardMilitary":
                List<CardMilitary> cardMilitaryList = userCardMilitaryService.GetUserCardMilitary(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
                cardObjects = cardMilitaryList.Cast<object>().ToList();
                break;

            case "CardSpell":
                List<CardSpell> cardSpellList = userCardSpellService.GetUserCardSpell(User.CurrentUserId, selectedOptionName, team_limit, team_offset);
                cardObjects = cardSpellList.Cast<object>().ToList();
                break;

            default:
                Debug.LogWarning("Unknown type: " + type);
                break;
        }

        if (cardObjects != null)
        {
            createCardTeams(cardObjects, choseTeam);
        }
    }
    public void CreatePosition(Transform positionPanel, int level, GameObject teamsObject)
    {
        foreach (Transform child in positionPanel)
        {
            Destroy(child.gameObject); // Hoặc DestroyImmediate(child.gameObject) nếu cần xóa ngay lập tức
        }
        if (mainType.Equals("CardHeroes"))
        {
            double totalPower = 0;
            List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroesTeam(User.CurrentUserId, team_id, position);
            cardHeroesList = cardHeroesList
                .Where(cardHero => cardHero.team_id.Equals(team_id)) // Lọc theo team_id
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
                CardHeroes matchingCardHero = cardHeroesList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardHero != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardHero.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardHero.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardHeroesService.UpdateTeamFactCardHeroes(null, null, matchingCardHero.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardHero.all_power, 0);
                    CreatePosition(positionPanel, level, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
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
                        dropHandler.card_id = matchingCardHero.id;
                        dropHandler.card_all_power = matchingCardHero.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = totalPower.ToString();
        }
        else if (mainType.Equals("CardCaptains"))
        {
            double totalPower = 0;
            List<CardCaptains> cardCaptainsList = userCardCaptainsService.GetUserCardCaptainsTeam(User.CurrentUserId, team_id, position);
            cardCaptainsList = cardCaptainsList
                .Where(cardHero => cardHero.team_id.Equals(team_id)) // Lọc theo team_id
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
                CardCaptains matchingCardCaptain = cardCaptainsList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardCaptain != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardCaptain.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardCaptain.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardCaptainsService.UpdateTeamFactCardCaptains(null, null, matchingCardCaptain.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardCaptain.all_power, 0);
                    CreatePosition(positionPanel, level, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
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
                        dropHandler.card_id = matchingCardCaptain.id;
                        dropHandler.card_all_power = matchingCardCaptain.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = totalPower.ToString();
        }
        else if (mainType.Equals("CardColonels"))
        {
            double totalPower = 0;
            List<CardColonels> cardColonelsList = userCardColonelsService.GetUserCardColonelsTeam(User.CurrentUserId, team_id, position);
            cardColonelsList = cardColonelsList
                .Where(cardHero => cardHero.team_id.Equals(team_id)) // Lọc theo team_id
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
                CardColonels matchingCardColonel = cardColonelsList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardColonel != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardColonel.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardColonel.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardColonelsService.UpdateTeamFactCardColonels(null, null, matchingCardColonel.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardColonel.all_power, 0);
                    CreatePosition(positionPanel, level, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
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
                        dropHandler.card_id = matchingCardColonel.id;
                        dropHandler.card_all_power = matchingCardColonel.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = totalPower.ToString();
        }
        else if (mainType.Equals("CardGenerals"))
        {
            double totalPower = 0;
            List<CardGenerals> cardGeneralsList = userCardGeneralsService.GetUserCardGeneralsTeam(User.CurrentUserId, team_id, position);
            cardGeneralsList = cardGeneralsList
                .Where(cardHero => cardHero.team_id.Equals(team_id)) // Lọc theo team_id
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
                CardGenerals matchingCardGeneral = cardGeneralsList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardGeneral != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardGeneral.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardGeneral.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardGeneralsService.UpdateTeamFactCardGenerals(null, null, matchingCardGeneral.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardGeneral.all_power, 0);
                    CreatePosition(positionPanel, level, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
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
                        dropHandler.card_id = matchingCardGeneral.id;
                        dropHandler.card_all_power = matchingCardGeneral.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = totalPower.ToString();
        }
        else if (mainType.Equals("CardAdmirals"))
        {
            double totalPower = 0;
            List<CardAdmirals> cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmiralsTeam(User.CurrentUserId, team_id, position);
            cardAdmiralsList = cardAdmiralsList
                .Where(cardHero => cardHero.team_id.Equals(team_id)) // Lọc theo team_id
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
                CardAdmirals matchingCardAdmiral = cardAdmiralsList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardAdmiral != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardAdmiral.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardAdmiral.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardAdmiralsService.UpdateTeamFactCardAdmirals(null, null, matchingCardAdmiral.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardAdmiral.all_power, 0);
                    CreatePosition(positionPanel, level, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
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
                        dropHandler.card_id = matchingCardAdmiral.id;
                        dropHandler.card_all_power = matchingCardAdmiral.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = totalPower.ToString();
        }
        else if (mainType.Equals("CardMonsters"))
        {
            double totalPower = 0;
            List<CardMonsters> cardMonstersList = userCardMonstersService.GetUserCardMonstersTeam(User.CurrentUserId, team_id, position);
            cardMonstersList = cardMonstersList
                .Where(cardHero => cardHero.team_id.Equals(team_id)) // Lọc theo team_id
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
                CardMonsters matchingCardMonster = cardMonstersList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardMonster != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardMonster.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardMonster.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardMonstersService.UpdateTeamFactCardMonsters(null, null, matchingCardMonster.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardMonster.all_power, 0);
                    CreatePosition(positionPanel, level, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
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
                        dropHandler.card_id = matchingCardMonster.id;
                        dropHandler.card_all_power = matchingCardMonster.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = totalPower.ToString();
        }
        else if (mainType.Equals("CardMilitary"))
        {
            double totalPower = 0;
            List<CardMilitary> cardMilitaryList = userCardMilitaryService.GetUserCardMilitaryTeam(User.CurrentUserId, team_id, position);
            cardMilitaryList = cardMilitaryList
                .Where(cardHero => cardHero.team_id.Equals(team_id)) // Lọc theo team_id
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
                CardMilitary matchingCardMilitary = cardMilitaryList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardMilitary != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardMilitary.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardMilitary.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardMilitaryService.UpdateTeamFactCardMilitary(null, null, matchingCardMilitary.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardMilitary.all_power, 0);
                    CreatePosition(positionPanel, level, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
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
                        dropHandler.card_id = matchingCardMilitary.id;
                        dropHandler.card_all_power = matchingCardMilitary.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = totalPower.ToString();
        }
        else if (mainType.Equals("CardSpell"))
        {
            double totalPower = 0;
            List<CardSpell> cardSpellList = userCardSpellService.GetUserCardSpellTeam(User.CurrentUserId, team_id, position);
            cardSpellList = cardSpellList
                .Where(cardHero => cardHero.team_id.Equals(team_id)) // Lọc theo team_id
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
                CardSpell matchingCardSpell = cardSpellList.FirstOrDefault(cardHero =>
                {
                    // Lấy số cuối từ cardHero.position
                    string[] parts = cardHero.position.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int orderIndex))
                    {
                        return orderIndex - 1 == i;
                    }
                    return false;
                });
                if (matchingCardSpell != null)
                {
                    // Gán texture từ cardHero vào Image
                    string fileNameWithoutExtension = matchingCardSpell.image.Replace(".png", "");
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardSpell.all_power;
                }
                else
                {
                    // Nếu không có card tại vị trí, để hình ảnh trống hoặc mặc định
                    image.texture = null; // Hoặc đặt texture mặc định
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardSpellService.UpdateTeamFactCardSpell(null, null, matchingCardSpell.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardSpell.all_power, 0);
                    CreatePosition(positionPanel, level, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, team_limit, team_offset, choseTeam);
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
                        dropHandler.card_id = matchingCardSpell.id;
                        dropHandler.card_all_power = matchingCardSpell.all_power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = totalPower.ToString();
        }
    }
    private void createCardTeams(List<object> obj, Transform panel)
    {
        foreach (Transform child in panel)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in obj)
        {
            if (item is CardHeroes cardHeroes)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardHeroes.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardHeroes.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardHeroes.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardHeroes.team_id != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardHeroes;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardHeroes", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardHeroes", selectedOptionName, team_limit, team_offset, choseTeam);
                            // CreatePosition("CardHeroes", team, positionPanel, typePanel, level, teamsObject);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardCaptains cardCaptains)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardCaptains.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardCaptains.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardCaptains.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardCaptains.team_id != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardCaptains;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardCaptains", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardCaptains", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardColonels cardColonels)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardColonels.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardColonels.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardColonels.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardColonels.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardColonels.team_id != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardColonels;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardColonels", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardColonels", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardGenerals cardGenerals)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardGenerals.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardGenerals.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardGenerals.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardGenerals.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardGenerals.team_id != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardGenerals;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardGenerals", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardGenerals", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardAdmirals cardAdmirals)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardAdmirals.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardAdmirals.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardAdmirals.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardAdmirals.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardAdmirals.team_id != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardAdmirals;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardAdmirals", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardAdmirals", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardMonsters cardMonsters)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardMonsters.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardMonsters.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardMonsters.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMonsters.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardMonsters.team_id != null)
                {
                    InTeam.gameObject.SetActive(true);
                }
                else
                {
                    if (cardObject.GetComponent<CardDragHandler>() == null)
                    {
                        CardDragHandler dragHandler = cardObject.AddComponent<CardDragHandler>();
                        dragHandler.cardTexture = texture; // Lưu texture để sử dụng khi kéo
                        dragHandler.obj = cardMonsters;
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardMonsters", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardMonsters", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardMilitary cardMilitary)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardMilitary.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardMilitary.all_power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardMilitary.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMilitary.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardMilitary.team_id != null)
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
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardMilitary", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardMilitary", selectedOptionName, team_limit, team_offset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardSpell cardSpell)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardSpell.name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = cardSpell.power.ToString();

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = cardSpell.image.Replace(".png", "");
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardSpell.rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardSpell.team_id != null)
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
                        dragHandler.team_id = team_id;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType("CardSpell", selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType("CardSpell", selectedOptionName, team_limit, team_offset, choseTeam);
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
    public void InsertCardToTeam(object obj, string position_id, string card_id, string team_id, double card_all_power)
    {
        string position = "F" + position_id;
        if (obj is CardHeroes cardHeroes)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardHeroesService.UpdateTeamFactCardHeroes(null, null, card_id);
                userCardHeroesService.UpdateTeamFactCardHeroes(team_id, position, cardHeroes.id);
                if (cardHeroes.all_power >= card_all_power)
                {
                    double newPower = cardHeroes.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardHeroes.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardHeroesService.UpdateTeamFactCardHeroes(team_id, position, cardHeroes.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.all_power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptains)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardCaptainsService.UpdateTeamFactCardCaptains(null, null, card_id);
                userCardCaptainsService.UpdateTeamFactCardCaptains(team_id, position, cardCaptains.id);
                if (cardCaptains.all_power >= card_all_power)
                {
                    double newPower = cardCaptains.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardCaptains.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardCaptainsService.UpdateTeamFactCardCaptains(team_id, position, cardCaptains.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.all_power, 1);
            }
        }
        else if (obj is CardColonels cardColonels)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardColonelsService.UpdateTeamFactCardColonels(null, null, card_id);
                userCardColonelsService.UpdateTeamFactCardColonels(team_id, position, cardColonels.id);
                if (cardColonels.all_power >= card_all_power)
                {
                    double newPower = cardColonels.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardColonels.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardColonelsService.UpdateTeamFactCardColonels(team_id, position, cardColonels.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.all_power, 1);
            }
        }
        else if (obj is CardGenerals cardGenerals)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardGeneralsService.UpdateTeamFactCardGenerals(null, null, card_id);
                userCardGeneralsService.UpdateTeamFactCardGenerals(team_id, position, cardGenerals.id);
                if (cardGenerals.all_power >= card_all_power)
                {
                    double newPower = cardGenerals.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardGenerals.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardGeneralsService.UpdateTeamFactCardGenerals(team_id, position, cardGenerals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.all_power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardAdmiralsService.UpdateTeamFactCardAdmirals(null, null, card_id);
                userCardAdmiralsService.UpdateTeamFactCardAdmirals(team_id, position, cardAdmirals.id);
                if (cardAdmirals.all_power >= card_all_power)
                {
                    double newPower = cardAdmirals.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardAdmirals.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardAdmiralsService.UpdateTeamFactCardAdmirals(team_id, position, cardAdmirals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.all_power, 1);
            }
        }
        else if (obj is CardMonsters cardMonsters)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardMonstersService.UpdateTeamFactCardMonsters(null, null, card_id);
                userCardMonstersService.UpdateTeamFactCardMonsters(team_id, position, cardMonsters.id);
                if (cardMonsters.all_power >= card_all_power)
                {
                    double newPower = cardMonsters.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardMonsters.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardMonstersService.UpdateTeamFactCardMonsters(team_id, position, cardMonsters.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.all_power, 1);
            }
        }
        else if (obj is CardMilitary cardMilitary)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardMilitaryService.UpdateTeamFactCardMilitary(null, null, card_id);
                userCardMilitaryService.UpdateTeamFactCardMilitary(team_id, position, cardMilitary.id);
                if (cardMilitary.all_power >= card_all_power)
                {
                    double newPower = cardMilitary.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardMilitary.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardMilitaryService.UpdateTeamFactCardMilitary(team_id, position, cardMilitary.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.all_power, 1);
            }
        }
        else if (obj is CardSpell cardSpell)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardSpellService.UpdateTeamFactCardSpell(null, null, card_id);
                userCardSpellService.UpdateTeamFactCardSpell(team_id, position, cardSpell.id);
                if (cardSpell.all_power >= card_all_power)
                {
                    double newPower = cardSpell.all_power - card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_all_power - cardSpell.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardSpellService.UpdateTeamFactCardSpell(team_id, position, cardSpell.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.all_power, 1);
            }
        }
    }
    public void Close(Transform content)
    {
        // offset = 0;
        currentPage = 1;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    private void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"UI/Background4/{image}");
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
