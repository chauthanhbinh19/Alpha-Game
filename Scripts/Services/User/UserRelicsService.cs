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

    
    

    public async Task<List<Relics>> GetUserRelicsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> list = await _userRelicsRepository.GetUserRelicsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRelicsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userRelicsRepository.GetUserRelicsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserRelicAsync(Relics relics, string userId)
    {
        return await _userRelicsRepository.InsertUserRelicAsync(relics, userId);
    }

    public async Task<bool> UpdateRelicLevelAsync(Relics relics, int cardLevel)
    {
        return await _userRelicsRepository.UpdateRelicLevelAsync(relics, cardLevel);
    }

    public async Task<bool> UpdateRelicBreakthroughAsync(Relics relics, int star, double quantity)
    {
        return await _userRelicsRepository.UpdateRelicBreakthroughAsync(relics, star, quantity);
    }

    public async Task<Relics> GetUserRelicByIdAsync(string user_id, string Id)
    {
        return await _userRelicsRepository.GetUserRelicByIdAsync(user_id, Id);
    }

    public async Task<Relics> SumPowerUserRelicsAsync()
    {
        return await _userRelicsRepository.SumPowerUserRelicsAsync();
    }
}
