using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnituresGalleryService
{
    Task<List<Furnitures>> GetFurnituresCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFurnituresCountAsync(string search, string type, string rare);
    Task InsertFurnitureGalleryAsync(string userId, string Id);
    Task UpdateStatusFurnitureGalleryAsync(string userId, string Id);
    Task UpdateStarFurnitureGalleryAsync(string userId, string Id, double star);
    Task UpdateFurnitureGalleryPowerAsync(string userId, string Id);
    Task<Furnitures> SumPowerFurnituresGalleryAsync(string userId);
}