using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBuildingsService
{
    Task<List<string>> GetUniqueBuildingsTypesAsync();
    Task<List<string>> GetUniqueBuildingsIdAsync();
    Task<List<Buildings>> GetBuildingsAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetBuildingsCountAsync(string type, string rare);
    Task<List<Buildings>> GetBuildingsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetBuildingsWithPriceCountAsync(string type);
    Task<Buildings> GetBuildingByIdAsync(string Id);
    Task<Buildings> SumPowerBuildingsPercentAsync();
}
