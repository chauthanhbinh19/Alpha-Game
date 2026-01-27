using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    private Transform ContentPanel;
    private Transform currentContent;
    private Transform currencyPanel;
    private Transform popupPanel;
    private Button CloseButton;
    private Button HomeButton;
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private Text titleText;
    private List<Items> items;
    private Currencies currentCurrency;

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
    public void Initialize()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        items = new List<Items>();
        RareMarketButtonPrefab = UIManager.Instance.Get("RareMarketButtonPrefab");
        RareMarketManagerPrefab = UIManager.Instance.Get("RareMarketManagerPrefab");
        RareMarketPrefab = UIManager.Instance.Get("RareMarketPrefab");
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
    }
    public async Task CreateRareMarketAsync(Transform panel)
    {
        ContentPanel = panel;
        GameObject RareMarketManagerObject = Instantiate(RareMarketManagerPrefab, ContentPanel);
        Transform RareMarketTransform = RareMarketManagerObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        titleText = RareMarketManagerObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = RareMarketManagerObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(RareMarketManagerObject);
        });
        // HomeButton = RareMarketManagerObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        // HomeButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Close(ContentPanel);
        // });

        titleText.text = LocalizationManager.Get(AppDisplayConstants.Market.RARE_MARKET);

        var allCurrencies = await CurrenciesService.Create().GetCurrencyListAsync();
        var currencies = allCurrencies.Where(c => c.Name != "Diamond" && c.Name != "Gold" && c.Name != "Silver")
            .ToList();
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(RareMarketButtonPrefab, RareMarketTransform);
            TextMeshProUGUI currencyText = currencyObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            currencyText.text = currency.Name.Replace("_", " ");

            RawImage currencyImage = currencyObject.transform.Find("MainImage").GetComponent<RawImage>();
            string currencyFileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(currency.Image);
            Texture currencyTexture = Resources.Load<Texture>($"{currencyFileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Button currencyButton = currencyObject.GetComponent<Button>();
            currencyButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreateRareMarketItemUIAsync(currency);
            });
        }
    }
    public async Task CreateRareMarketItemUIAsync(Currencies currency)
    {
        currentCurrency = currency;
        GameObject RareMarketObject = Instantiate(RareMarketPrefab, ContentPanel);
        currentContent = RareMarketObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        // TabButtonPanel = RareMarketObject.transform.Find("Scroll View/Viewport/Content");
        currencyPanel = RareMarketObject.transform.Find("DictionaryCards/Currency");
        PageText = RareMarketObject.transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = RareMarketObject.transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = RareMarketObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = RareMarketObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        // CloseButton = RareMarketObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        // CloseButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Destroy(RareMarketObject);
        // });
        // HomeButton = RareMarketObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        // HomeButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Close(ContentPanel);
        // });
        NextButton.onClick.AddListener(async ()=>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            await ChangeNextPageAsync();
        });
        PreviousButton.onClick.AddListener(async ()=>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            await ChangePreviousPageAsync();
        });

        titleText.text = LocalizationManager.Get(AppDisplayConstants.Market.RARE_MARKET);

        var allItems = await ItemsService.Create().GetItemsAsync();
        items = allItems.Where(item => item.Type.Equals(AppConstants.Market.RARE_MATERIAL_ITEM, StringComparison.OrdinalIgnoreCase))
            .ToList();

        totalPage = Mathf.CeilToInt((float)items.Count / pageSize);
        currentPage = 1;

        await LoadCurrentPageAsync(currency);
    }
    private async Task LoadCurrentPageAsync(Currencies currency)
    {
        ClearAllPrefabs();

        offset = (currentPage - 1) * pageSize;

        var pagedItems = items.Skip(offset).Take(pageSize).ToList();

        await ItemsController.Instance.CreateItemsTradeAsync(pagedItems, currency, currentContent, currencyPanel, popupPanel);

        PageText.text = $"{currentPage}/{totalPage}";
    }

    public async Task ChangeNextPageAsync()
    {
        if (currentPage < totalPage)
        {
            currentPage++;
            await LoadCurrentPageAsync(currentCurrency);
        }
    }
    public async Task ChangePreviousPageAsync()
    {
        if (currentPage > 1)
        {
            currentPage--;
            await LoadCurrentPageAsync(currentCurrency);
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
