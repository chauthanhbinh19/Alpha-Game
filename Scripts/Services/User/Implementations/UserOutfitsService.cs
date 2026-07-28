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

    public async Task<List<Outfits>> GetUserOutfitsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Outfits> list = await _userOutfitsRepository.GetUserOutfitsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserOutfitsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userOutfitsRepository.GetUserOutfitsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserOutfitAsync(Outfits outfit, string userId)
    {
        var result = await _userOutfitsRepository.InsertUserOutfitAsync(outfit, userId);
        if (result)
        {
            await OutfitsGalleryService.Create().InsertOutfitGalleryAsync(userId, outfit.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserOutfitLevelAsync(string userId, Outfits outfit)
    {
        return await _userOutfitsRepository.UpdateUserOutfitLevelAsync(userId, outfit);
    }

    public async Task<bool> UpdateUserOutfitStarAsync(string userId, Outfits outfit)
    {
        var result = await _userOutfitsRepository.UpdateUserOutfitStarAsync(userId, outfit);
        if (result)
        {
            await OutfitsGalleryService.Create().UpdateStarOutfitGalleryAsync(userId, outfit.Id, outfit.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserOutfitBreakthroughAsync(string userId, Outfits outfit, int star, double quantity)
    {
        return await _userOutfitsRepository.UpdateUserOutfitBreakthroughAsync(userId, outfit, star, quantity);
    }

    public async Task<Outfits> GetUserOutfitByIdAsync(string userId, string Id)
    {
        return await _userOutfitsRepository.GetUserOutfitByIdAsync(userId, Id);
    }

    public async Task<Outfits> SumPowerUserOutfitsAsync(string userId)
    {
        return await _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserOutfitsBatchAsync(string userId, List<Outfits> outfits)
    {
        return await _userOutfitsRepository.InsertOrUpdateUserOutfitsBatchAsync(userId, outfits);
    }
}
