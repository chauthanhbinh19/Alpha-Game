using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlantsService
{
    Task<List<string>> GetUniquePlantsIdAsync();
    Task<List<Plants>> GetPlantsAsync(string search, string rare, int pageSize, int offset);
    Task<List<Plants>> GetPlantsWithoutLimitAsync();
    Task<int> GetPlantsCountAsync(string search, string rare);
    Task<List<Plants>> GetPlantsWithPriceAsync(int pageSize, int offset);
    Task<int> GetPlantsWithPriceCountAsync();
    Task<Plants> GetPlantByIdAsync(string id);
    Task<Plants> SumPowerPlantsPercentAsync();
}
