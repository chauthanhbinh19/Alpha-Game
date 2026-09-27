using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentsController : MonoBehaviour
{
    public static EquipmentsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject EquipmentButtonPrefab;
    private GameObject EquipmentShopPrefab;
    private GameObject QuantityPopupPrefab;
    private GameObject ShopPanelPrefab;
    private Transform leftTransform;
    private Transform rightTransform;
    private PaginationManager PaginationManager;
    private Transform contentTransform;
    private TextMeshProUGUI TotalText;
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
        EquipmentButtonPrefab = UIManager.Instance.Get("EquipmentButtonPrefab");
        ShopPanelPrefab = UIManager.Instance.Get(PrefabConstants.Shop.SHOP_PANEL_PREFAB);
        EquipmentShopPrefab = UIManager.Instance.Get(PrefabConstants.Equipment.EQUIPMENT_SHOP_PREFAB);
        QuantityPopupPrefab = UIManager.Instance.Get(PrefabConstants.Shop.QUANTITY_POPUP_PREFAB);
    }
    public void CreateEquipmentsGallery(List<Equipments> equipments, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.EQUIPMENT_BUTTON_BACKGROUND_URL);

        foreach (var equipment in equipments)
        {
            try
            {
                GameObject equipmentObject = Instantiate(EquipmentButtonPrefab, contentPanel);
                Transform transform = equipmentObject.transform;

                TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                titleText.text = equipment.Name.Replace("_", " ");

                RawImage image = transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(equipment.Image);
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                image.texture = texture;

                ImageManager.Instance.ChangeSizeImageByTextureScale(image, texture);

                RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = bgTexture;

                Button button = transform.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(equipment, MainPanel);
                });

                TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(equipment.Rarity));
                rareText.text = equipment.Rarity;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public async Task CreateShopAsync(string shopCodeName)
    {
        GameObject gameObject = Instantiate(ShopPanelPrefab, MainPanel);
        Transform transform = gameObject.transform;
        contentTransform = transform.Find("Scroll View/Viewport/Content");
        leftTransform = transform.Find("Left Scroll View/Viewport/Content");
        rightTransform = transform.Find("Left Scroll View/Viewport/Content");
        TotalText = transform.Find("TitleGroup/TotalText").GetComponent<TextMeshProUGUI>();
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
    public void CreateEquipmentsShopAsync(ShopDTO shopDTO)
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
            GameObject equipmentObject = Instantiate(EquipmentShopPrefab, contentTransform);
            Transform itemTransform = equipmentObject.transform;

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

            TextMeshProUGUI stockTitleText = itemTransform.Find("StockTitleText")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI stockText = itemTransform.Find("StockText")?.GetComponent<TextMeshProUGUI>();
            // Tìm Transform của SoldOut (nếu nằm trong Prefab Item)
            Transform soldOutTransform = itemTransform.Find("SoldOut");
            if (stockTitleText != null)
            {
                stockTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.STOCK);
            }

            // Tách riêng hàm cập nhật Stock UI của Item này
            Transform buyButtonTransform = itemTransform.Find("Buy");
            Button buyButton = buyButtonTransform?.GetComponent<Button>();

            void UpdateItemStockUI()
            {
                int remainingStock = shopDetail.BuyLimitPerUser > 0
                    ? (shopDetail.BuyLimitPerUser - shopDetail.PurchaseCount)
                    : 99;

                bool isSoldOut = remainingStock <= 0;

                // 1. Cập nhật text số lượng còn lại
                if (stockText != null)
                {
                    stockText.text = Mathf.Max(0, remainingStock).ToString();
                }

                // 2. Bật/Tắt Overlay SoldOut
                if (soldOutTransform != null)
                {
                    soldOutTransform.gameObject.SetActive(isSoldOut);
                }

                // 3. Khóa/Mở tương tác nút Mua
                if (buyButton != null)
                {
                    buyButton.interactable = !isSoldOut;
                }
            }

            // Hiển thị Stock ban đầu
            UpdateItemStockUI();

            // Sự kiện nút Buy
            if (buyButton != null)
            {
                buyButton.onClick.RemoveAllListeners();
                var currentDetail = shopDetail; // Local copy

                buyButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

                    ShopDTO popupShopDTO = new ShopDTO
                    {
                        ShopId = shopDTO.ShopId,
                        ShopDetail = currentDetail
                    };

                    // Truyền callback để sau khi mua thành công sẽ cập nhật lại Item này
                    CreatePopupPanel(popupShopDTO, async (purchasedQuantity) =>
                    {
                        // Cập nhật số lượng đã mua vào DTO
                        currentDetail.PurchaseCount += purchasedQuantity;

                        // Tự động tính lại stock và SetActive(true) cho SoldOut nếu remainingStock <= 0
                        UpdateItemStockUI();

                        // 3. CẬP NHẬT LẠI SỐ DƯ TIỀN TỆ TRÊN RIGHT_TRANSFORM NGAY LẬP TỨC
                        await LoadCurrenciesAsync(shopDTO);
                    });
                });
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
                ObjectType = AppConstants.ObjectType.EQUIPMENTS
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
            CreateEquipmentsShopAsync(shopDTO);

            int listCount = shopDTO.ShopDetails?.Count ?? 0;
            TotalText.text = listCount.ToString();

            // 4. Lấy tổng số bản ghi
            int totalRecord = await shopService.GetShopItemCountAsync(shopRequestDTO);

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
    public async Task LoadCurrenciesAsync(ShopDTO shopDTO)
    {
        if (shopDTO == null || string.IsNullOrEmpty(shopDTO.ShopId) || rightTransform == null) return;

        ShopRequestDTO shopRequestDTO = new ShopRequestDTO
        {
            ShopId = shopDTO.ShopId,
            ShopCodeName = ShopCodeName,
            ShopType = AppConstants.Shop.ShopType.GENERAL,
            ObjectType = AppConstants.ObjectType.CARD_HEROES
        };

        // Lấy danh sách tiền tệ của Shop
        List<Currencies> currencies = await ShopsService.Create().GetCurrenciesByShopAsync(User.CurrentUserId, shopRequestDTO);

        if (currencies == null)
        {
            currencies = new List<Currencies>();
        }

        // Render danh sách tiền tệ lên panel bên phải (rightTransform)
        var currenciesManager = CurrenciesManager.Instance; // Hoặc FindFirstObjectByType<CurrenciesManager>()
        if (currenciesManager != null)
        {
            currenciesManager.CreateTabCurrency(currencies, rightTransform);
        }
    }
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
    public void CreatePopupPanel(ShopDTO shopDTO, Action<int> onPurchaseSuccess = null)
    {
        if (shopDTO == null || shopDTO.ShopDetail == null) return;

        // 1. Khởi tạo Prefab Popup vào Canvas/MainPanel
        GameObject quantityObject = Instantiate(QuantityPopupPrefab, MainPanel);

        // 2. Bắt các component UI từ Prefab
        Button increaseButton = quantityObject.transform.Find("IncreaseButton")?.GetComponent<Button>();
        Button decreaseButton = quantityObject.transform.Find("DecreaseButton")?.GetComponent<Button>();
        Button increase10Button = quantityObject.transform.Find("Increase10Button")?.GetComponent<Button>();
        Button decrease10Button = quantityObject.transform.Find("Decrease10Button")?.GetComponent<Button>();
        Button maxButton = quantityObject.transform.Find("MaxButton")?.GetComponent<Button>();
        Button minButton = quantityObject.transform.Find("MinButton")?.GetComponent<Button>();
        Button closeButton = quantityObject.transform.Find("CloseButton")?.GetComponent<Button>();
        Button confirmButton = quantityObject.transform.Find("Buy")?.GetComponent<Button>();

        TextMeshProUGUI quantityText = quantityObject.transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();
        RawImage currencyImage = quantityObject.transform.Find("Price/CurrencyImage")?.GetComponent<RawImage>();
        TextMeshProUGUI priceText = quantityObject.transform.Find("Price/PriceText")?.GetComponent<TextMeshProUGUI>();
        RawImage equipmentImage = quantityObject.transform.Find("Image")?.GetComponent<RawImage>();

        // 3. Khởi tạo biến theo dõi số lượng
        int currentQuantity = 1;
        int minQuantity = 1;

        // Tính số lượng còn lại người dùng ĐƯỢC PHÉP MUA
        int remainingStock = shopDTO.ShopDetail.BuyLimitPerUser > 0
            ? (shopDTO.ShopDetail.BuyLimitPerUser - shopDTO.ShopDetail.PurchaseCount)
            : 99; // Hoặc một giới hạn kho mặc định nếu BuyLimitPerUser <= 0

        // Đảm bảo maxQuantity không bị âm nếu người dùng bằng cách nào đó đã mua vượt limit
        int maxQuantity = Mathf.Max(0, remainingStock);

        // Nếu hết hàng (maxQuantity == 0), đặt minQuantity và currentQuantity về 0 để tránh đụng độ clamp
        if (maxQuantity == 0)
        {
            minQuantity = 0;
            currentQuantity = 0;
        }

        double unitPrice = shopDTO.ShopDetail.Price;

        // 4. Load hình ảnh hiển thị (Vật phẩm & Tiền tệ)
        if (equipmentImage != null && !string.IsNullOrEmpty(shopDTO.ShopDetail.ObjectImage))
        {
            string fileName = ImageHelper.RemoveImageExtension(shopDTO.ShopDetail.ObjectImage);
            Texture texture = TextureHelper.LoadTextureCached(fileName);
            if (texture != null)
            {
                equipmentImage.texture = texture;
                ImageManager.Instance.ChangeSizeImageByTextureScale(equipmentImage, texture);
            }
        }

        if (currencyImage != null && !string.IsNullOrEmpty(shopDTO.ShopDetail.CurrencyImage))
        {
            string currencyFileName = ImageHelper.RemoveImageExtension(shopDTO.ShopDetail.CurrencyImage);
            Texture currencyTexture = TextureHelper.LoadTextureCached(currencyFileName);
            if (currencyTexture != null)
            {
                currencyImage.texture = currencyTexture;
            }
        }

        // 5. Hàm cập nhật UI local khi thay đổi số lượng
        void UpdatePopupUI()
        {
            currentQuantity = Mathf.Clamp(currentQuantity, minQuantity, maxQuantity);

            if (quantityText != null)
            {
                quantityText.text = currentQuantity.ToString();
            }

            if (priceText != null)
            {
                double totalPrice = unitPrice * currentQuantity;
                priceText.text = NumberFormatterHelper.FormatNumber(totalPrice, false);
            }
        }

        // Gọi lần đầu để hiển thị mặc định
        UpdatePopupUI();

        // 6. Đăng ký sự kiện Nút Tăng/Giảm/Min/Max
        increaseButton?.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            currentQuantity++;
            UpdatePopupUI();
        });

        decreaseButton?.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            currentQuantity--;
            UpdatePopupUI();
        });

        increase10Button?.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            currentQuantity += 10;
            UpdatePopupUI();
        });

        decrease10Button?.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            currentQuantity -= 10;
            UpdatePopupUI();
        });

        minButton?.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            currentQuantity = minQuantity;
            UpdatePopupUI();
        });

        maxButton?.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            currentQuantity = maxQuantity;
            UpdatePopupUI();
        });

        // 7. Đăng ký sự kiện Nút Đóng Popup
        closeButton?.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(quantityObject);
        });

        // 8. Đăng ký sự kiện Nút Mua (Confirm Buy)
        confirmButton?.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            // Khóa nút mua tránh spam bấm nhiều lần
            confirmButton.interactable = false;

            string currentUserId = User.CurrentUserId; // Lấy ID người dùng hiện tại

            // Gọi API mua thẻ Hero
            var result = await UserShopPurchaseService.Create().PurchaseObjectFromShop(currentUserId, shopDTO, currentQuantity);

            if (result != null && result.IsSuccess)
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.PURCHASE_SOUND);
                // Mua thành công: Hiện thông báo, cập nhật lại Tiền tệ/Tài sản User trên UI và đóng Popup
                NotificationManager.Instance.ShowNotification(LocalizationManager.Get(MessageConstants.EQUIPMENTS_PURCHASED_SUCCESSFULLY));
                if (result.Data && result.IsChangePower)
                {
                    PowerResultDTO powerResult = await UserService.Create().UpdateUserPowerAsync();

                    if (powerResult.HasChanged)
                    {
                        PowerController.Instance.ShowPower(
                            powerResult.CurrentPower,
                            powerResult.Difference,
                            1
                        );
                    }
                }
                // Xử lý logic cập nhật UI Tiền tệ / Túi đồ của User tại đây (nếu có)
                // *** GỌI CALLBACK ĐỂ CẬP NHẬT UI TRÊN SHOP CỤ THỂ ***
                onPurchaseSuccess?.Invoke(currentQuantity);

                Destroy(quantityObject);
            }
            else
            {
                // Mua thất bại: Hiện thông báo lỗi và mở lại tương tác cho nút mua
                string errorMsg = result != null ? result.Message : MessageConstants.PURCHASE_FAILED;
                NotificationManager.Instance.ShowNotification(LocalizationManager.Get(errorMsg));

                confirmButton.interactable = true;
            }
        });
    }
}
