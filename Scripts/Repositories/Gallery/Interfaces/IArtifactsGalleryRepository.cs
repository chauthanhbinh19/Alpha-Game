using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtifactsGalleryRepository
{
    Task<List<Artifacts>> GetArtifactsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArtifactsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Artifacts>> InsertArtifactGalleryAsync(string userId, string Id, Artifacts ArtifactFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusArtifactGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArtifactsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarArtifactGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarArtifactGalleryAsync(string userId, string artifactId);
    Task<InsertOrUpdateResult<List<(string ArtifactId, double CurrentStar)>>> UpdateBatchCurrentStarArtifactsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Artifacts>>> InsertBatchArtifactsGalleryAsync(string userId, List<Artifacts> artifacts);
    Task<Artifacts> GetArtifactCollectionByIdAsync(string userId, string objectId);
    Task UpdateArtifactGalleryPowerAsync(string userId, string id, Artifacts ArtifactFromDB);
    Task<Artifacts> SumPowerArtifactsGalleryAsync(string userId);
}