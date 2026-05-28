using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksGalleryRepository
{
    Task<List<Artworks>> GetArtworksCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetArtworksCountAsync(string search, string type, string rare);
    Task InsertArtworkGalleryAsync(string Id, Artworks ArtworkFromDB);
    Task UpdateStatusArtworkGalleryAsync(string Id);
    Task UpdateStarArtworkGalleryAsync(string Id, double star);
    Task UpdateArtworkGalleryPowerAsync(string Id, Artworks ArtworkFromDB);
    Task<Artworks> SumPowerArtworksGalleryAsync();
}
