using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesRepository
{
    Task<List<string>> GetUniqueTitlesIdAsync();
    Task<List<Titles>> GetTitlesAsync(string search, string rare, int pageSize, int offset);
    Task<List<Titles>> GetTitlesWithoutLimitAsync();
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task<List<Titles>> GetTitlesWithPriceAsync(int pageSize, int offset);
    Task<int> GetTitlesWithPriceCountAsync();
    Task<Titles> GetTitleByIdAsync(string id);
    Task<Titles> SumPowerTitlesPercentAsync();
}
