using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlantsGalleryRepository
{
    Task<List<Plants>> GetPlantsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetPlantsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Plants>> InsertPlantGalleryAsync(string userId, string Id, Plants PlantFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusPlantGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusPlantsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarPlantGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarPlantGalleryAsync(string userId, string plantId);
    Task<InsertOrUpdateResult<List<(string PlantId, double CurrentStar)>>> UpdateBatchCurrentStarPlantsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Plants>>> InsertBatchPlantsGalleryAsync(string userId, List<Plants> plants);
    Task<Plants> GetPlantCollectionByIdAsync(string userId, string objectId);
    Task UpdatePlantGalleryPowerAsync(string userId, string id, Plants PlantFromDB);
    Task<Plants> SumPowerPlantsGalleryAsync(string userId);
}