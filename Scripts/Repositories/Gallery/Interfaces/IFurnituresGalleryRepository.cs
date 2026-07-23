using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnituresGalleryRepository
{
    Task<List<Furnitures>> GetFurnituresCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFurnituresCountAsync(string search, string type, string rare);
    Task InsertFurnitureGalleryAsync(string userId, string Id, Furnitures FurnitureFromDB);
    Task UpdateStatusFurnitureGalleryAsync(string userId, string Id);
    Task UpdateStarFurnitureGalleryAsync(string userId, string Id, double star);
    Task UpdateFurnitureGalleryPowerAsync(string userId, string Id, Furnitures FurnitureFromDB);
    Task<Furnitures> SumPowerFurnituresGalleryAsync(string userId);
}