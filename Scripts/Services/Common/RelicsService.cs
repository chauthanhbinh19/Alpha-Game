using System.Collections.Generic;
public class RelicsService : IRelicsService
{
    private readonly IRelicsRepository _relicsRepository;

    public RelicsService(IRelicsRepository relicsRepository)
    {
        _relicsRepository = relicsRepository;
    }

    public static RelicsService Create()
    {
        return new RelicsService(new RelicsRepository());
    }

    public List<string> GetUniqueRelicsTypes()
    {
        return _relicsRepository.GetUniqueRelicsTypes();
    }

    public List<Relics> GetRelics(string type, int pageSize, int offset)
    {
        List<Relics> list = _relicsRepository.GetRelics(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRelicsCount(string type)
    {
        return _relicsRepository.GetRelicsCount(type);
    }

    public List<Relics> GetRelicsWithPrice(string type, int pageSize, int offset)
    {
        List<Relics> list = _relicsRepository.GetRelicsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRelicsWithPriceCount(string type)
    {
        return _relicsRepository.GetRelicsWithPriceCount(type);
    }

    public Relics GetRelicsById(string Id)
    {
        return _relicsRepository.GetRelicsById(Id);
    }

    public Relics SumPowerRelicsPercent()
    {
        return _relicsRepository.SumPowerRelicsPercent();
    }
}
