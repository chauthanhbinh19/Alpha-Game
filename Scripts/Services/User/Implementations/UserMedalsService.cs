using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMedalsService : IUserMedalsService
{
    private readonly IUserMedalsRepository _userMedalsRepository;
    private readonly IMedalsGalleryService _medalsGalleryService;
    private readonly IMedalsService _medalsService;

    public UserMedalsService(
        IUserMedalsRepository userMedalsRepository,
        IMedalsGalleryService medalsGalleryService,
        IMedalsService medalsService)
    {
        _userMedalsRepository = userMedalsRepository;
        _medalsGalleryService = medalsGalleryService;
        _medalsService = medalsService;
    }

    public static IUserMedalsService Create() => ServiceContainer.GetService<IUserMedalsService>();

    public async Task<List<Medals>> GetUserMedalsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Medals> result = await _userMedalsRepository.GetUserMedalsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserMedalsCountAsync(string userId, string search, string rare)
    {
        return await _userMedalsRepository.GetUserMedalsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalAsync(string userId, Medals medal)
    {
        var checkMedalResult = await _medalsService.IsMedalDeletedOrInactiveAsync(medal.Id);
        if (checkMedalResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userMedalsRepository.InsertOrUpdateUserMedalAsync(userId, medal);

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

        await _medalsGalleryService.InsertMedalGalleryAsync(userId, medal.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalsBatchAsync(string userId, List<Medals> medals)
    {
        var insertOrUpdateResult = await _userMedalsRepository.InsertOrUpdateUserMedalsBatchAsync(userId, medals);

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
            await _medalsGalleryService.InsertBatchMedalsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMedalLevelAsync(string userId, Medals medal, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (medal == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Medal is required.");
        }

        var checkResult = await _medalsService.IsMedalDeletedOrInactiveAsync(medal.Id);
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

        var updateResult = await _userMedalsRepository.UpdateUserMedalLevelAsync(
            userId,
            medal,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMedalStarAsync(string userId, Medals medal)
    {
        var checkMedalResult = await _medalsService.IsMedalDeletedOrInactiveAsync(medal.Id);
        if (checkMedalResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userMedalsRepository.UpdateUserMedalStarAsync(userId, medal);

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

        await _medalsGalleryService.UpdateTempStarMedalGalleryAsync(userId, medal.Id, medal.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserMedalQuantityAsync(string userId, Medals medal, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userMedalsRepository.UpdateUserMedalQuantityAsync(userId, medal, totalDeductions);

        return result;
    }

    public async Task<Medals> GetUserMedalByIdAsync(string userId, string Id)
    {
        var result = await _userMedalsRepository.GetUserMedalByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Medals> SumPowerUserMedalsAsync(string userId)
    {
        return await _userMedalsRepository.SumPowerUserMedalsAsync(userId);
    }
}
