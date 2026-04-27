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

    public async Task<List<Foods>> GetUserFoodsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Foods> list = await _userFoodsRepository.GetUserFoodsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFoodsCountAsync(string user_id, string search, string rare)
    {
        return await _userFoodsRepository.GetUserFoodsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserFoodAsync(Foods Foods, string userId)
    {
        return await _userFoodsRepository.InsertUserFoodAsync(Foods, userId);
    }

    public async Task<bool> UpdateFoodLevelAsync(Foods Foods, int cardLevel)
    {
        return await _userFoodsRepository.UpdateFoodLevelAsync(Foods, cardLevel);
    }

    public async Task<bool> UpdateFoodBreakthroughAsync(Foods Foods, int star, double quantity)
    {
        return await _userFoodsRepository.UpdateFoodBreakthroughAsync(Foods, star, quantity);
    }

    public async Task<Foods> GetUserFoodByIdAsync(string user_id, string Id)
    {
        return await _userFoodsRepository.GetUserFoodByIdAsync(user_id, Id);
    }

    public async Task<Foods> SumPowerUserFoodsAsync()
    {
        return await _userFoodsRepository.SumPowerUserFoodsAsync();
    }
}
