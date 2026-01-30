using System.Collections.Generic;
using System.Threading.Tasks;

public class RobotsService : IRobotsService
{
    private readonly IRobotsRepository _RobotsRepository;

    public RobotsService(IRobotsRepository titleRepository)
    {
        _RobotsRepository = titleRepository;
    }

    public static RobotsService Create()
    {
        return new RobotsService(new RobotsRepository());
    }

    public async Task<List<Robots>> GetRobotsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Robots> list = await _RobotsRepository.GetRobotsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsCountAsync(string search, string rare)
    {
        return await _RobotsRepository.GetRobotsCountAsync(search, rare);
    }

    public async Task<List<Robots>> GetRobotsWithPriceAsync(int pageSize, int offset)
    {
        List<Robots> list = await _RobotsRepository.GetRobotsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsWithPriceCountAsync()
    {
        return await _RobotsRepository.GetRobotsWithPriceCountAsync();
    }

    public async Task<Robots> GetRobotByIdAsync(string Id)
    {
        return await _RobotsRepository.GetRobotByIdAsync(Id);
    }

    public async Task<Robots> SumPowerRobotsPercentAsync()
    {
        return await _RobotsRepository.SumPowerRobotsPercentAsync();
    }

    public async Task<List<string>> GetUniqueRobotsIdAsync()
    {
        return await _RobotsRepository.GetUniqueRobotsIdAsync();
    }
}
