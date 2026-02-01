using System.Collections.Generic;
using System.Threading.Tasks;

public class AlchemiesService : IAlchemiesService
{
    private static AlchemiesService _instance;
    private readonly IAlchemiesRepository _alchemyRepository;

    public AlchemiesService(IAlchemiesRepository alchemyRepository)
    {
        _alchemyRepository = alchemyRepository;
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
        return await _alchemyRepository.GetUniqueAlchemiesTypesAsync();
    }

    public async Task<List<Alchemies>> GetAlchemiesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Alchemies> list = await _alchemyRepository.GetAlchemiesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemiesCountAsync(string search, string type, string rare)
    {
        return await _alchemyRepository.GetAlchemiesCountAsync(search, type, rare);
    }

    public async Task<List<Alchemies>> GetAlchemiesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Alchemies> list = await _alchemyRepository.GetAlchemiesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemiesWithPriceCountAsync(string type)
    {
        return await _alchemyRepository.GetAlchemiesWithPriceCountAsync(type);
    }

    public async Task<Alchemies> GetAlchemyByIdAsync(string Id)
    {
        return await _alchemyRepository.GetAlchemyByIdAsync(Id);
    }

    public async Task<Alchemies> SumPowerAlchemiesPercentAsync()
    {
        return await _alchemyRepository.SumPowerAlchemiesPercentAsync();
    }

    public async Task<List<string>> GetUniqueAlchemiesIdAsync()
    {
        return await _alchemyRepository.GetUniqueAlchemiesIdAsync();
    }
}