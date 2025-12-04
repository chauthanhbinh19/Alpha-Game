using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISpiritCardsService
{
    Task<List<string>> GetUniqueSpiritCardsTypesAsync();
    Task<List<string>> GetUniqueSpiritCardsIdAsync();
    Task<List<SpiritCards>> GetSpiritCardsAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetSpiritCardsCountAsync(string type, string rare);
    Task<List<SpiritCards>> GetSpiritCardsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetSpiritCardsWithPriceCountAsync(string type);
    Task<SpiritCards> GetSpiritCardByIdAsync(string Id);
    Task<SpiritCards> SumPowerSpiritCardsPercentAsync();
}
