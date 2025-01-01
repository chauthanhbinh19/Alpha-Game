using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class EquipmentManagement : MonoBehaviour
{
    public GameObject equipmentMenuPanel;
    private Transform MainPanel;
    private Transform EquipmentMenuPanel;
    private GameObject ItemsPrefab;
    private GameObject EquipmentsPanelPrefab;
    // public Transform content;
    private GameObject MainMenuPanel;
    private Transform MainMenuContent;
    private GameObject MainMenuShopPanel;
    private Transform MainMenuShopContent;
    private GameObject MainMenuEnhancementPanel;
    private Transform MainMenuEnhancementContent;
    private GameObject MainMenuCampaignPanel;
    private GameObject equipmentsPrefab;
    private GameObject equipmentsShopPrefab;
    private GameObject currencyPrefab;
    private Transform popupPanel;
    private GameObject quantityPopupPrefab;
    private Transform tempContent;
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private int count = 1;

    private GameObject MainMenuShopPanelObject;
    // Start is called before the first frame update
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ItemsPrefab = UIManager.Instance.GetGameObject("ItemPrefab");
        MainMenuPanel = UIManager.Instance.GetGameObject("MainMenuPanel");
        MainMenuShopPanel = UIManager.Instance.GetGameObject("MainMenuShopPanel");
        MainMenuEnhancementPanel = UIManager.Instance.GetGameObject("MainMenuEnhancementPanel");
        MainMenuCampaignPanel = UIManager.Instance.GetGameObject("MainMenuCampaignPanel");
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
        quantityPopupPrefab = UIManager.Instance.GetGameObject("quantityPopupPrefab");
        equipmentsPrefab = UIManager.Instance.GetGameObject("equipmentsPrefab");
        equipmentsShopPrefab = UIManager.Instance.GetGameObject("equipmentsShopPrefab");
        currencyPrefab = UIManager.Instance.GetGameObject("currencyPrefab");
        EquipmentMenuPanel = UIManager.Instance.GetTransform("equipmentMenuPanel");
        EquipmentsPanelPrefab = UIManager.Instance.GetGameObject("EquipmentsPanelPrefab");

        MainMenuContent = MainMenuPanel.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuContentPanel").GetComponent<Transform>();
        MainMenuShopContent = MainMenuShopPanel.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuShopContentPanel").GetComponent<Transform>();
        MainMenuEnhancementContent = MainMenuEnhancementPanel.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuEnhancementContentPanel").GetComponent<Transform>();
        // Lấy tất cả các button con trong equipmentMenuPanel
        Button[] buttons = equipmentMenuPanel.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string buttonName = button.name; // Lưu lại giá trị cục bộ để tránh lỗi closure
            int localCount = count;
            button.onClick.AddListener(() => OnButtonClick(buttonName, localCount));
            count = count + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnButtonClick(string type, int count)
    {
        GameObject equipmentObject = Instantiate(EquipmentsPanelPrefab, MainPanel);

        Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
        Title.text = type.Replace("_", " ");

        RawImage Image = equipmentObject.transform.Find("Background").GetComponent<RawImage>();
        string image = "Background_V1_" + count;
        Texture texture = Resources.Load<Texture>($"UI/Background1/{image}");
        Image.texture = texture;
        Button closeBtn = equipmentObject.transform.Find("CloseButton").GetComponent<Button>();
        closeBtn.onClick.AddListener(() => OnClose());

        Transform gridLayout = equipmentObject.transform.Find("GridLayout");
        if (gridLayout != null)
        {
            Button bagBtn = gridLayout.transform.Find("Bag").GetComponent<Button>();
            bagBtn.onClick.AddListener(() => GetBag(type));
            Button shopBtn = gridLayout.transform.Find("Shop").GetComponent<Button>();
            shopBtn.onClick.AddListener(() => GetShop(type));
            Button enhancementBtn = gridLayout.transform.Find("Enhancement").GetComponent<Button>();
            enhancementBtn.onClick.AddListener(() => GetEnhancement(type));
            Button campaignBtn = gridLayout.transform.Find("Campaign").GetComponent<Button>();
            campaignBtn.onClick.AddListener(() => GetCampaign(type));
        }
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void Close(Transform content)
    {
        count = 1;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void OnClose()
    {
        equipmentsPrefab.SetActive(false);
        foreach (Transform child in MainPanel)
        {
            Destroy(child.gameObject);
        }
    }
    public void ClosePanel(Transform content, GameObject panel)
    {
        Close(content);
        offset = 0;
        currentPage = 1;
        panel.SetActive(false);
    }
    private void createEquipmentsBag(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsPrefab, tempContent);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = tempContent.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(110, 130);
        }
    }
    private void createEquipmentsShop(List<Equipments> equipmentList, string type)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsShopPrefab, tempContent);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.rare}");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = equipmentObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            fileNameWithoutExtension = equipment.currency_image.Replace(".png", "");
            Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Text currencyTitle = equipmentObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyTitle.text = equipment.price.ToString().Replace("_", " ");

            Button buy = equipmentObject.transform.Find("Buy").GetComponent<Button>();
            Equipments equipments = new Equipments();
            buy.onClick.AddListener(() =>
            {
                GetQuantity(type, equipment);
            });
        }
        // GridLayoutGroup gridLayout = MainMenuContent.GetComponent<GridLayoutGroup>();
        // if (gridLayout != null)
        // {
        //     gridLayout.cellSize = new Vector2(110, 130);
        // }
    }
    private void createEquipmentsEnhancement(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsPrefab, MainMenuEnhancementContent);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = MainMenuContent.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(110, 130);
        }
    }
    private void createCurrency(List<Currency> currencies, Transform CurrencyPanel)
    {
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(currencyPrefab, CurrencyPanel);

            Text Title = currencyObject.transform.Find("Content").GetComponent<Text>();
            Title.text = currency.quantity.ToString();

            RawImage Image = currencyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = currency.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
        }
    }
    public void GetBag(string type)
    {
        GameObject MainMenuPanelObject = Instantiate(MainMenuPanel, MainPanel);
        int totalRecord = 0;
        Equipments equipmentsManager = new Equipments();
        List<Equipments> equipments = equipmentsManager.GetUserEquipments(type, pageSize, offset);
        tempContent = MainMenuPanelObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuContentPanel");
        createEquipmentsBag(equipments);

        totalRecord = equipmentsManager.GetUserEquipmentsCount(type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = MainMenuPanelObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Bag";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content, MainMenuPanelObject));

            GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
            gridLayout.cellSize = new Vector2(350, 130);
        }

        Transform button = MainMenuPanelObject.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuContentPanel");
            Text PageText = button.transform.Find("Page").GetComponent<Text>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() => ChangeNextPage(1, PageText, content, type));
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() => ChangePreviousPage(1, PageText, content, type));
        }

    }
    public void GetShop(string type)
    {
        MainMenuShopPanelObject = Instantiate(MainMenuShopPanel, MainPanel);
        int totalRecord = 0;
        Equipments equipmentsManager = new Equipments();
        List<Equipments> equipments = equipmentsManager.GetEquipmentsWithCurrency(type, pageSize, offset);
        tempContent = MainMenuShopPanelObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuShopContentPanel");
        createEquipmentsShop(equipments, type);

        totalRecord = equipmentsManager.GetEquipmentsCount(type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = MainMenuShopPanelObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Shop";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuShopContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content, MainMenuShopPanelObject));
        }

        Transform button = MainMenuShopPanelObject.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuShopContentPanel");
            Text PageText = button.transform.Find("Page").GetComponent<Text>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() => ChangeNextPage(2, PageText, content, type));
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() => ChangePreviousPage(2, PageText, content, type));
        }
        Currency currency = new Currency();
        List<Currency> currencies = currency.GetEquipmentsCurrency(type);
        Transform CurrencyPanel = MainMenuShopPanelObject.transform.Find("DictionaryCards/Currency");
        createCurrency(currencies, CurrencyPanel);
    }
    public void GetEnhancement(string type)
    {
        GameObject MainMenuEnhancementPanelObject = Instantiate(MainMenuEnhancementPanel, MainPanel);
        int totalRecord = 0;
        Equipments equipmentsManager = new Equipments();
        List<Equipments> equipments = equipmentsManager.GetUserEquipments(type, pageSize, offset);
        tempContent = MainMenuEnhancementPanelObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuEnhancementContentPanel");
        createEquipmentsEnhancement(equipments);

        totalRecord = equipmentsManager.GetUserEquipmentsCount(type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = MainMenuEnhancementPanelObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Enhancement";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuEnhancementContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content, MainMenuEnhancementPanel));
        }

        Transform button = MainMenuEnhancementPanelObject.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuEnhancementContentPanel");
            Text PageText = button.transform.Find("Page").GetComponent<Text>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() => ChangeNextPage(3, PageText, content, type));
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() => ChangePreviousPage(3, PageText, content, type));
        }
    }
    public void GetCampaign(string type)
    {
        // int totalRecord = 0;
        // Equipments equipmentsManager = new Equipments();
        // List<Equipments> equipments = equipmentsManager.GetUserEquipments(type, pageSize, offset);
        // createEquipmentsEnhancement(equipments);

        // totalRecord = equipmentsManager.GetUserEquipmentsCount(type);
        // totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = MainMenuCampaignPanel.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Campaign";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuCampaignContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content, MainMenuCampaignPanel));
        }

        // Transform button = MainMenuShopPanel.transform.Find("Pagination");
        // if (button != null)
        // {
        //     Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
        //     Text PageText = button.transform.Find("Page").GetComponent<Text>();
        //     PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        //     Button NextButton = button.transform.Find("Next").GetComponent<Button>();
        //     NextButton.onClick.AddListener(() => ChangeNextPage(3, PageText, content,type));
        //     Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
        //     PreviousButton.onClick.AddListener(() => ChangePreviousPage(3, PageText, content,type));
        // }
    }
    public void ChangeNextPage(int status, Text PageText, Transform content, string subType)
    {
        Close(content);
        if (currentPage < totalPage)
        {
            int totalRecord = 0;

            if (status == 1)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetUserEquipments(subType, pageSize, offset);
                createEquipmentsBag(equipments);
            }
            else if (status == 2)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetEquipmentsWithCurrency(subType, pageSize, offset);
                createEquipmentsShop(equipments, subType);
            }
            else if (status == 3)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetUserEquipments(subType, pageSize, offset);
                createEquipmentsEnhancement(equipments);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage(int status, Text PageText, Transform content, string subType)
    {
        Close(content);
        if (currentPage > 1)
        {
            int totalRecord = 0;

            if (status == 1)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetUserEquipments(subType, pageSize, offset);
                createEquipmentsBag(equipments);
            }
            else if (status == 2)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetEquipmentsWithCurrency(subType, pageSize, offset);
                createEquipmentsShop(equipments, subType);
            }
            else if (status == 3)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetUserEquipments(subType, pageSize, offset);
                createEquipmentsShop(equipments, subType);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void GetQuantity(string type, Equipments equipments)
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

        Currency currency = new Currency();
        currency = currency.GetUserEquipmentsPrice(type,equipments.id);
        string fileNameWithoutExtension = currency.image.Replace(".png", "");
        Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        currencyImage.texture = currencyTexture;

        fileNameWithoutExtension = equipments.image.Replace(".png", "");
        Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        equipmentImage.texture = equipmentTexture;

        currency = currency.GetEquipmentsPrice(type, equipments.id);
        priceText.text = currency.quantity.ToString();
        double originPrice = currency.quantity;
        increaseButton.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            currentQuantity++;
            price = originPrice*currentQuantity;
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
                price = originPrice*currentQuantity;
                quantityText.text = currentQuantity.ToString();
                priceText.text = price.ToString();
            }
        });
        increase10Button.onClick.AddListener(() =>
        {
            int currentQuantity = int.Parse(quantityText.text);
            double price = double.Parse(priceText.text);
            currentQuantity = currentQuantity + 10;
            price = originPrice*currentQuantity;
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
                price = originPrice*currentQuantity;
                quantityText.text = currentQuantity.ToString();
                priceText.text = price.ToString();
            }
        });
        maxButton.onClick.AddListener(() =>
        {
            Currency currency = new Currency();
            List<Currency> userCurrency = currency.GetEquipmentsCurrency(type);
            Currency equipmentPrice = currency.GetEquipmentsPrice(type, equipments.id);
            double price = double.Parse(priceText.text);
            foreach (var user in userCurrency)
            {
                if (user.id == equipmentPrice.id)
                {
                    int max = (int)(user.quantity / equipmentPrice.quantity);
                    price = originPrice*max;
                    quantityText.text = max.ToString();
                    priceText.text = price.ToString();
                    break;
                }
            }
        });
        minButton.onClick.AddListener(() =>
        {
            quantityText.text = "1";
            double price = double.Parse(priceText.text);
            price = originPrice*1;
            priceText.text = price.ToString();
        });
        closeButton.onClick.AddListener(() => Close(popupPanel));
        confirmButton.onClick.AddListener(() =>
        {
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            for (int i = 1; i <= quantity; i++) // Duyệt từ 1 đến giá trị trong quantityText
            {
                equipments.UpdateUserCurrency(equipments.id);
                bool success = equipments.BuyEquipment(equipments.id); // Thực hiện mua từng món đồ
                if (!success)
                {
                    allSuccess = false; // Nếu có giao dịch thất bại, đánh dấu thất bại
                    break; // Ngừng vòng lặp nếu có lỗi
                }
            }

            // Hiển thị thông báo dựa trên kết quả
            if (allSuccess)
            {
                equipments.UpdateEquipmentsGallery(equipments.id);
                Currency currency = new Currency();
                List<Currency> currencies = currency.GetEquipmentsCurrency(type);
                Transform CurrencyPanel = MainMenuShopPanelObject.transform.Find("DictionaryCards/Currency");
                Close(CurrencyPanel);
                createCurrency(currencies, CurrencyPanel);
                Close(popupPanel);
                FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
            }
            else
            {
                FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
            }
        });
    }
}
