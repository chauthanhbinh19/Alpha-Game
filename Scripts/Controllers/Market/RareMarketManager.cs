using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RareMarketManager : MonoBehaviour
{
    public static RareMarketManager Instance { get; private set; }
    private GameObject RareMarketManagerPrefab;
    private GameObject RareMarketButtonPrefab;
    private GameObject RareMarketPrefab;
    private Transform MainPanel;
    private Transform currentContent;
    private Transform currencyPanel;
    private Button CloseButton;
    private Button HomeButton;
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private Text PageText;
    private Button NextButton;
    private Button PreviousButton;
    private Text titleText;
    private List<Items> items;
    private Currency currentCurrency;

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
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        items = new List<Items>();
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        RareMarketButtonPrefab = UIManager.Instance.GetGameObject("RareMarketButtonPrefab");
        RareMarketManagerPrefab = UIManager.Instance.GetGameObject("RareMarketManagerPrefab");
        RareMarketPrefab = UIManager.Instance.GetGameObject("RareMarketPrefab");
    }
    public void CreateRareMarket()
    {
        GameObject RareMarketManagerObject = Instantiate(RareMarketManagerPrefab, MainPanel);
        Transform RareMarketTransform = RareMarketManagerObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        titleText = RareMarketManagerObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = RareMarketManagerObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(RareMarketManagerObject));
        HomeButton = RareMarketManagerObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));

        titleText.text = LocalizationManager.Get(AppConstants.RareMarket);

        var currencies = CurrencyService.Create().GetCurrencyList();
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(RareMarketButtonPrefab, RareMarketTransform);
            TextMeshProUGUI currencyText = currencyObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            currencyText.text = currency.name.Replace("_", " ");

            RawImage currencyImage = currencyObject.transform.Find("MainImage").GetComponent<RawImage>();
            string currencyFileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(currency.image);
            Texture currencyTexture = Resources.Load<Texture>($"{currencyFileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Button currencyButton = currencyObject.GetComponent<Button>();
            currencyButton.onClick.AddListener(() =>
            {
                CreateRareMarketItemUI(currency);
            });
        }
    }
    public void CreateRareMarketItemUI(Currency currency)
    {
        currentCurrency = currency;
        GameObject RareMarketObject = Instantiate(RareMarketPrefab, MainPanel);
        currentContent = RareMarketObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        // TabButtonPanel = RareMarketObject.transform.Find("Scroll View/Viewport/Content");
        currencyPanel = RareMarketObject.transform.Find("DictionaryCards/Currency");
        PageText = RareMarketObject.transform.Find("Pagination/Page").GetComponent<Text>();
        NextButton = RareMarketObject.transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = RareMarketObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = RareMarketObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = RareMarketObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(RareMarketObject));
        HomeButton = RareMarketObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        NextButton.onClick.AddListener(ChangeNextPage);
        PreviousButton.onClick.AddListener(ChangePreviousPage);

        titleText.text = LocalizationManager.Get(AppConstants.RareMarket);

        items = ItemsService.Create().GetItems()
            .Where(item => item.type.Equals(AppConstants.RareMaterialItem, StringComparison.OrdinalIgnoreCase))
            .ToList();

        totalPage = Mathf.CeilToInt((float)items.Count / pageSize);
        currentPage = 1;

        LoadCurrentPage(currency);
    }
    private void LoadCurrentPage(Currency currency)
    {
        ClearAllPrefabs();

        offset = (currentPage - 1) * pageSize;

        var pagedItems = items.Skip(offset).Take(pageSize).ToList();

        ItemsController.Instance.CreateItemsTrade(pagedItems, currency, currentContent, currencyPanel);

        PageText.text = $"{currentPage}/{totalPage}";
    }

    public void ChangeNextPage()
    {
        if (currentPage < totalPage)
        {
            currentPage++;
            LoadCurrentPage(currentCurrency);
        }
    }
    public void ChangePreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            LoadCurrentPage(currentCurrency);
        }
    }
    public void ClearAllPrefabs()
    {
        foreach (Transform child in currentContent)
        {
            Destroy(child.gameObject);
        }
    }
    public void Close(Transform content)
    {
        offset = 0;
        currentPage = 1;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
}
