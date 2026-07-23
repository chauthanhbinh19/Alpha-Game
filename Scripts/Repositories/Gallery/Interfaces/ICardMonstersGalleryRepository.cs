using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMonstersGalleryRepository
{
    Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMonstersCountAsync(string search, string type, string rare);
    Task InsertCardMonsterGalleryAsync(string userId, string Id, CardMonsters CardMonsterFromDB);
    Task UpdateStatusCardMonsterGalleryAsync(string userId, string Id);
    Task UpdateStarCardMonsterGalleryAsync(string userId, string Id, double star);
    Task UpdateCardMonsterGalleryPowerAsync(string userId, string Id, CardMonsters CardMonsterFromDB);
    Task<CardMonsters> SumPowerCardMonstersGalleryAsync(string userId);
}