using System.Collections.Generic;

public class RunesService : IRunesService
{
    private readonly IRunesRepository _RunesRepository;

    public RunesService(IRunesRepository titleRepository)
    {
        _RunesRepository = titleRepository;
    }

    public static RunesService Create()
    {
        return new RunesService(new RunesRepository());
    }

    public List<Runes> GetRunes(int pageSize, int offset, string rare)
    {
        List<Runes> list = _RunesRepository.GetRunes(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRunesCount(string rare)
    {
        return _RunesRepository.GetRunesCount(rare);
    }

    public List<Runes> GetRunesWithPrice(int pageSize, int offset)
    {
        List<Runes> list = _RunesRepository.GetRunesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRunesWithPriceCount()
    {
        return _RunesRepository.GetRunesWithPriceCount();
    }

    public Runes GetRunesById(string Id)
    {
        return _RunesRepository.GetRunesById(Id);
    }

    public Runes SumPowerRunesPercent()
    {
        return _RunesRepository.SumPowerRunesPercent();
    }

    public List<string> GetUniqueRuneId()
    {
        return _RunesRepository.GetUniqueRuneId();
    }
}
