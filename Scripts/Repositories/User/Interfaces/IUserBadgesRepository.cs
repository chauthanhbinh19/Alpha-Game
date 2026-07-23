using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBadgesRepository
{
    Task<List<Badges>> GetUserBadgesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBadgesCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserBadgeAsync(Badges badge, string userId);
    Task<bool> InsertOrUpdateUserBadgesBatchAsync(string userId, List<Badges> badges);
    Task<bool> UpdateUserBadgeLevelAsync(string userId, Badges badge);
    Task<bool> UpdateUserBadgeStarAsync(string userId, Badges badge);
    Task<bool> UpdateUserBadgeBreakthroughAsync(string userId, Badges badge, int star, double quantity);
    Task<Badges> GetUserBadgeByIdAsync(string userId, string Id);
    Task<Badges> SumPowerUserBadgesAsync(string userId);
}