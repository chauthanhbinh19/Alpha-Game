using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtifactsGalleryService
{
    Task<List<Artifacts>> GetArtifactsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArtifactsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertArtifactGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusArtifactGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArtifactsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarArtifactGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarArtifactGalleryAsync(string userId, string artifactId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarArtifactsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchArtifactsGalleryAsync(string userId, List<Artifacts> artifacts);
    Task<Artifacts> GetArtifactCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateArtifactGalleryPowerAsync(string userId, string id);
    Task<Artifacts> SumPowerArtifactsGalleryAsync(string userId);
}