
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAlchemiesService : IUserAlchemiesService
{
     private static UserAlchemiesService _instance;
    private IUserAlchemiesRepository _userAlchemiesRepository;

    public UserAlchemiesService(IUserAlchemiesRepository userAlchemiesRepository)
    {
        _userAlchemiesRepository = userAlchemiesRepository;
    }

    public static UserAlchemiesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserAlchemiesService(new UserAlchemiesRepository());
        }
        return _instance;
    }

    public async Task<List<Alchemies>> GetUserAlchemiesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _userAlchemiesRepository.GetUserAlchemiesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAlchemiesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userAlchemiesRepository.GetUserAlchemiesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserAlchemyAsync(Alchemies alchemy, string userId)
    {
        return await _userAlchemiesRepository.InsertUserAlchemyAsync(alchemy, userId);
    }

    public async Task<bool> UpdateAlchemyLevelAsync(Alchemies alchemy, int cardLevel)
    {
        return await _userAlchemiesRepository.UpdateAlchemyLevelAsync(alchemy, cardLevel);
    }

    public async Task<bool> UpdateAlchemyBreakthroughAsync(Alchemies alchemy, int star, double quantity)
    {
        return await _userAlchemiesRepository.UpdateAlchemyBreakthroughAsync(alchemy, star, quantity);
    }

    public async Task<Alchemies> GetUserAlchemyByIdAsync(string user_id, string Id)
    {
        return await _userAlchemiesRepository.GetUserAlchemyByIdAsync(user_id, Id);
    }

    public async Task<Alchemies> SumPowerUserAlchemiesAsync()
    {
        return await _userAlchemiesRepository.SumPowerUserAlchemiesAsync();
    }
}
