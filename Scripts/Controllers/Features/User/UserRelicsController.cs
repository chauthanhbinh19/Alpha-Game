using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserRelicsController : MonoBehaviour
{
    public static UserRelicsController Instance { get; private set; }
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
    public void CreateUserRelics(List<Relics> relics, Transform DictionaryContentPanel)
    {
        foreach (var relic in relics)
        {
            GameObject relicObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = relicObject.transform.Find("Title").GetComponent<Text>();
            Title.text = relic.name.Replace("_", " ");

            RawImage Image = relicObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = relic.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(relic, MainPanel));
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

            RawImage frameImage = relicObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = relicObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relic.rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    public void ShowRelicsDetails(Relics relics, GameObject currentObject)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(3, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(relics, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(relics, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(relics, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });

        GetDetails(relics, currentObject);
        ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is Relics relics)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = relics.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ImageManager.Instance.ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = relics.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = relics.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relics.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = relics.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, relics, currentObject);
        }
    }
    public void GetLevel(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Relics relics)
        {
            PropertyInfo[] properties = relics.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, relics, increasePerLevel, currentObject);

            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForLevel("Relics");
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Relics currentCard = new Relics();
                currentCard = UserRelicsService.Create().GetUserRelicsById(User.CurrentUserId, relics.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Relics newCard = new Relics();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserRelicsService.Create().GetNewLevelPower(relics, increasePerLevel);
                    UserRelicsService.Create().UpdateRelicsLevel(newCard, currentLevel + 1);
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
                Relics currentCard = UserRelicsService.Create().GetUserRelicsById(User.CurrentUserId, relics.id);
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
                    Relics newCard = UserRelicsService.Create().GetNewLevelPower(relics, levelsGained * increasePerLevel);
                    UserRelicsService.Create().UpdateRelicsLevel(newCard, currentLevel);
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
        if (obj is Relics relics)
        {
            PropertyInfo[] properties = relics.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(relics, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForBreakthourgh("Relics");
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (relics.star + 1).ToString();
            }
            GameObject relicsObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage relicsImage = relicsObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = relics.image.Replace(".png", "");
            Texture relicsTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            relicsImage.texture = relicsTexture;

            TextMeshProUGUI relicsQuantity = relicsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            relicsQuantity.text = relics.quantity.ToString() + "/" + (relics.star + 1).ToString();

            UIManager.Instance.CreateStarUI(relics.star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = relics.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng di vật
                bool hasEnoughRelics = relics.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + relics.quantity >= requiredQuantity;

                if (hasEnoughRelics || hasEnoughItems)
                {
                    // Giảm số lượng di vật trước
                    if (relics.quantity >= requiredQuantity)
                    {
                        relics.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu di vật không đủ, dùng cả di vật + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - relics.quantity;
                        relics.quantity = 0; // Dùng hết di vật

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
                    Relics newRelics = new Relics();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newRelics = UserRelicsService.Create().GetNewBreakthroughPower(relics, increasePerUpgrade);
                    UserRelicsService.Create().UpdateRelicsBreakthrough(newRelics, relics.star + 1, relics.quantity);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    GetUpgrade(obj, currentObject);
                    UIManager.Instance.CreateStarUI(relics.star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp di vật!");
                }
            });
        }
    }
}
