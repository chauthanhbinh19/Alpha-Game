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
        List<Buildings> list = await _userBuildingsRepository.GetUserBuildingsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBuildingsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userBuildingsRepository.GetUserBuildingsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingAsync(string userId, Buildings building)
    {
        Buildings oldBuilding = await _buildingsService.SumPowerBuildingsPercentAsync(userId);
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

        Buildings newBuilding = await _buildingsService.SumPowerBuildingsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newBuilding - (PowerManager)oldBuilding;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingsBatchAsync(string userId, List<Buildings> buildinges)
    {
        Buildings oldBuilding = await _buildingsService.SumPowerBuildingsPercentAsync(userId);
        var repositoryResult = await _userBuildingsRepository.InsertOrUpdateUserBuildingsBatchAsync(userId, buildinges);

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
            await _buildingsGalleryService.InsertBatchBuildingsGalleryAsync(userId, newlyInsertedCards);
        }

        Buildings newBuilding = await _buildingsService.SumPowerBuildingsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newBuilding - (PowerManager)oldBuilding;

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

    public async Task<bool> UpdateUserBuildingLevelAsync(string userId, Buildings building)
    {
        var updateResult = await _userBuildingsRepository.UpdateUserBuildingLevelAsync(userId, building);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserBuildingStarAsync(string userId, Buildings building)
    {
        var updateResult = await _userBuildingsRepository.UpdateUserBuildingStarAsync(userId, building);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _buildingsGalleryService.UpdateTempStarBuildingGalleryAsync(userId, building.Id, building.Star);

        return true;
    }

    public async Task<Buildings> GetUserBuildingByIdAsync(string userId, string Id)
    {
        var result = await _userBuildingsRepository.GetUserBuildingByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Buildings> SumPowerUserBuildingsAsync(string userId)
    {
        return await _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);
    }
}
