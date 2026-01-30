using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnituresGalleryService
{
    Task<List<Furnitures>> GetFurnituresCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFurnituresCountAsync(string search, string type, string rare);
    Task InsertFurnitureGalleryAsync(string Id);
    Task UpdateStatusFurnitureGalleryAsync(string Id);
    Task UpdateStarFurnitureGalleryAsync(string Id, double star);
    Task UpdateFurnitureGalleryPowerAsync(string Id);
    Task<Furnitures> SumPowerFurnituresGalleryAsync();
}