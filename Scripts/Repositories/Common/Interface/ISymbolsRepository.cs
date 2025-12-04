using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISymbolsRepository
{
    Task<List<string>> GetUniqueSymbolsTypesAsync();
    Task<List<string>> GetUniqueSymbolsIdAsync();
    Task<List<Symbols>> GetSymbolsAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetSymbolsCountAsync(string type, string rare);
    Task<List<Symbols>> GetSymbolsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetSymbolsWithPriceCountAsync(string type);
    Task<Symbols> GetSymbolByIdAsync(string Id);
    Task<Symbols> SumPowerSymbolsPercentAsync();
}
