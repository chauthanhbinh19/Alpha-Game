using System.Collections.Generic;
using System.Threading.Tasks;

public class WeaponsService : IWeaponsService
{
    private readonly IWeaponsRepository _WeaponsRepository;

    public WeaponsService(IWeaponsRepository titleRepository)
    {
        _WeaponsRepository = titleRepository;
    }

    public static WeaponsService Create()
    {
        return new WeaponsService(new WeaponsRepository());
    }

    public async Task<List<Weapons>> GetWeaponsAsync(int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _WeaponsRepository.GetWeaponsAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsCountAsync(string rare)
    {
        return await _WeaponsRepository.GetWeaponsCountAsync(rare);
    }

    public async Task<List<Weapons>> GetWeaponsWithPriceAsync(int pageSize, int offset)
    {
        List<Weapons> list = await _WeaponsRepository.GetWeaponsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsWithPriceCountAsync()
    {
        return await _WeaponsRepository.GetWeaponsWithPriceCountAsync();
    }

    public async Task<Weapons> GetWeaponByIdAsync(string Id)
    {
        return await _WeaponsRepository.GetWeaponByIdAsync(Id);
    }

    public async Task<Weapons> SumPowerWeaponsPercentAsync()
    {
        return await _WeaponsRepository.SumPowerWeaponsPercentAsync();
    }

    public async Task<List<string>> GetUniqueWeaponsIdAsync()
    {
        return await _WeaponsRepository.GetUniqueWeaponsIdAsync();
    }
}
