using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCardLivesService : IUserCardLivesService
{
    private readonly IUserCardLivesRepository _userCardLivesRepository;
    private readonly ICardLivesGalleryService _cardLivesGalleryService;
    private readonly ICardLivesService _cardLivesService;

    public UserCardLivesService(
        IUserCardLivesRepository userCardLivesRepository,
        ICardLivesGalleryService cardLivesGalleryService,
        ICardLivesService cardLivesService)
    {
        _userCardLivesRepository = userCardLivesRepository;
        _cardLivesGalleryService = cardLivesGalleryService;
        _cardLivesService = cardLivesService;
    }

    public static IUserCardLivesService Create() => ServiceContainer.GetService<IUserCardLivesService>();

    public async Task<List<CardLives>> GetUserCardLivesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> result = await _userCardLivesRepository.GetUserCardLivesAsync(userId, search, type, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserCardLivesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCardLivesRepository.GetUserCardLivesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLifeAsync(string userId, CardLives cardLife)
    {
        var checkCardLifeResult = await _cardLivesService.IsCardLifeDeletedOrInactiveAsync(cardLife.Id);
        if (checkCardLifeResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userCardLivesRepository.InsertOrUpdateUserCardLifeAsync(userId, cardLife);

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

        await _cardLivesGalleryService.InsertCardLifeGalleryAsync(userId, cardLife.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLivesBatchAsync(string userId, List<CardLives> cardLives)
    {
        var insertOrUpdateResult = await _userCardLivesRepository.InsertOrUpdateUserCardLivesBatchAsync(userId, cardLives);

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
            await _cardLivesGalleryService.InsertBatchCardLivesGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCardLifeLevelAsync(string userId, CardLives cardLife, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (cardLife == null)
        {
            return InsertOrUpdateResult<bool>.Failure("CardLive is required.");
        }

        var checkResult = await _cardLivesService.IsCardLifeDeletedOrInactiveAsync(cardLife.Id);
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

        var updateResult = await _userCardLivesRepository.UpdateUserCardLifeLevelAsync(
            userId,
            cardLife,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCardLifeStarAsync(string userId, CardLives cardLife)
    {
        var checkCardLifeResult = await _cardLivesService.IsCardLifeDeletedOrInactiveAsync(cardLife.Id);
        if (checkCardLifeResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userCardLivesRepository.UpdateUserCardLifeStarAsync(userId, cardLife);

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

        await _cardLivesGalleryService.UpdateTempStarCardLifeGalleryAsync(userId, cardLife.Id, cardLife.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCardLifeQuantityAsync(string userId, CardLives cardLife, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userCardLivesRepository.UpdateUserCardLifeQuantityAsync(userId, cardLife, totalDeductions);

        return result;
    }

    public async Task<CardLives> GetUserCardLifeByIdAsync(string userId, string Id)
    {
        var result = await _userCardLivesRepository.GetUserCardLifeByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<CardLives> SumPowerUserCardLivesAsync(string userId)
    {
        return await _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);
    }
}
