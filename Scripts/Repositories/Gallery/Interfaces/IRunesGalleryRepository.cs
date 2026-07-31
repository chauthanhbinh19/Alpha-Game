using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRunesGalleryRepository
{
    Task<List<Runes>> GetRunesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRunesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Runes>> InsertRuneGalleryAsync(string userId, string Id, Runes RuneFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusRuneGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusRunesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarRuneGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarRuneGalleryAsync(string userId, string runeId);
    Task<InsertOrUpdateResult<List<(string RuneId, double CurrentStar)>>> UpdateBatchCurrentStarRunesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Runes>>> InsertBatchRunesGalleryAsync(string userId, List<Runes> runes);
    Task<Runes> GetRuneCollectionByIdAsync(string userId, string objectId);
    Task UpdateRuneGalleryPowerAsync(string userId, string id, Runes RuneFromDB);
    Task<Runes> SumPowerRunesGalleryAsync(string userId);
}