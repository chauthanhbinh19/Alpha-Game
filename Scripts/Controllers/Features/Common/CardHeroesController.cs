using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHeroesController : MonoBehaviour
{
    public static CardHeroesController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ShopPanelPrefab;
    private GameObject CardHeroButtonPrefab;
    private GameObject EquipmentShopPrefab;
    private GameObject QuantityPopupPrefab;
    private GameObject ReceivedNotificationPanelPrefab;
    private GameObject ItemPopupPrefab;
    private PaginationManager PaginationManager;
    private Transform contentTransform;
    private int Offset = 0;
    private int CurrentPage = 1;
    private int TotalItems;
    private const int PAGE_SIZE = 100;
    private bool IsSearchingOrFiltering = false;
    private string ShopCodeName = "";
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        ShopPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Shop.SHOP_PANEL_PREFAB);
        CardHeroButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.CARD_HERO_BUTTON_PREFAB);
        EquipmentShopPrefab = UIManager.Instance.Get(AppConstants.Prefab.Equipment.EQUIPMENT_SHOP_PREFAB);
        QuantityPopupPrefab = UIManager.Instance.Get(AppConstants.Prefab.Shop.QUANTITY_POPUP_PREFAB);
        ReceivedNotificationPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.RECEIVED_NOTIFICATION_PANEL_PREFAB);
        ItemPopupPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.ITEM_POPUP_PREFAB);
    }
    public void CreateCardHeroesGallery(List<CardHeroes> cardHeroes, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        foreach (var cardHero in cardHeroes)
        {
            GameObject cardHeroObject = Instantiate(CardHeroButtonPrefab, contentPanel);
            Transform transform = cardHeroObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardHero.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardHero.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardHero.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = LocalizationManager.Get(AppDisplayConstants.Title.CARD_HERO);

            TextMeshProUGUI typePanel = transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardHero.Type.ToString().Replace("_", " ");

            Image rareBackground = transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardHero.Rarity));

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardHero, MainPanel);
            });

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardHero.Rarity));
            rareText.text = cardHero.Rarity;
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(250, 360);
            gridLayout.spacing = new Vector2(23, 10);
        }
        // DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public async Task CreateCardHeroesTradeAsync(List<CardHeroes> cardHeroes, string subType, Transform currentContent, Transform currencyPanel, Transform popupPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = currentContent.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        foreach (var cardHero in cardHeroes)
        {
            GameObject cardHeroObject = Instantiate(EquipmentShopPrefab, currentContent);
            Transform transform = cardHeroObject.transform;

            TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = cardHero.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardHero.Image);
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

            RawImage frameImage = transform.Find("Frame").GetComponent<RawImage>();

            Button button = frameImage.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardHero, MainPanel);
            });

            RawImage topImage = transform.Find("TopImage").GetComponent<RawImage>();
            topImage.material = MaterialManager.Instance.Get("UI_Red_Gradient_Radius_Mat_MaskPercent_90");
            RawImage circleImage = transform.Find("BackgroundContent/CircleImage").GetComponent<RawImage>();
            circleImage.color = ColorHelper.HexToColor(ColorConstants.RED_COLOR);
            Outline bottomOutline = transform.Find("BottomImage").GetComponent<Outline>();
            bottomOutline.effectColor = ColorHelper.HexToColor(ColorConstants.RED_COLOR);
            Outline middleOutline = transform.Find("MiddleImage").GetComponent<Outline>();
            bottomOutline.effectColor = ColorHelper.HexToColor(ColorConstants.RED_COLOR);

            RawImage currencyImage = transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardHero.Currency.Image);
            Texture currencyTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            TextMeshProUGUI currencyText = transform.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
            currencyText.text = NumberFormatterHelper.FormatNumber(cardHero.Currency.Quantity, false);

            Button buyButton = transform.Find("Buy").GetComponent<Button>();
            TextMeshProUGUI buttonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = LocalizationManager.Get(AppDisplayConstants.Title.BUY);
            Image buttonBackgroundImage = buyButton.transform.Find("Background").GetComponent<Image>();
            buttonBackgroundImage.color = ColorHelper.HexToColor(ColorConstants.RED_COLOR);
            buyButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                GetQuantity(cardHero.Currency.Quantity, cardHero, subType, popupPanel, currencyPanel);
            });
        }
        List<Currencies> currencies = new List<Currencies>();
        currencies = await UserCurrenciesService.Create().GetCardHeroesCurrencyAsync(subType);
        FindObjectOfType<CurrenciesManager>().CreateCurrency(currencies, currencyPanel);
        currentContent.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public async Task CreateShopAsync(string shopCodeName)
    {
        GameObject gameObject = Instantiate(ShopPanelPrefab, MainPanel);
        Transform transform = gameObject.transform;
        contentTransform = transform.Find("Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(gameObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);

        });

        PaginationManager = transform.Find("PaginationPanelPrefab").GetComponent<PaginationManager>();
        IsSearchingOrFiltering = true;
        ShopCodeName = shopCodeName;
        await LoadCurrentPageAsync();
    }
    public async Task CreateCardHeroesShopAsync(ShopDTO shopDTO)
    {
        if (shopDTO == null || shopDTO.ShopDetails == null) return;

        // 1. Tắt Component Animation trước khi thao tác trên Content để tránh conflict
        var oldAnim = contentTransform.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null)
        {
            DestroyImmediate(oldAnim); // Dùng DestroyImmediate để xóa ngay lập tức thay vì chờ cuối frame
        }

        // 2. Clear danh sách con cũ an toàn
        for (int i = contentTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentTransform.GetChild(i).gameObject);
        }

        foreach (var shopDetail in shopDTO.ShopDetails)
        {
            GameObject cardHeroObject = Instantiate(EquipmentShopPrefab, contentTransform);
            Transform itemTransform = cardHeroObject.transform;

            // Title
            TextMeshProUGUI titleText = itemTransform.Find("Title")?.GetComponent<TextMeshProUGUI>();
            if (titleText != null && !string.IsNullOrEmpty(shopDetail.ObjectName))
            {
                titleText.text = shopDetail.ObjectName.Replace("_", " ");
            }

            // Image Item
            RawImage image = itemTransform.Find("Image")?.GetComponent<RawImage>();
            if (image != null && !string.IsNullOrEmpty(shopDetail.ObjectImage))
            {
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(shopDetail.ObjectImage);
                Texture texture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);
                if (texture != null)
                {
                    image.texture = texture;
                    ImageManager.Instance.ChangeSizeImageByTextureScale(image, texture);
                }
            }

            // Frame Button & Popup Event
            Transform frameTransform = itemTransform.Find("Frame");
            if (frameTransform != null)
            {
                Button button = frameTransform.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.RemoveAllListeners(); // Xóa listener cũ trước khi Add
                    var currentDetail = shopDetail; // Local copy để tránh lỗi Closure Capture trong C#
                    button.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        PopupDetailsManager.Instance.PopupDetails(currentDetail, MainPanel);
                    });
                }
            }

            // UI Styling (Materials, Colors, Outlines)
            RawImage topImage = itemTransform.Find("TopImage")?.GetComponent<RawImage>();
            if (topImage != null) topImage.material = MaterialManager.Instance.Get("UI_Red_Gradient_Radius_Mat_MaskPercent_90");

            RawImage circleImage = itemTransform.Find("BackgroundContent/CircleImage")?.GetComponent<RawImage>();
            if (circleImage != null) circleImage.color = ColorHelper.HexToColor(ColorConstants.RED_COLOR);

            Outline bottomOutline = itemTransform.Find("BottomImage")?.GetComponent<Outline>();
            if (bottomOutline != null) bottomOutline.effectColor = ColorHelper.HexToColor(ColorConstants.RED_COLOR);

            Outline middleOutline = itemTransform.Find("MiddleImage")?.GetComponent<Outline>();
            if (middleOutline != null) middleOutline.effectColor = ColorHelper.HexToColor(ColorConstants.RED_COLOR);

            // Currency Image & Text
            RawImage currencyImage = itemTransform.Find("CurrencyImage")?.GetComponent<RawImage>();
            if (currencyImage != null && !string.IsNullOrEmpty(shopDetail.CurrencyImage))
            {
                string currencyFileName = ImageHelper.RemoveImageExtension(shopDetail.CurrencyImage);
                Texture currencyTexture = TextureHelper.LoadTextureCached(currencyFileName);
                if (currencyTexture != null) currencyImage.texture = currencyTexture;
            }

            TextMeshProUGUI currencyText = itemTransform.Find("CurrencyText")?.GetComponent<TextMeshProUGUI>();
            if (currencyText != null)
            {
                currencyText.text = NumberFormatterHelper.FormatNumber(shopDetail.Price, false);
            }

            // Buy Button
            Transform buyButtonTransform = itemTransform.Find("Buy");
            if (buyButtonTransform != null)
            {
                Button buyButton = buyButtonTransform.GetComponent<Button>();
                TextMeshProUGUI buttonText = buyButtonTransform.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null) buttonText.text = LocalizationManager.Get(AppDisplayConstants.Title.BUY);

                Image buttonBackgroundImage = buyButtonTransform.Find("Background")?.GetComponent<Image>();
                if (buttonBackgroundImage != null) buttonBackgroundImage.color = ColorHelper.HexToColor(ColorConstants.RED_COLOR);
            }
        }
    }

    public async Task LoadCurrentPageAsync()
    {
        try
        {
            // 1. Kiểm tra Service khởi tạo an toàn
            var shopService = ShopsService.Create();
            if (shopService == null)
            {
                Debug.LogError("[LoadCurrentPageAsync] ShopsService.Create() trả về NULL! Dừng thực thi để tránh crash.");
                return;
            }

            ShopRequestDTO shopRequestDTO = new ShopRequestDTO
            {
                ShopName = "",
                ShopCodeName = ShopCodeName,
                ShopType = AppConstants.Shop.ShopType.GENERAL,
                Limit = PAGE_SIZE,
                Offset = Offset,
                ObjectType = AppConstants.ObjectType.CARD_HEROES
            };

            // 2. Lấy dữ liệu Shop
            ShopDTO shopDTO = await shopService.GetUserShopsAsync(User.CurrentUserId, shopRequestDTO);

            // Kiểm tra null cả DTO lẫn ShopId
            if (shopDTO == null || string.IsNullOrEmpty(shopDTO.ShopId))
            {
                Debug.LogWarning("[LoadCurrentPageAsync] Không tìm thấy dữ liệu Shop hoặc ShopId bị Null.");
                return;
            }

            // 3. Render UI danh sách vật phẩm
            await CreateCardHeroesShopAsync(shopDTO);

            int listCount = shopDTO.ShopDetails?.Count ?? 0;

            // 4. Lấy tổng số bản ghi
            int totalRecord = await shopService.GetShopItemCountAsync(shopRequestDTO);

            // 5. Gán ShopId an toàn & lấy danh sách tiền tệ
            shopRequestDTO.ShopId = shopDTO.ShopId;
            List<Currencies> currencies = await ShopsService.Create().GetCurrenciesByShopAsync(User.CurrentUserId, shopRequestDTO);

            // if (currencies == null)
            // {
            //     currencies = new List<Currencies>(); // Phòng ngừa null reference ở UI
            // }

            // Cập nhật UI tiền tệ nếu cần
            // var currenciesManager = FindObjectOfType<CurrenciesManager>();
            // if (currenciesManager != null)
            // {
            //     currenciesManager.CreateCurrency(currencies, currencyPanel);
            // }

            // 6. Xử lý Phân trang
            if (listCount > 0)
            {
                TotalItems = totalRecord;
            }

            if (IsSearchingOrFiltering && PaginationManager != null)
            {
                PaginationManager.OnPageChanged -= OnPageSelected;
                PaginationManager.InitPagination(TotalItems, PAGE_SIZE, CurrentPage);
                PaginationManager.OnPageChanged += OnPageSelected;
            }
        }
        catch (System.Exception ex)
        {
            // Bắt mọi exception trên Main Thread để Log Console thay vì văng Editor
            Debug.LogError($"[LoadCurrentPageAsync Exception]: {ex.Message}\n{ex.StackTrace}");
        }
    }
    // Hàm hứng sự kiện click nút phân trang
    private void OnPageSelected(int pageNumber)
    {
        CurrentPage = pageNumber;
        Offset = (CurrentPage - 1) * PAGE_SIZE;
        IsSearchingOrFiltering = false;
        _ = LoadCurrentPageAsync();
    }

    private void OnDestroy()
    {
        // Luôn luôn hủy đăng ký sự kiện khi Object bị xóa để tránh lỗi bộ nhớ
        if (PaginationManager != null)
        {
            PaginationManager.OnPageChanged -= OnPageSelected;
        }
    }
    public void GetQuantity(double originPrice, object obj, string subType, Transform popupPanel, Transform currencyPanel)
    {
        GameObject quantityObject = Instantiate(QuantityPopupPrefab, popupPanel);

        Button increaseButton = quantityObject.transform.Find("IncreaseButton").GetComponent<Button>();
        Button decreaseButton = quantityObject.transform.Find("DecreaseButton").GetComponent<Button>();
        Button increase10Button = quantityObject.transform.Find("Increase10Button").GetComponent<Button>();
        Button decrease10Button = quantityObject.transform.Find("Decrease10Button").GetComponent<Button>();
        Button maxButton = quantityObject.transform.Find("MaxButton").GetComponent<Button>();
        Button minButton = quantityObject.transform.Find("MinButton").GetComponent<Button>();
        Button closeButton = quantityObject.transform.Find("CloseButton").GetComponent<Button>();
        Button confirmButton = quantityObject.transform.Find("Buy").GetComponent<Button>();
        TextMeshProUGUI quantityText = quantityObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        RawImage currencyImage = quantityObject.transform.Find("Price/CurrencyImage").GetComponent<RawImage>();
        TextMeshProUGUI priceText = quantityObject.transform.Find("Price/PriceText").GetComponent<TextMeshProUGUI>();
        RawImage equipmentImage = quantityObject.transform.Find("Image").GetComponent<RawImage>();

        TextMeshProUGUI buttonText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = LocalizationManager.Get(AppDisplayConstants.Title.BUY);
        // Lấy thuộc tính `Id` và `Image` từ object
        var idProperty = obj.GetType().GetProperty(AppConstants.StatFields.ID);
        var imageProperty = obj.GetType().GetProperty(AppConstants.StatFields.IMAGE);
        var currencyProperty = obj.GetType().GetProperty(AppConstants.MainType.CURRENCY);

        priceText.text = originPrice.ToString();
        double price = originPrice;
        int quantity = 1;
        quantityText.text = quantity.ToString();

        if (idProperty != null && imageProperty != null && currencyProperty != null)
        {
            string id = (string)idProperty.GetValue(obj);
            string image = (string)imageProperty.GetValue(obj);

            // Lấy đối tượng currency từ obj
            var currencyObject = currencyProperty.GetValue(obj);

            if (currencyObject != null)
            {
                // Lấy thuộc tính "image" từ currencyObject
                var currencyImageProperty = currencyObject.GetType().GetProperty("image");
                if (currencyImageProperty != null)
                {
                    string currencyImageValue = (string)currencyImageProperty.GetValue(currencyObject);

                    if (!string.IsNullOrEmpty(currencyImageValue))
                    {
                        string currencyFileNameWithoutExtension = ImageHelper.RemoveImageExtension(currencyImageValue);
                        Texture currencyTexture = TextureHelper.LoadTextureCached($"{currencyFileNameWithoutExtension}");
                        currencyImage.texture = currencyTexture;
                    }
                }
            }

            // Xử lý image của obj
            if (!string.IsNullOrEmpty(image))
            {
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(image);
                Texture entityTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                equipmentImage.texture = entityTexture;
            }

            priceText.text = price.ToString();
        }

        else
        {
            Debug.LogError("Object không có thuộc tính Id hoặc Image");
        }

        increaseButton.onClick.AddListener(() =>
        {
            quantity++;
            price = originPrice * quantity;
            quantityText.text = quantity.ToString();
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        decreaseButton.onClick.AddListener(() =>
        {
            if (quantity > 1)
            {
                quantity--;
                price = originPrice * quantity;
                quantityText.text = quantity.ToString();
                priceText.text = price.ToString();
            }
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        increase10Button.onClick.AddListener(() =>
        {
            quantity = quantity + 10;
            price = originPrice * quantity;
            quantityText.text = quantity.ToString();
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        decrease10Button.onClick.AddListener(() =>
        {
            if (quantity > 10)
            {
                quantity = quantity - 10;
                price = originPrice * quantity;
                quantityText.text = quantity.ToString();
                priceText.text = price.ToString();
            }
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        maxButton.onClick.AddListener(async () =>
        {
            Currencies userCurrency = new Currencies();
            if (obj is CardHeroes cardHero)
            {
                userCurrency = await UserCurrenciesService.Create().GetUserCurrencyByIdAsync(User.CurrentUserId, cardHero.Currency.Id);
            }
            // double price = double.Parse(priceText.text);

            int max = (int)(userCurrency.Quantity / price);
            price = originPrice * max;
            quantityText.text = max.ToString();
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        minButton.onClick.AddListener(() =>
        {
            quantityText.text = "1";
            price = originPrice * 1;
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(popupPanel);
        });
        confirmButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên

            if (obj is CardHeroes cardHero)
            {
                cardHero.Quantity = cardHero.Quantity + quantity;
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(User.CurrentUserId, cardHero.Currency.Id, price);
                var result = await UserCardHeroesService.Create().InsertOrUpdateUserCardHeroAsync(User.CurrentUserId, cardHero);

                // Hiển thị thông báo dựa trên kết quả
                if (result.Data || result.OperationType != DatabaseOperationType.None || result.OperationType != DatabaseOperationType.Failed)
                {
                    string fileNameWithoutExtension = "";
                    // Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
                    List<Currencies> currencies = new List<Currencies>();

                    // cardHeros.InsertUserCardHeros(cardHeros);
                    currencies = await UserCurrenciesService.Create().GetCardHeroesCurrencyAsync(subType);
                    fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardHero.Image);

                    ButtonEvent.Instance.Close(currencyPanel);
                    FindObjectOfType<CurrenciesManager>().CreateCurrency(currencies, currencyPanel);
                    ButtonEvent.Instance.Close(popupPanel);
                    // FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
                    GameObject receivedNotificationObject = Instantiate(ReceivedNotificationPanelPrefab, popupPanel);

                    ButtonEvent.Instance.AddCloseEvent(receivedNotificationObject);
                    Transform itemContent = receivedNotificationObject.transform.Find("Scroll View/Viewport/Content");
                    GameObject itemObject = Instantiate(ItemPopupPrefab, itemContent);

                    RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
                    Texture equipmentTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                    eImage.texture = equipmentTexture;

                    TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                    eQuantity.text = quantity.ToString();

                    TextMeshProUGUI messageText = receivedNotificationObject.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();

                    if (result.OperationType == DatabaseOperationType.Inserted)
                    {
                        messageText.text = LocalizationManager.Get(MessageConstants.INSERT_ITEM_INTO_INVENTORY);

                        await PowerManagerService.Create().UpdateUserStatsAsync(User.CurrentUserId);
                        double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                        double currentPower = User.CurrentUserPower;
                        User.CurrentUserPower = newPower;
                        FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                    }
                    else
                    {
                        messageText.text = LocalizationManager.Get(MessageConstants.UPDATE_ITEM_QUANTITY_IN_INVENTORY);
                    }

                    Button closeButton = receivedNotificationObject.transform.Find("CloseButton").GetComponent<Button>();

                    closeButton.onClick.AddListener(() =>
                    {
                        Destroy(receivedNotificationObject);
                    });
                }
                else
                {
                    GameObject receivedNotificationObject = Instantiate(ReceivedNotificationPanelPrefab, popupPanel);
                    TextMeshProUGUI messageText = receivedNotificationObject.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();
                    messageText.text = LocalizationManager.Get(MessageConstants.PURCHASE_FAILED);
                }
            }
        }));
    }
}
