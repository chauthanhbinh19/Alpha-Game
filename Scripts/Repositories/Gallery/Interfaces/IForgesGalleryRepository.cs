using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IForgesGalleryRepository
{
    Task<List<Forges>> GetForgesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetForgesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Forges>> InsertForgeGalleryAsync(string userId, string Id, Forges ForgeFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusForgeGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusForgesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarForgeGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarForgeGalleryAsync(string userId, string forgeId);
    Task<InsertOrUpdateResult<List<(string ForgeId, double CurrentStar)>>> UpdateBatchCurrentStarForgesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Forges>>> InsertBatchForgesGalleryAsync(string userId, List<Forges> forges);
    Task<Forges> GetForgeCollectionByIdAsync(string userId, string objectId);
    Task UpdateForgeGalleryPowerAsync(string userId, string Id, Forges ForgeFromDB);
    Task<Forges> SumPowerForgesGalleryAsync(string userId);
}