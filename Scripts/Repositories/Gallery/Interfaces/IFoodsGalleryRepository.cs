using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFoodsGalleryRepository
{
    Task<List<Foods>> GetFoodsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetFoodsCountAsync(string search, string rare);
    Task InsertFoodGalleryAsync(string userId, string Id, Foods FoodFromDB);
    Task UpdateStatusFoodGalleryAsync(string userId, string Id);
    Task UpdateStarFoodGalleryAsync(string userId, string id, double star);
    Task UpdateFoodGalleryPowerAsync(string userId, string id, Foods FoodFromDB);
    Task<Foods> SumPowerFoodsGalleryAsync(string userId);
}