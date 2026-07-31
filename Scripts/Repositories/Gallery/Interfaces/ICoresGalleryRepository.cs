using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICoresGalleryRepository
{
    Task<List<Cores>> GetCoresCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCoresCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Cores>> InsertCoreGalleryAsync(string userId, string Id, Cores CoreFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCoreGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCoresGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarCoreGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCoreGalleryAsync(string userId, string coreId);
    Task<InsertOrUpdateResult<List<(string CoreId, double CurrentStar)>>> UpdateBatchCurrentStarCoresGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Cores>>> InsertBatchCoresGalleryAsync(string userId, List<Cores> cores);
    Task<Cores> GetCoreCollectionByIdAsync(string userId, string objectId);
    Task UpdateCoreGalleryPowerAsync(string userId, string id, Cores CoreFromDB);
    Task<Cores> SumPowerCoresGalleryAsync(string userId);
}