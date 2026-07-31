using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardGeneralsGalleryRepository
{
    Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardGeneralsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardGenerals>> InsertCardGeneralGalleryAsync(string userId, string Id, CardGenerals CardGeneralFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardGeneralGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardGeneralsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarCardGeneralGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardGeneralGalleryAsync(string userId, string cardGeneralId);
    Task<InsertOrUpdateResult<List<(string CardGeneralId, double CurrentStar)>>> UpdateBatchCurrentStarCardGeneralsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardGenerals>>> InsertBatchCardGeneralsGalleryAsync(string userId, List<CardGenerals> cardGenerals);
    Task<CardGenerals> GetCardGeneralCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardGeneralGalleryPowerAsync(string userId, string Id, CardGenerals CardGeneralFromDB);
    Task<CardGenerals> SumPowerCardGeneralsGalleryAsync(string userId);
}