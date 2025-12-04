using System.Collections.Generic;
using System.Threading.Tasks;

public class RunesService : IRunesService
{
    private readonly IRunesRepository _RunesRepository;

    public RunesService(IRunesRepository titleRepository)
    {
        _RunesRepository = titleRepository;
    }

    public static RunesService Create()
    {
        return new RunesService(new RunesRepository());
    }

    public async Task<List<Runes>> GetRunesAsync(int pageSize, int offset, string rare)
    {
        List<Runes> list = await _RunesRepository.GetRunesAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesCountAsync(string rare)
    {
        return await _RunesRepository.GetRunesCountAsync(rare);
    }

    public async Task<List<Runes>> GetRunesWithPriceAsync(int pageSize, int offset)
    {
        List<Runes> list = await _RunesRepository.GetRunesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesWithPriceCountAsync()
    {
        return await _RunesRepository.GetRunesWithPriceCountAsync();
    }

    public async Task<Runes> GetRuneByIdAsync(string Id)
    {
        return await _RunesRepository.GetRuneByIdAsync(Id);
    }

    public async Task<Runes> SumPowerRunesPercentAsync()
    {
        return await _RunesRepository.SumPowerRunesPercentAsync();
    }

    public async Task<List<string>> GetUniqueRunesIdAsync()
    {
        return await _RunesRepository.GetUniqueRunesIdAsync();
    }
}
