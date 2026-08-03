using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesService
{
    Task<List<string>> GetUniqueBadgesIdAsync();
    Task<List<Badges>> GetBadgesAsync(string search, string rare, int pageSize, int offset);
    Task<List<Badges>> GetBadgesWithoutLimitAsync();
    Task<int> GetBadgesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertBadgeAsync(Badges entity);
    Task<InsertOrUpdateResult<bool>> UpdateBadgeAsync(Badges entity);
    Task<List<Badges>> GetBadgesWithPriceAsync(int pageSize, int offset);
    Task<int> GetBadgesWithPriceCountAsync();
    Task<Badges> GetBadgeByIdAsync(string id);
    Task<Badges> SumPowerBadgesPercentAsync(string userId);
}
