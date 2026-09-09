using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMonstersGalleryService
{
    Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMonstersCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardMonsterGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardMonsterGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardMonstersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardMonsterGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardMonsterGalleryAsync(string userId, string cardMonsterId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardMonstersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardMonstersGalleryAsync(string userId, List<CardMonsters> cardMonsters);
    Task<CardMonsters> GetCardMonsterCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardMonsterGalleryPowerAsync(string userId, string Id);
    Task<CardMonsters> SumPowerCardMonstersGalleryAsync(string userId);
}