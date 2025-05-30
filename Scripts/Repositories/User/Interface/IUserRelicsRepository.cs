using System.Collections.Generic;

public interface IUserRelicsRepository
{
    List<Relics> GetUserRelics(string user_id, string type, int pageSize, int offset);
    int GetUserRelicsCount(string user_id, string type);
    bool InsertUserReclis(Relics relics);
    bool UpdateRelicsLevel(Relics relics, int cardLevel);
    bool UpdateRelicsBreakthrough(Relics relics, int star, int quantity);
    Relics GetUserRelicsById(string user_id, string id);
    Relics SumPowerUserRelics();
}