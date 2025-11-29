using System.Collections.Generic;

public interface IBasesService
{
    List<string> GetUniqueBaseId();
    List<Bases> GetBases(string userId, int pageSize, int offset);
    int GetBasesCount(string rare);
    List<Bases> GetBasesWithPrice(int pageSize, int offset);
    int GetBasesWithPriceCount();
    Bases GetBasesById(string Id);
    Bases SumPowerBasesPercent();
}
