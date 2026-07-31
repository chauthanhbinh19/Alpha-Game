using System.Collections.Generic;
using System.Threading.Tasks;

public interface IForgesGalleryService
{
    Task<List<Forges>> GetForgesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetForgesCountAsync(string search, string type, string rare);
    Task<bool> InsertForgeGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusForgeGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusForgesGalleryAsync(string userId);
    Task<bool> UpdateTempStarForgeGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarForgeGalleryAsync(string userId, string forgeId);
    Task<bool> UpdateBatchCurrentStarForgesGalleryAsync(string userId);
    Task<bool> InsertBatchForgesGalleryAsync(string userId, List<Forges> forges);
    Task<Forges> GetForgeCollectionByIdAsync(string userId, string objectId);
    Task UpdateForgeGalleryPowerAsync(string userId, string Id);
    Task<Forges> SumPowerForgesGalleryAsync(string userId);
}