using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBadgesRepository
{
    Task<List<Badges>> GetUserBadgesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBadgesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Badges>> InsertOrUpdateUserBadgeAsync(string userId, Badges badge);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Badges>>> InsertOrUpdateUserBadgesBatchAsync(string userId, List<Badges> badges);
    Task<InsertOrUpdateResult<bool>> UpdateUserBadgeLevelAsync(string userId, Badges badge);
    Task<InsertOrUpdateResult<bool>> UpdateUserBadgeStarAsync(string userId, Badges badge);
    Task<Badges> GetUserBadgeByIdAsync(string userId, string Id);
    Task<Badges> SumPowerUserBadgesAsync(string userId);
}