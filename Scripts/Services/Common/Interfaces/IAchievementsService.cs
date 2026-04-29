using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAchievementsService
{
    Task<List<Achievements>> GetAchievementsAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetAchievementsCountAsync(string search, string rare);
    Task<Achievements> GetAchievementByIdAsync(string id);
    Task<List<Achievements>> GetAchievementsWithPriceAsync(int pageSize, int offset);
    Task<int> GetAchievementsWithPriceCountAsync();
    Task<Achievements> SumPowerAchievementsPercentAsync();
}