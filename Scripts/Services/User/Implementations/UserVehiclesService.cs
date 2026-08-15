using System.Collections.Generic;
using System.Threading.Tasks;

public class UserVehiclesService : IUserVehiclesService
{
    private readonly IUserVehiclesRepository _userVehiclesRepository;
    private readonly IVehiclesGalleryService _vehiclesGalleryService;
    private readonly IVehiclesService _vehiclesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserVehiclesService(
        IUserVehiclesRepository userVehiclesRepository,
        IVehiclesGalleryService vehiclesGalleryService,
        IVehiclesService vehiclesService,
        IPowerManagerService powerManagerService)
    {
        _userVehiclesRepository = userVehiclesRepository;
        _vehiclesGalleryService = vehiclesGalleryService;
        _vehiclesService = vehiclesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserVehiclesService Create() => ServiceContainer.GetService<IUserVehiclesService>();

    public async Task<List<Vehicles>> GetUserVehiclesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> result = await _userVehiclesRepository.GetUserVehiclesAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserVehiclesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userVehiclesRepository.GetUserVehiclesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserVehicleAsync(string userId, Vehicles vehicle)
    {
        var oldVehicleTask = _vehiclesService.SumPowerVehiclesPercentAsync(userId);
        var oldUserVehicleTask = _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);

        await Task.WhenAll(oldVehicleTask, oldUserVehicleTask);

        Vehicles oldVehicle = oldVehicleTask.Result;
        Vehicles oldUserVehicle = oldUserVehicleTask.Result;

        var insertOrUpdateResult = await _userVehiclesRepository.InsertOrUpdateUserVehicleAsync(userId, vehicle);

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

        await _vehiclesGalleryService.InsertVehicleGalleryAsync(userId, vehicle.Id);

        var newVehicleTask = _vehiclesService.SumPowerVehiclesPercentAsync(userId);
        var newUserVehicleTask = _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);

        await Task.WhenAll(newVehicleTask, newUserVehicleTask);

        PowerManager deltaPower = (PowerManager)newVehicleTask.Result - (PowerManager)oldVehicle;
        PowerManager deltaUserPower = (PowerManager)newUserVehicleTask.Result - (PowerManager)oldUserVehicle;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserVehiclesBatchAsync(string userId, List<Vehicles> vehicles)
    {
        var oldVehicleTask = _vehiclesService.SumPowerVehiclesPercentAsync(userId);
        var oldUserVehicleTask = _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);

        await Task.WhenAll(oldVehicleTask, oldUserVehicleTask);

        Vehicles oldVehicle = oldVehicleTask.Result;
        Vehicles oldUserVehicle = oldUserVehicleTask.Result;

        var insertOrUpdateResult = await _userVehiclesRepository.InsertOrUpdateUserVehiclesBatchAsync(userId, vehicles);

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
            await _vehiclesGalleryService.InsertBatchVehiclesGalleryAsync(userId, newlyInsertedCards);

            var newVehicleTask = _vehiclesService.SumPowerVehiclesPercentAsync(userId);
            var newUserVehicleTask = _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);

            await Task.WhenAll(newVehicleTask, newUserVehicleTask);

            PowerManager deltaPower = (PowerManager)newVehicleTask.Result - (PowerManager)oldVehicle;
            PowerManager deltaUserPower = (PowerManager)newUserVehicleTask.Result - (PowerManager)oldUserVehicle;

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

    public async Task<bool> UpdateUserVehicleLevelAsync(string userId, Vehicles vehicle)
    {
        Vehicles oldUserVehicle = await _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);

        var updateResult = await _userVehiclesRepository.UpdateUserVehicleLevelAsync(userId, vehicle);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Vehicles newUserVehicle = await _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserVehicle - (PowerManager)oldUserVehicle;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserVehicleStarAsync(string userId, Vehicles vehicle)
    {
        Vehicles oldUserVehicle = await _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);

        var updateResult = await _userVehiclesRepository.UpdateUserVehicleStarAsync(userId, vehicle);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _vehiclesGalleryService.UpdateTempStarVehicleGalleryAsync(userId, vehicle.Id, vehicle.Star);

        Vehicles newUserVehicle = await _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserVehicle - (PowerManager)oldUserVehicle;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Vehicles> GetUserVehicleByIdAsync(string userId, string Id)
    {
        var result = await _userVehiclesRepository.GetUserVehicleByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Vehicles> SumPowerUserVehiclesAsync(string userId)
    {
        return await _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);
    }
}
