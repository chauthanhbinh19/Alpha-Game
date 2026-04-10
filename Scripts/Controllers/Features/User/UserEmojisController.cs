using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserEmojisController : MonoBehaviour
{
    public static UserEmojisController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject EmojiButtonPrefab;
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
        EmojiButtonPrefab = UIManager.Instance.Get("EmojiButtonPrefab");
        ElementDetails2Prefab = UIManager.Instance.Get("ElementDetails2Prefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void CreateUserEmojis(List<Emojis> emojis, Transform contentPanel)
    {
        foreach (var emoji in emojis)
        {
            GameObject emojiObject = Instantiate(EmojiButtonPrefab, contentPanel);

            TextMeshProUGUI titleText = emojiObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = emoji.Name.Replace("_", " ");

            RawImage image = emojiObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(emoji.Image);
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

            RawImage backgroundImage = emojiObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.EMOJI_BUTTON_BACKGROUND_URL);

            Button button = emojiObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                MainMenuDetailsManager.Instance.PopupDetails(emoji, MainPanel);
            });

            TextMeshProUGUI rareText = emojiObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(emoji.Rare));
            rareText.text = emoji.Rare;
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 240);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void ShowEmojiDetails(Emojis emoji, GameObject currentObject, int buttonType = 1)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(emoji, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            _ = GetLevelAsync(emoji, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            _ = GetUpgradeAsync(emoji, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(emoji, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                _ = GetLevelAsync(emoji, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(emoji, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                _ = GetUpgradeAsync(emoji, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            default:
                GetDetails(emoji, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is Emojis emoji)
        {
            RawImage image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(emoji.Image); // Lấy giá trị của image từ đối tượng Card
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;
            ImageManager.Instance.ChangeSizeImage(image, texture);

            TextMeshProUGUI nameText = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            nameText.text = emoji.Name;

            TextMeshProUGUI powerText = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            powerText.text = NumberFormatter.FormatNumber(emoji.Power, false);

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{emoji.Rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = emoji.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, emoji, currentObject);
        }
    }
    public async Task GetLevelAsync(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Emojis emoji)
        {
            PropertyInfo[] properties = emoji.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, emoji, increasePerLevel, currentObject);

            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForLevelAsync(AppConstants.MainType.TITLE);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Emojis currentEmoji = new Emojis();
                currentEmoji = await UserEmojisService.Create().GetUserEmojiByIdAsync(User.CurrentUserId, emoji.Id);
                double totalExperiment = currentEmoji.Experiment;
                int currentLevel = currentEmoji.Level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Emojis newEmoji = new Emojis();

                    newEmoji = await UserEmojisService.Create().GetNewLevelPowerAsync(emoji, increasePerLevel);
                    await UserEmojisService.Create().UpdateEmojiLevelAsync(newEmoji, currentLevel + 1);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
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
                Emojis currentEmoji = await UserEmojisService.Create().GetUserEmojiByIdAsync(User.CurrentUserId, emoji.Id);
                double totalExperiment = currentEmoji.Experiment;
                int currentLevel = currentEmoji.Level;
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

                    Emojis newEmoji = await UserEmojisService.Create().GetNewLevelPowerAsync(emoji, levelsGained * increasePerLevel);
                    await UserEmojisService.Create().UpdateEmojiLevelAsync(newEmoji, currentLevel);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
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
        if (obj is Emojis emoji)
        {
            PropertyInfo[] properties = emoji.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(emoji, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForBreakthourghAsync(AppConstants.MainType.TITLE);
            string fileNameWithoutExtension = "";
            foreach (Items item in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(item.Image);
                Texture itemTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = item.Quantity.ToString() + "/" + (emoji.Star + 1).ToString();
            }
            GameObject emojiObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage emojiImage = emojiObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(emoji.Image);
            Texture emojiTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            emojiImage.texture = emojiTexture;

            TextMeshProUGUI emojiQuantity = emojiObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            emojiQuantity.text = emoji.Quantity.ToString() + "/" + (emoji.Star + 1).ToString();

            UIManager.Instance.CreateStarUI(emoji.Star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                double requiredQuantity = emoji.Star + 1;
                double totalItemQuantity = 0;

                // Kiểm tra số lượng danh hiệu
                bool hasEnoughEmoji = emoji.Quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items item in items)
                {
                    totalItemQuantity += item.Quantity;
                }
                bool hasEnoughItem = totalItemQuantity + emoji.Quantity >= requiredQuantity;

                if (hasEnoughEmoji || hasEnoughItem)
                {
                    // Giảm số lượng danh hiệu trước
                    if (emoji.Quantity >= requiredQuantity)
                    {
                        emoji.Quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu danh hiệu không đủ, dùng cả danh hiệu + vật phẩm để bù vào
                        double remainingRequired = requiredQuantity - emoji.Quantity;
                        emoji.Quantity = 0; // Dùng hết danh hiệu

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
                    Emojis newEmoji = new Emojis();

                    newEmoji = await UserEmojisService.Create().GetNewBreakthroughPowerAsync(emoji, increasePerUpgrade);
                    await UserEmojisService.Create().UpdateEmojiBreakthroughAsync(newEmoji, emoji.Star + 1, emoji.Quantity);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    await EmojisGalleryService.Create().UpdateStarEmojiGalleryAsync(emoji.Id, emoji.Star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    await GetUpgradeAsync(obj, currentObject);
                    UIManager.Instance.CreateStarUI(emoji.Star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp danh hiệu!");
                }
            });
        }
    }
}
