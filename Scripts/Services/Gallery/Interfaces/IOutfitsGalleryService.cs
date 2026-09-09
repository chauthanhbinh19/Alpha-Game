using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOutfitsGalleryService
{
    Task<List<Outfits>> GetOutfitsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetOutfitsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOutfitGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusOutfitGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusOutfitsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarOutfitGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarOutfitGalleryAsync(string userId, string outfitId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarOutfitsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchOutfitsGalleryAsync(string userId, List<Outfits> outfits);
    Task<Outfits> GetOutfitCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateOutfitGalleryPowerAsync(string userId, string id);
    Task<Outfits> SumPowerOutfitsGalleryAsync(string userId);
}