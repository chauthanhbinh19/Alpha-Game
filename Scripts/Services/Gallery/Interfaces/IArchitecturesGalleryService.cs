using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesGalleryService
{
    Task<List<Architectures>> GetArchitecturesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArchitecturesCountAsync(string search, string rare);
    Task<bool> InsertArchitectureGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusArchitectureGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusArchitecturesGalleryAsync(string userId);
    Task<bool> UpdateStarArchitectureGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarArchitectureGalleryAsync(string userId, string architectureId);
    Task<bool> UpdateBatchCurrentStarArchitecturesGalleryAsync(string userId);
    Task<bool> InsertBatchArchitecturesGalleryAsync(string userId, List<Architectures> architectures);
    Task<Architectures> GetArchitectureCollectionByIdAsync(string userId, string objectId);
    Task UpdateArchitectureGalleryPowerAsync(string userId, string id);
    Task<Architectures> SumPowerArchitecturesGalleryAsync(string userId);
}