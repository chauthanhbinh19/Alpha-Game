using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSoldiersGalleryService
{
    Task<List<CardSoldiers>> GetCardSoldiersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardSoldiersCountAsync(string search, string type, string rare);
    Task<bool> InsertCardSoldierGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardSoldierGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardSoldiersGalleryAsync(string userId);
    Task<bool> UpdateTempStarCardSoldierGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardSoldierGalleryAsync(string userId, string cardSoldierId);
    Task<bool> UpdateBatchCurrentStarCardSoldiersGalleryAsync(string userId);
    Task<bool> InsertBatchCardSoldiersGalleryAsync(string userId, List<CardSoldiers> cardSoldiers);
    Task<CardSoldiers> GetCardSoldierCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardSoldierGalleryPowerAsync(string userId, string Id);
    Task<CardSoldiers> SumPowerCardSoldiersGalleryAsync(string userId);
}