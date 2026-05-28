using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritBeastsService : ISpiritBeastsService
{
    private static SpiritBeastsService _instance;
    private readonly ISpiritBeastsRepository _spiritBeastsRepository;

    public SpiritBeastsService(ISpiritBeastsRepository spiritBeastsRepository)
    {
        _spiritBeastsRepository = spiritBeastsRepository;
    }

    public static SpiritBeastsService Create()
    {
        if (_instance == null)
        {
            _instance = new SpiritBeastsService(new SpiritBeastsRepository());
        }
        return _instance;
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsAsync(string search, string rare, int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _spiritBeastsRepository.GetSpiritBeastsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsCountAsync(string search, string rare)
    {
        return await _spiritBeastsRepository.GetSpiritBeastCountAsync(search, rare);
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsWithPriceAsync(int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _spiritBeastsRepository.GetSpiritBeastsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsWithPriceCountAsync()
    {
        return await _spiritBeastsRepository.GetSpiritBeastsWithPriceCountAsync();
    }

    public async Task<SpiritBeasts> GetSpiritBeastByIdAsync(string Id)
    {
        return await _spiritBeastsRepository.GetSpiritBeastByIdAsync(Id);
    }

    public async Task<SpiritBeasts> SumPowerSpiritBeastsPercentAsync()
    {
        return await _spiritBeastsRepository.SumPowerSpiritBeastsPercentAsync();
    }

    public async Task<List<string>> GetUniqueSpiritBeastsIdAsync()
    {
        return await _spiritBeastsRepository.GetUniqueSpiritBeastsIdAsync();
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsWithoutLimitAsync()
    {
        return await _spiritBeastsRepository.GetSpiritBeastsWithoutLimitAsync();
    }
}
