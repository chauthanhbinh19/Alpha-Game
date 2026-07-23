using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsGalleryService
{
    Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMechaBeastsCountAsync(string search, string rare);
    Task InsertMechaBeastGalleryAsync(string userId, string Id);
    Task UpdateStatusMechaBeastGalleryAsync(string userId, string Id);
    Task UpdateStarMechaBeastGalleryAsync(string userId, string id, double star);
    Task UpdateMechaBeastGalleryPowerAsync(string userId, string id);
    Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync(string userId);
}