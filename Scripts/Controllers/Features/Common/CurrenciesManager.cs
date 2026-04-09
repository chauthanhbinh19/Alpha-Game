using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Threading.Tasks;

public class CurrenciesManager : MonoBehaviour
{
    public static CurrenciesManager Instance { get; private set; }
    private GameObject currencyPrefab;
    // Start is called before the first frame update
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
        currencyPrefab = UIManager.Instance.Get("CurrencyPrefab");
    }
    public void createCurrency(List<Currencies> currencies, Transform CurrencyPanel)
    {
        ButtonEvent.Instance.Close(CurrencyPanel);
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(currencyPrefab, CurrencyPanel);

            TextMeshProUGUI titleText = currencyObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            titleText.text = currency.Quantity.ToString();
            titleText.text = NumberFormatter.FormatNumber(currency.Quantity, false);

            RawImage image = currencyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(currency.Image);
            Texture texutre = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texutre;
        }
        GridLayoutGroup gridLayout = CurrencyPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(180, 100);
        }
    }
    public void createTicketsCurrency(List<Items> items, Transform itemPanel)
    {
        ButtonEvent.Instance.Close(itemPanel);
        foreach (var item in items)
        {
            GameObject currencyObject = Instantiate(currencyPrefab, itemPanel);

            TextMeshProUGUI titleText = currencyObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            titleText.text = NumberFormatter.FormatNumber(item.Quantity, false);

            RawImage image = currencyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(item.Image);
            Texture texutre = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texutre;
        }
        GridLayoutGroup gridLayout = itemPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(180, 100);
        }
    }
    public async Task GetEquipmentsCurrencyAsync(string type, Transform panel)
    {
        IUserCurrenciesRepository userCurrencyRepository = new UserCurrenciesRepository();
        UserCurrenciesService userCurrencyService = new UserCurrenciesService(userCurrencyRepository);
        List<Currencies> currencies = await userCurrencyService.GetEquipmentsCurrencyAsync(type);
        createCurrency(currencies, panel);
    }
    public void GetMainCurrency(List<Currencies> currencies, Transform panel)
    {
        List<Currencies> filteredCurrencies = currencies
            .Where(c => c.Name == AppConstants.Currency.DIAMOND || c.Name == AppConstants.Currency.GOLD || c.Name == AppConstants.Currency.SILVER)
            .ToList();
        createCurrency(filteredCurrencies, panel);
    }
    public void GetTicketsCurrency(List<Items> items, Transform itemPanel)
    {
        createTicketsCurrency(items, itemPanel);
    }
}
