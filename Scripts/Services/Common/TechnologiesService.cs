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

    public async Task<List<Technologies>> GetTechnologiesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Technologies> list = await _TechnologiesRepository.GetTechnologiesAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesCountAsync(string search, string rare)
    {
        return await _TechnologiesRepository.GetTechnologiesCountAsync(search, rare);
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
