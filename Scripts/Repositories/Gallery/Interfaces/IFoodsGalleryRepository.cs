using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFoodsGalleryRepository
{
    Task<List<Foods>> GetFoodsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetFoodsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Foods>> InsertFoodGalleryAsync(string userId, string Id, Foods FoodFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusFoodGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFoodsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarFoodGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarFoodGalleryAsync(string userId, string foodId);
    Task<InsertOrUpdateResult<List<(string FoodId, double CurrentStar)>>> UpdateBatchCurrentStarFoodsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Foods>>> InsertBatchFoodsGalleryAsync(string userId, List<Foods> foods);
    Task<Foods> GetFoodCollectionByIdAsync(string userId, string objectId);
    Task UpdateFoodGalleryPowerAsync(string userId, string id, Foods FoodFromDB);
    Task<Foods> SumPowerFoodsGalleryAsync(string userId);
}