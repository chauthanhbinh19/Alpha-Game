using System.Collections.Generic;

public interface IUserRelicsService
{
    Relics GetNewLevelPower(Relics c, double coefficient);
    Relics GetNewBreakthroughPower(Relics c, double coefficient);
    List<Relics> GetUserRelics(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserRelicsCount(string user_id, string type, string rare);
    bool InsertUserReclis(Relics relics, string userId);
    bool UpdateRelicsLevel(Relics relics, int cardLevel);
    bool UpdateRelicsBreakthrough(Relics relics, int star, double quantity);
    Relics GetUserRelicsById(string user_id, string Id);
    Relics SumPowerUserRelics();
}