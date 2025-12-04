using System.Collections.Generic;
using System.Threading.Tasks;

public interface IResearchsService
{
    Task<List<string>> GetUniqueResearchsIdAsync();
    Task<List<Researchs>> GetResearchsAsync(string userId, int pageSize, int offset);
    Task<int> GetResearchsCountAsync(string rare);
    Task<List<Researchs>> GetResearchsWithPriceAsync(int pageSize, int offset);
    Task<int> GetResearchsWithPriceCountAsync();
    Task<Researchs> GetResearchByIdAsync(string Id);
    Task<Researchs> SumPowerResearchsPercentAsync();
}
