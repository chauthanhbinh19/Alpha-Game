using System.Collections.Generic;

public interface IUserTalismanRepository
{
    List<Talisman> GetUserTalisman(string user_id, string type, int pageSize, int offset);
    int GetUserTalismanCount(string user_id, string type);
    bool InsertUserTalisman(Talisman talisman);
    bool UpdateTalismanLevel(Talisman talisman, int level);
    bool UpdateTalismanBreakthrough(Talisman talisman, int star, int quantity);
    Talisman GetUserTalismanById(string user_id, string id);
    Talisman SumPowerUserTalisman();

}