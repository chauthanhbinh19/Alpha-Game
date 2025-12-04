using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsGalleryService
{
    Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetMechaBeastsCountAsync(string rare);
    Task InsertMechaBeastGalleryAsync(string Id);
    Task UpdateStatusMechaBeastGalleryAsync(string Id);
    Task UpdateStarMechaBeastGalleryAsync(string id, double star);
    Task UpdateMechaBeastGalleryPowerAsync(string id);
    Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync();
}