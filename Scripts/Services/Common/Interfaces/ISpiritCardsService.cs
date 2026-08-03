using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritCardsService
{
    Task<List<string>> GetUniqueSpiritCardsTypesAsync();
    Task<List<string>> GetUniqueSpiritCardsIdAsync();
    Task<List<SpiritCards>> GetSpiritCardsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<SpiritCards>> GetSpiritCardsWithoutLimitAsync();
    Task<int> GetSpiritCardsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertSpiritCardAsync(SpiritCards entity);
    Task<InsertOrUpdateResult<bool>> UpdateSpiritCardAsync(SpiritCards entity);
    Task<List<SpiritCards>> GetSpiritCardsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetSpiritCardsWithPriceCountAsync(string type);
    Task<SpiritCards> GetSpiritCardByIdAsync(string Id);
    Task<SpiritCards> SumPowerSpiritCardsPercentAsync(string userId);
}
