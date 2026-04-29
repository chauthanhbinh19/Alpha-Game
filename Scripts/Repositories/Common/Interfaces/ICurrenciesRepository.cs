using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICurrenciesRepository
{
    Task<List<Currencies>> GetCurrencyListAsync();
}
