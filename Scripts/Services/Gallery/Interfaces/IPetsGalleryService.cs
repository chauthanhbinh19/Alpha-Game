using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPetsGalleryService
{
    Task<List<Pets>> GetPetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPetsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertPetGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusPetGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusPetsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarPetGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarPetGalleryAsync(string userId, string petId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarPetsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchPetsGalleryAsync(string userId, List<Pets> pets);
    Task<Pets> GetPetCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdatePetGalleryPowerAsync(string userId, string Id);
    Task<Pets> SumPowerPetsGalleryAsync(string userId);
}