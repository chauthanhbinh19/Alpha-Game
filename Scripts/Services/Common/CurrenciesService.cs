using System.Collections.Generic;
using System.Threading.Tasks;

public class CurrenciesService : ICurrenciesService
{
    private readonly ICurrenciesRepository _currencyRepository;

    public CurrenciesService(ICurrenciesRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public static CurrenciesService Create()
    {
        return new CurrenciesService(new CurrenciesRepository());
    }

    public async Task<List<Currencies>> GetCurrencyListAsync()
    {
        return await _currencyRepository.GetCurrencyListAsync();
    }
}
