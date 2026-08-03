using System.Collections.Generic;
using System.Threading.Tasks;

public class WeaponsService : IWeaponsService
{
    private readonly IWeaponsRepository _weaponsRepository;

    public WeaponsService(IWeaponsRepository weaponsRepository)
    {
        _weaponsRepository = weaponsRepository;
    }

    public static IWeaponsService Create() => ServiceContainer.GetService<IWeaponsService>();

    public async Task<List<string>> GetUniqueWeaponsTypesAsync()
    {
        return await _weaponsRepository.GetUniqueWeaponsTypesAsync();
    }

    public async Task<List<Weapons>> GetWeaponsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Weapons> list = await _weaponsRepository.GetWeaponsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsCountAsync(string search, string type, string rare)
    {
        return await _weaponsRepository.GetWeaponsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertWeaponAsync(Weapons entity)
    {
        var result = await _weaponsRepository.InsertWeaponAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateWeaponAsync(Weapons entity)
    {
        var result = await _weaponsRepository.UpdateWeaponAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Weapons>> GetWeaponsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Weapons> list = await _weaponsRepository.GetWeaponsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsWithPriceCountAsync(string type)
    {
        return await _weaponsRepository.GetWeaponsWithPriceCountAsync(type);
    }

    public async Task<Weapons> GetWeaponByIdAsync(string Id)
    {
        return await _weaponsRepository.GetWeaponByIdAsync(Id);
    }

    public async Task<Weapons> SumPowerWeaponsPercentAsync(string userId)
    {
        return await _weaponsRepository.SumPowerWeaponsPercentAsync(userId);
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
