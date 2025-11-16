using System.Collections.Generic;

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

    public List<Technologies> GetTechnologies(int pageSize, int offset, string rare)
    {
        List<Technologies> list = _TechnologiesRepository.GetTechnologies(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTechnologiesCount(string rare)
    {
        return _TechnologiesRepository.GetTechnologiesCount(rare);
    }

    public List<Technologies> GetTechnologiesWithPrice(int pageSize, int offset)
    {
        List<Technologies> list = _TechnologiesRepository.GetTechnologiesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTechnologiesWithPriceCount()
    {
        return _TechnologiesRepository.GetTechnologiesWithPriceCount();
    }

    public Technologies GetTechnologiesById(string Id)
    {
        return _TechnologiesRepository.GetTechnologiesById(Id);
    }

    public Technologies SumPowerTechnologiesPercent()
    {
        return _TechnologiesRepository.SumPowerTechnologiesPercent();
    }

    public List<string> GetUniqueTechnologyId()
    {
        return _TechnologiesRepository.GetUniqueTechnologyId();
    }
}
