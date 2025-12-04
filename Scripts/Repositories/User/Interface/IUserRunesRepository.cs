using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRunesRepository
{
    Task<List<Runes>> GetUserRunesAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserRunesCountAsync(string user_id, string rare);
    Task<bool> InsertUserRuneAsync(Runes Runes, string userId);
    Task<bool> UpdateRuneLevelAsync(Runes Runes, int RuneLevel);
    Task<bool> UpdateRuneBreakthroughAsync(Runes Runes, int star, double quantity);
    Task<Runes> GetUserRuneByIdAsync(string user_id, string Id);
    Task<Runes> SumPowerUserRunesAsync();
}