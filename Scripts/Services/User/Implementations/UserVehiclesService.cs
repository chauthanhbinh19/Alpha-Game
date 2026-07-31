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
        List<Vehicles> list = await _userVehiclesRepository.GetUserVehiclesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserVehiclesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userVehiclesRepository.GetUserVehiclesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserVehicleAsync(string userId, Vehicles vehicle)
    {
        Vehicles oldVehicle = await _vehiclesService.SumPowerVehiclesPercentAsync(userId);
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

        Vehicles newVehicle = await _vehiclesService.SumPowerVehiclesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newVehicle - (PowerManager)oldVehicle;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserVehiclesBatchAsync(string userId, List<Vehicles> vehiclees)
    {
        Vehicles oldVehicle = await _vehiclesService.SumPowerVehiclesPercentAsync(userId);
        var repositoryResult = await _userVehiclesRepository.InsertOrUpdateUserVehiclesBatchAsync(userId, vehiclees);

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
            await _vehiclesGalleryService.InsertBatchVehiclesGalleryAsync(userId, newlyInsertedCards);
        }

        Vehicles newVehicle = await _vehiclesService.SumPowerVehiclesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newVehicle - (PowerManager)oldVehicle;

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

    public async Task<bool> UpdateUserVehicleLevelAsync(string userId, Vehicles vehicle)
    {
        var updateResult = await _userVehiclesRepository.UpdateUserVehicleLevelAsync(userId, vehicle);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserVehicleStarAsync(string userId, Vehicles vehicle)
    {
        var updateResult = await _userVehiclesRepository.UpdateUserVehicleStarAsync(userId, vehicle);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _vehiclesGalleryService.UpdateTempStarVehicleGalleryAsync(userId, vehicle.Id, vehicle.Star);

        return true;
    }

    public async Task<Vehicles> GetUserVehicleByIdAsync(string userId, string Id)
    {
        var result = await _userVehiclesRepository.GetUserVehicleByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Vehicles> SumPowerUserVehiclesAsync(string userId)
    {
        return await _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);
    }
}
