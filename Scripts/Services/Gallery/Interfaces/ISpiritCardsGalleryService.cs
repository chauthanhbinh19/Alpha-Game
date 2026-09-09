using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritCardsGalleryService
{
    Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSpiritCardsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertSpiritCardGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusSpiritCardGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSpiritCardsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarSpiritCardGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarSpiritCardGalleryAsync(string userId, string spiritCardId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarSpiritCardsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchSpiritCardsGalleryAsync(string userId, List<SpiritCards> spiritCards);
    Task<SpiritCards> GetSpiritCardCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateSpiritCardGalleryPowerAsync(string userId, string Id);
    Task<SpiritCards> SumPowerSpiritCardsGalleryAsync(string userId);
}