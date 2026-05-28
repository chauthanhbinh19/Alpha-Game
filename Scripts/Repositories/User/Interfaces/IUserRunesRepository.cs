using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRunesRepository
{
    Task<List<Runes>> GetUserRunesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserRunesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserRuneAsync(Runes rune, string userId);
    Task<bool> InsertOrUpdateUserRunesBatchAsync(List<Runes> runes);
    Task<bool> UpdateRuneLevelAsync(Runes rune, int RuneLevel);
    Task<bool> UpdateRuneBreakthroughAsync(Runes rune, int star, double quantity);
    Task<Runes> GetUserRuneByIdAsync(string user_id, string Id);
    Task<Runes> SumPowerUserRunesAsync();
}