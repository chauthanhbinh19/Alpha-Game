using System.Collections.Generic;

public interface IUserTitlesService
{
    Architectures GetNewLevelPower(Architectures c, double coefficient);
    Architectures GetNewBreakthroughPower(Architectures c, double coefficient);
    List<Architectures> GetUserTitles(string user_id, int pageSize, int offset, string rare);
    int GetUserTitlesCount(string user_id, string rare);
    bool InsertUserTitles(Architectures titles);
    bool UpdateTitlesLevel(Architectures titles, int cardLevel);
    bool UpdateTitlesBreakthrough(Architectures titles, int star, double quantity);
    Architectures GetUserTitlesById(string user_id, string Id);
    Architectures SumPowerUserTitles();
}