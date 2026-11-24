using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCoresService : IUserCoresService
{
    private readonly IUserCoresRepository _userCoresRepository;
    private readonly ICoresGalleryService _coresGalleryService;
    private readonly ICoresService _coresService;

    public UserCoresService(
        IUserCoresRepository userCoresRepository,
        ICoresGalleryService coresGalleryService,
        ICoresService coresService)
    {
        _userCoresRepository = userCoresRepository;
        _coresGalleryService = coresGalleryService;
        _coresService = coresService;
    }

    public static IUserCoresService Create() => ServiceContainer.GetService<IUserCoresService>();

    public async Task<List<Cores>> GetUserCoresAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Cores> result = await _userCoresRepository.GetUserCoresAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserCoresCountAsync(string userId, string search, string rare)
    {
        return await _userCoresRepository.GetUserCoresCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoreAsync(string userId, Cores core)
    {
        var checkCoreResult = await _coresService.IsCoreDeletedOrInactiveAsync(core.Id);
        if (checkCoreResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userCoresRepository.InsertOrUpdateUserCoreAsync(userId, core);

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

        await _coresGalleryService.InsertCoreGalleryAsync(userId, core.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoresBatchAsync(string userId, List<Cores> cores)
    {
        var insertOrUpdateResult = await _userCoresRepository.InsertOrUpdateUserCoresBatchAsync(userId, cores);

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
            await _coresGalleryService.InsertBatchCoresGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCoreLevelAsync(string userId, Cores core, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (core == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Core is required.");
        }

        var checkResult = await _coresService.IsCoreDeletedOrInactiveAsync(core.Id);
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

        var updateResult = await _userCoresRepository.UpdateUserCoreLevelAsync(
            userId,
            core,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCoreStarAsync(string userId, Cores core)
    {
        var checkCoreResult = await _coresService.IsCoreDeletedOrInactiveAsync(core.Id);
        if (checkCoreResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userCoresRepository.UpdateUserCoreStarAsync(userId, core);

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

        await _coresGalleryService.UpdateTempStarCoreGalleryAsync(userId, core.Id, core.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCoreQuantityAsync(string userId, Cores core, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userCoresRepository.UpdateUserCoreQuantityAsync(userId, core, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCoreAffinityAsync(string userId, Cores core, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (core == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Core is required.");
        }

        var checkResult = await _coresService.IsCoreDeletedOrInactiveAsync(core.Id);
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

        var updateResult = await _userCoresRepository.UpdateUserCoreAffinityAsync(
            userId,
            core,
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

    public async Task<Cores> GetUserCoreByIdAsync(string userId, string Id)
    {
        var result = await _userCoresRepository.GetUserCoreByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Cores> SumPowerUserCoresAsync(string userId)
    {
        return await _userCoresRepository.SumPowerUserCoresAsync(userId);
    }
}
