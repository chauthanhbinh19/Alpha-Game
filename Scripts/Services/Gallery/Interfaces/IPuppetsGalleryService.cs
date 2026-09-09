using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPuppetsGalleryService
{
    Task<List<Puppets>> GetPuppetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPuppetsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertPuppetGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusPuppetGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusPuppetsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarPuppetGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarPuppetGalleryAsync(string userId, string puppetId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarPuppetsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchPuppetsGalleryAsync(string userId, List<Puppets> puppets);
    Task<Puppets> GetPuppetCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdatePuppetGalleryPowerAsync(string userId, string Id);
    Task<Puppets> SumPowerPuppetsGalleryAsync(string userId);
}