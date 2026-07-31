using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesGalleryService
{
    Task<List<Badges>> GetBadgesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBadgesCountAsync(string search, string rare);
    Task<bool> InsertBadgeGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusBadgeGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusBadgesGalleryAsync(string userId);
    Task<bool> UpdateTempStarBadgeGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarBadgeGalleryAsync(string userId, string badgeId);
    Task<bool> UpdateBatchCurrentStarBadgesGalleryAsync(string userId);
    Task<bool> InsertBatchBadgesGalleryAsync(string userId, List<Badges> badges);
    Task<Badges> GetBadgeCollectionByIdAsync(string userId, string objectId);
    Task UpdateBadgeGalleryPowerAsync(string userId, string id);
    Task<Badges> SumPowerBadgesGalleryAsync(string userId);
}