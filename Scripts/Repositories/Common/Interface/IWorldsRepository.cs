using System.Collections.Generic;

public interface IWorldsRepository
{
    List<string> GetUniqueWorldId();
    List<Worlds> GetWorlds(string userId, int pageSize, int offset);
    int GetWorldsCount(string rare);
    List<Worlds> GetWorldsWithPrice(int pageSize, int offset);
    int GetWorldsWithPriceCount();
    Worlds GetWorldsById(string Id);
    Worlds SumPowerWorldsPercent();
}
