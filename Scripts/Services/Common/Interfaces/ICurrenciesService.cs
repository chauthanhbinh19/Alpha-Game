using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICurrenciesService
{
    Task<List<Currencies>> GetCurrencyListAsync();
}
    
