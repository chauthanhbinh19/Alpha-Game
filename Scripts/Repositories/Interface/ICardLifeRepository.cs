using System.Collections.Generic;

public interface ICardLifeRepository
{
    List<string> GetUniqueCardLifeTypes();
    List<CardLife> GetCardLife(string type, int pageSize, int offset);
    int GetCardLifeCount(string type);
    List<CardLife> GetCardLifeWithPrice(string type, int pageSize, int offset);
    int GetCardLifeWithPriceCount(string type);
    CardLife GetCardLifeById(string Id);
    CardLife SumPowerCardLifePercent();
}