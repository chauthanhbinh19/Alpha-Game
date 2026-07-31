using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBuildingsGalleryRepository
{
    Task<List<Buildings>> GetBuildingsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBuildingsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Buildings>> InsertBuildingGalleryAsync(string userId, string Id, Buildings BuildingFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBuildingGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBuildingsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarBuildingGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarBuildingGalleryAsync(string userId, string buildingId);
    Task<InsertOrUpdateResult<List<(string BuildingId, double CurrentStar)>>> UpdateBatchCurrentStarBuildingsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Buildings>>> InsertBatchBuildingsGalleryAsync(string userId, List<Buildings> buildings);
    Task<Buildings> GetBuildingCollectionByIdAsync(string userId, string objectId);
    Task UpdateBuildingGalleryPowerAsync(string userId, string Id, Buildings BuildingFromDB);
    Task<Buildings> SumPowerBuildingsGalleryAsync(string userId);
}