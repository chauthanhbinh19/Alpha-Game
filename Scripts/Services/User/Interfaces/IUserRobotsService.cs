using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRobotsService
{
    Task<List<Robots>> GetUserRobotsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserRobotsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotAsync(string userId, Robots robot);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotsBatchAsync(string userId, List<Robots> robots);
    Task<bool> UpdateUserRobotLevelAsync(string userId, Robots robot);
    Task<bool> UpdateUserRobotStarAsync(string userId, Robots robot);  
    Task<Robots> GetUserRobotByIdAsync(string userId, string Id);
    Task<Robots> SumPowerUserRobotsAsync(string userId);
}