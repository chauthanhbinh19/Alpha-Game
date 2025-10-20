using System.Collections.Generic;

public interface ICardLifeService
{
    List<string> GetUniqueCardLifeTypes();
    List<string> GetUniqueCardLifeId();
    List<CardLives> GetCardLife(string type, int pageSize, int offset, string rare);
    int GetCardLifeCount(string type, string rare);
    List<CardLives> GetCardLifeWithPrice(string type, int pageSize, int offset);
    int GetCardLifeWithPriceCount(string type);
    CardLives GetCardLifeById(string Id);
    CardLives SumPowerCardLifePercent();
}