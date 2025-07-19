using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private static readonly List<string> FeatureList = new List<string>{
        AppConstants.Equipments, AppConstants.Realm, AppConstants.Upgrade, AppConstants.Aptitude,
        AppConstants.Affinity, AppConstants.Blessing, AppConstants.Core, AppConstants.Physique,
        AppConstants.Bloodline, AppConstants.Omnivision, AppConstants.Omnipotence, AppConstants.Omnipresence,
        AppConstants.Omniscience, AppConstants.Omnivory, AppConstants.Angel, AppConstants.Demon,
        AppConstants.Sword, AppConstants.Spear, AppConstants.Shield, AppConstants.Bow,
        AppConstants.Gun, AppConstants.Cyber, AppConstants.Fairy, AppConstants.Dark,
        AppConstants.Light, AppConstants.Fire, AppConstants.Ice, AppConstants.Earth,
        AppConstants.Thunder, AppConstants.Life, AppConstants.Space, AppConstants.Time,
        AppConstants.Nanotech, AppConstants.Quantum, AppConstants.Holography, AppConstants.Plasma,
        AppConstants.Biomech, AppConstants.Cryotech, AppConstants.Psionics, AppConstants.Neurotech,
        AppConstants.Antimatter, AppConstants.Phantomware, AppConstants.Gravitech, AppConstants.Aethernet,
        AppConstants.Starforge, AppConstants.Orbitalis, AppConstants.Azathoth, AppConstants.YogSothoth,
        AppConstants.Nyarlathotep, AppConstants.ShubNiggurath, AppConstants.Nihorath, AppConstants.Aeonax,
        AppConstants.Seraphiros, AppConstants.Thorindar, AppConstants.Zilthros, AppConstants.Khorazal,
        AppConstants.Ixithra, AppConstants.Omnitheus, AppConstants.Phyrixa, AppConstants.Atherion,
        AppConstants.Vorathos, AppConstants.Tenebris, AppConstants.Xylkor, AppConstants.Veltharion,
        AppConstants.Arcanos, AppConstants.Dolomath, AppConstants.Arathor, AppConstants.Xyphos,
        AppConstants.Vaelith, AppConstants.Zarx, AppConstants.Raik, AppConstants.Drax,
        AppConstants.Kron, AppConstants.Zolt, AppConstants.Gorr, AppConstants.Ryze,
        AppConstants.Jaxx, AppConstants.Thar, AppConstants.Vorn, AppConstants.Nyx,
        AppConstants.Aros, AppConstants.Hex, AppConstants.Lorn, AppConstants.Baxx,
        AppConstants.Zeph, AppConstants.Kael, AppConstants.Drav, AppConstants.Torn,
        AppConstants.Myrr, AppConstants.Vask, AppConstants.Jorr, AppConstants.Quen
    };
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        items = new List<Items>();
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MysticMarketButtonPrefab = UIManager.Instance.GetGameObject("MysticMarketButtonPrefab");
        MysticMarketManagerPrefab = UIManager.Instance.GetGameObject("MysticMarketManagerPrefab");
        MysticMarketPrefab = UIManager.Instance.GetGameObject("MysticMarketPrefab");
    }
    public void CreateMysticMarket()
    {
        GameObject mysticMarketManagerObject = Instantiate(MysticMarketManagerPrefab, MainPanel);
        Transform mysticMarketTransform = mysticMarketManagerObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        titleText = mysticMarketManagerObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = mysticMarketManagerObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(mysticMarketManagerObject));
        HomeButton = mysticMarketManagerObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));

        titleText.text = LocalizationManager.Get(AppConstants.MysticMarket);

        var currencies = CurrencyService.Create().GetCurrencyList();
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(MysticMarketButtonPrefab, mysticMarketTransform);
            TextMeshProUGUI currencyText = currencyObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            currencyText.text = currency.name.Replace("_", " ");

            RawImage currencyImage = currencyObject.transform.Find("MainImage").GetComponent<RawImage>();
            string currencyFileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(currency.image);
            Texture currencyTexture = Resources.Load<Texture>($"{currencyFileNameWithoutExtension}");
            currencyImage.texture = currencyTexture;

            Button currencyButton = currencyObject.GetComponent<Button>();
            currencyButton.onClick.AddListener(() =>
            {
                CreateMysticMarketItemUI(currency);
            });
        }
    }
    public void CreateMysticMarketItemUI(Currency currency)
    {
        currentCurrency = currency;
        GameObject mysticMarketObject = Instantiate(MysticMarketPrefab, MainPanel);
        currentContent = mysticMarketObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        // TabButtonPanel = mysticMarketObject.transform.Find("Scroll View/Viewport/Content");
        currencyPanel = mysticMarketObject.transform.Find("DictionaryCards/Currency");
        PageText = mysticMarketObject.transform.Find("Pagination/Page").GetComponent<Text>();
        NextButton = mysticMarketObject.transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = mysticMarketObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = mysticMarketObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = mysticMarketObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(mysticMarketObject));
        HomeButton = mysticMarketObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        NextButton.onClick.AddListener(ChangeNextPage);
        PreviousButton.onClick.AddListener(ChangePreviousPage);

        titleText.text = LocalizationManager.Get(AppConstants.MysticMarket);

        items = ItemsService.Create().GetItems()
            .Where(item => item.type.Equals(AppConstants.PackageItem, StringComparison.OrdinalIgnoreCase))
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