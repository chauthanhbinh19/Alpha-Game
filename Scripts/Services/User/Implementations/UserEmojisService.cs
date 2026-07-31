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
        Emojis oldEmoji = await _emojisService.SumPowerEmojisPercentAsync(userId);
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

        Emojis newEmoji = await _emojisService.SumPowerEmojisPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newEmoji - (PowerManager)oldEmoji;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEmojisBatchAsync(string userId, List<Emojis> emojies)
    {
        Emojis oldEmoji = await _emojisService.SumPowerEmojisPercentAsync(userId);
        var repositoryResult = await _userEmojisRepository.InsertOrUpdateUserEmojisBatchAsync(userId, emojies);

        // 1. Kiểm tra Null hoặc nếu Repository trả về không thành công
        if (repositoryResult?.Data == null || !repositoryResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        // 2. Gộp logic xử lý Gallery nếu có thẻ mới được Insert (dùng cho cả Inserted và Mixed)
        var newlyInsertedCards = repositoryResult.Data.InsertedItems;
        if (newlyInsertedCards != null && newlyInsertedCards.Count > 0)
        {
            await _emojisGalleryService.InsertBatchEmojisGalleryAsync(userId, newlyInsertedCards);
        }

        Emojis newEmoji = await _emojisService.SumPowerEmojisPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newEmoji - (PowerManager)oldEmoji;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        // 3. Mapping kết quả OperationType trả về gọn gàng
        return repositoryResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserEmojiLevelAsync(string userId, Emojis emoji)
    {
        var updateResult = await _userEmojisRepository.UpdateUserEmojiLevelAsync(userId, emoji);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserEmojiStarAsync(string userId, Emojis emoji)
    {
        var updateResult = await _userEmojisRepository.UpdateUserEmojiStarAsync(userId, emoji);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _emojisGalleryService.UpdateTempStarEmojiGalleryAsync(userId, emoji.Id, emoji.Star);

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
