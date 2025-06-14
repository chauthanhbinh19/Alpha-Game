using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MasterBoardController : MonoBehaviour
{
    public static MasterBoardController Instance { get; private set; }
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform Content;
    private GameObject currentObject;
    private GameObject buttonPrefab;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        MasterBoardNodePrefab = UIManager.Instance.GetGameObject("MasterBoardNodePrefab");
        MasterBoardPopupPrefab = UIManager.Instance.GetGameObject("MasterBoardPopupPrefab");

        userItemsService = UserItemsService.Create();
        teamsService = TeamsService.Create();
    }
    public void CreateMasterBoard(GameObject gameObject)
    {
        currentObject = gameObject;
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        Content = currentObject.transform.Find("DictionaryCards/Viewport/Content");
        Content.gameObject.AddComponent<DragToMoveUI>();

        List<string> uniqueTypes = new List<string>();
        Features features = new Features();
        uniqueTypes = MasterBoardService.Create().GetUniqueName();
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var name in uniqueTypes)
            {
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = name.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, name));

                if (index == 0)
                {
                    mainType = name;
                    UIManager.Instance.ChangeButtonBackground(button, "Background_V4_166");
                    // mainId = cardHeroes.id;
                    List<MasterBoard> masterBoards = UserMasterBoardService.Create().GetUserMasterBoard(User.CurrentUserId, name);
                    CreateMasterBoardUI(masterBoards);
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, "Background_V4_167");
                }
                index = index + 1;
            }
        }
    }
    void OnButtonClick(GameObject clickedButton, string type)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                UIManager.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_167"); // Giả sử bạn có texture trắng
            }
        }

        mainType = type;
        UIManager.Instance.ChangeButtonBackground(clickedButton, "Background_V4_166");
        Content.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // mainId = cardHeroes.id;
        List<MasterBoard> masterBoards = UserMasterBoardService.Create().GetUserMasterBoard(User.CurrentUserId, type);
        CreateMasterBoardUI(masterBoards);
    }
    public void CreateMasterBoardUI(List<MasterBoard> masterBoards)
    {
        ButtonEvent.Instance.Close(Content);
        foreach (var masterBoard in masterBoards)
        {
            GameObject node = Instantiate(MasterBoardNodePrefab, Content);
            RectTransform rectTransform = node.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(masterBoard.position_x * 105, masterBoard.position_y * 105);

            RawImage mainImage = node.transform.Find("MainImage").GetComponent<RawImage>();
            Texture mainTexture = Resources.Load<Texture>($"UI/Master_Board/{masterBoard.type}");
            mainImage.texture = mainTexture;

            RawImage backgroundImage = node.transform.Find("Background").GetComponent<RawImage>();
            if (masterBoard.status.Equals("block"))
            {
                Texture backgroundTexture = Resources.Load<Texture>($"UI/Background4/Node_0");
                backgroundImage.texture = backgroundTexture;
                mainImage.color = Color.black;
            }
            else
            {
                Texture backgroundTexture = Resources.Load<Texture>($"UI/Background4/Node_5");
                backgroundImage.texture = backgroundTexture;
                mainImage.color = Color.white;
            }
            node.GetComponent<Button>().onClick.AddListener(() => ShowMasterBoardPopup(masterBoard));
            // node.GetComponent<Button>().onClick.AddListener(() =>
            // {
            //     Debug.Log("Click OK");
            // });
        }
    }
    public void ShowMasterBoardPopup(MasterBoard masterBoard)
    {
        GameObject popup = Instantiate(MasterBoardPopupPrefab, MainPanel);
        Button CloseButton = popup.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(popup));
        Button buyButton = popup.transform.Find("Buy").GetComponent<Button>();

        RawImage mainImage = popup.transform.Find("MainImage").GetComponent<RawImage>();
        Texture mainTexture = Resources.Load<Texture>($"UI/Master_Board/{masterBoard.type}");
        mainImage.texture = mainTexture;

        RawImage backgroundImage = popup.transform.Find("BackgroundImage").GetComponent<RawImage>();
        Texture backgroundTexture = Resources.Load<Texture>($"UI/Background4/Node_5");
        backgroundImage.texture = backgroundTexture;
        mainImage.color = Color.white;

        RawImage materialImage = popup.transform.Find("Material/MaterialImage").GetComponent<RawImage>();
        Items items = userItemsService.GetUserItemByName("Attack Amulet");
        string fileNameWithoutExtension = items.image.Split('.')[0];
        Texture materialTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        materialImage.texture = materialTexture;

        TextMeshProUGUI materialNameText = popup.transform.Find("Material/QuantityText").GetComponent<TextMeshProUGUI>();
        int materialQuantity = QualityEvaluator.CheckQuality(masterBoard.rank_level);
        materialNameText.text = materialQuantity.ToString();

        buyButton.onClick.AddListener(() =>
        {
            if (masterBoard.status.Equals("block"))
            {

                if (items.quantity >= materialQuantity)
                {
                    items.quantity = items.quantity - materialQuantity;
                    userItemsService.UpdateUserItemsQuantity(items);
                    // newanimeStats = EnhanceAnimeStats(animeStats, 1);
                    // double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    UserMasterBoardService.Create().InsertUserMasterBoard(User.CurrentUserId, masterBoard);
                    // double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    // FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                    Destroy(popup);
                    CreateMasterBoard(currentObject);
                }
                else
                {
                    NotificationManager.Instance.ShowNotification(MessageHelper.ItemConstants.ItemNotEnough);
                }
            }
            else
            {
                if (items.quantity >= materialQuantity)
                {
                    items.quantity = items.quantity - materialQuantity;
                    userItemsService.UpdateUserItemsQuantity(items);
                    masterBoard.rank_level = QualityEvaluator.GetNextQuality(masterBoard.rank_level);
                    // newanimeStats = EnhanceAnimeStats(animeStats, 1);
                    // double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    UserMasterBoardService.Create().UpdateUserMasterBoard(User.CurrentUserId, masterBoard);
                    // double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    // FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                    Destroy(popup);
                    CreateMasterBoard(currentObject);
                }
                else
                {
                    NotificationManager.Instance.ShowNotification(MessageHelper.ItemConstants.ItemNotEnough);
                }
            }
        });
    }
}