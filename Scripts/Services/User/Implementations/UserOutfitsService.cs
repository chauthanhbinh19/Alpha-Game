using System.Collections.Generic;
using System.Threading.Tasks;

public class UserOutfitsService : IUserOutfitsService
{
     private static UserOutfitsService _instance;
    private readonly IUserOutfitsRepository _userOutfitsRepository;

    public UserOutfitsService(IUserOutfitsRepository userOutfitsRepository)
    {
        _userOutfitsRepository = userOutfitsRepository;
    }

    public static UserOutfitsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserOutfitsService(new UserOutfitsRepository());
        }
        return _instance;
    }

    public async Task<List<Outfits>> GetUserOutfitsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Outfits> list = await _userOutfitsRepository.GetUserOutfitsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserOutfitsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userOutfitsRepository.GetUserOutfitsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserOutfitAsync(Outfits outfit, string userId)
    {
        return await _userOutfitsRepository.InsertUserOutfitAsync(outfit, userId);
    }

    public async Task<bool> UpdateOutfitLevelAsync(Outfits outfit)
    {
        return await _userOutfitsRepository.UpdateOutfitLevelAsync(outfit);
    }

    public async Task<bool> UpdateOutfitStarAsync(Outfits outfit)
    {
        return await _userOutfitsRepository.UpdateOutfitStarAsync(outfit);
    }

    public async Task<bool> UpdateOutfitBreakthroughAsync(Outfits outfit, int star, double quantity)
    {
        return await _userOutfitsRepository.UpdateOutfitBreakthroughAsync(outfit, star, quantity);
    }

    public async Task<Outfits> GetUserOutfitByIdAsync(string user_id, string Id)
    {
        return await _userOutfitsRepository.GetUserOutfitByIdAsync(user_id, Id);
    }

    public async Task<Outfits> SumPowerUserOutfitsAsync()
    {
        return await _userOutfitsRepository.SumPowerUserOutfitsAsync();
    }

    public async Task<bool> InsertOrUpdateUserOutfitsBatchAsync(List<Outfits> outfits)
    {
        return await _userOutfitsRepository.InsertOrUpdateUserOutfitsBatchAsync(outfits);
    }
}
