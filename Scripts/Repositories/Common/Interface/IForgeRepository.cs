using System.Collections.Generic;

public interface IForgeRepository
{
    List<string> GetUniqueForgeTypes();
    List<string> GetUniqueForgeId();
    List<Forges> GetForge(string type, int pageSize, int offset, string rare);
    int GetForgeCount(string type, string rare);
    List<Forges> GetForgeWithPrice(string type, int pageSize, int offset);
    int GetForgeWithPriceCount(string type);
    Forges GetForgeById(string Id);
    Forges SumPowerForgePercent();
}
