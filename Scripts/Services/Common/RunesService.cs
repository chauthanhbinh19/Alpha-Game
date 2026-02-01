using System.Collections.Generic;
using System.Threading.Tasks;

public class RunesService : IRunesService
{
    private static RunesService _instance;
    private readonly IRunesRepository _runesRepository;

    public RunesService(IRunesRepository runesRepository)
    {
        _runesRepository = runesRepository;
    }

    public static RunesService Create()
    {
        if (_instance == null)
        {
            _instance = new RunesService(new RunesRepository());
        }
        return _instance;
    }

    public async Task<List<Runes>> GetRunesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Runes> list = await _runesRepository.GetRunesAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesCountAsync(string search, string rare)
    {
        return await _runesRepository.GetRunesCountAsync(search, rare);
    }

    public async Task<List<Runes>> GetRunesWithPriceAsync(int pageSize, int offset)
    {
        List<Runes> list = await _runesRepository.GetRunesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesWithPriceCountAsync()
    {
        return await _runesRepository.GetRunesWithPriceCountAsync();
    }

    public async Task<Runes> GetRuneByIdAsync(string Id)
    {
        return await _runesRepository.GetRuneByIdAsync(Id);
    }

    public async Task<Runes> SumPowerRunesPercentAsync()
    {
        return await _runesRepository.SumPowerRunesPercentAsync();
    }

    public async Task<List<string>> GetUniqueRunesIdAsync()
    {
        return await _runesRepository.GetUniqueRunesIdAsync();
    }
}
