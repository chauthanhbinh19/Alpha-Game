using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksGalleryService
{
    Task<List<Artworks>> GetArtworksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetArtworksCountAsync(string search, string type, string rare);
    Task InsertArtworkGalleryAsync(string userId, string Id);
    Task UpdateStatusArtworkGalleryAsync(string userId, string Id);
    Task UpdateStarArtworkGalleryAsync(string userId, string Id, double star);
    Task UpdateArtworkGalleryPowerAsync(string userId, string Id);
    Task<Artworks> SumPowerArtworksGalleryAsync(string userId);
}
