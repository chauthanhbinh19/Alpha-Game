using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSpellsGalleryRepository
{
    Task<List<CardSpells>> GetCardSpellsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSpellsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardSpells>> InsertCardSpellGalleryAsync(string userId, string Id, CardSpells CardSpellFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardSpellGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardSpellsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCardSpellGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardSpellGalleryAsync(string userId, string cardSpellId);
    Task<InsertOrUpdateResult<List<(string CardSpellId, double CurrentStar)>>> UpdateBatchCurrentStarCardSpellsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardSpells>>> InsertBatchCardSpellsGalleryAsync(string userId, List<CardSpells> cardSpells);
    Task<CardSpells> GetCardSpellCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardSpellGalleryPowerAsync(string userId, string Id, CardSpells CardSpellFromDB);
    Task<CardSpells> SumPowerCardSpellsGalleryAsync(string userId);
}