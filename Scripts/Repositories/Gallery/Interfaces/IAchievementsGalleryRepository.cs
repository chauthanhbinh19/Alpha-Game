using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAchievementsGalleryRepository
{
    Task<List<Achievements>> GetAchievementsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAchievementsCountAsync(string search, string rare);
    Task InsertAchievementsGalleryAsync(string userId, string Id, Achievements AchievementFromDB);
    Task UpdateStatusAchievementsGalleryAsync(string userId, string Id);
    Task UpdateStarAchievementsGalleryAsync(string userId, string id, double star);
    Task UpdateAchievementsGalleryPowerAsync(string userId, string id, Achievements AchievementFromDB);
    Task<Achievements> SumPowerAchievementsGalleryAsync(string userId);
}