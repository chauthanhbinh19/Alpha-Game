using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMonstersService
{
    Task<List<string>> GetUniqueCardMonstersTypesAsync();
    Task<List<string>> GetUniqueCardMonstersIdAsync();
    Task<List<CardMonsters>> GetCardMonstersAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetCardMonstersCountAsync(string search, string type, string rare);
    Task<List<CardMonsters>> GetCardMonstersRandomAsync(string type, int pageSize);
    Task<List<CardMonsters>> GetAllCardMonstersAsync(string type);
    Task<CardMonsters> GetCardMonsterByIdAsync(string id);
    Task<List<CardMonsters>> GetCardMonstersWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardMonstersWithPriceCountAsync(string type);
}
