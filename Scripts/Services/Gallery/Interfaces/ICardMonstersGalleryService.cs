using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMonstersGalleryService
{
    Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMonstersCountAsync(string search, string type, string rare);
    Task InsertCardMonsterGalleryAsync(string Id);
    Task UpdateStatusCardMonsterGalleryAsync(string Id);
    Task UpdateStarCardMonsterGalleryAsync(string Id, double star);
    Task UpdateCardMonsterGalleryPowerAsync(string Id);
    Task<CardMonsters> SumPowerCardMonstersGalleryAsync();
}