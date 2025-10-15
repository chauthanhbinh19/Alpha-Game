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
    private GameObject TeamsTypePrefab;
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
        PopupTeamsPrefab = UIManager.Instance.GetGameObject("PopupTeamsPrefab");
        TeamsPanelPrefab = UIManager.Instance.GetGameObject("TeamsPanelPrefab");
        TeamsPositionPrefab = UIManager.Instance.GetGameObject("TeamsPositionPrefab");
        TeamsTypePrefab = UIManager.Instance.GetGameObject("TeamsTypePrefab");
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
        teamsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.Team);
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
            teamsPositionText.text = team.team_number.ToString();
            Texture teamAvatarTexture = Resources.Load<Texture>(team.team_avatar);
            Texture teamBorderTexture = Resources.Load<Texture>(team.team_border);
            teamAvatarImage.texture = teamAvatarTexture;
            teamBorderImage.texture = teamBorderTexture;

            GameObject cardHeroesObject = Instantiate(TeamsTypePrefab, cardContent);
            TextMeshProUGUI cardHeroesQuantityText = cardHeroesObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardHeroesTitleText = cardHeroesObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardHeroesBackground1Image = cardHeroesObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardHeroesBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_438");
            cardHeroesBackground1Image.texture = cardHeroesBackground1Texture;
            int cardHeroesQuantity = UserCardHeroesService.Create().GetUserCardHeroesTeamsCount(User.CurrentUserId, team.team_id);
            cardHeroesQuantityText.text = cardHeroesQuantity.ToString();
            cardHeroesTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CardHeroes);

            GameObject cardCaptainsObject = Instantiate(TeamsTypePrefab, cardContent);
            TextMeshProUGUI cardCaptainsQuantityText = cardCaptainsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardCaptainsTitleText = cardCaptainsObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardCaptainsBackground1Image = cardCaptainsObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardCaptainsBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_439");
            cardCaptainsBackground1Image.texture = cardCaptainsBackground1Texture;
            int cardCaptainsQuantity = UserCardCaptainsService.Create().GetUserCardCaptainsTeamsCount(User.CurrentUserId, team.team_id);
            cardCaptainsQuantityText.text = cardCaptainsQuantity.ToString();
            cardCaptainsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CardCaptains);

            GameObject cardColonelsObject = Instantiate(TeamsTypePrefab, cardContent);
            TextMeshProUGUI cardColonelsQuantityText = cardColonelsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardColonelsTitleText = cardColonelsObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardColonelsBackground1Image = cardColonelsObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardColonelsBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_438");
            cardColonelsBackground1Image.texture = cardColonelsBackground1Texture;
            int cardColonelsQuantity = UserCardColonelsService.Create().GetUserCardColonelsTeamsCount(User.CurrentUserId, team.team_id);
            cardColonelsQuantityText.text = cardColonelsQuantity.ToString();
            cardColonelsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CardColonels);

            GameObject cardGeneralsObject = Instantiate(TeamsTypePrefab, cardContent);
            TextMeshProUGUI cardGeneralsQuantityText = cardGeneralsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardGeneralsTitleText = cardGeneralsObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardGeneralsBackground1Image = cardGeneralsObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardGeneralsBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_439");
            cardGeneralsBackground1Image.texture = cardGeneralsBackground1Texture;
            int cardGeneralsQuantity = UserCardGeneralsService.Create().GetUserCardGeneralsTeamsCount(User.CurrentUserId, team.team_id);
            cardGeneralsQuantityText.text = cardGeneralsQuantity.ToString();
            cardGeneralsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CardGenerals);

            GameObject cardAdmiralsObject = Instantiate(TeamsTypePrefab, cardContent);
            TextMeshProUGUI cardAdmiralsQuantityText = cardAdmiralsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardAdmiralsTitleText = cardAdmiralsObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardAdmiralsBackground1Image = cardAdmiralsObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardAdmiralsBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_438");
            cardAdmiralsBackground1Image.texture = cardAdmiralsBackground1Texture;
            int cardAdmiralsQuantity = UserCardAdmiralsService.Create().GetUserCardAdmiralsTeamsCount(User.CurrentUserId, team.team_id);
            cardAdmiralsQuantityText.text = cardAdmiralsQuantity.ToString();
            cardAdmiralsTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CardAdmirals);

            GameObject cardMonstersObject = Instantiate(TeamsTypePrefab, cardContent);
            TextMeshProUGUI cardMonstersQuantityText = cardMonstersObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardMonstersTitleText = cardMonstersObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardMonstersBackground1Image = cardMonstersObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardMonstersBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_439");
            cardMonstersBackground1Image.texture = cardMonstersBackground1Texture;
            int cardMonstersQuantity = UserCardMonstersService.Create().GetUserCardMonstersTeamsCount(User.CurrentUserId, team.team_id);
            cardMonstersQuantityText.text = cardMonstersQuantity.ToString();
            cardMonstersTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CardMonsters);

            GameObject cardMilitaryObject = Instantiate(TeamsTypePrefab, cardContent);
            TextMeshProUGUI cardMilitaryQuantityText = cardMilitaryObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardMilitaryTitleText = cardMilitaryObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardMilitaryBackground1Image = cardMilitaryObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardMilitaryBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_438");
            cardMilitaryBackground1Image.texture = cardMilitaryBackground1Texture;
            int cardMilitaryQuantity = UserCardMilitaryService.Create().GetUserCardMilitaryTeamsCount(User.CurrentUserId, team.team_id);
            cardMilitaryQuantityText.text = cardMilitaryQuantity.ToString();
            cardMilitaryTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CardMilitary);

            GameObject cardSpellObject = Instantiate(TeamsTypePrefab, cardContent);
            TextMeshProUGUI cardSpellQuantityText = cardSpellObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cardSpellTitleText = cardSpellObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            RawImage cardSpellBackground1Image = cardSpellObject.transform.Find("Background1").GetComponent<RawImage>();
            Texture cardSpellBackground1Texture = Resources.Load<Texture>("UI/Background4/Background_V4_439");
            cardSpellBackground1Image.texture = cardSpellBackground1Texture;
            int cardSpellQuantity = UserCardSpellService.Create().GetUserCardSpellTeamsCount(User.CurrentUserId, team.team_id);
            cardSpellQuantityText.text = cardSpellQuantity.ToString();
            cardSpellTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CardSpell);

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

            int index = team.team_number;
            string tempTeamId = team.team_id;
            changeCardButton.onClick.AddListener(()=>{
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                position = index.ToString();
                team_id = tempTeamId;
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
        Transform tempLeftContent = teamsObject.transform.Find("ScrollViewLeft/Viewport/Content");
        Transform tempRightContent = teamsObject.transform.Find("ScrollViewRight/Viewport/Content");
        CloseButton = teamsObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Close(MainPanel);
            CreateTeams();
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

        team_limit = 10;
        team_offset = 0;
        int page = 1;
        User user = new User();
        user = UserService.Create().GetUserById(User.CurrentUserId);
        CardHeroes cardHeroes = new CardHeroes();
        List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroes(User.CurrentUserId, "Adamas", team_limit, team_offset, rare);
        // Gọi script quản lý cuộn
        // ScrollManager scrollManager = teamsObject.AddComponent<ScrollManager>();
        // scrollManager.Initialize(scrollRect, arrowUp, arrowDown);
        // Thêm sự kiện OnScroll vào ScrollRect
        // scrollRect.onValueChanged.AddListener((Vector2 position) =>
        // {
        //     scrollManager.UpdateArrows(); // Cập nhật mũi tên khi cuộn
        // });

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
            CreatePosition(positionPanel, user.level, teamsObject);
            typeText.text = AppDisplayConstants.MainType.CardHero;

            List<CardHeroes> cardHeroesList = userCardHeroesService.GetUserCardHeroes(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardHeroesList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, choseTeam);
            int totalRecord = userCardHeroesService.GetUserCardHeroesCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_CAPTAIN;
            CreatePosition(positionPanel, user.level, teamsObject);
            typeText.text = AppDisplayConstants.MainType.CardCaptain;

            List<CardCaptains> cardCaptainsList = userCardCaptainsService.GetUserCardCaptains(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardCaptainsList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, choseTeam);
            int totalRecord = userCardCaptainsService.GetUserCardCaptainsCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_COLONEL;
            CreatePosition(positionPanel, user.level, teamsObject);
            typeText.text = AppDisplayConstants.MainType.CardColonel;

            List<CardColonels> cardColonelsList = userCardColonelsService.GetUserCardColonels(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardColonelsList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, choseTeam);
            int totalRecord = userCardColonelsService.GetUserCardColonelsCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_GENERAL;
            CreatePosition(positionPanel, user.level, teamsObject);
            typeText.text = AppDisplayConstants.MainType.CardGeneral;

            List<CardGenerals> cardGeneralsList = userCardGeneralsService.GetUserCardGenerals(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardGeneralsList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, choseTeam);
            int totalRecord = userCardGeneralsService.GetUserCardGeneralsCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_ADMIRAL;
            CreatePosition(positionPanel, user.level, teamsObject);
            typeText.text = AppDisplayConstants.MainType.CardAdmiral;

            List<CardAdmirals> cardAdmiralsList = userCardAdmiralsService.GetUserCardAdmirals(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardAdmiralsList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, choseTeam);
            int totalRecord = userCardAdmiralsService.GetUserCardAdmiralsCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_MONSTER;
            CreatePosition(positionPanel, user.level, teamsObject);
            typeText.text = AppDisplayConstants.MainType.CardMonster;

            List<CardMonsters> cardMonstersList = userCardMonstersService.GetUserCardMonsters(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardMonstersList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, choseTeam);
            int totalRecord = userCardMonstersService.GetUserCardMonstersCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_MILITARY;
            CreatePosition(positionPanel, user.level, teamsObject);
            typeText.text = AppDisplayConstants.MainType.CardMilitary;

            List<CardMilitary> cardMilitaryList = userCardMilitaryService.GetUserCardMilitary(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardMilitaryList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, choseTeam);
            int totalRecord = userCardMilitaryService.GetUserCardMilitaryCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", tempLeftContent, () =>
        {
            mainType = AppConstants.MainType.CARD_SPELL;
            CreatePosition(positionPanel, user.level, teamsObject);
            typeText.text = AppDisplayConstants.MainType.CardSpell;

            List<CardSpell> cardSpellList = userCardSpellService.GetUserCardSpell(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardSpellList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, choseTeam);
            int totalRecord = userCardSpellService.GetUserCardSpellCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        });

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
        CreateCardTeams(cardObjects, choseTeam);
        selectedOptionName = dropdownType.options[dropdownType.value].text;
        int totalRecord = userCardHeroesService.GetUserCardHeroesCount(User.CurrentUserId, selectedOptionName, rare);
        totalPage = CalculateTotalPages(totalRecord, team_limit);
        pageText.text = page.ToString() + "/" + totalPage.ToString();

        nextButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
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
            List<CardMilitary> cardMilitaryList = userCardMilitaryService.GetUserCardMilitary(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
            List<object> cardObjects = cardMilitaryList.Cast<object>().ToList();
            CreateCardTeams(cardObjects, panel);
            int totalRecord = userCardMilitaryService.GetUserCardMilitaryCount(User.CurrentUserId, selectedOptionName, rare);
            totalPage = CalculateTotalPages(totalRecord, team_limit);
        }
        else if (type.Equals(AppConstants.MainType.CARD_SPELL))
        {
            List<CardSpell> cardSpellList = userCardSpellService.GetUserCardSpell(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
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
    private void LoadCardDataByType(string type, string selectedOptionName, int team_limit, int team_offset, Transform choseTeam)
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
                List<CardMilitary> cardMilitaryList = userCardMilitaryService.GetUserCardMilitary(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
                cardObjects = cardMilitaryList.Cast<object>().ToList();
                break;

            case AppConstants.MainType.CARD_SPELL:
                List<CardSpell> cardSpellList = userCardSpellService.GetUserCardSpell(User.CurrentUserId, selectedOptionName, team_limit, team_offset, rare);
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
    public void CreatePosition(Transform positionPanel, int level, GameObject teamsObject)
    {
        foreach (Transform child in positionPanel)
        {
            Destroy(child.gameObject); // Hoặc DestroyImmediate(child.gameObject) nếu cần xóa ngay lập tức
        }
        var backgroundTexture = "Background_V4_408";
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardHero.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardHero.power;
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
                    userCardHeroesService.UpdateTeamCardHeroes(null, null, matchingCardHero.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardHero.power, 0);
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
                        dropHandler.card_power = matchingCardHero.power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardCaptain.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardCaptain.power;
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
                    userCardCaptainsService.UpdateTeamCardCaptains(null, null, matchingCardCaptain.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardCaptain.power, 0);
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
                        dropHandler.card_power = matchingCardCaptain.power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardColonel.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardColonel.power;
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
                    userCardColonelsService.UpdateTeamCardColonels(null, null, matchingCardColonel.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardColonel.power, 0);
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
                        dropHandler.card_power = matchingCardColonel.power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardGeneral.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardGeneral.power;
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
                    userCardGeneralsService.UpdateTeamCardGenerals(null, null, matchingCardGeneral.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardGeneral.power, 0);
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
                        dropHandler.card_power = matchingCardGeneral.power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardAdmiral.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardAdmiral.power;
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
                    userCardAdmiralsService.UpdateTeamCardAdmirals(null, null, matchingCardAdmiral.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardAdmiral.power, 0);
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
                        dropHandler.card_power = matchingCardAdmiral.power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardMonster.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardMonster.power;
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
                    userCardMonstersService.UpdateTeamCardMonsters(null, null, matchingCardMonster.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardMonster.power, 0);
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
                        dropHandler.card_power = matchingCardMonster.power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardMilitary.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardMilitary.power;
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
                    userCardMilitaryService.UpdateTeamCardMilitary(null, null, matchingCardMilitary.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardMilitary.power, 0);
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
                        dropHandler.card_power = matchingCardMilitary.power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
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
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(matchingCardSpell.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    image.texture = texture;

                    LeaveBackground.gameObject.SetActive(true);
                    leaveButton.gameObject.SetActive(true);

                    count = count + 1;
                    totalPower = totalPower + matchingCardSpell.power;
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
                    userCardSpellService.UpdateTeamCardSpell(null, null, matchingCardSpell.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, matchingCardSpell.power, 0);
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
                        dropHandler.card_power = matchingCardSpell.power;
                    }
                    dropHandler.OnDropEnd = () =>
                    {
                        CreatePosition(positionPanel, level, teamsObject);
                    };
                }
            }
            powerText.text = NumberFormatter.FormatNumber(totalPower);
        }
    }
    private void CreateCardTeams(List<object> obj, Transform panel)
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
                Power.text = NumberFormatter.FormatNumber(cardHeroes.power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardHeroes.image);
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
                            LoadCardDataByType(AppConstants.MainType.CARD_HERO, selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_HERO, selectedOptionName, team_limit, team_offset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardCaptains.power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardCaptains.image);
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
                            LoadCardDataByType(AppConstants.MainType.CARD_CAPTAIN, selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_CAPTAIN, selectedOptionName, team_limit, team_offset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardColonels.power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardColonels.image);
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
                            LoadCardDataByType(AppConstants.MainType.CARD_COLONEL, selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_COLONEL, selectedOptionName, team_limit, team_offset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardGenerals.power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardGenerals.image);
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
                            LoadCardDataByType(AppConstants.MainType.CARD_GENERAL, selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_GENERAL, selectedOptionName, team_limit, team_offset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardAdmirals.power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardAdmirals.image);
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
                            LoadCardDataByType(AppConstants.MainType.CARD_ADMIRAL, selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_ADMIRAL, selectedOptionName, team_limit, team_offset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardMonsters.power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMonsters.image);
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
                            LoadCardDataByType(AppConstants.MainType.CARD_MONSTER, selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_MONSTER, selectedOptionName, team_limit, team_offset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardMilitary.power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMilitary.image);
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
                            LoadCardDataByType(AppConstants.MainType.CARD_MILITARY, selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_MILITARY, selectedOptionName, team_limit, team_offset, choseTeam);
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
                Power.text = NumberFormatter.FormatNumber(cardSpell.power);

                RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardSpell.image);
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
                            LoadCardDataByType(AppConstants.MainType.CARD_SPELL, selectedOptionName, team_limit, team_offset, choseTeam);
                        };

                        cardDragHandlers.Add(dragHandler);  // Lưu CardDragHandler vào danh sách
                        // Thêm EventTrigger để xử lý sự kiện click
                        EventTrigger trigger = cardObject.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener((data) =>
                        {
                            dragHandler.OnCardClicked();
                            LoadCardDataByType(AppConstants.MainType.CARD_SPELL, selectedOptionName, team_limit, team_offset, choseTeam);
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
                userCardHeroesService.UpdateTeamCardHeroes(team_id, position, cardHeroes.id);
                if (cardHeroes.power >= card_power)
                {
                    double newPower = cardHeroes.power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardHeroes.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardHeroesService.UpdateTeamCardHeroes(team_id, position, cardHeroes.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptains)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardCaptainsService.UpdateTeamCardCaptains(null, null, card_id);
                userCardCaptainsService.UpdateTeamCardCaptains(team_id, position, cardCaptains.id);
                if (cardCaptains.power >= card_power)
                {
                    double newPower = cardCaptains.power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardCaptains.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardCaptainsService.UpdateTeamCardCaptains(team_id, position, cardCaptains.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.power, 1);
            }
        }
        else if (obj is CardColonels cardColonels)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardColonelsService.UpdateTeamCardColonels(null, null, card_id);
                userCardColonelsService.UpdateTeamCardColonels(team_id, position, cardColonels.id);
                if (cardColonels.power >= card_power)
                {
                    double newPower = cardColonels.power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardColonels.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardColonelsService.UpdateTeamCardColonels(team_id, position, cardColonels.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.power, 1);
            }
        }
        else if (obj is CardGenerals cardGenerals)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardGeneralsService.UpdateTeamCardGenerals(null, null, card_id);
                userCardGeneralsService.UpdateTeamCardGenerals(team_id, position, cardGenerals.id);
                if (cardGenerals.power >= card_power)
                {
                    double newPower = cardGenerals.power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardGenerals.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardGeneralsService.UpdateTeamCardGenerals(team_id, position, cardGenerals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardAdmiralsService.UpdateTeamCardAdmirals(null, null, card_id);
                userCardAdmiralsService.UpdateTeamCardAdmirals(team_id, position, cardAdmirals.id);
                if (cardAdmirals.power >= card_power)
                {
                    double newPower = cardAdmirals.power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardAdmirals.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardAdmiralsService.UpdateTeamCardAdmirals(team_id, position, cardAdmirals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.power, 1);
            }
        }
        else if (obj is CardMonsters cardMonsters)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardMonstersService.UpdateTeamCardMonsters(null, null, card_id);
                userCardMonstersService.UpdateTeamCardMonsters(team_id, position, cardMonsters.id);
                if (cardMonsters.power >= card_power)
                {
                    double newPower = cardMonsters.power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardMonsters.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardMonstersService.UpdateTeamCardMonsters(team_id, position, cardMonsters.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.power, 1);
            }
        }
        else if (obj is CardMilitary cardMilitary)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardMilitaryService.UpdateTeamCardMilitary(null, null, card_id);
                userCardMilitaryService.UpdateTeamCardMilitary(team_id, position, cardMilitary.id);
                if (cardMilitary.power >= card_power)
                {
                    double newPower = cardMilitary.power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardMilitary.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardMilitaryService.UpdateTeamCardMilitary(team_id, position, cardMilitary.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.power, 1);
            }
        }
        else if (obj is CardSpell cardSpell)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(card_id))
            {
                userCardSpellService.UpdateTeamCardSpell(null, null, card_id);
                userCardSpellService.UpdateTeamCardSpell(team_id, position, cardSpell.id);
                if (cardSpell.power >= card_power)
                {
                    double newPower = cardSpell.power - card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = card_power - cardSpell.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardSpellService.UpdateTeamCardSpell(team_id, position, cardSpell.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.power, 1);
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
