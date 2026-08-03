using System.Collections.Generic;
using System.Threading.Tasks;

public class RobotsService : IRobotsService
{
    private readonly IRobotsRepository _robotsRepository;

    public RobotsService(IRobotsRepository robotsRepository)
    {
        _robotsRepository = robotsRepository;
    }

    public static IRobotsService Create() => ServiceContainer.GetService<IRobotsService>();

    public async Task<List<Robots>> GetRobotsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Robots> list = await _robotsRepository.GetRobotsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsCountAsync(string search, string rare)
    {
        return await _robotsRepository.GetRobotsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertRobotAsync(Robots entity)
    {
        var result = await _robotsRepository.InsertRobotAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateRobotAsync(Robots entity)
    {
        var result = await _robotsRepository.UpdateRobotAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Robots>> GetRobotsWithPriceAsync(int pageSize, int offset)
    {
        List<Robots> list = await _robotsRepository.GetRobotsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
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

    public async Task<Robots> SumPowerRobotsPercentAsync(string userId)
    {
        return await _robotsRepository.SumPowerRobotsPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueRobotsIdAsync()
    {
        return await _robotsRepository.GetUniqueRobotsIdAsync();
    }

    public async Task<List<Robots>> GetRobotsWithoutLimitAsync()
    {
        return await _robotsRepository.GetRobotsWithoutLimitAsync();
    }
}
