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

    public async Task<List<Robots>> GetUserRobotsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Robots> list = await _userRobotsRepository.GetUserRobotsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRobotsCountAsync(string user_id, string search, string rare)
    {
        return await _userRobotsRepository.GetUserRobotsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserRobotAsync(Robots Robots, string userId)
    {
        return await _userRobotsRepository.InsertUserRobotAsync(Robots, userId);
    }

    public async Task<bool> UpdateRobotLevelAsync(Robots Robots, int cardLevel)
    {
        return await _userRobotsRepository.UpdateRobotLevelAsync(Robots, cardLevel);
    }

    public async Task<bool> UpdateRobotBreakthroughAsync(Robots Robots, int star, double quantity)
    {
        return await _userRobotsRepository.UpdateRobotBreakthroughAsync(Robots, star, quantity);
    }

    public async Task<Robots> GetUserRobotByIdAsync(string user_id, string Id)
    {
        return await _userRobotsRepository.GetUserRobotByIdAsync(user_id, Id);
    }

    public async Task<Robots> SumPowerUserRobotsAsync()
    {
        return await _userRobotsRepository.SumPowerUserRobotsAsync();
    }
}
