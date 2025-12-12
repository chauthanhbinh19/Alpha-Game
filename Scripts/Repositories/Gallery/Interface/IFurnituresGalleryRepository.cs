using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnitureGalleryRepository
{
    Task<List<Furnitures>> GetFurnituresCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetFurnituresCountAsync(string type, string rare);
    Task InsertFurnitureGalleryAsync(string Id, Furnitures FurnitureFromDB);
    Task UpdateStatusFurnitureGalleryAsync(string Id);
    Task UpdateStarFurnitureGalleryAsync(string Id, double star);
    Task UpdateFurnitureGalleryPowerAsync(string Id, Furnitures FurnitureFromDB);
    Task<Furnitures> SumPowerFurnituresGalleryAsync();
}