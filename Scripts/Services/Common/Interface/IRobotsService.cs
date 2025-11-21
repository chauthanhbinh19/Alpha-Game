using System.Collections.Generic;

public interface IRobotsService
{
    List<string> GetUniqueRobotId();
    List<Robots> GetRobots(int pageSize, int offset, string rare);
    int GetRobotsCount(string rare);
    List<Robots> GetRobotsWithPrice(int pageSize, int offset);
    int GetRobotsWithPriceCount();
    Robots GetRobotsById(string Id);
    Robots SumPowerRobotsPercent();
}
