using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBeveragesService : IUserBeveragesService
{
    private static UserBeveragesService _instance;
    private readonly IUserBeveragesRepository _userBeveragesRepository;

    public UserBeveragesService(IUserBeveragesRepository userBeveragesRepository)
    {
        _userBeveragesRepository = userBeveragesRepository;
    }

    public static UserBeveragesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserBeveragesService(new UserBeveragesRepository());
        }
        return _instance;
    }

    public async Task<List<Beverages>> GetUserBeveragesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _userBeveragesRepository.GetUserBeveragesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBeveragesCountAsync(string userId, string search, string rare)
    {
        return await _userBeveragesRepository.GetUserBeveragesCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserBeverageAsync(Beverages beverage, string userId)
    {
        return await _userBeveragesRepository.InsertUserBeverageAsync(beverage, userId);
    }

    public async Task<bool> UpdateUserBeverageLevelAsync(string userId, Beverages beverage)
    {
        return await _userBeveragesRepository.UpdateUserBeverageLevelAsync(userId, beverage);
    }

    public async Task<bool> UpdateUserBeverageStarAsync(string userId, Beverages beverage)
    {
        return await _userBeveragesRepository.UpdateUserBeverageStarAsync(userId, beverage);
    }

    public async Task<bool> UpdateUserBeverageBreakthroughAsync(string userId, Beverages beverage, int star, double quantity)
    {
        return await _userBeveragesRepository.UpdateUserBeverageBreakthroughAsync(userId, beverage, star, quantity);
    }

    public async Task<Beverages> GetUserBeverageByIdAsync(string userId, string Id)
    {
        return await _userBeveragesRepository.GetUserBeverageByIdAsync(userId, Id);
    }

    public async Task<Beverages> SumPowerUserBeveragesAsync(string userId)
    {
        return await _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserBeveragesBatchAsync(string userId, List<Beverages> beverages)
    {
        return await _userBeveragesRepository.InsertOrUpdateUserBeveragesBatchAsync(userId, beverages);
    }
}
