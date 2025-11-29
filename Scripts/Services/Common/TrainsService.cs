using System.Collections.Generic;

public class TrainsService : ITrainsService
{
    private readonly ITrainsRepository _TrainsRepository;

    public TrainsService(ITrainsRepository titleRepository)
    {
        _TrainsRepository = titleRepository;
    }

    public static TrainsService Create()
    {
        return new TrainsService(new TrainsRepository());
    }

    public List<Trains> GetTrains(string userId, int pageSize, int offset)
    {
        List<Trains> list = _TrainsRepository.GetTrains(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTrainsCount(string rare)
    {
        return _TrainsRepository.GetTrainsCount(rare);
    }

    public List<Trains> GetTrainsWithPrice(int pageSize, int offset)
    {
        List<Trains> list = _TrainsRepository.GetTrainsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTrainsWithPriceCount()
    {
        return _TrainsRepository.GetTrainsWithPriceCount();
    }

    public Trains GetTrainsById(string Id)
    {
        return _TrainsRepository.GetTrainsById(Id);
    }

    public Trains SumPowerTrainsPercent()
    {
        return _TrainsRepository.SumPowerTrainsPercent();
    }

    public List<string> GetUniqueTrainId()
    {
        return _TrainsRepository.GetUniqueTrainId();
    }
}
