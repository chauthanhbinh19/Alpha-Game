using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MysticMarketManager : MonoBehaviour
{
    public static MysticMarketManager Instance { get; private set; }
    private GameObject MysticMarketManagerPrefab;
    private GameObject MysticMarketButtonPrefab;
    private GameObject MysticMarketPrefab;
    private Transform ContentPanel;
    private Transform CurrentContent;
    private Transform CurrencyPanel;
    private Transform PopupPanel;
    private Button CloseButton;
    private Button HomeButton;
    private int Offset;
    private int CurrentPage;
    private int TotalPage;
    private const int PAGE_SIZE = 100;
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        Offset = 0;
        CurrentPage = 1;
        Items = new List<Items>();
        MysticMarketButtonPrefab = UIManager.Instance.Get("MysticMarketButtonPrefab");
        MysticMarketManagerPrefab = UIManager.Instance.Get("MysticMarketManagerPrefab");
        MysticMarketPrefab = UIManager.Instance.Get("MysticMarketPrefab");
        PopupPanel = UIManager.Instance.GetTransform("popupPanel");
    }
    public async Task CreateMysticMarketAsync(Transform panel)
    {
        ContentPanel = panel;
        GameObject mysticMarketManagerObject = Instantiate(MysticMarketManagerPrefab, ContentPanel);
        Transform transform = mysticMarketManagerObject.transform;
        Transform mysticMarketTransform = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(mysticMarketManagerObject);
        });
        HomeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener( () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(ContentPanel);
        });

        TitleText.text = LocalizationManager.Get(AppDisplayConstants.Market.MYSTIC_MARKET);

        var allCurrencies = await CurrenciesService.Create().GetCurrencyListAsync();
        var currencies = allCurrencies.Where(c => c.Name != "Diamond" && c.Name != "Gold" && c.Name != "Silver")
            .ToList();
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(MysticMarketButtonPrefab, mysticMarketTransform);
            Transform currencyTransform = currencyObject.transform;
            TextMeshProUGUI currencyText = currencyTransform.Find("NameText").GetComponent<TextMeshProUGUI>();
            currencyText.text = currency.Name.Replace("_", " ");

            RawImage currencyImage = currencyTransform.Find("MainImage").GetComponent<RawImage>();
            string currencyFileNameWithoutExtension = ImageHelper.RemoveImageExtension(currency.Image);
            Texture currencyTexture = TextureHelper.LoadTextureCached($"{currencyFileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Button currencyButton = currencyTransform.GetComponent<Button>();
            currencyButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreateMysticMarketItemUIAsync(currency);
            });
        }
    }
    public async Task CreateMysticMarketItemUIAsync(Currencies currency)
    {
        CurrentCurrency = currency;
        GameObject mysticMarketObject = Instantiate(MysticMarketPrefab, ContentPanel);
        Transform transform = mysticMarketObject.transform;
        CurrentContent = transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        // TabButtonPanel = mysticMarketObject.transform.Find("Scroll View/Viewport/Content");
        CurrencyPanel = transform.Find("DictionaryCards/Currency");
        PageText = transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = transform.Find("Pagination/Previous").GetComponent<Button>();
        TitleText = transform.Find("DictionaryCards/Title").GetComponent<Text>();
        // CloseButton = mysticMarketObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        // CloseButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Destroy(mysticMarketObject);
        // });
        // HomeButton = mysticMarketObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
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

        TitleText.text = LocalizationManager.Get(AppDisplayConstants.Market.MYSTIC_MARKET);

        var allItems = await ItemsService.Create().GetItemsAsync();
        Items = allItems.Where(item => item.Type.Equals(AppConstants.Market.MYSTIC_MATERIAL_ITEM, StringComparison.OrdinalIgnoreCase))
            .ToList();

        TotalPage = Mathf.CeilToInt((float)Items.Count / PAGE_SIZE);
        CurrentPage = 1;

        await LoadCurrentPageAsync(currency);
    }
    private async Task LoadCurrentPageAsync(Currencies currency)
    {
        ClearAllPrefabs();

        Offset = (CurrentPage - 1) * PAGE_SIZE;

        var pagedItems = Items.Skip(Offset).Take(PAGE_SIZE).ToList();

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