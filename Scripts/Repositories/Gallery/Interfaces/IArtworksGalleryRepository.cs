using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksGalleryRepository
{
    Task<List<Artworks>> GetArtworksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetArtworksCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Artworks>> InsertArtworkGalleryAsync(string userId, string Id, Artworks ArtworkFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusArtworkGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArtworksGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarArtworkGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarArtworkGalleryAsync(string userId, string artworkId);
    Task<InsertOrUpdateResult<List<(string ArtworkId, double CurrentStar)>>> UpdateBatchCurrentStarArtworksGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Artworks>>> InsertBatchArtworksGalleryAsync(string userId, List<Artworks> artworks);
    Task<Artworks> GetArtworkCollectionByIdAsync(string userId, string objectId);
    Task UpdateArtworkGalleryPowerAsync(string userId, string Id, Artworks ArtworkFromDB);
    Task<Artworks> SumPowerArtworksGalleryAsync(string userId);
}
