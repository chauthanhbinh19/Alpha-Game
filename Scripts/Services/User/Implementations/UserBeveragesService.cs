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

        foreach (var item in list)
        {
            item.BaseStats = new BaseStats(item);
        }

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
        var oldBeverageTask = _beveragesService.SumPowerBeveragesPercentAsync(userId);
        var oldUserBeverageTask = _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);

        await Task.WhenAll(oldBeverageTask, oldUserBeverageTask);

        Beverages oldBeverage = oldBeverageTask.Result;
        Beverages oldUserBeverage = oldUserBeverageTask.Result;

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

        var newBeverageTask = _beveragesService.SumPowerBeveragesPercentAsync(userId);
        var newUserBeverageTask = _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);

        await Task.WhenAll(newBeverageTask, newUserBeverageTask);

        PowerManager deltaPower = (PowerManager)newBeverageTask.Result - (PowerManager)oldBeverage;
        PowerManager deltaUserPower = (PowerManager)newUserBeverageTask.Result - (PowerManager)oldUserBeverage;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBeveragesBatchAsync(string userId, List<Beverages> beverages)
    {
        var oldBeverageTask = _beveragesService.SumPowerBeveragesPercentAsync(userId);
        var oldUserBeverageTask = _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);

        await Task.WhenAll(oldBeverageTask, oldUserBeverageTask);

        Beverages oldBeverage = oldBeverageTask.Result;
        Beverages oldUserBeverage = oldUserBeverageTask.Result;

        var insertOrUpdateResult = await _userBeveragesRepository.InsertOrUpdateUserBeveragesBatchAsync(userId, beverages);

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
            await _beveragesGalleryService.InsertBatchBeveragesGalleryAsync(userId, newlyInsertedCards);

            var newBeverageTask = _beveragesService.SumPowerBeveragesPercentAsync(userId);
            var newUserBeverageTask = _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);

            await Task.WhenAll(newBeverageTask, newUserBeverageTask);

            PowerManager deltaPower = (PowerManager)newBeverageTask.Result - (PowerManager)oldBeverage;
            PowerManager deltaUserPower = (PowerManager)newUserBeverageTask.Result - (PowerManager)oldUserBeverage;

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

    public async Task<bool> UpdateUserBeverageLevelAsync(string userId, Beverages beverage)
    {
        Beverages oldUserBeverage = await _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);

        var updateResult = await _userBeveragesRepository.UpdateUserBeverageLevelAsync(userId, beverage);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Beverages newUserBeverage = await _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserBeverage - (PowerManager)oldUserBeverage;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserBeverageStarAsync(string userId, Beverages beverage)
    {
        Beverages oldUserBeverage = await _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);

        var updateResult = await _userBeveragesRepository.UpdateUserBeverageStarAsync(userId, beverage);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _beveragesGalleryService.UpdateTempStarBeverageGalleryAsync(userId, beverage.Id, beverage.Star);

        Beverages newUserBeverage = await _userBeveragesRepository.SumPowerUserBeveragesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserBeverage - (PowerManager)oldUserBeverage;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Beverages> GetUserBeverageByIdAsync(string userId, string Id)
    {
        var result = await _userBeveragesRepository.GetUserBeverageByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

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
