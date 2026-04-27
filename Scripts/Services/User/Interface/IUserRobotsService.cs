using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRobotsService
{
    Task<List<Robots>> GetUserRobotsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserRobotsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserRobotAsync(Robots Robots, string userId);
    Task<bool> UpdateRobotLevelAsync(Robots Robots, int RobotLevel);
    Task<bool> UpdateRobotBreakthroughAsync(Robots Robots, int star, double quantity);
    Task<Robots> GetUserRobotByIdAsync(string user_id, string Id);
    Task<Robots> SumPowerUserRobotsAsync();
}