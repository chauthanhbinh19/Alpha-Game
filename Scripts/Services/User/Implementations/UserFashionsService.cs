using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFashionsService : IUserFashionsService
{
    private readonly IUserFashionsRepository _userFashionsRepository;
    private readonly IFashionsGalleryService _fashionsGalleryService;
    private readonly IFashionsService _fashionsService;

    public UserFashionsService(
        IUserFashionsRepository userFashionsRepository,
        IFashionsGalleryService fashionsGalleryService,
        IFashionsService fashionsService)
    {
        _userFashionsRepository = userFashionsRepository;
        _fashionsGalleryService = fashionsGalleryService;
        _fashionsService = fashionsService;
    }

    public static IUserFashionsService Create() => ServiceContainer.GetService<IUserFashionsService>();

    public async Task<List<Fashions>> GetUserFashionsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> result = await _userFashionsRepository.GetUserFashionsAsync(userId, search, type, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserFashionsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userFashionsRepository.GetUserFashionsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionAsync(string userId, Fashions fashion)
    {
        var checkFashionResult = await _fashionsService.IsFashionDeletedOrInactiveAsync(fashion.Id);
        if (checkFashionResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userFashionsRepository.InsertOrUpdateUserFashionAsync(userId, fashion);

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

        await _fashionsGalleryService.InsertFashionGalleryAsync(userId, fashion.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionsBatchAsync(string userId, List<Fashions> fashions)
    {
        var insertOrUpdateResult = await _userFashionsRepository.InsertOrUpdateUserFashionsBatchAsync(userId, fashions);

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
            await _fashionsGalleryService.InsertBatchFashionsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserFashionLevelAsync(string userId, Fashions fashion, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (fashion == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Fashion is required.");
        }

        var checkResult = await _fashionsService.IsFashionDeletedOrInactiveAsync(fashion.Id);
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

        var updateResult = await _userFashionsRepository.UpdateUserFashionLevelAsync(
            userId,
            fashion,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserFashionStarAsync(string userId, Fashions fashion)
    {
        var checkFashionResult = await _fashionsService.IsFashionDeletedOrInactiveAsync(fashion.Id);
        if (checkFashionResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userFashionsRepository.UpdateUserFashionStarAsync(userId, fashion);

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

        await _fashionsGalleryService.UpdateTempStarFashionGalleryAsync(userId, fashion.Id, fashion.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserFashionQuantityAsync(string userId, Fashions fashion, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userFashionsRepository.UpdateUserFashionQuantityAsync(userId, fashion, totalDeductions);

        return result;
    }

    public async Task<Fashions> GetUserFashionByIdAsync(string userId, string Id)
    {
        var result = await _userFashionsRepository.GetUserFashionByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Fashions> SumPowerUserFashionsAsync(string userId)
    {
        return await _userFashionsRepository.SumPowerUserFashionsAsync(userId);
    }
}
