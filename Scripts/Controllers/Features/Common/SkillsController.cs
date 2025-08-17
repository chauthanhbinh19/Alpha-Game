using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillsController : MonoBehaviour
{
    public static SkillsController Instance { get; private set; }
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        equipmentsShopPrefab = UIManager.Instance.GetGameObject("equipmentsShopPrefab");
        quantityPopupPrefab = UIManager.Instance.GetGameObject("quantityPopupPrefab");
        receivedNotification = UIManager.Instance.GetGameObject("ReceivedNotification");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateSkillsGallery(List<Skills> skillsList, Transform DictionaryContentPanel)
    {
        foreach (var skill in skillsList)
        {
            GameObject skillObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = skillObject.transform.Find("Title").GetComponent<Text>();
            Title.text = skill.name.Replace("_", " ");

            RawImage Image = skillObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(skill.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            
            Button button = skillObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                PopupDetailsManager.Instance.PopupDetails(skill, MainPanel);
            });
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = skillObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skill.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
        }
    }
    public void CreateSkillsTrade(List<Skills> skillsList, string subType, Transform currentContent,
    Transform currencyPanel, Transform popupPanel)
    {
        foreach (var skill in skillsList)
        {
            GameObject skillObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = skillObject.transform.Find("Title").GetComponent<Text>();
            Title.text = skill.name.Replace("_", " ");

            RawImage Image = skillObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(skill.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = skillObject.transform.Find("Frame").GetComponent<RawImage>();
            
            Button button = FrameImage.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                PopupDetailsManager.Instance.PopupDetails(skill, MainPanel);
            });
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            // RawImage rareImage = skillObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skill.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = skillObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(skill.currency.image);
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = skillObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = NumberFormatter.FormatNumber(skill.currency.quantity, false);

            Button buy = skillObject.transform.Find("Buy").GetComponent<Button>();
            TextMeshProUGUI buttonText = buy.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = LocalizationManager.Get(AppConstants.Buy);
            buy.onClick.AddListener(() =>
            {
                GetQuantity(skill.currency.quantity, skill, subType, popupPanel, currencyPanel);
            });
        }

        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    public void GetQuantity(int originPrice, object obj, string subType, Transform popupPanel, Transform currencyPanel)
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
        buttonText.text = LocalizationManager.Get(AppConstants.Buy);
        // Lấy thuộc tính `Id` và `Image` từ object
        var idProperty = obj.GetType().GetProperty("id");
        var imageProperty = obj.GetType().GetProperty("image");
        var currencyProperty = obj.GetType().GetProperty("currency");

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
        });
        increase10Button.onClick.AddListener(() =>
        {
            quantity = quantity + 10;
            price = originPrice * quantity;
            quantityText.text = quantity.ToString();
            priceText.text = price.ToString();
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
        });
        maxButton.onClick.AddListener(() =>
        {
            Currency userCurrency = new Currency();
            if (obj is Skills skill)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(skill.currency.id);
            }
            // double price = double.Parse(priceText.text);

            int max = (int)(userCurrency.quantity / price);
            price = originPrice * max;
            quantityText.text = max.ToString();
            priceText.text = price.ToString();
        });
        minButton.onClick.AddListener(() =>
        {
            quantityText.text = "1";
            price = originPrice * 1;
            priceText.text = price.ToString();
        });
        closeButton.onClick.AddListener(() => ButtonEvent.Instance.Close(popupPanel));
        confirmButton.onClick.AddListener(() =>
        {
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            if (obj is Skills skill)
            {
                skill.quantity = skill.quantity + quantity;
                UserCurrencyService.Create().UpdateUserCurrency(skill.currency.id, price);
                bool success = UserSkillsService.Create().InsertUserSkills(skill);
                if (!success)
                {
                    allSuccess = false;
                }

                // Hiển thị thông báo dựa trên kết quả
                if (allSuccess)
                {
                    string fileNameWithoutExtension = "";
                    // Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
                    List<Currency> currencies = new List<Currency>();

                    SkillsGalleryService.Create().InsertSkillsGallery(skill.id);
                    currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
                    fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(skill.image);

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
                }
                else
                {
                    FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
                }
            }
        });
    }
}
