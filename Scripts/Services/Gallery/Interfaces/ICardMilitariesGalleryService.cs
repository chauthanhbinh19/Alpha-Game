using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMilitariesGalleryService
{
    Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMilitariesCountAsync(string search, string type, string rare);
    Task<bool> InsertCardMilitaryGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardMilitaryGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardMilitariesGalleryAsync(string userId);
    Task<bool> UpdateTempStarCardMilitaryGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardMilitaryGalleryAsync(string userId, string cardMilitaryId);
    Task<bool> UpdateBatchCurrentStarCardMilitariesGalleryAsync(string userId);
    Task<bool> InsertBatchCardMilitariesGalleryAsync(string userId, List<CardMilitaries> cardMilitaries);
    Task<CardMilitaries> GetCardMilitaryCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardMilitaryGalleryPowerAsync(string userId, string Id);
    Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync(string userId);
}