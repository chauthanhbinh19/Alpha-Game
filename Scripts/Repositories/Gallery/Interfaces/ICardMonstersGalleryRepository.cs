using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMonstersGalleryRepository
{
    Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMonstersCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardMonsters>> InsertCardMonsterGalleryAsync(string userId, string Id, CardMonsters CardMonsterFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardMonsterGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardMonstersGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarCardMonsterGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardMonsterGalleryAsync(string userId, string cardMonsterId);
    Task<InsertOrUpdateResult<List<(string CardMonsterId, double CurrentStar)>>> UpdateBatchCurrentStarCardMonstersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardMonsters>>> InsertBatchCardMonstersGalleryAsync(string userId, List<CardMonsters> cardMonsters);
    Task<CardMonsters> GetCardMonsterCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardMonsterGalleryPowerAsync(string userId, string Id, CardMonsters CardMonsterFromDB);
    Task<CardMonsters> SumPowerCardMonstersGalleryAsync(string userId);
}