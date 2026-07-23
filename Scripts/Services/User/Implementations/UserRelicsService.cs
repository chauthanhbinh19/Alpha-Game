using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRelicsService : IUserRelicsService
{
    private static UserRelicsService _instance;
    private readonly IUserRelicsRepository _userRelicsRepository;

    public UserRelicsService(IUserRelicsRepository userRelicsRepository)
    {
        _userRelicsRepository = userRelicsRepository;
    }

    public static UserRelicsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserRelicsService(new UserRelicsRepository());
        }
        return _instance;
    }




    public async Task<List<Relics>> GetUserRelicsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> list = await _userRelicsRepository.GetUserRelicsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRelicsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userRelicsRepository.GetUserRelicsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserRelicAsync(Relics relic, string userId)
    {
        return await _userRelicsRepository.InsertUserRelicAsync(relic, userId);
    }

    public async Task<bool> UpdateUserRelicLevelAsync(string userId, Relics relic)
    {
        return await _userRelicsRepository.UpdateUserRelicLevelAsync(userId, relic);
    }

    public async Task<bool> UpdateUserRelicStarAsync(string userId, Relics relic)
    {
        return await _userRelicsRepository.UpdateUserRelicStarAsync(userId, relic);
    }

    public async Task<bool> UpdateUserRelicBreakthroughAsync(string userId, Relics relic, int star, double quantity)
    {
        return await _userRelicsRepository.UpdateUserRelicBreakthroughAsync(userId, relic, star, quantity);
    }

    public async Task<Relics> GetUserRelicByIdAsync(string userId, string Id)
    {
        return await _userRelicsRepository.GetUserRelicByIdAsync(userId, Id);
    }

    public async Task<Relics> SumPowerUserRelicsAsync(string userId)
    {
        return await _userRelicsRepository.SumPowerUserRelicsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserRelicsBatchAsync(string userId, List<Relics> relics)
    {
        return await _userRelicsRepository.InsertOrUpdateUserRelicsBatchAsync(userId, relics);
    }
}
