using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardColonelsGalleryService
{
    Task<List<CardColonels>> GetCardColonelsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardColonelsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardColonelGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardColonelGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardColonelsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardColonelGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardColonelGalleryAsync(string userId, string cardColonelId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardColonelsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardColonelsGalleryAsync(string userId, List<CardColonels> cardColonels);
    Task<CardColonels> GetCardColonelCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardColonelGalleryPowerAsync(string userId, string Id);
    Task<CardColonels> SumPowerCardColonelsGalleryAsync(string userId);
}