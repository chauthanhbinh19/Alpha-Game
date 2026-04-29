using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFoodsGalleryService
{
    Task<List<Foods>> GetFoodsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetFoodsCountAsync(string search, string rare);
    Task InsertFoodGalleryAsync(string Id);
    Task UpdateStatusFoodGalleryAsync(string Id);
    Task UpdateStarFoodGalleryAsync(string id, double star);
    Task UpdateFoodGalleryPowerAsync(string id);
    Task<Foods> SumPowerFoodsGalleryAsync();
}