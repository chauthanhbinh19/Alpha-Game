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

    public List<Alchemies> GetAlchemy(string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = _alchemyRepository.GetAlchemy(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAlchemyCount(string type, string rare)
    {
        return _alchemyRepository.GetAlchemyCount(type, rare);
    }

    public List<Alchemies> GetAlchemyWithPrice(string type, int pageSize, int offset)
    {
        List<Alchemies> list = _alchemyRepository.GetAlchemyWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAlchemyWithPriceCount(string type)
    {
        return _alchemyRepository.GetAlchemyWithPriceCount(type);
    }

    public Alchemies GetAlchemyById(string Id)
    {
        return _alchemyRepository.GetAlchemyById(Id);
    }

    public Alchemies SumPowerAlchemyPercent()
    {
        return _alchemyRepository.SumPowerAlchemyPercent();
    }

    public List<string> GetUniqueAlchemyId()
    {
        return _alchemyRepository.GetUniqueAlchemyId();
    }
}