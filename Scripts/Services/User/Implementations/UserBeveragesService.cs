using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBeveragesService : IUserBeveragesService
{
    private readonly IUserBeveragesRepository _userBeveragesRepository;
    private readonly IBeveragesGalleryService _beveragesGalleryService;
    private readonly IBeveragesService _beveragesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserBeveragesService(
        IUserBeveragesRepository userBeveragesRepository,
        IBeveragesGalleryService beveragesGalleryService,
        IBeveragesService beveragesService,
        IPowerManagerService powerManagerService)
    {
        _userBeveragesRepository = userBeveragesRepository;
        _beveragesGalleryService = beveragesGalleryService;
        _beveragesService = beveragesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserBeveragesService Create() => ServiceContainer.GetService<IUserBeveragesService>();

    public async Task<List<Beverages>> GetUserBeveragesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _userBeveragesRepository.GetUserBeveragesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBeveragesCountAsync(string userId, string search, string rare)
    {
        return await _userBeveragesRepository.GetUserBeveragesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBeverageAsync(string userId, Beverages beverage)
    {
        Beverages oldBeverage = await _beveragesService.SumPowerBeveragesPercentAsync(userId);
        var insertOrUpdateResult = await _userBeveragesRepository.InsertOrUpdateUserBeverageAsync(userId, beverage);

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

        await _beveragesGalleryService.InsertBeverageGalleryAsync(userId, beverage.Id);

        Beverages newBeverage = await _beveragesService.SumPowerBeveragesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newBeverage - (PowerManager)oldBeverage;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBeveragesBatchAsync(string userId, List<Beverages> beveragees)
    {
        Beverages oldBeverage = await _beveragesService.SumPowerBeveragesPercentAsync(userId);
        var repositoryResult = await _userBeveragesRepository.InsertOrUpdateUserBeveragesBatchAsync(userId, beveragees);

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
            await _beveragesGalleryService.InsertBatchBeveragesGalleryAsync(userId, newlyInsertedCards);
        }

        Beverages newBeverage = await _beveragesService.SumPowerBeveragesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newBeverage - (PowerManager)oldBeverage;

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

    public async Task<bool> UpdateUserBeverageLevelAsync(string userId, Beverages beverage)
    {
        var updateResult = await _userBeveragesRepository.UpdateUserBeverageLevelAsync(userId, beverage);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserBeverageStarAsync(string userId, Beverages beverage)
    {
        var updateResult = await _userBeveragesRepository.UpdateUserBeverageStarAsync(userId, beverage);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _beveragesGalleryService.UpdateTempStarBeverageGalleryAsync(userId, beverage.Id, beverage.Star);

        return true;
    }

    public async Task<Beverages> GetUserBeverageByIdAsync(string userId, string Id)
    {
        var result = await _userBeveragesRepository.GetUserBeverageByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Beverages> SumPowerUserBeveragesAsync(string userId)
    {
        return await _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);
    }
}
