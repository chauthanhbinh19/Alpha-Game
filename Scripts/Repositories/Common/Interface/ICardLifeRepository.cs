using System.Collections.Generic;

public interface ICardLifeRepository
{
    List<string> GetUniqueCardLifeTypes();
    List<string> GetUniqueCardLifeId();
    List<CardLife> GetCardLife(string type, int pageSize, int offset, string rare);
    int GetCardLifeCount(string type, string rare);
    List<CardLife> GetCardLifeWithPrice(string type, int pageSize, int offset);
    int GetCardLifeWithPriceCount(string type);
    CardLife GetCardLifeById(string Id);
    CardLife SumPowerCardLifePercent();
}