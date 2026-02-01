using System.Collections.Generic;
using System.Threading.Tasks;
public class RelicsService : IRelicsService
{
    private static RelicsService _instance;
    private readonly IRelicsRepository _relicsRepository;

    public RelicsService(IRelicsRepository relicsRepository)
    {
        _relicsRepository = relicsRepository;
    }

    public static RelicsService Create()
    {
        if (_instance == null)
        {
            _instance = new RelicsService(new RelicsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueRelicsTypesAsync()
    {
        return await _relicsRepository.GetUniqueRelicsTypesAsync();
    }

    public async Task<List<Relics>> GetRelicsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Relics> list = await _relicsRepository.GetRelicsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRelicsCountAsync(string search, string type, string rare)
    {
        return await _relicsRepository.GetRelicsCountAsync(search, type, rare);
    }

    public async Task<List<Relics>> GetRelicsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Relics> list = await _relicsRepository.GetRelicsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRelicsWithPriceCountAsync(string type)
    {
        return await _relicsRepository.GetRelicsWithPriceCountAsync(type);
    }

    public async Task<Relics> GetRelicByIdAsync(string Id)
    {
        return await _relicsRepository.GetRelicByIdAsync(Id);
    }

    public async Task<Relics> SumPowerRelicsPercentAsync()
    {
        return await _relicsRepository.SumPowerRelicsPercentAsync();
    }

    public async Task<List<string>> GetUniqueRelicsIdAsync()
    {
        return await _relicsRepository.GetUniqueRelicsIdAsync();
    }
}
