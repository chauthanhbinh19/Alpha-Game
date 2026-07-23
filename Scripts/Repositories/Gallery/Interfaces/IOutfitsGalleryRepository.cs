using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOutfitsGalleryRepository
{
    Task<List<Outfits>> GetOutfitsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetOutfitsCountAsync(string search, string type, string rare);
    Task InsertOutfitGalleryAsync(string userId, string Id, Outfits OutfitFromDB);
    Task UpdateStatusOutfitGalleryAsync(string userId, string Id);
    Task UpdateStarOutfitGalleryAsync(string userId, string id, double star);
    Task UpdateOutfitGalleryPowerAsync(string userId, string id, Outfits OutfitFromDB);
    Task<Outfits> SumPowerOutfitsGalleryAsync(string userId);
}