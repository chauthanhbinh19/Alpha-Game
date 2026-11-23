using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRobotsService : IUserRobotsService
{
    private readonly IUserRobotsRepository _userRobotsRepository;
    private readonly IRobotsGalleryService _robotsGalleryService;
    private readonly IRobotsService _robotsService;

    public UserRobotsService(
        IUserRobotsRepository userRobotsRepository,
        IRobotsGalleryService robotsGalleryService,
        IRobotsService robotsService)
    {
        _userRobotsRepository = userRobotsRepository;
        _robotsGalleryService = robotsGalleryService;
        _robotsService = robotsService;
    }

    public static IUserRobotsService Create() => ServiceContainer.GetService<IUserRobotsService>();

    public async Task<List<Robots>> GetUserRobotsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Robots> result = await _userRobotsRepository.GetUserRobotsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserRobotsCountAsync(string userId, string search, string rare)
    {
        return await _userRobotsRepository.GetUserRobotsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotAsync(string userId, Robots robot)
    {
        var checkRobotResult = await _robotsService.IsRobotDeletedOrInactiveAsync(robot.Id);
        if (checkRobotResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userRobotsRepository.InsertOrUpdateUserRobotAsync(userId, robot);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = true,
                OperationType = DatabaseOperationType.Updated,
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        await _robotsGalleryService.InsertRobotGalleryAsync(userId, robot.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotsBatchAsync(string userId, List<Robots> robots)
    {
        var insertOrUpdateResult = await _userRobotsRepository.InsertOrUpdateUserRobotsBatchAsync(userId, robots);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _robotsGalleryService.InsertBatchRobotsGalleryAsync(userId, newlyInsertedCards);
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
                IsChangePower = false,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRobotLevelAsync(string userId, Robots robot, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (robot == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Robot is required.");
        }

        var checkResult = await _robotsService.IsRobotDeletedOrInactiveAsync(robot.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userRobotsRepository.UpdateUserRobotLevelAsync(
            userId,
            robot,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRobotStarAsync(string userId, Robots robot)
    {
        var checkRobotResult = await _robotsService.IsRobotDeletedOrInactiveAsync(robot.Id);
        if (checkRobotResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userRobotsRepository.UpdateUserRobotStarAsync(userId, robot);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRobotQuantityAsync(string userId, Robots robot, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userRobotsRepository.UpdateUserRobotQuantityAsync(userId, robot, totalDeductions);

        return result;
    }

    public async Task<Robots> GetUserRobotByIdAsync(string userId, string Id)
    {
        var result = await _userRobotsRepository.GetUserRobotByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Robots> SumPowerUserRobotsAsync(string userId)
    {
        return await _userRobotsRepository.SumPowerUserRobotsAsync(userId);
    }
}
