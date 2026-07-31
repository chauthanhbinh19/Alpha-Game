using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardColonelsGalleryRepository
{
    Task<List<CardColonels>> GetCardColonelsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardColonelsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardColonels>> InsertCardColonelGalleryAsync(string userId, string Id, CardColonels CardColonelFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardColonelGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardColonelsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarCardColonelGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardColonelGalleryAsync(string userId, string cardColonelId);
    Task<InsertOrUpdateResult<List<(string CardColonelId, double CurrentStar)>>> UpdateBatchCurrentStarCardColonelsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardColonels>>> InsertBatchCardColonelsGalleryAsync(string userId, List<CardColonels> cardColonels);
    Task<CardColonels> GetCardColonelCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardColonelGalleryPowerAsync(string userId, string Id, CardColonels CardColonelFromDB);
    Task<CardColonels> SumPowerCardColonelsGalleryAsync(string userId);
}