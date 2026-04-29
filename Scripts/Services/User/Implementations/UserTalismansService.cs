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

    public async Task<List<Talismans>> GetUserTalismansAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = await _userTalismansRepository.GetUserTalismansAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTalismansCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userTalismansRepository.GetUserTalismansCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserTalismanAsync(Talismans talisman, string userId)
    {
        return await _userTalismansRepository.InsertUserTalismanAsync(talisman, userId);
    }

    public async Task<bool> UpdateTalismanLevelAsync(Talismans talisman, int level)
    {
        return await _userTalismansRepository.UpdateTalismanLevelAsync(talisman, level);
    }

    public async Task<bool> UpdateTalismanBreakthroughAsync(Talismans talisman, int star, double quantity)
    {
        return await _userTalismansRepository.UpdateTalismanBreakthroughAsync(talisman, star, quantity);
    }

    public async Task<Talismans> GetUserTalismanByIdAsync(string user_id, string Id)
    {
        return await _userTalismansRepository.GetUserTalismanByIdAsync(user_id, Id);
    }

    public async Task<Talismans> SumPowerUserTalismansAsync()
    {
        return await _userTalismansRepository.SumPowerUserTalismansAsync();
    }

    public async Task<bool> InsertOrUpdateUserTalismansBatchAsync(List<Talismans> talismans)
    {
        return await _userTalismansRepository.InsertOrUpdateUserTalismansBatchAsync(talismans);
    }
}
