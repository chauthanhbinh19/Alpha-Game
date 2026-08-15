using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFurnituresService : IUserFurnituresService
{
    private readonly IUserFurnituresRepository _userFurnituresRepository;
    private readonly IFurnituresGalleryService _furnituresGalleryService;
    private readonly IFurnituresService _furnituresService;
    private readonly IPowerManagerService _powerManagerService;

    public UserFurnituresService(
        IUserFurnituresRepository userFurnituresRepository,
        IFurnituresGalleryService furnituresGalleryService,
        IFurnituresService furnituresService,
        IPowerManagerService powerManagerService)
    {
        _userFurnituresRepository = userFurnituresRepository;
        _furnituresGalleryService = furnituresGalleryService;
        _furnituresService = furnituresService;
        _powerManagerService = powerManagerService;
    }

    public static IUserFurnituresService Create() => ServiceContainer.GetService<IUserFurnituresService>();

    public async Task<List<Furnitures>> GetUserFurnituresAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> result = await _userFurnituresRepository.GetUserFurnituresAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserFurnituresCountAsync(string userId, string search, string type, string rare)
    {
        return await _userFurnituresRepository.GetUserFurnituresCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFurnitureAsync(string userId, Furnitures furniture)
    {
        var oldFurnitureTask = _furnituresService.SumPowerFurnituresPercentAsync(userId);
        var oldUserFurnitureTask = _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);

        await Task.WhenAll(oldFurnitureTask, oldUserFurnitureTask);

        Furnitures oldFurniture = oldFurnitureTask.Result;
        Furnitures oldUserFurniture = oldUserFurnitureTask.Result;

        var insertOrUpdateResult = await _userFurnituresRepository.InsertOrUpdateUserFurnitureAsync(userId, furniture);

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

        await _furnituresGalleryService.InsertFurnitureGalleryAsync(userId, furniture.Id);

        var newFurnitureTask = _furnituresService.SumPowerFurnituresPercentAsync(userId);
        var newUserFurnitureTask = _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);

        await Task.WhenAll(newFurnitureTask, newUserFurnitureTask);

        PowerManager deltaPower = (PowerManager)newFurnitureTask.Result - (PowerManager)oldFurniture;
        PowerManager deltaUserPower = (PowerManager)newUserFurnitureTask.Result - (PowerManager)oldUserFurniture;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFurnituresBatchAsync(string userId, List<Furnitures> furnitures)
    {
        var oldFurnitureTask = _furnituresService.SumPowerFurnituresPercentAsync(userId);
        var oldUserFurnitureTask = _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);

        await Task.WhenAll(oldFurnitureTask, oldUserFurnitureTask);

        Furnitures oldFurniture = oldFurnitureTask.Result;
        Furnitures oldUserFurniture = oldUserFurnitureTask.Result;

        var insertOrUpdateResult = await _userFurnituresRepository.InsertOrUpdateUserFurnituresBatchAsync(userId, furnitures);

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
            await _furnituresGalleryService.InsertBatchFurnituresGalleryAsync(userId, newlyInsertedCards);

            var newFurnitureTask = _furnituresService.SumPowerFurnituresPercentAsync(userId);
            var newUserFurnitureTask = _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);

            await Task.WhenAll(newFurnitureTask, newUserFurnitureTask);

            PowerManager deltaPower = (PowerManager)newFurnitureTask.Result - (PowerManager)oldFurniture;
            PowerManager deltaUserPower = (PowerManager)newUserFurnitureTask.Result - (PowerManager)oldUserFurniture;

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

    public async Task<bool> UpdateUserFurnitureLevelAsync(string userId, Furnitures furniture)
    {
        Furnitures oldUserFurniture = await _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);

        var updateResult = await _userFurnituresRepository.UpdateUserFurnitureLevelAsync(userId, furniture);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Furnitures newUserFurniture = await _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserFurniture - (PowerManager)oldUserFurniture;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserFurnitureStarAsync(string userId, Furnitures furniture)
    {
        Furnitures oldUserFurniture = await _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);

        var updateResult = await _userFurnituresRepository.UpdateUserFurnitureStarAsync(userId, furniture);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _furnituresGalleryService.UpdateTempStarFurnitureGalleryAsync(userId, furniture.Id, furniture.Star);

        Furnitures newUserFurniture = await _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserFurniture - (PowerManager)oldUserFurniture;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Furnitures> GetUserFurnitureByIdAsync(string userId, string Id)
    {
        var result = await _userFurnituresRepository.GetUserFurnitureByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Furnitures> SumPowerUserFurnituresAsync(string userId)
    {
        return await _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);
    }
}
