using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAchievementsRepository
{
    Task<List<Achievements>> GetUserAchievementsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArchievementsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserAchievementsAsync(Achievements achievement, string userId);
    Task<bool> UpdateAchievementLevelAsync(Achievements achievement, int achievementLevel);
    Task<bool> UpdateAchievementBreakthroughAsync(Achievements achievement, int star, double quantity);
    Task<Achievements> GetUserAchievementByIdAsync(string user_id, string id);
    Task<Achievements> SumPowerUserAchievementsAsync();
}
