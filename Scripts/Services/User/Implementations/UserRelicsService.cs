using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRelicsService : IUserRelicsService
{
    private readonly IUserRelicsRepository _userRelicsRepository;
    private readonly IRelicsGalleryService _relicsGalleryService;
    private readonly IRelicsService _relicsService;

    public UserRelicsService(
        IUserRelicsRepository userRelicsRepository,
        IRelicsGalleryService relicsGalleryService,
        IRelicsService relicsService)
    {
        _userRelicsRepository = userRelicsRepository;
        _relicsGalleryService = relicsGalleryService;
        _relicsService = relicsService;
    }

    public static IUserRelicsService Create() => ServiceContainer.GetService<IUserRelicsService>();

    public async Task<List<Relics>> GetUserRelicsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> result = await _userRelicsRepository.GetUserRelicsAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserRelicsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userRelicsRepository.GetUserRelicsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicAsync(string userId, Relics relic)
    {
        var checkRelicResult = await _relicsService.IsRelicDeletedOrInactiveAsync(relic.Id);
        if (checkRelicResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userRelicsRepository.InsertOrUpdateUserRelicAsync(userId, relic);

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

        await _relicsGalleryService.InsertRelicGalleryAsync(userId, relic.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicsBatchAsync(string userId, List<Relics> relics)
    {
        var insertOrUpdateResult = await _userRelicsRepository.InsertOrUpdateUserRelicsBatchAsync(userId, relics);

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
            await _relicsGalleryService.InsertBatchRelicsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRelicLevelAsync(string userId, Relics relic, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (relic == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Relic is required.");
        }

        var checkResult = await _relicsService.IsRelicDeletedOrInactiveAsync(relic.Id);
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

        var updateResult = await _userRelicsRepository.UpdateUserRelicLevelAsync(
            userId,
            relic,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRelicStarAsync(string userId, Relics relic)
    {
        var checkRelicResult = await _relicsService.IsRelicDeletedOrInactiveAsync(relic.Id);
        if (checkRelicResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userRelicsRepository.UpdateUserRelicStarAsync(userId, relic);

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

        await _relicsGalleryService.UpdateTempStarRelicGalleryAsync(userId, relic.Id, relic.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRelicQuantityAsync(string userId, Relics relic, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userRelicsRepository.UpdateUserRelicQuantityAsync(userId, relic, totalDeductions);

        return result;
    }

    public async Task<Relics> GetUserRelicByIdAsync(string userId, string Id)
    {
        var result = await _userRelicsRepository.GetUserRelicByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Relics> SumPowerUserRelicsAsync(string userId)
    {
        return await _userRelicsRepository.SumPowerUserRelicsAsync(userId);
    }
}
