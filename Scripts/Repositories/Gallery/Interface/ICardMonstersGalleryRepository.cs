using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMonstersGalleryRepository
{
    Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetCardMonstersCountAsync(string type, string rare);
    Task InsertCardMonsterGalleryAsync(string Id, CardMonsters CardMonsterFromDB);
    Task UpdateStatusCardMonsterGalleryAsync(string Id);
    Task UpdateStarCardMonsterGalleryAsync(string Id, double star);
    Task UpdateCardMonsterGalleryPowerAsync(string Id, CardMonsters CardMonsterFromDB);
    Task<CardMonsters> SumPowerCardMonstersGalleryAsync();
}