using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFashionsGalleryRepository
{
    Task<List<Fashions>> GetFashionsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFashionsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Fashions>> InsertFashionGalleryAsync(string userId, string Id, Fashions FashionFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusFashionGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFashionsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarFashionGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarFashionGalleryAsync(string userId, string fashionId);
    Task<InsertOrUpdateResult<List<(string FashionId, double CurrentStar)>>> UpdateBatchCurrentStarFashionsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Fashions>>> InsertBatchFashionsGalleryAsync(string userId, List<Fashions> fashions);
    Task<Fashions> GetFashionCollectionByIdAsync(string userId, string objectId);
    Task UpdateFashionGalleryPowerAsync(string userId, string Id, Fashions FashionFromDB);
    Task<Fashions> SumPowerFashionsGalleryAsync(string userId);
}