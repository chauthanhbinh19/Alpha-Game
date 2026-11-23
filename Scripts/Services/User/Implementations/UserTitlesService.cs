using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTitlesService : IUserTitlesService
{
    private readonly IUserTitlesRepository _userTitlesRepository;
    private readonly ITitlesGalleryService _titlesGalleryService;
    private readonly ITitlesService _titlesService;

    public UserTitlesService(
        IUserTitlesRepository userTitlesRepository,
        ITitlesGalleryService titlesGalleryService,
        ITitlesService titlesService)
    {
        _userTitlesRepository = userTitlesRepository;
        _titlesGalleryService = titlesGalleryService;
        _titlesService = titlesService;
    }

    public static IUserTitlesService Create() => ServiceContainer.GetService<IUserTitlesService>();

    public async Task<List<Titles>> GetUserTitlesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Titles> result = await _userTitlesRepository.GetUserTitlesAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserTitlesCountAsync(string userId, string search, string rare)
    {
        return await _userTitlesRepository.GetUserTitlesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTitleAsync(string userId, Titles title)
    {
        var checkTitleResult = await _titlesService.IsTitleDeletedOrInactiveAsync(title.Id);
        if (checkTitleResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userTitlesRepository.InsertOrUpdateUserTitleAsync(userId, title);

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

        await _titlesGalleryService.InsertTitleGalleryAsync(userId, title.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTitlesBatchAsync(string userId, List<Titles> titles)
    {
        var insertOrUpdateResult = await _userTitlesRepository.InsertOrUpdateUserTitlesBatchAsync(userId, titles);

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
            await _titlesGalleryService.InsertBatchTitlesGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTitleLevelAsync(string userId, Titles title, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (title == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Title is required.");
        }

        var checkResult = await _titlesService.IsTitleDeletedOrInactiveAsync(title.Id);
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

        var updateResult = await _userTitlesRepository.UpdateUserTitleLevelAsync(
            userId,
            title,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTitleStarAsync(string userId, Titles title)
    {
        var checkTitleResult = await _titlesService.IsTitleDeletedOrInactiveAsync(title.Id);
        if (checkTitleResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userTitlesRepository.UpdateUserTitleStarAsync(userId, title);

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

        await _titlesGalleryService.UpdateTempStarTitleGalleryAsync(userId, title.Id, title.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTitleQuantityAsync(string userId, Titles title, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userTitlesRepository.UpdateUserTitleQuantityAsync(userId, title, totalDeductions);

        return result;
    }

    public async Task<Titles> GetUserTitleByIdAsync(string userId, string Id)
    {
        var result = await _userTitlesRepository.GetUserTitleByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Titles> SumPowerUserTitlesAsync(string userId)
    {
        return await _userTitlesRepository.SumPowerUserTitlesAsync(userId);
    }
}
