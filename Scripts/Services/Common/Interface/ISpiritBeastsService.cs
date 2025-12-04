using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsService
{
    Task<List<string>> GetUniqueSpiritBeastsIdAsync();
    Task<List<SpiritBeasts>> GetSpiritBeastsAsync(int pageSize, int offset, string rare);
    Task<int> GetSpiritBeastsCountAsync(string rare);
    Task<List<SpiritBeasts>> GetSpiritBeastsWithPriceAsync(int pageSize, int offset);
    Task<int> GetSpiritBeastsWithPriceCountAsync();
    Task<SpiritBeasts> GetSpiritBeastByIdAsync(string Id);
    Task<SpiritBeasts> SumPowerSpiritBeastsPercentAsync();
}
