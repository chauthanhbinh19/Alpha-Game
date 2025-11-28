using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserEquipmentsController : MonoBehaviour
{
    public static UserEquipmentsController Instance { get; private set; }
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
        Initialize();
    }

    public void Initialize()
    {
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void ShowEquipmentsDetails(Equipments equipments, GameObject currentObject, int buttonType = 1)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(equipments, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(equipments, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });

        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(equipments, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(equipments, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                GetLevel(equipments, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(equipments, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                GetUpgrade(equipments, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            default:
                GetDetails(equipments, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is Equipments equipment)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipment.Image); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ImageManager.Instance.ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = equipment.Name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = NumberFormatter.FormatNumber(equipment.Power, false);

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.Rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = equipment.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, equipment, currentObject);
        }
    }
    public void GetLevel(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Equipments equipment)
        {
            PropertyInfo[] properties = equipment.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, equipment, increasePerLevel, currentObject);
            
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForLevel(AppConstants.MainType.EQUIPMENT);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Equipments currentCard = new Equipments();
                currentCard = UserEquipmentsService.Create().GetUserEquipmentsById(User.CurrentUserId, equipment.Id);
                double totalExperiment = currentCard.Experiment;
                int currentLevel = currentCard.Level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Equipments newCard = new Equipments();

                    newCard = UserEquipmentsService.Create().GetNewLevelPower(equipment, increasePerLevel);
                    UserEquipmentsService.Create().UpdateEquipmentsLevel(newCard, currentLevel + 1);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    GetLevel(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Equipments currentCard = UserEquipmentsService.Create().GetUserEquipmentsById(User.CurrentUserId, equipment.Id);
                double totalExperiment = currentCard.Experiment;
                int currentLevel = currentCard.Level;
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

                    Equipments newCard = UserEquipmentsService.Create().GetNewLevelPower(equipment, levelsGained * increasePerLevel);
                    UserEquipmentsService.Create().UpdateEquipmentsLevel(newCard, currentLevel);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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
        if (obj is Equipments equipments)
        {
            PropertyInfo[] properties = equipments.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(equipments, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForBreakthourgh(AppConstants.MainType.EQUIPMENT);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(items1.Image);
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.Quantity.ToString() + "/" + (equipments.Star + 1).ToString();
            }
            GameObject equipmentObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage equipmentImage = equipmentObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipments.Image);
            Texture equipTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            equipmentImage.texture = equipTexture;

            TextMeshProUGUI equipQuantity = equipmentObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            equipQuantity.text = equipments.Quantity.ToString() + "/" + (equipments.Star + 1).ToString();

            UIManager.Instance.CreateStarUI(equipments.Star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                double requiredQuantity = equipments.Star + 1;
                double totalItemQuantity = 0;

                // Kiểm tra số lượng trang bị
                bool hasEnoughEquipments = equipments.Quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.Quantity;
                }
                bool hasEnoughItems = totalItemQuantity + equipments.Quantity >= requiredQuantity;

                if (hasEnoughEquipments || hasEnoughItems)
                {
                    // Giảm số lượng trang bị trước
                    if (equipments.Quantity >= requiredQuantity)
                    {
                        equipments.Quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu trang bị không đủ, dùng cả trang bị + vật phẩm để bù vào
                        double remainingRequired = requiredQuantity - equipments.Quantity;
                        equipments.Quantity = 0; // Dùng hết trang bị

                        foreach (Items items1 in items)
                        {
                            if (remainingRequired <= 0) break; // Đã đủ vật phẩm để nâng cấp

                            if (items1.Quantity >= remainingRequired)
                            {
                                items1.Quantity -= remainingRequired;
                                remainingRequired = 0;
                            }
                            else
                            {
                                remainingRequired -= items1.Quantity;
                                items1.Quantity = 0; // Dùng hết vật phẩm này
                            }
                        }
                    }

                    foreach (Items items1 in items)
                    {
                        userItemsService.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Equipments newEquipment = new Equipments();

                    newEquipment = UserEquipmentsService.Create().GetNewBreakthroughPower(equipments, increasePerUpgrade);
                    UserEquipmentsService.Create().UpdateEquipmentsBreakthrough(newEquipment, equipments.Star + 1, equipments.Quantity);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    EquipmentsGalleryService.Create().UpdateStarEquipmentsGallery(equipments.Id, equipments.Star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    GetUpgrade(obj, currentObject);
                    UIManager.Instance.CreateStarUI(equipments.Star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp Equipments!");
                }
            });
        }
    }
}
