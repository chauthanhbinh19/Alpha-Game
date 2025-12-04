using System.Collections.Generic;
using System.Threading.Tasks;

public class TechnologiesService : ITechnologiesService
{
    private readonly ITechnologiesRepository _TechnologiesRepository;

    public TechnologiesService(ITechnologiesRepository titleRepository)
    {
        _TechnologiesRepository = titleRepository;
    }

    public static TechnologiesService Create()
    {
        return new TechnologiesService(new TechnologiesRepository());
    }

    public async Task<List<Technologies>> GetTechnologiesAsync(int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _TechnologiesRepository.GetTechnologiesAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesCountAsync(string rare)
    {
        return await _TechnologiesRepository.GetTechnologiesCountAsync(rare);
    }

    public async Task<List<Technologies>> GetTechnologiesWithPriceAsync(int pageSize, int offset)
    {
        List<Technologies> list = await _TechnologiesRepository.GetTechnologiesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesWithPriceCountAsync()
    {
        return await _TechnologiesRepository.GetTechnologiesWithPriceCountAsync();
    }

    public async Task<Technologies> GetTechnologyByIdAsync(string Id)
    {
        return await _TechnologiesRepository.GetTechnologyByIdAsync(Id);
    }

    public async Task<Technologies> SumPowerTechnologiesPercentAsync()
    {
        return await _TechnologiesRepository.SumPowerTechnologiesPercentAsync();
    }

    public async Task<List<string>> GetUniqueTechnologiesIdAsync()
    {
        return await _TechnologiesRepository.GetUniqueTechnologiesIdAsync();
    }
}
