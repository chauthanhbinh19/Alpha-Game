using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class EquipmentManager : MonoBehaviour
{
    private GameObject equipmentMenuPanel;
    private Transform MainPanel;
    // private Transform EquipmentMenuPanel;
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
    private Transform popupPanel;
    private GameObject quantityPopupPrefab;
    private Transform tempContent;
    private GameObject campaignPrefab;
    private GameObject campaignDetailPrefab;
    private GameObject cardsPrefab;
    private GameObject ReceivedNotification;
    private GameObject ItemThird;
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private int count = 1;

    private GameObject currentObject;
    // Start is called before the first frame update
    public void CreateEquipments(Transform EquipmentMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        equipmentMenuPanel = EquipmentMenuPanel.gameObject;
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
        // EquipmentMenuPanel = UIManager.Instance.GetTransform("equipmentMenuPanel");
        EquipmentsPanelPrefab = UIManager.Instance.GetGameObject("EquipmentsPanelPrefab");
        campaignPrefab = UIManager.Instance.GetGameObject("CampaignPrefab");
        campaignDetailPrefab = UIManager.Instance.GetGameObject("CampaignDetailPrefab");
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        ReceivedNotification = UIManager.Instance.GetGameObject("ReceivedNotification");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");

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
        // equipmentsPrefab.SetActive(false);
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

            Text Power = equipmentObject.transform.Find("Power").GetComponent<Text>();
            Power.text = equipment.power.ToString();

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage BorderImage = equipmentObject.transform.Find("Border").GetComponent<RawImage>();
            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = BorderImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = BorderImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(equipment, MainPanel));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = tempContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);

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
    private void createEquipmentsCampaign(Campaigns campaigns)
    {
        float xOffset = -650; // Khoảng cách cố định giữa các equipmentObject trên trục X
        float yAmplitude = 100; // Biên độ dao động trên trục Y
        float yFrequency = Mathf.PI / 6; // Tần số dao động trên trục Y

        int index = 0; // Dùng để tính toán vị trí dựa trên chỉ s
        foreach (var campaignDetail in campaigns.campaignDetails)
        {
            GameObject equipmentObject = Instantiate(campaignPrefab, tempContent);

            TextMeshProUGUI title = equipmentObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            title.text = campaignDetail.name.Replace("_", " ");

            // RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            // string fileNameWithoutExtension = equipment.image.Replace(".png", "");
            // fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            // Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            // Image.texture = texture;
            // Tính toán vị trí cho equipmentObject
            RectTransform equipmentRect = equipmentObject.GetComponent<RectTransform>();
            float x = xOffset + 140 * index; // Khoảng cách cố định giữa các equipmentObject trên trục X
            float y = Mathf.Sin(index * yFrequency) * yAmplitude; // Dao động theo hàm sin trên trục Y
            equipmentRect.anchoredPosition = new Vector2(x, y);

            if (campaignDetail.Equals("available"))
            {
                Transform starGroup = equipmentObject.transform.Find("StarGroup");
                Texture texture = Resources.Load<Texture>("UI/UI/BlueStar");

                // Xóa các ngôi sao hiện có trong StarGroup để tránh trùng lặp
                foreach (Transform child in starGroup)
                {
                    GameObject.Destroy(child.gameObject);
                }

                // Tạo các ngôi sao dựa trên số lượng sao
                int starCount = campaignDetail.stars; // Số lượng sao, ví dụ: 1, 2, ..., 5
                for (int i = 0; i < starCount; i++)
                {
                    GameObject star = new GameObject("Star"); // Tạo đối tượng GameObject mới
                    star.transform.SetParent(starGroup, false); // Đặt làm con của StarGroup

                    RawImage rawImage = star.AddComponent<RawImage>(); // Thêm RawImage
                    rawImage.texture = texture; // Gán texture ngôi sao
                    rawImage.rectTransform.sizeDelta = new Vector2(50, 50); // Kích thước ngôi sao
                    rawImage.rectTransform.anchoredPosition = new Vector2(i * 60, 0); // Vị trí sao (cách đều nhau)
                }
            }
            RawImage FlagImage = equipmentObject.transform.Find("FlagImage").GetComponent<RawImage>();
            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = FlagImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FlagImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }
            // Gán sự kiện click
            AddClickListener(eventTrigger, () => PopupCampaignDetail(campaignDetail));
            index++; // Tăng chỉ số cho lần lặp tiếp theo
        }
        // GridLayoutGroup gridLayout = tempContent.GetComponent<GridLayoutGroup>();
        // if (gridLayout != null)
        // {
        //     gridLayout.cellSize = new Vector2(110, 130);
        // }
    }
    public void GetBag(string type)
    {
        currentObject = Instantiate(MainMenuPanel, MainPanel);
        int totalRecord = 0;
        var userEquipmentsService = UserEquipmentsService.Create();
        List<Equipments> equipments = userEquipmentsService.GetUserEquipments(User.CurrentUserId, type, pageSize, offset);
        tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuContentPanel");
        createEquipmentsBag(equipments);

        totalRecord = userEquipmentsService.GetUserEquipmentsCount(User.CurrentUserId, type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = currentObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Bag";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Destroy(currentObject));
            Button HomeButton = DictionaryPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));

            GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
            gridLayout.cellSize = new Vector2(350, 130);
        }

        Transform button = currentObject.transform.Find("Pagination");
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
        currentObject = Instantiate(MainMenuShopPanel, MainPanel);
        int totalRecord = 0;
        var equipmentsService = EquipmentsService.Create();
        List<Equipments> equipments = equipmentsService.GetEquipmentsWithCurrency(type, pageSize, offset);
        tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuShopContentPanel");
        createEquipmentsShop(equipments, type);

        totalRecord = equipmentsService.GetEquipmentsCount(type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = currentObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Shop";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuShopContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Destroy(currentObject));
            Button HomeButton = DictionaryPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
        }

        Transform button = currentObject.transform.Find("Pagination");
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
        Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
        FindObjectOfType<CurrencyManager>().GetEquipmentsCurrency(type, CurrencyPanel);
    }
    public void GetEnhancement(string type)
    {
        currentObject = Instantiate(MainMenuEnhancementPanel, MainPanel);
        int totalRecord = 0;
        var userEquipmentsService = UserEquipmentsService.Create();
        List<Equipments> equipments = userEquipmentsService.GetUserEquipments(User.CurrentUserId, type, pageSize, offset);
        tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuEnhancementContentPanel");
        createEquipmentsEnhancement(equipments);

        totalRecord = userEquipmentsService.GetUserEquipmentsCount(User.CurrentUserId, type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = currentObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Enhancement";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuEnhancementContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Destroy(currentObject));
            Button HomeButton = DictionaryPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
        }

        Transform button = currentObject.transform.Find("Pagination");
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
        currentObject = Instantiate(MainMenuCampaignPanel, MainPanel);
        // int totalRecord = 0;
        UserCampaign userCampaign = new UserCampaign();
        Campaigns userCampaigns = userCampaign.GetCampaignsForUser("Chapter 1", type);
        tempContent = currentObject.transform.Find("DictionaryCards/CampaignGroup");
        createEquipmentsCampaign(userCampaigns);

        // totalRecord = equipmentsManager.GetUserEquipmentsCount(type);
        // totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = currentObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Campaign";
            TextMeshProUGUI chapter = DictionaryPanel.transform.Find("CampaignTitleText").GetComponent<TextMeshProUGUI>();
            chapter.text = userCampaigns.chapter.Replace("_", " ");
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuCampaignContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => Destroy(currentObject));
            Button HomeButton = DictionaryPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() => Close(MainPanel));
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
        Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
        var currencyService = UserCurrencyService.Create();
        List<Currency> currencies = new List<Currency>();
        currencies = currencyService.GetUserCurrency();
        FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);
    }
    public void ChangeNextPage(int status, Text PageText, Transform content, string subType)
    {
        if (currentPage < totalPage)
        {
            Close(content);
            int totalRecord = 0;

            if (status == 1)
            {
                var userEquipmentsService = UserEquipmentsService.Create();
                totalRecord = userEquipmentsService.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = userEquipmentsService.GetUserEquipments(User.CurrentUserId, subType, pageSize, offset);
                createEquipmentsBag(equipments);
            }
            else if (status == 2)
            {
                var equipmentsService = EquipmentsService.Create();
                totalRecord = equipmentsService.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentsService.GetEquipmentsWithCurrency(subType, pageSize, offset);
                createEquipmentsShop(equipments, subType);
            }
            else if (status == 3)
            {
                var userEquipmentsService = UserEquipmentsService.Create();
                totalRecord = userEquipmentsService.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = userEquipmentsService.GetUserEquipments(User.CurrentUserId, subType, pageSize, offset);
                createEquipmentsEnhancement(equipments);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage(int status, Text PageText, Transform content, string subType)
    {
        if (currentPage > 1)
        {
            Close(content);
            int totalRecord = 0;

            if (status == 1)
            {
                var userEquipmentsService = UserEquipmentsService.Create();
                totalRecord = userEquipmentsService.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = userEquipmentsService.GetUserEquipments(User.CurrentUserId, subType, pageSize, offset);
                createEquipmentsBag(equipments);
            }
            else if (status == 2)
            {
                var equipmentsService = EquipmentsService.Create();
                totalRecord = equipmentsService.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentsService.GetEquipmentsWithCurrency(subType, pageSize, offset);
                createEquipmentsShop(equipments, subType);
            }
            else if (status == 3)
            {
                var userEquipmentsService = UserEquipmentsService.Create();
                totalRecord = userEquipmentsService.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = userEquipmentsService.GetUserEquipments(User.CurrentUserId, subType, pageSize, offset);
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

        var userEquipmentsService = UserEquipmentsService.Create();
        var equipmentsGalleryService = EquipmentsGalleryService.Create();
        var userCurrencyService = UserCurrencyService.Create();
        Currency currency = userCurrencyService.GetUserEquipmentsPrice(type, equipments.id);
        string fileNameWithoutExtension = currency.image.Replace(".png", "");
        Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        currencyImage.texture = currencyTexture;

        fileNameWithoutExtension = equipments.image.Replace(".png", "");
        fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
        Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        equipmentImage.texture = equipmentTexture;

        currency = userCurrencyService.GetEquipmentsPrice(type, equipments.id);
        priceText.text = currency.quantity.ToString();
        double originPrice = currency.quantity;
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
            var userCurrencyService = UserCurrencyService.Create();
            List<Currency> userCurrency = userCurrencyService.GetEquipmentsCurrency(type);
            Currency equipmentPrice = userCurrencyService.GetEquipmentsPrice(type, equipments.id);
            double price = double.Parse(priceText.text);
            foreach (var user in userCurrency)
            {
                if (user.id == equipmentPrice.id)
                {
                    int max = (int)(user.quantity / equipmentPrice.quantity);
                    price = originPrice * max;
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
            price = originPrice * 1;
            priceText.text = price.ToString();
        });
        closeButton.onClick.AddListener(() => Close(popupPanel));
        confirmButton.onClick.AddListener(() =>
        {
            int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
            bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

            for (int i = 1; i <= quantity; i++) // Duyệt từ 1 đến giá trị trong quantityText
            {
                userEquipmentsService.UpdateUserCurrency(equipments.id);
                bool success = userEquipmentsService.BuyEquipment(equipments.id); // Thực hiện mua từng món đồ
                if (!success)
                {
                    allSuccess = false; // Nếu có giao dịch thất bại, đánh dấu thất bại
                    break; // Ngừng vòng lặp nếu có lỗi
                }
            }

            // Hiển thị thông báo dựa trên kết quả
            if (allSuccess)
            {
                equipmentsGalleryService.InsertEquipmentsGallery(equipments.id);
                Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
                Close(CurrencyPanel);
                FindObjectOfType<CurrencyManager>().GetEquipmentsCurrency(type, CurrencyPanel);
                Close(popupPanel);
                // FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
                GameObject receivedNotificationObject = Instantiate(ReceivedNotification, popupPanel);

                AddCloseEvent(receivedNotificationObject);
                Transform itemContent = receivedNotificationObject.transform.Find("Scroll View/Viewport/Content");
                GameObject itemObject = Instantiate(ItemThird, itemContent);

                RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
                fileNameWithoutExtension = equipments.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                eQuantity.text = quantity.ToString();
            }
            else
            {
                FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
            }
        });
    }
    private void AddCloseEvent(GameObject obj)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) =>
        {
           Destroy(obj);
        });
        trigger.triggers.Add(entry);
    }
    void AddClickListener(EventTrigger trigger, System.Action callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { callback(); });
        trigger.triggers.Add(entry);
    }
    public void PopupCampaignDetail(CampaignDetail campaignDetail)
    {
        GameObject popupObject = Instantiate(campaignDetailPrefab, MainPanel);
        Transform itemsGroup = popupObject.transform.Find("ItemsGroup/Scroll View/Viewport/Content");
        Transform enemyGroup = popupObject.transform.Find("EnemyGroup/Scroll View/Viewport/Content");
        Transform currencyGroup = popupObject.transform.Find("CurrencyGroup");
        Transform starsGroup = popupObject.transform.Find("StarsGroup");
        Button closeButton = popupObject.transform.Find("CloseButton").GetComponent<Button>();
        Button startButton = popupObject.transform.Find("StartButton").GetComponent<Button>();
        TextMeshProUGUI titleText = popupObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();

        closeButton.onClick.AddListener(() =>
        {
            Destroy(popupObject);
        });
        titleText.text = campaignDetail.name;
        foreach (CampaignReward campaignReward in campaignDetail.campaignRewards)
        {
            GameObject itemObject = Instantiate(ItemsPrefab, itemsGroup);
            Text itemTitleText = itemObject.transform.Find("ItemName").GetComponent<Text>();
            RawImage itemBackground = itemObject.transform.Find("ItemBackground").GetComponent<RawImage>();
            itemTitleText.gameObject.SetActive(false);
            // itemBackground.gameObject.SetActive(false);

            Texture backgroundTexture = Resources.Load<Texture>("UI/Material_473");
            itemBackground.texture = backgroundTexture;

            RawImage itemImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = campaignReward.items.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            itemImage.texture = itemTexture;

            // Chỉnh kích thước RectTransform thành 100x100
            RectTransform rectTransform = itemImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            RectTransform backgroundRectTransform = itemBackground.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            // Debug.Log(campaignReward.items.image);
        }
        foreach (CampaignDetailCard campaignDetailCardcard in campaignDetail.campaignDetailCards)
        {
            GameObject cardObject = Instantiate(cardsPrefab, enemyGroup);
            RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = campaignDetailCardcard.cards.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
        }
    }
}
