using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSpellsGalleryService
{
    Task<List<CardSpells>> GetCardSpellsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSpellsCountAsync(string search, string type, string rare);
    Task InsertCardSpellGalleryAsync(string userId, string Id);
    Task UpdateStatusCardSpellGalleryAsync(string userId, string Id);
    Task UpdateStarCardSpellGalleryAsync(string userId, string Id, double star);
    Task UpdateCardSpellGalleryPowerAsync(string userId, string Id);
    Task<CardSpells> SumPowerCardSpellsGalleryAsync(string userId);
}