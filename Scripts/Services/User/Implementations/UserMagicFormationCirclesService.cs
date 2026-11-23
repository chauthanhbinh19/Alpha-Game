using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMagicFormationCirclesService : IUserMagicFormationCirclesService
{
    private readonly IUserMagicFormationCirclesRepository _userMagicFormationCirclesRepository;
    private readonly IMagicFormationCirclesGalleryService _magicFormationCirclesGalleryService;
    private readonly IMagicFormationCirclesService _magicFormationCirclesService;

    public UserMagicFormationCirclesService(
        IUserMagicFormationCirclesRepository userMagicFormationCirclesRepository,
        IMagicFormationCirclesGalleryService magicFormationCirclesGalleryService,
        IMagicFormationCirclesService magicFormationCirclesService)
    {
        _userMagicFormationCirclesRepository = userMagicFormationCirclesRepository;
        _magicFormationCirclesGalleryService = magicFormationCirclesGalleryService;
        _magicFormationCirclesService = magicFormationCirclesService;
    }

    public static IUserMagicFormationCirclesService Create() => ServiceContainer.GetService<IUserMagicFormationCirclesService>();

    public async Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> result = await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesAsync(userId, search, type, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserMagicFormationCirclesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCircleAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        var checkMagicFormationCircleResult = await _magicFormationCirclesService.IsMagicFormationCircleDeletedOrInactiveAsync(magicFormationCircle.Id);
        if (checkMagicFormationCircleResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userMagicFormationCirclesRepository.InsertOrUpdateUserMagicFormationCircleAsync(userId, magicFormationCircle);

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

        await _magicFormationCirclesGalleryService.InsertMagicFormationCircleGalleryAsync(userId, magicFormationCircle.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCirclesBatchAsync(string userId, List<MagicFormationCircles> magicFormationCircles)
    {
        var insertOrUpdateResult = await _userMagicFormationCirclesRepository.InsertOrUpdateUserMagicFormationCirclesBatchAsync(userId, magicFormationCircles);

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
            await _magicFormationCirclesGalleryService.InsertBatchMagicFormationCirclesGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMagicFormationCircleLevelAsync(string userId, MagicFormationCircles magicFormationCircle, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (magicFormationCircle == null)
        {
            return InsertOrUpdateResult<bool>.Failure("MagicFormationCircle is required.");
        }

        var checkResult = await _magicFormationCirclesService.IsMagicFormationCircleDeletedOrInactiveAsync(magicFormationCircle.Id);
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

        var updateResult = await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleLevelAsync(
            userId,
            magicFormationCircle,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMagicFormationCircleStarAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        var checkMagicFormationCircleResult = await _magicFormationCirclesService.IsMagicFormationCircleDeletedOrInactiveAsync(magicFormationCircle.Id);
        if (checkMagicFormationCircleResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleStarAsync(userId, magicFormationCircle);

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

        await _magicFormationCirclesGalleryService.UpdateTempStarMagicFormationCircleGalleryAsync(userId, magicFormationCircle.Id, magicFormationCircle.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMagicFormationCircleQuantityAsync(string userId, MagicFormationCircles magicFormationCircle, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleQuantityAsync(userId, magicFormationCircle, totalDeductions);

        return result;
    }

    public async Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string userId, string Id)
    {
        var result = await _userMagicFormationCirclesRepository.GetUserMagicFormationCircleByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync(string userId)
    {
        return await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);
    }
}
