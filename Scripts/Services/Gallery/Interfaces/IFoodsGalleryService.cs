using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFoodsGalleryService
{
    Task<List<Foods>> GetFoodsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetFoodsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertFoodGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusFoodGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFoodsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarFoodGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarFoodGalleryAsync(string userId, string foodId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarFoodsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchFoodsGalleryAsync(string userId, List<Foods> foods);
    Task<Foods> GetFoodCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateFoodGalleryPowerAsync(string userId, string id);
    Task<Foods> SumPowerFoodsGalleryAsync(string userId);
}