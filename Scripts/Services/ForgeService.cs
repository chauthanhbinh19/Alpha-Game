using System.Collections.Generic;

public class ForgeService : IForgeService
{
    private readonly IForgeRepository _forgeRepository;

    public ForgeService(IForgeRepository forgeRepository)
    {
        _forgeRepository = forgeRepository;
    }

    public static ForgeService Create()
    {
        return new ForgeService(new ForgeRepository());
    }

    public List<string> GetUniqueForgeTypes()
    {
        return _forgeRepository.GetUniqueForgeTypes();
    }

    public List<Forge> GetForge(string type, int pageSize, int offset)
    {
        List<Forge> list = _forgeRepository.GetForge(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetForgeCount(string type)
    {
        return _forgeRepository.GetForgeCount(type);
    }

    public List<Forge> GetForgeWithPrice(string type, int pageSize, int offset)
    {
        List<Forge> list = _forgeRepository.GetForgeWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetForgeWithPriceCount(string type)
    {
        return _forgeRepository.GetForgeWithPriceCount(type);
    }

    public Forge GetForgeById(string Id)
    {
        return _forgeRepository.GetForgeById(Id);
    }

    public Forge SumPowerForgePercent()
    {
        return _forgeRepository.SumPowerForgePercent();
    }
}
