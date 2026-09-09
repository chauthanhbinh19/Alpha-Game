using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlantsGalleryService
{
    Task<List<Plants>> GetPlantsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetPlantsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertPlantGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusPlantGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusPlantsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarPlantGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarPlantGalleryAsync(string userId, string plantId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarPlantsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchPlantsGalleryAsync(string userId, List<Plants> plants);
    Task<Plants> GetPlantCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdatePlantGalleryPowerAsync(string userId, string id);
    Task<Plants> SumPowerPlantsGalleryAsync(string userId);
}