using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMagicFormationCirclesService
{
    Task<List<string>> GetUniqueMagicFormationCirclesTypesAsync();
    Task<List<string>> GetUniqueMagicFormationCirclesIdAsync();
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare);
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetMagicFormationCirclesWithPriceCountAsync(string type);
    Task<MagicFormationCircles> GetMagicFormationCircleByIdAsync(string Id);
    Task<MagicFormationCircles> SumPowerMagicFormationCirclesPercentAsync();
}
