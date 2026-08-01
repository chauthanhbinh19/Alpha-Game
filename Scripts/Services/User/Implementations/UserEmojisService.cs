using System.Collections.Generic;
using System.Threading.Tasks;

public class UserEmojisService : IUserEmojisService
{
    private readonly IUserEmojisRepository _userEmojisRepository;
    private readonly IEmojisGalleryService _emojisGalleryService;
    private readonly IEmojisService _emojisService;
    private readonly IPowerManagerService _powerManagerService;

    public UserEmojisService(
        IUserEmojisRepository userEmojisRepository,
        IEmojisGalleryService emojisGalleryService,
        IEmojisService emojisService,
        IPowerManagerService powerManagerService)
    {
        _userEmojisRepository = userEmojisRepository;
        _emojisGalleryService = emojisGalleryService;
        _emojisService = emojisService;
        _powerManagerService = powerManagerService;
    }

    public static IUserEmojisService Create() => ServiceContainer.GetService<IUserEmojisService>();

    public async Task<List<Emojis>> GetUserEmojisAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> list = await _userEmojisRepository.GetUserEmojisAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserEmojisCountAsync(string userId, string search, string rare)
    {
        return await _userEmojisRepository.GetUserEmojisCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEmojiAsync(string userId, Emojis emoji)
    {
        var oldEmojiTask = _emojisService.SumPowerEmojisPercentAsync(userId);
        var oldUserEmojiTask = _userEmojisRepository.SumPowerUserEmojisAsync(userId);

        await Task.WhenAll(oldEmojiTask, oldUserEmojiTask);

        Emojis oldEmoji = oldEmojiTask.Result;
        Emojis oldUserEmoji = oldUserEmojiTask.Result;

        var insertOrUpdateResult = await _userEmojisRepository.InsertOrUpdateUserEmojiAsync(userId, emoji);

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

        await _emojisGalleryService.InsertEmojiGalleryAsync(userId, emoji.Id);

        var newEmojiTask = _emojisService.SumPowerEmojisPercentAsync(userId);
        var newUserEmojiTask = _userEmojisRepository.SumPowerUserEmojisAsync(userId);

        await Task.WhenAll(newEmojiTask, newUserEmojiTask);

        PowerManager deltaPower = (PowerManager)newEmojiTask.Result - (PowerManager)oldEmoji;
        PowerManager deltaUserPower = (PowerManager)newUserEmojiTask.Result - (PowerManager)oldUserEmoji;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEmojisBatchAsync(string userId, List<Emojis> emojis)
    {
        var oldEmojiTask = _emojisService.SumPowerEmojisPercentAsync(userId);
        var oldUserEmojiTask = _userEmojisRepository.SumPowerUserEmojisAsync(userId);

        await Task.WhenAll(oldEmojiTask, oldUserEmojiTask);

        Emojis oldEmoji = oldEmojiTask.Result;
        Emojis oldUserEmoji = oldUserEmojiTask.Result;

        var insertOrUpdateResult = await _userEmojisRepository.InsertOrUpdateUserEmojisBatchAsync(userId, emojis);

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
            await _emojisGalleryService.InsertBatchEmojisGalleryAsync(userId, newlyInsertedCards);

            var newEmojiTask = _emojisService.SumPowerEmojisPercentAsync(userId);
            var newUserEmojiTask = _userEmojisRepository.SumPowerUserEmojisAsync(userId);

            await Task.WhenAll(newEmojiTask, newUserEmojiTask);

            PowerManager deltaPower = (PowerManager)newEmojiTask.Result - (PowerManager)oldEmoji;
            PowerManager deltaUserPower = (PowerManager)newUserEmojiTask.Result - (PowerManager)oldUserEmoji;

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

    public async Task<bool> UpdateUserEmojiLevelAsync(string userId, Emojis emoji)
    {
        Emojis oldUserEmoji = await _userEmojisRepository.SumPowerUserEmojisAsync(userId);

        var updateResult = await _userEmojisRepository.UpdateUserEmojiLevelAsync(userId, emoji);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Emojis newUserEmoji = await _userEmojisRepository.SumPowerUserEmojisAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserEmoji - (PowerManager)oldUserEmoji;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserEmojiStarAsync(string userId, Emojis emoji)
    {
        Emojis oldUserEmoji = await _userEmojisRepository.SumPowerUserEmojisAsync(userId);

        var updateResult = await _userEmojisRepository.UpdateUserEmojiStarAsync(userId, emoji);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _emojisGalleryService.UpdateTempStarEmojiGalleryAsync(userId, emoji.Id, emoji.Star);

        Emojis newUserEmoji = await _userEmojisRepository.SumPowerUserEmojisAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserEmoji - (PowerManager)oldUserEmoji;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Emojis> GetUserEmojiByIdAsync(string userId, string Id)
    {
        var result = await _userEmojisRepository.GetUserEmojiByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Emojis> SumPowerUserEmojisAsync(string userId)
    {
        return await _userEmojisRepository.SumPowerUserEmojisAsync(userId);
    }
}
