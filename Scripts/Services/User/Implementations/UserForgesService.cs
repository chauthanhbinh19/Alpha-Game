using System.Collections.Generic;
using System.Threading.Tasks;

public class UserForgesService : IUserForgesService
{
    private readonly IUserForgesRepository _userForgesRepository;
    private readonly IForgesGalleryService _forgesGalleryService;
    private readonly IForgesService _forgesService;

    public UserForgesService(
        IUserForgesRepository userForgesRepository,
        IForgesGalleryService forgesGalleryService,
        IForgesService forgesService)
    {
        _userForgesRepository = userForgesRepository;
        _forgesGalleryService = forgesGalleryService;
        _forgesService = forgesService;
    }

    public static IUserForgesService Create() => ServiceContainer.GetService<IUserForgesService>();

    public async Task<List<Forges>> GetUserForgesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> result = await _userForgesRepository.GetUserForgesAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserForgesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userForgesRepository.GetUserForgesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgeAsync(string userId, Forges forge)
    {
        var checkForgeResult = await _forgesService.IsForgeDeletedOrInactiveAsync(forge.Id);
        if (checkForgeResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userForgesRepository.InsertOrUpdateUserForgeAsync(userId, forge);

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

        await _forgesGalleryService.InsertForgeGalleryAsync(userId, forge.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgesBatchAsync(string userId, List<Forges> forges)
    {
        var insertOrUpdateResult = await _userForgesRepository.InsertOrUpdateUserForgesBatchAsync(userId, forges);

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
            await _forgesGalleryService.InsertBatchForgesGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserForgeLevelAsync(string userId, Forges forge, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (forge == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Forge is required.");
        }

        var checkResult = await _forgesService.IsForgeDeletedOrInactiveAsync(forge.Id);
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

        var updateResult = await _userForgesRepository.UpdateUserForgeLevelAsync(
            userId,
            forge,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserForgeStarAsync(string userId, Forges forge)
    {
        var checkForgeResult = await _forgesService.IsForgeDeletedOrInactiveAsync(forge.Id);
        if (checkForgeResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userForgesRepository.UpdateUserForgeStarAsync(userId, forge);

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

        await _forgesGalleryService.UpdateTempStarForgeGalleryAsync(userId, forge.Id, forge.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserForgeQuantityAsync(string userId, Forges forge, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userForgesRepository.UpdateUserForgeQuantityAsync(userId, forge, totalDeductions);

        return result;
    }

    public async Task<Forges> GetUserForgeByIdAsync(string userId, string Id)
    {
        var result = await _userForgesRepository.GetUserForgeByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Forges> SumPowerUserForgesAsync(string userId)
    {
        return await _userForgesRepository.SumPowerUserForgesAsync(userId);
    }
}
