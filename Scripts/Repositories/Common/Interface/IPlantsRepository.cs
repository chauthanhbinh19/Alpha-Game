using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlantsRepository
{
    Task<List<string>> GetUniquePlantsIdAsync();
    Task<List<Plants>> GetPlantsAsync(int pageSize, int offset, string rare);
    Task<int> GetPlantsCountAsync(string rare);
    Task<List<Plants>> GetPlantsWithPriceAsync(int pageSize, int offset);
    Task<int> GetPlantsWithPriceCountAsync();
    Task<Plants> GetPlantByIdAsync(string id);
    Task<Plants> SumPowerPlantsPercentAsync();
}
