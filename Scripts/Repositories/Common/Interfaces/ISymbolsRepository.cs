using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISymbolsRepository
{
    Task<List<string>> GetUniqueSymbolsTypesAsync();
    Task<List<string>> GetUniqueSymbolsIdAsync();
    Task<List<Symbols>> GetSymbolsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Symbols>> GetSymbolsWithoutLimitAsync();
    Task<int> GetSymbolsCountAsync(string search, string type, string rare);
    Task<List<Symbols>> GetSymbolsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetSymbolsWithPriceCountAsync(string type);
    Task<Symbols> GetSymbolByIdAsync(string Id);
    Task<Symbols> SumPowerSymbolsPercentAsync();
}
