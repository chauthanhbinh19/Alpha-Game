using System.Collections.Generic;

public class AlchemyService : IAlchemyService
{
    private readonly IAlchemyRepository _alchemyRepository;

    public AlchemyService(IAlchemyRepository alchemyRepository)
    {
        _alchemyRepository = alchemyRepository;
    }

    public static AlchemyService Create()
    {
        return new AlchemyService(new AlchemyRepository());
    }

    public List<string> GetUniqueAlchemyTypes()
    {
        return _alchemyRepository.GetUniqueAlchemyTypes();
    }

    public List<Alchemy> GetAlchemy(string type, int pageSize, int offset)
    {
        List<Alchemy> list = _alchemyRepository.GetAlchemy(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAlchemyCount(string type)
    {
        return _alchemyRepository.GetAlchemyCount(type);
    }

    public List<Alchemy> GetAlchemyWithPrice(string type, int pageSize, int offset)
    {
        List<Alchemy> list = _alchemyRepository.GetAlchemyWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAlchemyWithPriceCount(string type)
    {
        return _alchemyRepository.GetAlchemyWithPriceCount(type);
    }

    public Alchemy GetAlchemyById(string Id)
    {
        return _alchemyRepository.GetAlchemyById(Id);
    }

    public Alchemy SumPowerAlchemyPercent()
    {
        return _alchemyRepository.SumPowerAlchemyPercent();
    }
}