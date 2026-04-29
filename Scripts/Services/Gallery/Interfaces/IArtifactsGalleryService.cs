using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtifactsGalleryService
{
    Task<List<Artifacts>> GetArtifactsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetArtifactsCountAsync(string search, string rare);
    Task InsertArtifactGalleryAsync(string Id);
    Task UpdateStatusArtifactGalleryAsync(string Id);
    Task UpdateStarArtifactGalleryAsync(string id, double star);
    Task UpdateArtifactGalleryPowerAsync(string id);
    Task<Artifacts> SumPowerArtifactsGalleryAsync();
}