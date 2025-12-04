using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsGalleryRepository
{
    Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetSpiritBeastsCountAsync(string rare);
    Task InsertSpiritBeastGalleryAsync(string Id, SpiritBeasts SpiritBeastFromDB);
    Task UpdateStatusSpiritBeastGalleryAsync(string Id);
    Task UpdateStarSpiritBeastGalleryAsync(string id, double star);
    Task UpdateSpiritBeastGalleryPowerAsync(string id, SpiritBeasts SpiritBeastFromDB);
    Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync();
}