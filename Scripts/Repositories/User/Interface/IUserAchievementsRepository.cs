using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAchievementsRepository
{
    Task<List<Achievements>> GetUserAchievementsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserArchievementsCountAsync(string user_id, string rare);
    Task<bool> InsertUserAchievementsAsync(Achievements achievements, string userId);
    Task<bool> UpdateAchievementLevelAsync(Achievements achievements, int cardLevel);
    Task<bool> UpdateAchievementBreakthroughAsync(Achievements achievements, int star, double quantity);
    Task<Achievements> GetUserAchievementByIdAsync(string user_id, string id);
    Task<Achievements> SumPowerUserAchievementsAsync();
}
