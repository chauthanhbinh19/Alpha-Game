using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsGalleryService
{
    Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMechaBeastsCountAsync(string search, string rare);
    Task<bool> InsertMechaBeastGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusMechaBeastGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusMechaBeastsGalleryAsync(string userId);
    Task<bool> UpdateTempStarMechaBeastGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarMechaBeastGalleryAsync(string userId, string mechaBeastId);
    Task<bool> UpdateBatchCurrentStarMechaBeastsGalleryAsync(string userId);
    Task<bool> InsertBatchMechaBeastsGalleryAsync(string userId, List<MechaBeasts> mechaBeasts);
    Task<MechaBeasts> GetMechaBeastCollectionByIdAsync(string userId, string objectId);
    Task UpdateMechaBeastGalleryPowerAsync(string userId, string id);
    Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync(string userId);
}