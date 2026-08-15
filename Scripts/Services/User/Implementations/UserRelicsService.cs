using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRelicsService : IUserRelicsService
{
    private readonly IUserRelicsRepository _userRelicsRepository;
    private readonly IRelicsGalleryService _relicsGalleryService;
    private readonly IRelicsService _relicsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserRelicsService(
        IUserRelicsRepository userRelicsRepository,
        IRelicsGalleryService relicsGalleryService,
        IRelicsService relicsService,
        IPowerManagerService powerManagerService)
    {
        _userRelicsRepository = userRelicsRepository;
        _relicsGalleryService = relicsGalleryService;
        _relicsService = relicsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserRelicsService Create() => ServiceContainer.GetService<IUserRelicsService>();

    public async Task<List<Relics>> GetUserRelicsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> result = await _userRelicsRepository.GetUserRelicsAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserRelicsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userRelicsRepository.GetUserRelicsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicAsync(string userId, Relics relic)
    {
        var oldRelicTask = _relicsService.SumPowerRelicsPercentAsync(userId);
        var oldUserRelicTask = _userRelicsRepository.SumPowerUserRelicsAsync(userId);

        await Task.WhenAll(oldRelicTask, oldUserRelicTask);

        Relics oldRelic = oldRelicTask.Result;
        Relics oldUserRelic = oldUserRelicTask.Result;

        var insertOrUpdateResult = await _userRelicsRepository.InsertOrUpdateUserRelicAsync(userId, relic);

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

        await _relicsGalleryService.InsertRelicGalleryAsync(userId, relic.Id);

        var newRelicTask = _relicsService.SumPowerRelicsPercentAsync(userId);
        var newUserRelicTask = _userRelicsRepository.SumPowerUserRelicsAsync(userId);

        await Task.WhenAll(newRelicTask, newUserRelicTask);

        PowerManager deltaPower = (PowerManager)newRelicTask.Result - (PowerManager)oldRelic;
        PowerManager deltaUserPower = (PowerManager)newUserRelicTask.Result - (PowerManager)oldUserRelic;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicsBatchAsync(string userId, List<Relics> relics)
    {
        var oldRelicTask = _relicsService.SumPowerRelicsPercentAsync(userId);
        var oldUserRelicTask = _userRelicsRepository.SumPowerUserRelicsAsync(userId);

        await Task.WhenAll(oldRelicTask, oldUserRelicTask);

        Relics oldRelic = oldRelicTask.Result;
        Relics oldUserRelic = oldUserRelicTask.Result;

        var insertOrUpdateResult = await _userRelicsRepository.InsertOrUpdateUserRelicsBatchAsync(userId, relics);

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
            await _relicsGalleryService.InsertBatchRelicsGalleryAsync(userId, newlyInsertedCards);

            var newRelicTask = _relicsService.SumPowerRelicsPercentAsync(userId);
            var newUserRelicTask = _userRelicsRepository.SumPowerUserRelicsAsync(userId);

            await Task.WhenAll(newRelicTask, newUserRelicTask);

            PowerManager deltaPower = (PowerManager)newRelicTask.Result - (PowerManager)oldRelic;
            PowerManager deltaUserPower = (PowerManager)newUserRelicTask.Result - (PowerManager)oldUserRelic;

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

    public async Task<bool> UpdateUserRelicLevelAsync(string userId, Relics relic)
    {
        Relics oldUserRelic = await _userRelicsRepository.SumPowerUserRelicsAsync(userId);

        var updateResult = await _userRelicsRepository.UpdateUserRelicLevelAsync(userId, relic);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Relics newUserRelic = await _userRelicsRepository.SumPowerUserRelicsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserRelic - (PowerManager)oldUserRelic;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserRelicStarAsync(string userId, Relics relic)
    {
        Relics oldUserRelic = await _userRelicsRepository.SumPowerUserRelicsAsync(userId);

        var updateResult = await _userRelicsRepository.UpdateUserRelicStarAsync(userId, relic);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _relicsGalleryService.UpdateTempStarRelicGalleryAsync(userId, relic.Id, relic.Star);

        Relics newUserRelic = await _userRelicsRepository.SumPowerUserRelicsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserRelic - (PowerManager)oldUserRelic;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Relics> GetUserRelicByIdAsync(string userId, string Id)
    {
        var result = await _userRelicsRepository.GetUserRelicByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Relics> SumPowerUserRelicsAsync(string userId)
    {
        return await _userRelicsRepository.SumPowerUserRelicsAsync(userId);
    }
}
