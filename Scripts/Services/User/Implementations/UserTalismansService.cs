using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTalismansService : IUserTalismansService
{
    private static UserTalismansService _instance;
    private readonly IUserTalismansRepository _userTalismansRepository;

    public UserTalismansService(IUserTalismansRepository userTalismansRepository)
    {
        _userTalismansRepository = userTalismansRepository;
    }

    public static UserTalismansService Create()
    {
        if (_instance == null)
        {
            _instance = new UserTalismansService(new UserTalismansRepository());
        }
        return _instance;
    }

    public async Task<List<Talismans>> GetUserTalismansAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = await _userTalismansRepository.GetUserTalismansAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTalismansCountAsync(string userId, string search, string type, string rare)
    {
        return await _userTalismansRepository.GetUserTalismansCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserTalismanAsync(Talismans talisman, string userId)
    {
        return await _userTalismansRepository.InsertUserTalismanAsync(talisman, userId);
    }

    public async Task<bool> UpdateUserTalismanLevelAsync(string userId, Talismans talisman)
    {
        return await _userTalismansRepository.UpdateUserTalismanLevelAsync(userId, talisman);
    }

    public async Task<bool> UpdateUserTalismanStarAsync(string userId, Talismans talisman)
    {
        return await _userTalismansRepository.UpdateUserTalismanStarAsync(userId, talisman);
    }

    public async Task<bool> UpdateUserTalismanBreakthroughAsync(string userId, Talismans talisman, int star, double quantity)
    {
        return await _userTalismansRepository.UpdateUserTalismanBreakthroughAsync(userId, talisman, star, quantity);
    }

    public async Task<Talismans> GetUserTalismanByIdAsync(string userId, string Id)
    {
        return await _userTalismansRepository.GetUserTalismanByIdAsync(userId, Id);
    }

    public async Task<Talismans> SumPowerUserTalismansAsync(string userId)
    {
        return await _userTalismansRepository.SumPowerUserTalismansAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserTalismansBatchAsync(string userId, List<Talismans> talismans)
    {
        return await _userTalismansRepository.InsertOrUpdateUserTalismansBatchAsync(userId, talismans);
    }
}
