using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBadgesService : IUserBadgesService
{
     private static UserBadgesService _instance;
    private readonly IUserBadgesRepository _userBadgesRepository;

    public UserBadgesService(IUserBadgesRepository userBadgesRepository)
    {
        _userBadgesRepository = userBadgesRepository;
    }

    public static UserBadgesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserBadgesService(new UserBadgesRepository());
        }
        return _instance;
    }

    public async Task<List<Badges>> GetUserBadgesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Badges> list = await _userBadgesRepository.GetUserBadgesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBadgesCountAsync(string user_id, string search, string rare)
    {
        return await _userBadgesRepository.GetUserBadgesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserBadgeAsync(Badges badge, string userId)
    {
        return await _userBadgesRepository.InsertUserBadgeAsync(badge, userId);
    }

    public async Task<bool> UpdateBadgeLevelAsync(Badges badge, int level)
    {
        return await _userBadgesRepository.UpdateBadgeLevelAsync(badge, level);
    }

    public async Task<bool> UpdateBadgeBreakthroughAsync(Badges badge, int star, double quantity)
    {
        return await _userBadgesRepository.UpdateBadgeBreakthroughAsync(badge, star, quantity);
    }

    public async Task<Badges> GetUserBadgeByIdAsync(string user_id, string Id)
    {
        return await _userBadgesRepository.GetUserBadgeByIdAsync(user_id, Id);
    }

    public async Task<Badges> SumPowerUserBadgesAsync()
    {
        return await _userBadgesRepository.SumPowerUserBadgesAsync();
    }

    public async Task<bool> InsertOrUpdateUserBadgesBatchAsync(List<Badges> badges)
    {
        return await _userBadgesRepository.InsertOrUpdateUserBadgesBatchAsync(badges);
    }
}
