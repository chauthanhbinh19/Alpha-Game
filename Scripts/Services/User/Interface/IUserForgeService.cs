using System.Collections.Generic;

public interface IUserForgeService
{
    Forge GetNewLevelPower(Forge c, double coefficient);
    Forge GetNewBreakthroughPower(Forge c, double coefficient);
    List<Forge> GetUserForge(string user_id, string type, int pageSize, int offset);
    int GetUserForgeCount(string user_id, string type);
    bool InsertUserForge(Forge Forge);
    bool UpdateForgeLevel(Forge Forge, int cardLevel);
    bool UpdateForgeBreakthrough(Forge Forge, int star, int quantity);
    Forge GetUserForgeById(string user_id, string Id);
    Forge SumPowerUserForge();
}