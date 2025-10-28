using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarsController : MonoBehaviour
{
    public static AvatarsController Instance { get; private set; }
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

    // Update is called once per frame
    void Update()
    {

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
    public void CreateAvatarsGallery(List<Avatars> avatars, Transform DictionaryContentPanel)
    {
        foreach (var avatar in avatars)
        {
            GameObject avatarObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = avatarObject.transform.Find("Title").GetComponent<Text>();
            Title.text = avatar.Name.Replace("_", " ");

            RawImage Image = avatarObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(avatar.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            Image.SetNativeSize();
            Image.rectTransform.sizeDelta = new Vector2(128, 128);

            Button button = avatarObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                PopupDetailsManager.Instance.PopupDetails(avatar, MainPanel);
            });

            RawImage rareImage = avatarObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{avatar.Rare}");
            rareImage.texture = rareTexture;

            RawImage rareBackgroundImage = avatarObject.transform.Find("RareBackground").GetComponent<RawImage>();
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
    public void CreateAvatarsTrade(List<Avatars> avatars, string subType, Transform currentContent,
    Transform currencyPanel, Transform popupPanel)
    {
        foreach (var avatar in avatars)
        {
            GameObject avatarObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = avatarObject.transform.Find("Title").GetComponent<Text>();
            Title.text = avatar.Name.Replace("_", " ");

            RawImage Image = avatarObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(avatar.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = avatarObject.transform.Find("Frame").GetComponent<RawImage>();

            Button button = FrameImage.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                PopupDetailsManager.Instance.PopupDetails(avatar, MainPanel);
            });

            // RawImage rareImage = medalObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = avatarObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(avatar.Currency.Image);
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = avatarObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = NumberFormatter.FormatNumber(avatar.Currency.Quantity, false);

            Button buy = avatarObject.transform.Find("Buy").GetComponent<Button>();
            TextMeshProUGUI buttonText = buy.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUY);
            buy.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                GetQuantity(avatar.Currency.Quantity, avatar, subType, popupPanel, currencyPanel);
            });
        }

        List<Currencies> currencies = new List<Currencies>();
        currencies = UserCurrencyService.Create().GetBordersCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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
            quantity++;
            price = originPrice * quantity;
            quantityText.text = quantity.ToString();
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
        });
        increase10Button.onClick.AddListener(() =>
        {
            quantity = quantity + 10;
            price = originPrice * quantity;
            quantityText.text = quantity.ToString();
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
        });
        maxButton.onClick.AddListener(() =>
        {
            Currencies userCurrency = new Currencies();
            if (obj is Borders borders)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(borders.Currency.Id);
            }
            // double price = double.Parse(priceText.text);

            int max = (int)(userCurrency.Quantity / price);
            price = originPrice * max;
            quantityText.text = max.ToString();
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
        });
        minButton.onClick.AddListener(() =>
        {
            quantityText.text = "1";
            price = originPrice * 1;
            priceText.text = price.ToString();
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
        });
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ButtonEvent.Instance.Close(popupPanel);
        });
        confirmButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            if (obj is Avatars avatars)
            {
                avatars.Quantity = avatars.Quantity + quantity;
                UserCurrencyService.Create().UpdateUserCurrency(avatars.Currency.Id, price);
                bool success = UserAvatarsService.Create().InsertUserAvatars(avatars);
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

                    AvatarsGalleryService.Create().InsertAvatarsGallery(avatars.Id);
                    currencies = UserCurrencyService.Create().GetBooksCurrency(subType);
                    fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(avatars.Image);

                    ButtonEvent.Instance.Close(currencyPanel);
                    FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
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

                    double currentPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
                    PowerManagerService.Create().UpdateUserStats(User.CurrentUserId);
                    double newPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
                }
            }
        });
    }
}
