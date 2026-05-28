using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRunesRepository
{
    Task<List<string>> GetUniqueRunesIdAsync();
    Task<List<Runes>> GetRunesAsync(string search, string rare, int pageSize, int offset);
    Task<List<Runes>> GetRunesWithoutLimitAsync();
    Task<int> GetRunesCountAsync(string search, string rare);
    Task<List<Runes>> GetRunesWithPriceAsync(int pageSize, int offset);
    Task<int> GetRunesWithPriceCountAsync();
    Task<Runes> GetRuneByIdAsync(string id);
    Task<Runes> SumPowerRunesPercentAsync();
}
