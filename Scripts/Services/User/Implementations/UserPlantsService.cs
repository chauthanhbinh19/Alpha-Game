using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPlantsService : IUserPlantsService
{
    private readonly IUserPlantsRepository _userPlantsRepository;
    private readonly IPlantsGalleryService _plantsGalleryService;
    private readonly IPlantsService _plantsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserPlantsService(
        IUserPlantsRepository userPlantsRepository,
        IPlantsGalleryService plantsGalleryService,
        IPlantsService plantsService,
        IPowerManagerService powerManagerService)
    {
        _userPlantsRepository = userPlantsRepository;
        _plantsGalleryService = plantsGalleryService;
        _plantsService = plantsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserPlantsService Create() => ServiceContainer.GetService<IUserPlantsService>();

    public async Task<List<Plants>> GetUserPlantsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Plants> result = await _userPlantsRepository.GetUserPlantsAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserPlantsCountAsync(string userId, string search, string rare)
    {
        return await _userPlantsRepository.GetUserPlantsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPlantAsync(string userId, Plants plant)
    {
        var oldPlantTask = _plantsService.SumPowerPlantsPercentAsync(userId);
        var oldUserPlantTask = _userPlantsRepository.SumPowerUserPlantsAsync(userId);

        await Task.WhenAll(oldPlantTask, oldUserPlantTask);

        Plants oldPlant = oldPlantTask.Result;
        Plants oldUserPlant = oldUserPlantTask.Result;

        var insertOrUpdateResult = await _userPlantsRepository.InsertOrUpdateUserPlantAsync(userId, plant);

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

        await _plantsGalleryService.InsertPlantGalleryAsync(userId, plant.Id);

        var newPlantTask = _plantsService.SumPowerPlantsPercentAsync(userId);
        var newUserPlantTask = _userPlantsRepository.SumPowerUserPlantsAsync(userId);

        await Task.WhenAll(newPlantTask, newUserPlantTask);

        PowerManager deltaPower = (PowerManager)newPlantTask.Result - (PowerManager)oldPlant;
        PowerManager deltaUserPower = (PowerManager)newUserPlantTask.Result - (PowerManager)oldUserPlant;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPlantsBatchAsync(string userId, List<Plants> plants)
    {
        var oldPlantTask = _plantsService.SumPowerPlantsPercentAsync(userId);
        var oldUserPlantTask = _userPlantsRepository.SumPowerUserPlantsAsync(userId);

        await Task.WhenAll(oldPlantTask, oldUserPlantTask);

        Plants oldPlant = oldPlantTask.Result;
        Plants oldUserPlant = oldUserPlantTask.Result;

        var insertOrUpdateResult = await _userPlantsRepository.InsertOrUpdateUserPlantsBatchAsync(userId, plants);

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
            await _plantsGalleryService.InsertBatchPlantsGalleryAsync(userId, newlyInsertedCards);

            var newPlantTask = _plantsService.SumPowerPlantsPercentAsync(userId);
            var newUserPlantTask = _userPlantsRepository.SumPowerUserPlantsAsync(userId);

            await Task.WhenAll(newPlantTask, newUserPlantTask);

            PowerManager deltaPower = (PowerManager)newPlantTask.Result - (PowerManager)oldPlant;
            PowerManager deltaUserPower = (PowerManager)newUserPlantTask.Result - (PowerManager)oldUserPlant;

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

    public async Task<bool> UpdateUserPlantLevelAsync(string userId, Plants plant)
    {
        Plants oldUserPlant = await _userPlantsRepository.SumPowerUserPlantsAsync(userId);

        var updateResult = await _userPlantsRepository.UpdateUserPlantLevelAsync(userId, plant);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Plants newUserPlant = await _userPlantsRepository.SumPowerUserPlantsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserPlant - (PowerManager)oldUserPlant;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserPlantStarAsync(string userId, Plants plant)
    {
        Plants oldUserPlant = await _userPlantsRepository.SumPowerUserPlantsAsync(userId);

        var updateResult = await _userPlantsRepository.UpdateUserPlantStarAsync(userId, plant);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _plantsGalleryService.UpdateTempStarPlantGalleryAsync(userId, plant.Id, plant.Star);

        Plants newUserPlant = await _userPlantsRepository.SumPowerUserPlantsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserPlant - (PowerManager)oldUserPlant;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Plants> GetUserPlantByIdAsync(string userId, string Id)
    {
        var result = await _userPlantsRepository.GetUserPlantByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Plants> SumPowerUserPlantsAsync(string userId)
    {
        return await _userPlantsRepository.SumPowerUserPlantsAsync(userId);
    }
}
