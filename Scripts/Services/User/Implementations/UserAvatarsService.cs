using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAvatarsService : IUserAvatarsService
{
    private readonly IUserAvatarsRepository _userAvatarsRepository;
    private readonly IAvatarsGalleryService _avatarsGalleryService;
    private readonly IAvatarsService _avatarsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserAvatarsService(
        IUserAvatarsRepository userAvatarsRepository,
        IAvatarsGalleryService avatarsGalleryService,
        IAvatarsService avatarsService,
        IPowerManagerService powerManagerService)
    {
        _userAvatarsRepository = userAvatarsRepository;
        _avatarsGalleryService = avatarsGalleryService;
        _avatarsService = avatarsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserAvatarsService Create() => ServiceContainer.GetService<IUserAvatarsService>();

    public async Task<List<Avatars>> GetUserAvatarsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Avatars> list = await _userAvatarsRepository.GetUserAvatarsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in list)
        {
            item.BaseStats = new BaseStats(item);
        }

        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAvatarsCountAsync(string userId, string search, string rare)
    {
        return await _userAvatarsRepository.GetUserAvatarsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserAvatarByIdAsync(string avatarId, string userId)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        return await _userAvatarsRepository.InsertUserAvatarByIdAsync(await _service.GetAvatarByIdAsync(avatarId), userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAvatarAsync(string userId, Avatars avatar)
    {
        var oldAvatarTask = _avatarsService.SumPowerAvatarsPercentAsync(userId);
        var oldUserAvatarTask = _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);

        await Task.WhenAll(oldAvatarTask, oldUserAvatarTask);

        Avatars oldAvatar = oldAvatarTask.Result;
        Avatars oldUserAvatar = oldUserAvatarTask.Result;

        var insertOrUpdateResult = await _userAvatarsRepository.InsertOrUpdateUserAvatarAsync(userId, avatar);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _avatarsGalleryService.InsertAvatarGalleryAsync(userId, avatar.Id);

        var newAvatarTask = _avatarsService.SumPowerAvatarsPercentAsync(userId);
        var newUserAvatarTask = _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);

        await Task.WhenAll(newAvatarTask, newUserAvatarTask);

        PowerManager deltaPower = (PowerManager)newAvatarTask.Result - (PowerManager)oldAvatar;
        PowerManager deltaUserPower = (PowerManager)newUserAvatarTask.Result - (PowerManager)oldUserAvatar;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAvatarsBatchAsync(string userId, List<Avatars> avatars)
    {
        var oldAvatarTask = _avatarsService.SumPowerAvatarsPercentAsync(userId);
        var oldUserAvatarTask = _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);

        await Task.WhenAll(oldAvatarTask, oldUserAvatarTask);

        Avatars oldAvatar = oldAvatarTask.Result;
        Avatars oldUserAvatar = oldUserAvatarTask.Result;

        var insertOrUpdateResult = await _userAvatarsRepository.InsertOrUpdateUserAvatarsBatchAsync(userId, avatars);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _avatarsGalleryService.InsertBatchAvatarsGalleryAsync(userId, newlyInsertedCards);

            var newAvatarTask = _avatarsService.SumPowerAvatarsPercentAsync(userId);
            var newUserAvatarTask = _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);

            await Task.WhenAll(newAvatarTask, newUserAvatarTask);

            PowerManager deltaPower = (PowerManager)newAvatarTask.Result - (PowerManager)oldAvatar;
            PowerManager deltaUserPower = (PowerManager)newUserAvatarTask.Result - (PowerManager)oldUserAvatar;

            PowerManager totalDelta = new PowerManager();
            if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
            if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

            if (totalDelta.HasAnyPositiveStat())
            {
                PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
                PowerManager updatedPower = currentPower + totalDelta;
                await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
            }
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
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserAvatarLevelAsync(string userId, Avatars avatar)
    {
        Avatars oldUserAvatar = await _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);

        var updateResult = await _userAvatarsRepository.UpdateUserAvatarLevelAsync(userId, avatar);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Avatars newUserAvatar = await _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserAvatar - (PowerManager)oldUserAvatar;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserAvatarStarAsync(string userId, Avatars avatar)
    {
        Avatars oldUserAvatar = await _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);

        var updateResult = await _userAvatarsRepository.UpdateUserAvatarStarAsync(userId, avatar);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _avatarsGalleryService.UpdateTempStarAvatarGalleryAsync(userId, avatar.Id, avatar.Star);

        Avatars newUserAvatar = await _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserAvatar - (PowerManager)oldUserAvatar;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
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

    public async Task<Avatars> SumPowerUserAvatarsAsync(string userId)
    {
        return await _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);
    }
}