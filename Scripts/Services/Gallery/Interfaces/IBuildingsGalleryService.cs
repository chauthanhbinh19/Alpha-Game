using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBuildingsGalleryService
{
    Task<List<Buildings>> GetBuildingsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBuildingsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertBuildingGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBuildingGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBuildingsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarBuildingGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBuildingGalleryAsync(string userId, string buildingId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBuildingsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchBuildingsGalleryAsync(string userId, List<Buildings> buildings);
    Task<Buildings> GetBuildingCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateBuildingGalleryPowerAsync(string userId, string Id);
    Task<Buildings> SumPowerBuildingsGalleryAsync(string userId);
}