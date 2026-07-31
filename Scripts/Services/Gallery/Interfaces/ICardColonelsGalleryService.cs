using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardColonelsGalleryService
{
    Task<List<CardColonels>> GetCardColonelsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardColonelsCountAsync(string search, string type, string rare);
    Task<bool> InsertCardColonelGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardColonelGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardColonelsGalleryAsync(string userId);
    Task<bool> UpdateTempStarCardColonelGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardColonelGalleryAsync(string userId, string cardColonelId);
    Task<bool> UpdateBatchCurrentStarCardColonelsGalleryAsync(string userId);
    Task<bool> InsertBatchCardColonelsGalleryAsync(string userId, List<CardColonels> cardColonels);
    Task<CardColonels> GetCardColonelCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardColonelGalleryPowerAsync(string userId, string Id);
    Task<CardColonels> SumPowerCardColonelsGalleryAsync(string userId);
}