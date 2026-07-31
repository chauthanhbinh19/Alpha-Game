using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTalismansService
{
    Task<List<Talismans>> GetUserTalismansAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserTalismansCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismanAsync(string userId, Talismans talisman);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismansBatchAsync(string userId, List<Talismans> talismans);
    Task<bool> UpdateUserTalismanLevelAsync(string userId, Talismans talisman);
    Task<bool> UpdateUserTalismanStarAsync(string userId, Talismans talisman);
    Task<Talismans> GetUserTalismanByIdAsync(string userId, string Id);
    Task<Talismans> SumPowerUserTalismansAsync(string userId);

}