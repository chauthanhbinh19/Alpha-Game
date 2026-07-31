using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOutfitsGalleryRepository
{
    Task<List<Outfits>> GetOutfitsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetOutfitsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Outfits>> InsertOutfitGalleryAsync(string userId, string Id, Outfits OutfitFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusOutfitGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusOutfitsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarOutfitGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarOutfitGalleryAsync(string userId, string outfitId);
    Task<InsertOrUpdateResult<List<(string OutfitId, double CurrentStar)>>> UpdateBatchCurrentStarOutfitsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Outfits>>> InsertBatchOutfitsGalleryAsync(string userId, List<Outfits> outfits);
    Task<Outfits> GetOutfitCollectionByIdAsync(string userId, string objectId);
    Task UpdateOutfitGalleryPowerAsync(string userId, string id, Outfits OutfitFromDB);
    Task<Outfits> SumPowerOutfitsGalleryAsync(string userId);
}