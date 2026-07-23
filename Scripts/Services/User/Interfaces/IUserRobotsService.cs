using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRobotsService
{
    Task<List<Robots>> GetUserRobotsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserRobotsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserRobotAsync(Robots robot, string userId);
    Task<bool> InsertOrUpdateUserRobotsBatchAsync(string userId, List<Robots> robots);
    Task<bool> UpdateUserRobotLevelAsync(string userId, Robots robot);
    Task<bool> UpdateUserRobotStarAsync(string userId, Robots robot);  
    Task<bool> UpdateUserRobotBreakthroughAsync(string userId, Robots robot, int star, double quantity);
    Task<Robots> GetUserRobotByIdAsync(string userId, string Id);
    Task<Robots> SumPowerUserRobotsAsync(string userId);
}