using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSymbolsService
{
    Task<List<Symbols>> GetUserSymbolsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSymbolsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSymbolAsync(string userId, Symbols symbol);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSymbolsBatchAsync(string userId, List<Symbols> symbols);
    Task<bool> UpdateUserSymbolLevelAsync(string userId, Symbols symbol);
    Task<bool> UpdateUserSymbolStarAsync(string userId, Symbols symbol);
    Task<Symbols> GetUserSymbolByIdAsync(string userId, string Id);
    Task<Symbols> SumPowerUserSymbolsAsync(string userId);
}