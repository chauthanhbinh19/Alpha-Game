using System.Collections.Generic;

public interface IUserTitlesService
{
    Titles GetNewLevelPower(Titles c, double coefficient);
    Titles GetNewBreakthroughPower(Titles c, double coefficient);
    List<Titles> GetUserTitles(string user_id, int pageSize, int offset);
    int GetUserTitlesCount(string user_id);
    bool InsertUserTitles(Titles titles);
    bool UpdateTitlesLevel(Titles titles, int cardLevel);
    bool UpdateTitlesBreakthrough(Titles titles, int star, int quantity);
    Titles GetUserTitlesById(string user_id, string Id);
    Titles SumPowerUserTitles();
}