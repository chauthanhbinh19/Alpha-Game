using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesGalleryService
{
    Task<List<Badges>> GetBadgesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBadgesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertBadgeGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBadgeGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBadgesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarBadgeGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBadgeGalleryAsync(string userId, string badgeId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBadgesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchBadgesGalleryAsync(string userId, List<Badges> badges);
    Task<Badges> GetBadgeCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateBadgeGalleryPowerAsync(string userId, string id);
    Task<Badges> SumPowerBadgesGalleryAsync(string userId);
}