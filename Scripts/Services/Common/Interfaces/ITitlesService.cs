using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesService
{
    Task<List<string>> GetUniqueTitlesIdAsync();
    Task<List<Titles>> GetTitlesAsync(string search, string rare, int pageSize, int offset);
    Task<List<Titles>> GetTitlesWithoutLimitAsync();
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertTitleAsync(Titles entity);
    Task<InsertOrUpdateResult<bool>> UpdateTitleAsync(Titles entity);
    Task<List<Titles>> GetTitlesWithPriceAsync(int pageSize, int offset);
    Task<int> GetTitlesWithPriceCountAsync();
    Task<Titles> GetTitleByIdAsync(string id);
    Task<Titles> SumPowerTitlesPercentAsync(string userId);
}
