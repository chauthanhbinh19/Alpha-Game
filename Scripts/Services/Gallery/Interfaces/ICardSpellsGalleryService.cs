using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSpellsGalleryService
{
    Task<List<CardSpells>> GetCardSpellsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSpellsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardSpellGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardSpellGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardSpellsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardSpellGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardSpellGalleryAsync(string userId, string cardSpellId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardSpellsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardSpellsGalleryAsync(string userId, List<CardSpells> cardSpells);
    Task<CardSpells> GetCardSpellCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardSpellGalleryPowerAsync(string userId, string Id);
    Task<CardSpells> SumPowerCardSpellsGalleryAsync(string userId);
}