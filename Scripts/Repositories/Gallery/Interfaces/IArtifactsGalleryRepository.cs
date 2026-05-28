using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtifactsGalleryRepository
{
    Task<List<Artifacts>> GetArtifactsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetArtifactsCountAsync(string search, string rare);
    Task InsertArtifactGalleryAsync(string Id, Artifacts ArtifactFromDB);
    Task UpdateStatusArtifactGalleryAsync(string Id);
    Task UpdateStarArtifactGalleryAsync(string id, double star);
    Task UpdateArtifactGalleryPowerAsync(string id, Artifacts ArtifactFromDB);
    Task<Artifacts> SumPowerArtifactsGalleryAsync();
}