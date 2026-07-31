using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSymbolsRepository
{
    Task<List<Symbols>> GetUserSymbolsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSymbolsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Symbols>> InsertOrUpdateUserSymbolAsync(string userId, Symbols symbol);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Symbols>>> InsertOrUpdateUserSymbolsBatchAsync(string userId, List<Symbols> symbols);
    Task<InsertOrUpdateResult<bool>> UpdateUserSymbolLevelAsync(string userId, Symbols symbol);
    Task<InsertOrUpdateResult<bool>> UpdateUserSymbolStarAsync(string userId, Symbols symbol);
    Task<Symbols> GetUserSymbolByIdAsync(string userId, string Id);
    Task<Symbols> SumPowerUserSymbolsAsync(string userId);
}