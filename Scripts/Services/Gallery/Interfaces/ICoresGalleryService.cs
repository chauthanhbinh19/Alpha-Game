using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICoresGalleryService
{
    Task<List<Cores>> GetCoresCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCoresCountAsync(string search, string rare);
    Task<bool> InsertCoreGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCoreGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCoresGalleryAsync(string userId);
    Task<bool> UpdateTempStarCoreGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarCoreGalleryAsync(string userId, string coreId);
    Task<bool> UpdateBatchCurrentStarCoresGalleryAsync(string userId);
    Task<bool> InsertBatchCoresGalleryAsync(string userId, List<Cores> cores);
    Task<Cores> GetCoreCollectionByIdAsync(string userId, string objectId);
    Task UpdateCoreGalleryPowerAsync(string userId, string id);
    Task<Cores> SumPowerCoresGalleryAsync(string userId);
}