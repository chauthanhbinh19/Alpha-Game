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
    private Transform CurrentContent;
    private Transform CurrencyPanel;
    private Transform PopupPanel;
    private Button CloseButton;
    private Button HomeButton;
    private int Offset;
    private int CurrentPage;
    private int TotalPage;
    private const int PAGE_SIZe = 100;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private Text TitleText;
    private List<Items> Items;
    private Currencies CurrentCurrency;

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
        Offset = 0;
        CurrentPage = 1;
        Items = new List<Items>();
        RareMarketButtonPrefab = UIManager.Instance.Get("RareMarketButtonPrefab");
        RareMarketManagerPrefab = UIManager.Instance.Get("RareMarketManagerPrefab");
        RareMarketPrefab = UIManager.Instance.Get("RareMarketPrefab");
        PopupPanel = UIManager.Instance.GetTransform("popupPanel");
    }
    public async Task CreateRareMarketAsync(Transform panel)
    {
        ContentPanel = panel;
        GameObject rareMarketManagerObject = Instantiate(RareMarketManagerPrefab, ContentPanel);
        Transform transform = rareMarketManagerObject.transform;
        Transform rareMarketTransform = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(rareMarketManagerObject);
        });
        HomeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener( () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(ContentPanel);
        });

        TitleText.text = LocalizationManager.Get(AppDisplayConstants.Market.RARE_MARKET);

        var allCurrencies = await CurrenciesService.Create().GetCurrencyListAsync();
        var currencies = allCurrencies.Where(c => c.Name != "Diamond" && c.Name != "Gold" && c.Name != "Silver")
            .ToList();
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(RareMarketButtonPrefab, rareMarketTransform);
            Transform currencyTransform = currencyObject.transform;
            TextMeshProUGUI currencyText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            currencyText.text = currency.Name.Replace("_", " ");

            RawImage currencyImage = transform.Find("MainImage").GetComponent<RawImage>();
            string currencyFileNameWithoutExtension = ImageHelper.RemoveImageExtension(currency.Image);
            Texture currencyTexture = TextureHelper.LoadTextureCached($"{currencyFileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Button currencyButton = currencyTransform.GetComponent<Button>();
            currencyButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreateRareMarketItemUIAsync(currency);
            });
        }
    }
    public async Task CreateRareMarketItemUIAsync(Currencies currency)
    {
        CurrentCurrency = currency;
        GameObject rareMarketObject = Instantiate(RareMarketPrefab, ContentPanel);
        Transform transform = rareMarketObject.transform;
        CurrentContent = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        // TabButtonPanel = RareMarketObject.transform.Find("Scroll View/Viewport/Content");
        CurrencyPanel = transform.Find("DictionaryCards/Currency");
        PageText = transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = transform.Find("Pagination/Previous").GetComponent<Button>();
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
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

        TitleText.text = LocalizationManager.Get(AppDisplayConstants.Market.RARE_MARKET);

        var allItems = await ItemsService.Create().GetItemsAsync();
        Items = allItems.Where(item => item.Type.Equals(AppConstants.Market.RARE_MATERIAL_ITEM, StringComparison.OrdinalIgnoreCase))
            .ToList();

        TotalPage = Mathf.CeilToInt((float)Items.Count / PAGE_SIZe);
        CurrentPage = 1;

        await LoadCurrentPageAsync(currency);
    }
    private async Task LoadCurrentPageAsync(Currencies currency)
    {
        ClearAllPrefabs();

        Offset = (CurrentPage - 1) * PAGE_SIZe;

        var pagedItems = Items.Skip(Offset).Take(PAGE_SIZe).ToList();

        await ItemsController.Instance.CreateItemsTradeAsync(pagedItems, currency, CurrentContent, CurrencyPanel, PopupPanel);

        PageText.text = $"{CurrentPage}/{TotalPage}";
    }
    public async Task ChangeNextPageAsync()
    {
        if (CurrentPage < TotalPage)
        {
            CurrentPage++;
            await LoadCurrentPageAsync(CurrentCurrency);
        }
    }
    public async Task ChangePreviousPageAsync()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            await LoadCurrentPageAsync(CurrentCurrency);
        }
    }
    public void ClearAllPrefabs()
    {
        foreach (Transform child in CurrentContent)
        {
            Destroy(child.gameObject);
        }
    }
    public void Close(Transform content)
    {
        Offset = 0;
        CurrentPage = 1;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
