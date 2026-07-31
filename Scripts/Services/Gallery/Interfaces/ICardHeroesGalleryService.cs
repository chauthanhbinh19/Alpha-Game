using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardHeroesGalleryService
{
    Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardHeroesCountAsync(string search, string type, string rare);
    Task<bool> InsertCardHeroGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardHeroGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardHeroesGalleryAsync(string userId);
    Task<bool> UpdateTempStarCardHeroGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardHeroGalleryAsync(string userId, string cardHeroId);
    Task<bool> UpdateBatchCurrentStarCardHeroesGalleryAsync(string userId);
    Task<bool> InsertBatchCardHeroesGalleryAsync(string userId, List<CardHeroes> cardHeroes);
    Task<CardHeroes> GetCardHeroCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardHeroGalleryPowerAsync(string userId, string Id);
    Task<CardHeroes> SumPowerCardHeroesGalleryAsync(string userId);
}