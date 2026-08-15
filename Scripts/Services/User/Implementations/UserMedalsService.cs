using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMedalsService : IUserMedalsService
{
    private readonly IUserMedalsRepository _userMedalsRepository;
    private readonly IMedalsGalleryService _medalsGalleryService;
    private readonly IMedalsService _medalsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserMedalsService(
        IUserMedalsRepository userMedalsRepository,
        IMedalsGalleryService medalsGalleryService,
        IMedalsService medalsService,
        IPowerManagerService powerManagerService)
    {
        _userMedalsRepository = userMedalsRepository;
        _medalsGalleryService = medalsGalleryService;
        _medalsService = medalsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserMedalsService Create() => ServiceContainer.GetService<IUserMedalsService>();

    public async Task<List<Medals>> GetUserMedalsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Medals> result = await _userMedalsRepository.GetUserMedalsAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserMedalsCountAsync(string userId, string search, string rare)
    {
        return await _userMedalsRepository.GetUserMedalsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalAsync(string userId, Medals medal)
    {
        var oldMedalTask = _medalsService.SumPowerMedalsPercentAsync(userId);
        var oldUserMedalTask = _userMedalsRepository.SumPowerUserMedalsAsync(userId);

        await Task.WhenAll(oldMedalTask, oldUserMedalTask);

        Medals oldMedal = oldMedalTask.Result;
        Medals oldUserMedal = oldUserMedalTask.Result;

        var insertOrUpdateResult = await _userMedalsRepository.InsertOrUpdateUserMedalAsync(userId, medal);

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

        await _medalsGalleryService.InsertMedalGalleryAsync(userId, medal.Id);

        var newMedalTask = _medalsService.SumPowerMedalsPercentAsync(userId);
        var newUserMedalTask = _userMedalsRepository.SumPowerUserMedalsAsync(userId);

        await Task.WhenAll(newMedalTask, newUserMedalTask);

        PowerManager deltaPower = (PowerManager)newMedalTask.Result - (PowerManager)oldMedal;
        PowerManager deltaUserPower = (PowerManager)newUserMedalTask.Result - (PowerManager)oldUserMedal;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalsBatchAsync(string userId, List<Medals> medals)
    {
        var oldMedalTask = _medalsService.SumPowerMedalsPercentAsync(userId);
        var oldUserMedalTask = _userMedalsRepository.SumPowerUserMedalsAsync(userId);

        await Task.WhenAll(oldMedalTask, oldUserMedalTask);

        Medals oldMedal = oldMedalTask.Result;
        Medals oldUserMedal = oldUserMedalTask.Result;

        var insertOrUpdateResult = await _userMedalsRepository.InsertOrUpdateUserMedalsBatchAsync(userId, medals);

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
            await _medalsGalleryService.InsertBatchMedalsGalleryAsync(userId, newlyInsertedCards);

            var newMedalTask = _medalsService.SumPowerMedalsPercentAsync(userId);
            var newUserMedalTask = _userMedalsRepository.SumPowerUserMedalsAsync(userId);

            await Task.WhenAll(newMedalTask, newUserMedalTask);

            PowerManager deltaPower = (PowerManager)newMedalTask.Result - (PowerManager)oldMedal;
            PowerManager deltaUserPower = (PowerManager)newUserMedalTask.Result - (PowerManager)oldUserMedal;

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

    public async Task<bool> UpdateUserMedalLevelAsync(string userId, Medals medal)
    {
        Medals oldUserMedal = await _userMedalsRepository.SumPowerUserMedalsAsync(userId);

        var updateResult = await _userMedalsRepository.UpdateUserMedalLevelAsync(userId, medal);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Medals newUserMedal = await _userMedalsRepository.SumPowerUserMedalsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserMedal - (PowerManager)oldUserMedal;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserMedalStarAsync(string userId, Medals medal)
    {
        Medals oldUserMedal = await _userMedalsRepository.SumPowerUserMedalsAsync(userId);

        var updateResult = await _userMedalsRepository.UpdateUserMedalStarAsync(userId, medal);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _medalsGalleryService.UpdateTempStarMedalGalleryAsync(userId, medal.Id, medal.Star);

        Medals newUserMedal = await _userMedalsRepository.SumPowerUserMedalsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserMedal - (PowerManager)oldUserMedal;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Medals> GetUserMedalByIdAsync(string userId, string Id)
    {
        var result = await _userMedalsRepository.GetUserMedalByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Medals> SumPowerUserMedalsAsync(string userId)
    {
        return await _userMedalsRepository.SumPowerUserMedalsAsync(userId);
    }
}
