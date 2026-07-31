using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtifactsGalleryService
{
    Task<List<Artifacts>> GetArtifactsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArtifactsCountAsync(string search, string rare);
    Task<bool> InsertArtifactGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusArtifactGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusArtifactsGalleryAsync(string userId);
    Task<bool> UpdateTempStarArtifactGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarArtifactGalleryAsync(string userId, string artifactId);
    Task<bool> UpdateBatchCurrentStarArtifactsGalleryAsync(string userId);
    Task<bool> InsertBatchArtifactsGalleryAsync(string userId, List<Artifacts> artifacts);
    Task<Artifacts> GetArtifactCollectionByIdAsync(string userId, string objectId);
    Task UpdateArtifactGalleryPowerAsync(string userId, string id);
    Task<Artifacts> SumPowerArtifactsGalleryAsync(string userId);
}