using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
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
        currencyPrefab = UIManager.Instance.GetGameObject("currencyPrefab");
    }
    public void createCurrency(List<Currencies> currencies, Transform CurrencyPanel)
    {
        ButtonEvent.Instance.Close(CurrencyPanel);
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(currencyPrefab, CurrencyPanel);

            TextMeshProUGUI Title = currencyObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            Title.text = currency.Quantity.ToString();
            Title.text = NumberFormatter.FormatNumber(currency.Quantity, false);

            RawImage Image = currencyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(currency.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
        }
        GridLayoutGroup gridLayout = CurrencyPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 100);
        }
    }
    public void createTicketsCurrency(List<Items> items, Transform itemPanel)
    {
        ButtonEvent.Instance.Close(itemPanel);
        foreach (var item in items)
        {
            GameObject currencyObject = Instantiate(currencyPrefab, itemPanel);

            TextMeshProUGUI Title = currencyObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            Title.text = NumberFormatter.FormatNumber(item.Quantity, false);

            RawImage Image = currencyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(item.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
        }
        GridLayoutGroup gridLayout = itemPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 100);
        }
    }
    public void GetEquipmentsCurrency(string type, Transform panel)
    {
        IUserCurrencyRepository userCurrencyRepository = new UserCurrencyRepository();
        UserCurrencyService userCurrencyService = new UserCurrencyService(userCurrencyRepository);
        List<Currencies> currencies = userCurrencyService.GetEquipmentsCurrency(type);
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
