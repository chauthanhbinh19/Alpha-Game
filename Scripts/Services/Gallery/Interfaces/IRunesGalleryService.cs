using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRunesGalleryService
{
    Task<List<Runes>> GetRunesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRunesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertRuneGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusRuneGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusRunesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarRuneGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarRuneGalleryAsync(string userId, string runeId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarRunesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchRunesGalleryAsync(string userId, List<Runes> runes);
    Task<Runes> GetRuneCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateRuneGalleryPowerAsync(string userId, string id);
    Task<Runes> SumPowerRunesGalleryAsync(string userId);
}