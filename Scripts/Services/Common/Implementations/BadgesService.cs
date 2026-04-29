using System.Collections.Generic;
using System.Threading.Tasks;

public class BadgesService : IBadgesService
{
    private static BadgesService _instance;
    private readonly IBadgesRepository _badgesRepository;

    public BadgesService(IBadgesRepository badgesRepository)
    {
        _badgesRepository = badgesRepository;
    }

    public static BadgesService Create()
    {
        if (_instance == null)
        {
            _instance = new BadgesService(new BadgesRepository());
        }
        return _instance;
    }

    public async Task<List<Badges>> GetBadgesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Badges> list = await _badgesRepository.GetBadgesAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBadgesCountAsync(string search, string rare)
    {
        return await _badgesRepository.GetBadgesCountAsync(search, rare);
    }

    public async Task<List<Badges>> GetBadgesWithPriceAsync(int pageSize, int offset)
    {
        List<Badges> list = await _badgesRepository.GetBadgesWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBadgesWithPriceCountAsync()
    {
        return await _badgesRepository.GetBadgesWithPriceCountAsync();
    }

    public async Task<Badges> GetBadgeByIdAsync(string Id)
    {
        return await _badgesRepository.GetBadgeByIdAsync(Id);
    }

    public async Task<Badges> SumPowerBadgesPercentAsync()
    {
        return await _badgesRepository.SumPowerBadgesPercentAsync();
    }

    public async Task<List<string>> GetUniqueBadgesIdAsync()
    {
        return await _badgesRepository.GetUniqueBadgesIdAsync();
    }
}
