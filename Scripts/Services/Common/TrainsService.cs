using System.Collections.Generic;
using System.Threading.Tasks;

public class TrainsService : ITrainsService
{
    private readonly ITrainsRepository _TrainsRepository;

    public TrainsService(ITrainsRepository titleRepository)
    {
        _TrainsRepository = titleRepository;
    }

    public static TrainsService Create()
    {
        return new TrainsService(new TrainsRepository());
    }

    public async Task<List<Trains>> GetTrainsAsync(string userId, int pageSize, int offset)
    {
        List<Trains> list = await _TrainsRepository.GetTrainsAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTrainsCountAsync(string rare)
    {
        return await _TrainsRepository.GetTrainsCountAsync(rare);
    }

    public async Task<List<Trains>> GetTrainsWithPriceAsync(int pageSize, int offset)
    {
        List<Trains> list = await _TrainsRepository.GetTrainsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTrainsWithPriceCountAsync()
    {
        return await _TrainsRepository.GetTrainsWithPriceCountAsync();
    }

    public async Task<Trains> GetTrainByIdAsync(string Id)
    {
        return await _TrainsRepository.GetTrainByIdAsync(Id);
    }

    public async Task<Trains> SumPowerTrainsPercentAsync()
    {
        return await _TrainsRepository.SumPowerTrainsPercentAsync();
    }

    public async Task<List<string>> GetUniqueTrainsIdAsync()
    {
        return await _TrainsRepository.GetUniqueTrainsIdAsync();
    }
}
