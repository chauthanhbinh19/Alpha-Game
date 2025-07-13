using System.Collections.Generic;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public static CurrencyService Create()
    {
        return new CurrencyService(new CurrencyRepository());
    }

    public List<Currency> GetCurrencyList()
    {
        return _currencyRepository.GetCurrencyList();
    }
}
