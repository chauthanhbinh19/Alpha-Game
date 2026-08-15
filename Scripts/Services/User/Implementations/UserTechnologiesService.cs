using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTechnologiesService : IUserTechnologiesService
{
    private readonly IUserTechnologiesRepository _userTechnologiesRepository;
    private readonly ITechnologiesGalleryService _technologiesGalleryService;
    private readonly ITechnologiesService _technologiesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserTechnologiesService(
        IUserTechnologiesRepository userTechnologiesRepository,
        ITechnologiesGalleryService technologiesGalleryService,
        ITechnologiesService technologiesService,
        IPowerManagerService powerManagerService)
    {
        _userTechnologiesRepository = userTechnologiesRepository;
        _technologiesGalleryService = technologiesGalleryService;
        _technologiesService = technologiesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserTechnologiesService Create() => ServiceContainer.GetService<IUserTechnologiesService>();

    public async Task<List<Technologies>> GetUserTechnologiesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Technologies> result = await _userTechnologiesRepository.GetUserTechnologiesAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserTechnologiesCountAsync(string userId, string search, string rare)
    {
        return await _userTechnologiesRepository.GetUserTechnologiesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologyAsync(string userId, Technologies technology)
    {
        var oldTechnologyTask = _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        var oldUserTechnologyTask = _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);

        await Task.WhenAll(oldTechnologyTask, oldUserTechnologyTask);

        Technologies oldTechnology = oldTechnologyTask.Result;
        Technologies oldUserTechnology = oldUserTechnologyTask.Result;

        var insertOrUpdateResult = await _userTechnologiesRepository.InsertOrUpdateUserTechnologyAsync(userId, technology);

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

        await _technologiesGalleryService.InsertTechnologyGalleryAsync(userId, technology.Id);

        var newTechnologyTask = _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        var newUserTechnologyTask = _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);

        await Task.WhenAll(newTechnologyTask, newUserTechnologyTask);

        PowerManager deltaPower = (PowerManager)newTechnologyTask.Result - (PowerManager)oldTechnology;
        PowerManager deltaUserPower = (PowerManager)newUserTechnologyTask.Result - (PowerManager)oldUserTechnology;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologiesBatchAsync(string userId, List<Technologies> technologies)
    {
        var oldTechnologyTask = _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        var oldUserTechnologyTask = _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);

        await Task.WhenAll(oldTechnologyTask, oldUserTechnologyTask);

        Technologies oldTechnology = oldTechnologyTask.Result;
        Technologies oldUserTechnology = oldUserTechnologyTask.Result;

        var insertOrUpdateResult = await _userTechnologiesRepository.InsertOrUpdateUserTechnologiesBatchAsync(userId, technologies);

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
            await _technologiesGalleryService.InsertBatchTechnologiesGalleryAsync(userId, newlyInsertedCards);

            var newTechnologyTask = _technologiesService.SumPowerTechnologiesPercentAsync(userId);
            var newUserTechnologyTask = _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);

            await Task.WhenAll(newTechnologyTask, newUserTechnologyTask);

            PowerManager deltaPower = (PowerManager)newTechnologyTask.Result - (PowerManager)oldTechnology;
            PowerManager deltaUserPower = (PowerManager)newUserTechnologyTask.Result - (PowerManager)oldUserTechnology;

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

    public async Task<bool> UpdateUserTechnologyLevelAsync(string userId, Technologies technology)
    {
        Technologies oldUserTechnology = await _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);

        var updateResult = await _userTechnologiesRepository.UpdateUserTechnologyLevelAsync(userId, technology);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Technologies newUserTechnology = await _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserTechnology - (PowerManager)oldUserTechnology;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserTechnologyStarAsync(string userId, Technologies technology)
    {
        Technologies oldUserTechnology = await _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);

        var updateResult = await _userTechnologiesRepository.UpdateUserTechnologyStarAsync(userId, technology);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _technologiesGalleryService.UpdateTempStarTechnologyGalleryAsync(userId, technology.Id, technology.Star);

        Technologies newUserTechnology = await _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserTechnology - (PowerManager)oldUserTechnology;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Technologies> GetUserTechnologyByIdAsync(string userId, string Id)
    {
        var result = await _userTechnologiesRepository.GetUserTechnologyByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Technologies> SumPowerUserTechnologiesAsync(string userId)
    {
        return await _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);
    }
}
