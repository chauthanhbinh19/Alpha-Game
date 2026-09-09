using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICoresGalleryService
{
    Task<List<Cores>> GetCoresCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCoresCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCoreGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCoreGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCoresGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCoreGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCoreGalleryAsync(string userId, string coreId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCoresGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCoresGalleryAsync(string userId, List<Cores> cores);
    Task<Cores> GetCoreCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCoreGalleryPowerAsync(string userId, string id);
    Task<Cores> SumPowerCoresGalleryAsync(string userId);
}