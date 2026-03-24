using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class EquipmentManager : MonoBehaviour
{
    private GameObject ItemButtonPrefab;
    private GameObject equipmentMenuPanel;
    private Transform MainPanel;
    // private Transform EquipmentMenuPanel;
    private GameObject ItemsPrefab;
    private GameObject EquipmentPanelPrefab;
    // public Transform content;
    private GameObject MainMenuPanelPrefab;
    private Transform MainMenuContent;
    private GameObject MainMenuShopPanelPrefab;
    private Transform MainMenuShopContent;
    private GameObject MainMenuEnhancementPanelPrefab;
    private Transform MainMenuEnhancementContent;
    private GameObject MainMenuCampaignPanel;
    private GameObject equipmentsPrefab;
    private GameObject EquipmentShopPrefab;
    private GameObject EquipmentPrefab;
    private Transform popupPanel;
    private GameObject quantityPopupPrefab;
    private Transform tempContent;
    private GameObject campaignPrefab;
    private GameObject campaignDetailPrefab;
    private GameObject cardsPrefab;
    private GameObject ReceivedNotificationPanelPrefab;
    private GameObject ItemPopupPrefab;
    private int offset;
    private int currentPage;
    private int totalPage;
    private const int PAGE_SIZE = 100;
    private int count = 1;
    private string search;
    // private string type;
    private string rare;
    private GameObject currentObject;
    public static EquipmentManager Instance { get; private set; }
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        ItemButtonPrefab = UIManager.Instance.Get("ItemButtonPrefab");
    }
    public async Task CreateEquipmentsButtonAsync(Transform equipmentMenuPanel)
    {
        Texture2D itemBackground = Resources.Load<Texture2D>(ImageConstants.Badge.BADGE_EQUIPMENT_URL);
        //Equipment menu
        var equipment = EquipmentsService.Create();
        List<string> uniqueTypes = await equipment.GetUniqueEquipmentsTypesAsync();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                string subtype = uniqueTypes[i];
                CreateEquipmentButtonUI(subtype, itemBackground, Resources.Load<Texture2D>($"UI/Button/Equipments/{subtype}"), equipmentMenuPanel);
            }
        }
        FindAnyObjectByType<EquipmentManager>().CreateEquipments(equipmentMenuPanel);
        equipmentMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateEquipmentButtonUI(string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ItemButtonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName.Replace("_", ""));
        }
    }
    public void CreateEquipments(Transform EquipmentMenuPanel)
    {
        offset = 0;
        currentPage = 1;
        search = "";
        // type = AppConstants.Type.ALL;
        rare = AppConstants.Rare.ALL;
        equipmentMenuPanel = EquipmentMenuPanel.gameObject;
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ItemsPrefab = UIManager.Instance.Get("ItemPrefab");
        MainMenuPanelPrefab = UIManager.Instance.Get("MainMenuPanelPrefab");
        MainMenuShopPanelPrefab = UIManager.Instance.Get("MainMenuShopPanelPrefab");
        MainMenuEnhancementPanelPrefab = UIManager.Instance.Get("MainMenuEnhancementPanelPrefab");
        MainMenuCampaignPanel = UIManager.Instance.Get("MainMenuCampaignPanel");
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
        quantityPopupPrefab = UIManager.Instance.Get("QuantityPopupPrefab");
        equipmentsPrefab = UIManager.Instance.Get("equipmentsPrefab");
        EquipmentShopPrefab = UIManager.Instance.Get("EquipmentShopPrefab");
        EquipmentPrefab = UIManager.Instance.Get("EquipmentPrefab");
        // EquipmentMenuPanel = UIManager.Instance.GetTransform("equipmentMenuPanel");
        EquipmentPanelPrefab = UIManager.Instance.Get("EquipmentPanelPrefab");
        campaignPrefab = UIManager.Instance.Get("CampaignPrefab");
        campaignDetailPrefab = UIManager.Instance.Get("CampaignDetailPrefab");
        cardsPrefab = UIManager.Instance.Get("CardsPrefab");
        ReceivedNotificationPanelPrefab = UIManager.Instance.Get("ReceivedNotificationPanelPrefab");
        ItemPopupPrefab = UIManager.Instance.Get("ItemPopupPrefab");

        MainMenuContent = MainMenuPanelPrefab.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuContentPanel").GetComponent<Transform>();
        MainMenuShopContent = MainMenuShopPanelPrefab.transform.Find("DictionaryCards/Scroll View/Viewport/Content").GetComponent<Transform>();
        MainMenuEnhancementContent = MainMenuEnhancementPanelPrefab.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuEnhancementContentPanel").GetComponent<Transform>();
        // Lấy tất cả các button con trong equipmentMenuPanel
        Button[] buttons = equipmentMenuPanel.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string buttonName = button.name; // Lưu lại giá trị cục bộ để tránh lỗi closure
            int localCount = count;
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                OnButtonClick(buttonName, localCount);
            });
            count = count + 1;
        }
    }
    public void OnButtonClick(string type, int count)
    {
        GameObject equipmentObject = Instantiate(EquipmentPanelPrefab, MainPanel);

        Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
        Title.text = LocalizationManager.Get(type);

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
            bagBtn.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
                await GetBagAsync(type);
            });
            Button shopBtn = gridLayout.transform.Find("Shop").GetComponent<Button>();
            shopBtn.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
                await GetShopAsync(type);
            });
            Button enhancementBtn = gridLayout.transform.Find("Enhancement").GetComponent<Button>();
            enhancementBtn.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
                await GetEnhancementAsync(type);
            });
            Button missionBtn = gridLayout.transform.Find("Mission").GetComponent<Button>();
            Button campaignBtn = gridLayout.transform.Find("Campaign").GetComponent<Button>();
            campaignBtn.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
                // await GetCampaignAsync(type);
            });
        }
        gridLayout.gameObject.AddComponent<SlideTopToBottomAnimation>();
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
    private void CreateEquipmentsBag(List<Equipments> equipments, string type)
    {
        foreach (var equipment in equipments)
        {
            GameObject equipmentObject = Instantiate(EquipmentPrefab, tempContent);

            TextMeshProUGUI Title = equipmentObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = equipment.Name.Replace("_", " ");

            TextMeshProUGUI Power = equipmentObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
            Power.text = equipment.Power.ToString();

            RawImage BackgroundImage = equipmentObject.transform.Find("Background").GetComponent<RawImage>();
            Texture backgroundTexture = Resources.Load<Texture>($"{EvaluateSlotForEquipment.BackgroundImageForEquipment(type)}");
            BackgroundImage.texture = backgroundTexture;

            RawImage Image = equipmentObject.transform.Find("MainImage").GetComponent<RawImage>();
            Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(equipment.Image)}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage BorderImage = equipmentObject.transform.Find("FrameImage").GetComponent<RawImage>();
            Texture frameTexture = Resources.Load<Texture>($"{EvaluateSlotForEquipment.FrameImageForEquipment(type)}");
            BorderImage.texture = frameTexture;
            // Lấy EventTrigger của RawImage
            EventTrigger eventTrigger = BorderImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = BorderImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<MainMenuDetailsManager>().PopupDetails(equipment, MainPanel));
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
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.Rare}");
            rareImage.texture = rareTexture;
        }
        tempContent.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateEquipmentsShop(List<Equipments> equipments, string type)
    {
        foreach (var equipment in equipments)
        {
            GameObject equipmentObject = Instantiate(EquipmentShopPrefab, tempContent);

            TextMeshProUGUI Title = equipmentObject.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            Title.text = equipment.Name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(equipment.Image)}");
            Image.texture = texture;
            // RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            // Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.rare}");
            // rareImage.texture = rareTexture;

            RawImage currencyImage = equipmentObject.transform.Find("CurrencyImage").GetComponent<RawImage>();
            Texture currencyTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(equipment.CurrencyImage)}");
            currencyImage.texture = currencyTexture;

            TextMeshProUGUI currencyTitle = equipmentObject.transform.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
            currencyTitle.text = equipment.Price.ToString().Replace("_", " ");

            Button buy = equipmentObject.transform.Find("Buy").GetComponent<Button>();
            TextMeshProUGUI buttonText = buy.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUY);
            buy.onClick.AddListener(async () =>
            {
                await GetQuantityAsync(type, equipment);
            });
        }
        // GridLayoutGroup gridLayout = MainMenuContent.GetComponent<GridLayoutGroup>();
        // if (gridLayout != null)
        // {
        //     gridLayout.cellSize = new Vector2(110, 130);
        // }
        tempContent.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateEquipmentsEnhancement(List<Equipments> equipments)
    {
        foreach (var equipment in equipments)
        {
            GameObject equipmentObject = Instantiate(equipmentsPrefab, MainMenuEnhancementContent);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.Name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(equipment.Image)}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.Rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = MainMenuContent.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(110, 130);
        }
    }
    private void CreateEquipmentsCampaign(Campaigns campaigns)
    {
        float xOffset = -650; // Khoảng cách cố định giữa các equipmentObject trên trục X
        float yAmplitude = 100; // Biên độ dao động trên trục Y
        float yFrequency = Mathf.PI / 6; // Tần số dao động trên trục Y

        int index = 0; // Dùng để tính toán vị trí dựa trên chỉ s
        foreach (var campaignDetail in campaigns.CampaignDetails)
        {
            GameObject equipmentObject = Instantiate(campaignPrefab, tempContent);

            TextMeshProUGUI title = equipmentObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            title.text = campaignDetail.Name.Replace("_", " ");

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
                int starCount = campaignDetail.Stars; // Số lượng sao, ví dụ: 1, 2, ..., 5
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
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => PopupCampaignDetail(campaignDetail));
            index++; // Tăng chỉ số cho lần lặp tiếp theo
        }
        // GridLayoutGroup gridLayout = tempContent.GetComponent<GridLayoutGroup>();
        // if (gridLayout != null)
        // {
        //     gridLayout.cellSize = new Vector2(110, 130);
        // }
    }
    public async Task GetBagAsync(string type)
    {
        currentObject = Instantiate(MainMenuPanelPrefab, MainPanel);
        int totalRecord = 0;
        var userEquipmentsService = UserEquipmentsService.Create();
        List<Equipments> equipments = await userEquipmentsService.GetUserEquipmentsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
        tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuContentPanel");
        CreateEquipmentsBag(equipments, type);

        totalRecord = await userEquipmentsService.GetUserEquipmentsCountAsync(User.CurrentUserId, search, type, rare);
        totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);

        Transform DictionaryPanel = currentObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = LocalizationManager.Get("bag");
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

                offset = 0;
                currentPage = 1;
                Destroy(currentObject);
            });
            Button HomeButton = DictionaryPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
            });

            GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
            gridLayout.cellSize = new Vector2(200, 300);
        }

        Transform button = currentObject.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuContentPanel");
            TextMeshProUGUI PageText = button.transform.Find("Page").GetComponent<TextMeshProUGUI>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
                _=ChangeNextPageAsync(1, PageText, content, type);
            });
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
                _=ChangePreviousPageAsync(1, PageText, content, type);
            });
        }

    }
    public async Task GetShopAsync(string type)
    {
        currentObject = Instantiate(MainMenuShopPanelPrefab, MainPanel);
        int totalRecord = 0;
        var equipmentsService = EquipmentsService.Create();
        List<Equipments> equipments = await equipmentsService.GetEquipmentsWithCurrencyAsync(type, PAGE_SIZE, offset);
        tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        CreateEquipmentsShop(equipments, type);

        totalRecord = await equipmentsService.GetEquipmentsCountAsync(search, type, rare);
        totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);

        Transform DictionaryPanel = currentObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            TextMeshProUGUI Title = DictionaryPanel.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = LocalizationManager.Get(AppDisplayConstants.MainType.SHOP);
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

                offset = 0;
                currentPage = 1;
                Destroy(currentObject);
            });
            Button HomeButton = DictionaryPanel.transform.Find("HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
            });
        }

        Transform button = currentObject.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            TextMeshProUGUI PageText = button.transform.Find("Page").GetComponent<TextMeshProUGUI>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
                _=ChangeNextPageAsync(2, PageText, content, type);
            });
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
                _=ChangePreviousPageAsync(2, PageText, content, type);
            });
        }
        Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
        await FindObjectOfType<CurrenciesManager>().GetEquipmentsCurrencyAsync(type, CurrencyPanel);
    }
    public async Task GetEnhancementAsync(string type)
    {
        currentObject = Instantiate(MainMenuEnhancementPanelPrefab, MainPanel);
        int totalRecord = 0;
        var userEquipmentsService = UserEquipmentsService.Create();
        List<Equipments> equipments = await userEquipmentsService.GetUserEquipmentsAsync(User.CurrentUserId, search, type, PAGE_SIZE, offset, rare);
        tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/MainMenuEnhancementContentPanel");
        CreateEquipmentsEnhancement(equipments);

        totalRecord = await userEquipmentsService.GetUserEquipmentsCountAsync(User.CurrentUserId, search, type, rare);
        totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);

        Transform DictionaryPanel = currentObject.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = LocalizationManager.Get("enhancement");
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
            TextMeshProUGUI PageText = button.transform.Find("Page").GetComponent<TextMeshProUGUI>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() => _=ChangeNextPageAsync(3, PageText, content, type));
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() => _=ChangePreviousPageAsync(3, PageText, content, type));
        }
    }
    // public async Task GetCampaignAsync(string type)
    // {
    //     currentObject = Instantiate(MainMenuCampaignPanel, MainPanel);
    //     // int totalRecord = 0;
    //     UserCampaign userCampaign = new UserCampaign();
    //     Campaigns userCampaigns = userCampaign.GetCampaignsForUser("Chapter 1", type);
    //     tempContent = currentObject.transform.Find("DictionaryCards/CampaignGroup");
    //     CreateEquipmentsCampaign(userCampaigns);

    //     // totalRecord = equipmentsManager.GetUserEquipmentsCount(type);
    //     // totalPage = CalculateTotalPages(totalRecord, pageSize);

    //     Transform DictionaryPanel = currentObject.transform.Find("DictionaryCards");
    //     if (DictionaryPanel != null)
    //     {
    //         Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
    //         Title.text = LocalizationManager.Get("campaign");
    //         TextMeshProUGUI chapter = DictionaryPanel.transform.Find("CampaignTitleText").GetComponent<TextMeshProUGUI>();
    //         chapter.text = userCampaigns.Chapter.Replace("_", " ");
    //         Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuCampaignContentPanel");
    //         Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
    //         CloseButton.onClick.AddListener(() => Destroy(currentObject));
    //         Button HomeButton = DictionaryPanel.transform.Find("HomeButton").GetComponent<Button>();
    //         HomeButton.onClick.AddListener(() => Close(MainPanel));
    //     }

    //     // Transform button = MainMenuShopPanel.transform.Find("Pagination");
    //     // if (button != null)
    //     // {
    //     //     Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
    //     //     Text PageText = button.transform.Find("Page").GetComponent<Text>();
    //     //     PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
    //     //     Button NextButton = button.transform.Find("Next").GetComponent<Button>();
    //     //     NextButton.onClick.AddListener(() => ChangeNextPage(3, PageText, content,type));
    //     //     Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
    //     //     PreviousButton.onClick.AddListener(() => ChangePreviousPage(3, PageText, content,type));
    //     // }
    //     Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
    //     var currencyService = UserCurrencyService.Create();
    //     List<Currencies> currencies = new List<Currencies>();
    //     currencies = await currencyService.GetUserCurrencyAsync(User.CurrentUserId);
    //     FindObjectOfType<CurrenciesManager>().GetMainCurrency(currencies, CurrencyPanel);
    // }
    public async Task ChangeNextPageAsync(int status, TextMeshProUGUI PageText, Transform content, string subType)
    {
        if (currentPage < totalPage)
        {
            Close(content);
            int totalRecord = 0;

            if (status == 1)
            {
                var userEquipmentsService = UserEquipmentsService.Create();
                totalRecord = await userEquipmentsService.GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
                currentPage = currentPage + 1;
                offset = offset + PAGE_SIZE;
                List<Equipments> equipments = await userEquipmentsService.GetUserEquipmentsAsync(User.CurrentUserId, search, subType, PAGE_SIZE, offset, rare);
                CreateEquipmentsBag(equipments, subType);
            }
            else if (status == 2)
            {
                var equipmentsService = EquipmentsService.Create();
                totalRecord = await equipmentsService.GetEquipmentsCountAsync(search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
                currentPage = currentPage + 1;
                offset = offset + PAGE_SIZE;
                List<Equipments> equipments = await equipmentsService.GetEquipmentsWithCurrencyAsync(subType, PAGE_SIZE, offset);
                CreateEquipmentsShop(equipments, subType);
            }
            else if (status == 3)
            {
                var userEquipmentsService = UserEquipmentsService.Create();
                totalRecord = await userEquipmentsService.GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
                currentPage = currentPage + 1;
                offset = offset + PAGE_SIZE;
                List<Equipments> equipments = await userEquipmentsService.GetUserEquipmentsAsync(User.CurrentUserId, search, subType, PAGE_SIZE, offset, rare);
                CreateEquipmentsEnhancement(equipments);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public async Task ChangePreviousPageAsync(int status, TextMeshProUGUI PageText, Transform content, string subType)
    {
        if (currentPage > 1)
        {
            Close(content);
            int totalRecord = 0;

            if (status == 1)
            {
                var userEquipmentsService = UserEquipmentsService.Create();
                totalRecord = await userEquipmentsService.GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
                currentPage = currentPage - 1;
                offset = offset - PAGE_SIZE;
                List<Equipments> equipments = await userEquipmentsService.GetUserEquipmentsAsync(User.CurrentUserId, search, subType, PAGE_SIZE, offset, rare);
                CreateEquipmentsBag(equipments, subType);
            }
            else if (status == 2)
            {
                var equipmentsService = EquipmentsService.Create();
                totalRecord = await equipmentsService.GetEquipmentsCountAsync(search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
                currentPage = currentPage - 1;
                offset = offset - PAGE_SIZE;
                List<Equipments> equipments = await equipmentsService.GetEquipmentsWithCurrencyAsync(subType, PAGE_SIZE, offset);
                CreateEquipmentsShop(equipments, subType);
            }
            else if (status == 3)
            {
                var userEquipmentsService = UserEquipmentsService.Create();
                totalRecord = await userEquipmentsService.GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, PAGE_SIZE);
                currentPage = currentPage - 1;
                offset = offset - PAGE_SIZE;
                List<Equipments> equipments = await userEquipmentsService.GetUserEquipmentsAsync(User.CurrentUserId, search, subType, PAGE_SIZE, offset, rare);
                CreateEquipmentsShop(equipments, subType);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public async Task GetQuantityAsync(string type, Equipments equipments)
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
        var userCurrencyService = UserCurrenciesService.Create();
        Currencies currency = await userCurrencyService.GetUserEquipmentsPriceAsync(type, equipments.Id);
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(currency.Image);;
        Texture currencyTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        currencyImage.texture = currencyTexture;

        fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipments.Image);
        Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        equipmentImage.texture = equipmentTexture;

        currency = await userCurrencyService.GetEquipmentsPriceAsync(type, equipments.Id);
        priceText.text = currency.Quantity.ToString();
        double originPrice = currency.Quantity;
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
        maxButton.onClick.AddListener(async () =>
        {
            var userCurrencyService = UserCurrenciesService.Create();
            List<Currencies> userCurrency = await userCurrencyService.GetEquipmentsCurrencyAsync(type);
            Currencies equipmentPrice = await userCurrencyService.GetEquipmentsPriceAsync(type, equipments.Id);
            double price = double.Parse(priceText.text);
            foreach (var user in userCurrency)
            {
                if (user.Id == equipmentPrice.Id)
                {
                    int max = (int)(user.Quantity / equipmentPrice.Quantity);
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
        confirmButton.onClick.AddListener(async () =>
        {
            int quantity = int.Parse(quantityText.text);
            double totalCost = originPrice * quantity;

            List<Currencies> userCurrency = await userCurrencyService.GetEquipmentsCurrencyAsync(type);
            bool hasEnough = false;
            foreach (var uc in userCurrency)
            {
                if (uc.Id == currency.Id && uc.Quantity >= totalCost)
                {
                    hasEnough = true;
                    break;
                }
            }

            if (!hasEnough)
            {
                FindObjectOfType<NotificationManager>().ShowNotification("Not enough currency!");
                return;
            }

            bool success = await userEquipmentsService.BuyEquipmentAsync(equipments.Id, quantity);
            if (success)
            {
                await UserEquipmentsService.Create().UpdateUserCurrencyAsync(User.CurrentUserId, totalCost);
                await equipmentsGalleryService.InsertEquipmentGalleryAsync(equipments.Id);
                Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
                Close(CurrencyPanel);
                await FindObjectOfType<CurrenciesManager>().GetEquipmentsCurrencyAsync(type, CurrencyPanel);
                Close(popupPanel);
                // FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
                GameObject receivedNotificationObject = Instantiate(ReceivedNotificationPanelPrefab, popupPanel);

                AddCloseEvent(receivedNotificationObject);
                Transform itemContent = receivedNotificationObject.transform.Find("Scroll View/Viewport/Content");
                GameObject itemObject = Instantiate(ItemPopupPrefab, itemContent);

                RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipments.Image);
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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            Destroy(popupObject);
        });
        titleText.text = campaignDetail.Name;
        foreach (CampaignReward campaignReward in campaignDetail.CampaignRewards)
        {
            GameObject itemObject = Instantiate(ItemsPrefab, itemsGroup);
            Text itemTitleText = itemObject.transform.Find("ItemName").GetComponent<Text>();
            RawImage itemBackground = itemObject.transform.Find("ItemBackground").GetComponent<RawImage>();
            itemTitleText.gameObject.SetActive(false);
            // itemBackground.gameObject.SetActive(false);

            Texture backgroundTexture = Resources.Load<Texture>("UI/Material_473");
            itemBackground.texture = backgroundTexture;

            RawImage itemImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            Texture itemTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(campaignReward.Items.Image)}");
            itemImage.texture = itemTexture;

            // Chỉnh kích thước RectTransform thành 100x100
            RectTransform rectTransform = itemImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            RectTransform backgroundRectTransform = itemBackground.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            // Debug.Log(campaignReward.items.image);
        }
        foreach (CampaignDetailCard campaignDetailCardcard in campaignDetail.CampaignDetailCards)
        {
            GameObject cardObject = Instantiate(cardsPrefab, enemyGroup);
            RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(campaignDetailCardcard.Cards.Image)}");
            Image.texture = texture;
        }
    }
}
