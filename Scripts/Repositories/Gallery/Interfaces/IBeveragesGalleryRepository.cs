using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesGalleryRepository
{
    Task<List<Beverages>> GetBeveragesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBeveragesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Beverages>> InsertBeverageGalleryAsync(string userId, string Id, Beverages BeverageFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBeverageGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBeveragesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarBeverageGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarBeverageGalleryAsync(string userId, string beverageId);
    Task<InsertOrUpdateResult<List<(string BeverageId, double CurrentStar)>>> UpdateBatchCurrentStarBeveragesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Beverages>>> InsertBatchBeveragesGalleryAsync(string userId, List<Beverages> beverages);
    Task<Beverages> GetBeverageCollectionByIdAsync(string userId, string beverageId);
    Task UpdateBeverageGalleryPowerAsync(string userId, string id, Beverages BeverageFromDB);
    Task<Beverages> SumPowerBeveragesGalleryAsync(string userId);
}