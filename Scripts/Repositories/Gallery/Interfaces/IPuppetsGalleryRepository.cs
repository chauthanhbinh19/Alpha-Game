using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPuppetsGalleryRepository
{
    Task<List<Puppets>> GetPuppetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPuppetsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Puppets>> InsertPuppetGalleryAsync(string userId, string Id, Puppets PuppetFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusPuppetGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusPuppetsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarPuppetGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarPuppetGalleryAsync(string userId, string puppetId);
    Task<InsertOrUpdateResult<List<(string PuppetId, double CurrentStar)>>> UpdateBatchCurrentStarPuppetsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Puppets>>> InsertBatchPuppetsGalleryAsync(string userId, List<Puppets> puppets);
    Task<Puppets> GetPuppetCollectionByIdAsync(string userId, string objectId);
    Task UpdatePuppetGalleryPowerAsync(string userId, string Id, Puppets PuppetFromDB);
    Task<Puppets> SumPowerPuppetsGalleryAsync(string userId);
}