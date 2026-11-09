using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRobotsRepository
{
    Task<List<Robots>> GetUserRobotsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserRobotsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Robots>> InsertOrUpdateUserRobotAsync(string userId, Robots robot);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Robots>>> InsertOrUpdateUserRobotsBatchAsync(string userId, List<Robots> robots);
    Task<InsertOrUpdateResult<bool>> UpdateUserRobotLevelAsync(string userId, Robots robot, List<RecipeItemDetailDTO> totalDeductions);
    Task<InsertOrUpdateResult<bool>> UpdateUserRobotStarAsync(string userId, Robots robot);
    Task<InsertOrUpdateResult<bool>> UpdateUserRobotQuantityAsync(string userId, Robots robot, List<RecipeItemDetailDTO> totalDeductions);
    Task<Robots> GetUserRobotByIdAsync(string userId, string Id);
    Task<Robots> SumPowerUserRobotsAsync(string userId);
}