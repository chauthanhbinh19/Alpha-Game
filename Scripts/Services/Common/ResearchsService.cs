using System.Collections.Generic;
using System.Threading.Tasks;

public class ResearchsService : IResearchsService
{
    private static ResearchsService _instance;
    private readonly IResearchsRepository _researchsRepository;

    public ResearchsService(IResearchsRepository researchsRepository)
    {
        _researchsRepository = researchsRepository;
    }

    public static ResearchsService Create()
    {
        if (_instance == null)
        {
            _instance = new ResearchsService(new ResearchsRepository());
        }
        return _instance;
    }

    public async Task<List<Researchs>> GetResearchsAsync(string userId, int pageSize, int offset)
    {
        List<Researchs> list = await _researchsRepository.GetResearchsAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetResearchsCountAsync(string rare)
    {
        return await _researchsRepository.GetResearchsCountAsync(rare);
    }

    public async Task<List<Researchs>> GetResearchsWithPriceAsync(int pageSize, int offset)
    {
        List<Researchs> list = await _researchsRepository.GetResearchsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetResearchsWithPriceCountAsync()
    {
        return await _researchsRepository.GetResearchsWithPriceCountAsync();
    }

    public async Task<Researchs> GetResearchByIdAsync(string Id)
    {
        return await _researchsRepository.GetResearchByIdAsync(Id);
    }

    public async Task<Researchs> SumPowerResearchsPercentAsync()
    {
        return await _researchsRepository.SumPowerResearchsPercentAsync();
    }

    public async Task<List<string>> GetUniqueResearchsIdAsync()
    {
        return await _researchsRepository.GetUniqueResearchsIdAsync();
    }
}
