using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRobotsService : IUserRobotsService
{
    private readonly IUserRobotsRepository _userRobotsRepository;
    private readonly IRobotsGalleryService _robotsGalleryService;
    private readonly IRobotsService _robotsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserRobotsService(
        IUserRobotsRepository userRobotsRepository,
        IRobotsGalleryService robotsGalleryService,
        IRobotsService robotsService,
        IPowerManagerService powerManagerService)
    {
        _userRobotsRepository = userRobotsRepository;
        _robotsGalleryService = robotsGalleryService;
        _robotsService = robotsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserRobotsService Create() => ServiceContainer.GetService<IUserRobotsService>();

    public async Task<List<Robots>> GetUserRobotsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Robots> list = await _userRobotsRepository.GetUserRobotsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRobotsCountAsync(string userId, string search, string rare)
    {
        return await _userRobotsRepository.GetUserRobotsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotAsync(string userId, Robots robot)
    {
        Robots oldRobot = await _robotsService.SumPowerRobotsPercentAsync(userId);
        var insertOrUpdateResult = await _userRobotsRepository.InsertOrUpdateUserRobotAsync(userId, robot);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _robotsGalleryService.InsertRobotGalleryAsync(userId, robot.Id);

        Robots newRobot = await _robotsService.SumPowerRobotsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newRobot - (PowerManager)oldRobot;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotsBatchAsync(string userId, List<Robots> robotes)
    {
        Robots oldRobot = await _robotsService.SumPowerRobotsPercentAsync(userId);
        var repositoryResult = await _userRobotsRepository.InsertOrUpdateUserRobotsBatchAsync(userId, robotes);

        // 1. Kiểm tra Null hoặc nếu Repository trả về không thành công
        if (repositoryResult?.Data == null || !repositoryResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        // 2. Gộp logic xử lý Gallery nếu có thẻ mới được Insert (dùng cho cả Inserted và Mixed)
        var newlyInsertedCards = repositoryResult.Data.InsertedItems;
        if (newlyInsertedCards != null && newlyInsertedCards.Count > 0)
        {
            await _robotsGalleryService.InsertBatchRobotsGalleryAsync(userId, newlyInsertedCards);
        }

        Robots newRobot = await _robotsService.SumPowerRobotsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newRobot - (PowerManager)oldRobot;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        // 3. Mapping kết quả OperationType trả về gọn gàng
        return repositoryResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserRobotLevelAsync(string userId, Robots robot)
    {
        var updateResult = await _userRobotsRepository.UpdateUserRobotLevelAsync(userId, robot);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserRobotStarAsync(string userId, Robots robot)
    {
        var updateResult = await _userRobotsRepository.UpdateUserRobotStarAsync(userId, robot);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _robotsGalleryService.UpdateTempStarRobotGalleryAsync(userId, robot.Id, robot.Star);

        return true;
    }

    public async Task<Robots> GetUserRobotByIdAsync(string userId, string Id)
    {
        var result = await _userRobotsRepository.GetUserRobotByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Robots> SumPowerUserRobotsAsync(string userId)
    {
        return await _userRobotsRepository.SumPowerUserRobotsAsync(userId);
    }
}
