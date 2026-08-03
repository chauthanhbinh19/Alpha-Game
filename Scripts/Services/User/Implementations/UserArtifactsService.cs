using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtifactsService : IUserArtifactsService
{
    private readonly IUserArtifactsRepository _userArtifactsRepository;
    private readonly IArtifactsGalleryService _artifactsGalleryService;
    private readonly IArtifactsService _artifactsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserArtifactsService(
        IUserArtifactsRepository userArtifactsRepository,
        IArtifactsGalleryService artifactsGalleryService,
        IArtifactsService artifactsService,
        IPowerManagerService powerManagerService)
    {
        _userArtifactsRepository = userArtifactsRepository;
        _artifactsGalleryService = artifactsGalleryService;
        _artifactsService = artifactsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserArtifactsService Create() => ServiceContainer.GetService<IUserArtifactsService>();

    public async Task<List<Artifacts>> GetUserArtifactsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> list = await _userArtifactsRepository.GetUserArtifactsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in list)
        {
            item.BaseStats = new BaseStats(item);
        }

        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtifactsCountAsync(string userId, string search, string rare)
    {
        return await _userArtifactsRepository.GetUserArtifactsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactAsync(string userId, Artifacts artifact)
    {
        var oldArtifactTask = _artifactsService.SumPowerArtifactsPercentAsync(userId);
        var oldUserArtifactTask = _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);

        await Task.WhenAll(oldArtifactTask, oldUserArtifactTask);

        Artifacts oldArtifact = oldArtifactTask.Result;
        Artifacts oldUserArtifact = oldUserArtifactTask.Result;

        var insertOrUpdateResult = await _userArtifactsRepository.InsertOrUpdateUserArtifactAsync(userId, artifact);

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

        await _artifactsGalleryService.InsertArtifactGalleryAsync(userId, artifact.Id);

        var newArtifactTask = _artifactsService.SumPowerArtifactsPercentAsync(userId);
        var newUserArtifactTask = _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);

        await Task.WhenAll(newArtifactTask, newUserArtifactTask);

        PowerManager deltaPower = (PowerManager)newArtifactTask.Result - (PowerManager)oldArtifact;
        PowerManager deltaUserPower = (PowerManager)newUserArtifactTask.Result - (PowerManager)oldUserArtifact;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactsBatchAsync(string userId, List<Artifacts> artifacts)
    {
        var oldArtifactTask = _artifactsService.SumPowerArtifactsPercentAsync(userId);
        var oldUserArtifactTask = _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);

        await Task.WhenAll(oldArtifactTask, oldUserArtifactTask);

        Artifacts oldArtifact = oldArtifactTask.Result;
        Artifacts oldUserArtifact = oldUserArtifactTask.Result;

        var insertOrUpdateResult = await _userArtifactsRepository.InsertOrUpdateUserArtifactsBatchAsync(userId, artifacts);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _artifactsGalleryService.InsertBatchArtifactsGalleryAsync(userId, newlyInsertedCards);

            var newArtifactTask = _artifactsService.SumPowerArtifactsPercentAsync(userId);
            var newUserArtifactTask = _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);

            await Task.WhenAll(newArtifactTask, newUserArtifactTask);

            PowerManager deltaPower = (PowerManager)newArtifactTask.Result - (PowerManager)oldArtifact;
            PowerManager deltaUserPower = (PowerManager)newUserArtifactTask.Result - (PowerManager)oldUserArtifact;

            PowerManager totalDelta = new PowerManager();
            if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
            if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

            if (totalDelta.HasAnyPositiveStat())
            {
                PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
                PowerManager updatedPower = currentPower + totalDelta;
                await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
            }
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
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserArtifactLevelAsync(string userId, Artifacts artifact)
    {
        Artifacts oldUserArtifact = await _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);

        var updateResult = await _userArtifactsRepository.UpdateUserArtifactLevelAsync(userId, artifact);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Artifacts newUserArtifact = await _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserArtifact - (PowerManager)oldUserArtifact;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserArtifactStarAsync(string userId, Artifacts artifact)
    {
        Artifacts oldUserArtifact = await _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);

        var updateResult = await _userArtifactsRepository.UpdateUserArtifactStarAsync(userId, artifact);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _artifactsGalleryService.UpdateTempStarArtifactGalleryAsync(userId, artifact.Id, artifact.Star);

        Artifacts newUserArtifact = await _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserArtifact - (PowerManager)oldUserArtifact;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Artifacts> GetUserArtifactByIdAsync(string userId, string Id)
    {
        var result = await _userArtifactsRepository.GetUserArtifactByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Artifacts> SumPowerUserArtifactsAsync(string userId)
    {
        return await _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);
    }
}
