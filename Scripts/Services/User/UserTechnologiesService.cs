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

    public async Task<List<Technologies>> GetUserTechnologiesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _userTechnologiesRepository.GetUserTechnologiesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTechnologiesCountAsync(string user_id, string search, string rare)
    {
        return await _userTechnologiesRepository.GetUserTechnologiesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserTechnologyAsync(Technologies Technologies, string userId)
    {
        return await _userTechnologiesRepository.InsertUserTechnologyAsync(Technologies, userId);
    }

    public async Task<bool> UpdateTechnologyLevelAsync(Technologies Technologies, int cardLevel)
    {
        return await _userTechnologiesRepository.UpdateTechnologyLevelAsync(Technologies, cardLevel);
    }

    public async Task<bool> UpdateTechnologyBreakthroughAsync(Technologies Technologies, int star, double quantity)
    {
        return await _userTechnologiesRepository.UpdateTechnologyBreakthroughAsync(Technologies, star, quantity);
    }

    public async Task<Technologies> GetUserTechnologyByIdAsync(string user_id, string Id)
    {
        return await _userTechnologiesRepository.GetUserTechnologyByIdAsync(user_id, Id);
    }

    public async Task<Technologies> SumPowerUserTechnologiesAsync()
    {
        return await _userTechnologiesRepository.SumPowerUserTechnologiesAsync();
    }
}
