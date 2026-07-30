using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITalismansGalleryRepository
{
    Task<List<Talismans>> GetTalismansCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetTalismansCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Talismans>> InsertTalismanGalleryAsync(string userId, string Id, Talismans TalismanFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusTalismanGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTalismansGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarTalismanGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarTalismanGalleryAsync(string userId, string talismanId);
    Task<InsertOrUpdateResult<List<(string TalismanId, double CurrentStar)>>> UpdateBatchCurrentStarTalismansGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Talismans>>> InsertBatchTalismansGalleryAsync(string userId, List<Talismans> talismans);
    Task<Talismans> GetTalismanCollectionByIdAsync(string userId, string objectId);
    Task UpdateTalismanGalleryPowerAsync(string userId, string Id, Talismans TalismanFromDB);
    Task<Talismans> SumPowerTalismansGalleryAsync(string userId);
}