using System.Collections.Generic;
using System.Threading.Tasks;

public class UserEmojisService : IUserEmojisService
{
    private readonly IUserEmojisRepository _userEmojisRepository;
    private readonly IEmojisGalleryService _emojisGalleryService;
    private readonly IEmojisService _emojisService;

    public UserEmojisService(
        IUserEmojisRepository userEmojisRepository,
        IEmojisGalleryService emojisGalleryService,
        IEmojisService emojisService)
    {
        _userEmojisRepository = userEmojisRepository;
        _emojisGalleryService = emojisGalleryService;
        _emojisService = emojisService;
    }

    public static IUserEmojisService Create() => ServiceContainer.GetService<IUserEmojisService>();

    public async Task<List<Emojis>> GetUserEmojisAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> result = await _userEmojisRepository.GetUserEmojisAsync(userId, search, pageSize, offset, rare);


        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }
        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserEmojisCountAsync(string userId, string search, string rare)
    {
        return await _userEmojisRepository.GetUserEmojisCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEmojiAsync(string userId, Emojis emoji)
    {
        var checkEmojiResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(emoji.Id);
        if (checkEmojiResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userEmojisRepository.InsertOrUpdateUserEmojiAsync(userId, emoji);

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

        await _emojisGalleryService.InsertEmojiGalleryAsync(userId, emoji.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEmojisBatchAsync(string userId, List<Emojis> emojis)
    {
        var insertOrUpdateResult = await _userEmojisRepository.InsertOrUpdateUserEmojisBatchAsync(userId, emojis);

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
            await _emojisGalleryService.InsertBatchEmojisGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserEmojiLevelAsync(string userId, Emojis emoji, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (emoji == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Emoji is required.");
        }

        var checkResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(emoji.Id);
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

        var updateResult = await _userEmojisRepository.UpdateUserEmojiLevelAsync(
            userId,
            emoji,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserEmojiStarAsync(string userId, Emojis emoji)
    {
        var checkEmojiResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(emoji.Id);
        if (checkEmojiResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userEmojisRepository.UpdateUserEmojiStarAsync(userId, emoji);

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

        await _emojisGalleryService.UpdateTempStarEmojiGalleryAsync(userId, emoji.Id, emoji.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserEmojiQuantityAsync(string userId, Emojis emoji, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userEmojisRepository.UpdateUserEmojiQuantityAsync(userId, emoji, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserEmojiAffinityAsync(string userId, Emojis emoji, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (emoji == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Emoji is required.");
        }

        var checkResult = await _emojisService.IsEmojiDeletedOrInactiveAsync(emoji.Id);
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

        var updateResult = await _userEmojisRepository.UpdateUserEmojiAffinityAsync(
            userId,
            emoji,
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

    public async Task<Emojis> GetUserEmojiByIdAsync(string userId, string Id)
    {
        var result = await _userEmojisRepository.GetUserEmojiByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Emojis> SumPowerUserEmojisAsync(string userId)
    {
        return await _userEmojisRepository.SumPowerUserEmojisAsync(userId);
    }
}
