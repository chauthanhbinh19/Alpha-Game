using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserBooksController : MonoBehaviour
{
    public static UserBooksController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private GameObject PositionPrefab;
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
        cardsPrefab = UIManager.Instance.Get("CardsPrefab");
        PositionPrefab = UIManager.Instance.Get("PositionPrefab");
        ElementDetails2Prefab = UIManager.Instance.Get("ElementDetails2Prefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void CreateUserBooks(List<Books> books, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.BOOK_BUTTON_BACKGROUND_URL);

        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(cardsPrefab, contentPanel);
            Transform transform = bookObject.transform;

            Text titleText = transform.Find("Title").GetComponent<Text>();
            titleText.text = book.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image);
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

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(book.Rare));
            rareText.text = book.Rare;

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                MainMenuDetailsManager.Instance.PopupDetails(book, MainPanel);
            });

            GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(260, 320);
            }
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateUserBooksForSummon(List<Books> books, Transform PositionPanel)
    {
        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(PositionPrefab, PositionPanel);
            Transform transform = bookObject.transform;

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;
            image.SetNativeSize();

            // Chỉnh width và height
            RectTransform rectTransform = image.rectTransform;
            // rectTransform.sizeDelta = new Vector2(300f, 375f);
            if (texture.width < 1400 && texture.height < 1400 && texture.width > 700 && texture.height > 700)
            {
                image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (texture.width > 1000 && texture.height <= 2100 && texture.width < 2000 && texture.height > 1000)
            {
                image.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
            else if (texture.width <= 700 && texture.height <= 700)
            {
                image.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            else if (texture.width <= 700 && texture.height <= 1100)
            {
                image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (texture.width > 700 && texture.height <= 700)
            {
                image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else
            {
                image.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            GridLayoutGroup gridLayout = PositionPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.spacing = new Vector2(100, 0);
            }
        }
    }
    public void ShowBookDetails(Books book, GameObject currentObject, int buttonType = 1)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(3, "Skills", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(book, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            _ = GetLevelAsync(book, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(book, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            _ = GetUpgradeAsync(book, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(book, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                _ = GetLevelAsync(book, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(book, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                _ = GetUpgradeAsync(book, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            default:
                GetDetails(book, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        Transform transform = currentObject.transform;
        if (obj is Books book)
        {
            RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image); // Lấy giá trị của image từ đối tượng Card
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");

            ImageManager.Instance.ChangeSizeImage(image, texture, 700f);
            image.texture = texture;

            TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            nameText.text = book.Name;

            TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            powerText.text = NumberFormatterHelper.FormatNumber(book.Power, false);

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{book.Rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = book.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, book, currentObject);
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
        if (obj is Books book)
        {
            PropertyInfo[] properties = book.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, book, INCREASE_PER_LEVEL, currentObject);

            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForLevelAsync(AppConstants.MainType.BOOK);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Books currentBook = new Books();
                currentBook = await UserBooksService.Create().GetUserBookByIdAsync(User.CurrentUserId, book.Id);
                double totalExperiment = currentBook.Experiment;
                int currentLevel = currentBook.Level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Books newBook = new Books();

                    newBook = await UnitLevelHelper.GetNewLevelPowerAsync(book, INCREASE_PER_LEVEL);
                    await UserBooksService.Create().UpdateBookLevelAsync(newBook, currentLevel + 1);
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
                Books currentBook = await UserBooksService.Create().GetUserBookByIdAsync(User.CurrentUserId, book.Id);
                double totalExperiment = currentBook.Experiment;
                int currentLevel = currentBook.Level;
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

                    Books newBook = await UnitLevelHelper.GetNewLevelPowerAsync(book, levelsGained * INCREASE_PER_LEVEL);
                    await UserBooksService.Create().UpdateBookLevelAsync(newBook, currentLevel);
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
        Transform transform = currentObject.transform;
        Button breakthroughButton = transform.Find("DictionaryCards/Content/UpgradePanel/BreakthroughButton").GetComponent<Button>();
        Transform UpgradeElementContent = transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        Transform UpgradeMaterialContent = transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Books book)
        {
            PropertyInfo[] properties = book.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(book, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, INCREASE_PER_UPGRADE, currentObject);
            }
            List<Items> items = new List<Items>();
            items = await userItemsService.GetItemForBreakthourghAsync(AppConstants.MainType.BOOK);
            string fileNameWithoutExtension = "";
            foreach (Items item in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);
                Transform itemTransform = itemObject.transform;

                RawImage eImage = itemTransform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageHelper.RemoveImageExtension(item.Image);
                Texture equipmentTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = item.Quantity.ToString() + "/" + (book.Star + 1).ToString();
            }
            GameObject bookObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);
            Transform bookTransform = bookObject.transform;

            RawImage bookImage = bookTransform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image);
            Texture bookTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            bookImage.texture = bookTexture;

            TextMeshProUGUI bookQuantity = bookTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            bookQuantity.text = book.Quantity.ToString() + "/" + (book.Star + 1).ToString();

            UIManager.Instance.CreateStarUI(book.Star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                double requiredQuantity = book.Star + 1;
                double totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughBook = book.Quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items item in items)
                {
                    totalItemQuantity += item.Quantity;
                }
                bool hasEnoughItem = totalItemQuantity + book.Quantity >= requiredQuantity;

                if (hasEnoughBook || hasEnoughItem)
                {
                    // Giảm số lượng thẻ bài trước
                    if (book.Quantity >= requiredQuantity)
                    {
                        book.Quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        double remainingRequired = requiredQuantity - book.Quantity;
                        book.Quantity = 0; // Dùng hết thẻ bài

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
                    Books newBook = new Books();

                    newBook = await UnitBreakthroughHelper.GetNewBreakthroughPowerAsync(book, INCREASE_PER_UPGRADE);
                    await UserBooksService.Create().UpdateBookBreakthroughAsync(newBook, book.Star + 1, book.Quantity);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                    await BooksGalleryService.Create().UpdateStarBookGalleryAsync(book.Id, book.Star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    await GetUpgradeAsync(obj, currentObject);
                    UIManager.Instance.CreateStarUI(book.Star, currentObject);
                }
                else
                {
                    Debug.Log(MessageConstants.ITEM_NOT_ENOUGH);
                }
            });
        }
    }
}
