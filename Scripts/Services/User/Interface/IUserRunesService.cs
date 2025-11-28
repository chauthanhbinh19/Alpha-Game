using System.Collections.Generic;

public interface IUserRunesService
{
    Runes GetNewLevelPower(Runes c, double coefficient);
    Runes GetNewBreakthroughPower(Runes c, double coefficient);
    List<Runes> GetUserRunes(string user_id, int pageSize, int offset, string rare);
    int GetUserRunesCount(string user_id, string rare);
    bool InsertUserRunes(Runes Runes, string userId);
    bool UpdateRunesLevel(Runes Runes, int cardLevel);
    bool UpdateRunesBreakthrough(Runes Runes, int star, double quantity);
    Runes GetUserRunesById(string user_id, string Id);
    Runes SumPowerUserRunes();
}