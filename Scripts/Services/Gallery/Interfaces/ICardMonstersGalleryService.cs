using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMonstersGalleryService
{
    Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMonstersCountAsync(string search, string type, string rare);
    Task<bool> InsertCardMonsterGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardMonsterGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardMonstersGalleryAsync(string userId);
    Task<bool> UpdateStarCardMonsterGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardMonsterGalleryAsync(string userId, string cardMonsterId);
    Task<bool> UpdateBatchCurrentStarCardMonstersGalleryAsync(string userId);
    Task<bool> InsertBatchCardMonstersGalleryAsync(string userId, List<CardMonsters> cardMonsters);
    Task<CardMonsters> GetCardMonsterCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardMonsterGalleryPowerAsync(string userId, string Id);
    Task<CardMonsters> SumPowerCardMonstersGalleryAsync(string userId);
}