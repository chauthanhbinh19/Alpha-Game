using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBuildingsGalleryService
{
    Task<List<Buildings>> GetBuildingsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBuildingsCountAsync(string search, string type, string rare);
    Task<bool> InsertBuildingGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusBuildingGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusBuildingsGalleryAsync(string userId);
    Task<bool> UpdateStarBuildingGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarBuildingGalleryAsync(string userId, string buildingId);
    Task<bool> UpdateBatchCurrentStarBuildingsGalleryAsync(string userId);
    Task<bool> InsertBatchBuildingsGalleryAsync(string userId, List<Buildings> buildings);
    Task<Buildings> GetBuildingCollectionByIdAsync(string userId, string objectId);
    Task UpdateBuildingGalleryPowerAsync(string userId, string Id);
    Task<Buildings> SumPowerBuildingsGalleryAsync(string userId);
}