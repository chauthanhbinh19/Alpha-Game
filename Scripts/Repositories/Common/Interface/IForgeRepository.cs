using System.Collections.Generic;

public interface IForgeRepository
{
    List<string> GetUniqueForgeTypes();
    List<Forge> GetForge(string type, int pageSize, int offset);
    int GetForgeCount(string type);
    List<Forge> GetForgeWithPrice(string type, int pageSize, int offset);
    int GetForgeWithPriceCount(string type);
    Forge GetForgeById(string Id);
    Forge SumPowerForgePercent();
}
