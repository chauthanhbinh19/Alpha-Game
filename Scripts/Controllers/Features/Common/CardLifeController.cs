using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardLifeController : MonoBehaviour
{
    public static CardLifeController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject cardsPrefab;
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
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        equipmentsShopPrefab = UIManager.Instance.GetGameObject("equipmentsShopPrefab");
        quantityPopupPrefab = UIManager.Instance.GetGameObject("quantityPopupPrefab");
        receivedNotification = UIManager.Instance.GetGameObject("ReceivedNotification");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateCardLifeGallery(List<CardLife> cards, Transform DictionaryContentPanel)
    {
        foreach (var card in cards)
        {
            GameObject cardObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
            Title.text = card.name.Replace("_", " ");

            RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(card, MainPanel));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = DictionaryContentPanel.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
    }
    public void CreateCardLifeTrade(List<CardLife> cards, string subType, Transform currentContent,
    Transform currencyPanel, Transform popupPanel)
    {
        foreach (var card in cards)
        {
            GameObject cardObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
            Title.text = card.name.Replace("_", " ");

            RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = cardObject.transform.Find("Frame").GetComponent<RawImage>();
            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(card, MainPanel));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

            // RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = cardObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = card.currency.image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = cardObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = card.currency.quantity.ToString();

            Button buy = cardObject.transform.Find("Buy").GetComponent<Button>();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(card.currency.quantity, card, subType, popupPanel, currencyPanel);
            });
        }

        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetCardLifeCurrency(subType);
        FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    }
    public void GetQuantity(int price, object obj, string subType, Transform popupPanel, Transform currencyPanel)
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

        // Lấy thuộc tính `Id` và `Image` từ object
        var idProperty = obj.GetType().GetProperty("id");
        var imageProperty = obj.GetType().GetProperty("image");
        var currencyProperty = obj.GetType().GetProperty("currency");
        

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
                        string currencyFileNameWithoutExtension = currencyImageValue.Replace(".png", "");
                        Texture currencyTexture = Resources.Load<Texture>($"{currencyFileNameWithoutExtension}");
                        currencyImage.texture = currencyTexture;
                    }
                }
            }

            // Xử lý image của obj
            if (!string.IsNullOrEmpty(image))
            {
                string fileNameWithoutExtension = image.Replace(".png", "");
                Texture entityTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                equipmentImage.texture = entityTexture;
            }

            priceText.text = price.ToString();
        }

        else
        {
            Debug.LogError("Object không có thuộc tính Id hoặc Image");
        }

        priceText.text = price.ToString();
        double originPrice = price;
        increaseButton.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            currentQuantity++;
            price = originPrice * currentQuantity;
            quantityText.text = currentQuantity.ToString();
            priceText.text = price.ToString();
        });
        decreaseButton.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            if (currentQuantity > 1)
            {
                currentQuantity--;
                price = originPrice * currentQuantity;
                quantityText.text = currentQuantity.ToString();
                priceText.text = price.ToString();
            }
        });
        increase10Button.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            currentQuantity = currentQuantity + 10;
            price = originPrice * currentQuantity;
            quantityText.text = currentQuantity.ToString();
            priceText.text = price.ToString();
        });
        decrease10Button.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            if (currentQuantity > 10)
            {
                currentQuantity = currentQuantity - 10;
                price = originPrice * currentQuantity;
                quantityText.text = currentQuantity.ToString();
                priceText.text = price.ToString();
            }
        });
        maxButton.onClick.AddListener(() =>
        {
            Currency userCurrency = new Currency();
            if (obj is CardLife cardLife)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(cardLife.currency.id);
            }
            // double price = double.Parse(priceText.text);

            int max = (int)(userCurrency.quantity / price);
            double newprice = originPrice * max;
            quantityText.text = max.ToString();
            priceText.text = newprice.ToString();
        });
        minButton.onClick.AddListener(() =>
        {
            quantityText.text = "1";
            double price = double.Parse(priceText.text);
            price = originPrice * 1;
            priceText.text = price.ToString();
        });
        closeButton.onClick.AddListener(() => ButtonEvent.Instance.Close(popupPanel));
        confirmButton.onClick.AddListener(() =>
        {
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            for (int i = 1; i <= quantity; i++) // Duyệt từ 1 đến giá trị trong quantityText
            {
                if (obj is CardLife cardLife)
                {
                    UserCurrencyService.Create().UpdateUserCurrency(cardLife.currency.id, price);
                    bool success = UserCardLifeService.Create().InsertUserCardLife(cardLife);
                    if (!success)
                    {
                        allSuccess = false;
                        break;
                    }
                }
            }

            // Hiển thị thông báo dựa trên kết quả
            if (allSuccess)
            {
                string fileNameWithoutExtension = "";
                // Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
                List<Currency> currencies = new List<Currency>();
                string objType = "";
                if (obj is CardLife cardLife)
                {
                    CardLifeGalleryService.Create().InsertCardLifeGallery(cardLife.id);
                    currencies = UserCurrencyService.Create().GetSkillsCurrency(subType);
                    fileNameWithoutExtension = cardLife.image.Replace(".png", "");
                }
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

                if (objType.Equals("Achievements") || objType.Equals("Borders")
                || objType.Equals("Collaboration") || objType.Equals("CollaborationEquipment")
                || objType.Equals("Titles") || objType.Equals("Symbols") || objType.Equals("Medals")
                || objType.Equals("MagicFormationCircle") || objType.Equals("Talisman") || objType.Equals("Puppet")
                || objType.Equals("Alchemy") || objType.Equals("Forge") || objType.Equals("CardLife"))
                {
                    double currentPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
                    PowerManagerService.Create().UpdateUserStats(User.CurrentUserId);
                    double newPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
            }
            else
            {
                FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
            }
        });
    }
}
