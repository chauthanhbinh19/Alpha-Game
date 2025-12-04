using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesService
{
    Task<List<string>> GetUniqueBadgesIdAsync();
    Task<List<Badges>> GetBadgesAsync(int pageSize, int offset, string rare);
    Task<int> GetBadgesCountAsync(string rare);
    Task<List<Badges>> GetBadgesWithPriceAsync(int pageSize, int offset);
    Task<int> GetBadgesWithPriceCountAsync();
    Task<Badges> GetBadgeByIdAsync(string id);
    Task<Badges> SumPowerBadgesPercentAsync();
}
