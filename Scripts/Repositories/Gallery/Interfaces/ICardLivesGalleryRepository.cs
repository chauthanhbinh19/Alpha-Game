using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardLivesGalleryRepository
{
    Task<List<CardLives>> GetCardLivesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardLivesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardLives>> InsertCardLifeGalleryAsync(string userId, string Id, CardLives CardLifeFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardLifeGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardLivesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCardLifeGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardLifeGalleryAsync(string userId, string cardLiveId);
    Task<InsertOrUpdateResult<List<(string CardLifeId, double CurrentStar)>>> UpdateBatchCurrentStarCardLivesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardLives>>> InsertBatchCardLivesGalleryAsync(string userId, List<CardLives> cardLives);
    Task<CardLives> GetCardLifeCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardLifeGalleryPowerAsync(string userId, string Id, CardLives CardLifeFromDB);
    Task<CardLives> SumPowerCardLivesGalleryAsync(string userId);
}