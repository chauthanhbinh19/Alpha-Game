using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFoodsGalleryService
{
    Task<List<Foods>> GetFoodsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetFoodsCountAsync(string search, string rare);
    Task<bool> InsertFoodGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusFoodGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusFoodsGalleryAsync(string userId);
    Task<bool> UpdateStarFoodGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarFoodGalleryAsync(string userId, string foodId);
    Task<bool> UpdateBatchCurrentStarFoodsGalleryAsync(string userId);
    Task<bool> InsertBatchFoodsGalleryAsync(string userId, List<Foods> foods);
    Task<Foods> GetFoodCollectionByIdAsync(string userId, string objectId);
    Task UpdateFoodGalleryPowerAsync(string userId, string id);
    Task<Foods> SumPowerFoodsGalleryAsync(string userId);
}