using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesGalleryRepository
{
    Task<List<Architectures>> GetArchitecturesCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetArchitecturesCountAsync(string rare);
    Task InsertArchitectureGalleryAsync(string Id, Architectures ArchitectureFromDB);
    Task UpdateStatusArchitectureGalleryAsync(string Id);
    Task UpdateStarArchitectureGalleryAsync(string id, double star);
    Task UpdateArchitectureGalleryPowerAsync(string id, Architectures ArchitectureFromDB);
    Task<Architectures> SumPowerArchitecturesGalleryAsync();
}