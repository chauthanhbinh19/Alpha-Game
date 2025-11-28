using System.Collections.Generic;

public interface IBadgesService
{
    List<string> GetUniqueBadgeId();
    List<Badges> GetBadges(int pageSize, int offset, string rare);
    int GetBadgesCount(string rare);
    List<Badges> GetBadgesWithPrice(int pageSize, int offset);
    int GetBadgesWithPriceCount();
    Badges GetBadgesById(string Id);
    Badges SumPowerBadgesPercent();
}
