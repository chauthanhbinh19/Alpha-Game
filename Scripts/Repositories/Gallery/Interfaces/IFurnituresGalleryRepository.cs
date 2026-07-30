using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnituresGalleryRepository
{
    Task<List<Furnitures>> GetFurnituresCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFurnituresCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Furnitures>> InsertFurnitureGalleryAsync(string userId, string Id, Furnitures FurnitureFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusFurnitureGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFurnituresGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarFurnitureGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarFurnitureGalleryAsync(string userId, string furnitureId);
    Task<InsertOrUpdateResult<List<(string FurnitureId, double CurrentStar)>>> UpdateBatchCurrentStarFurnituresGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Furnitures>>> InsertBatchFurnituresGalleryAsync(string userId, List<Furnitures> furnitures);
    Task<Furnitures> GetFurnitureCollectionByIdAsync(string userId, string objectId);
    Task UpdateFurnitureGalleryPowerAsync(string userId, string Id, Furnitures FurnitureFromDB);
    Task<Furnitures> SumPowerFurnituresGalleryAsync(string userId);
}