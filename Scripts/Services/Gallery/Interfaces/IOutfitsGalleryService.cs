using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOutfitsGalleryService
{
    Task<List<Outfits>> GetOutfitsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetOutfitsCountAsync(string search, string type, string rare);
    Task<bool> InsertOutfitGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusOutfitGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusOutfitsGalleryAsync(string userId);
    Task<bool> UpdateStarOutfitGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarOutfitGalleryAsync(string userId, string outfitId);
    Task<bool> UpdateBatchCurrentStarOutfitsGalleryAsync(string userId);
    Task<bool> InsertBatchOutfitsGalleryAsync(string userId, List<Outfits> outfits);
    Task<Outfits> GetOutfitCollectionByIdAsync(string userId, string objectId);
    Task UpdateOutfitGalleryPowerAsync(string userId, string id);
    Task<Outfits> SumPowerOutfitsGalleryAsync(string userId);
}