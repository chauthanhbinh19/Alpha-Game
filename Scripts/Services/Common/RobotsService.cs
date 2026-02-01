using System.Collections.Generic;
using System.Threading.Tasks;

public class RobotsService : IRobotsService
{
    private static RobotsService _instance;
    private readonly IRobotsRepository _robotsRepository;

    public RobotsService(IRobotsRepository robotsRepository)
    {
        _robotsRepository = robotsRepository;
    }

    public static RobotsService Create()
    {
        if (_instance == null)
        {
            _instance = new RobotsService(new RobotsRepository());
        }
        return _instance;
    }

    public async Task<List<Robots>> GetRobotsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Robots> list = await _robotsRepository.GetRobotsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsCountAsync(string search, string rare)
    {
        return await _robotsRepository.GetRobotsCountAsync(search, rare);
    }

    public async Task<List<Robots>> GetRobotsWithPriceAsync(int pageSize, int offset)
    {
        List<Robots> list = await _robotsRepository.GetRobotsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsWithPriceCountAsync()
    {
        return await _robotsRepository.GetRobotsWithPriceCountAsync();
    }

    public async Task<Robots> GetRobotByIdAsync(string Id)
    {
        return await _robotsRepository.GetRobotByIdAsync(Id);
    }

    public async Task<Robots> SumPowerRobotsPercentAsync()
    {
        return await _robotsRepository.SumPowerRobotsPercentAsync();
    }

    public async Task<List<string>> GetUniqueRobotsIdAsync()
    {
        return await _robotsRepository.GetUniqueRobotsIdAsync();
    }
}
