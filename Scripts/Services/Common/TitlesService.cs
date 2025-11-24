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

    public List<Architectures> GetTitles(int pageSize, int offset, string rare)
    {
        List<Architectures> list = _titlesRepository.GetTitles(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTitlesCount(string rare)
    {
        return _titlesRepository.GetTitlesCount(rare);
    }

    public List<Architectures> GetTitlesWithPrice(int pageSize, int offset)
    {
        List<Architectures> list = _titlesRepository.GetTitlesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTitlesWithPriceCount()
    {
        return _titlesRepository.GetTitlesWithPriceCount();
    }

    public Architectures GetTitlesById(string Id)
    {
        return _titlesRepository.GetTitlesById(Id);
    }

    public Architectures SumPowerTitlesPercent()
    {
        return _titlesRepository.SumPowerTitlesPercent();
    }

    public List<string> GetUniqueTitleId()
    {
        return _titlesRepository.GetUniqueTitleId();
    }
}
