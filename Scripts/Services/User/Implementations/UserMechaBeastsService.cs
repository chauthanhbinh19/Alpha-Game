using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMechaBeastsService : IUserMechaBeastsService
{
    private readonly IUserMechaBeastsRepository _userMechaBeastsRepository;
    private readonly IMechaBeastsGalleryService _mechaBeastsGalleryService;
    private readonly IMechaBeastsService _mechaBeastsService;

    public UserMechaBeastsService(
        IUserMechaBeastsRepository userMechaBeastsRepository,
        IMechaBeastsGalleryService mechaBeastsGalleryService,
        IMechaBeastsService mechaBeastsService)
    {
        _userMechaBeastsRepository = userMechaBeastsRepository;
        _mechaBeastsGalleryService = mechaBeastsGalleryService;
        _mechaBeastsService = mechaBeastsService;
    }

    public static IUserMechaBeastsService Create() => ServiceContainer.GetService<IUserMechaBeastsService>();

    public async Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> result = await _userMechaBeastsRepository.GetUserMechaBeastsAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserMechaBeastsCountAsync(string userId, string search, string rare)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastAsync(string userId, MechaBeasts mechaBeast)
    {
        var checkMechaBeastResult = await _mechaBeastsService.IsMechaBeastDeletedOrInactiveAsync(mechaBeast.Id);
        if (checkMechaBeastResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userMechaBeastsRepository.InsertOrUpdateUserMechaBeastAsync(userId, mechaBeast);

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

        await _mechaBeastsGalleryService.InsertMechaBeastGalleryAsync(userId, mechaBeast.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastsBatchAsync(string userId, List<MechaBeasts> mechaBeasts)
    {
        var insertOrUpdateResult = await _userMechaBeastsRepository.InsertOrUpdateUserMechaBeastsBatchAsync(userId, mechaBeasts);

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
            await _mechaBeastsGalleryService.InsertBatchMechaBeastsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMechaBeastLevelAsync(string userId, MechaBeasts mechaBeast, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (mechaBeast == null)
        {
            return InsertOrUpdateResult<bool>.Failure("MechaBeast is required.");
        }

        var checkResult = await _mechaBeastsService.IsMechaBeastDeletedOrInactiveAsync(mechaBeast.Id);
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

        var updateResult = await _userMechaBeastsRepository.UpdateUserMechaBeastLevelAsync(
            userId,
            mechaBeast,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMechaBeastStarAsync(string userId, MechaBeasts mechaBeast)
    {
        var checkMechaBeastResult = await _mechaBeastsService.IsMechaBeastDeletedOrInactiveAsync(mechaBeast.Id);
        if (checkMechaBeastResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userMechaBeastsRepository.UpdateUserMechaBeastStarAsync(userId, mechaBeast);

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

        await _mechaBeastsGalleryService.UpdateTempStarMechaBeastGalleryAsync(userId, mechaBeast.Id, mechaBeast.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMechaBeastQuantityAsync(string userId, MechaBeasts mechaBeast, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userMechaBeastsRepository.UpdateUserMechaBeastQuantityAsync(userId, mechaBeast, totalDeductions);

        return result;
    }

    public async Task<MechaBeasts> GetUserMechaBeastByIdAsync(string userId, string Id)
    {
        var result = await _userMechaBeastsRepository.GetUserMechaBeastByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<MechaBeasts> SumPowerUserMechaBeastsAsync(string userId)
    {
        return await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);
    }
}
