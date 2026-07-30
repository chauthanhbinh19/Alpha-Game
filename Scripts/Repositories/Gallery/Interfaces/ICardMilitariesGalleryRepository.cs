using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMilitariesGalleryRepository
{
    Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMilitariesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardMilitaries>> InsertCardMilitaryGalleryAsync(string userId, string Id, CardMilitaries CardMilitaryFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardMilitaryGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardMilitariesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCardMilitaryGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardMilitaryGalleryAsync(string userId, string cardMilitaryId);
    Task<InsertOrUpdateResult<List<(string CardMilitaryId, double CurrentStar)>>> UpdateBatchCurrentStarCardMilitariesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardMilitaries>>> InsertBatchCardMilitariesGalleryAsync(string userId, List<CardMilitaries> cardMilitaries);
    Task<CardMilitaries> GetCardMilitaryCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardMilitaryGalleryPowerAsync(string userId, string Id, CardMilitaries CardMilitaryFromDB);
    Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync(string userId);
}