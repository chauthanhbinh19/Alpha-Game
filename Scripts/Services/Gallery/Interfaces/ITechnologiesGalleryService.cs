using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITechnologiesGalleryService
{
    Task<List<Technologies>> GetTechnologiesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTechnologiesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertTechnologyGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusTechnologyGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTechnologiesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarTechnologyGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarTechnologyGalleryAsync(string userId, string technologyId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarTechnologiesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchTechnologiesGalleryAsync(string userId, List<Technologies> technologies);
    Task<Technologies> GetTechnologyCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateTechnologyGalleryPowerAsync(string userId, string id);
    Task<Technologies> SumPowerTechnologiesGalleryAsync(string userId);
}