using System.Collections.Generic;

public interface IUserForgeService
{
    Forges GetNewLevelPower(Forges c, double coefficient);
    Forges GetNewBreakthroughPower(Forges c, double coefficient);
    List<Forges> GetUserForge(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserForgeCount(string user_id, string type, string rare);
    bool InsertUserForge(Forges Forge, string userId);
    bool UpdateForgeLevel(Forges Forge, int cardLevel);
    bool UpdateForgeBreakthrough(Forges Forge, int star, double quantity);
    Forges GetUserForgeById(string user_id, string Id);
    Forges SumPowerUserForge();
}