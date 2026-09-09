using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITalismansGalleryService
{
    Task<List<Talismans>> GetTalismansCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetTalismansCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertTalismanGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusTalismanGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTalismansGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarTalismanGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarTalismanGalleryAsync(string userId, string talismanId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarTalismansGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchTalismansGalleryAsync(string userId, List<Talismans> talismans);
    Task<Talismans> GetTalismanCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateTalismanGalleryPowerAsync(string userId, string Id);
    Task<Talismans> SumPowerTalismansGalleryAsync(string userId);
}