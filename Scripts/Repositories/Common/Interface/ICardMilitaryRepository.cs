using System.Collections.Generic;

public interface ICardMilitaryRepository
{
    List<string> GetUniqueCardMilitaryTypes();
    List<string> GetUniqueCardMilitaryId();
    List<CardMilitary> GetCardMilitary(string type, int pageSize, int offset, string rare);
    int GetCardMilitaryCount(string type, string rare);
    List<CardMilitary> GetCardMilitaryRandom(string type, int pageSize);
    List<CardMilitary> GetAllCardMilitary(string type);
    CardMilitary GetCardMilitaryById(string Id);
    List<CardMilitary> GetCardMilitaryWithPrice(string type, int pageSize, int offset);
    int GetCardMilitaryWithPriceCount(string type);
}