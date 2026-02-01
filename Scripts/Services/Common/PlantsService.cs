using System.Collections.Generic;
using System.Threading.Tasks;

public class PlantsService : IPlantsService
{
    private static PlantsService _instance;
    private readonly IPlantsRepository _plantsRepository;

    public PlantsService(IPlantsRepository PlantsRepository)
    {
        _plantsRepository = PlantsRepository;
    }

    public static PlantsService Create()
    {
        if (_instance == null)
        {
            _instance = new PlantsService(new PlantsRepository());
        }
        return _instance;
    }

    public async Task<List<Plants>> GetPlantsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Plants> list = await _plantsRepository.GetPlantsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPlantsCountAsync(string search, string rare)
    {
        return await _plantsRepository.GetPlantsCountAsync(search, rare);
    }

    public async Task<List<Plants>> GetPlantsWithPriceAsync(int pageSize, int offset)
    {
        List<Plants> list = await _plantsRepository.GetPlantsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPlantsWithPriceCountAsync()
    {
        return await _plantsRepository.GetPlantsWithPriceCountAsync();
    }

    public async Task<Plants> GetPlantByIdAsync(string Id)
    {
        return await _plantsRepository.GetPlantByIdAsync(Id);
    }

    public async Task<Plants> SumPowerPlantsPercentAsync()
    {
        return await _plantsRepository.SumPowerPlantsPercentAsync();
    }

    public async Task<List<string>> GetUniquePlantsIdAsync()
    {
        return await _plantsRepository.GetUniquePlantsIdAsync();
    }
}
