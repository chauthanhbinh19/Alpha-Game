using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CurrencyManager : MonoBehaviour
{
    private GameObject currencyPrefab;
    // Start is called before the first frame update
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
    public void GetEquipmentsCurrency(string type,Transform panel){
        Currency currency = new Currency();
        List<Currency> currencies = currency.GetEquipmentsCurrency(type);
        createCurrency(currencies,panel);
    }
    public void GetMainCurrency(List<Currency>currencies,Transform panel){
        List<Currency> filteredCurrencies = currencies
            .Where(c => c.name == "Diamond" || c.name == "Gold" || c.name == "Silver")
            .ToList();
        createCurrency(filteredCurrencies,panel);
    }
}
