using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsGalleryRepository
{
    Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMechaBeastsCountAsync(string search, string rare);
    Task InsertMechaBeastGalleryAsync(string userId, string Id, MechaBeasts MechaBeastFromDB);
    Task UpdateStatusMechaBeastGalleryAsync(string userId, string Id);
    Task UpdateStarMechaBeastGalleryAsync(string userId, string id, double star);
    Task UpdateMechaBeastGalleryPowerAsync(string userId, string id, MechaBeasts MechaBeastFromDB);
    Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync(string userId);
}