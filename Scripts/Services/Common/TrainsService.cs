using System.Collections.Generic;
using System.Threading.Tasks;

public class TrainsService : ITrainsService
{
    private static TrainsService _instance;
    private readonly ITrainsRepository _trainsRepository;

    public TrainsService(ITrainsRepository trainsRepository)
    {
        _trainsRepository = trainsRepository;
    }

    public static TrainsService Create()
    {
        if (_instance == null)
        {
            _instance = new TrainsService(new TrainsRepository());
        }
        return _instance;
    }

    public async Task<List<Trains>> GetTrainsAsync(string userId, int pageSize, int offset)
    {
        List<Trains> list = await _trainsRepository.GetTrainsAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTrainsCountAsync(string rare)
    {
        return await _trainsRepository.GetTrainsCountAsync(rare);
    }

    public async Task<List<Trains>> GetTrainsWithPriceAsync(int pageSize, int offset)
    {
        List<Trains> list = await _trainsRepository.GetTrainsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTrainsWithPriceCountAsync()
    {
        return await _trainsRepository.GetTrainsWithPriceCountAsync();
    }

    public async Task<Trains> GetTrainByIdAsync(string Id)
    {
        return await _trainsRepository.GetTrainByIdAsync(Id);
    }

    public async Task<Trains> SumPowerTrainsPercentAsync()
    {
        return await _trainsRepository.SumPowerTrainsPercentAsync();
    }

    public async Task<List<string>> GetUniqueTrainsIdAsync()
    {
        return await _trainsRepository.GetUniqueTrainsIdAsync();
    }
}
