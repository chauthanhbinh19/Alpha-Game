using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritBeastsService : ISpiritBeastsService
{
    private readonly ISpiritBeastsRepository _SpiritBeastRepository;

    public SpiritBeastsService(ISpiritBeastsRepository titleRepository)
    {
        _SpiritBeastRepository = titleRepository;
    }

    public static SpiritBeastsService Create()
    {
        return new SpiritBeastsService(new SpiritBeastsRepository());
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsAsync(int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _SpiritBeastRepository.GetSpiritBeastsAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsCountAsync(string rare)
    {
        return await _SpiritBeastRepository.GetSpiritBeastCountAsync(rare);
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsWithPriceAsync(int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _SpiritBeastRepository.GetSpiritBeastsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsWithPriceCountAsync()
    {
        return await _SpiritBeastRepository.GetSpiritBeastsWithPriceCountAsync();
    }

    public async Task<SpiritBeasts> GetSpiritBeastByIdAsync(string Id)
    {
        return await _SpiritBeastRepository.GetSpiritBeastByIdAsync(Id);
    }

    public async Task<SpiritBeasts> SumPowerSpiritBeastsPercentAsync()
    {
        return await _SpiritBeastRepository.SumPowerSpiritBeastsPercentAsync();
    }

    public async Task<List<string>> GetUniqueSpiritBeastsIdAsync()
    {
        return await _SpiritBeastRepository.GetUniqueSpiritBeastsIdAsync();
    }
}
