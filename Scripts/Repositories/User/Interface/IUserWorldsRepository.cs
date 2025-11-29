using System.Collections.Generic;

public interface IUserWorldsRepository
{
    List<Worlds> GetUserWorlds(string user_id, int pageSize, int offset);
    int GetUserWorldsCount(string user_id, string rare);
    bool InsertUserWorlds(Worlds Worlds, string userId);
    bool UpdateWorldsLevel(Worlds Worlds, int WorldLevel);
    bool UpdateWorldsBreakthrough(Worlds Worlds, int star, double quantity);
    Worlds GetUserWorldsById(string user_id, string Id);
    Worlds SumPowerUserWorlds();
}