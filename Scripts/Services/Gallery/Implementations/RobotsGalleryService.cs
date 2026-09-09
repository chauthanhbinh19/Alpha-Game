using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RobotsGalleryService : IRobotsGalleryService
{
    private readonly IRobotsGalleryRepository _robotsGalleryRepository;
    private readonly IRobotsService _robotsService;
    private readonly IPowerManagerService _powerManagerService;

    public RobotsGalleryService(
        IRobotsGalleryRepository robotsGalleryRepository,
        IRobotsService robotsService,
        IPowerManagerService powerManagerService)
    {
        _robotsGalleryRepository = robotsGalleryRepository;
        _robotsService = robotsService;
        _powerManagerService = powerManagerService;
    }

    public static IRobotsGalleryService Create() => ServiceContainer.GetService<IRobotsGalleryService>();

    public async Task<List<Robots>> GetRobotsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Robots> list = await _robotsGalleryRepository.GetRobotsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsCountAsync(string search, string rare)
    {
        return await _robotsGalleryRepository.GetRobotsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertRobotGalleryAsync(string userId, string Id)
    {
        var checkResult = await _robotsService.IsRobotDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _robotsGalleryRepository.InsertRobotGalleryAsync(userId, Id, await _robotsService.GetRobotByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusRobotGalleryAsync(string userId, string robotId)
    {
        var checkResult = await _robotsService.IsRobotDeletedOrInactiveAsync(robotId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _robotsGalleryRepository.UpdateStatusRobotGalleryAsync(userId, robotId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Robots robotGallery = await GetRobotCollectionByIdAsync(userId, robotId) ?? new Robots();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)robotGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusRobotsGalleryAsync(string userId)
    {
        Robots oldRobot = await SumPowerRobotsGalleryAsync(userId);

        var updateResult = await _robotsGalleryRepository.UpdateBatchStatusRobotsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Robots newRobot = await SumPowerRobotsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRobot - (PowerManager)oldRobot;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Robots> SumPowerRobotsGalleryAsync(string userId)
    {
        return await _robotsGalleryRepository.SumPowerRobotsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarRobotGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _robotsService.IsRobotDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _robotsGalleryRepository.UpdateTempStarRobotGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarRobotGalleryAsync(string userId, string robotId)
    {
        var checkResult = await _robotsService.IsRobotDeletedOrInactiveAsync(robotId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Robots oldRobot = await GetRobotCollectionByIdAsync(userId, robotId) ?? new Robots();

        var updateResult = await _robotsGalleryRepository.UpdateCurrentStarRobotGalleryAsync(userId, robotId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Robots newRobot = await GetRobotCollectionByIdAsync(userId, robotId) ?? new Robots();
        PowerManager deltaPower = (PowerManager)newRobot - (PowerManager)oldRobot;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarRobotsGalleryAsync(string userId)
    {
        Robots oldRobot = await SumPowerRobotsGalleryAsync(userId);

        var updateResult = await _robotsGalleryRepository.UpdateBatchCurrentStarRobotsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Robots newRobot = await SumPowerRobotsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRobot - (PowerManager)oldRobot;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchRobotsGalleryAsync(string userId, List<Robots> robots)
    {
        var insertResult = await _robotsGalleryRepository.InsertBatchRobotsGalleryAsync(userId, robots);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Robots> GetRobotCollectionByIdAsync(string userId, string robotId)
    {
        var result = await _robotsGalleryRepository.GetRobotCollectionByIdAsync(userId, robotId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateRobotGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _robotsService.IsRobotDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        await _robotsGalleryRepository.UpdateRobotGalleryPowerAsync(userId, Id, await _service.GetRobotByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
