using System.Collections.Generic;
using System.Threading.Tasks;

public class PlantsService : IPlantsService
{
    private readonly IPlantsRepository _PlantsRepository;

    public PlantsService(IPlantsRepository PlantRepository)
    {
        _PlantsRepository = PlantRepository;
    }

    public static PlantsService Create()
    {
        return new PlantsService(new PlantsRepository());
    }

    public async Task<List<Plants>> GetPlantsAsync(int pageSize, int offset, string rare)
    {
        List<Plants> list = await _PlantsRepository.GetPlantsAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPlantsCountAsync(string rare)
    {
        return await _PlantsRepository.GetPlantsCountAsync(rare);
    }

    public async Task<List<Plants>> GetPlantsWithPriceAsync(int pageSize, int offset)
    {
        List<Plants> list = await _PlantsRepository.GetPlantsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPlantsWithPriceCountAsync()
    {
        return await _PlantsRepository.GetPlantsWithPriceCountAsync();
    }

    public async Task<Plants> GetPlantByIdAsync(string Id)
    {
        return await _PlantsRepository.GetPlantByIdAsync(Id);
    }

    public async Task<Plants> SumPowerPlantsPercentAsync()
    {
        return await _PlantsRepository.SumPowerPlantsPercentAsync();
    }

    public async Task<List<string>> GetUniquePlantsIdAsync()
    {
        return await _PlantsRepository.GetUniquePlantsIdAsync();
    }
}
