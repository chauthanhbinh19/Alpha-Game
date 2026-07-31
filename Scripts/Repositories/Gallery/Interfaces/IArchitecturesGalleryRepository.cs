using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesGalleryRepository
{
    Task<List<Architectures>> GetArchitecturesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetArchitecturesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Architectures>> InsertArchitectureGalleryAsync(string userId, string Id, Architectures ArchitectureFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusArchitectureGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArchitecturesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarArchitectureGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarArchitectureGalleryAsync(string userId, string architectureId);
    Task<InsertOrUpdateResult<List<(string ArchitectureId, double CurrentStar)>>> UpdateBatchCurrentStarArchitecturesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Architectures>>> InsertBatchArchitecturesGalleryAsync(string userId, List<Architectures> architectures);
    Task<Architectures> GetArchitectureCollectionByIdAsync(string userId, string objectId);
    Task UpdateArchitectureGalleryPowerAsync(string userId, string id, Architectures ArchitectureFromDB);
    Task<Architectures> SumPowerArchitecturesGalleryAsync(string userId);
}