using System.Collections.Generic;

public class BasesService : IBasesService
{
    private readonly IBasesRepository _BasesRepository;

    public BasesService(IBasesRepository titleRepository)
    {
        _BasesRepository = titleRepository;
    }

    public static BasesService Create()
    {
        return new BasesService(new BasesRepository());
    }

    public List<Bases> GetBases(string userId, int pageSize, int offset)
    {
        List<Bases> list = _BasesRepository.GetBases(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBasesCount(string rare)
    {
        return _BasesRepository.GetBasesCount(rare);
    }

    public List<Bases> GetBasesWithPrice(int pageSize, int offset)
    {
        List<Bases> list = _BasesRepository.GetBasesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBasesWithPriceCount()
    {
        return _BasesRepository.GetBasesWithPriceCount();
    }

    public Bases GetBasesById(string Id)
    {
        return _BasesRepository.GetBasesById(Id);
    }

    public Bases SumPowerBasesPercent()
    {
        return _BasesRepository.SumPowerBasesPercent();
    }

    public List<string> GetUniqueBaseId()
    {
        return _BasesRepository.GetUniqueBaseId();
    }
}
