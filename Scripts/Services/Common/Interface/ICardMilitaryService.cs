using System.Collections.Generic;

public interface ICardMilitaryService
{
    List<string> GetUniqueCardMilitaryTypes();
    List<string> GetUniqueCardMilitaryId();
    List<CardMilitaries> GetCardMilitary(string type, int pageSize, int offset, string rare);
    int GetCardMilitaryCount(string type, string rare);
    List<CardMilitaries> GetCardMilitaryRandom(string type, int pageSize);
    List<CardMilitaries> GetAllCardMilitary(string type);
    CardMilitaries GetCardMilitaryById(string Id);
    List<CardMilitaries> GetCardMilitaryWithPrice(string type, int pageSize, int offset);
    int GetCardMilitaryWithPriceCount(string type);
}