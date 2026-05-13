using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsRepository
{
    Task<List<string>> GetUniqueSpiritBeastsIdAsync();
    Task<List<SpiritBeasts>> GetSpiritBeastsAsync(string search, string rare, int pageSize, int offset);
    Task<List<SpiritBeasts>> GetSpiritBeastsWithoutLimitAsync();
    Task<int> GetSpiritBeastCountAsync(string search, string rare);
    Task<List<SpiritBeasts>> GetSpiritBeastsWithPriceAsync(int pageSize, int offset);
    Task<int> GetSpiritBeastsWithPriceCountAsync();
    Task<SpiritBeasts> GetSpiritBeastByIdAsync(string Id);
    Task<SpiritBeasts> SumPowerSpiritBeastsPercentAsync();
}
