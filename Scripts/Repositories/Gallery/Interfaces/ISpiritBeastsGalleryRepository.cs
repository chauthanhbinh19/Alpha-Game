using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsGalleryRepository
{
    Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetSpiritBeastsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<SpiritBeasts>> InsertSpiritBeastGalleryAsync(string userId, string Id, SpiritBeasts SpiritBeastFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusSpiritBeastGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSpiritBeastsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarSpiritBeastGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarSpiritBeastGalleryAsync(string userId, string spiritBeastId);
    Task<InsertOrUpdateResult<List<(string SpiritBeastId, double CurrentStar)>>> UpdateBatchCurrentStarSpiritBeastsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<SpiritBeasts>>> InsertBatchSpiritBeastsGalleryAsync(string userId, List<SpiritBeasts> spiritBeasts);
    Task<SpiritBeasts> GetSpiritBeastCollectionByIdAsync(string userId, string objectId);
    Task UpdateSpiritBeastGalleryPowerAsync(string userId, string id, SpiritBeasts SpiritBeastFromDB);
    Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync(string userId);
}