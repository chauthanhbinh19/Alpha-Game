using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMagicFormationCirclesRepository
{
    Task<List<string>> GetUniqueMagicFormationCirclesTypesAsync();
    Task<List<string>> GetUniqueMagicFormationCirclesIdAsync();
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesWithoutLimitAsync();
    Task<int> GetMagicFormationCirclesCountAsync(string seach, string type, string rare);
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetMagicFormationCirclesWithPriceCountAsync(string type);
    Task<MagicFormationCircles> GetMagicFormationCircleByIdAsync(string Id);
    Task<MagicFormationCircles> SumPowerMagicFormationCirclesPercentAsync();
}
