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

    public async Task<List<Fashions>> GetUserFashionsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _userFashionsRepository.GetUserFashionsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFashionsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userFashionsRepository.GetUserFashionsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserFashionAsync(Fashions fashion, string userId)
    {
        return await _userFashionsRepository.InsertUserFashionAsync(fashion, userId);
    }

    public async Task<bool> UpdateUserFashionLevelAsync(string userId, Fashions fashion)
    {
        return await _userFashionsRepository.UpdateUserFashionLevelAsync(userId, fashion);
    }

    public async Task<bool> UpdateUserFashionStarAsync(string userId, Fashions fashion)
    {
        return await _userFashionsRepository.UpdateUserFashionStarAsync(userId, fashion);
    }

    public async Task<bool> UpdateUserFashionBreakthroughAsync(string userId, Fashions fashion, int star, double quantity)
    {
        return await _userFashionsRepository.UpdateUserFashionBreakthroughAsync(userId, fashion, star, quantity);
    }

    public async Task<Fashions> GetUserFashionByIdAsync(string userId, string Id)
    {
        return await _userFashionsRepository.GetUserFashionByIdAsync(userId, Id);
    }

    public async Task<Fashions> SumPowerUserFashionsAsync(string userId)
    {
        return await _userFashionsRepository.SumPowerUserFashionsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserFashionsBatchAsync(string userId, List<Fashions> fashions)
    {
        return await _userFashionsRepository.InsertOrUpdateUserFashionsBatchAsync(userId, fashions);
    }
}
