using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFoodsService : IUserFoodsService
{
    private readonly IUserFoodsRepository _userFoodsRepository;
    private readonly IFoodsGalleryService _foodsGalleryService;
    private readonly IFoodsService _foodsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserFoodsService(
        IUserFoodsRepository userFoodsRepository,
        IFoodsGalleryService foodsGalleryService,
        IFoodsService foodsService,
        IPowerManagerService powerManagerService)
    {
        _userFoodsRepository = userFoodsRepository;
        _foodsGalleryService = foodsGalleryService;
        _foodsService = foodsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserFoodsService Create() => ServiceContainer.GetService<IUserFoodsService>();

    public async Task<List<Foods>> GetUserFoodsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Foods> result = await _userFoodsRepository.GetUserFoodsAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserFoodsCountAsync(string userId, string search, string rare)
    {
        return await _userFoodsRepository.GetUserFoodsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFoodAsync(string userId, Foods food)
    {
        var oldFoodTask = _foodsService.SumPowerFoodsPercentAsync(userId);
        var oldUserFoodTask = _userFoodsRepository.SumPowerUserFoodsAsync(userId);

        await Task.WhenAll(oldFoodTask, oldUserFoodTask);

        Foods oldFood = oldFoodTask.Result;
        Foods oldUserFood = oldUserFoodTask.Result;

        var insertOrUpdateResult = await _userFoodsRepository.InsertOrUpdateUserFoodAsync(userId, food);

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

        await _foodsGalleryService.InsertFoodGalleryAsync(userId, food.Id);

        var newFoodTask = _foodsService.SumPowerFoodsPercentAsync(userId);
        var newUserFoodTask = _userFoodsRepository.SumPowerUserFoodsAsync(userId);

        await Task.WhenAll(newFoodTask, newUserFoodTask);

        PowerManager deltaPower = (PowerManager)newFoodTask.Result - (PowerManager)oldFood;
        PowerManager deltaUserPower = (PowerManager)newUserFoodTask.Result - (PowerManager)oldUserFood;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFoodsBatchAsync(string userId, List<Foods> foods)
    {
        var oldFoodTask = _foodsService.SumPowerFoodsPercentAsync(userId);
        var oldUserFoodTask = _userFoodsRepository.SumPowerUserFoodsAsync(userId);

        await Task.WhenAll(oldFoodTask, oldUserFoodTask);

        Foods oldFood = oldFoodTask.Result;
        Foods oldUserFood = oldUserFoodTask.Result;

        var insertOrUpdateResult = await _userFoodsRepository.InsertOrUpdateUserFoodsBatchAsync(userId, foods);

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
            await _foodsGalleryService.InsertBatchFoodsGalleryAsync(userId, newlyInsertedCards);

            var newFoodTask = _foodsService.SumPowerFoodsPercentAsync(userId);
            var newUserFoodTask = _userFoodsRepository.SumPowerUserFoodsAsync(userId);

            await Task.WhenAll(newFoodTask, newUserFoodTask);

            PowerManager deltaPower = (PowerManager)newFoodTask.Result - (PowerManager)oldFood;
            PowerManager deltaUserPower = (PowerManager)newUserFoodTask.Result - (PowerManager)oldUserFood;

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

    public async Task<bool> UpdateUserFoodLevelAsync(string userId, Foods food)
    {
        Foods oldUserFood = await _userFoodsRepository.SumPowerUserFoodsAsync(userId);

        var updateResult = await _userFoodsRepository.UpdateUserFoodLevelAsync(userId, food);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Foods newUserFood = await _userFoodsRepository.SumPowerUserFoodsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserFood - (PowerManager)oldUserFood;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserFoodStarAsync(string userId, Foods food)
    {
        Foods oldUserFood = await _userFoodsRepository.SumPowerUserFoodsAsync(userId);

        var updateResult = await _userFoodsRepository.UpdateUserFoodStarAsync(userId, food);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _foodsGalleryService.UpdateTempStarFoodGalleryAsync(userId, food.Id, food.Star);

        Foods newUserFood = await _userFoodsRepository.SumPowerUserFoodsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserFood - (PowerManager)oldUserFood;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Foods> GetUserFoodByIdAsync(string userId, string Id)
    {
        var result = await _userFoodsRepository.GetUserFoodByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Foods> SumPowerUserFoodsAsync(string userId)
    {
        return await _userFoodsRepository.SumPowerUserFoodsAsync(userId);
    }
}
