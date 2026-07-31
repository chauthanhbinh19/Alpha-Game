using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPetsGalleryRepository
{
    Task<List<Pets>> GetPetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPetsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Pets>> InsertPetGalleryAsync(string userId, string Id, Pets PetFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusPetGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusPetsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarPetGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarPetGalleryAsync(string userId, string petId);
    Task<InsertOrUpdateResult<List<(string PetId, double CurrentStar)>>> UpdateBatchCurrentStarPetsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Pets>>> InsertBatchPetsGalleryAsync(string userId, List<Pets> pets);
    Task<Pets> GetPetCollectionByIdAsync(string userId, string objectId);
    Task UpdatePetGalleryPowerAsync(string userId, string Id, Pets PetFromDB);
    Task<Pets> SumPowerPetsGalleryAsync(string userId);
}