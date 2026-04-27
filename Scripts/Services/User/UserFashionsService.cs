using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFashionsService : IUserFashionsService
{
     private static UserFashionsService _instance;
    private readonly IUserFashionsRepository _userFashionsRepository;

    public UserFashionsService(IUserFashionsRepository userFashionsRepository)
    {
        _userFashionsRepository = userFashionsRepository;
    }

    public static UserFashionsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserFashionsService(new UserFashionsRepository());
        }
        return _instance;
    }

    public async Task<List<Fashions>> GetUserFashionsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _userFashionsRepository.GetUserFashionsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFashionsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userFashionsRepository.GetUserFashionsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserFashionAsync(Fashions Fashion, string userId)
    {
        return await _userFashionsRepository.InsertUserFashionAsync(Fashion, userId);
    }

    public async Task<bool> UpdateFashionLevelAsync(Fashions Fashion, int cardLevel)
    {
        return await _userFashionsRepository.UpdateFashionLevelAsync(Fashion, cardLevel);
    }

    public async Task<bool> UpdateFashionBreakthroughAsync(Fashions Fashion, int star, double quantity)
    {
        return await _userFashionsRepository.UpdateFashionBreakthroughAsync(Fashion, star, quantity);
    }

    public async Task<Fashions> GetUserFashionByIdAsync(string user_id, string Id)
    {
        return await _userFashionsRepository.GetUserFashionByIdAsync(user_id, Id);
    }

    public async Task<Fashions> SumPowerUserFashionsAsync()
    {
        return await _userFashionsRepository.SumPowerUserFashionsAsync();
    }
}
