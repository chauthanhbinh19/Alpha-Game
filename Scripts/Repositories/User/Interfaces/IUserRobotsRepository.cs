using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRobotsRepository
{
    Task<List<Robots>> GetUserRobotsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserRobotsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserRobotAsync(Robots robot, string userId);
    Task<bool> InsertOrUpdateUserRobotsBatchAsync(List<Robots> robots);
    Task<bool> UpdateRobotLevelAsync(Robots robot, int level);
    Task<bool> UpdateRobotBreakthroughAsync(Robots robot, int star, double quantity);
    Task<Robots> GetUserRobotByIdAsync(string user_id, string Id);
    Task<Robots> SumPowerUserRobotsAsync();
}