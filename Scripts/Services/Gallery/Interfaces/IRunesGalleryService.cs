using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRunesGalleryService
{
    Task<List<Runes>> GetRunesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRunesCountAsync(string search, string rare);
    Task<bool> InsertRuneGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusRuneGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusRunesGalleryAsync(string userId);
    Task<bool> UpdateStarRuneGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarRuneGalleryAsync(string userId, string runeId);
    Task<bool> UpdateBatchCurrentStarRunesGalleryAsync(string userId);
    Task<bool> InsertBatchRunesGalleryAsync(string userId, List<Runes> runes);
    Task<Runes> GetRuneCollectionByIdAsync(string userId, string objectId);
    Task UpdateRuneGalleryPowerAsync(string userId, string id);
    Task<Runes> SumPowerRunesGalleryAsync(string userId);
}