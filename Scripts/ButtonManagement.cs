using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class ButtonManagement : MonoBehaviour
{
    public Button GalleryButton;
    public Button CollectionButton;
    public Button EquipmentsButton;
    public GameObject GalleryPanel;
    public GameObject CollectionPanel;
    public GameObject EquipmentsPanel;
    public GameObject EquipmentsContentPanel;
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
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        GalleryButton.onClick.AddListener(OnGalleryButtonClick);
        CollectionButton.onClick.AddListener(OnCollectionButtonClick);
        EquipmentsButton.onClick.AddListener(onEquipmentsButtonClick);

        // CloseMainMenuButton.onClick.AddListener(ClosePanel);
        // NextButton.onClick.AddListener(ChangeNextPage);
        // PreviousButton.onClick.AddListener(ChangePreviousPage);
    }

    void Update()
    {

    }
    public void OnGalleryButtonClick()
    {
        GalleryPanel.SetActive(true);
        CollectionPanel.SetActive(false);
        EquipmentsPanel.SetActive(false);
        Close(content);
    }
    public void OnCollectionButtonClick()
    {
        GalleryPanel.SetActive(false);
        CollectionPanel.SetActive(true);
        EquipmentsPanel.SetActive(false);
        Close(content);
    }
    public List<string> GetUniqueTypes()
    {
        // if (mainType.Equals("Equipments"))
        // {
        //     return Equipments.GetUniqueEquipmentsTypes();
        // }
        return Equipments.GetUniqueEquipmentsTypes();
    }
    public void onEquipmentsButtonClick()
    {
        Close(content);
        EquipmentsPanel.SetActive(true);
        GalleryPanel.SetActive(false);
        CollectionPanel.SetActive(false);
        List<string> uniqueTypes = GetUniqueTypes();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(ItemsPrefab, content);

                Text Title = button.transform.Find("ItemName").GetComponent<Text>();
                Title.text = subtype.Replace("_", " ");

                RawImage buttonImage = button.transform.Find("ItemImage").GetComponent<RawImage>();
                string fileNameWithoutExtension = subtype.Replace(".png", "");
                string image = fileNameWithoutExtension;
                Texture texture = Resources.Load<Texture>($"UI/Button/{image}");
                buttonImage.texture = texture;

                Button btn = button.GetComponent<Button>();
                int localCount = count;
                btn.onClick.AddListener(() => OnButtonClick(subtype, localCount));
                count++;
            }
        }
    }
    public void OnButtonClick(string type, int count)
    {
        EquipmentsContentPanel.SetActive(true);
        GameObject equipmentObject = Instantiate(EquipmentsPanelPrefab, MainContent);

        Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
        Title.text = type.Replace("_", " ");

        RawImage Image = equipmentObject.transform.Find("Background").GetComponent<RawImage>();
        string image = "Background_" + count;
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
        EquipmentsContentPanel.SetActive(false);
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
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content,MainMenuPanel));
        }

        Transform button = MainMenuPanel.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Text PageText = button.transform.Find("Page").GetComponent<Text>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() => ChangeNextPage(1, PageText, content,type));
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() => ChangePreviousPage(1, PageText, content,type));
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
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content,MainMenuShopPanel));
        }

        Transform button = MainMenuShopPanel.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Text PageText = button.transform.Find("Page").GetComponent<Text>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() => ChangeNextPage(2, PageText, content,type));
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() => ChangePreviousPage(2, PageText, content,type));
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
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content,MainMenuEnhancementPanel));
        }

        Transform button = MainMenuShopPanel.transform.Find("Pagination");
        if (button != null)
        {
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Text PageText = button.transform.Find("Page").GetComponent<Text>();
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
            Button NextButton = button.transform.Find("Next").GetComponent<Button>();
            NextButton.onClick.AddListener(() => ChangeNextPage(3, PageText, content,type));
            Button PreviousButton = button.transform.Find("Previous").GetComponent<Button>();
            PreviousButton.onClick.AddListener(() => ChangePreviousPage(3, PageText, content,type));
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
            Transform content = DictionaryPanel.Find("Scroll View/Viewport/Content");
            Button CloseButton = DictionaryPanel.transform.Find("CloseButton").GetComponent<Button>();
            CloseButton.onClick.AddListener(() => ClosePanel(content,MainMenuEnhancementPanel));
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
    public void ChangePreviousPage(int status, Text PageText, Transform content,string subType)
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
