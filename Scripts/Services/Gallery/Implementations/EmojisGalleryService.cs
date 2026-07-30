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

    public async Task<bool> InsertEmojiGalleryAsync(string userId, string Id)
    {
        var insertResult = await _emojisGalleryRepository.InsertEmojiGalleryAsync(userId, Id, await _emojisService.GetEmojiByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusEmojiGalleryAsync(string userId, string emojiId)
    {
        var updateResult = await _emojisGalleryRepository.UpdateStatusEmojiGalleryAsync(userId, emojiId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Emojis emojiGallery = await GetEmojiCollectionByIdAsync(userId, emojiId) ?? new Emojis();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)emojiGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusEmojisGalleryAsync(string userId)
    {
        Emojis oldEmoji = await SumPowerEmojisGalleryAsync(userId);

        var updateResult = await _emojisGalleryRepository.UpdateBatchStatusEmojisGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Emojis newEmoji = await SumPowerEmojisGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newEmoji - (PowerManager)oldEmoji;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Emojis> SumPowerEmojisGalleryAsync(string userId)
    {
        return await _emojisGalleryRepository.SumPowerEmojisGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarEmojiGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _emojisGalleryRepository.UpdateStarEmojiGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarEmojiGalleryAsync(string userId, string emojiId)
    {
        Emojis oldEmoji = await GetEmojiCollectionByIdAsync(userId, emojiId) ?? new Emojis();

        var updateResult = await _emojisGalleryRepository.UpdateCurrentStarEmojiGalleryAsync(userId, emojiId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Emojis newEmoji = await GetEmojiCollectionByIdAsync(userId, emojiId) ?? new Emojis();
        PowerManager deltaPower = (PowerManager)newEmoji - (PowerManager)oldEmoji;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarEmojisGalleryAsync(string userId)
    {
        Emojis oldEmoji = await SumPowerEmojisGalleryAsync(userId);

        var updateResult = await _emojisGalleryRepository.UpdateBatchCurrentStarEmojisGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Emojis newEmoji = await SumPowerEmojisGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newEmoji - (PowerManager)oldEmoji;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchEmojisGalleryAsync(string userId, List<Emojis> emojis)
    {
        var insertResult = await _emojisGalleryRepository.InsertBatchEmojisGalleryAsync(userId, emojis);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Emojis> GetEmojiCollectionByIdAsync(string userId, string emojiId)
    {
        var result = await _emojisGalleryRepository.GetEmojiCollectionByIdAsync(userId, emojiId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateEmojiGalleryPowerAsync(string userId, string Id)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        await _emojisGalleryRepository.UpdateEmojiGalleryPowerAsync(userId, Id, await _service.GetEmojiByIdAsync(Id));
    }
}
