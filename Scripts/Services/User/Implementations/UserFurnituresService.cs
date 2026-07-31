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
        List<Furnitures> list = await _userFurnituresRepository.GetUserFurnituresAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFurnituresCountAsync(string userId, string search, string type, string rare)
    {
        return await _userFurnituresRepository.GetUserFurnituresCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFurnitureAsync(string userId, Furnitures furniture)
    {
        Furnitures oldFurniture = await _furnituresService.SumPowerFurnituresPercentAsync(userId);
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

        Furnitures newFurniture = await _furnituresService.SumPowerFurnituresPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newFurniture - (PowerManager)oldFurniture;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFurnituresBatchAsync(string userId, List<Furnitures> furniturees)
    {
        Furnitures oldFurniture = await _furnituresService.SumPowerFurnituresPercentAsync(userId);
        var repositoryResult = await _userFurnituresRepository.InsertOrUpdateUserFurnituresBatchAsync(userId, furniturees);

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
            await _furnituresGalleryService.InsertBatchFurnituresGalleryAsync(userId, newlyInsertedCards);
        }

        Furnitures newFurniture = await _furnituresService.SumPowerFurnituresPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newFurniture - (PowerManager)oldFurniture;

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

    public async Task<bool> UpdateUserFurnitureLevelAsync(string userId, Furnitures furniture)
    {
        var updateResult = await _userFurnituresRepository.UpdateUserFurnitureLevelAsync(userId, furniture);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserFurnitureStarAsync(string userId, Furnitures furniture)
    {
        var updateResult = await _userFurnituresRepository.UpdateUserFurnitureStarAsync(userId, furniture);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _furnituresGalleryService.UpdateTempStarFurnitureGalleryAsync(userId, furniture.Id, furniture.Star);

        return true;
    }

    public async Task<Furnitures> GetUserFurnitureByIdAsync(string userId, string Id)
    {
        var result = await _userFurnituresRepository.GetUserFurnitureByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Furnitures> SumPowerUserFurnituresAsync(string userId)
    {
        return await _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);
    }
}
