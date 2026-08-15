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
        List<Robots> result = await _userRobotsRepository.GetUserRobotsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserRobotsCountAsync(string userId, string search, string rare)
    {
        return await _userRobotsRepository.GetUserRobotsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotAsync(string userId, Robots robot)
    {
        var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        Robots oldRobot = oldRobotTask.Result;
        Robots oldUserRobot = oldUserRobotTask.Result;

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

        var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        await Task.WhenAll(newRobotTask, newUserRobotTask);

        PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotsBatchAsync(string userId, List<Robots> robots)
    {
        var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        Robots oldRobot = oldRobotTask.Result;
        Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userRobotsRepository.InsertOrUpdateUserRobotsBatchAsync(userId, robots);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _robotsGalleryService.InsertBatchRobotsGalleryAsync(userId, newlyInsertedCards);

            var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
            var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

            await Task.WhenAll(newRobotTask, newUserRobotTask);

            PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
            PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

            PowerManager totalDelta = new PowerManager();
            if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
            if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

            if (totalDelta.HasAnyPositiveStat())
            {
                PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
                PowerManager updatedPower = currentPower + totalDelta;
                await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
            }
        }

        return insertOrUpdateResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserRobotLevelAsync(string userId, Robots robot)
    {
        Robots oldUserRobot = await _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        var updateResult = await _userRobotsRepository.UpdateUserRobotLevelAsync(userId, robot);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Robots newUserRobot = await _userRobotsRepository.SumPowerUserRobotsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserRobot - (PowerManager)oldUserRobot;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserRobotStarAsync(string userId, Robots robot)
    {
        Robots oldUserRobot = await _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        var updateResult = await _userRobotsRepository.UpdateUserRobotStarAsync(userId, robot);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _robotsGalleryService.UpdateTempStarRobotGalleryAsync(userId, robot.Id, robot.Star);

        Robots newUserRobot = await _userRobotsRepository.SumPowerUserRobotsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserRobot - (PowerManager)oldUserRobot;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Robots> GetUserRobotByIdAsync(string userId, string Id)
    {
        var result = await _userRobotsRepository.GetUserRobotByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Robots> SumPowerUserRobotsAsync(string userId)
    {
        return await _userRobotsRepository.SumPowerUserRobotsAsync(userId);
    }
}
