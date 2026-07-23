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

    public async Task<List<Badges>> GetUserBadgesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Badges> list = await _userBadgesRepository.GetUserBadgesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBadgesCountAsync(string userId, string search, string rare)
    {
        return await _userBadgesRepository.GetUserBadgesCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserBadgeAsync(Badges badge, string userId)
    {
        return await _userBadgesRepository.InsertUserBadgeAsync(badge, userId);
    }

    public async Task<bool> UpdateUserBadgeLevelAsync(string userId, Badges badge)
    {
        return await _userBadgesRepository.UpdateUserBadgeLevelAsync(userId, badge);
    }

    public async Task<bool> UpdateUserBadgeStarAsync(string userId, Badges badge)
    {
        return await _userBadgesRepository.UpdateUserBadgeStarAsync(userId, badge);
    }

    public async Task<bool> UpdateUserBadgeBreakthroughAsync(string userId, Badges badge, int star, double quantity)
    {
        return await _userBadgesRepository.UpdateUserBadgeBreakthroughAsync(userId, badge, star, quantity);
    }

    public async Task<Badges> GetUserBadgeByIdAsync(string userId, string Id)
    {
        return await _userBadgesRepository.GetUserBadgeByIdAsync(userId, Id);
    }

    public async Task<Badges> SumPowerUserBadgesAsync(string userId)
    {
        return await _userBadgesRepository.SumPowerUserBadgesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserBadgesBatchAsync(string userId, List<Badges> badges)
    {
        return await _userBadgesRepository.InsertOrUpdateUserBadgesBatchAsync(userId, badges);
    }
}
