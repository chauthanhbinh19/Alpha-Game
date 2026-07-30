using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAchievementsGalleryRepository
{
    Task<List<Achievements>> GetAchievementsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAchievementsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Achievements>> InsertAchievementGalleryAsync(string userId, string Id, Achievements AchievementFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusAchievementGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAchievementsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarAchievementGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarAchievementGalleryAsync(string userId, string achievementId);
    Task<InsertOrUpdateResult<List<(string AchievementId, double CurrentStar)>>> UpdateBatchCurrentStarAchievementsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Achievements>>> InsertBatchAchievementsGalleryAsync(string userId, List<Achievements> achievements);
    Task<Achievements> GetAchievementCollectionByIdAsync(string userId, string objectId);
    Task UpdateAchievementGalleryPowerAsync(string userId, string id, Achievements AchievementFromDB);
    Task<Achievements> SumPowerAchievementsGalleryAsync(string userId);
}