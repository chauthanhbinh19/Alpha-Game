using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAchievementsRepository
{
    Task<List<Achievements>> GetUserAchievementsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArchievementsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserAchievementsAsync(Achievements achievement, string userId);
    Task<bool> InsertOrUpdateUserAchievementsBatchAsync(string userId, List<Achievements> achievements);
    Task<bool> UpdateUserAchievementLevelAsync(string userId, Achievements achievement);
    Task<bool> UpdateUserAchievementStarAsync(string userId, Achievements achievement);
    Task<bool> UpdateUserAchievementBreakthroughAsync(string userId, Achievements achievement, int star, double quantity);
    Task<Achievements> GetUserAchievementByIdAsync(string userId, string id);
    Task<Achievements> SumPowerUserAchievementsAsync(string userId);
}
