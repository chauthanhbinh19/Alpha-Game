using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaymentController : MonoBehaviour
{
    public static PaymentController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject TopupPanelPrefab;
    private GameObject TopupTabButtonPrefab;
    private GameObject TopupButtonPrefab;
    private GameObject PopupTopupPanelPrefab;
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
        TopupPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.TOPUP_PANEL_PREFAB);
        TopupTabButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.TOPUP_TAB_BUTTON_PREFAB);
        TopupButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.TOPUP_BUTTON_PREFAB);
        PopupTopupPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.POPUP_TOPUP_PANEL_PREFAB);
    }
    public async Task CreateShopPackageAsync()
    {
        GameObject topupPanelObject = Instantiate(TopupPanelPrefab, MainPanel);
        Transform transform = topupPanelObject.transform;
        Transform tabButtonTransform = transform.Find("Tab Scroll View/Viewport/Content");
        Transform contentTransform = transform.Find("Scroll View/Viewport/Content");
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.SHOP_PACKAGE);
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(topupPanelObject);
        });
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        List<string> categories = await PaymentService.Create().GetAllCategoriesAsync();
        // Danh sách lưu trữ các UI Tab để quản lý toggle trạng thái Selected / Default
        List<(GameObject defaultObj, GameObject selectedObj)> tabUIList = new List<(GameObject, GameObject)>();

        for (int i = 0; i < categories.Count; i++)
        {
            string category = categories[i];

            // Instantiate tab button và đặt parent vào Content của Tab Scroll View
            GameObject topupTabButtonObject = Instantiate(TopupTabButtonPrefab, tabButtonTransform);

            GameObject defaultObj = topupTabButtonObject.transform.Find("Default").gameObject;
            GameObject selectedObj = topupTabButtonObject.transform.Find("Selected").gameObject;

            TextMeshProUGUI titleText1 = defaultObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI titleText2 = selectedObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();

            titleText1.text = category;
            titleText2.text = category;

            // Trạng thái mặc định: Phần tử đầu tiên (i == 0) sẽ Active Selected, còn lại Active Default
            bool isFirst = (i == 0);
            defaultObj.SetActive(!isFirst);
            selectedObj.SetActive(isFirst);

            tabUIList.Add((defaultObj, selectedObj));

            // Đăng ký sự kiện Click cho Tab Button
            Button tabBtn = topupTabButtonObject.GetComponent<Button>();
            tabBtn.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND_2);

                // 1. Chuyển tất cả các Tab về dạng Default
                foreach (var tabUI in tabUIList)
                {
                    tabUI.defaultObj.SetActive(true);
                    tabUI.selectedObj.SetActive(false);
                }

                // 2. Bật dạng Selected cho Tab vừa được click
                defaultObj.SetActive(false);
                selectedObj.SetActive(true);

                // TODO: Gọi hàm load/filter danh sách gói nạp theo category này vào contentTransform
                await LoadPackagesByCategoryAsync(category, contentTransform);
            });
        }

        // Load gói nạp của Tab đầu tiên nếu có danh mục
        if (categories.Count > 0)
        {
            await LoadPackagesByCategoryAsync(categories[0], contentTransform);
        }
    }
    public async Task LoadPackagesByCategoryAsync(string category, Transform contentTransform)
    {
        // Dọn dẹp các item cũ trong ScrollView
        for (int i = contentTransform.childCount - 1; i >= 0; i--)
        {
            Transform child = contentTransform.GetChild(i);
            child.SetParent(null);
            Destroy(child.gameObject);
        }

        List<ShopPackageModel> shopPackageModels = await PaymentService.Create().GetAllActivePackagesAsync(category);

        if (shopPackageModels == null || shopPackageModels.Count == 0)
        {
            return;
        }

        foreach (var package in shopPackageModels)
        {
            GameObject topupButtonObject = Instantiate(TopupButtonPrefab, contentTransform);
            TextMeshProUGUI titleText = topupButtonObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = package.PackageName;

            TextMeshProUGUI rewardText = topupButtonObject.transform.Find("RewardText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI bonusText = topupButtonObject.transform.Find("BonusText").GetComponent<TextMeshProUGUI>();
            rewardText.text = package.RewardAmount.ToString();
            bonusText.text = "0";

            RawImage rewardImage = topupButtonObject.transform.Find("RewardImage").GetComponent<RawImage>();
            RawImage bonusImage = topupButtonObject.transform.Find("BonusImage").GetComponent<RawImage>();
            RawImage mainImage = topupButtonObject.transform.Find("Image").GetComponent<RawImage>();
            Texture currencyTexture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(package.RewardCurrencyImage));
            rewardImage.texture = currencyTexture;
            bonusImage.texture = currencyTexture;
            mainImage.texture = currencyTexture;

            // Hiển thị Giá tiền (USD)
            TextMeshProUGUI currencyText = topupButtonObject.transform.Find("CurrencyText")?.GetComponent<TextMeshProUGUI>();
            if (currencyText != null) currencyText.text = $"${package.PriceUsd:F2}";

            // Khi bấm vào nút gói nạp -> Mở Popup Xác Nhận Thanh Toán
            Button buyButton = topupButtonObject.GetComponent<Button>();
            if (buyButton != null)
            {
                buyButton.onClick.RemoveAllListeners();
                buyButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    CreateTopupPopupPanel(package);
                });
            }
        }
    }
    public void CreateTopupPopupPanel(ShopPackageModel shopPackageModel)
    {
        GameObject popupTopupPanelObject = Instantiate(PopupTopupPanelPrefab, MainPanel);
        Transform transform = popupTopupPanelObject.transform;

        TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.SHOP_PACKAGE);

        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        Button confirmButton = transform.Find("ConfirmButton").GetComponent<Button>();

        TextMeshProUGUI rewardTitleText = transform.Find("Content/RewardTitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bonusTitleText = transform.Find("Content/BonusTitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceTitleText = transform.Find("Content/PriceTitleText").GetComponent<TextMeshProUGUI>();
        rewardTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.REWARD);
        bonusTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.BONUS);
        priceTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.PRICE);

        TextMeshProUGUI rewardText = transform.Find("Content/RewardText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bonusText = transform.Find("Content/BonusText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = transform.Find("Content/PriceText").GetComponent<TextMeshProUGUI>();
        rewardText.text = shopPackageModel.RewardAmount.ToString();
        bonusText.text = "0";
        priceText.text = $"${shopPackageModel.PriceUsd:F2}";

        RawImage rewardImage = transform.Find("Content/RewardImage").GetComponent<RawImage>();
        RawImage bonusImage = transform.Find("Content/BonusImage").GetComponent<RawImage>();
        RawImage mainImage = transform.Find("RewardImage").GetComponent<RawImage>();
        TextMeshProUGUI priceUnitText = transform.Find("Content/PriceUnitText").GetComponent<TextMeshProUGUI>();
        Texture currencyTexture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(shopPackageModel.RewardCurrencyImage));
        rewardImage.texture = currencyTexture;
        bonusImage.texture = currencyTexture;
        mainImage.texture = currencyTexture;

        // 1. Sinh Idempotency Key CỐ ĐỊNH cho duy nhất phiên mở Popup này
        string popupIdempotencyKey = $"REQ_{User.CurrentUserId}_{shopPackageModel.PackageId}_{Guid.NewGuid()}";

        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupTopupPanelObject);
        });

        confirmButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            // 2. Khóa nút Confirm và Close để tránh Spam Click / Tắt popup khi đang nạp
            confirmButton.interactable = false;
            closeButton.interactable = false;

            try
            {
                // 3. Gọi Service xử lý nạp tiền với popupIdempotencyKey cố định
                TopupResponseDTO response = await PaymentService.Create().ProcessPackagePaymentAsync(
                    userId: User.CurrentUserId,
                    package: shopPackageModel,
                    idempotencyKey: popupIdempotencyKey
                );

                if (response.Success)
                {
                    // Thông báo thành công và đóng Popup
                    NotificationManager.Instance.ShowNotification(LocalizationManager.Get(response.Message));
                    Destroy(popupTopupPanelObject);
                }
                else
                {
                    // Thông báo thất bại và mở lại tương tác nút
                    NotificationManager.Instance.ShowNotification(LocalizationManager.Get(response.Message));
                    confirmButton.interactable = true;
                    closeButton.interactable = true;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[TopupPopup] Error: {ex.Message}");
                confirmButton.interactable = true;
                closeButton.interactable = true;
            }
        });
    }
}