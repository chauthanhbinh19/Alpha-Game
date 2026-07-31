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
        List<Plants> list = await _userPlantsRepository.GetUserPlantsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPlantsCountAsync(string userId, string search, string rare)
    {
        return await _userPlantsRepository.GetUserPlantsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPlantAsync(string userId, Plants plant)
    {
        Plants oldPlant = await _plantsService.SumPowerPlantsPercentAsync(userId);
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

        Plants newPlant = await _plantsService.SumPowerPlantsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newPlant - (PowerManager)oldPlant;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPlantsBatchAsync(string userId, List<Plants> plantes)
    {
        Plants oldPlant = await _plantsService.SumPowerPlantsPercentAsync(userId);
        var repositoryResult = await _userPlantsRepository.InsertOrUpdateUserPlantsBatchAsync(userId, plantes);

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
            await _plantsGalleryService.InsertBatchPlantsGalleryAsync(userId, newlyInsertedCards);
        }

        Plants newPlant = await _plantsService.SumPowerPlantsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newPlant - (PowerManager)oldPlant;

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

    public async Task<bool> UpdateUserPlantLevelAsync(string userId, Plants plant)
    {
        var updateResult = await _userPlantsRepository.UpdateUserPlantLevelAsync(userId, plant);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserPlantStarAsync(string userId, Plants plant)
    {
        var updateResult = await _userPlantsRepository.UpdateUserPlantStarAsync(userId, plant);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _plantsGalleryService.UpdateTempStarPlantGalleryAsync(userId, plant.Id, plant.Star);

        return true;
    }

    public async Task<Plants> GetUserPlantByIdAsync(string userId, string Id)
    {
        var result = await _userPlantsRepository.GetUserPlantByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Plants> SumPowerUserPlantsAsync(string userId)
    {
        return await _userPlantsRepository.SumPowerUserPlantsAsync(userId);
    }
}
