using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EquipmentManagement : MonoBehaviour
{
    public GameObject equipmentMenuPanel;
    public Transform MainContent;
    public GameObject ItemsPrefab;
    public GameObject EquipmentsPanelPrefab;
    public Transform content;
    public GameObject MainMenuPanel;
    public Transform MainMenuContent;
    public GameObject MainMenuShopPanel;
    public Transform MainMenuShopContent;
    public GameObject MainMenuEnhancementPanel;
    public Transform MainMenuEnhancementContent;
    public GameObject MainMenuCampaignPanel;
    public GameObject equipmentsPrefab;
    public GameObject equipmentsShopPrefab;
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private int count = 1;
    // Start is called before the first frame update
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        // Lấy tất cả các button con trong equipmentMenuPanel
        Button[] buttons = equipmentMenuPanel.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string buttonName = button.name; // Lưu lại giá trị cục bộ để tránh lỗi closure
            int localCount = count;
            button.onClick.AddListener(() => OnButtonClick(buttonName, localCount));
            count=count+1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnButtonClick(string type, int count)
    {
        equipmentsPrefab.SetActive(true);
        GameObject equipmentObject = Instantiate(EquipmentsPanelPrefab, MainContent);

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
        foreach (Transform child in MainContent)
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
            GameObject equipmentObject = Instantiate(equipmentsPrefab, MainMenuContent);

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
    private void createEquipmentsShop(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsShopPrefab, MainMenuShopContent);

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
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture currencyTexture = Resources.Load<Texture>($"Currency/Purple_IV_Crystal");
            currencyImage.texture = currencyTexture;

            Text currencyTitle = equipmentObject.transform.Find("CurrencyText").GetComponent<Text>();
            currencyTitle.text = equipment.price.ToString().Replace("_", " ");
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
    public void GetBag(string type)
    {
        MainMenuPanel.SetActive(true);
        int totalRecord = 0;
        Equipments equipmentsManager = new Equipments();
        List<Equipments> equipments = equipmentsManager.GetUserEquipments(type, pageSize, offset);
        createEquipmentsBag(equipments);

        totalRecord = equipmentsManager.GetUserEquipmentsCount(type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = MainMenuPanel.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Bag";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content, MainMenuPanel));
        }

        Transform button = MainMenuPanel.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
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
        MainMenuShopPanel.SetActive(true);
        int totalRecord = 0;
        Equipments equipmentsManager = new Equipments();
        List<Equipments> equipments = equipmentsManager.GetEquipmentsWithCurrency(type, pageSize, offset);
        createEquipmentsShop(equipments);

        totalRecord = equipmentsManager.GetEquipmentsCount(type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = MainMenuShopPanel.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Shop";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuShopContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content, MainMenuShopPanel));
        }

        Transform button = MainMenuShopPanel.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Text PageText = button.transform.Find("Page").GetComponent<Text>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() => ChangeNextPage(2, PageText, content, type));
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() => ChangePreviousPage(2, PageText, content, type));
        }
    }
    public void GetEnhancement(string type)
    {
        MainMenuEnhancementPanel.SetActive(true);
        int totalRecord = 0;
        Equipments equipmentsManager = new Equipments();
        List<Equipments> equipments = equipmentsManager.GetUserEquipments(type, pageSize, offset);
        createEquipmentsEnhancement(equipments);

        totalRecord = equipmentsManager.GetUserEquipmentsCount(type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        Transform DictionaryPanel = MainMenuShopPanel.transform.Find("DictionaryCards");
        if (DictionaryPanel != null)
        {
            Text Title = DictionaryPanel.transform.Find("Title").GetComponent<Text>();
            Title.text = "Enhancement";
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/MainMenuEnhancementContentPanel");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content, MainMenuEnhancementPanel));
        }

        Transform button = MainMenuShopPanel.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
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
        MainMenuCampaignPanel.SetActive(true);
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
                createEquipmentsShop(equipments);
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
                createEquipmentsShop(equipments);
            }
            else if (status == 3)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetUserEquipments(subType, pageSize, offset);
                createEquipmentsShop(equipments);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
}
