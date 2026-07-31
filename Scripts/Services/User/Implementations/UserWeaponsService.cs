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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserWeaponAsync(string userId, Weapons cardLife)
    {
        Weapons oldWeapon = await _weaponsService.SumPowerWeaponsPercentAsync(userId);
        var insertOrUpdateResult = await _userWeaponsRepository.InsertOrUpdateUserWeaponAsync(userId, cardLife);

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

        await _weaponsGalleryService.InsertWeaponGalleryAsync(userId, cardLife.Id);

        Weapons newWeapon = await _weaponsService.SumPowerWeaponsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newWeapon - (PowerManager)oldWeapon;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserWeaponsBatchAsync(string userId, List<Weapons> cardLifees)
    {
        Weapons oldWeapon = await _weaponsService.SumPowerWeaponsPercentAsync(userId);
        var repositoryResult = await _userWeaponsRepository.InsertOrUpdateUserWeaponsBatchAsync(userId, cardLifees);

        // 1. Kiểm tra Null hoặc nếu Repository trả về không thành công
        if (repositoryResult?.Data == null || !repositoryResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        // 2. Gộp logic xử lý Gallery nếu có thẻ mới được Insert (dùng cho cả Inserted và Mixed)
        var newlyInsertedCards = repositoryResult.Data.InsertedItems;
        if (newlyInsertedCards != null && newlyInsertedCards.Count > 0)
        {
            await _weaponsGalleryService.InsertBatchWeaponsGalleryAsync(userId, newlyInsertedCards);
        }

        Weapons newWeapon = await _weaponsService.SumPowerWeaponsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newWeapon - (PowerManager)oldWeapon;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        // 3. Mapping kết quả OperationType trả về gọn gàng
        return repositoryResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserWeaponLevelAsync(string userId, Weapons cardLife)
    {
        var updateResult = await _userWeaponsRepository.UpdateUserWeaponLevelAsync(userId, cardLife);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserWeaponStarAsync(string userId, Weapons cardLife)
    {
        var updateResult = await _userWeaponsRepository.UpdateUserWeaponStarAsync(userId, cardLife);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _weaponsGalleryService.UpdateTempStarWeaponGalleryAsync(userId, cardLife.Id, cardLife.Star);

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
