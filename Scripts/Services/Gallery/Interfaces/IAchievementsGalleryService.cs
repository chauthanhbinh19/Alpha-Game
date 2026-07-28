using System.Collections.Generic;
using System.Threading.Tasks;
public interface IAchievementsGalleryService
{
    Task<List<Achievements>> GetAchievementsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAchievementsCountAsync(string search, string rare);
    Task InsertAchievementGalleryAsync(string userId, string Id);
    Task UpdateStatusAchievementGalleryAsync(string userId, string Id);
    Task UpdateStarAchievementGalleryAsync(string userId, string id, double star);
    Task UpdateAchievementGalleryPowerAsync(string userId, string id, Achievements AchievementFromDB);
    Task<Achievements> SumPowerAchievementsGalleryAsync(string userId);
}