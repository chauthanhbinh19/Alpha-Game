using System.Collections.Generic;

public class BadgesService : IBadgesService
{
    private readonly IBadgesRepository _BadgesRepository;

    public BadgesService(IBadgesRepository titleRepository)
    {
        _BadgesRepository = titleRepository;
    }

    public static BadgesService Create()
    {
        return new BadgesService(new BadgesRepository());
    }

    public List<Badges> GetBadges(int pageSize, int offset, string rare)
    {
        List<Badges> list = _BadgesRepository.GetBadges(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBadgesCount(string rare)
    {
        return _BadgesRepository.GetBadgesCount(rare);
    }

    public List<Badges> GetBadgesWithPrice(int pageSize, int offset)
    {
        List<Badges> list = _BadgesRepository.GetBadgesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBadgesWithPriceCount()
    {
        return _BadgesRepository.GetBadgesWithPriceCount();
    }

    public Badges GetBadgesById(string Id)
    {
        return _BadgesRepository.GetBadgesById(Id);
    }

    public Badges SumPowerBadgesPercent()
    {
        return _BadgesRepository.SumPowerBadgesPercent();
    }

    public List<string> GetUniqueBadgeId()
    {
        return _BadgesRepository.GetUniqueBadgeId();
    }
}
