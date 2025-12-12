using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFoodsGalleryRepository
{
    Task<List<Foods>> GetFoodsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetFoodsCountAsync(string rare);
    Task InsertFoodGalleryAsync(string Id, Foods FoodFromDB);
    Task UpdateStatusFoodGalleryAsync(string Id);
    Task UpdateStarFoodGalleryAsync(string id, double star);
    Task UpdateFoodGalleryPowerAsync(string id, Foods FoodFromDB);
    Task<Foods> SumPowerFoodsGalleryAsync();
}