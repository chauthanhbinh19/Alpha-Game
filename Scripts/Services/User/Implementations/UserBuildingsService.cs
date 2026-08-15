using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBuildingsService : IUserBuildingsService
{
    private readonly IUserBuildingsRepository _userBuildingsRepository;
    private readonly IBuildingsGalleryService _buildingsGalleryService;
    private readonly IBuildingsService _buildingsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserBuildingsService(
        IUserBuildingsRepository userBuildingsRepository,
        IBuildingsGalleryService buildingsGalleryService,
        IBuildingsService buildingsService,
        IPowerManagerService powerManagerService)
    {
        _userBuildingsRepository = userBuildingsRepository;
        _buildingsGalleryService = buildingsGalleryService;
        _buildingsService = buildingsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserBuildingsService Create() => ServiceContainer.GetService<IUserBuildingsService>();

    public async Task<List<Buildings>> GetUserBuildingsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> result = await _userBuildingsRepository.GetUserBuildingsAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserBuildingsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userBuildingsRepository.GetUserBuildingsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingAsync(string userId, Buildings building)
    {
        var oldBuildingTask = _buildingsService.SumPowerBuildingsPercentAsync(userId);
        var oldUserBuildingTask = _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);

        await Task.WhenAll(oldBuildingTask, oldUserBuildingTask);

        Buildings oldBuilding = oldBuildingTask.Result;
        Buildings oldUserBuilding = oldUserBuildingTask.Result;

        var insertOrUpdateResult = await _userBuildingsRepository.InsertOrUpdateUserBuildingAsync(userId, building);

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

        await _buildingsGalleryService.InsertBuildingGalleryAsync(userId, building.Id);

        var newBuildingTask = _buildingsService.SumPowerBuildingsPercentAsync(userId);
        var newUserBuildingTask = _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);

        await Task.WhenAll(newBuildingTask, newUserBuildingTask);

        PowerManager deltaPower = (PowerManager)newBuildingTask.Result - (PowerManager)oldBuilding;
        PowerManager deltaUserPower = (PowerManager)newUserBuildingTask.Result - (PowerManager)oldUserBuilding;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingsBatchAsync(string userId, List<Buildings> buildings)
    {
        var oldBuildingTask = _buildingsService.SumPowerBuildingsPercentAsync(userId);
        var oldUserBuildingTask = _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);

        await Task.WhenAll(oldBuildingTask, oldUserBuildingTask);

        Buildings oldBuilding = oldBuildingTask.Result;
        Buildings oldUserBuilding = oldUserBuildingTask.Result;

        var insertOrUpdateResult = await _userBuildingsRepository.InsertOrUpdateUserBuildingsBatchAsync(userId, buildings);

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
            await _buildingsGalleryService.InsertBatchBuildingsGalleryAsync(userId, newlyInsertedCards);

            var newBuildingTask = _buildingsService.SumPowerBuildingsPercentAsync(userId);
            var newUserBuildingTask = _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);

            await Task.WhenAll(newBuildingTask, newUserBuildingTask);

            PowerManager deltaPower = (PowerManager)newBuildingTask.Result - (PowerManager)oldBuilding;
            PowerManager deltaUserPower = (PowerManager)newUserBuildingTask.Result - (PowerManager)oldUserBuilding;

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

    public async Task<bool> UpdateUserBuildingLevelAsync(string userId, Buildings building)
    {
        Buildings oldUserBuilding = await _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);

        var updateResult = await _userBuildingsRepository.UpdateUserBuildingLevelAsync(userId, building);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Buildings newUserBuilding = await _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserBuilding - (PowerManager)oldUserBuilding;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserBuildingStarAsync(string userId, Buildings building)
    {
        Buildings oldUserBuilding = await _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);

        var updateResult = await _userBuildingsRepository.UpdateUserBuildingStarAsync(userId, building);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _buildingsGalleryService.UpdateTempStarBuildingGalleryAsync(userId, building.Id, building.Star);

        Buildings newUserBuilding = await _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserBuilding - (PowerManager)oldUserBuilding;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Buildings> GetUserBuildingByIdAsync(string userId, string Id)
    {
        var result = await _userBuildingsRepository.GetUserBuildingByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Buildings> SumPowerUserBuildingsAsync(string userId)
    {
        return await _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);
    }
}
