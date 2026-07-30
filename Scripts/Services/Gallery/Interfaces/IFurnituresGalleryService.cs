using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnituresGalleryService
{
    Task<List<Furnitures>> GetFurnituresCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFurnituresCountAsync(string search, string type, string rare);
    Task<bool> InsertFurnitureGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusFurnitureGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusFurnituresGalleryAsync(string userId);
    Task<bool> UpdateStarFurnitureGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarFurnitureGalleryAsync(string userId, string furnitureId);
    Task<bool> UpdateBatchCurrentStarFurnituresGalleryAsync(string userId);
    Task<bool> InsertBatchFurnituresGalleryAsync(string userId, List<Furnitures> furnitures);
    Task<Furnitures> GetFurnitureCollectionByIdAsync(string userId, string objectId);
    Task UpdateFurnitureGalleryPowerAsync(string userId, string Id);
    Task<Furnitures> SumPowerFurnituresGalleryAsync(string userId);
}