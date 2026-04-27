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

    public async Task<List<Beverages>> GetUserBeveragesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _userBeveragesRepository.GetUserBeveragesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBeveragesCountAsync(string user_id, string search, string rare)
    {
        return await _userBeveragesRepository.GetUserBeveragesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserBeverageAsync(Beverages Beverages, string userId)
    {
        return await _userBeveragesRepository.InsertUserBeverageAsync(Beverages, userId);
    }

    public async Task<bool> UpdateBeverageLevelAsync(Beverages Beverages, int cardLevel)
    {
        return await _userBeveragesRepository.UpdateBeverageLevelAsync(Beverages, cardLevel);
    }

    public async Task<bool> UpdateBeverageBreakthroughAsync(Beverages Beverages, int star, double quantity)
    {
        return await _userBeveragesRepository.UpdateBeverageBreakthroughAsync(Beverages, star, quantity);
    }

    public async Task<Beverages> GetUserBeverageByIdAsync(string user_id, string Id)
    {
        return await _userBeveragesRepository.GetUserBeverageByIdAsync(user_id, Id);
    }

    public async Task<Beverages> SumPowerUserBeveragesAsync()
    {
        return await _userBeveragesRepository.SumPowerUserBeveragesAsync();
    }
}
