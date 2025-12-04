using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITechnologiesService
{
    Task<List<string>> GetUniqueTechnologiesIdAsync();
    Task<List<Technologies>> GetTechnologiesAsync(int pageSize, int offset, string rare);
    Task<int> GetTechnologiesCountAsync(string rare);
    Task<List<Technologies>> GetTechnologiesWithPriceAsync(int pageSize, int offset);
    Task<int> GetTechnologiesWithPriceCountAsync();
    Task<Technologies> GetTechnologyByIdAsync(string id);
    Task<Technologies> SumPowerTechnologiesPercentAsync();
}
