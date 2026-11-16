using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTalismansService : IUserTalismansService
{
    private readonly IUserTalismansRepository _userTalismansRepository;
    private readonly ITalismansGalleryService _talismansGalleryService;
    private readonly ITalismansService _talismansService;

    public UserTalismansService(
        IUserTalismansRepository userTalismansRepository,
        ITalismansGalleryService talismansGalleryService,
        ITalismansService talismansService)
    {
        _userTalismansRepository = userTalismansRepository;
        _talismansGalleryService = talismansGalleryService;
        _talismansService = talismansService;
    }

    public static IUserTalismansService Create() => ServiceContainer.GetService<IUserTalismansService>();

    public async Task<List<Talismans>> GetUserTalismansAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> result = await _userTalismansRepository.GetUserTalismansAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserTalismansCountAsync(string userId, string search, string type, string rare)
    {
        return await _userTalismansRepository.GetUserTalismansCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismanAsync(string userId, Talismans talisman)
    {
        var checkTalismanResult = await _talismansService.IsTalismanDeletedOrInactiveAsync(talisman.Id);
        if (checkTalismanResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userTalismansRepository.InsertOrUpdateUserTalismanAsync(userId, talisman);

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

        await _talismansGalleryService.InsertTalismanGalleryAsync(userId, talisman.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismansBatchAsync(string userId, List<Talismans> talismans)
    {
        var insertOrUpdateResult = await _userTalismansRepository.InsertOrUpdateUserTalismansBatchAsync(userId, talismans);

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
            await _talismansGalleryService.InsertBatchTalismansGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTalismanLevelAsync(string userId, Talismans talisman, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (talisman == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Talisman is required.");
        }

        var checkResult = await _talismansService.IsTalismanDeletedOrInactiveAsync(talisman.Id);
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

        var updateResult = await _userTalismansRepository.UpdateUserTalismanLevelAsync(
            userId,
            talisman,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTalismanStarAsync(string userId, Talismans talisman)
    {
        var checkTalismanResult = await _talismansService.IsTalismanDeletedOrInactiveAsync(talisman.Id);
        if (checkTalismanResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userTalismansRepository.UpdateUserTalismanStarAsync(userId, talisman);

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

        await _talismansGalleryService.UpdateTempStarTalismanGalleryAsync(userId, talisman.Id, talisman.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserTalismanQuantityAsync(string userId, Talismans talisman, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userTalismansRepository.UpdateUserTalismanQuantityAsync(userId, talisman, totalDeductions);

        return result;
    }

    public async Task<Talismans> GetUserTalismanByIdAsync(string userId, string Id)
    {
        var result = await _userTalismansRepository.GetUserTalismanByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Talismans> SumPowerUserTalismansAsync(string userId)
    {
        return await _userTalismansRepository.SumPowerUserTalismansAsync(userId);
    }
}
