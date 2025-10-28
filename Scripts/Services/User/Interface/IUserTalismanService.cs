using System.Collections.Generic;

public interface IUserTalismanService
{
    Talismans GetNewLevelPower(Talismans c, double coefficient);
    Talismans GetNewBreakthroughPower(Talismans c, double coefficient);
    List<Talismans> GetUserTalisman(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserTalismanCount(string user_id, string type, string rare);
    bool InsertUserTalisman(Talismans talisman);
    bool UpdateTalismanLevel(Talismans talisman, int level);
    bool UpdateTalismanBreakthrough(Talismans talisman, int star, double quantity);
    Talismans GetUserTalismanById(string user_id, string Id);
    Talismans SumPowerUserTalisman();

}