using System.Collections.Generic;

public interface ICardColonelsService
{
    List<string> GetUniqueCardColonelsTypes();
    List<string> GetUniqueCardColonelsId();
    List<CardColonels> GetCardColonels(string type, int pageSize, int offset);
    int GetCardColonelsCount(string type);
    List<CardColonels> GetCardColonelsRandom(string type, int pageSize);
    List<CardColonels> GetAllCardColonels(string type);
    CardColonels GetCardColonelsById(string Id);
    List<CardColonels> GetCardColonelsWithPrice(string type, int pageSize, int offset);
    int GetCardColonelsWithPriceCount(string type);
}