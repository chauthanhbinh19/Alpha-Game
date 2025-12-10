using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserVehiclesController : MonoBehaviour
{
    public static UserVehiclesController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject VehicleButtonPrefab;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        VehicleButtonPrefab = UIManager.Instance.Get("VehicleButtonPrefab");
        ElementDetails2Prefab = UIManager.Instance.Get("ElementDetails2Prefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void CreateUserVehicle(List<Vehicles> vehicles, Transform contentPanel)
    {
        foreach (var vehicle in vehicles)
        {
            GameObject vehicleObject = Instantiate(VehicleButtonPrefab, contentPanel);

            TextMeshProUGUI Title = vehicleObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = vehicle.Name.Replace("_", " ");

            RawImage image = vehicleObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(vehicle.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            image.texture = texture;

            // Kích thước của RawImage (khung hiển thị)
            RectTransform rect = image.GetComponent<RectTransform>();
            float maxWidth = rect.rect.width;
            float maxHeight = rect.rect.height;

            // Kích thước thật của texture
            float texWidth = texture.width;
            float texHeight = texture.height;

            // Tính scale để texture nằm gọn trong khung
            float widthRatio = maxWidth / texWidth;
            float heightRatio = maxHeight / texHeight;
            float finalScale = Mathf.Min(widthRatio, heightRatio);  // scale nhỏ nhất

            // Áp dụng scale theo tỉ lệ đúng
            image.SetNativeSize();
            image.transform.localScale = new Vector3(finalScale, finalScale, 1f);

            RawImage backgroundImage = vehicleObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.VEHICLE_BUTTON_BACKGROUND_URL);

            Button button = vehicleObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                MainMenuDetailsManager.Instance.PopupDetails(vehicle, MainPanel);
            });

            RawImage frameImage = vehicleObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = vehicleObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{vehicle.Rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void ShowVehicleDetails(Vehicles Vehicle, GameObject currentObject, int buttonType = 1)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(Vehicle, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            _=GetLevelAsync(Vehicle, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            _=GetUpgradeAsync(Vehicle, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(Vehicle, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                _=GetLevelAsync(Vehicle, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(Vehicle, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                _=GetUpgradeAsync(Vehicle, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            default:
                GetDetails(Vehicle, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is Vehicles Vehicle)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Vehicle.Image); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ImageManager.Instance.ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = Vehicle.Name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = NumberFormatter.FormatNumber(Vehicle.Power, false);

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Vehicle.Rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = Vehicle.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, Vehicle, currentObject);
        }
    }
    public async Task GetLevelAsync(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Vehicles Vehicle)
        {
            PropertyInfo[] properties = Vehicle.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, Vehicle, increasePerLevel, currentObject);

            Items item = new Items();
            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForLevelAsync(AppConstants.MainType.VEHICLE);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Vehicles currentCard = new Vehicles();
                currentCard = await UserVehicleService.Create().GetUserVehicleByIdAsync(User.CurrentUserId, Vehicle.Id);
                double totalExperiment = currentCard.Experiment;
                int currentLevel = currentCard.Level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Vehicles newCard = new Vehicles();

                    newCard = await UserVehicleService.Create().GetNewLevelPowerAsync(Vehicle, increasePerLevel);
                    await UserVehicleService.Create().UpdateVehicleLevelAsync(newCard, currentLevel + 1);
                    double newPower =  await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    await GetLevelAsync(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
            upMaxLevelButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Vehicles currentCard = await UserVehicleService.Create().GetUserVehicleByIdAsync(User.CurrentUserId, Vehicle.Id);
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

                    Vehicles newCard = await UserVehicleService.Create().GetNewLevelPowerAsync(Vehicle, levelsGained * increasePerLevel);
                    await UserVehicleService.Create().UpdateVehicleLevelAsync(newCard, currentLevel);
                    double newPower =  await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    await GetLevelAsync(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
        }
    }
    public void GetSkills(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonSkillsPanels();
    }
    public async Task GetUpgradeAsync(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonUpgradePanels();
         Button breakthroughButton = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/BreakthroughButton").GetComponent<Button>();
        Transform UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        Transform UpgradeMaterialContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Vehicles Vehicle)
        {
            PropertyInfo[] properties = Vehicle.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(Vehicle, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForBreakthourghAsync(AppConstants.MainType.VEHICLE);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(items1.Image);
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.Quantity.ToString() + "/" + (Vehicle.Star + 1).ToString();
            }
            GameObject magicFormationObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage magicFormationImage = magicFormationObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Vehicle.Image);
            Texture magicFormationTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            magicFormationImage.texture = magicFormationTexture;

            TextMeshProUGUI magicFormationQuantity = magicFormationObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            magicFormationQuantity.text = Vehicle.Quantity.ToString() + "/" + (Vehicle.Star + 1).ToString();

            UIManager.Instance.CreateStarUI(Vehicle.Star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                double requiredQuantity = Vehicle.Star + 1;
                double totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughMagicFormation = Vehicle.Quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.Quantity;
                }
                bool hasEnoughItems = totalItemQuantity + Vehicle.Quantity >= requiredQuantity;

                if (hasEnoughMagicFormation || hasEnoughItems)
                {
                    // Giảm số lượng vòng phép trước
                    if (Vehicle.Quantity >= requiredQuantity)
                    {
                        Vehicle.Quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        double remainingRequired = requiredQuantity - Vehicle.Quantity;
                        Vehicle.Quantity = 0; // Dùng hết vòng phép

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
                        await userItemsService.UpdateUserItemQuantityAsync(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Vehicles newVehicle = new Vehicles();

                    newVehicle = await UserVehicleService.Create().GetNewBreakthroughPowerAsync(Vehicle, increasePerUpgrade);
                    await UserVehicleService.Create().UpdateVehicleBreakthroughAsync(newVehicle, Vehicle.Star + 1, Vehicle.Quantity);
                    double newPower =  await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    await VehicleGalleryService.Create().UpdateStarVehicleGalleryAsync(Vehicle.Id, Vehicle.Star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    await GetUpgradeAsync(obj, currentObject);
                    UIManager.Instance.CreateStarUI(Vehicle.Star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp vòng phép!");
                }
            });
        }
    }
}
