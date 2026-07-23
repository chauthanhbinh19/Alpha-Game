using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTechnologiesService : IUserTechnologiesService
{
    private static UserTechnologiesService _instance;
    private readonly IUserTechnologiesRepository _userTechnologiesRepository;

    public UserTechnologiesService(IUserTechnologiesRepository userTechnologiesRepository)
    {
        _userTechnologiesRepository = userTechnologiesRepository;
    }

    public static UserTechnologiesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserTechnologiesService(new UserTechnologiesRepository());
        }
        return _instance;
    }

    public async Task<List<Technologies>> GetUserTechnologiesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _userTechnologiesRepository.GetUserTechnologiesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTechnologiesCountAsync(string userId, string search, string rare)
    {
        return await _userTechnologiesRepository.GetUserTechnologiesCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserTechnologyAsync(Technologies technology, string userId)
    {
        return await _userTechnologiesRepository.InsertUserTechnologyAsync(technology, userId);
    }

    public async Task<bool> UpdateUserTechnologyLevelAsync(string userId, Technologies technology)
    {
        return await _userTechnologiesRepository.UpdateUserTechnologyLevelAsync(userId, technology);
    }

    public async Task<bool> UpdateUserTechnologyStarAsync(string userId, Technologies technology)
    {
        return await _userTechnologiesRepository.UpdateUserTechnologyStarAsync(userId, technology);
    }

    public async Task<bool> UpdateUserTechnologyBreakthroughAsync(string userId, Technologies technology, int star, double quantity)
    {
        return await _userTechnologiesRepository.UpdateUserTechnologyBreakthroughAsync(userId, technology, star, quantity);
    }

    public async Task<Technologies> GetUserTechnologyByIdAsync(string userId, string Id)
    {
        return await _userTechnologiesRepository.GetUserTechnologyByIdAsync(userId, Id);
    }

    public async Task<Technologies> SumPowerUserTechnologiesAsync(string userId)
    {
        return await _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserTechnologiesBatchAsync(string userId, List<Technologies> technologies)
    {
        return await _userTechnologiesRepository.InsertOrUpdateUserTechnologiesBatchAsync(userId, technologies);
    }
}
