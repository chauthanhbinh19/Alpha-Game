using System.Collections.Generic;

public interface IUserTalismanRepository
{
    List<Talismans> GetUserTalisman(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserTalismanCount(string user_id, string type, string rare);
    bool InsertUserTalisman(Talismans talisman);
    bool UpdateTalismanLevel(Talismans talisman, int level);
    bool UpdateTalismanBreakthrough(Talismans talisman, int star, int quantity);
    Talismans GetUserTalismanById(string user_id, string id);
    Talismans SumPowerUserTalisman();

}