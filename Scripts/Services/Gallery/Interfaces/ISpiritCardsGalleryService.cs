using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritCardsGalleryService
{
    Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSpiritCardsCountAsync(string search, string type, string rare);
    Task InsertSpiritCardGalleryAsync(string Id);
    Task UpdateStatusSpiritCardGalleryAsync(string Id);
    Task UpdateStarSpiritCardGalleryAsync(string Id, double star);
    Task UpdateSpiritCardGalleryPowerAsync(string Id);
    Task<SpiritCards> SumPowerSpiritCardsGalleryAsync();
}