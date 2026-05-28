using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardHeroesGalleryService
{ 
    Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardHeroesCountAsync(string search, string type, string rare);
    Task InsertCardHeroGalleryAsync(string Id);
    Task UpdateStatusCardHeroGalleryAsync(string Id);
    Task UpdateStarCardHeroGalleryAsync(string Id, double star);
    Task UpdateCardHeroGalleryPowerAsync(string Id);
    Task<CardHeroes> SumPowerCardHeroesGalleryAsync();
}