using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAvatarsService : IUserAvatarsService
{
    private readonly IUserAvatarsRepository _userAvatarsRepository;
    private readonly IAvatarsGalleryService _avatarsGalleryService;
    private readonly IAvatarsService _avatarsService;

    public UserAvatarsService(
        IUserAvatarsRepository userAvatarsRepository,
        IAvatarsGalleryService avatarsGalleryService,
        IAvatarsService avatarsService)
    {
        _userAvatarsRepository = userAvatarsRepository;
        _avatarsGalleryService = avatarsGalleryService;
        _avatarsService = avatarsService;
    }

    public static IUserAvatarsService Create() => ServiceContainer.GetService<IUserAvatarsService>();

    public async Task<List<Avatars>> GetUserAvatarsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Avatars> result = await _userAvatarsRepository.GetUserAvatarsAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserAvatarsCountAsync(string userId, string search, string rare)
    {
        return await _userAvatarsRepository.GetUserAvatarsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertUserAvatarByIdAsync(string avatarId, string userId)
    {
        var checkAvatarResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(avatarId);
        if (checkAvatarResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var avatar = await _avatarsService.GetAvatarByIdAsync(avatarId);

        var insertOrUpdateResult = await _userAvatarsRepository.InsertOrUpdateUserAvatarAsync(userId, avatar);

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

        await _avatarsGalleryService.InsertAvatarGalleryAsync(userId, avatar.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAvatarAsync(string userId, Avatars avatar)
    {
        var checkAvatarResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(avatar.Id);
        if (checkAvatarResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userAvatarsRepository.InsertOrUpdateUserAvatarAsync(userId, avatar);

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

        await _avatarsGalleryService.InsertAvatarGalleryAsync(userId, avatar.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAvatarsBatchAsync(string userId, List<Avatars> avatars)
    {
        var insertOrUpdateResult = await _userAvatarsRepository.InsertOrUpdateUserAvatarsBatchAsync(userId, avatars);

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
            await _avatarsGalleryService.InsertBatchAvatarsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserAvatarLevelAsync(string userId, Avatars avatar, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (avatar == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Avatar is required.");
        }

        var checkResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(avatar.Id);
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

        var updateResult = await _userAvatarsRepository.UpdateUserAvatarLevelAsync(
            userId,
            avatar,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserAvatarStarAsync(string userId, Avatars avatar)
    {
        var checkAvatarResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(avatar.Id);
        if (checkAvatarResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userAvatarsRepository.UpdateUserAvatarStarAsync(userId, avatar);

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

        await _avatarsGalleryService.UpdateTempStarAvatarGalleryAsync(userId, avatar.Id, avatar.Star);

        return new InsertOrUpdateResult<bool>
            {
                Data = true,
                OperationType = DatabaseOperationType.Updated,
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserAvatarQuantityAsync(string userId, Avatars avatar, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userAvatarsRepository.UpdateUserAvatarQuantityAsync(userId, avatar, totalDeductions);

        return result;
    }
    
    public async Task<Avatars> GetUserAvatarByUsedAsync(string userId)
    {
        var result = await _userAvatarsRepository.GetUserAvatarByUsedAsync(userId);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        return result;
    }

    public async Task UpdateIsUsedUserAvatarAsync(string avatarId, string userId, bool is_used)
    {
        await _userAvatarsRepository.UpdateIsUsedUserAvatarAsync(avatarId, userId, is_used);
    }
    
    public async Task<Avatars> GetUserAvatarByIdAsync(string userId, string Id)
    {
        var result = await _userAvatarsRepository.GetUserAvatarByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Avatars> SumPowerUserAvatarsAsync(string userId)
    {
        return await _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);
    }
}