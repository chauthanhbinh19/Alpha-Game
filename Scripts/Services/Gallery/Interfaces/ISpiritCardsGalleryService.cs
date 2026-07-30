using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritCardsGalleryService
{
    Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSpiritCardsCountAsync(string search, string type, string rare);
    Task<bool> InsertSpiritCardGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusSpiritCardGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusSpiritCardsGalleryAsync(string userId);
    Task<bool> UpdateStarSpiritCardGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarSpiritCardGalleryAsync(string userId, string spiritCardId);
    Task<bool> UpdateBatchCurrentStarSpiritCardsGalleryAsync(string userId);
    Task<bool> InsertBatchSpiritCardsGalleryAsync(string userId, List<SpiritCards> spiritCards);
    Task<SpiritCards> GetSpiritCardCollectionByIdAsync(string userId, string objectId);
    Task UpdateSpiritCardGalleryPowerAsync(string userId, string Id);
    Task<SpiritCards> SumPowerSpiritCardsGalleryAsync(string userId);
}