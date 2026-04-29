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

    public async Task<bool> InsertUserFashionAsync(Fashions fashion, string userId)
    {
        return await _userFashionsRepository.InsertUserFashionAsync(fashion, userId);
    }

    public async Task<bool> UpdateFashionLevelAsync(Fashions fashion, int level)
    {
        return await _userFashionsRepository.UpdateFashionLevelAsync(fashion, level);
    }

    public async Task<bool> UpdateFashionBreakthroughAsync(Fashions fashion, int star, double quantity)
    {
        return await _userFashionsRepository.UpdateFashionBreakthroughAsync(fashion, star, quantity);
    }

    public async Task<Fashions> GetUserFashionByIdAsync(string user_id, string Id)
    {
        return await _userFashionsRepository.GetUserFashionByIdAsync(user_id, Id);
    }

    public async Task<Fashions> SumPowerUserFashionsAsync()
    {
        return await _userFashionsRepository.SumPowerUserFashionsAsync();
    }

    public async Task<bool> InsertOrUpdateUserFashionsBatchAsync(List<Fashions> fashions)
    {
        return await _userFashionsRepository.InsertOrUpdateUserFashionsBatchAsync(fashions);
    }
}
