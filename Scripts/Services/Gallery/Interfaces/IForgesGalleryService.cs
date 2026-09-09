using System.Collections.Generic;
using System.Threading.Tasks;

public interface IForgesGalleryService
{
    Task<List<Forges>> GetForgesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetForgesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertForgeGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusForgeGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusForgesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarForgeGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarForgeGalleryAsync(string userId, string forgeId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarForgesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchForgesGalleryAsync(string userId, List<Forges> forges);
    Task<Forges> GetForgeCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateForgeGalleryPowerAsync(string userId, string Id);
    Task<Forges> SumPowerForgesGalleryAsync(string userId);
}