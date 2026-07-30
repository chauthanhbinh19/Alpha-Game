using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITechnologiesGalleryService
{
    Task<List<Technologies>> GetTechnologiesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTechnologiesCountAsync(string search, string rare);
    Task<bool> InsertTechnologyGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusTechnologyGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusTechnologiesGalleryAsync(string userId);
    Task<bool> UpdateStarTechnologyGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarTechnologyGalleryAsync(string userId, string technologyId);
    Task<bool> UpdateBatchCurrentStarTechnologiesGalleryAsync(string userId);
    Task<bool> InsertBatchTechnologiesGalleryAsync(string userId, List<Technologies> technologies);
    Task<Technologies> GetTechnologyCollectionByIdAsync(string userId, string objectId);
    Task UpdateTechnologyGalleryPowerAsync(string userId, string id);
    Task<Technologies> SumPowerTechnologiesGalleryAsync(string userId);
}