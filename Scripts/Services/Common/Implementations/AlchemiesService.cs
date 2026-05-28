using System.Collections.Generic;
using System.Threading.Tasks;

public class AlchemiesService : IAlchemiesService
{
    private static AlchemiesService _instance;
    private readonly IAlchemiesRepository _alchemiesRepository;

    public AlchemiesService(IAlchemiesRepository alchemiesRepository)
    {
        _alchemiesRepository = alchemiesRepository;
    }

    public static AlchemiesService Create()
    {
        if (_instance == null)
        {
            _instance = new AlchemiesService(new AlchemiesRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueAlchemiesTypesAsync()
    {
        return await _alchemiesRepository.GetUniqueAlchemiesTypesAsync();
    }

    public async Task<List<Alchemies>> GetAlchemiesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Alchemies> list = await _alchemiesRepository.GetAlchemiesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemiesCountAsync(string search, string type, string rare)
    {
        return await _alchemiesRepository.GetAlchemiesCountAsync(search, type, rare);
    }

    public async Task<List<Alchemies>> GetAlchemiesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Alchemies> list = await _alchemiesRepository.GetAlchemiesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemiesWithPriceCountAsync(string type)
    {
        return await _alchemiesRepository.GetAlchemiesWithPriceCountAsync(type);
    }

    public async Task<Alchemies> GetAlchemyByIdAsync(string Id)
    {
        return await _alchemiesRepository.GetAlchemyByIdAsync(Id);
    }

    public async Task<Alchemies> SumPowerAlchemiesPercentAsync()
    {
        return await _alchemiesRepository.SumPowerAlchemiesPercentAsync();
    }

    public async Task<List<string>> GetUniqueAlchemiesIdAsync()
    {
        return await _alchemiesRepository.GetUniqueAlchemiesIdAsync();
    }

    public async Task<List<Alchemies>> GetAlchemiesWithoutLimitAsync()
    {
        return await _alchemiesRepository.GetAlchemiesWithoutLimitAsync();
    }
}