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

    public async Task<List<Weapons>> GetUserWeaponsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _userWeaponsRepository.GetUserWeaponsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserWeaponsCountAsync(string user_id, string search, string rare)
    {
        return await _userWeaponsRepository.GetUserWeaponsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserWeaponAsync(Weapons Weapons, string userId)
    {
        return await _userWeaponsRepository.InsertUserWeaponAsync(Weapons, userId);
    }

    public async Task<bool> UpdateWeaponLevelAsync(Weapons Weapons, int cardLevel)
    {
        return await _userWeaponsRepository.UpdateWeaponLevelAsync(Weapons, cardLevel);
    }

    public async Task<bool> UpdateWeaponBreakthroughAsync(Weapons Weapons, int star, double quantity)
    {
        return await _userWeaponsRepository.UpdateWeaponBreakthroughAsync(Weapons, star, quantity);
    }

    public async Task<Weapons> GetUserWeaponByIdAsync(string user_id, string Id)
    {
        return await _userWeaponsRepository.GetUserWeaponByIdAsync(user_id, Id);
    }

    public async Task<Weapons> SumPowerUserWeaponsAsync()
    {
        return await _userWeaponsRepository.SumPowerUserWeaponsAsync();
    }
}
