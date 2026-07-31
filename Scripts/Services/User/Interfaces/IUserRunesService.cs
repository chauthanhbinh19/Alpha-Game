using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRunesService
{
    Task<List<Runes>> GetUserRunesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserRunesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRuneAsync(string userId, Runes rune);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRunesBatchAsync(string userId, List<Runes> runes);
    Task<bool> UpdateUserRuneLevelAsync(string userId, Runes rune);
    Task<bool> UpdateUserRuneStarAsync(string userId, Runes rune);
    Task<Runes> GetUserRuneByIdAsync(string userId, string Id);
    Task<Runes> SumPowerUserRunesAsync(string userId);
}