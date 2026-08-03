using System.Collections.Generic;
using System.Threading.Tasks;

public class BadgesService : IBadgesService
{
    private readonly IBadgesRepository _badgesRepository;

    public BadgesService(IBadgesRepository badgesRepository)
    {
        _badgesRepository = badgesRepository;
    }

    public static IBadgesService Create() => ServiceContainer.GetService<IBadgesService>();

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

    public async Task<InsertOrUpdateResult<bool>> InsertBadgeAsync(Badges entity)
    {
        var result = await _badgesRepository.InsertBadgeAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBadgeAsync(Badges entity)
    {
        var result = await _badgesRepository.UpdateBadgeAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
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

    public async Task<Badges> SumPowerBadgesPercentAsync(string userId)
    {
        return await _badgesRepository.SumPowerBadgesPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueBadgesIdAsync()
    {
        return await _badgesRepository.GetUniqueBadgesIdAsync();
    }

    public async Task<List<Badges>> GetBadgesWithoutLimitAsync()
    {
        return await _badgesRepository.GetBadgesWithoutLimitAsync();
    }
}
