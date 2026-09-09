using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesGalleryService
{
    Task<List<Architectures>> GetArchitecturesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArchitecturesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertArchitectureGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusArchitectureGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArchitecturesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarArchitectureGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarArchitectureGalleryAsync(string userId, string architectureId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarArchitecturesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchArchitecturesGalleryAsync(string userId, List<Architectures> architectures);
    Task<Architectures> GetArchitectureCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateArchitectureGalleryPowerAsync(string userId, string id);
    Task<Architectures> SumPowerArchitecturesGalleryAsync(string userId);
}