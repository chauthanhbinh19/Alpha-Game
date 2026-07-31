using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBadgesService
{
    Task<List<Badges>> GetUserBadgesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBadgesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgeAsync(string userId, Badges badge);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgesBatchAsync(string userId, List<Badges> badges);
    Task<bool> UpdateUserBadgeLevelAsync(string userId, Badges badge);
    Task<bool> UpdateUserBadgeStarAsync(string userId, Badges badge);
    Task<Badges> GetUserBadgeByIdAsync(string userId, string Id);
    Task<Badges> SumPowerUserBadgesAsync(string userId);
}