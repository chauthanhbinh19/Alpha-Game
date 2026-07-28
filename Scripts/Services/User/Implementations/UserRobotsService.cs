using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRobotsService : IUserRobotsService
{
    private static UserRobotsService _instance;
    private readonly IUserRobotsRepository _userRobotsRepository;

    public UserRobotsService(IUserRobotsRepository userRobotsRepository)
    {
        _userRobotsRepository = userRobotsRepository;
    }

    public static UserRobotsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserRobotsService(new UserRobotsRepository());
        }
        return _instance;
    }

    public async Task<List<Robots>> GetUserRobotsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Robots> list = await _userRobotsRepository.GetUserRobotsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRobotsCountAsync(string userId, string search, string rare)
    {
        return await _userRobotsRepository.GetUserRobotsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserRobotAsync(Robots robot, string userId)
    {
        var result = await _userRobotsRepository.InsertUserRobotAsync(robot, userId);
        if (result)
        {
            await RobotsGalleryService.Create().InsertRobotGalleryAsync(userId, robot.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserRobotLevelAsync(string userId, Robots robot)
    {
        return await _userRobotsRepository.UpdateUserRobotLevelAsync(userId, robot);
    }

    public async Task<bool> UpdateUserRobotStarAsync(string userId, Robots robot)
    {
        var result = await _userRobotsRepository.UpdateUserRobotStarAsync(userId, robot);
        if (result)
        {
            await RobotsGalleryService.Create().UpdateStarRobotGalleryAsync(userId, robot.Id, robot.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserRobotBreakthroughAsync(string userId, Robots robot, int star, double quantity)
    {
        return await _userRobotsRepository.UpdateUserRobotBreakthroughAsync(userId, robot, star, quantity);
    }

    public async Task<Robots> GetUserRobotByIdAsync(string userId, string Id)
    {
        return await _userRobotsRepository.GetUserRobotByIdAsync(userId, Id);
    }

    public async Task<Robots> SumPowerUserRobotsAsync(string userId)
    {
        return await _userRobotsRepository.SumPowerUserRobotsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserRobotsBatchAsync(string userId, List<Robots> robots)
    {
        return await _userRobotsRepository.InsertOrUpdateUserRobotsBatchAsync(userId, robots);
    }
}
