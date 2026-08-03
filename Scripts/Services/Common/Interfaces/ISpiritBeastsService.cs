using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritBeastsService
{
    Task<List<string>> GetUniqueSpiritBeastsIdAsync();
    Task<List<SpiritBeasts>> GetSpiritBeastsAsync(string search, string rare, int pageSize, int offset);
    Task<List<SpiritBeasts>> GetSpiritBeastsWithoutLimitAsync();
    Task<int> GetSpiritBeastsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertSpiritBeastAsync(SpiritBeasts entity);
    Task<InsertOrUpdateResult<bool>> UpdateSpiritBeastAsync(SpiritBeasts entity);
    Task<List<SpiritBeasts>> GetSpiritBeastsWithPriceAsync(int pageSize, int offset);
    Task<int> GetSpiritBeastsWithPriceCountAsync();
    Task<SpiritBeasts> GetSpiritBeastByIdAsync(string Id);
    Task<SpiritBeasts> SumPowerSpiritBeastsPercentAsync(string userId);
}
