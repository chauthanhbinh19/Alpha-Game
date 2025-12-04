using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRunesService
{
    Task<List<string>> GetUniqueRunesIdAsync();
    Task<List<Runes>> GetRunesAsync(int pageSize, int offset, string rare);
    Task<int> GetRunesCountAsync(string rare);
    Task<List<Runes>> GetRunesWithPriceAsync(int pageSize, int offset);
    Task<int> GetRunesWithPriceCountAsync();
    Task<Runes> GetRuneByIdAsync(string id);
    Task<Runes> SumPowerRunesPercentAsync();
}
