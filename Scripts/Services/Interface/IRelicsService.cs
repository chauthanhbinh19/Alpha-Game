using System.Collections.Generic;
public interface IRelicsService
{
    List<string> GetUniqueRelicsTypes();
    List<Relics> GetRelics(string type, int pageSize, int offset);
    int GetRelicsCount(string type);
    List<Relics> GetRelicsWithPrice(string type, int pageSize, int offset);
    int GetRelicsWithPriceCount(string type);
    Relics GetRelicsById(string Id);
    Relics SumPowerRelicsPercent();
}
