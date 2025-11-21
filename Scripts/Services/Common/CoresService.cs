using System.Collections.Generic;

public class CoresService : ICoresService
{
    private readonly ICoresRepository _CoresRepository;

    public CoresService(ICoresRepository titleRepository)
    {
        _CoresRepository = titleRepository;
    }

    public static CoresService Create()
    {
        return new CoresService(new CoresRepository());
    }

    public List<Cores> GetCores(int pageSize, int offset, string rare)
    {
        List<Cores> list = _CoresRepository.GetCores(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCoresCount(string rare)
    {
        return _CoresRepository.GetCoresCount(rare);
    }

    public List<Cores> GetCoresWithPrice(int pageSize, int offset)
    {
        List<Cores> list = _CoresRepository.GetCoresWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCoresWithPriceCount()
    {
        return _CoresRepository.GetCoresWithPriceCount();
    }

    public Cores GetCoresById(string Id)
    {
        return _CoresRepository.GetCoresById(Id);
    }

    public Cores SumPowerCoresPercent()
    {
        return _CoresRepository.SumPowerCoresPercent();
    }

    public List<string> GetUniqueCoreId()
    {
        return _CoresRepository.GetUniqueCoreId();
    }
}
