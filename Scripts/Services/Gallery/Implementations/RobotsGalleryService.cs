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

    public async Task<bool> InsertRobotGalleryAsync(string userId, string Id)
    {
        var insertResult = await _robotsGalleryRepository.InsertRobotGalleryAsync(userId, Id, await _robotsService.GetRobotByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusRobotGalleryAsync(string userId, string robotId)
    {
        var updateResult = await _robotsGalleryRepository.UpdateStatusRobotGalleryAsync(userId, robotId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Robots robotGallery = await GetRobotCollectionByIdAsync(userId, robotId) ?? new Robots();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)robotGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusRobotsGalleryAsync(string userId)
    {
        Robots oldRobot = await SumPowerRobotsGalleryAsync(userId);

        var updateResult = await _robotsGalleryRepository.UpdateBatchStatusRobotsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Robots newRobot = await SumPowerRobotsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRobot - (PowerManager)oldRobot;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Robots> SumPowerRobotsGalleryAsync(string userId)
    {
        return await _robotsGalleryRepository.SumPowerRobotsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarRobotGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _robotsGalleryRepository.UpdateTempStarRobotGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarRobotGalleryAsync(string userId, string robotId)
    {
        Robots oldRobot = await GetRobotCollectionByIdAsync(userId, robotId) ?? new Robots();

        var updateResult = await _robotsGalleryRepository.UpdateCurrentStarRobotGalleryAsync(userId, robotId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Robots newRobot = await GetRobotCollectionByIdAsync(userId, robotId) ?? new Robots();
        PowerManager deltaPower = (PowerManager)newRobot - (PowerManager)oldRobot;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarRobotsGalleryAsync(string userId)
    {
        Robots oldRobot = await SumPowerRobotsGalleryAsync(userId);

        var updateResult = await _robotsGalleryRepository.UpdateBatchCurrentStarRobotsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Robots newRobot = await SumPowerRobotsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRobot - (PowerManager)oldRobot;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchRobotsGalleryAsync(string userId, List<Robots> robots)
    {
        var insertResult = await _robotsGalleryRepository.InsertBatchRobotsGalleryAsync(userId, robots);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Robots> GetRobotCollectionByIdAsync(string userId, string robotId)
    {
        var result = await _robotsGalleryRepository.GetRobotCollectionByIdAsync(userId, robotId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateRobotGalleryPowerAsync(string userId, string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        await _robotsGalleryRepository.UpdateRobotGalleryPowerAsync(userId, Id, await _service.GetRobotByIdAsync(Id));
    }
}
