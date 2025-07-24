using System.Collections.Generic;

public interface ICardGeneralsRepository
{
    List<string> GetUniqueCardGeneralsTypes();
    List<string> GetUniqueCardGeneralsId();
    List<CardGenerals> GetCardGenerals(string type, int pageSize, int offset, string rare);
    int GetCardGeneralsCount(string type, string rare);
    List<CardGenerals> GetCardGeneralsRandom(string type, int pageSize);
    List<CardGenerals> GetAllCardGenerals(string type);
    CardGenerals GetCardGeneralsById(string Id);
    List<CardGenerals> GetCardGeneralsWithPrice(string type, int pageSize, int offset);
    int GetCardGeneralsWithPriceCount(string type);
}