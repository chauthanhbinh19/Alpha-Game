using System.Collections.Generic;
using System.Threading.Tasks;

public class WeaponsService : IWeaponsService
{
    private static WeaponsService _instance;
    private readonly IWeaponsRepository _weaponsRepository;

    public WeaponsService(IWeaponsRepository weaponsRepository)
    {
        _weaponsRepository = weaponsRepository;
    }

    public static WeaponsService Create()
    {
        if (_instance == null)
        {
            _instance = new WeaponsService(new WeaponsRepository());
        }
        return _instance;
    }

    public async Task<List<Weapons>> GetWeaponsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Weapons> list = await _weaponsRepository.GetWeaponsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsCountAsync(string search, string rare)
    {
        return await _weaponsRepository.GetWeaponsCountAsync(search, rare);
    }

    public async Task<List<Weapons>> GetWeaponsWithPriceAsync(int pageSize, int offset)
    {
        List<Weapons> list = await _weaponsRepository.GetWeaponsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsWithPriceCountAsync()
    {
        return await _weaponsRepository.GetWeaponsWithPriceCountAsync();
    }

    public async Task<Weapons> GetWeaponByIdAsync(string Id)
    {
        return await _weaponsRepository.GetWeaponByIdAsync(Id);
    }

    public async Task<Weapons> SumPowerWeaponsPercentAsync()
    {
        return await _weaponsRepository.SumPowerWeaponsPercentAsync();
    }

    public async Task<List<string>> GetUniqueWeaponsIdAsync()
    {
        return await _weaponsRepository.GetUniqueWeaponsIdAsync();
    }

    public async Task<List<Weapons>> GetWeaponsWithoutLimitAsync()
    {
        return await _weaponsRepository.GetWeaponsWithoutLimitAsync();
    }
}
