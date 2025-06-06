using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserCollaborationEquipmentController : MonoBehaviour
{
    public static UserCollaborationEquipmentController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject equipmentsPrefab;
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
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateUserCollaborationEquipments(List<CollaborationEquipment> collaborationEquipmentList, Transform DictionaryContentPanel)
    {
        foreach (var collaborationEquipment in collaborationEquipmentList)
        {
            GameObject collaborationEquipmentObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = collaborationEquipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = collaborationEquipment.name.Replace("_", " ");

            RawImage Image = collaborationEquipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(collaborationEquipment, MainPanel));
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

            RawImage rareImage = collaborationEquipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
            rareImage.texture = rareTexture;

            GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    public void ShowCollaborationEquipmentsDetails(CollaborationEquipment collaborationEquipment, GameObject currentObject)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(3, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(collaborationEquipment, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(collaborationEquipment, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetUpgrade(collaborationEquipment, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
        });

        GetDetails(collaborationEquipment, currentObject);
        ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is CollaborationEquipment collaborationEquipment)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ImageManager.Instance.ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = collaborationEquipment.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = collaborationEquipment.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, collaborationEquipment, currentObject);
        }
    }
    public void GetLevel(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CollaborationEquipment collaborationEquipment)
        {
            PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, collaborationEquipment, increasePerLevel, currentObject);

            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForLevel("CollaborationEquipments");
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CollaborationEquipment currentCard = new CollaborationEquipment();
                currentCard = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentsById(User.CurrentUserId, collaborationEquipment.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CollaborationEquipment newCard = new CollaborationEquipment();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCollaborationEquipmentService.Create().GetNewLevelPower(collaborationEquipment, increasePerLevel);
                    UserCollaborationEquipmentService.Create().UpdateCollaborationEquipmentsLevel(newCard, currentLevel + 1);
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
                CollaborationEquipment currentCard = UserCollaborationEquipmentService.Create().GetUserCollaborationEquipmentsById(User.CurrentUserId, collaborationEquipment.id);
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
                    CollaborationEquipment newCard = UserCollaborationEquipmentService.Create().GetNewLevelPower(collaborationEquipment, levelsGained * increasePerLevel);
                    UserCollaborationEquipmentService.Create().UpdateCollaborationEquipmentsLevel(newCard, currentLevel);
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
        if (obj is CollaborationEquipment collaborationEquipment)
        {
            PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(collaborationEquipment, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForBreakthourgh("CollaborationEquipments");
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (collaborationEquipment.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = collaborationEquipment.quantity.ToString() + "/" + (collaborationEquipment.star + 1).ToString();

            UIManager.Instance.CreateStarUI(collaborationEquipment.star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = collaborationEquipment.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = collaborationEquipment.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + collaborationEquipment.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (collaborationEquipment.quantity >= requiredQuantity)
                    {
                        collaborationEquipment.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - collaborationEquipment.quantity;
                        collaborationEquipment.quantity = 0; // Dùng hết thẻ bài

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
                    CollaborationEquipment newCard = new CollaborationEquipment();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCollaborationEquipmentService.Create().GetNewBreakthroughPower(collaborationEquipment, increasePerUpgrade);
                    UserCollaborationEquipmentService.Create().UpdateCollaborationEquipmentsBreakthrough(newCard, collaborationEquipment.star + 1, collaborationEquipment.quantity);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    GetUpgrade(obj, currentObject);
                    UIManager.Instance.CreateStarUI(collaborationEquipment.star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
    }
}
