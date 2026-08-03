using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTitlesService : IUserTitlesService
{
    private readonly IUserTitlesRepository _userTitlesRepository;
    private readonly ITitlesGalleryService _titlesGalleryService;
    private readonly ITitlesService _titlesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserTitlesService(
        IUserTitlesRepository userTitlesRepository,
        ITitlesGalleryService titlesGalleryService,
        ITitlesService titlesService,
        IPowerManagerService powerManagerService)
    {
        _userTitlesRepository = userTitlesRepository;
        _titlesGalleryService = titlesGalleryService;
        _titlesService = titlesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserTitlesService Create() => ServiceContainer.GetService<IUserTitlesService>();

    public async Task<List<Titles>> GetUserTitlesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Titles> list = await _userTitlesRepository.GetUserTitlesAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserTitlesCountAsync(string userId, string search, string rare)
    {
        return await _userTitlesRepository.GetUserTitlesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTitleAsync(string userId, Titles title)
    {
        var oldTitleTask = _titlesService.SumPowerTitlesPercentAsync(userId);
        var oldUserTitleTask = _userTitlesRepository.SumPowerUserTitlesAsync(userId);

        await Task.WhenAll(oldTitleTask, oldUserTitleTask);

        Titles oldTitle = oldTitleTask.Result;
        Titles oldUserTitle = oldUserTitleTask.Result;

        var insertOrUpdateResult = await _userTitlesRepository.InsertOrUpdateUserTitleAsync(userId, title);

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

        await _titlesGalleryService.InsertTitleGalleryAsync(userId, title.Id);

        var newTitleTask = _titlesService.SumPowerTitlesPercentAsync(userId);
        var newUserTitleTask = _userTitlesRepository.SumPowerUserTitlesAsync(userId);

        await Task.WhenAll(newTitleTask, newUserTitleTask);

        PowerManager deltaPower = (PowerManager)newTitleTask.Result - (PowerManager)oldTitle;
        PowerManager deltaUserPower = (PowerManager)newUserTitleTask.Result - (PowerManager)oldUserTitle;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTitlesBatchAsync(string userId, List<Titles> titles)
    {
        var oldTitleTask = _titlesService.SumPowerTitlesPercentAsync(userId);
        var oldUserTitleTask = _userTitlesRepository.SumPowerUserTitlesAsync(userId);

        await Task.WhenAll(oldTitleTask, oldUserTitleTask);

        Titles oldTitle = oldTitleTask.Result;
        Titles oldUserTitle = oldUserTitleTask.Result;

        var insertOrUpdateResult = await _userTitlesRepository.InsertOrUpdateUserTitlesBatchAsync(userId, titles);

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
            await _titlesGalleryService.InsertBatchTitlesGalleryAsync(userId, newlyInsertedCards);

            var newTitleTask = _titlesService.SumPowerTitlesPercentAsync(userId);
            var newUserTitleTask = _userTitlesRepository.SumPowerUserTitlesAsync(userId);

            await Task.WhenAll(newTitleTask, newUserTitleTask);

            PowerManager deltaPower = (PowerManager)newTitleTask.Result - (PowerManager)oldTitle;
            PowerManager deltaUserPower = (PowerManager)newUserTitleTask.Result - (PowerManager)oldUserTitle;

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

    public async Task<bool> UpdateUserTitleLevelAsync(string userId, Titles title)
    {
        Titles oldUserTitle = await _userTitlesRepository.SumPowerUserTitlesAsync(userId);

        var updateResult = await _userTitlesRepository.UpdateUserTitleLevelAsync(userId, title);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Titles newUserTitle = await _userTitlesRepository.SumPowerUserTitlesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserTitle - (PowerManager)oldUserTitle;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserTitleStarAsync(string userId, Titles title)
    {
        Titles oldUserTitle = await _userTitlesRepository.SumPowerUserTitlesAsync(userId);

        var updateResult = await _userTitlesRepository.UpdateUserTitleStarAsync(userId, title);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _titlesGalleryService.UpdateTempStarTitleGalleryAsync(userId, title.Id, title.Star);

        Titles newUserTitle = await _userTitlesRepository.SumPowerUserTitlesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserTitle - (PowerManager)oldUserTitle;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Titles> GetUserTitleByIdAsync(string userId, string Id)
    {
        var result = await _userTitlesRepository.GetUserTitleByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Titles> SumPowerUserTitlesAsync(string userId)
    {
        return await _userTitlesRepository.SumPowerUserTitlesAsync(userId);
    }
}
