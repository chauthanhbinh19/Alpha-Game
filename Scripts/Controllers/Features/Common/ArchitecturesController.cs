using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArchitecturesController : MonoBehaviour
{
    public static ArchitecturesController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject equipmentsPrefab;
    private GameObject equipmentsShopPrefab;
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
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        equipmentsShopPrefab = UIManager.Instance.GetGameObject("equipmentsShopPrefab");
        quantityPopupPrefab = UIManager.Instance.GetGameObject("quantityPopupPrefab");
        receivedNotification = UIManager.Instance.GetGameObject("ReceivedNotification");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
    }
    public void CreateArchitecturesGallery(List<Architectures> ArchitecturesList, Transform DictionaryContentPanel)
    {
        foreach (var architecture in ArchitecturesList)
        {
            GameObject architectureObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = architectureObject.transform.Find("Title").GetComponent<Text>();
            Title.text = architecture.Name.Replace("_", " ");

            RawImage image = architectureObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(architecture.Image);
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

            Button button = architectureObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(architecture, MainPanel);
            });

            RawImage rareImage = architectureObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{architecture.Rare}");
            rareImage.texture = rareTexture;

            RawImage rareBackgroundImage = architectureObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateArchitecturesTrade(List<Architectures> ArchitecturesList, string subType, Transform currentContent,
    Transform CurrencyPanel, Transform popupPanel)
    {
        foreach (var architecture in ArchitecturesList)
        {
            GameObject architectureObject = Instantiate(equipmentsShopPrefab, currentContent);

            TextMeshProUGUI Title = architectureObject.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            Title.text = architecture.Name.Replace("_", " ");

            RawImage image = architectureObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(architecture.Image);
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

            RawImage FrameImage = architectureObject.transform.Find("Frame").GetComponent<RawImage>();

            Button button = FrameImage.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(architecture, MainPanel);
            });

            RawImage topImage = architectureObject.transform.Find("TopImage").GetComponent<RawImage>();
            topImage.material = MaterialManager.Instance.GetRedMaterial("UI_Red_Radius_Mat");
            RawImage circleImage = architectureObject.transform.Find("BackgroundContent/CircleImage").GetComponent<RawImage>();
            circleImage.color = ColorHelper.ToColor(ColorConstants.RED_COLOR);
            Outline bottomOutline = architectureObject.transform.Find("BottomImage").GetComponent<Outline>();
            bottomOutline.effectColor = ColorHelper.ToColor(ColorConstants.RED_COLOR);
            Outline middleOutline = architectureObject.transform.Find("MiddleImage").GetComponent<Outline>();
            bottomOutline.effectColor = ColorHelper.ToColor(ColorConstants.RED_COLOR);

            RawImage CurrencyImage = architectureObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(architecture.Currency.Image);
            Texture CurrencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            CurrencyImage.texture = CurrencyTexture;

            TextMeshProUGUI CurrencyText = architectureObject.transform.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
            CurrencyText.text = NumberFormatter.FormatNumber(architecture.Currency.Quantity, false);

            Button buy = architectureObject.transform.Find("Buy").GetComponent<Button>();
            TextMeshProUGUI buttonText = buy.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUY);
            RawImage buttonBackgroundImage = buy.transform.Find("Background").GetComponent<RawImage>();
            buttonBackgroundImage.color = ColorHelper.ToColor(ColorConstants.RED_COLOR);
            buy.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                GetQuantity(architecture.Currency.Quantity, architecture, subType, popupPanel, CurrencyPanel);
            });
        }

        List<Currencies> currencies = new List<Currencies>();
        currencies = UserCurrencyService.Create().GetArchitecturesCurrency(subType);
        FindObjectOfType<CurrenciesManager>().createCurrency(currencies, CurrencyPanel);
        currentContent.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void GetQuantity(double originPrice, object obj, string subType, Transform popupPanel, Transform CurrencyPanel)
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
        RawImage CurrencyImage = quantityObject.transform.Find("Price/CurrencyImage").GetComponent<RawImage>();
        TextMeshProUGUI priceText = quantityObject.transform.Find("Price/PriceText").GetComponent<TextMeshProUGUI>();
        RawImage equipmentImage = quantityObject.transform.Find("Image").GetComponent<RawImage>();

        TextMeshProUGUI buttonText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUY);
        // Lấy thuộc tính `Id` và `Image` từ object
        var idProperty = obj.GetType().GetProperty(AppConstants.StatFields.ID);
        var imageProperty = obj.GetType().GetProperty(AppConstants.StatFields.IMAGE);
        var CurrencyProperty = obj.GetType().GetProperty(AppConstants.MainType.CURRENCY);

        priceText.text = originPrice.ToString();
        double price = originPrice;
        int quantity = 1;
        quantityText.text = quantity.ToString();

        if (idProperty != null && imageProperty != null && CurrencyProperty != null)
        {
            string id = (string)idProperty.GetValue(obj);
            string image = (string)imageProperty.GetValue(obj);

            // Lấy đối tượng Currency từ obj
            var CurrencyObject = CurrencyProperty.GetValue(obj);

            if (CurrencyObject != null)
            {
                // Lấy thuộc tính "image" từ CurrencyObject
                var CurrencyImageProperty = CurrencyObject.GetType().GetProperty(AppConstants.StatFields.IMAGE);
                if (CurrencyImageProperty != null)
                {
                    string CurrencyImageValue = (string)CurrencyImageProperty.GetValue(CurrencyObject);

                    if (!string.IsNullOrEmpty(CurrencyImageValue))
                    {
                        string CurrencyFileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(CurrencyImageValue);
                        Texture CurrencyTexture = Resources.Load<Texture>($"{CurrencyFileNameWithoutExtension}");
                        CurrencyImage.texture = CurrencyTexture;
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
        maxButton.onClick.AddListener(() =>
        {
            Currencies userCurrency = new Currencies();
            if (obj is Architectures Architectures)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(Architectures.Currency.Id);
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
        confirmButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            if (obj is Architectures architecture)
            {
                architecture.Quantity = architecture.Quantity + quantity;
                UserCurrencyService.Create().UpdateUserCurrency(architecture.Currency.Id, price);
                bool success = UserArchitecturesService.Create().InsertUserArchitectures(architecture, User.CurrentUserId);
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

                    ArchitecturesGalleryService.Create().InsertArchitecturesGallery(architecture.Id);
                    currencies = UserCurrencyService.Create().GetArchitecturesCurrency(subType);
                    fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(architecture.Image);

                    ButtonEvent.Instance.Close(CurrencyPanel);
                    FindObjectOfType<CurrenciesManager>().createCurrency(currencies, CurrencyPanel);
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

                    PowerManagerService.Create().UpdateUserStats(User.CurrentUserId);
                    double newPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
                }
            }
        });
    }
}
