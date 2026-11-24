using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArchitecturesService : IUserArchitecturesService
{
    private readonly IUserArchitecturesRepository _userArchitecturesRepository;
    private readonly IArchitecturesGalleryService _architecturesGalleryService;
    private readonly IArchitecturesService _architecturesService;

    public UserArchitecturesService(
        IUserArchitecturesRepository userArchitecturesRepository,
        IArchitecturesGalleryService architecturesGalleryService,
        IArchitecturesService architecturesService)
    {
        _userArchitecturesRepository = userArchitecturesRepository;
        _architecturesGalleryService = architecturesGalleryService;
        _architecturesService = architecturesService;
    }

    public static IUserArchitecturesService Create() => ServiceContainer.GetService<IUserArchitecturesService>();

    public async Task<List<Architectures>> GetUserArchitecturesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Architectures> result = await _userArchitecturesRepository.GetUserArchitecturesAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserArchitecturesCountAsync(string userId, string search, string rare)
    {
        return await _userArchitecturesRepository.GetUserArchitecturesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArchitectureAsync(string userId, Architectures architecture)
    {
        var checkArchitectureResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(architecture.Id);
        if (checkArchitectureResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userArchitecturesRepository.InsertOrUpdateUserArchitectureAsync(userId, architecture);

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

        await _architecturesGalleryService.InsertArchitectureGalleryAsync(userId, architecture.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArchitecturesBatchAsync(string userId, List<Architectures> architectures)
    {
        var insertOrUpdateResult = await _userArchitecturesRepository.InsertOrUpdateUserArchitecturesBatchAsync(userId, architectures);

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
            await _architecturesGalleryService.InsertBatchArchitecturesGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserArchitectureLevelAsync(string userId, Architectures architecture, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (architecture == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Architecture is required.");
        }

        var checkResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(architecture.Id);
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

        var updateResult = await _userArchitecturesRepository.UpdateUserArchitectureLevelAsync(
            userId,
            architecture,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserArchitectureStarAsync(string userId, Architectures architecture)
    {
        var checkArchitectureResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(architecture.Id);
        if (checkArchitectureResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userArchitecturesRepository.UpdateUserArchitectureStarAsync(userId, architecture);

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

        await _architecturesGalleryService.UpdateTempStarArchitectureGalleryAsync(userId, architecture.Id, architecture.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserArchitectureQuantityAsync(string userId, Architectures architecture, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userArchitecturesRepository.UpdateUserArchitectureQuantityAsync(userId, architecture, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserArchitectureAffinityAsync(string userId, Architectures architecture, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (architecture == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Architecture is required.");
        }

        var checkResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(architecture.Id);
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

        var updateResult = await _userArchitecturesRepository.UpdateUserArchitectureAffinityAsync(
            userId,
            architecture,
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

    public async Task<Architectures> GetUserArchitectureByIdAsync(string userId, string Id)
    {
        var result = await _userArchitecturesRepository.GetUserArchitectureByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Architectures> SumPowerUserArchitecturesAsync(string userId)
    {
        return await _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);
    }
}
