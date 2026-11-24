using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTechnologiesService : IUserTechnologiesService
{
    private readonly IUserTechnologiesRepository _userTechnologiesRepository;
    private readonly ITechnologiesGalleryService _technologiesGalleryService;
    private readonly ITechnologiesService _technologiesService;

    public UserTechnologiesService(
        IUserTechnologiesRepository userTechnologiesRepository,
        ITechnologiesGalleryService technologiesGalleryService,
        ITechnologiesService technologiesService)
    {
        _userTechnologiesRepository = userTechnologiesRepository;
        _technologiesGalleryService = technologiesGalleryService;
        _technologiesService = technologiesService;
    }

    public static IUserTechnologiesService Create() => ServiceContainer.GetService<IUserTechnologiesService>();

    public async Task<List<Technologies>> GetUserTechnologiesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Technologies> result = await _userTechnologiesRepository.GetUserTechnologiesAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserTechnologiesCountAsync(string userId, string search, string rare)
    {
        return await _userTechnologiesRepository.GetUserTechnologiesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologyAsync(string userId, Technologies technology)
    {
        var checkTechnologyResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(technology.Id);
        if (checkTechnologyResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userTechnologiesRepository.InsertOrUpdateUserTechnologyAsync(userId, technology);

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

        await _technologiesGalleryService.InsertTechnologyGalleryAsync(userId, technology.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologiesBatchAsync(string userId, List<Technologies> technologies)
    {
        var insertOrUpdateResult = await _userTechnologiesRepository.InsertOrUpdateUserTechnologiesBatchAsync(userId, technologies);

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
            await _technologiesGalleryService.InsertBatchTechnologiesGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTechnologyLevelAsync(string userId, Technologies technology, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (technology == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Technology is required.");
        }

        var checkResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(technology.Id);
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

        var updateResult = await _userTechnologiesRepository.UpdateUserTechnologyLevelAsync(
            userId,
            technology,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTechnologyStarAsync(string userId, Technologies technology)
    {
        var checkTechnologyResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(technology.Id);
        if (checkTechnologyResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userTechnologiesRepository.UpdateUserTechnologyStarAsync(userId, technology);

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

        await _technologiesGalleryService.UpdateTempStarTechnologyGalleryAsync(userId, technology.Id, technology.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTechnologyQuantityAsync(string userId, Technologies technology, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userTechnologiesRepository.UpdateUserTechnologyQuantityAsync(userId, technology, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTechnologyAffinityAsync(string userId, Technologies technology, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (technology == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Technology is required.");
        }

        var checkResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(technology.Id);
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

        var updateResult = await _userTechnologiesRepository.UpdateUserTechnologyAffinityAsync(
            userId,
            technology,
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

    public async Task<Technologies> GetUserTechnologyByIdAsync(string userId, string Id)
    {
        var result = await _userTechnologiesRepository.GetUserTechnologyByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Technologies> SumPowerUserTechnologiesAsync(string userId)
    {
        return await _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);
    }
}
