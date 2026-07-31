using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsGalleryRepository
{
    Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMechaBeastsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<MechaBeasts>> InsertMechaBeastGalleryAsync(string userId, string Id, MechaBeasts MechaBeastFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusMechaBeastGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMechaBeastsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarMechaBeastGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarMechaBeastGalleryAsync(string userId, string mechaBeastId);
    Task<InsertOrUpdateResult<List<(string MechaBeastId, double CurrentStar)>>> UpdateBatchCurrentStarMechaBeastsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<MechaBeasts>>> InsertBatchMechaBeastsGalleryAsync(string userId, List<MechaBeasts> mechaBeasts);
    Task<MechaBeasts> GetMechaBeastCollectionByIdAsync(string userId, string objectId);
    Task UpdateMechaBeastGalleryPowerAsync(string userId, string id, MechaBeasts MechaBeastFromDB);
    Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync(string userId);
}