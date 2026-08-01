using System.Collections.Generic;
using System.Threading.Tasks;

public class UserWeaponsService : IUserWeaponsService
{
    private readonly IUserWeaponsRepository _userWeaponsRepository;
    private readonly IWeaponsGalleryService _weaponsGalleryService;
    private readonly IWeaponsService _weaponsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserWeaponsService(
        IUserWeaponsRepository userWeaponsRepository,
        IWeaponsGalleryService weaponsGalleryService,
        IWeaponsService weaponsService,
        IPowerManagerService powerManagerService)
    {
        _userWeaponsRepository = userWeaponsRepository;
        _weaponsGalleryService = weaponsGalleryService;
        _weaponsService = weaponsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserWeaponsService Create() => ServiceContainer.GetService<IUserWeaponsService>();

    public async Task<List<Weapons>> GetUserWeaponsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _userWeaponsRepository.GetUserWeaponsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserWeaponsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userWeaponsRepository.GetUserWeaponsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserWeaponAsync(string userId, Weapons weapon)
    {
        var oldWeaponTask = _weaponsService.SumPowerWeaponsPercentAsync(userId);
        var oldUserWeaponTask = _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);

        await Task.WhenAll(oldWeaponTask, oldUserWeaponTask);

        Weapons oldWeapon = oldWeaponTask.Result;
        Weapons oldUserWeapon = oldUserWeaponTask.Result;

        var insertOrUpdateResult = await _userWeaponsRepository.InsertOrUpdateUserWeaponAsync(userId, weapon);

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

        await _weaponsGalleryService.InsertWeaponGalleryAsync(userId, weapon.Id);

        var newWeaponTask = _weaponsService.SumPowerWeaponsPercentAsync(userId);
        var newUserWeaponTask = _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);

        await Task.WhenAll(newWeaponTask, newUserWeaponTask);

        PowerManager deltaPower = (PowerManager)newWeaponTask.Result - (PowerManager)oldWeapon;
        PowerManager deltaUserPower = (PowerManager)newUserWeaponTask.Result - (PowerManager)oldUserWeapon;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserWeaponsBatchAsync(string userId, List<Weapons> weapons)
    {
        var oldWeaponTask = _weaponsService.SumPowerWeaponsPercentAsync(userId);
        var oldUserWeaponTask = _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);

        await Task.WhenAll(oldWeaponTask, oldUserWeaponTask);

        Weapons oldWeapon = oldWeaponTask.Result;
        Weapons oldUserWeapon = oldUserWeaponTask.Result;

        var insertOrUpdateResult = await _userWeaponsRepository.InsertOrUpdateUserWeaponsBatchAsync(userId, weapons);

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
            await _weaponsGalleryService.InsertBatchWeaponsGalleryAsync(userId, newlyInsertedCards);

            var newWeaponTask = _weaponsService.SumPowerWeaponsPercentAsync(userId);
            var newUserWeaponTask = _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);

            await Task.WhenAll(newWeaponTask, newUserWeaponTask);

            PowerManager deltaPower = (PowerManager)newWeaponTask.Result - (PowerManager)oldWeapon;
            PowerManager deltaUserPower = (PowerManager)newUserWeaponTask.Result - (PowerManager)oldUserWeapon;

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

    public async Task<bool> UpdateUserWeaponLevelAsync(string userId, Weapons weapon)
    {
        Weapons oldUserWeapon = await _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);

        var updateResult = await _userWeaponsRepository.UpdateUserWeaponLevelAsync(userId, weapon);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Weapons newUserWeapon = await _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserWeapon - (PowerManager)oldUserWeapon;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserWeaponStarAsync(string userId, Weapons weapon)
    {
        Weapons oldUserWeapon = await _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);

        var updateResult = await _userWeaponsRepository.UpdateUserWeaponStarAsync(userId, weapon);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _weaponsGalleryService.UpdateTempStarWeaponGalleryAsync(userId, weapon.Id, weapon.Star);

        Weapons newUserWeapon = await _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserWeapon - (PowerManager)oldUserWeapon;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Weapons> GetUserWeaponByIdAsync(string userId, string Id)
    {
        var result = await _userWeaponsRepository.GetUserWeaponByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Weapons> SumPowerUserWeaponsAsync(string userId)
    {
        return await _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);
    }
}
