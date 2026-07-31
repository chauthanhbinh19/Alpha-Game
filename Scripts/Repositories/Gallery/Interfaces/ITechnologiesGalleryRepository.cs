using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITechnologiesGalleryRepository
{
    Task<List<Technologies>> GetTechnologiesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTechnologiesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Technologies>> InsertTechnologyGalleryAsync(string userId, string Id, Technologies TechnologyFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusTechnologyGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTechnologiesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarTechnologyGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarTechnologyGalleryAsync(string userId, string technologyId);
    Task<InsertOrUpdateResult<List<(string TechnologyId, double CurrentStar)>>> UpdateBatchCurrentStarTechnologiesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Technologies>>> InsertBatchTechnologiesGalleryAsync(string userId, List<Technologies> technologies);
    Task<Technologies> GetTechnologyCollectionByIdAsync(string userId, string objectId);
    Task UpdateTechnologyGalleryPowerAsync(string userId, string id, Technologies TechnologyFromDB);
    Task<Technologies> SumPowerTechnologiesGalleryAsync(string userId);
}