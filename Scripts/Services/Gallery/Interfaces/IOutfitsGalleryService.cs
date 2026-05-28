using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOutfitsGalleryService
{
    Task<List<Outfits>> GetOutfitsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetOutfitsCountAsync(string search, string type, string rare);
    Task InsertOutfitGalleryAsync(string Id);
    Task UpdateStatusOutfitGalleryAsync(string Id);
    Task UpdateStarOutfitGalleryAsync(string id, double star);
    Task UpdateOutfitGalleryPowerAsync(string id);
    Task<Outfits> SumPowerOutfitsGalleryAsync();
}