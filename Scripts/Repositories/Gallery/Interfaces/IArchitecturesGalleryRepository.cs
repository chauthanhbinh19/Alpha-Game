using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesGalleryRepository
{
    Task<List<Architectures>> GetArchitecturesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArchitecturesCountAsync(string search, string rare);
    Task InsertArchitectureGalleryAsync(string userId, string Id, Architectures ArchitectureFromDB);
    Task UpdateStatusArchitectureGalleryAsync(string userId, string Id);
    Task UpdateStarArchitectureGalleryAsync(string userId, string id, double star);
    Task UpdateArchitectureGalleryPowerAsync(string userId, string id, Architectures ArchitectureFromDB);
    Task<Architectures> SumPowerArchitecturesGalleryAsync(string userId);
}