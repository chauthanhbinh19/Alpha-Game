using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardGeneralsGalleryService
{
    Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardGeneralsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardGeneralGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardGeneralGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardGeneralsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardGeneralGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardGeneralGalleryAsync(string userId, string cardGeneralId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardGeneralsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardGeneralsGalleryAsync(string userId, List<CardGenerals> cardGenerals);
    Task<CardGenerals> GetCardGeneralCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardGeneralGalleryPowerAsync(string userId, string Id);
    Task<CardGenerals> SumPowerCardGeneralsGalleryAsync(string userId);
}