using System.Collections.Generic;
using System.Threading.Tasks;
public interface IAchievementsGalleryService
{
    Task<List<Achievements>> GetAchievementsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAchievementsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertAchievementGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusAchievementGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAchievementsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarAchievementGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarAchievementGalleryAsync(string userId, string achievementId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarAchievementsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchAchievementsGalleryAsync(string userId, List<Achievements> achievements);
    Task<Achievements> GetAchievementCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateAchievementGalleryPowerAsync(string userId, string id, Achievements AchievementFromDB);
    Task<Achievements> SumPowerAchievementsGalleryAsync(string userId);
}