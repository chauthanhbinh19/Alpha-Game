using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSoldiersGalleryRepository
{
    Task<List<CardSoldiers>> GetCardSoldiersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSoldiersCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardSoldiers>> InsertCardSoldierGalleryAsync(string userId, string Id, CardSoldiers CardSoldierFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardSoldierGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardSoldiersGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCardSoldierGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardSoldierGalleryAsync(string userId, string cardSoldierId);
    Task<InsertOrUpdateResult<List<(string CardSoldierId, double CurrentStar)>>> UpdateBatchCurrentStarCardSoldiersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardSoldiers>>> InsertBatchCardSoldiersGalleryAsync(string userId, List<CardSoldiers> cardSoldiers);
    Task<CardSoldiers> GetCardSoldierCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardSoldierGalleryPowerAsync(string userId, string Id, CardSoldiers CardSoldierFromDB);
    Task<CardSoldiers> SumPowerCardSoldiersGalleryAsync(string userId);
}