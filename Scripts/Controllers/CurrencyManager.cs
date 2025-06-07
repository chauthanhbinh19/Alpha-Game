using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
        currencyPrefab = UIManager.Instance.GetGameObject("currencyPrefab");
    }

    public void createCurrency(List<Currency> currencies, Transform CurrencyPanel)
    {
        foreach (var currency in currencies)
        {
            GameObject currencyObject = Instantiate(currencyPrefab, CurrencyPanel);

            Text Title = currencyObject.transform.Find("Content").GetComponent<Text>();
            Title.text = currency.quantity.ToString();

            RawImage Image = currencyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = currency.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
        }
    }
    public void createTicketsCurrency(List<Items> items, Transform itemPanel)
    {
        foreach (var item in items)
        {
            GameObject currencyObject = Instantiate(currencyPrefab, itemPanel);

            Text Title = currencyObject.transform.Find("Content").GetComponent<Text>();
            Title.text = item.quantity.ToString();

            RawImage Image = currencyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = item.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
        }
    }
    public void GetEquipmentsCurrency(string type, Transform panel)
    {
        IUserCurrencyRepository userCurrencyRepository = new UserCurrencyRepository();
        UserCurrencyService userCurrencyService = new UserCurrencyService(userCurrencyRepository);
        List<Currency> currencies = userCurrencyService.GetEquipmentsCurrency(type);
        createCurrency(currencies, panel);
    }
    public void GetMainCurrency(List<Currency> currencies, Transform panel)
    {
        ButtonEvent.Instance.Close(panel);
        List<Currency> filteredCurrencies = currencies
            .Where(c => c.name == "Diamond" || c.name == "Gold" || c.name == "Silver")
            .ToList();
        createCurrency(filteredCurrencies, panel);
    }
    public void GetTicketsCurrency(List<Items> items, Transform itemPanel)
    {
        ButtonEvent.Instance.Close(itemPanel);
        createTicketsCurrency(items, itemPanel);
    }
}
