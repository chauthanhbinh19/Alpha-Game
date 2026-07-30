using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardLivesGalleryService
{
    Task<List<CardLives>> GetCardLivesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardLivesCountAsync(string search, string type, string rare);
    Task<bool> InsertCardLifeGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardLifeGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardLivesGalleryAsync(string userId);
    Task<bool> UpdateStarCardLifeGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardLifeGalleryAsync(string userId, string cardLiveId);
    Task<bool> UpdateBatchCurrentStarCardLivesGalleryAsync(string userId);
    Task<bool> InsertBatchCardLivesGalleryAsync(string userId, List<CardLives> cardLives);
    Task<CardLives> GetCardLifeCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardLifeGalleryPowerAsync(string userId, string Id);
    Task<CardLives> SumPowerCardLivesGalleryAsync(string userId);
}