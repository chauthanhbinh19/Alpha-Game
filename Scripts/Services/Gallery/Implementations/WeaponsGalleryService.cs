using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class WeaponsGalleryService : IWeaponsGalleryService
{
    private readonly IWeaponsGalleryRepository _weaponsGalleryRepository;
    private readonly IWeaponsService _weaponsService;
    private readonly IPowerManagerService _powerManagerService;

    public WeaponsGalleryService(
        IWeaponsGalleryRepository weaponsGalleryRepository,
        IWeaponsService weaponsService,
        IPowerManagerService powerManagerService)
    {
        _weaponsGalleryRepository = weaponsGalleryRepository;
        _weaponsService = weaponsService;
        _powerManagerService = powerManagerService;
    }

    public static IWeaponsGalleryService Create() => ServiceContainer.GetService<IWeaponsGalleryService>();

    public async Task<List<Weapons>> GetWeaponsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _weaponsGalleryRepository.GetWeaponsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsCountAsync(string search, string type, string rare)
    {
        return await _weaponsGalleryRepository.GetWeaponsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertWeaponGalleryAsync(string userId, string Id)
    {
        var checkResult = await _weaponsService.IsWeaponDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _weaponsGalleryRepository.InsertWeaponGalleryAsync(userId, Id, await _weaponsService.GetWeaponByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusWeaponGalleryAsync(string userId, string weaponId)
    {
        var checkResult = await _weaponsService.IsWeaponDeletedOrInactiveAsync(weaponId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _weaponsGalleryRepository.UpdateStatusWeaponGalleryAsync(userId, weaponId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Weapons weaponGallery = await GetWeaponCollectionByIdAsync(userId, weaponId) ?? new Weapons();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)weaponGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusWeaponsGalleryAsync(string userId)
    {
        Weapons oldWeapon = await SumPowerWeaponsGalleryAsync(userId);

        var updateResult = await _weaponsGalleryRepository.UpdateBatchStatusWeaponsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Weapons newWeapon = await SumPowerWeaponsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newWeapon - (PowerManager)oldWeapon;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<Weapons> SumPowerWeaponsGalleryAsync(string userId)
    {
        return await _weaponsGalleryRepository.SumPowerWeaponsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarWeaponGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _weaponsService.IsWeaponDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _weaponsGalleryRepository.UpdateTempStarWeaponGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarWeaponGalleryAsync(string userId, string weaponId)
    {
        var checkResult = await _weaponsService.IsWeaponDeletedOrInactiveAsync(weaponId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Weapons oldWeapon = await GetWeaponCollectionByIdAsync(userId, weaponId) ?? new Weapons();

        var updateResult = await _weaponsGalleryRepository.UpdateCurrentStarWeaponGalleryAsync(userId, weaponId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Weapons newWeapon = await GetWeaponCollectionByIdAsync(userId, weaponId) ?? new Weapons();
        PowerManager deltaPower = (PowerManager)newWeapon - (PowerManager)oldWeapon;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarWeaponsGalleryAsync(string userId)
    {
        Weapons oldWeapon = await SumPowerWeaponsGalleryAsync(userId);

        var updateResult = await _weaponsGalleryRepository.UpdateBatchCurrentStarWeaponsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Weapons newWeapon = await SumPowerWeaponsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newWeapon - (PowerManager)oldWeapon;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchWeaponsGalleryAsync(string userId, List<Weapons> weapons)
    {
        var insertResult = await _weaponsGalleryRepository.InsertBatchWeaponsGalleryAsync(userId, weapons);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<Weapons> GetWeaponCollectionByIdAsync(string userId, string weaponId)
    {
        var result = await _weaponsGalleryRepository.GetWeaponCollectionByIdAsync(userId, weaponId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateWeaponGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _weaponsService.IsWeaponDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        await _weaponsGalleryRepository.UpdateWeaponGalleryPowerAsync(userId, Id, await _service.GetWeaponByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
