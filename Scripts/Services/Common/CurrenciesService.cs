using System.Collections.Generic;
using System.Threading.Tasks;

public class CurrenciesService : ICurrenciesService
{
    private static CurrenciesService _instance;
    private readonly ICurrenciesRepository _currenciesRepository;

    public CurrenciesService(ICurrenciesRepository currenciesRepository)
    {
        _currenciesRepository = currenciesRepository;
    }

    public static CurrenciesService Create()
    {
        if (_instance == null)
        {
            _instance = new CurrenciesService(new CurrenciesRepository());
        }
        return _instance;
    }

    public async Task<List<Currencies>> GetCurrencyListAsync()
    {
        return await _currenciesRepository.GetCurrencyListAsync();
    }
}
