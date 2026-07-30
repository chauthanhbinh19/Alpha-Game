using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsGalleryService
{
    Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetSpiritBeastsCountAsync(string search, string rare);
    Task<bool> InsertSpiritBeastGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusSpiritBeastGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusSpiritBeastsGalleryAsync(string userId);
    Task<bool> UpdateStarSpiritBeastGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarSpiritBeastGalleryAsync(string userId, string spiritBeastId);
    Task<bool> UpdateBatchCurrentStarSpiritBeastsGalleryAsync(string userId);
    Task<bool> InsertBatchSpiritBeastsGalleryAsync(string userId, List<SpiritBeasts> spiritBeasts);
    Task<SpiritBeasts> GetSpiritBeastCollectionByIdAsync(string userId, string objectId);
    Task UpdateSpiritBeastGalleryPowerAsync(string userId, string id);
    Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync(string userId);
}