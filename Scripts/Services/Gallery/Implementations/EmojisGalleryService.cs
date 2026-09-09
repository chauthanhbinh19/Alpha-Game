using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EmojisGalleryService : IEmojisGalleryService
{
    private readonly IEmojisGalleryRepository _emojisGalleryRepository;
    private readonly IEmojisService _emojisService;
    private readonly IPowerManagerService _powerManagerService;

    public EmojisGalleryService(
        IEmojisGalleryRepository emojisGalleryRepository,
        IEmojisService emojisService,
        IPowerManagerService powerManagerService)
    {
        _emojisGalleryRepository = emojisGalleryRepository;
        _emojisService = emojisService;
        _powerManagerService = powerManagerService;
    }

    public static IEmojisGalleryService Create() => ServiceContainer.GetService<IEmojisGalleryService>();

    public async Task<List<Emojis>> GetEmojisCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> list = await _emojisGalleryRepository.GetEmojisCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmojisCountAsync(string search, string rare)
    {
        return await _emojisGalleryRepository.GetEmojisCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertEmojiGalleryAsync(string userId, string Id)
    {
        var checkResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _emojisGalleryRepository.InsertEmojiGalleryAsync(userId, Id, await _emojisService.GetEmojiByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusEmojiGalleryAsync(string userId, string emojiId)
    {
        var checkResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(emojiId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _emojisGalleryRepository.UpdateStatusEmojiGalleryAsync(userId, emojiId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Emojis emojiGallery = await GetEmojiCollectionByIdAsync(userId, emojiId) ?? new Emojis();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)emojiGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusEmojisGalleryAsync(string userId)
    {
        Emojis oldEmoji = await SumPowerEmojisGalleryAsync(userId);

        var updateResult = await _emojisGalleryRepository.UpdateBatchStatusEmojisGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Emojis newEmoji = await SumPowerEmojisGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newEmoji - (PowerManager)oldEmoji;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Emojis> SumPowerEmojisGalleryAsync(string userId)
    {
        return await _emojisGalleryRepository.SumPowerEmojisGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarEmojiGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _emojisGalleryRepository.UpdateTempStarEmojiGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarEmojiGalleryAsync(string userId, string emojiId)
    {
        var checkResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(emojiId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Emojis oldEmoji = await GetEmojiCollectionByIdAsync(userId, emojiId) ?? new Emojis();

        var updateResult = await _emojisGalleryRepository.UpdateCurrentStarEmojiGalleryAsync(userId, emojiId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Emojis newEmoji = await GetEmojiCollectionByIdAsync(userId, emojiId) ?? new Emojis();
        PowerManager deltaPower = (PowerManager)newEmoji - (PowerManager)oldEmoji;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarEmojisGalleryAsync(string userId)
    {
        Emojis oldEmoji = await SumPowerEmojisGalleryAsync(userId);

        var updateResult = await _emojisGalleryRepository.UpdateBatchCurrentStarEmojisGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Emojis newEmoji = await SumPowerEmojisGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newEmoji - (PowerManager)oldEmoji;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchEmojisGalleryAsync(string userId, List<Emojis> emojis)
    {
        var insertResult = await _emojisGalleryRepository.InsertBatchEmojisGalleryAsync(userId, emojis);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Emojis> GetEmojiCollectionByIdAsync(string userId, string emojiId)
    {
        var result = await _emojisGalleryRepository.GetEmojiCollectionByIdAsync(userId, emojiId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateEmojiGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        await _emojisGalleryRepository.UpdateEmojiGalleryPowerAsync(userId, Id, await _service.GetEmojiByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
