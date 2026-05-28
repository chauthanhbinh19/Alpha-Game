using System.Collections.Generic;
using System.Threading.Tasks;

public class MagicFormationCirclesService : IMagicFormationCirclesService
{
    private static MagicFormationCirclesService _instance;
    private readonly IMagicFormationCirclesRepository _magicFormationCirclesRepository;

    public MagicFormationCirclesService(IMagicFormationCirclesRepository magicFormationCirclesRepository)
    {
        _magicFormationCirclesRepository = magicFormationCirclesRepository;
    }

    public static MagicFormationCirclesService Create()
    {
        if (_instance == null)
        {
            _instance = new MagicFormationCirclesService(new MagicFormationCirclesRepository());
        }
        return _instance;
    }

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

    public async Task<MagicFormationCircles> SumPowerMagicFormationCirclesPercentAsync()
    {
        return await _magicFormationCirclesRepository.SumPowerMagicFormationCirclesPercentAsync();
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
