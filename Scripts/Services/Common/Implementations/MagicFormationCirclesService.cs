using System.Collections.Generic;
using System.Threading.Tasks;

public class MagicFormationCirclesService : IMagicFormationCirclesService
{
    private readonly IMagicFormationCirclesRepository _magicFormationCirclesRepository;

    public MagicFormationCirclesService(IMagicFormationCirclesRepository magicFormationCirclesRepository)
    {
        _magicFormationCirclesRepository = magicFormationCirclesRepository;
    }

    public static IMagicFormationCirclesService Create() => ServiceContainer.GetService<IMagicFormationCirclesService>();

    public async Task<List<string>> GetUniqueMagicFormationCirclesTypesAsync()
    {
        return await _magicFormationCirclesRepository.GetUniqueMagicFormationCirclesTypesAsync();
    }

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<MagicFormationCircles> list = await _magicFormationCirclesRepository.GetMagicFormationCirclesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare)
    {
        return await _magicFormationCirclesRepository.GetMagicFormationCirclesCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertMagicFormationCircleAsync(MagicFormationCircles entity)
    {
        var result = await _magicFormationCirclesRepository.InsertMagicFormationCircleAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateMagicFormationCircleAsync(MagicFormationCircles entity)
    {
        var result = await _magicFormationCirclesRepository.UpdateMagicFormationCircleAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<MagicFormationCircles> list = await _magicFormationCirclesRepository.GetMagicFormationCirclesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesWithPriceCountAsync(string type)
    {
        return await _magicFormationCirclesRepository.GetMagicFormationCirclesWithPriceCountAsync(type);
    }

    public async Task<MagicFormationCircles> GetMagicFormationCircleByIdAsync(string Id)
    {
        return await _magicFormationCirclesRepository.GetMagicFormationCircleByIdAsync(Id);
    }

    public async Task<MagicFormationCircles> SumPowerMagicFormationCirclesPercentAsync(string userId)
    {
        return await _magicFormationCirclesRepository.SumPowerMagicFormationCirclesPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueMagicFormationCirclesIdAsync()
    {
        return await _magicFormationCirclesRepository.GetUniqueMagicFormationCirclesIdAsync();
    }

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesWithoutLimitAsync()
    {
        return await _magicFormationCirclesRepository.GetMagicFormationCirclesWithoutLimitAsync();
    }
}
