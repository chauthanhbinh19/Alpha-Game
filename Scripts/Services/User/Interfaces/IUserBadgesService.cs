using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBadgesService
{
    Task<List<Badges>> GetUserBadgesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBadgesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserBadgeAsync(Badges badge, string userId);
    Task<bool> InsertOrUpdateUserBadgesBatchAsync(List<Badges> badges);
    Task<bool> UpdateBadgeLevelAsync(Badges badge, int level);
    Task<bool> UpdateBadgeBreakthroughAsync(Badges badge, int star, double quantity);
    Task<Badges> GetUserBadgeByIdAsync(string user_id, string Id);
    Task<Badges> SumPowerUserBadgesAsync();
}