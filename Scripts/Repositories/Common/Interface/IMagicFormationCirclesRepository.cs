using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMagicFormationCirclesRepository
{
    Task<List<string>> GetUniqueMagicFormationCirclesTypesAsync();
    Task<List<string>> GetUniqueMagicFormationCirclesIdAsync();
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetMagicFormationCirclesCountAsync(string type, string rare);
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetMagicFormationCirclesWithPriceCountAsync(string type);
    Task<MagicFormationCircles> GetMagicFormationCircleByIdAsync(string Id);
    Task<MagicFormationCircles> SumPowerMagicFormationCirclesPercentAsync();
}
