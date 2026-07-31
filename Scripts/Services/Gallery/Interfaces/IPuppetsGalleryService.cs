using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPuppetsGalleryService
{
    Task<List<Puppets>> GetPuppetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPuppetsCountAsync(string search, string type, string rare);
    Task<bool> InsertPuppetGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusPuppetGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusPuppetsGalleryAsync(string userId);
    Task<bool> UpdateTempStarPuppetGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarPuppetGalleryAsync(string userId, string puppetId);
    Task<bool> UpdateBatchCurrentStarPuppetsGalleryAsync(string userId);
    Task<bool> InsertBatchPuppetsGalleryAsync(string userId, List<Puppets> puppets);
    Task<Puppets> GetPuppetCollectionByIdAsync(string userId, string objectId);
    Task UpdatePuppetGalleryPowerAsync(string userId, string Id);
    Task<Puppets> SumPowerPuppetsGalleryAsync(string userId);
}