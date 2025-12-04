using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksGalleryService
{
    Task<List<Artworks>> GetArtworksCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetArtworksCountAsync(string type, string rare);
    Task InsertArtworkGalleryAsync(string Id);
    Task UpdateStatusArtworkGalleryAsync(string Id);
    Task UpdateStarArtworkGalleryAsync(string Id, double star);
    Task UpdateArtworkGalleryPowerAsync(string Id);
    Task<Artworks> SumPowerArtworksGalleryAsync();
}
