using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksGalleryService
{
    Task<List<Artworks>> GetArtworksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetArtworksCountAsync(string search, string type, string rare);
    Task<bool> InsertArtworkGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusArtworkGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusArtworksGalleryAsync(string userId);
    Task<bool> UpdateStarArtworkGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarArtworkGalleryAsync(string userId, string artworkId);
    Task<bool> UpdateBatchCurrentStarArtworksGalleryAsync(string userId);
    Task<bool> InsertBatchArtworksGalleryAsync(string userId, List<Artworks> artworks);
    Task<Artworks> GetArtworkCollectionByIdAsync(string userId, string objectId);
    Task UpdateArtworkGalleryPowerAsync(string userId, string Id);
    Task<Artworks> SumPowerArtworksGalleryAsync(string userId);
}
