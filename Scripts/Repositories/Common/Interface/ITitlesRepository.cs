using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesRepository
{
    Task<List<string>> GetUniqueTitlesIdAsync();
    Task<List<Titles>> GetTitlesAsync(int pageSize, int offset, string rare);
    Task<int> GetTitlesCountAsync(string rare);
    Task<List<Titles>> GetTitlesWithPriceAsync(int pageSize, int offset);
    Task<int> GetTitlesWithPriceCountAsync();
    Task<Titles> GetTitleByIdAsync(string id);
    Task<Titles> SumPowerTitlesPercentAsync();
}
