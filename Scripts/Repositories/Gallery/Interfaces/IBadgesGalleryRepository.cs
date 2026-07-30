using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesGalleryRepository
{
    Task<List<Badges>> GetBadgesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBadgesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Badges>> InsertBadgeGalleryAsync(string userId, string Id, Badges BadgeFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBadgeGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBadgesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarBadgeGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarBadgeGalleryAsync(string userId, string badgeId);
    Task<InsertOrUpdateResult<List<(string BadgeId, double CurrentStar)>>> UpdateBatchCurrentStarBadgesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Badges>>> InsertBatchBadgesGalleryAsync(string userId, List<Badges> badges);
    Task<Badges> GetBadgeCollectionByIdAsync(string userId, string objectId);
    Task UpdateBadgeGalleryPowerAsync(string userId, string id, Badges BadgeFromDB);
    Task<Badges> SumPowerBadgesGalleryAsync(string userId);
}