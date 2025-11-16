using System.Collections.Generic;

public class ArchitecturesService : IArchitecturesService
{
    private readonly IArchitecturesRepository _ArchitecturesRepository;

    public ArchitecturesService(IArchitecturesRepository titleRepository)
    {
        _ArchitecturesRepository = titleRepository;
    }

    public static ArchitecturesService Create()
    {
        return new ArchitecturesService(new ArchitecturesRepository());
    }

    public List<Architectures> GetArchitectures(int pageSize, int offset, string rare)
    {
        List<Architectures> list = _ArchitecturesRepository.GetArchitectures(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetArchitecturesCount(string rare)
    {
        return _ArchitecturesRepository.GetArchitecturesCount(rare);
    }

    public List<Architectures> GetArchitecturesWithPrice(int pageSize, int offset)
    {
        List<Architectures> list = _ArchitecturesRepository.GetArchitecturesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetArchitecturesWithPriceCount()
    {
        return _ArchitecturesRepository.GetArchitecturesWithPriceCount();
    }

    public Architectures GetArchitecturesById(string Id)
    {
        return _ArchitecturesRepository.GetArchitecturesById(Id);
    }

    public Architectures SumPowerArchitecturesPercent()
    {
        return _ArchitecturesRepository.SumPowerArchitecturesPercent();
    }

    public List<string> GetUniqueArchitectureId()
    {
        return _ArchitecturesRepository.GetUniqueArchitectureId();
    }
}
