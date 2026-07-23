using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritCardsGalleryService
{
    Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSpiritCardsCountAsync(string search, string type, string rare);
    Task InsertSpiritCardGalleryAsync(string userId, string Id);
    Task UpdateStatusSpiritCardGalleryAsync(string userId, string Id);
    Task UpdateStarSpiritCardGalleryAsync(string userId, string Id, double star);
    Task UpdateSpiritCardGalleryPowerAsync(string userId, string Id);
    Task<SpiritCards> SumPowerSpiritCardsGalleryAsync(string userId);
}