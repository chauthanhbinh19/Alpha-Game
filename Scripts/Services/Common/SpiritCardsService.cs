using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritCardsService : ISpiritCardsService
{
    private readonly ISpiritCardsRepository _SpiritCardRepository;

    public SpiritCardsService(ISpiritCardsRepository titleRepository)
    {
        _SpiritCardRepository = titleRepository;
    }

    public static SpiritCardsService Create()
    {
        return new SpiritCardsService(new SpiritCardsRepository());
    }

    public async Task<List<string>> GetUniqueSpiritCardsTypesAsync()
    {
        return await _SpiritCardRepository.GetUniqueSpiritCardsTypesAsync();
    }

    public async Task<List<SpiritCards>> GetSpiritCardsAsync(string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _SpiritCardRepository.GetSpiritCardsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritCardsCountAsync(string type, string rare)
    {
        return await _SpiritCardRepository.GetSpiritCardsCountAsync(type, rare);
    }

    public async Task<List<SpiritCards>> GetSpiritCardsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<SpiritCards> list = await _SpiritCardRepository.GetSpiritCardsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritCardsWithPriceCountAsync(string type)
    {
        return await _SpiritCardRepository.GetSpiritCardsWithPriceCountAsync(type);
    }

    public async Task<SpiritCards> GetSpiritCardByIdAsync(string Id)
    {
        return await _SpiritCardRepository.GetSpiritCardByIdAsync(Id);
    }

    public async Task<SpiritCards> SumPowerSpiritCardsPercentAsync()
    {
        return await _SpiritCardRepository.SumPowerSpiritCardsPercentAsync();
    }

    public async Task<List<string>> GetUniqueSpiritCardsIdAsync()
    {
        return await _SpiritCardRepository.GetUniqueSpiritCardsIdAsync();
    }
}
