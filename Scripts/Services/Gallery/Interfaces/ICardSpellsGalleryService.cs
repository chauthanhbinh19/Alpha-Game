using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSpellsGalleryService
{
    Task<List<CardSpells>> GetCardSpellsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSpellsCountAsync(string search, string type, string rare);
    Task<bool> InsertCardSpellGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardSpellGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardSpellsGalleryAsync(string userId);
    Task<bool> UpdateStarCardSpellGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardSpellGalleryAsync(string userId, string cardSpellId);
    Task<bool> UpdateBatchCurrentStarCardSpellsGalleryAsync(string userId);
    Task<bool> InsertBatchCardSpellsGalleryAsync(string userId, List<CardSpells> cardSpells);
    Task<CardSpells> GetCardSpellCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardSpellGalleryPowerAsync(string userId, string Id);
    Task<CardSpells> SumPowerCardSpellsGalleryAsync(string userId);
}