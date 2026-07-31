using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritCardsGalleryRepository
{
    Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSpiritCardsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<SpiritCards>> InsertSpiritCardGalleryAsync(string userId, string Id, SpiritCards SpiritCardFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusSpiritCardGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSpiritCardsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarSpiritCardGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarSpiritCardGalleryAsync(string userId, string spiritCardId);
    Task<InsertOrUpdateResult<List<(string SpiritCardId, double CurrentStar)>>> UpdateBatchCurrentStarSpiritCardsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<SpiritCards>>> InsertBatchSpiritCardsGalleryAsync(string userId, List<SpiritCards> spiritCards);
    Task<SpiritCards> GetSpiritCardCollectionByIdAsync(string userId, string objectId);
    Task UpdateSpiritCardGalleryPowerAsync(string userId, string Id, SpiritCards SpiritCardFromDB);
    Task<SpiritCards> SumPowerSpiritCardsGalleryAsync(string userId);
}