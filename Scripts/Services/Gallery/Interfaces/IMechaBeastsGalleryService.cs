using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsGalleryService
{
    Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMechaBeastsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertMechaBeastGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusMechaBeastGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMechaBeastsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarMechaBeastGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarMechaBeastGalleryAsync(string userId, string mechaBeastId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarMechaBeastsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchMechaBeastsGalleryAsync(string userId, List<MechaBeasts> mechaBeasts);
    Task<MechaBeasts> GetMechaBeastCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateMechaBeastGalleryPowerAsync(string userId, string id);
    Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync(string userId);
}