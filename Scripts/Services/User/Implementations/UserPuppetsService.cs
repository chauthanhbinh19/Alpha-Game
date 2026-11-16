using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPuppetsService : IUserPuppetsService
{
    private readonly IUserPuppetsRepository _userPuppetsRepository;
    private readonly IPuppetsGalleryService _puppetsGalleryService;
    private readonly IPuppetsService _puppetsService;

    public UserPuppetsService(
        IUserPuppetsRepository userPuppetsRepository,
        IPuppetsGalleryService puppetsGalleryService,
        IPuppetsService puppetsService)
    {
        _userPuppetsRepository = userPuppetsRepository;
        _puppetsGalleryService = puppetsGalleryService;
        _puppetsService = puppetsService;
    }

    public static IUserPuppetsService Create() => ServiceContainer.GetService<IUserPuppetsService>();

    public async Task<List<Puppets>> GetUserPuppetsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> result = await _userPuppetsRepository.GetUserPuppetsAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserPuppetsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userPuppetsRepository.GetUserPuppetsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetAsync(string userId, Puppets puppet)
    {
        var checkPuppetResult = await _puppetsService.IsPuppetDeletedOrInactiveAsync(puppet.Id);
        if (checkPuppetResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userPuppetsRepository.InsertOrUpdateUserPuppetAsync(userId, puppet);

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

        await _puppetsGalleryService.InsertPuppetGalleryAsync(userId, puppet.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetsBatchAsync(string userId, List<Puppets> puppets)
    {
        var insertOrUpdateResult = await _userPuppetsRepository.InsertOrUpdateUserPuppetsBatchAsync(userId, puppets);

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
            await _puppetsGalleryService.InsertBatchPuppetsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserPuppetLevelAsync(string userId, Puppets puppet, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (puppet == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Puppet is required.");
        }

        var checkResult = await _puppetsService.IsPuppetDeletedOrInactiveAsync(puppet.Id);
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

        var updateResult = await _userPuppetsRepository.UpdateUserPuppetLevelAsync(
            userId,
            puppet,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserPuppetStarAsync(string userId, Puppets puppet)
    {
        var checkPuppetResult = await _puppetsService.IsPuppetDeletedOrInactiveAsync(puppet.Id);
        if (checkPuppetResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userPuppetsRepository.UpdateUserPuppetStarAsync(userId, puppet);

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

        await _puppetsGalleryService.UpdateTempStarPuppetGalleryAsync(userId, puppet.Id, puppet.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserPuppetQuantityAsync(string userId, Puppets puppet, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userPuppetsRepository.UpdateUserPuppetQuantityAsync(userId, puppet, totalDeductions);

        return result;
    }

    public async Task<Puppets> GetUserPuppetByIdAsync(string userId, string Id)
    {
        var result = await _userPuppetsRepository.GetUserPuppetByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Puppets> SumPowerUserPuppetsAsync(string userId)
    {
        return await _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);
    }
}
