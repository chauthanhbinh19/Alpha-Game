using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesGalleryService
{
    Task<List<Architectures>> GetArchitecturesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArchitecturesCountAsync(string search, string rare);
    Task InsertArchitectureGalleryAsync(string userId, string Id);
    Task UpdateStatusArchitectureGalleryAsync(string userId, string Id);
    Task UpdateStarArchitectureGalleryAsync(string userId, string id, double star);
    Task UpdateArchitectureGalleryPowerAsync(string userId, string id);
    Task<Architectures> SumPowerArchitecturesGalleryAsync(string userId);
}