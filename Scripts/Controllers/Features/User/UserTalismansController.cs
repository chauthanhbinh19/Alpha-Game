using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserTalismansController : MonoBehaviour
{
    public static UserTalismansController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject TalismanButtonPrefab;
    private GameObject ElementDetails2Prefab;
    private const double INCREASE_PER_LEVEL = 0.01;
    private const double INCREASE_PER_UPGRADE = 1.1;
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
        TalismanButtonPrefab = UIManager.Instance.Get("TalismanButtonPrefab");
        ElementDetails2Prefab = UIManager.Instance.Get("ElementDetails2Prefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void CreateUserTalismans(List<Talismans> talismans, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.TALISMAN_BUTTON_BACKGROUND_URL);

        foreach (var talisman in talismans)
        {
            GameObject talismanObject = Instantiate(TalismanButtonPrefab, contentPanel);
            Transform transform = talismanObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = talisman.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(talisman.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
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

            RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = bgTexture;

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                MainMenuDetailsManager.Instance.PopupDetails(talisman, MainPanel);
            });

            RawImage frameImage = transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(talisman.Rare));
            rareText.text = talisman.Rare;

        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 240);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void ShowTalismanDetails(Talismans talisman, GameObject currentObject, int buttonType = 1)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(talisman, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            _=GetLevelAsync(talisman, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            _=GetUpgradeAsync(talisman, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(talisman, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                _=GetLevelAsync(talisman, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(talisman, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                _=GetUpgradeAsync(talisman, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            default:
                GetDetails(talisman, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        Transform transform = currentObject.transform;
        if (obj is Talismans talisman)
        {
            RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(talisman.Image); // Lấy giá trị của image từ đối tượng Card
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;
            ImageManager.Instance.ChangeSizeImage(image, texture);

            TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            nameText.text = talisman.Name;

            TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            powerText.text = NumberFormatterHelper.FormatNumber(talisman.Power, false);

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{talisman.Rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = talisman.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, talisman, currentObject);
        }
    }
    public async Task GetLevelAsync(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Transform transform = currentObject.transform;
        Button up1LevelButton = transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Talismans talisman)
        {
            PropertyInfo[] properties = talisman.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, talisman, INCREASE_PER_LEVEL, currentObject);

            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForLevelAsync(AppConstants.MainType.TALISMAN);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Talismans currentTalisman = new Talismans();
                currentTalisman = await UserTalismansService.Create().GetUserTalismanByIdAsync(User.CurrentUserId, talisman.Id);
                double totalExperiment = currentTalisman.Experiment;
                int currentLevel = currentTalisman.Level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Talismans newTalisman = new Talismans();

                    newTalisman = await UnitLevelHelper.GetNewLevelPowerAsync(talisman, INCREASE_PER_LEVEL);
                    await UserTalismansService.Create().UpdateTalismanLevelAsync(newTalisman, currentLevel + 1);
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
                Talismans currentTalisman = await UserTalismansService.Create().GetUserTalismanByIdAsync(User.CurrentUserId, talisman.Id);
                double totalExperiment = currentTalisman.Experiment;
                int currentLevel = currentTalisman.Level;
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

                    Talismans newTalisman = await UnitLevelHelper.GetNewLevelPowerAsync(talisman, levelsGained * INCREASE_PER_LEVEL);
                    await UserTalismansService.Create().UpdateTalismanLevelAsync(newTalisman, currentLevel);
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
        Transform transform = currentObject.transform;
        Button breakthroughButton = transform.Find("DictionaryCards/Content/UpgradePanel/BreakthroughButton").GetComponent<Button>();
        Transform UpgradeElementContent = transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        Transform UpgradeMaterialContent = transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Talismans talisman)
        {
            PropertyInfo[] properties = talisman.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(talisman, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, INCREASE_PER_UPGRADE, currentObject);
            }
            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForBreakthourghAsync(AppConstants.MainType.TALISMAN);
            string fileNameWithoutExtension = "";
            foreach (Items item in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);
                Transform itemTransform = itemObject.transform;

                RawImage eImage = itemTransform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageHelper.RemoveImageExtension(item.Image);
                Texture itemTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = item.Quantity.ToString() + "/" + (talisman.Star + 1).ToString();
            }
            GameObject talismanObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);
            Transform talismanTransform = talismanObject.transform;

            RawImage talismanImage = talismanTransform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageHelper.RemoveImageExtension(talisman.Image);
            Texture talismanTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            talismanImage.texture = talismanTexture;

            TextMeshProUGUI talismanQuantity = talismanTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            talismanQuantity.text = talisman.Quantity.ToString() + "/" + (talisman.Star + 1).ToString();

            UIManager.Instance.CreateStarUI(talisman.Star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                double requiredQuantity = talisman.Star + 1;
                double totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughTalisman = talisman.Quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items item in items)
                {
                    totalItemQuantity += item.Quantity;
                }
                bool hasEnoughItem = totalItemQuantity + talisman.Quantity >= requiredQuantity;

                if (hasEnoughTalisman || hasEnoughItem)
                {
                    // Giảm số lượng vòng phép trước
                    if (talisman.Quantity >= requiredQuantity)
                    {
                        talisman.Quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        double remainingRequired = requiredQuantity - talisman.Quantity;
                        talisman.Quantity = 0; // Dùng hết vòng phép

                        foreach (Items item in items)
                        {
                            if (remainingRequired <= 0) break; // Đã đủ vật phẩm để nâng cấp

                            if (item.Quantity >= remainingRequired)
                            {
                                item.Quantity -= remainingRequired;
                                remainingRequired = 0;
                            }
                            else
                            {
                                remainingRequired -= item.Quantity;
                                item.Quantity = 0; // Dùng hết vật phẩm này
                            }
                        }
                    }

                    foreach (Items item in items)
                    {
                        await userItemsService.UpdateUserItemQuantityAsync(item);
                    }
                    // Cập nhật cấp sao (Star)
                    Talismans newTalisman = new Talismans();

                    newTalisman = await UnitBreakthroughHelper.GetNewBreakthroughPowerAsync(talisman, INCREASE_PER_UPGRADE);
                    await UserTalismansService.Create().UpdateTalismanBreakthroughAsync(newTalisman, talisman.Star + 1, talisman.Quantity);
                    double newPower =  await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    await TalismansGalleryService.Create().UpdateStarTalismanGalleryAsync(talisman.Id, talisman.Star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    await GetUpgradeAsync(obj, currentObject);
                    UIManager.Instance.CreateStarUI(talisman.Star, currentObject);
                }
                else
                {
                    Debug.Log(MessageConstants.ITEM_NOT_ENOUGH);
                }
            });
        }
    }
}
