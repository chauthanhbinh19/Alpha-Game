using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class MasterBoardController : MonoBehaviour
{
    public static MasterBoardController Instance { get; private set; }
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform Content;
    private GameObject currentObject;
    private GameObject TypeButtonPrefab;
    private GameObject MasterBoardNodePrefab;
    private GameObject MasterBoardPopupPrefab;
    private string mainType;
    UserItemsService userItemsService;
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        MasterBoardNodePrefab = UIManager.Instance.Get("MasterBoardNodePrefab");
        MasterBoardPopupPrefab = UIManager.Instance.Get("MasterBoardPopupPrefab");

        userItemsService = UserItemsService.Create();
        teamsService = TeamsService.Create();
    }
    public async Task CreateMasterBoardAsync(GameObject gameObject)
    {
        currentObject = gameObject;
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        Content = currentObject.transform.Find("DictionaryCards/Viewport/Content");
        Content.gameObject.AddComponent<DragToMoveUI>();

        List<string> uniqueTypes = new List<string>();
        Features features = new Features();
        uniqueTypes = await MasterBoardService.Create().GetUniqueNameAsync();
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var name in uniqueTypes)
            {
                GameObject button = Instantiate(TypeButtonPrefab, TabButtonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = name.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await OnButtonClickAsync(button, name);
                });

                if (index == 0)
                {
                    mainType = name;
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    // mainId = cardHeroes.id;
                    List<MasterBoard> masterBoards = await UserMasterBoardService.Create().GetUserMasterBoardAsync(User.CurrentUserId, name);
                    CreateMasterBoardUI(masterBoards);
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
                index = index + 1;
            }
        }
    }
    async Task OnButtonClickAsync(GameObject clickedButton, string type)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                UIManager.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL); // Giả sử bạn có texture trắng
            }
        }

        mainType = type;
        UIManager.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
        Content.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // mainId = cardHeroes.id;
        List<MasterBoard> masterBoards = await UserMasterBoardService.Create().GetUserMasterBoardAsync(User.CurrentUserId, type);
        CreateMasterBoardUI(masterBoards);
    }
    public void CreateMasterBoardUI(List<MasterBoard> masterBoards)
    {
        ButtonEvent.Instance.Close(Content);
        foreach (var masterBoard in masterBoards)
        {
            GameObject node = Instantiate(MasterBoardNodePrefab, Content);
            RectTransform rectTransform = node.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(masterBoard.PositionX * 105, masterBoard.PositionY * 105);

            RawImage mainImage = node.transform.Find("MainImage").GetComponent<RawImage>();
            Texture mainTexture = TextureHelper.LoadTextureCached($"UI/Master_Board/{masterBoard.Type}");
            mainImage.texture = mainTexture;

            RawImage backgroundImage = node.transform.Find("Background").GetComponent<RawImage>();
            if (masterBoard.Status.Equals("block"))
            {
                Texture backgroundTexture = TextureHelper.LoadTextureCached($"UI/Background4/Node_0");
                backgroundImage.texture = backgroundTexture;
                mainImage.color = Color.black;
            }
            else
            {
                Texture backgroundTexture = TextureHelper.LoadTextureCached($"UI/Background4/Node_5");
                backgroundImage.texture = backgroundTexture;
                mainImage.color = Color.white;
            }
            node.GetComponent<Button>().onClick.AddListener(async () => await ShowMasterBoardPopupAsync(masterBoard));
            // node.GetComponent<Button>().onClick.AddListener(() =>
            // {
            //     Debug.Log("Click OK");
            // });
        }
    }
    public async Task ShowMasterBoardPopupAsync(MasterBoard masterBoard)
    {
        GameObject popup = Instantiate(MasterBoardPopupPrefab, MainPanel);
        Button CloseButton = popup.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popup);
        });
        Button buyButton = popup.transform.Find("Buy").GetComponent<Button>();
        TextMeshProUGUI buttonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUY);

        RawImage mainImage = popup.transform.Find("MainImage").GetComponent<RawImage>();
        Texture mainTexture = TextureHelper.LoadTextureCached($"UI/Master_Board/{masterBoard.Type}");
        mainImage.texture = mainTexture;

        RawImage backgroundImage = popup.transform.Find("BackgroundImage").GetComponent<RawImage>();
        Texture backgroundTexture = TextureHelper.LoadTextureCached($"UI/Background4/Node_5");
        backgroundImage.texture = backgroundTexture;
        mainImage.color = Color.white;

        RawImage materialImage = popup.transform.Find("Material/MaterialImage").GetComponent<RawImage>();
        Items items = await userItemsService.GetUserItemByNameAsync("Attack Amulet");
        string fileNameWithoutExtension = items.Image.Split('.')[0];
        Texture materialTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        materialImage.texture = materialTexture;

        TextMeshProUGUI materialNameText = popup.transform.Find("Material/QuantityText").GetComponent<TextMeshProUGUI>();
        int materialQuantity = QualityEvaluator.CheckQuality(masterBoard.RankLevel);
        materialNameText.text = NumberFormatter.FormatNumber(materialQuantity, false);

        buyButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (masterBoard.Status.Equals("block"))
            {

                if (items.Quantity >= materialQuantity)
                {
                    items.Quantity = items.Quantity - materialQuantity;
                    await userItemsService.UpdateUserItemQuantityAsync(items);
                    // newanimeStats = EnhanceAnimeStats(animeStats, 1);
                    // double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    await UserMasterBoardService.Create().InsertUserMasterBoardAsync(User.CurrentUserId, masterBoard);
                    // double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    // FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                    Destroy(popup);
                    await CreateMasterBoardAsync(currentObject);
                }
                else
                {
                    NotificationManager.Instance.ShowNotification(MessageConstants.ITEM_NOT_ENOUGH);
                }
            }
            else
            {
                if (items.Quantity >= materialQuantity)
                {
                    items.Quantity = items.Quantity - materialQuantity;
                    await userItemsService.UpdateUserItemQuantityAsync(items);
                    masterBoard.RankLevel = QualityEvaluator.GetNextQuality(masterBoard.RankLevel);
                    // newanimeStats = EnhanceAnimeStats(animeStats, 1);
                    // double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    await UserMasterBoardService.Create().UpdateUserMasterBoardAsync(User.CurrentUserId, masterBoard);
                    // double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    // FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                    Destroy(popup);
                    await CreateMasterBoardAsync(currentObject);
                }
                else
                {
                    NotificationManager.Instance.ShowNotification(MessageConstants.ITEM_NOT_ENOUGH);
                }
            }
        });
    }
}