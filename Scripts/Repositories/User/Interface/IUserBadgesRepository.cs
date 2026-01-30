using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBadgesRepository
{
    Task<List<Badges>> GetUserBadgesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBadgesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserBadgeAsync(Badges Badges, string userId);
    Task<bool> UpdateBadgeLevelAsync(Badges Badges, int cardLevel);
    Task<bool> UpdateBadgeBreakthroughAsync(Badges Badges, int star, double quantity);
    Task<Badges> GetUserBadgeByIdAsync(string user_id, string Id);
    Task<Badges> SumPowerUserBadgesAsync();
}