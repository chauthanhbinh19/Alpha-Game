using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSoldiersGalleryService
{
    Task<List<CardSoldiers>> GetCardSoldiersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSoldiersCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardSoldierGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardSoldierGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardSoldiersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardSoldierGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardSoldierGalleryAsync(string userId, string cardSoldierId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardSoldiersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardSoldiersGalleryAsync(string userId, List<CardSoldiers> cardSoldiers);
    Task<CardSoldiers> GetCardSoldierCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardSoldierGalleryPowerAsync(string userId, string Id);
    Task<CardSoldiers> SumPowerCardSoldiersGalleryAsync(string userId);
}