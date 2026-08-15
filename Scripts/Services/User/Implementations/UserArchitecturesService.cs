using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArchitecturesService : IUserArchitecturesService
{
    private readonly IUserArchitecturesRepository _userArchitecturesRepository;
    private readonly IArchitecturesGalleryService _architecturesGalleryService;
    private readonly IArchitecturesService _architecturesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserArchitecturesService(
        IUserArchitecturesRepository userArchitecturesRepository,
        IArchitecturesGalleryService architecturesGalleryService,
        IArchitecturesService architecturesService,
        IPowerManagerService powerManagerService)
    {
        _userArchitecturesRepository = userArchitecturesRepository;
        _architecturesGalleryService = architecturesGalleryService;
        _architecturesService = architecturesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserArchitecturesService Create() => ServiceContainer.GetService<IUserArchitecturesService>();

    public async Task<List<Architectures>> GetUserArchitecturesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Architectures> result = await _userArchitecturesRepository.GetUserArchitecturesAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserArchitecturesCountAsync(string userId, string search, string rare)
    {
        return await _userArchitecturesRepository.GetUserArchitecturesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArchitectureAsync(string userId, Architectures architecture)
    {
        var oldArchitectureTask = _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        var oldUserArchitectureTask = _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);

        await Task.WhenAll(oldArchitectureTask, oldUserArchitectureTask);

        Architectures oldArchitecture = oldArchitectureTask.Result;
        Architectures oldUserArchitecture = oldUserArchitectureTask.Result;

        var insertOrUpdateResult = await _userArchitecturesRepository.InsertOrUpdateUserArchitectureAsync(userId, architecture);

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

        await _architecturesGalleryService.InsertArchitectureGalleryAsync(userId, architecture.Id);

        var newArchitectureTask = _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        var newUserArchitectureTask = _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);

        await Task.WhenAll(newArchitectureTask, newUserArchitectureTask);

        PowerManager deltaPower = (PowerManager)newArchitectureTask.Result - (PowerManager)oldArchitecture;
        PowerManager deltaUserPower = (PowerManager)newUserArchitectureTask.Result - (PowerManager)oldUserArchitecture;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArchitecturesBatchAsync(string userId, List<Architectures> architectures)
    {
        var oldArchitectureTask = _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        var oldUserArchitectureTask = _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);

        await Task.WhenAll(oldArchitectureTask, oldUserArchitectureTask);

        Architectures oldArchitecture = oldArchitectureTask.Result;
        Architectures oldUserArchitecture = oldUserArchitectureTask.Result;

        var insertOrUpdateResult = await _userArchitecturesRepository.InsertOrUpdateUserArchitecturesBatchAsync(userId, architectures);

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
            await _architecturesGalleryService.InsertBatchArchitecturesGalleryAsync(userId, newlyInsertedCards);

            var newArchitectureTask = _architecturesService.SumPowerArchitecturesPercentAsync(userId);
            var newUserArchitectureTask = _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);

            await Task.WhenAll(newArchitectureTask, newUserArchitectureTask);

            PowerManager deltaPower = (PowerManager)newArchitectureTask.Result - (PowerManager)oldArchitecture;
            PowerManager deltaUserPower = (PowerManager)newUserArchitectureTask.Result - (PowerManager)oldUserArchitecture;

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

    public async Task<bool> UpdateUserArchitectureLevelAsync(string userId, Architectures architecture)
    {
        Architectures oldUserArchitecture = await _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);

        var updateResult = await _userArchitecturesRepository.UpdateUserArchitectureLevelAsync(userId, architecture);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Architectures newUserArchitecture = await _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserArchitecture - (PowerManager)oldUserArchitecture;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserArchitectureStarAsync(string userId, Architectures architecture)
    {
        Architectures oldUserArchitecture = await _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);

        var updateResult = await _userArchitecturesRepository.UpdateUserArchitectureStarAsync(userId, architecture);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _architecturesGalleryService.UpdateTempStarArchitectureGalleryAsync(userId, architecture.Id, architecture.Star);

        Architectures newUserArchitecture = await _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserArchitecture - (PowerManager)oldUserArchitecture;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Architectures> GetUserArchitectureByIdAsync(string userId, string Id)
    {
        var result = await _userArchitecturesRepository.GetUserArchitectureByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Architectures> SumPowerUserArchitecturesAsync(string userId)
    {
        return await _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);
    }
}
