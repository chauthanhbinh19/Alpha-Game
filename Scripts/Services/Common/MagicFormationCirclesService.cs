using System.Collections.Generic;
using System.Threading.Tasks;

public class MagicFormationCirclesService : IMagicFormationCirclesService
{
    private readonly IMagicFormationCirclesRepository _magicFormationCircleRepository;

    public MagicFormationCirclesService(IMagicFormationCirclesRepository magicFormationCircleRepository)
    {
        _magicFormationCircleRepository = magicFormationCircleRepository;
    }

    public static MagicFormationCirclesService Create()
    {
        return new MagicFormationCirclesService(new MagicFormationCirclesRepository());
    }

    public async Task<List<string>> GetUniqueMagicFormationCirclesTypesAsync()
    {
        return await _magicFormationCircleRepository.GetUniqueMagicFormationCirclesTypesAsync();
    }

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesAsync(string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _magicFormationCircleRepository.GetMagicFormationCirclesAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesCountAsync(string type, string rare)
    {
        return await _magicFormationCircleRepository.GetMagicFormationCirclesCountAsync(type, rare);
    }

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<MagicFormationCircles> list = await _magicFormationCircleRepository.GetMagicFormationCirclesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesWithPriceCountAsync(string type)
    {
        return await _magicFormationCircleRepository.GetMagicFormationCirclesWithPriceCountAsync(type);
    }

    public async Task<MagicFormationCircles> GetMagicFormationCircleByIdAsync(string Id)
    {
        return await _magicFormationCircleRepository.GetMagicFormationCircleByIdAsync(Id);
    }

    public async Task<MagicFormationCircles> SumPowerMagicFormationCirclesPercentAsync()
    {
        return await _magicFormationCircleRepository.SumPowerMagicFormationCirclesPercentAsync();
    }

    public async Task<List<string>> GetUniqueMagicFormationCirclesIdAsync()
    {
        return await _magicFormationCircleRepository.GetUniqueMagicFormationCirclesIdAsync();
    }
}
