using System.Collections.Generic;

public interface IUserRobotsRepository
{
    List<Robots> GetUserRobots(string user_id, int pageSize, int offset, string rare);
    int GetUserRobotsCount(string user_id, string rare);
    bool InsertUserRobots(Robots Robots, string userId);
    bool UpdateRobotsLevel(Robots Robots, int cardLevel);
    bool UpdateRobotsBreakthrough(Robots Robots, int star, double quantity);
    Robots GetUserRobotsById(string user_id, string Id);
    Robots SumPowerUserRobots();
}