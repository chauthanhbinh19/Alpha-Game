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
        List<Foods> list = await _userFoodsRepository.GetUserFoodsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFoodsCountAsync(string userId, string search, string rare)
    {
        return await _userFoodsRepository.GetUserFoodsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFoodAsync(string userId, Foods food)
    {
        Foods oldFood = await _foodsService.SumPowerFoodsPercentAsync(userId);
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

        Foods newFood = await _foodsService.SumPowerFoodsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newFood - (PowerManager)oldFood;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFoodsBatchAsync(string userId, List<Foods> foodes)
    {
        Foods oldFood = await _foodsService.SumPowerFoodsPercentAsync(userId);
        var repositoryResult = await _userFoodsRepository.InsertOrUpdateUserFoodsBatchAsync(userId, foodes);

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
            await _foodsGalleryService.InsertBatchFoodsGalleryAsync(userId, newlyInsertedCards);
        }

        Foods newFood = await _foodsService.SumPowerFoodsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newFood - (PowerManager)oldFood;

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

    public async Task<bool> UpdateUserFoodLevelAsync(string userId, Foods food)
    {
        var updateResult = await _userFoodsRepository.UpdateUserFoodLevelAsync(userId, food);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserFoodStarAsync(string userId, Foods food)
    {
        var updateResult = await _userFoodsRepository.UpdateUserFoodStarAsync(userId, food);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _foodsGalleryService.UpdateTempStarFoodGalleryAsync(userId, food.Id, food.Star);

        return true;
    }

    public async Task<Foods> GetUserFoodByIdAsync(string userId, string Id)
    {
        var result = await _userFoodsRepository.GetUserFoodByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Foods> SumPowerUserFoodsAsync(string userId)
    {
        return await _userFoodsRepository.SumPowerUserFoodsAsync(userId);
    }
}
