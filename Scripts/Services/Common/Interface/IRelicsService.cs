using System.Collections.Generic;
public interface IRelicsService
{
    List<string> GetUniqueRelicsTypes();
    List<string> GetUniqueRelicsId();
    List<Relics> GetRelics(string type, int pageSize, int offset, string rare);
    int GetRelicsCount(string type, string rare);
    List<Relics> GetRelicsWithPrice(string type, int pageSize, int offset);
    int GetRelicsWithPriceCount(string type);
    Relics GetRelicsById(string Id);
    Relics SumPowerRelicsPercent();
}
