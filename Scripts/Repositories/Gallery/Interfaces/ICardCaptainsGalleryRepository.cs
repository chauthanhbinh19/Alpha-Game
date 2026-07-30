using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardCaptainsGalleryRepository
{
    Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardCaptainsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardCaptains>> InsertCardCaptainGalleryAsync(string userId, string Id, CardCaptains CardCaptainFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardCaptainGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardCaptainsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCardCaptainGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardCaptainGalleryAsync(string userId, string cardCaptainId);
    Task<InsertOrUpdateResult<List<(string CardCaptainId, double CurrentStar)>>> UpdateBatchCurrentStarCardCaptainsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardCaptains>>> InsertBatchCardCaptainsGalleryAsync(string userId, List<CardCaptains> cardCaptains);
    Task<CardCaptains> GetCardCaptainCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardCaptainGalleryPowerAsync(string userId, string Id, CardCaptains CardCaptainFromDB);
    Task<CardCaptains> SumPowerCardCaptainsGalleryAsync(string userId);
}