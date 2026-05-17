using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemsController : MonoBehaviour
{
    public static ItemsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject equipmentsPrefab;
    private GameObject ItemShopButtonPrefab;
    private GameObject quantityPopupPrefab;
    private GameObject receivedNotification;
    private GameObject ItemPopupPrefab;
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
        equipmentsPrefab = UIManager.Instance.Get("EquipmentFirstPrefab");
        ItemShopButtonPrefab = UIManager.Instance.Get("ItemShopButtonPrefab");
        quantityPopupPrefab = UIManager.Instance.Get("QuantityPopupPrefab");
        receivedNotification = UIManager.Instance.Get("ReceivedNotificationPanelPrefab");
        ItemPopupPrefab = UIManager.Instance.Get("ItemPopupPrefab");
    }
    public async Task CreateItemsTradeAsync(List<Items> items, Currencies currency, Transform currentContent, Transform currencyPanel, Transform popupPanel)
    {
        List<Currencies> currencies = new List<Currencies>();
        var tempCurrency = await UserCurrenciesService.Create().GetUserCurrencyByIdAsync(currency.Id);
        currencies.Add(tempCurrency);
        FindObjectOfType<CurrenciesManager>().CreateCurrency(currencies, currencyPanel);

        foreach (var item in items)
        {
            GameObject itemObject = Instantiate(ItemShopButtonPrefab, currentContent);

            TextMeshProUGUI titleText = itemObject.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            titleText.text = item.Name.Replace("_", " ");

            RawImage image = itemObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(item.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            RawImage topImage = itemObject.transform.Find("TopImage").GetComponent<RawImage>();
            topImage.material = MaterialManager.Instance.Get("UI_Yellow_Gradient_Radius_Mat_MaskPercent_90");
            RawImage circleImage = itemObject.transform.Find("BackgroundContent/CircleImage").GetComponent<RawImage>();
            circleImage.color = ColorHelper.HexToColor(ColorConstants.YELLOW_COLOR);
            Outline bottomOutline = itemObject.transform.Find("BottomImage").GetComponent<Outline>();
            bottomOutline.effectColor = ColorHelper.HexToColor(ColorConstants.YELLOW_COLOR);
            Outline middleOutline = itemObject.transform.Find("MiddleImage").GetComponent<Outline>();
            bottomOutline.effectColor = ColorHelper.HexToColor(ColorConstants.YELLOW_COLOR);

            RawImage currencyImage = itemObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageHelper.RemoveImageExtension(currency.Image);
            Texture currencyTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            TextMeshProUGUI currencyText = itemObject.transform.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
            currencyText.text = NumberFormatterHelper.FormatNumber(item.Price, false);

            Button buy = itemObject.transform.Find("Buy").GetComponent<Button>();
            TextMeshProUGUI buttonText = buy.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUY);
            Image buttonBackgroundImage = buy.transform.Find("Background").GetComponent<Image>();
            buttonBackgroundImage.color = ColorHelper.HexToColor(ColorConstants.YELLOW_COLOR);
            buy.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                GetQuantity(item.Price, item, popupPanel, currency, currencyPanel);
            });
        }
        currentContent.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void GetQuantity(double originPrice, object obj, Transform popupPanel, Currencies currency, Transform currencyPanel)
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

        priceText.text = originPrice.ToString();
        double price = originPrice;
        int quantity = 1;

        quantityText.text = quantity.ToString();


        if (idProperty != null && imageProperty != null)
        {
            string id = (string)idProperty.GetValue(obj);
            string image = (string)imageProperty.GetValue(obj);

            string currencyFileNameWithoutExtension = ImageHelper.RemoveImageExtension(currency.Image);
            Texture currencyTexture = TextureHelper.LoadTextureCached($"{currencyFileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            // Xử lý image của obj
            if (!string.IsNullOrEmpty(image))
            {
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(image);
                Texture entityTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                equipmentImage.texture = entityTexture;
            }

            priceText.text = originPrice.ToString();
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
        maxButton.onClick.AddListener(async () =>
        {
            Currencies userCurrency = userCurrency = await UserCurrenciesService.Create().GetUserCurrencyByIdAsync(currency.Id);
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
        confirmButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            if (obj is Items item)
            {
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, price);
                bool success = await UserItemsService.Create().InsertUserItemAsync(item, quantity);
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
                    var tempCurrency = await UserCurrenciesService.Create().GetUserCurrencyByIdAsync(currency.Id);
                    currencies.Add(tempCurrency);
                    fileNameWithoutExtension = ImageHelper.RemoveImageExtension(item.Image);

                    ButtonEvent.Instance.Close(currencyPanel);
                    FindObjectOfType<CurrenciesManager>().CreateCurrency(currencies, currencyPanel);
                    ButtonEvent.Instance.Close(popupPanel);
                    // FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
                    GameObject receivedNotificationObject = Instantiate(receivedNotification, popupPanel);

                    ButtonEvent.Instance.AddCloseEvent(receivedNotificationObject);
                    Transform itemContent = receivedNotificationObject.transform.Find("Scroll View/Viewport/Content");
                    GameObject itemObject = Instantiate(ItemPopupPrefab, itemContent);

                    RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
                    Texture equipmentTexture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                    eImage.texture = equipmentTexture;

                    TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                    eQuantity.text = quantity.ToString();
                }
                else
                {
                    NotificationManager.Instance.ShowNotification(LocalizationManager.Get(AppDisplayConstants.Message.PURCHASE_FAILED));
                }
            }
        });
    }
}
