using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSymbolsService
{
    Task<List<Symbols>> GetUserSymbolsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSymbolsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserSymbolAsync(Symbols symbol, string userId);
    Task<bool> InsertOrUpdateUserSymbolsBatchAsync(List<Symbols> symbols);
    Task<bool> UpdateSymbolLevelAsync(Symbols symbol, int level);
    Task<bool> UpdateSymbolBreakthroughAsync(Symbols symbol, int star, double quantity);
    Task<Symbols> GetUserSymbolByIdAsync(string user_id, string Id);
    Task<Symbols> SumPowerUserSymbolsAsync();
}