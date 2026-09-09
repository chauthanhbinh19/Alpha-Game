using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMilitariesGalleryService
{
    Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMilitariesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardMilitaryGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardMilitaryGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardMilitariesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardMilitaryGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardMilitaryGalleryAsync(string userId, string cardMilitaryId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardMilitariesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardMilitariesGalleryAsync(string userId, List<CardMilitaries> cardMilitaries);
    Task<CardMilitaries> GetCardMilitaryCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardMilitaryGalleryPowerAsync(string userId, string Id);
    Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync(string userId);
}