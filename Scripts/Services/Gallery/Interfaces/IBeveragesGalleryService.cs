using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesGalleryService
{
    Task<List<Beverages>> GetBeveragesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBeveragesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertBeverageGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBeverageGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBeveragesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarBeverageGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBeverageGalleryAsync(string userId, string beverageId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBeveragesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchBeveragesGalleryAsync(string userId, List<Beverages> beverages);
    Task<Beverages> GetBeverageCollectionByIdAsync(string userId, string beverageId);
    Task<InsertOrUpdateResult<bool>> UpdateBeverageGalleryPowerAsync(string userId, string id);
    Task<Beverages> SumPowerBeveragesGalleryAsync(string userId);
}