using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtifactsService : IUserArtifactsService
{
    private readonly IUserArtifactsRepository _userArtifactsRepository;
    private readonly IArtifactsGalleryService _artifactsGalleryService;
    private readonly IArtifactsService _artifactsService;

    public UserArtifactsService(
        IUserArtifactsRepository userArtifactsRepository,
        IArtifactsGalleryService artifactsGalleryService,
        IArtifactsService artifactsService)
    {
        _userArtifactsRepository = userArtifactsRepository;
        _artifactsGalleryService = artifactsGalleryService;
        _artifactsService = artifactsService;
    }

    public static IUserArtifactsService Create() => ServiceContainer.GetService<IUserArtifactsService>();

    public async Task<List<Artifacts>> GetUserArtifactsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> result = await _userArtifactsRepository.GetUserArtifactsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserArtifactsCountAsync(string userId, string search, string rare)
    {
        return await _userArtifactsRepository.GetUserArtifactsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactAsync(string userId, Artifacts artifact)
    {
        var checkArtifactResult = await _artifactsService.IsArtifactDeletedOrInactiveAsync(artifact.Id);
        if (checkArtifactResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userArtifactsRepository.InsertOrUpdateUserArtifactAsync(userId, artifact);

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

        await _artifactsGalleryService.InsertArtifactGalleryAsync(userId, artifact.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactsBatchAsync(string userId, List<Artifacts> artifacts)
    {
        var insertOrUpdateResult = await _userArtifactsRepository.InsertOrUpdateUserArtifactsBatchAsync(userId, artifacts);

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
            await _artifactsGalleryService.InsertBatchArtifactsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserArtifactLevelAsync(string userId, Artifacts artifact, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (artifact == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Artifact is required.");
        }

        var checkResult = await _artifactsService.IsArtifactDeletedOrInactiveAsync(artifact.Id);
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

        var updateResult = await _userArtifactsRepository.UpdateUserArtifactLevelAsync(
            userId,
            artifact,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserArtifactStarAsync(string userId, Artifacts artifact)
    {
        var checkArtifactResult = await _artifactsService.IsArtifactDeletedOrInactiveAsync(artifact.Id);
        if (checkArtifactResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userArtifactsRepository.UpdateUserArtifactStarAsync(userId, artifact);

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

        await _artifactsGalleryService.UpdateTempStarArtifactGalleryAsync(userId, artifact.Id, artifact.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserArtifactQuantityAsync(string userId, Artifacts artifact, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userArtifactsRepository.UpdateUserArtifactQuantityAsync(userId, artifact, totalDeductions);

        return result;
    }
    
    public async Task<Artifacts> GetUserArtifactByIdAsync(string userId, string Id)
    {
        var result = await _userArtifactsRepository.GetUserArtifactByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Artifacts> SumPowerUserArtifactsAsync(string userId)
    {
        return await _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);
    }
}
