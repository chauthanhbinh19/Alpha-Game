using System.Collections.Generic;
using System.Threading.Tasks;

public class ResearchsService : IResearchsService
{
    private readonly IResearchsRepository _ResearchsRepository;

    public ResearchsService(IResearchsRepository titleRepository)
    {
        _ResearchsRepository = titleRepository;
    }

    public static ResearchsService Create()
    {
        return new ResearchsService(new ResearchsRepository());
    }

    public async Task<List<Researchs>> GetResearchsAsync(string userId, int pageSize, int offset)
    {
        List<Researchs> list = await _ResearchsRepository.GetResearchsAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetResearchsCountAsync(string rare)
    {
        return await _ResearchsRepository.GetResearchsCountAsync(rare);
    }

    public async Task<List<Researchs>> GetResearchsWithPriceAsync(int pageSize, int offset)
    {
        List<Researchs> list = await _ResearchsRepository.GetResearchsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetResearchsWithPriceCountAsync()
    {
        return await _ResearchsRepository.GetResearchsWithPriceCountAsync();
    }

    public async Task<Researchs> GetResearchByIdAsync(string Id)
    {
        return await _ResearchsRepository.GetResearchByIdAsync(Id);
    }

    public async Task<Researchs> SumPowerResearchsPercentAsync()
    {
        return await _ResearchsRepository.SumPowerResearchsPercentAsync();
    }

    public async Task<List<string>> GetUniqueResearchsIdAsync()
    {
        return await _ResearchsRepository.GetUniqueResearchsIdAsync();
    }
}
