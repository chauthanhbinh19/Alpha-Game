using System.Collections.Generic;

public class TitlesService : ITitlesService
{
    private readonly ITitlesRepository _titlesRepository;

    public TitlesService(ITitlesRepository titleRepository)
    {
        _titlesRepository = titleRepository;
    }

    public static TitlesService Create()
    {
        return new TitlesService(new TitlesRepository());
    }

    public List<Titles> GetTitles(int pageSize, int offset)
    {
        List<Titles> list = _titlesRepository.GetTitles(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTitlesCount()
    {
        return _titlesRepository.GetTitlesCount();
    }

    public List<Titles> GetTitlesWithPrice(int pageSize, int offset)
    {
        List<Titles> list = _titlesRepository.GetTitlesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTitlesWithPriceCount()
    {
        return _titlesRepository.GetTitlesWithPriceCount();
    }

    public Titles GetTitlesById(string Id)
    {
        return _titlesRepository.GetTitlesById(Id);
    }

    public Titles SumPowerTitlesPercent()
    {
        return _titlesRepository.SumPowerTitlesPercent();
    }
}
