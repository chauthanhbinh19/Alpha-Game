using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnituresGalleryService
{
    Task<List<Furnitures>> GetFurnituresCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFurnituresCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertFurnitureGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusFurnitureGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFurnituresGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarFurnitureGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarFurnitureGalleryAsync(string userId, string furnitureId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarFurnituresGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchFurnituresGalleryAsync(string userId, List<Furnitures> furnitures);
    Task<Furnitures> GetFurnitureCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateFurnitureGalleryPowerAsync(string userId, string Id);
    Task<Furnitures> SumPowerFurnituresGalleryAsync(string userId);
}