using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlantsGalleryService
{
    Task<List<Plants>> GetPlantsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetPlantsCountAsync(string search, string rare);
    Task<bool> InsertPlantGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusPlantGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusPlantsGalleryAsync(string userId);
    Task<bool> UpdateTempStarPlantGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarPlantGalleryAsync(string userId, string plantId);
    Task<bool> UpdateBatchCurrentStarPlantsGalleryAsync(string userId);
    Task<bool> InsertBatchPlantsGalleryAsync(string userId, List<Plants> plants);
    Task<Plants> GetPlantCollectionByIdAsync(string userId, string objectId);
    Task UpdatePlantGalleryPowerAsync(string userId, string id);
    Task<Plants> SumPowerPlantsGalleryAsync(string userId);
}