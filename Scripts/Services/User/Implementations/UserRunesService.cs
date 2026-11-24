using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRunesService : IUserRunesService
{
    private readonly IUserRunesRepository _userRunesRepository;
    private readonly IRunesGalleryService _runesGalleryService;
    private readonly IRunesService _runesService;

    public UserRunesService(
        IUserRunesRepository userRunesRepository,
        IRunesGalleryService runesGalleryService,
        IRunesService runesService)
    {
        _userRunesRepository = userRunesRepository;
        _runesGalleryService = runesGalleryService;
        _runesService = runesService;
    }

    public static IUserRunesService Create() => ServiceContainer.GetService<IUserRunesService>();

    public async Task<List<Runes>> GetUserRunesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Runes> result = await _userRunesRepository.GetUserRunesAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserRunesCountAsync(string userId, string search, string rare)
    {
        return await _userRunesRepository.GetUserRunesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRuneAsync(string userId, Runes rune)
    {
        var checkRuneResult = await _runesService.IsRuneDeletedOrInactiveAsync(rune.Id);
        if (checkRuneResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userRunesRepository.InsertOrUpdateUserRuneAsync(userId, rune);

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

        await _runesGalleryService.InsertRuneGalleryAsync(userId, rune.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRunesBatchAsync(string userId, List<Runes> runes)
    {
        var insertOrUpdateResult = await _userRunesRepository.InsertOrUpdateUserRunesBatchAsync(userId, runes);

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
            await _runesGalleryService.InsertBatchRunesGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRuneLevelAsync(string userId, Runes rune, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (rune == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Rune is required.");
        }

        var checkResult = await _runesService.IsRuneDeletedOrInactiveAsync(rune.Id);
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

        var updateResult = await _userRunesRepository.UpdateUserRuneLevelAsync(
            userId,
            rune,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRuneStarAsync(string userId, Runes rune)
    {
        var checkRuneResult = await _runesService.IsRuneDeletedOrInactiveAsync(rune.Id);
        if (checkRuneResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userRunesRepository.UpdateUserRuneStarAsync(userId, rune);

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

        await _runesGalleryService.UpdateTempStarRuneGalleryAsync(userId, rune.Id, rune.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRuneQuantityAsync(string userId, Runes rune, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userRunesRepository.UpdateUserRuneQuantityAsync(userId, rune, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserRuneAffinityAsync(string userId, Runes rune, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (rune == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Rune is required.");
        }

        var checkResult = await _runesService.IsRuneDeletedOrInactiveAsync(rune.Id);
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

        var updateResult = await _userRunesRepository.UpdateUserRuneAffinityAsync(
            userId,
            rune,
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

    public async Task<Runes> GetUserRuneByIdAsync(string userId, string Id)
    {
        var result = await _userRunesRepository.GetUserRuneByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Runes> SumPowerUserRunesAsync(string userId)
    {
        return await _userRunesRepository.SumPowerUserRunesAsync(userId);
    }
}
