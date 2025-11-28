using System.Collections.Generic;

public interface IUserTitlesService
{
    Titles GetNewLevelPower(Titles c, double coefficient);
    Titles GetNewBreakthroughPower(Titles c, double coefficient);
    List<Titles> GetUserTitles(string user_id, int pageSize, int offset, string rare);
    int GetUserTitlesCount(string user_id, string rare);
    bool InsertUserTitles(Titles titles, string userId);
    bool UpdateTitlesLevel(Titles titles, int cardLevel);
    bool UpdateTitlesBreakthrough(Titles titles, int star, double quantity);
    Titles GetUserTitlesById(string user_id, string Id);
    Titles SumPowerUserTitles();
}