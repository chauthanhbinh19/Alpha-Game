using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFashionsService : IUserFashionsService
{
    private readonly IUserFashionsRepository _userFashionsRepository;
    private readonly IFashionsGalleryService _fashionsGalleryService;
    private readonly IFashionsService _fashionsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserFashionsService(
        IUserFashionsRepository userFashionsRepository,
        IFashionsGalleryService fashionsGalleryService,
        IFashionsService fashionsService,
        IPowerManagerService powerManagerService)
    {
        _userFashionsRepository = userFashionsRepository;
        _fashionsGalleryService = fashionsGalleryService;
        _fashionsService = fashionsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserFashionsService Create() => ServiceContainer.GetService<IUserFashionsService>();

    public async Task<List<Fashions>> GetUserFashionsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _userFashionsRepository.GetUserFashionsAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserFashionsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userFashionsRepository.GetUserFashionsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionAsync(string userId, Fashions fashion)
    {
        var oldFashionTask = _fashionsService.SumPowerFashionsPercentAsync(userId);
        var oldUserFashionTask = _userFashionsRepository.SumPowerUserFashionsAsync(userId);

        await Task.WhenAll(oldFashionTask, oldUserFashionTask);

        Fashions oldFashion = oldFashionTask.Result;
        Fashions oldUserFashion = oldUserFashionTask.Result;

        var insertOrUpdateResult = await _userFashionsRepository.InsertOrUpdateUserFashionAsync(userId, fashion);

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

        await _fashionsGalleryService.InsertFashionGalleryAsync(userId, fashion.Id);

        var newFashionTask = _fashionsService.SumPowerFashionsPercentAsync(userId);
        var newUserFashionTask = _userFashionsRepository.SumPowerUserFashionsAsync(userId);

        await Task.WhenAll(newFashionTask, newUserFashionTask);

        PowerManager deltaPower = (PowerManager)newFashionTask.Result - (PowerManager)oldFashion;
        PowerManager deltaUserPower = (PowerManager)newUserFashionTask.Result - (PowerManager)oldUserFashion;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionsBatchAsync(string userId, List<Fashions> fashions)
    {
        var oldFashionTask = _fashionsService.SumPowerFashionsPercentAsync(userId);
        var oldUserFashionTask = _userFashionsRepository.SumPowerUserFashionsAsync(userId);

        await Task.WhenAll(oldFashionTask, oldUserFashionTask);

        Fashions oldFashion = oldFashionTask.Result;
        Fashions oldUserFashion = oldUserFashionTask.Result;

        var insertOrUpdateResult = await _userFashionsRepository.InsertOrUpdateUserFashionsBatchAsync(userId, fashions);

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
            await _fashionsGalleryService.InsertBatchFashionsGalleryAsync(userId, newlyInsertedCards);

            var newFashionTask = _fashionsService.SumPowerFashionsPercentAsync(userId);
            var newUserFashionTask = _userFashionsRepository.SumPowerUserFashionsAsync(userId);

            await Task.WhenAll(newFashionTask, newUserFashionTask);

            PowerManager deltaPower = (PowerManager)newFashionTask.Result - (PowerManager)oldFashion;
            PowerManager deltaUserPower = (PowerManager)newUserFashionTask.Result - (PowerManager)oldUserFashion;

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

    public async Task<bool> UpdateUserFashionLevelAsync(string userId, Fashions fashion)
    {
        Fashions oldUserFashion = await _userFashionsRepository.SumPowerUserFashionsAsync(userId);

        var updateResult = await _userFashionsRepository.UpdateUserFashionLevelAsync(userId, fashion);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Fashions newUserFashion = await _userFashionsRepository.SumPowerUserFashionsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserFashion - (PowerManager)oldUserFashion;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserFashionStarAsync(string userId, Fashions fashion)
    {
        Fashions oldUserFashion = await _userFashionsRepository.SumPowerUserFashionsAsync(userId);

        var updateResult = await _userFashionsRepository.UpdateUserFashionStarAsync(userId, fashion);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _fashionsGalleryService.UpdateTempStarFashionGalleryAsync(userId, fashion.Id, fashion.Star);

        Fashions newUserFashion = await _userFashionsRepository.SumPowerUserFashionsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserFashion - (PowerManager)oldUserFashion;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Fashions> GetUserFashionByIdAsync(string userId, string Id)
    {
        var result = await _userFashionsRepository.GetUserFashionByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Fashions> SumPowerUserFashionsAsync(string userId)
    {
        return await _userFashionsRepository.SumPowerUserFashionsAsync(userId);
    }
}
