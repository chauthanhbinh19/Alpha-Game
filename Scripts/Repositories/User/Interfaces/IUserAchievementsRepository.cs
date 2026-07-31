using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAchievementsRepository
{
    Task<List<Achievements>> GetUserAchievementsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArchievementsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Achievements>> InsertOrUpdateUserAchievementAsync(string userId, Achievements achievement);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Achievements>>> InsertOrUpdateUserAchievementsBatchAsync(string userId, List<Achievements> achievements);
    Task<InsertOrUpdateResult<bool>> UpdateUserAchievementLevelAsync(string userId, Achievements achievement);
    Task<InsertOrUpdateResult<bool>> UpdateUserAchievementStarAsync(string userId, Achievements achievement);
    Task<Achievements> GetUserAchievementByIdAsync(string userId, string id);
    Task<Achievements> SumPowerUserAchievementsAsync(string userId);
}
