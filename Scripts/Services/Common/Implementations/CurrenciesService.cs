using System.Collections.Generic;
using System.Threading.Tasks;

public class CurrenciesService : ICurrenciesService
{
    private readonly ICurrenciesRepository _currenciesRepository;

    public CurrenciesService(ICurrenciesRepository currenciesRepository)
    {
        _currenciesRepository = currenciesRepository;
    }

    public static ICurrenciesService Create() => ServiceContainer.GetService<ICurrenciesService>();

    public async Task<List<Currencies>> GetCurrencyListAsync()
    {
        return await _currenciesRepository.GetCurrencyListAsync();
    }
}
