using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritCardsService : ISpiritCardsService
{
    private static SpiritCardsService _instance;
    private readonly ISpiritCardsRepository _spiritCardsRepository;

    public SpiritCardsService(ISpiritCardsRepository spiritCardsRepository)
    {
        _spiritCardsRepository = spiritCardsRepository;
    }

    public static SpiritCardsService Create()
    {
        if (_instance == null)
        {
            _instance = new SpiritCardsService(new SpiritCardsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueSpiritCardsTypesAsync()
    {
        return await _spiritCardsRepository.GetUniqueSpiritCardsTypesAsync();
    }

    public async Task<List<SpiritCards>> GetSpiritCardsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<SpiritCards> list = await _spiritCardsRepository.GetSpiritCardsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritCardsCountAsync(string search, string type, string rare)
    {
        return await _spiritCardsRepository.GetSpiritCardsCountAsync(search, type, rare);
    }

    public async Task<List<SpiritCards>> GetSpiritCardsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<SpiritCards> list = await _spiritCardsRepository.GetSpiritCardsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritCardsWithPriceCountAsync(string type)
    {
        return await _spiritCardsRepository.GetSpiritCardsWithPriceCountAsync(type);
    }

    public async Task<SpiritCards> GetSpiritCardByIdAsync(string Id)
    {
        return await _spiritCardsRepository.GetSpiritCardByIdAsync(Id);
    }

    public async Task<SpiritCards> SumPowerSpiritCardsPercentAsync()
    {
        return await _spiritCardsRepository.SumPowerSpiritCardsPercentAsync();
    }

    public async Task<List<string>> GetUniqueSpiritCardsIdAsync()
    {
        return await _spiritCardsRepository.GetUniqueSpiritCardsIdAsync();
    }
}
