using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPetsGalleryService
{
    Task<List<Pets>> GetPetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPetsCountAsync(string search, string type, string rare);
    Task<bool> InsertPetGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusPetGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusPetsGalleryAsync(string userId);
    Task<bool> UpdateStarPetGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarPetGalleryAsync(string userId, string petId);
    Task<bool> UpdateBatchCurrentStarPetsGalleryAsync(string userId);
    Task<bool> InsertBatchPetsGalleryAsync(string userId, List<Pets> pets);
    Task<Pets> GetPetCollectionByIdAsync(string userId, string objectId);
    Task UpdatePetGalleryPowerAsync(string userId, string Id);
    Task<Pets> SumPowerPetsGalleryAsync(string userId);
}