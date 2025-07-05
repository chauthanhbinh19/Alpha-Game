using System.Collections.Generic;

public interface ICardGeneralsRepository
{
    List<string> GetUniqueCardGeneralsTypes();
    List<string> GetUniqueCardGeneralsId();
    List<CardGenerals> GetCardGenerals(string type, int pageSize, int offset);
    int GetCardGeneralsCount(string type);
    List<CardGenerals> GetCardGeneralsRandom(string type, int pageSize);
    List<CardGenerals> GetAllCardGenerals(string type);
    CardGenerals GetCardGeneralsById(string Id);
    List<CardGenerals> GetCardGeneralsWithPrice(string type, int pageSize, int offset);
    int GetCardGeneralsWithPriceCount(string type);
}