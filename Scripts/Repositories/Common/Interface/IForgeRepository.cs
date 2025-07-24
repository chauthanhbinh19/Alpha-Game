using System.Collections.Generic;

public interface IForgeRepository
{
    List<string> GetUniqueForgeTypes();
    List<string> GetUniqueForgeId();
    List<Forge> GetForge(string type, int pageSize, int offset, string rare);
    int GetForgeCount(string type, string rare);
    List<Forge> GetForgeWithPrice(string type, int pageSize, int offset);
    int GetForgeWithPriceCount(string type);
    Forge GetForgeById(string Id);
    Forge SumPowerForgePercent();
}
