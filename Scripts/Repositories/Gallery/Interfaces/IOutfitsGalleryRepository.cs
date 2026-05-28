using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOutfitsGalleryRepository
{
    Task<List<Outfits>> GetOutfitsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetOutfitsCountAsync(string search, string type, string rare);
    Task InsertOutfitGalleryAsync(string Id, Outfits OutfitFromDB);
    Task UpdateStatusOutfitGalleryAsync(string Id);
    Task UpdateStarOutfitGalleryAsync(string id, double star);
    Task UpdateOutfitGalleryPowerAsync(string id, Outfits OutfitFromDB);
    Task<Outfits> SumPowerOutfitsGalleryAsync();
}