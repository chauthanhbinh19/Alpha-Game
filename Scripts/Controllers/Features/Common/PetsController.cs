using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PetsController : MonoBehaviour
{
    public static PetsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject cardsPrefab;
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
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
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
    public void CreatePetsGallery(List<Pets> petsList, Transform DictionaryContentPanel)
    {
        foreach (var pet in petsList)
        {
            GameObject petsObject;
            if (pet.type.Equals("Legendary_Dragon") || pet.type.Equals("Naruto_Bijuu") || pet.type.Equals("Naruto_Susanoo") || pet.type.Equals("One_Piece_Ship") || pet.type.Equals("Prime_Monster"))
            {
                petsObject = Instantiate(cardsPrefab, DictionaryContentPanel);
                RawImage Background = petsObject.transform.Find("Background").GetComponent<RawImage>();
                Background.gameObject.SetActive(true);

                GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
                if (gridLayout != null)
                {
                    gridLayout.cellSize = new Vector2(280, 280);
                }
            }
            else
            {
                petsObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

                GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
                if (gridLayout != null)
                {
                    gridLayout.cellSize = new Vector2(200, 230);
                }
            }

            Text Title = petsObject.transform.Find("Title").GetComponent<Text>();
            Title.text = pet.name.Replace("_", " ");

            RawImage Image = petsObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(pet.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            EventTrigger eventTrigger = Image.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = Image.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(pet, MainPanel));
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

            if (pet.type.Equals("Prime_Monster"))
            {
                Image.SetNativeSize();
                Image.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            RawImage rareImage = petsObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

        }
    }
    public void CreatePetsTrade(List<Pets> petsList, string subType, Transform currentContent,
    Transform currencyPanel, Transform popupPanel)
    {
        foreach (var pet in petsList)
        {
            GameObject petsObject;
            if (pet.type.Equals("Legendary_Dragon") || pet.type.Equals("Naruto_Bijuu") || pet.type.Equals("Naruto_Susanoo") || pet.type.Equals("One_Piece_Ship") || pet.type.Equals("Prime_Monster"))
            {
                petsObject = Instantiate(equipmentsShopPrefab, currentContent);
                RawImage Background = petsObject.transform.Find("Background").GetComponent<RawImage>();
                Background.gameObject.SetActive(true);

                // GridLayoutGroup gridLayout = currentContent.GetComponent<GridLayoutGroup>();
                // if (gridLayout != null)
                // {
                //     gridLayout.cellSize = new Vector2(280, 280);
                // }
            }
            else
            {
                petsObject = Instantiate(equipmentsShopPrefab, currentContent);

                // GridLayoutGroup gridLayout = currentContent.GetComponent<GridLayoutGroup>();
                // if (gridLayout != null)
                // {
                //     gridLayout.cellSize = new Vector2(200, 230);
                // }
            }

            Text Title = petsObject.transform.Find("Title").GetComponent<Text>();
            Title.text = pet.name.Replace("_", " ");

            RawImage Image = petsObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(pet.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = petsObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(pet, MainPanel));
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

            // if (pet.type.Equals("Prime_Monster"))
            // {
            //     Image.SetNativeSize();
            //     Image.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            // }

            // RawImage rareImage = petsObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            // rareImage.texture = rareTexture;
            RawImage currencyImage = petsObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(pet.currency.image);
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyText = petsObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyText.text = pet.currency.quantity.ToString();

            Button buy = petsObject.transform.Find("Buy").GetComponent<Button>();
            TextMeshProUGUI buttonText = buy.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = LocalizationManager.Get(AppConstants.Buy);
            buy.onClick.AddListener(() =>
            {
                GetQuantity(pet.currency.quantity, pet, subType, popupPanel, currencyPanel);
            });
        }

        List<Currency> currencies = new List<Currency>();
        currencies = UserCurrencyService.Create().GetPetsCurrency(subType);
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

        TextMeshProUGUI buttonText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = LocalizationManager.Get(AppConstants.Buy);
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
            if (obj is Pets pets)
            {
                userCurrency = UserCurrencyService.Create().GetUserCurrencyById(pets.currency.id);
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

            if (obj is Pets pets)
            {
                pets.quantity = pets.quantity + quantity;
                UserCurrencyService.Create().UpdateUserCurrency(pets.currency.id, price);
                bool success = UserPetsService.Create().InsertUserPets(pets);
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
                    string objType = "";

                    PetsGalleryService.Create().InsertPetsGallery(pets.id);
                    currencies = UserCurrencyService.Create().GetPetsCurrency(subType);
                    fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(pets.image);

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
            }
        });
    }
}
