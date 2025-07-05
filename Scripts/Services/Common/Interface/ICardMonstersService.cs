using System.Collections.Generic;

public interface ICardMonstersService
{
    List<string> GetUniqueCardMonstersTypes();
    List<string> GetUniqueCardMonstersId();
    List<CardMonsters> GetCardMonsters(string type, int pageSize, int offset);
    int GetCardMonstersCount(string type);
    List<CardMonsters> GetCardMonstersRandom(string type, int pageSize);
    List<CardMonsters> GetAllCardMonsters(string type);
    CardMonsters GetCardMonstersById(string Id);
    List<CardMonsters> GetCardMonstersWithPrice(string type, int pageSize, int offset);
    int GetCardMonstersWithPriceCount(string type);
}
