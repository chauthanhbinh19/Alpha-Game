using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksGalleryService
{
    Task<List<Artworks>> GetArtworksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetArtworksCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertArtworkGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusArtworkGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArtworksGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarArtworkGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarArtworkGalleryAsync(string userId, string artworkId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarArtworksGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchArtworksGalleryAsync(string userId, List<Artworks> artworks);
    Task<Artworks> GetArtworkCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateArtworkGalleryPowerAsync(string userId, string Id);
    Task<Artworks> SumPowerArtworksGalleryAsync(string userId);
}
