using System.Collections.Generic;

public interface IUserRobotsService
{
    Robots GetNewLevelPower(Robots c, double coefficient);
    Robots GetNewBreakthroughPower(Robots c, double coefficient);
    List<Robots> GetUserRobots(string user_id, int pageSize, int offset, string rare);
    int GetUserRobotsCount(string user_id, string rare);
    bool InsertUserRobots(Robots Robots);
    bool UpdateRobotsLevel(Robots Robots, int cardLevel);
    bool UpdateRobotsBreakthrough(Robots Robots, int star, double quantity);
    Robots GetUserRobotsById(string user_id, string Id);
    Robots SumPowerUserRobots();
}