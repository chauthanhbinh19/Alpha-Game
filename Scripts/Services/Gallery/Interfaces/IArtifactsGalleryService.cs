using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtifactsGalleryService
{
    Task<List<Artifacts>> GetArtifactsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArtifactsCountAsync(string search, string rare);
    Task InsertArtifactGalleryAsync(string userId, string Id);
    Task UpdateStatusArtifactGalleryAsync(string userId, string Id);
    Task UpdateStarArtifactGalleryAsync(string userId, string id, double star);
    Task UpdateArtifactGalleryPowerAsync(string userId, string id);
    Task<Artifacts> SumPowerArtifactsGalleryAsync(string userId);
}