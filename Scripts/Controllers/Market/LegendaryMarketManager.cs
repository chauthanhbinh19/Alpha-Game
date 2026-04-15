using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LegendaryMarketManager : MonoBehaviour
{
    public static LegendaryMarketManager Instance { get; private set; }
    private GameObject LegendaryMarketManagerPrefab;
    private GameObject LegendaryMarketButtonPrefab;
    private GameObject LegendaryMarketPrefab;
    private Transform ContentPanel;
    private Transform currentContent;
    private Transform currencyPanel;
    private Transform popupPanel;
    private Button closeButton;
    private Button homeButton;
    private int offset;
    private int currentPage;
    private int totalPage;
    private const int PAGE_SIZE = 100;
    private TextMeshProUGUI PageText;
    private Button nextButton;
    private Button previousButton;
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
        items = new List<Items>();
        LegendaryMarketButtonPrefab = UIManager.Instance.Get("LegendaryMarketButtonPrefab");
        LegendaryMarketManagerPrefab = UIManager.Instance.Get("LegendaryMarketManagerPrefab");
        LegendaryMarketPrefab = UIManager.Instance.Get("LegendaryMarketPrefab");
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
    }
    public async Task CreateLegendaryMarketAsync(Transform panel)
    {
        ContentPanel = panel;
        GameObject legendaryMarketManagerObject = Instantiate(LegendaryMarketManagerPrefab, ContentPanel);
        Transform transform = legendaryMarketManagerObject.transform;
        Transform legendaryMarketTransform = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        titleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
        closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(legendaryMarketManagerObject);
        });
        homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(ContentPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });

        titleText.text = LocalizationManager.Get(AppDisplayConstants.Market.LEGENDARY_MARKET);

        var allCurrencies = await CurrenciesService.Create().GetCurrencyListAsync();
        var currencies = allCurrencies.Where(c => c.Name != "Diamond" && c.Name != "Gold" && c.Name != "Silver")
            .ToList();
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(LegendaryMarketButtonPrefab, legendaryMarketTransform);
            TextMeshProUGUI currencyText = currencyObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            currencyText.text = currency.Name.Replace("_", " ");

            RawImage currencyImage = currencyObject.transform.Find("MainImage").GetComponent<RawImage>();
            string currencyFileNameWithoutExtension = ImageHelper.RemoveImageExtension(currency.Image);
            Texture currencyTexture = TextureHelper.LoadTextureCached($"{currencyFileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Button currencyButton = currencyObject.GetComponent<Button>();
            currencyButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreateLegendaryMarketItemUIAsync(currency);
            }));
        }
    }
    public async Task CreateLegendaryMarketItemUIAsync(Currencies currency)
    {
        currentCurrency = currency;
        GameObject legendaryMarketObject = Instantiate(LegendaryMarketPrefab, ContentPanel);
        Transform transform = legendaryMarketObject.transform;
        currentContent = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        // TabButtonPanel = LegendaryMarketObject.transform.Find("Scroll View/Viewport/Content");
        currencyPanel = transform.Find("DictionaryCards/Currency");
        PageText = transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        nextButton = transform.Find("Pagination/Next").GetComponent<Button>();
        previousButton = transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
        // CloseButton = LegendaryMarketObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        // CloseButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Destroy(LegendaryMarketObject);
        // });
        // HomeButton = LegendaryMarketObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        // HomeButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Close(ContentPanel);
        // });
        nextButton.onClick.AddListener(async ()=>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            await ChangeNextPageAsync();
        });
        previousButton.onClick.AddListener(async ()=>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            await ChangePreviousPageAsync();
        });

        titleText.text = LocalizationManager.Get(AppDisplayConstants.Market.LEGENDARY_MARKET);

        var allItems = await ItemsService.Create().GetItemsAsync();
        items = allItems.Where(item => item.Type.Equals(AppConstants.Market.LEGENDARY_MATERIAL_ITEM, StringComparison.OrdinalIgnoreCase))
            .ToList();

        totalPage = Mathf.CeilToInt((float)items.Count / PAGE_SIZE);
        currentPage = 1;

        await LoadCurrentPageAsync(currency);
    }
    private async Task LoadCurrentPageAsync(Currencies currency)
    {
        ClearAllPrefabs();

        offset = (currentPage - 1) * PAGE_SIZE;

        var pagedItems = items.Skip(offset).Take(PAGE_SIZE).ToList();

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
