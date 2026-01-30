using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsGalleryRepository
{
    Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetMechaBeastsCountAsync(string search, string rare);
    Task InsertMechaBeastGalleryAsync(string Id, MechaBeasts MechaBeastFromDB);
    Task UpdateStatusMechaBeastGalleryAsync(string Id);
    Task UpdateStarMechaBeastGalleryAsync(string id, double star);
    Task UpdateMechaBeastGalleryPowerAsync(string id, MechaBeasts MechaBeastFromDB);
    Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync();
}