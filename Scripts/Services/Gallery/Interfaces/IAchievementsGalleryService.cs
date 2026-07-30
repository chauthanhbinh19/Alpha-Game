using System.Collections.Generic;
using System.Threading.Tasks;
public interface IAchievementsGalleryService
{
    Task<List<Achievements>> GetAchievementsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAchievementsCountAsync(string search, string rare);
    Task<bool> InsertAchievementGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusAchievementGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusAchievementsGalleryAsync(string userId);
    Task<bool> UpdateStarAchievementGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarAchievementGalleryAsync(string userId, string achievementId);
    Task<bool> UpdateBatchCurrentStarAchievementsGalleryAsync(string userId);
    Task<bool> InsertBatchAchievementsGalleryAsync(string userId, List<Achievements> achievements);
    Task<Achievements> GetAchievementCollectionByIdAsync(string userId, string objectId);
    Task UpdateAchievementGalleryPowerAsync(string userId, string id, Achievements AchievementFromDB);
    Task<Achievements> SumPowerAchievementsGalleryAsync(string userId);
}