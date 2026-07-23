using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsGalleryRepository
{
    Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetSpiritBeastsCountAsync(string search, string rare);
    Task InsertSpiritBeastGalleryAsync(string userId, string Id, SpiritBeasts SpiritBeastFromDB);
    Task UpdateStatusSpiritBeastGalleryAsync(string userId, string Id);
    Task UpdateStarSpiritBeastGalleryAsync(string userId, string id, double star);
    Task UpdateSpiritBeastGalleryPowerAsync(string userId, string id, SpiritBeasts SpiritBeastFromDB);
    Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync(string userId);
}