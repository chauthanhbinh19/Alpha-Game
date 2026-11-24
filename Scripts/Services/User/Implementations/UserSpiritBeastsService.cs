using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritBeastsService : IUserSpiritBeastsService
{
    private readonly IUserSpiritBeastsRepository _userSpiritBeastsRepository;
    private readonly ISpiritBeastsGalleryService _spiritBeastsGalleryService;
    private readonly ISpiritBeastsService _spiritBeastsService;

    public UserSpiritBeastsService(
        IUserSpiritBeastsRepository userSpiritBeastsRepository,
        ISpiritBeastsGalleryService spiritBeastsGalleryService,
        ISpiritBeastsService spiritBeastsService)
    {
        _userSpiritBeastsRepository = userSpiritBeastsRepository;
        _spiritBeastsGalleryService = spiritBeastsGalleryService;
        _spiritBeastsService = spiritBeastsService;
    }

    public static IUserSpiritBeastsService Create() => ServiceContainer.GetService<IUserSpiritBeastsService>();

    public async Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> result = await _userSpiritBeastsRepository.GetUserSpiritBeastsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserSpiritBeastsCountAsync(string userId, string search, string rare)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastAsync(string userId, SpiritBeasts spiritBeast)
    {
        var checkSpiritBeastResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(spiritBeast.Id);
        if (checkSpiritBeastResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userSpiritBeastsRepository.InsertOrUpdateUserSpiritBeastAsync(userId, spiritBeast);

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

        await _spiritBeastsGalleryService.InsertSpiritBeastGalleryAsync(userId, spiritBeast.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastsBatchAsync(string userId, List<SpiritBeasts> spiritBeasts)
    {
        var insertOrUpdateResult = await _userSpiritBeastsRepository.InsertOrUpdateUserSpiritBeastsBatchAsync(userId, spiritBeasts);

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
            await _spiritBeastsGalleryService.InsertBatchSpiritBeastsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserSpiritBeastLevelAsync(string userId, SpiritBeasts spiritBeast, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (spiritBeast == null)
        {
            return InsertOrUpdateResult<bool>.Failure("SpiritBeast is required.");
        }

        var checkResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(spiritBeast.Id);
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

        var updateResult = await _userSpiritBeastsRepository.UpdateUserSpiritBeastLevelAsync(
            userId,
            spiritBeast,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserSpiritBeastStarAsync(string userId, SpiritBeasts spiritBeast)
    {
        var checkSpiritBeastResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(spiritBeast.Id);
        if (checkSpiritBeastResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userSpiritBeastsRepository.UpdateUserSpiritBeastStarAsync(userId, spiritBeast);

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

        await _spiritBeastsGalleryService.UpdateTempStarSpiritBeastGalleryAsync(userId, spiritBeast.Id, spiritBeast.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserSpiritBeastQuantityAsync(string userId, SpiritBeasts spiritBeast, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userSpiritBeastsRepository.UpdateUserSpiritBeastQuantityAsync(userId, spiritBeast, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserSpiritBeastAffinityAsync(string userId, SpiritBeasts spiritBeast, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (spiritBeast == null)
        {
            return InsertOrUpdateResult<bool>.Failure("SpiritBeast is required.");
        }

        var checkResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(spiritBeast.Id);
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

        var updateResult = await _userSpiritBeastsRepository.UpdateUserSpiritBeastAffinityAsync(
            userId,
            spiritBeast,
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

    public async Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string userId, string Id)
    {
        var result = await _userSpiritBeastsRepository.GetUserSpiritBeastByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync(string userId)
    {
        return await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);
    }
}
