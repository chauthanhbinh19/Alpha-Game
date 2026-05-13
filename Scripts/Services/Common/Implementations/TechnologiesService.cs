using System.Collections.Generic;
using System.Threading.Tasks;

public class TechnologiesService : ITechnologiesService
{
    private static TechnologiesService _instance;
    private readonly ITechnologiesRepository _technologiesRepository;

    public TechnologiesService(ITechnologiesRepository technologiesRepository)
    {
        _technologiesRepository = technologiesRepository;
    }

    public static TechnologiesService Create()
    {
        if (_instance == null)
        {
            _instance = new TechnologiesService(new TechnologiesRepository());
        }
        return _instance;
    }

    public async Task<List<Technologies>> GetTechnologiesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Technologies> list = await _technologiesRepository.GetTechnologiesAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesCountAsync(string search, string rare)
    {
        return await _technologiesRepository.GetTechnologiesCountAsync(search, rare);
    }

    public async Task<List<Technologies>> GetTechnologiesWithPriceAsync(int pageSize, int offset)
    {
        List<Technologies> list = await _technologiesRepository.GetTechnologiesWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesWithPriceCountAsync()
    {
        return await _technologiesRepository.GetTechnologiesWithPriceCountAsync();
    }

    public async Task<Technologies> GetTechnologyByIdAsync(string Id)
    {
        return await _technologiesRepository.GetTechnologyByIdAsync(Id);
    }

    public async Task<Technologies> SumPowerTechnologiesPercentAsync()
    {
        return await _technologiesRepository.SumPowerTechnologiesPercentAsync();
    }

    public async Task<List<string>> GetUniqueTechnologiesIdAsync()
    {
        return await _technologiesRepository.GetUniqueTechnologiesIdAsync();
    }

    public async Task<List<Technologies>> GetTechnologiesWithoutLimitAsync()
    {
        return await _technologiesRepository.GetTechnologiesWithoutLimitAsync();
    }
}
