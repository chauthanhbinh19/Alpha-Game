using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsGalleryService
{
    Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetSpiritBeastsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertSpiritBeastGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusSpiritBeastGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSpiritBeastsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarSpiritBeastGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarSpiritBeastGalleryAsync(string userId, string spiritBeastId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarSpiritBeastsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchSpiritBeastsGalleryAsync(string userId, List<SpiritBeasts> spiritBeasts);
    Task<SpiritBeasts> GetSpiritBeastCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateSpiritBeastGalleryPowerAsync(string userId, string id);
    Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync(string userId);
}