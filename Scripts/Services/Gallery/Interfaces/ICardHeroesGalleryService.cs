using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardHeroesGalleryService
{ 
    Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardHeroesCountAsync(string search, string type, string rare);
    Task InsertCardHeroGalleryAsync(string userId, string Id);
    Task UpdateStatusCardHeroGalleryAsync(string userId, string Id);
    Task UpdateStarCardHeroGalleryAsync(string userId, string Id, double star);
    Task UpdateCardHeroGalleryPowerAsync(string userId, string Id);
    Task<CardHeroes> SumPowerCardHeroesGalleryAsync(string userId);
}