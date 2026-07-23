using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksGalleryRepository
{
    Task<List<Artworks>> GetArtworksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetArtworksCountAsync(string search, string type, string rare);
    Task InsertArtworkGalleryAsync(string userId, string Id, Artworks ArtworkFromDB);
    Task UpdateStatusArtworkGalleryAsync(string userId, string Id);
    Task UpdateStarArtworkGalleryAsync(string userId, string Id, double star);
    Task UpdateArtworkGalleryPowerAsync(string userId, string Id, Artworks ArtworkFromDB);
    Task<Artworks> SumPowerArtworksGalleryAsync(string userId);
}
