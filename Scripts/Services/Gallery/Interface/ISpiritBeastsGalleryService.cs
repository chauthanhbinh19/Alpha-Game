using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsGalleryService
{
    Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetSpiritBeastsCountAsync(string rare);
    Task InsertSpiritBeastGalleryAsync(string Id);
    Task UpdateStatusSpiritBeastGalleryAsync(string Id);
    Task UpdateStarSpiritBeastGalleryAsync(string id, double star);
    Task UpdateSpiritBeastGalleryPowerAsync(string id);
    Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync();
}