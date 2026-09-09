using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardHeroesGalleryService
{
    Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardHeroesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardHeroGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardHeroGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardHeroesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardHeroGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardHeroGalleryAsync(string userId, string cardHeroId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardHeroesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardHeroesGalleryAsync(string userId, List<CardHeroes> cardHeroes);
    Task<CardHeroes> GetCardHeroCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardHeroGalleryPowerAsync(string userId, string Id);
    Task<CardHeroes> SumPowerCardHeroesGalleryAsync(string userId);
}