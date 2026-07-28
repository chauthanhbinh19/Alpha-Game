using System.Collections.Generic;
using System.Threading.Tasks;

public class UserWeaponsService : IUserWeaponsService
{
    private static UserWeaponsService _instance;
    private readonly IUserWeaponsRepository _userWeaponsRepository;

    public UserWeaponsService(IUserWeaponsRepository userWeaponsRepository)
    {
        _userWeaponsRepository = userWeaponsRepository;
    }

    public static UserWeaponsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserWeaponsService(new UserWeaponsRepository());
        }
        return _instance;
    }

    public async Task<List<Weapons>> GetUserWeaponsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _userWeaponsRepository.GetUserWeaponsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserWeaponsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userWeaponsRepository.GetUserWeaponsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserWeaponAsync(Weapons weapon, string userId)
    {
        var result = await _userWeaponsRepository.InsertUserWeaponAsync(weapon, userId);
        if (result)
        {
            await WeaponsGalleryService.Create().InsertWeaponGalleryAsync(userId, weapon.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserWeaponLevelAsync(string userId, Weapons weapon)
    {
        return await _userWeaponsRepository.UpdateUserWeaponLevelAsync(userId, weapon);
    }

    public async Task<bool> UpdateUserWeaponStarAsync(string userId, Weapons weapon)
    {
        var result = await _userWeaponsRepository.UpdateUserWeaponStarAsync(userId, weapon);
        if (result)
        {
            await WeaponsGalleryService.Create().UpdateStarWeaponGalleryAsync(userId, weapon.Id, weapon. Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserWeaponBreakthroughAsync(string userId, Weapons weapon, int star, double quantity)
    {
        return await _userWeaponsRepository.UpdateUserWeaponBreakthroughAsync(userId, weapon, star, quantity);
    }

    public async Task<Weapons> GetUserWeaponByIdAsync(string userId, string Id)
    {
        return await _userWeaponsRepository.GetUserWeaponByIdAsync(userId, Id);
    }

    public async Task<Weapons> SumPowerUserWeaponsAsync(string userId)
    {
        return await _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserWeaponsBatchAsync(string userId, List<Weapons> weapons)
    {
        return await _userWeaponsRepository.InsertOrUpdateUserWeaponsBatchAsync(userId, weapons);
    }
}
