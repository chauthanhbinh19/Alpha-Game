using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardHeroesGalleryRepository
{ 
    Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetCardHeroesCountAsync(string type, string rare);
    Task InsertCardHeroGalleryAsync(string Id, CardHeroes CardHeroFromDB);
    Task UpdateStatusCardHeroGalleryAsync(string Id);
    Task UpdateStarCardHeroGalleryAsync(string Id, double star);
    Task UpdateCardHeroGalleryPowerAsync(string Id, CardHeroes CardHeroFromDB);
    Task<CardHeroes> SumPowerCardHeroesGalleryAsync();
}