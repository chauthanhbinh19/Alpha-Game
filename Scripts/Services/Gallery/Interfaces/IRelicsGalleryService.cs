using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRelicsGalleryService
{
    Task<List<Relics>> GetRelicsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetRelicsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertRelicGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusRelicGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusRelicsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarRelicGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarRelicGalleryAsync(string userId, string relicId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarRelicsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchRelicsGalleryAsync(string userId, List<Relics> relics);
    Task<Relics> GetRelicCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateRelicGalleryPowerAsync(string userId, string Id);
    Task<Relics> SumPowerRelicsGalleryAsync(string userId);
}