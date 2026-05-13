using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITechnologiesRepository
{
    Task<List<string>> GetUniqueTechnologiesIdAsync();
    Task<List<Technologies>> GetTechnologiesAsync(string search, string rare, int pageSize, int offset);
    Task<List<Technologies>> GetTechnologiesWithoutLimitAsync();
    Task<int> GetTechnologiesCountAsync(string search, string rare);
    Task<List<Technologies>> GetTechnologiesWithPriceAsync(int pageSize, int offset);
    Task<int> GetTechnologiesWithPriceCountAsync();
    Task<Technologies> GetTechnologyByIdAsync(string id);
    Task<Technologies> SumPowerTechnologiesPercentAsync();
}
