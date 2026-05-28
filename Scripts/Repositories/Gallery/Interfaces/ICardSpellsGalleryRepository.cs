using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSpellsGalleryRepository
{
    Task<List<CardSpells>> GetCardSpellsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSpellsCountAsync(string search, string type, string rare);
    Task InsertCardSpellGalleryAsync(string Id, CardSpells CardSpellFromDB);
    Task UpdateStatusCardSpellGalleryAsync(string Id);
    Task UpdateStarCardSpellGalleryAsync(string Id, double star);
    Task UpdateCardSpellGalleryPowerAsync(string Id, CardSpells CardSpellFromDB);
    Task<CardSpells> SumPowerCardSpellsGalleryAsync();
}