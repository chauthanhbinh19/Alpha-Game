using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFoodsService : IUserFoodsService
{
    private static UserFoodsService _instance;
    private readonly IUserFoodsRepository _userFoodsRepository;

    public UserFoodsService(IUserFoodsRepository userFoodsRepository)
    {
        _userFoodsRepository = userFoodsRepository;
    }

    public static UserFoodsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserFoodsService(new UserFoodsRepository());
        }
        return _instance;
    }

    public async Task<List<Foods>> GetUserFoodsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Foods> list = await _userFoodsRepository.GetUserFoodsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFoodsCountAsync(string userId, string search, string rare)
    {
        return await _userFoodsRepository.GetUserFoodsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserFoodAsync(Foods food, string userId)
    {
        return await _userFoodsRepository.InsertUserFoodAsync(food, userId);
    }

    public async Task<bool> UpdateUserFoodLevelAsync(string userId, Foods food)
    {
        return await _userFoodsRepository.UpdateUserFoodLevelAsync(userId, food);
    }

    public async Task<bool> UpdateUserFoodStarAsync(string userId, Foods food)
    {
        return await _userFoodsRepository.UpdateUserFoodStarAsync(userId, food);
    }

    public async Task<bool> UpdateUserFoodBreakthroughAsync(string userId, Foods food, int star, double quantity)
    {
        return await _userFoodsRepository.UpdateUserFoodBreakthroughAsync(userId, food, star, quantity);
    }

    public async Task<Foods> GetUserFoodByIdAsync(string userId, string Id)
    {
        return await _userFoodsRepository.GetUserFoodByIdAsync(userId, Id);
    }

    public async Task<Foods> SumPowerUserFoodsAsync(string userId)
    {
        return await _userFoodsRepository.SumPowerUserFoodsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserFoodsBatchAsync(string userId, List<Foods> foods)
    {
        return await _userFoodsRepository.InsertOrUpdateUserFoodsBatchAsync(userId, foods);
    }
}
