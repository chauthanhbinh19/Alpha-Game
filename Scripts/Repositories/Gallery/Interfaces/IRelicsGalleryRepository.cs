using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRelicsGalleryRepository
{
    Task<List<Relics>> GetRelicsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetRelicsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Relics>> InsertRelicGalleryAsync(string userId, string Id, Relics RelicFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusRelicGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusRelicsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarRelicGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarRelicGalleryAsync(string userId, string relicId);
    Task<InsertOrUpdateResult<List<(string RelicId, double CurrentStar)>>> UpdateBatchCurrentStarRelicsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Relics>>> InsertBatchRelicsGalleryAsync(string userId, List<Relics> relics);
    Task<Relics> GetRelicCollectionByIdAsync(string userId, string objectId);
    Task UpdateRelicGalleryPowerAsync(string userId, string Id, Relics RelicFromDB);
    Task<Relics> SumPowerRelicsGalleryAsync(string userId);
}