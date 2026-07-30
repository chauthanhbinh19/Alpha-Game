using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardHeroesGalleryRepository
{
    Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardHeroesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardHeroes>> InsertCardHeroGalleryAsync(string userId, string Id, CardHeroes CardHeroFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardHeroGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardHeroesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCardHeroGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardHeroGalleryAsync(string userId, string cardHeroId);
    Task<InsertOrUpdateResult<List<(string CardHeroId, double CurrentStar)>>> UpdateBatchCurrentStarCardHeroesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardHeroes>>> InsertBatchCardHeroesGalleryAsync(string userId, List<CardHeroes> cardHeroes);
    Task<CardHeroes> GetCardHeroCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardHeroGalleryPowerAsync(string userId, string Id, CardHeroes CardHeroFromDB);
    Task<CardHeroes> SumPowerCardHeroesGalleryAsync(string userId);
}