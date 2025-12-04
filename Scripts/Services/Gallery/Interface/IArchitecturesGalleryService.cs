using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesGalleryService
{
    Task<List<Architectures>> GetArchitecturesCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetArchitecturesCountAsync(string rare);
    Task InsertArchitectureGalleryAsync(string Id);
    Task UpdateStatusArchitectureGalleryAsync(string Id);
    Task UpdateStarArchitectureGalleryAsync(string id, double star);
    Task UpdateArchitectureGalleryPowerAsync(string id);
    Task<Architectures> SumPowerArchitecturesGalleryAsync();
}