using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAchievementsRepository
{
    Task<List<Achievements>> GetAchievementsAsync(int pageSize, int offset, string rare);
    Task<int> GetAchievementsCountAsync(string rare);
    Task<Achievements> GetAchievementByIdAsync(string id);
    Task<List<Achievements>> GetAchievementsWithPriceAsync(int pageSize, int offset);
    Task<int> GetAchievementsWithPriceCountAsync();
    Task<Achievements> SumPowerAchievementsPercentAsync();
}