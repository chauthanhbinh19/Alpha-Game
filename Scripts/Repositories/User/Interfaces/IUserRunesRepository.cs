using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRunesRepository
{
    Task<List<Runes>> GetUserRunesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserRunesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Runes>> InsertOrUpdateUserRuneAsync(string userId, Runes rune);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Runes>>> InsertOrUpdateUserRunesBatchAsync(string userId, List<Runes> runes);
    Task<InsertOrUpdateResult<bool>> UpdateUserRuneLevelAsync(string userId, Runes rune);
    Task<InsertOrUpdateResult<bool>> UpdateUserRuneStarAsync(string userId, Runes rune);
    Task<Runes> GetUserRuneByIdAsync(string userId, string Id);
    Task<Runes> SumPowerUserRunesAsync(string userId);
}