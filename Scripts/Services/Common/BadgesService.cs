using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<List<Badges>> GetBadgesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Badges> list = await _BadgesRepository.GetBadgesAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBadgesCountAsync(string search, string rare)
    {
        return await _BadgesRepository.GetBadgesCountAsync(search, rare);
    }

    public async Task<List<Badges>> GetBadgesWithPriceAsync(int pageSize, int offset)
    {
        List<Badges> list = await _BadgesRepository.GetBadgesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBadgesWithPriceCountAsync()
    {
        return await _BadgesRepository.GetBadgesWithPriceCountAsync();
    }

    public async Task<Badges> GetBadgeByIdAsync(string Id)
    {
        return await _BadgesRepository.GetBadgeByIdAsync(Id);
    }

    public async Task<Badges> SumPowerBadgesPercentAsync()
    {
        return await _BadgesRepository.SumPowerBadgesPercentAsync();
    }

    public async Task<List<string>> GetUniqueBadgesIdAsync()
    {
        return await _BadgesRepository.GetUniqueBadgesIdAsync();
    }
}
