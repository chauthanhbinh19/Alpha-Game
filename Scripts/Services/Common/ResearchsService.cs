using System.Collections.Generic;

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

    public List<Researchs> GetResearchs(string userId, int pageSize, int offset)
    {
        List<Researchs> list = _ResearchsRepository.GetResearchs(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetResearchsCount(string rare)
    {
        return _ResearchsRepository.GetResearchsCount(rare);
    }

    public List<Researchs> GetResearchsWithPrice(int pageSize, int offset)
    {
        List<Researchs> list = _ResearchsRepository.GetResearchsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetResearchsWithPriceCount()
    {
        return _ResearchsRepository.GetResearchsWithPriceCount();
    }

    public Researchs GetResearchsById(string Id)
    {
        return _ResearchsRepository.GetResearchsById(Id);
    }

    public Researchs SumPowerResearchsPercent()
    {
        return _ResearchsRepository.SumPowerResearchsPercent();
    }

    public List<string> GetUniqueResearchId()
    {
        return _ResearchsRepository.GetUniqueResearchId();
    }
}
