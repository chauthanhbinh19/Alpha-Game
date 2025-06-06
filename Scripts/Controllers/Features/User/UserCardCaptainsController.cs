using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserCardCaptainsController : MonoBehaviour
{
    public static UserCardCaptainsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private GameObject PositionPrefab;
    private GameObject ElementDetails2Prefab;
    private double increasePerLevel = 0.01;
    private double increasePerUpgrade = 1.1;
    private TeamsService teamsService;
    private UserItemsService userItemsService;
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
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        PositionPrefab = UIManager.Instance.GetGameObject("PositionPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateUserCardCaptains(List<CardCaptains> captainsList, Transform DictionaryContentPanel)
    {
        foreach (var captain in captainsList)
        {
            GameObject captainsObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = captainsObject.transform.Find("Title").GetComponent<Text>();
            Title.text = captain.name.Replace("_", " ");

            RawImage Image = captainsObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = captain.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = captainsObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captain.rare}");
            rareImage.texture = rareTexture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(captain, MainPanel));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = DictionaryContentPanel.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 250);
            }
        }
    }
    public void CreateUserCardCaptainsForSummon(List<CardCaptains> captains, Transform PositionPanel)
    {
        foreach (var captain in captains)
        {
            GameObject captainObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = captainObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = captain.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            // if (gridLayout != null)
            // {
            //     gridLayout.cellSize = new Vector2(200, 250);
            // }
        }
    }
    public void ShowCardCaptainsDetails(CardCaptains cardCaptains, GameObject currentObject)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(3, "Skills", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardCaptains, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardCaptains, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardCaptains, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardCaptains, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });

        GetDetails(cardCaptains, currentObject);
        ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is CardCaptains cardCaptains)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardCaptains.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardCaptains.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardCaptains.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardCaptains.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardCaptains.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, cardCaptains, currentObject);
        }
    }
    public void GetLevel(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CardCaptains cardCaptains)
        {
            PropertyInfo[] properties = cardCaptains.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, cardCaptains, increasePerLevel, currentObject);
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForLevel("CardCaptains");
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardCaptains currentCard = new CardCaptains();
                currentCard = UserCardCaptainsService.Create().GetUserCardCaptainsById(User.CurrentUserId, cardCaptains.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardCaptains newCard = new CardCaptains();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCardCaptainsService.Create().GetNewLevelPower(cardCaptains, increasePerLevel);
                    UserCardCaptainsService.Create().UpdateCardCaptainsLevel(newCard, currentLevel + 1);
                    UserCardCaptainsService.Create().UpdateFactCardCaptains(newCard);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    GetLevel(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardCaptains currentCard = UserCardCaptainsService.Create().GetUserCardCaptainsById(User.CurrentUserId, cardCaptains.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
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

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    CardCaptains newCard = UserCardCaptainsService.Create().GetNewLevelPower(cardCaptains, levelsGained * increasePerLevel);
                    UserCardCaptainsService.Create().UpdateCardCaptainsLevel(newCard, currentLevel);
                    UserCardCaptainsService.Create().UpdateFactCardCaptains(newCard);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    GetLevel(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
        }
    }
    public void GetSkills(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonSkillsPanels();
    }
    public void GetUpgrade(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonUpgradePanels();
        Button breakthroughButton = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/BreakthroughButton").GetComponent<Button>();
        Transform UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        Transform UpgradeMaterialContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CardCaptains cardCaptains)
        {
            PropertyInfo[] properties = cardCaptains.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardCaptains, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForBreakthourgh("CardCaptains");
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardCaptains.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardCaptains.quantity.ToString() + "/" + (cardCaptains.star + 1).ToString();

            UIManager.Instance.CreateStarUI(cardCaptains.star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardCaptains.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardCaptains.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardCaptains.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardCaptains.quantity >= requiredQuantity)
                    {
                        cardCaptains.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardCaptains.quantity;
                        cardCaptains.quantity = 0; // Dùng hết thẻ bài

                        foreach (Items items1 in items)
                        {
                            if (remainingRequired <= 0) break; // Đã đủ vật phẩm để nâng cấp

                            if (items1.quantity >= remainingRequired)
                            {
                                items1.quantity -= remainingRequired;
                                remainingRequired = 0;
                            }
                            else
                            {
                                remainingRequired -= items1.quantity;
                                items1.quantity = 0; // Dùng hết vật phẩm này
                            }
                        }
                    }

                    foreach (Items items1 in items)
                    {
                        userItemsService.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardCaptains newCard = new CardCaptains();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCardCaptainsService.Create().GetNewBreakthroughPower(cardCaptains, increasePerUpgrade);
                    UserCardCaptainsService.Create().UpdateCardCaptainsBreakthrough(newCard, cardCaptains.star + 1, cardCaptains.quantity);
                    UserCardCaptainsService.Create().UpdateFactCardCaptains(newCard);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    GetUpgrade(obj, currentObject);
                    UIManager.Instance.CreateStarUI(cardCaptains.star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
    }
}
