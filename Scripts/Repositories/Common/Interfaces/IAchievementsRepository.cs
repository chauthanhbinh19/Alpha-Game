using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAchievementsRepository
{
    Task<List<Achievements>> GetAchievementsAsync(string search, string rare, int pageSize, int offset);
    Task<List<Achievements>> GetAchievementsWithoutLimitAsync();
    Task<int> GetAchievementsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Achievements>> InsertAchievementAsync(Achievements entity);
    Task<InsertOrUpdateResult<Achievements>> UpdateAchievementAsync(Achievements entity);
    Task<Achievements> GetAchievementByIdAsync(string id);
    Task<List<Achievements>> GetAchievementsWithPriceAsync(int pageSize, int offset);
    Task<int> GetAchievementsWithPriceCountAsync();
    Task<Achievements> SumPowerAchievementsPercentAsync(string userId);
}