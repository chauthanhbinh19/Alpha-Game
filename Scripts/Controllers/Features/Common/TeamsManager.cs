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
    private GameObject PopupTeamFirstPrefab;
    private GameObject PopupTeamSecondPrefab;
    private GameObject TeamsPanelPrefab;
    private GameObject TeamsPositionPrefab;
    private GameObject TeamTypePrefab;
    private GameObject TeamSlotPrefab;
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
    private string teamId;
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
    UserCardMilitaryService userCardMilitaryService;
    UserCardSpellService userCardSpellService;
    TeamsService teamsService;
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
        cardsPrefab3 = UIManager.Instance.GetGameObject("CardsThirdPrefab");
        PopupTeamFirstPrefab = UIManager.Instance.GetGameObject("PopupTeamFirstPrefab");
        PopupTeamSecondPrefab = UIManager.Instance.GetGameObject("PopupTeamSecondPrefab");
        TeamsPanelPrefab = UIManager.Instance.GetGameObject("TeamsPanelPrefab");
        TeamsPositionPrefab = UIManager.Instance.GetGameObject("TeamsPositionPrefab");
        TeamTypePrefab = UIManager.Instance.GetGameObject("TeamTypePrefab");
        TeamSlotPrefab = UIManager.Instance.GetGameObject("TeamSlotPrefab");
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
        rare = "All";
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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            foreach (Transform child in MainPanel)
            {
                Destroy(child.gameObject);
            }
        });
        HomeButton = teamsObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Close(MainPanel);
        });

        mainType = AppConstants.MainType.CARD_HERO;
        teamsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TEAM);
        CreateTeamsPosition(positionTeamsPanel);
    }
    public void CreateTeamsPosition(Transform positionTeamsPanel)
    {
        Close(positionTeamsPanel);
        var userTeams = TeamsService.Create().GetUserTeams(User.CurrentUserId);
        foreach (var team in userTeams)
        {
            GameObject cardTeam = Instantiate(TeamsPositionPrefab, positionTeamsPanel);
            Transform cardContent = cardTeam.transform.Find("Content");
            RawImage cardImage = cardTeam.transform.Find("CardImage").GetComponent<RawImage>();
            TextMeshProUGUI teamsPositionText = cardTeam.transform.Find("TeamNumberText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI teamsContentText = cardTeam.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
            RawImage teamAvatarImage = cardTeam.transform.Find("AvatarImage").GetComponent<RawImage>();
            RawImage teamBorderImage = cardTeam.transform.Find("BorderImage").GetComponent<RawImage>();
            teamsPositionText.text = team.TeamNumber.ToString();
            Texture teamAvatarTexture = Resources.Load<Texture>(team.TeamAvatar);
            Texture teamBorderTexture = Resources.Load<Texture>(team.TeamBorder);
            teamAvatarImage.texture = teamAvatarTexture;
            teamBorderImage.texture = teamBorderTexture;

            GameObject cardHeroesObject = Instantiate(TeamTypePrefab, cardContent);
            TextMeshProUGUI cardHeroesQuantityText = cardHeroesObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardHeroesTitleText = cardHeroesObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardHeroesBackground1Image = cardHeroesObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardHeroesBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_438");
            cardHeroesBackground1Image.texture = cardHeroesBackground1Texture;
            int cardHeroesQuantity = UserCardHeroesService.Create().GetUserCardHeroesTeamsCount(User.CurrentUserId, team.TeamId);
            cardHeroesQuantityText.text = cardHeroesQuantity.ToString();
            cardHeroesTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HEROES);

            GameObject cardCaptainsObject = Instantiate(TeamTypePrefab, cardContent);
            TextMeshProUGUI cardCaptainsQuantityText = cardCaptainsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardCaptainsTitleText = cardCaptainsObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardCaptainsBackground1Image = cardCaptainsObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardCaptainsBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_439");
            cardCaptainsBackground1Image.texture = cardCaptainsBackground1Texture;
            int cardCaptainsQuantity = UserCardCaptainsService.Create().GetUserCardCaptainsTeamsCount(User.CurrentUserId, team.TeamId);
            cardCaptainsQuantityText.text = cardCaptainsQuantity.ToString();
            cardCaptainsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAINS);

            GameObject cardColonelsObject = Instantiate(TeamTypePrefab, cardContent);
            TextMeshProUGUI cardColonelsQuantityText = cardColonelsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardColonelsTitleText = cardColonelsObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardColonelsBackground1Image = cardColonelsObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardColonelsBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_438");
            cardColonelsBackground1Image.texture = cardColonelsBackground1Texture;
            int cardColonelsQuantity = UserCardColonelsService.Create().GetUserCardColonelsTeamsCount(User.CurrentUserId, team.TeamId);
            cardColonelsQuantityText.text = cardColonelsQuantity.ToString();
            cardColonelsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_COLONELS);

            GameObject cardGeneralsObject = Instantiate(TeamTypePrefab, cardContent);
            TextMeshProUGUI cardGeneralsQuantityText = cardGeneralsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardGeneralsTitleText = cardGeneralsObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardGeneralsBackground1Image = cardGeneralsObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardGeneralsBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_439");
            cardGeneralsBackground1Image.texture = cardGeneralsBackground1Texture;
            int cardGeneralsQuantity = UserCardGeneralsService.Create().GetUserCardGeneralsTeamsCount(User.CurrentUserId, team.TeamId);
            cardGeneralsQuantityText.text = cardGeneralsQuantity.ToString();
            cardGeneralsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERALS);

            GameObject cardAdmiralsObject = Instantiate(TeamTypePrefab, cardContent);
            TextMeshProUGUI cardAdmiralsQuantityText = cardAdmiralsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardAdmiralsTitleText = cardAdmiralsObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardAdmiralsBackground1Image = cardAdmiralsObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardAdmiralsBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_438");
            cardAdmiralsBackground1Image.texture = cardAdmiralsBackground1Texture;
            int cardAdmiralsQuantity = UserCardAdmiralsService.Create().GetUserCardAdmiralsTeamsCount(User.CurrentUserId, team.TeamId);
            cardAdmiralsQuantityText.text = cardAdmiralsQuantity.ToString();
            cardAdmiralsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRALS);

            GameObject cardMonstersObject = Instantiate(TeamTypePrefab, cardContent);
            TextMeshProUGUI cardMonstersQuantityText = cardMonstersObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardMonstersTitleText = cardMonstersObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardMonstersBackground1Image = cardMonstersObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardMonstersBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_439");
            cardMonstersBackground1Image.texture = cardMonstersBackground1Texture;
            int cardMonstersQuantity = UserCardMonstersService.Create().GetUserCardMonstersTeamsCount(User.CurrentUserId, team.TeamId);
            cardMonstersQuantityText.text = cardMonstersQuantity.ToString();
            cardMonstersTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MONSTERS);

            GameObject cardMilitaryObject = Instantiate(TeamTypePrefab, cardContent);
            TextMeshProUGUI cardMilitaryQuantityText = cardMilitaryObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardMilitaryTitleText = cardMilitaryObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardMilitaryBackground1Image = cardMilitaryObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardMilitaryBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_438");
            cardMilitaryBackground1Image.texture = cardMilitaryBackground1Texture;
            int cardMilitaryQuantity = UserCardMilitaryService.Create().GetUserCardMilitaryTeamsCount(User.CurrentUserId, team.TeamId);
            cardMilitaryQuantityText.text = cardMilitaryQuantity.ToString();
            cardMilitaryTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MILITARY);

            GameObject cardSpellObject = Instantiate(TeamTypePrefab, cardContent);
            TextMeshProUGUI cardSpellQuantityText = cardSpellObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardSpellTitleText = cardSpellObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardSpellBackground1Image = cardSpellObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardSpellBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_439");
            cardSpellBackground1Image.texture = cardSpellBackground1Texture;
            int cardSpellQuantity = UserCardSpellService.Create().GetUserCardSpellTeamsCount(User.CurrentUserId, team.TeamId);
            cardSpellQuantityText.text = cardSpellQuantity.ToString();
            cardSpellTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_SPELL);

            // if (mainType.Equals(AppConstants.MainType.CardHero))
            // {
            //     positionNumber = userCardHeroesService.GetUserCardHeroesTeamsPositionCount(User.CurrentUserId, team_id, team.team_id.ToString());
            // }
            // else if (mainType.Equals(AppConstants.MainType.CardCaptain))
            // {
            //     positionNumber = userCardCaptainsService.GetUserCardCaptainsTeamsPositionCount(User.CurrentUserId, team_id, team.team_id.ToString());
            // }
            // else if (mainType.Equals(AppConstants.MainType.CardColonel))
            // {
            //     positionNumber = userCardColonelsService.GetUserCardColonelsTeamsPositionCount(User.CurrentUserId, team_id, team.team_id.ToString());
            // }
            // else if (mainType.Equals(AppConstants.MainType.CardGeneral))
            // {
            //     positionNumber = userCardGeneralsService.GetUserCardGeneralsTeamsPositionCount(User.CurrentUserId, team_id, team.team_id.ToString());
            // }
            // else if (mainType.Equals(AppConstants.MainType.CardAdmiral))
            // {
            //     positionNumber = userCardAdmiralsService.GetUserCardAdmiralsTeamsPositionCount(User.CurrentUserId, team_id, team.team_id.ToString());
            // }
            // else if (mainType.Equals(AppConstants.MainType.CardMonster))
            // {
            //     positionNumber = userCardMonstersService.GetUserCardMonstersTeamsPositionCount(User.CurrentUserId, team_id, team.team_id.ToString());
            // }
            // else if (mainType.Equals(AppConstants.MainType.CardMilitary))
            // {
            //     positionNumber = userCardMilitaryService.GetUserCardMilitaryTeamsPositionCount(User.CurrentUserId, team_id, team.team_id.ToString());
            // }
            // else if (mainType.Equals(AppConstants.MainType.CardSpell))
            // {
            //     positionNumber = userCardSpellService.GetUserCardSpellTeamsPositionCount(User.CurrentUserId, team_id, team.team_id.ToString());
            // }
            // teamsContentText.text = positionNumber.ToString() + "/10";

            Button changeCardButton = cardTeam.transform.Find("ChangeCardButton").GetComponent<Button>();

            string tempTeamId = team.TeamId;
            changeCardButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                teamId = tempTeamId;
                CreatePopupTeamFirstPanel();
            });
        }
    }
    public void CreatePopupTeamFirstPanel()
    {
        GameObject teamsObject = Instantiate(PopupTeamFirstPrefab, MainPanel);
        titleText = teamsObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        ScrollRect scrollRect = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition").GetComponent<ScrollRect>();
        Transform teamSlotPanel = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        Transform tempLeftContent = teamsObject.transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = teamsObject.transform.Find("ScrollViewRight/Viewport/Content");
        CloseButton = teamsObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(teamsObject);
            CreateTeams();
        });
        HomeButton = teamsObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Close(MainPanel);
        });

        TextMeshProUGUI typeText = teamsObject.transform.Find("DictionaryCards/TypeText").GetComponent<TextMeshProUGUI>();

        CreateButton(1, AppConstants.MainType.CARD_HERO, tempLeftContent);
        CreateButton(2, AppConstants.MainType.CARD_CAPTAIN, tempLeftContent);
        CreateButton(3, AppConstants.MainType.CARD_COLONEL, tempLeftContent);
        CreateButton(4, AppConstants.MainType.CARD_GENERAL, tempLeftContent);
        CreateButton(5, AppConstants.MainType.CARD_ADMIRAL, tempLeftContent);
        CreateButton(6, AppConstants.MainType.CARD_MONSTER, tempLeftContent);
        CreateButton(7, AppConstants.MainType.CARD_MILITARY, tempLeftContent);
        CreateButton(8, AppConstants.MainType.CARD_SPELL, tempLeftContent);
        ButtonEvent.Instance.AssignButtonEvent("Button_1", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_HERO;
            CreateSlot(teamSlotPanel);
            typeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HERO);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_CAPTAIN;
            CreateSlot(teamSlotPanel);
            typeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_COLONEL;
            CreateSlot(teamSlotPanel);
            typeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_COLONEL);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_GENERAL;
            CreateSlot(teamSlotPanel);
            typeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERAL);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_ADMIRAL;
            CreateSlot(teamSlotPanel);
            typeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_MONSTER;
            CreateSlot(teamSlotPanel);
            typeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MONSTER);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_MILITARY;
            CreateSlot(teamSlotPanel);
            typeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MILITARY);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_SPELL;
            CreateSlot(teamSlotPanel);
            typeText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_SPELL);
        });

        CreateSlot(teamSlotPanel);
    }
    public void CreatePopupTeamSecondPanel()
    {
        GameObject teamsObject = Instantiate(PopupTeamSecondPrefab, MainPanel);
        titleText = teamsObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        ScrollRect scrollRect = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition").GetComponent<ScrollRect>();
        positionPanel = teamsObject.transform.Find("DictionaryCards/ScrollViewPosition/Viewport/Content");
        Transform tempLeftContent = teamsObject.transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = teamsObject.transform.Find("ScrollViewRight/Viewport/Content");
        CloseButton = teamsObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(teamsObject);
            CreatePopupTeamFirstPanel();
        });
        HomeButton = teamsObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Close(MainPanel);
        });
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

        teamLimit = 10;
        teamOffset = 0;
        int page = 1;
        // User user = new User();
        // user = UserService.Create().GetUserById(User.CurrentUserId);

        List<object> cardObjects = new List<object>();

        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroes(User.CurrentUserId, "Adamas", teamLimit, teamOffset, rare);
            cardObjects = cardHeroesList.Cast<object>().ToList();
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> cardCaptainsList = userCardCaptainsService.GetUserCardCaptains(User.CurrentUserId, "Adamas", teamLimit, teamOffset, rare);
            cardObjects = cardCaptainsList.Cast<object>().ToList();
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonelsList = userCardColonelsService.GetUserCardColonels(User.CurrentUserId, "Adamas", teamLimit, teamOffset, rare);
            cardObjects = cardColonelsList.Cast<object>().ToList();
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGeneralsList = userCardGeneralsService.GetUserCardGenerals(User.CurrentUserId, "Adamas", teamLimit, teamOffset, rare);
            cardObjects = cardGeneralsList.Cast<object>().ToList();
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmirals(User.CurrentUserId, "Adamas", teamLimit, teamOffset, rare);
            cardObjects = cardAdmiralsList.Cast<object>().ToList();
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonstersList = userCardMonstersService.GetUserCardMonsters(User.CurrentUserId, "Adamas", teamLimit, teamOffset, rare);
            cardObjects = cardMonstersList.Cast<object>().ToList();
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitariesList = userCardMilitaryService.GetUserCardMilitary(User.CurrentUserId, "Adamas", teamLimit, teamOffset, rare);
            cardObjects = cardMilitariesList.Cast<object>().ToList();
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpellsList = userCardSpellService.GetUserCardSpell(User.CurrentUserId, "Adamas", teamLimit, teamOffset, rare);
            cardObjects = cardSpellsList.Cast<object>().ToList();
        }

        // Gọi script quản lý cuộn
        // ScrollManager scrollManager = teamsObject.AddComponent<ScrollManager>();
        // scrollManager.Initialize(scrollRect, arrowUp, arrowDown);
        // Thêm sự kiện OnScroll vào ScrollRect
        // scrollRect.onValueChanged.AddListener((Vector2 position) =>
        // {
        //     scrollManager.UpdateArrows(); // Cập nhật mũi tên khi cuộn
        // });



        GetTeamsType(mainType, dropdownType, choseTeam, pageText, teamLimit, newOffset =>
        {
            teamOffset = newOffset;
        }, newCurrentPage =>
        {
            page = newCurrentPage;
        });
        typeText.text = string.Concat(mainType.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x : x.ToString()));

        CreatePosition(positionPanel, teamsObject);
        CreateCardTeams(cardObjects, choseTeam);
        selectedOptionName = dropdownType.options[dropdownType.value].text;
        int totalRecord = userCardHeroesService.GetUserCardHeroesCount(User.CurrentUserId, selectedOptionName, rare);
        totalPage = CalculateTotalPages(totalRecord, teamLimit);
        pageText.text = page.ToString() + "/" + totalPage.ToString();

        nextButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            if (page < totalPage)
            {
                teamOffset = teamOffset + teamLimit;
                page = page + 1;
                LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                pageText.text = page.ToString() + "/" + totalPage.ToString();
            }
        });
        previousButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            if (page > 1)
            {
                teamOffset = teamOffset - teamLimit;
                page = page - 1;
                LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
                pageText.text = page.ToString() + "/" + totalPage.ToString();
            }
        });
    }
    public void CreateButton(int index, string itemName, Transform panel)
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

        if (type.Equals(AppConstants.MainType.CARD_HERO))
        {
            List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroes(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardHeroesList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardHeroesService.GetUserCardHeroesCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            List<CardCaptains> cardCaptainsList = userCardCaptainsService.GetUserCardCaptains(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardCaptainsList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardCaptainsService.GetUserCardCaptainsCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            List<CardColonels> cardColonelsList = userCardColonelsService.GetUserCardColonels(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardColonelsList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardColonelsService.GetUserCardColonelsCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            List<CardGenerals> cardGeneralsList = userCardGeneralsService.GetUserCardGenerals(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardGeneralsList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardGeneralsService.GetUserCardGeneralsCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmirals(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardAdmiralsList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardAdmiralsService.GetUserCardAdmiralsCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            List<CardMonsters> cardMonstersList = userCardMonstersService.GetUserCardMonsters(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardMonstersList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardMonstersService.GetUserCardMonstersCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaryList = userCardMilitaryService.GetUserCardMilitary(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardMilitaryList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardMilitaryService.GetUserCardMilitaryCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpells> cardSpellList = userCardSpellService.GetUserCardSpell(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardSpellList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardSpellService.GetUserCardSpellCount(User.CurrentUserId, selectedOptionName, rare);
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
    public void LoadCardDataByType(string type, string selectedOptionName, int team_limit, int team_offset, Transform choseTeam)
    {
        List<object> cardObjects = null;

        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroes(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardHeroesList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_CAPTAIN:
                List<CardCaptains> cardCaptainsList = userCardCaptainsService.GetUserCardCaptains(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardCaptainsList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_COLONEL:
                List<CardColonels> cardColonelsList = userCardColonelsService.GetUserCardColonels(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardColonelsList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_GENERAL:
                List<CardGenerals> cardGeneralsList = userCardGeneralsService.GetUserCardGenerals(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardGeneralsList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_ADMIRAL:
                List<CardAdmirals> cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmirals(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardAdmiralsList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_MONSTER:
                List<CardMonsters> cardMonstersList = userCardMonstersService.GetUserCardMonsters(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardMonstersList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_MILITARY:
                List<CardMilitaries> cardMilitaryList = userCardMilitaryService.GetUserCardMilitary(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardMilitaryList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_SPELL:
                List<CardSpells> cardSpellList = userCardSpellService.GetUserCardSpell(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
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
    public void CreateSlot(Transform slotPanel)
    {
        ButtonEvent.Instance.Close(slotPanel);

        List<CardHeroes> cardHeroesList = null;
        List<CardCaptains> cardCaptainsList = null;
        List<CardColonels> cardColonelsList = null;
        List<CardGenerals> cardGeneralsList = null;
        List<CardAdmirals> cardAdmiralsList = null;
        List<CardMonsters> cardMonstersList = null;
        List<CardMilitaries> cardMilitariesList = null;
        List<CardSpells> cardSpellsList = null;

        switch (mainType)
        {
            case AppConstants.MainType.CARD_HERO:
                // Tải dữ liệu 1 lần duy nhất
                cardHeroesList = userCardHeroesService.GetUserCardHeroesTeamWithoutPosition(User.CurrentUserId, teamId);
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                // Tải dữ liệu 1 lần duy nhất
                cardCaptainsList = userCardCaptainsService.GetUserCardCaptainsTeamWithoutPosition(User.CurrentUserId, teamId);
                break;
            case AppConstants.MainType.CARD_COLONEL:
                // Tải dữ liệu 1 lần duy nhất
                cardColonelsList = userCardColonelsService.GetUserCardColonelsTeamWithoutPosition(User.CurrentUserId, teamId);
                break;
            case AppConstants.MainType.CARD_GENERAL:
                // Tải dữ liệu 1 lần duy nhất
                cardGeneralsList = userCardGeneralsService.GetUserCardGeneralsTeamWithoutPosition(User.CurrentUserId, teamId);
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                // Tải dữ liệu 1 lần duy nhất
                cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmiralsTeamWithoutPosition(User.CurrentUserId, teamId);
                break;
            case AppConstants.MainType.CARD_MONSTER:
                // Tải dữ liệu 1 lần duy nhất
                cardMonstersList = userCardMonstersService.GetUserCardMonstersTeamWithoutPosition(User.CurrentUserId, teamId);
                break;
            case AppConstants.MainType.CARD_MILITARY:
                // Tải dữ liệu 1 lần duy nhất
                cardMilitariesList = userCardMilitaryService.GetUserCardMilitaryTeamWithoutPosition(User.CurrentUserId, teamId);
                break;
            case AppConstants.MainType.CARD_SPELL:
                // Tải dữ liệu 1 lần duy nhất
                cardSpellsList = userCardSpellService.GetUserCardSpellTeamWithoutPosition(User.CurrentUserId, teamId);
                break;
                // ... (Tải dữ liệu cho các case khác)
        }

        for (int i = 1; i <= 10; i++)
        {
            GameObject teamSlotObject = Instantiate(TeamSlotPrefab, slotPanel);
            TextMeshProUGUI titleText = teamSlotObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI quantityText = teamSlotObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            Button popupButton = teamSlotObject.GetComponent<Button>();

            titleText.text = "Slot " + i.ToString();
            int tempPosition = i;
            int countPosition = 0;
            switch (mainType)
            {
                case AppConstants.MainType.CARD_HERO:
                    if(cardHeroesList!= null)
                    {
                        countPosition = cardHeroesList
                            .Count(card => card.Position != null && card.Position.StartsWith($"{i}-"));
                    }
                    
                    quantityText.text = countPosition.ToString();

                    popupButton.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                        position = tempPosition.ToString();
                        CreatePopupTeamSecondPanel();
                    });
                    break;
                case AppConstants.MainType.CARD_CAPTAIN:
                    if(cardCaptainsList!= null)
                    {
                        countPosition = cardCaptainsList
                        .Count(card => card.Position != null && card.Position.StartsWith($"{i}-"));
                    }
                    
                    quantityText.text = countPosition.ToString();

                    popupButton.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                        position = tempPosition.ToString();
                        CreatePopupTeamSecondPanel();
                    });
                    break;
                case AppConstants.MainType.CARD_COLONEL:
                    if(cardColonelsList!= null)
                    {
                        countPosition = cardColonelsList
                        .Count(card => card.Position != null && card.Position.StartsWith($"{i}-"));
                    }
                    
                    quantityText.text = countPosition.ToString();

                    popupButton.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                        position = tempPosition.ToString();
                        CreatePopupTeamSecondPanel();
                    });
                    break;
                case AppConstants.MainType.CARD_GENERAL:
                    if(cardGeneralsList!= null)
                    {
                        countPosition = cardGeneralsList
                        .Count(card => card.Position != null && card.Position.StartsWith($"{i}-"));
                    }
                    
                    quantityText.text = countPosition.ToString();

                    popupButton.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                        position = tempPosition.ToString();
                        CreatePopupTeamSecondPanel();
                    });
                    break;
                case AppConstants.MainType.CARD_ADMIRAL:
                    if(cardAdmiralsList!= null)
                    {
                        countPosition = cardAdmiralsList
                        .Count(card => card.Position != null && card.Position.StartsWith($"{i}-"));
                    }
                    
                    quantityText.text = countPosition.ToString();

                    popupButton.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                        position = tempPosition.ToString();
                        CreatePopupTeamSecondPanel();
                    });
                    break;
                case AppConstants.MainType.CARD_MONSTER:
                    if(cardMonstersList!= null)
                    {
                        countPosition = cardMonstersList
                        .Count(card => card.Position != null && card.Position.StartsWith($"{i}-"));
                    }
                    
                    quantityText.text = countPosition.ToString();

                    popupButton.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                        position = tempPosition.ToString();
                        CreatePopupTeamSecondPanel();
                    });
                    break;
                case AppConstants.MainType.CARD_MILITARY:
                    if(cardMilitariesList!= null)
                    {
                        countPosition = cardMilitariesList
                        .Count(card => card.Position != null && card.Position.StartsWith($"{i}-"));
                    }
                    
                    quantityText.text = countPosition.ToString();

                    popupButton.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                        position = tempPosition.ToString();
                        CreatePopupTeamSecondPanel();
                    });
                    break;
                case AppConstants.MainType.CARD_SPELL:
                    if(cardSpellsList!= null)
                    {
                        countPosition = cardSpellsList
                        .Count(card => card.Position != null && card.Position.StartsWith($"{i}-"));
                    }
                    
                    quantityText.text = countPosition.ToString();

                    popupButton.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                        position = tempPosition.ToString();
                        CreatePopupTeamSecondPanel();
                    });
                    break;
                default:
                    break;
            }
        }
    }
    public void CreatePosition(Transform positionPanel, GameObject teamsObject)
    {
        foreach (Transform child in positionPanel)
        {
            Destroy(child.gameObject); // Hoặc DestroyImmediate(child.gameObject) nếu cần xóa ngay lập tức
        }
        var backgroundTexture = "Background_V4_408";
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            double totalPower = 0;
            List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroesTeam(User.CurrentUserId, teamId, position);
            cardHeroesList = cardHeroesList
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
                CardHeroes matchingCardHero = cardHeroesList.FirstOrDefault(cardHero =>
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
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardHero.Power;
                }
                else
                {
                    Texture texture = Resources.Load<Texture>($"UI/Background4/{backgroundTexture}");
                    image.texture = texture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardHeroesService.UpdateTeamCardHeroes(null, null, matchingCardHero.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardHero.Power, 0);
                    CreatePosition(positionPanel, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            double totalPower = 0;
            List<CardCaptains> cardCaptainsList = userCardCaptainsService.GetUserCardCaptainsTeam(User.CurrentUserId, teamId, position);
            cardCaptainsList = cardCaptainsList
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
                CardCaptains matchingCardCaptain = cardCaptainsList.FirstOrDefault(cardHero =>
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
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardCaptain.Power;
                }
                else
                {
                    Texture texture = Resources.Load<Texture>($"UI/Background4/{backgroundTexture}");
                    image.texture = texture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardCaptainsService.UpdateTeamCardCaptains(null, null, matchingCardCaptain.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardCaptain.Power, 0);
                    CreatePosition(positionPanel, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            double totalPower = 0;
            List<CardColonels> cardColonelsList = userCardColonelsService.GetUserCardColonelsTeam(User.CurrentUserId, teamId, position);
            cardColonelsList = cardColonelsList
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
                CardColonels matchingCardColonel = cardColonelsList.FirstOrDefault(cardHero =>
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
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardColonel.Power;
                }
                else
                {
                    Texture texture = Resources.Load<Texture>($"UI/Background4/{backgroundTexture}");
                    image.texture = texture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardColonelsService.UpdateTeamCardColonels(null, null, matchingCardColonel.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardColonel.Power, 0);
                    CreatePosition(positionPanel, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            double totalPower = 0;
            List<CardGenerals> cardGeneralsList = userCardGeneralsService.GetUserCardGeneralsTeam(User.CurrentUserId, teamId, position);
            cardGeneralsList = cardGeneralsList
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
                CardGenerals matchingCardGeneral = cardGeneralsList.FirstOrDefault(cardHero =>
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
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardGeneral.Power;
                }
                else
                {
                    Texture texture = Resources.Load<Texture>($"UI/Background4/{backgroundTexture}");
                    image.texture = texture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardGeneralsService.UpdateTeamCardGenerals(null, null, matchingCardGeneral.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardGeneral.Power, 0);
                    CreatePosition(positionPanel, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            double totalPower = 0;
            List<CardAdmirals> cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmiralsTeam(User.CurrentUserId, teamId, position);
            cardAdmiralsList = cardAdmiralsList
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
                CardAdmirals matchingCardAdmiral = cardAdmiralsList.FirstOrDefault(cardHero =>
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
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardAdmiral.Power;
                }
                else
                {
                    Texture texture = Resources.Load<Texture>($"UI/Background4/{backgroundTexture}");
                    image.texture = texture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardAdmiralsService.UpdateTeamCardAdmirals(null, null, matchingCardAdmiral.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardAdmiral.Power, 0);
                    CreatePosition(positionPanel, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            double totalPower = 0;
            List<CardMonsters> cardMonstersList = userCardMonstersService.GetUserCardMonstersTeam(User.CurrentUserId, teamId, position);
            cardMonstersList = cardMonstersList
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
                CardMonsters matchingCardMonster = cardMonstersList.FirstOrDefault(cardHero =>
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
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardMonster.Power;
                }
                else
                {
                    Texture texture = Resources.Load<Texture>($"UI/Background4/{backgroundTexture}");
                    image.texture = texture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardMonstersService.UpdateTeamCardMonsters(null, null, matchingCardMonster.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardMonster.Power, 0);
                    CreatePosition(positionPanel, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            double totalPower = 0;
            List<CardMilitaries> cardMilitaryList = userCardMilitaryService.GetUserCardMilitaryTeam(User.CurrentUserId, teamId, position);
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
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardMilitary.Power;
                }
                else
                {
                    Texture texture = Resources.Load<Texture>($"UI/Background4/{backgroundTexture}");
                    image.texture = texture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardMilitaryService.UpdateTeamCardMilitary(null, null, matchingCardMilitary.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardMilitary.Power, 0);
                    CreatePosition(positionPanel, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            double totalPower = 0;
            List<CardSpells> cardSpellList = userCardSpellService.GetUserCardSpellTeam(User.CurrentUserId, teamId, position);
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
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardSpell.Power;
                }
                else
                {
                    Texture texture = Resources.Load<Texture>($"UI/Background4/{backgroundTexture}");
                    image.texture = texture;
                    buttonText.text = "Front";
                }

                leaveButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    image.texture = null;
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    userCardSpellService.UpdateTeamCardSpell(null, null, matchingCardSpell.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardSpell.Power, 0);
                    CreatePosition(positionPanel, teamsObject);
                    LoadCardDataByType(mainType, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, teamsObject);
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
            if (item is CardHeroes cardHeroes)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardHeroes.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatter.FormatNumber(cardHeroes.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardHeroes.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardHeroes.Rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardHeroes.TeamId != null)
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType(AppConstants.MainType.CARD_HERO, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_HERO, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
                Title.text = cardCaptains.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatter.FormatNumber(cardCaptains.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardCaptains.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardCaptains.Rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardCaptains.TeamId != null)
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType(AppConstants.MainType.CARD_CAPTAIN, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_CAPTAIN, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardColonels cardColonels)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardColonels.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatter.FormatNumber(cardColonels.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardColonels.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardColonels.Rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardColonels.TeamId != null)
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType(AppConstants.MainType.CARD_COLONEL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_COLONEL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardGenerals cardGenerals)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardGenerals.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatter.FormatNumber(cardGenerals.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardGenerals.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardGenerals.Rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardGenerals.TeamId != null)
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType(AppConstants.MainType.CARD_GENERAL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_GENERAL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardAdmirals cardAdmirals)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardAdmirals.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatter.FormatNumber(cardAdmirals.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardAdmirals.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardAdmirals.Rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardAdmirals.TeamId != null)
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType(AppConstants.MainType.CARD_ADMIRAL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_ADMIRAL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardMonsters cardMonsters)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardMonsters.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatter.FormatNumber(cardMonsters.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMonsters.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMonsters.Rare}");
                rareImage.texture = rareTexture;
                Transform InTeam = cardObject.transform.Find("InTeam");
                if (cardMonsters.TeamId != null)
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
                        dragHandler.team_id = teamId;
                        dragHandler.InTeam = InTeam;
                        dragHandler.mainPosition = position;
                        dragHandler.positionPanel = positionPanel;
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType(AppConstants.MainType.CARD_MONSTER, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_MONSTER, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardMilitaries cardMilitary)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardMilitary.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatter.FormatNumber(cardMilitary.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMilitary.Rare}");
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
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType(AppConstants.MainType.CARD_MILITARY, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_MILITARY, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        }); // Gọi OnCardClicked của dragHandler
                        trigger.triggers.Add(entry);
                    }
                }
            }
            else if (item is CardSpells cardSpell)
            {
                GameObject cardObject = Instantiate(cardsPrefab3, panel);

                Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
                Title.text = cardSpell.Name.Replace("_", " ");
                TextMeshProUGUI Power = cardObject.transform.Find("Power/PowerText").GetComponent<TextMeshProUGUI>();
                Power.text = NumberFormatter.FormatNumber(cardSpell.Power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardSpell.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                Image.texture = texture;

                RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardSpell.Rare}");
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
                        dragHandler.OnDragEnd = () =>
                        {
                            LoadCardDataByType(AppConstants.MainType.CARD_SPELL, selectedOptionName, teamLimit, teamOffset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_SPELL, selectedOptionName, teamLimit, teamOffset, choseTeam);
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
    public void InsertCardToTeam(object obj, string position_id, string card_id, string team_id, double card_power)
    {
        string position = "F" + position_id;
        if (obj is CardHeroes cardHeroes)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardHeroesService.UpdateTeamCardHeroes(null, null, card_id);
                userCardHeroesService.UpdateTeamCardHeroes(team_id, position, cardHeroes.Id);
                if (cardHeroes.Power >= card_power)
                {
                    double newPower = cardHeroes.Power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardHeroes.Power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardHeroesService.UpdateTeamCardHeroes(team_id, position, cardHeroes.Id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.Power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptains)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardCaptainsService.UpdateTeamCardCaptains(null, null, card_id);
                userCardCaptainsService.UpdateTeamCardCaptains(team_id, position, cardCaptains.Id);
                if (cardCaptains.Power >= card_power)
                {
                    double newPower = cardCaptains.Power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardCaptains.Power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardCaptainsService.UpdateTeamCardCaptains(team_id, position, cardCaptains.Id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.Power, 1);
            }
        }
        else if (obj is CardColonels cardColonels)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardColonelsService.UpdateTeamCardColonels(null, null, card_id);
                userCardColonelsService.UpdateTeamCardColonels(team_id, position, cardColonels.Id);
                if (cardColonels.Power >= card_power)
                {
                    double newPower = cardColonels.Power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardColonels.Power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardColonelsService.UpdateTeamCardColonels(team_id, position, cardColonels.Id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.Power, 1);
            }
        }
        else if (obj is CardGenerals cardGenerals)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardGeneralsService.UpdateTeamCardGenerals(null, null, card_id);
                userCardGeneralsService.UpdateTeamCardGenerals(team_id, position, cardGenerals.Id);
                if (cardGenerals.Power >= card_power)
                {
                    double newPower = cardGenerals.Power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardGenerals.Power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardGeneralsService.UpdateTeamCardGenerals(team_id, position, cardGenerals.Id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.Power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardAdmiralsService.UpdateTeamCardAdmirals(null, null, card_id);
                userCardAdmiralsService.UpdateTeamCardAdmirals(team_id, position, cardAdmirals.Id);
                if (cardAdmirals.Power >= card_power)
                {
                    double newPower = cardAdmirals.Power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardAdmirals.Power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardAdmiralsService.UpdateTeamCardAdmirals(team_id, position, cardAdmirals.Id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.Power, 1);
            }
        }
        else if (obj is CardMonsters cardMonsters)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardMonstersService.UpdateTeamCardMonsters(null, null, card_id);
                userCardMonstersService.UpdateTeamCardMonsters(team_id, position, cardMonsters.Id);
                if (cardMonsters.Power >= card_power)
                {
                    double newPower = cardMonsters.Power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardMonsters.Power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardMonstersService.UpdateTeamCardMonsters(team_id, position, cardMonsters.Id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.Power, 1);
            }
        }
        else if (obj is CardMilitaries cardMilitary)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardMilitaryService.UpdateTeamCardMilitary(null, null, card_id);
                userCardMilitaryService.UpdateTeamCardMilitary(team_id, position, cardMilitary.Id);
                if (cardMilitary.Power >= card_power)
                {
                    double newPower = cardMilitary.Power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardMilitary.Power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardMilitaryService.UpdateTeamCardMilitary(team_id, position, cardMilitary.Id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.Power, 1);
            }
        }
        else if (obj is CardSpells cardSpell)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardSpellService.UpdateTeamCardSpell(null, null, card_id);
                userCardSpellService.UpdateTeamCardSpell(team_id, position, cardSpell.Id);
                if (cardSpell.Power >= card_power)
                {
                    double newPower = cardSpell.Power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardSpell.Power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardSpellService.UpdateTeamCardSpell(team_id, position, cardSpell.Id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.Power, 1);
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
