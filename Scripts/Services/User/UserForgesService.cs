using System.Collections.Generic;
using System.Threading.Tasks;

public class UserForgesService : IUserForgesService
{
     private static UserForgesService _instance;
    private IUserForgesRepository _userForgesRepository;

    public UserForgesService(IUserForgesRepository userForgesRepository)
    {
        _userForgesRepository = userForgesRepository;
    }

    public static UserForgesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserForgesService(new UserForgesRepository());
        }
        return _instance;
    }

    public async Task<List<Forges>> GetUserForgesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _userForgesRepository.GetUserForgesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserForgesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userForgesRepository.GetUserForgesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserForgeAsync(Forges forge, string userId)
    {
        return await _userForgesRepository.InsertUserForgeAsync(forge, userId);
    }

    public async Task<bool> UpdateForgeLevelAsync(Forges forge, int cardLevel)
    {
        return await _userForgesRepository.UpdateForgeLevelAsync(forge, cardLevel);
    }

    public async Task<bool> UpdateForgeBreakthroughAsync(Forges forge, int star, double quantity)
    {
        return await _userForgesRepository.UpdateForgeBreakthroughAsync(forge, star, quantity);
    }

    public async Task<Forges> GetUserForgeByIdAsync(string user_id, string Id)
    {
        return await _userForgesRepository.GetUserForgeByIdAsync(user_id, Id);
    }

    public async Task<Forges> SumPowerUserForgesAsync()
    {
        return await _userForgesRepository.SumPowerUserForgesAsync();
    }
}
