using System.Collections.Generic;
using System.Threading.Tasks;

public class UserWeaponsService : IUserWeaponsService
{
    private readonly IUserWeaponsRepository _userWeaponsRepository;
    private readonly IWeaponsGalleryService _weaponsGalleryService;
    private readonly IWeaponsService _weaponsService;

    public UserWeaponsService(
        IUserWeaponsRepository userWeaponsRepository,
        IWeaponsGalleryService weaponsGalleryService,
        IWeaponsService weaponsService)
    {
        _userWeaponsRepository = userWeaponsRepository;
        _weaponsGalleryService = weaponsGalleryService;
        _weaponsService = weaponsService;
    }

    public static IUserWeaponsService Create() => ServiceContainer.GetService<IUserWeaponsService>();

    public async Task<List<Weapons>> GetUserWeaponsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Weapons> result = await _userWeaponsRepository.GetUserWeaponsAsync(userId, search, type, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserWeaponsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userWeaponsRepository.GetUserWeaponsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserWeaponAsync(string userId, Weapons weapon)
    {
        var checkWeaponResult = await _weaponsService.IsWeaponDeletedOrInactiveAsync(weapon.Id);
        if (checkWeaponResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userWeaponsRepository.InsertOrUpdateUserWeaponAsync(userId, weapon);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = true,
                OperationType = DatabaseOperationType.Updated,
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        await _weaponsGalleryService.InsertWeaponGalleryAsync(userId, weapon.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserWeaponsBatchAsync(string userId, List<Weapons> weapons)
    {
        var insertOrUpdateResult = await _userWeaponsRepository.InsertOrUpdateUserWeaponsBatchAsync(userId, weapons);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _weaponsGalleryService.InsertBatchWeaponsGalleryAsync(userId, newlyInsertedCards);
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
                IsChangePower = false,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserWeaponLevelAsync(string userId, Weapons weapon, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (weapon == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Weapon is required.");
        }

        var checkResult = await _weaponsService.IsWeaponDeletedOrInactiveAsync(weapon.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userWeaponsRepository.UpdateUserWeaponLevelAsync(
            userId,
            weapon,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserWeaponStarAsync(string userId, Weapons weapon)
    {
        var checkWeaponResult = await _weaponsService.IsWeaponDeletedOrInactiveAsync(weapon.Id);
        if (checkWeaponResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userWeaponsRepository.UpdateUserWeaponStarAsync(userId, weapon);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        await _weaponsGalleryService.UpdateTempStarWeaponGalleryAsync(userId, weapon.Id, weapon.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserWeaponQuantityAsync(string userId, Weapons weapon, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userWeaponsRepository.UpdateUserWeaponQuantityAsync(userId, weapon, totalDeductions);

        return result;
    }

    public async Task<Weapons> GetUserWeaponByIdAsync(string userId, string Id)
    {
        var result = await _userWeaponsRepository.GetUserWeaponByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Weapons> SumPowerUserWeaponsAsync(string userId)
    {
        return await _userWeaponsRepository.SumPowerUserWeaponsAsync(userId);
    }
}
