using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodsController : MonoBehaviour
{
    public static FoodsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject FoodButtonPrefab;
    private GameObject EquipmentShopPrefab;
    private GameObject quantityPopupPrefab;
    private GameObject receivedNotification;
    private GameObject ItemThird;
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
        FoodButtonPrefab = UIManager.Instance.Get("FoodButtonPrefab");
        EquipmentShopPrefab = UIManager.Instance.Get("EquipmentShopPrefab");
        quantityPopupPrefab = UIManager.Instance.Get("QuantityPopupPrefab");
        receivedNotification = UIManager.Instance.Get("ReceivedNotificationPanelPrefab");
        ItemThird = UIManager.Instance.Get("ItemThird");
    }
    public void CreateFoodsGallery(List<Foods> Foods, Transform contentPanel)
    {
        foreach (var Food in Foods)
        {
            GameObject FoodObject = Instantiate(FoodButtonPrefab, contentPanel);

            TextMeshProUGUI title = FoodObject.transform.Find("FoodText").GetComponent<TextMeshProUGUI>();
            title.text = Food.Name.Replace("_", " ");

            RawImage image = FoodObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Food.Image);
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

            RawImage backgroundImage = FoodObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.FOOD_BUTTON_BACKGROUND_URL);

            Button button = FoodObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(Food, MainPanel);
            });

            RawImage rareImage = FoodObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Food.Rare}");
            rareImage.texture = rareTexture;

            RawImage rareBackgroundImage = FoodObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 240);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public async Task CreateFoodsTradeAsync(List<Foods> FoodsList, string subType, Transform currentContent,
    Transform currencyPanel, Transform popupPanel)
    {
        foreach (var Food in FoodsList)
        {
            GameObject FoodObject = Instantiate(EquipmentShopPrefab, currentContent);

            TextMeshProUGUI title = FoodObject.transform.Find("Food").GetComponent<TextMeshProUGUI>();
            title.text = Food.Name.Replace("_", " ");

            RawImage Image = FoodObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Food.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
            RawImage FrameImage = FoodObject.transform.Find("Frame").GetComponent<RawImage>();

            Button button = FrameImage.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(Food, MainPanel);
            });

            RawImage topImage = FoodObject.transform.Find("TopImage").GetComponent<RawImage>();
            topImage.material = MaterialManager.Instance.Get("UI_Red_Radius_Mat");
            RawImage circleImage = FoodObject.transform.Find("BackgroundContent/CircleImage").GetComponent<RawImage>();
            circleImage.color = ColorHelper.ToColor(ColorConstants.RED_COLOR);
            Outline bottomOutline = FoodObject.transform.Find("BottomImage").GetComponent<Outline>();
            bottomOutline.effectColor = ColorHelper.ToColor(ColorConstants.RED_COLOR);
            Outline middleOutline = FoodObject.transform.Find("MiddleImage").GetComponent<Outline>();
            bottomOutline.effectColor = ColorHelper.ToColor(ColorConstants.RED_COLOR);

            RawImage currencyImage = FoodObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Food.Currency.Image);
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            TextMeshProUGUI currencyText = FoodObject.transform.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
            currencyText.text = NumberFormatter.FormatNumber(Food.Currency.Quantity, false);

            Button buy = FoodObject.transform.Find("Buy").GetComponent<Button>();
            TextMeshProUGUI buttonText = buy.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUY);
            RawImage buttonBackgroundImage = buy.transform.Find("Background").GetComponent<RawImage>();
            buttonBackgroundImage.color = ColorHelper.ToColor(ColorConstants.RED_COLOR);
            buy.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                GetQuantity(Food.Currency.Quantity, Food, subType, popupPanel, currencyPanel);
            });
        }

        List<Currencies> currencies = new List<Currencies>();
        currencies = await UserCurrenciesService.Create().GetFoodsCurrencyAsync(subType);
        FindObjectOfType<CurrenciesManager>().createCurrency(currencies, currencyPanel);
        currentContent.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void GetQuantity(double originPrice, object obj, string subType, Transform popupPanel, Transform currencyPanel)
    {
        GameObject quantityObject = Instantiate(quantityPopupPrefab, popupPanel);

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
        buttonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUY);
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
                var currencyImageProperty = currencyObject.GetType().GetProperty(AppConstants.StatFields.IMAGE);
                if (currencyImageProperty != null)
                {
                    string currencyImageValue = (string)currencyImageProperty.GetValue(currencyObject);

                    if (!string.IsNullOrEmpty(currencyImageValue))
                    {
                        string currencyFileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(currencyImageValue);
                        Texture currencyTexture = Resources.Load<Texture>($"{currencyFileNameWithoutExtension}");
                        currencyImage.texture = currencyTexture;
                    }
                }
            }

            // Xử lý image của obj
            if (!string.IsNullOrEmpty(image))
            {
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(image);
                Texture entityTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
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
            quantity = quantity + 1;
            price = originPrice * quantity;
            quantityText.text = quantity.ToString();
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        decreaseButton.onClick.AddListener(() =>
        {
            if (quantity > 1)
            {
                quantity = quantity - 1;
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
        maxButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
        {
            Currencies userCurrency = new Currencies();
            if (obj is Foods Foods)
            {
                userCurrency = await UserCurrenciesService.Create().GetUserCurrencyByIdAsync((string)Foods.Currency.Id);
            }
            // double price = double.Parse(priceText.text);

            int max = (int)(userCurrency.Quantity / price);
            price = originPrice * max;
            quantityText.text = max.ToString();
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        }));
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
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            if (obj is Foods Food)
            {
                Food.Quantity = Food.Quantity + quantity;
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync((string)Food.Currency.Id, price);
                bool success = await UserFoodsService.Create().InsertUserFoodAsync(Food, User.CurrentUserId);
                if (!success)
                {
                    allSuccess = false;
                }

                // Hiển thị thông báo dựa trên kết quả
                if (allSuccess)
                {
                    string fileNameWithoutExtension = "";
                    // Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
                    List<Currencies> currencies = new List<Currencies>();

                    await FoodsGalleryService.Create().InsertFoodGalleryAsync((string)Food.Id);
                    currencies = await UserCurrenciesService.Create().GetFoodsCurrencyAsync(subType);
                    fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension((string)Food.Image);

                    ButtonEvent.Instance.Close(currencyPanel);
                    FindObjectOfType<CurrenciesManager>().createCurrency(currencies, currencyPanel);
                    ButtonEvent.Instance.Close(popupPanel);
                    // FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
                    GameObject receivedNotificationObject = Instantiate(receivedNotification, popupPanel);

                    ButtonEvent.Instance.AddCloseEvent(receivedNotificationObject);
                    Transform itemContent = receivedNotificationObject.transform.Find("Scroll View/Viewport/Content");
                    GameObject itemObject = Instantiate(ItemThird, itemContent);

                    RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
                    Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    eImage.texture = equipmentTexture;

                    TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                    eQuantity.text = quantity.ToString();

                    await PowerManagerService.Create().UpdateUserStatsAsync(User.CurrentUserId);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
                }
            }
        }));
    }
}
