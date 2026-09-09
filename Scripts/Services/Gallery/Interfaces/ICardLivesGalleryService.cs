using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardLivesGalleryService
{
    Task<List<CardLives>> GetCardLivesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardLivesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardLifeGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardLifeGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardLivesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardLifeGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardLifeGalleryAsync(string userId, string cardLiveId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardLivesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardLivesGalleryAsync(string userId, List<CardLives> cardLives);
    Task<CardLives> GetCardLifeCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardLifeGalleryPowerAsync(string userId, string Id);
    Task<CardLives> SumPowerCardLivesGalleryAsync(string userId);
}