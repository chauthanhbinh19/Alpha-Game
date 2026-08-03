using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRunesService : IUserRunesService
{
    private readonly IUserRunesRepository _userRunesRepository;
    private readonly IRunesGalleryService _runesGalleryService;
    private readonly IRunesService _runesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserRunesService(
        IUserRunesRepository userRunesRepository,
        IRunesGalleryService runesGalleryService,
        IRunesService runesService,
        IPowerManagerService powerManagerService)
    {
        _userRunesRepository = userRunesRepository;
        _runesGalleryService = runesGalleryService;
        _runesService = runesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserRunesService Create() => ServiceContainer.GetService<IUserRunesService>();

    public async Task<List<Runes>> GetUserRunesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Runes> list = await _userRunesRepository.GetUserRunesAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserRunesCountAsync(string userId, string search, string rare)
    {
        return await _userRunesRepository.GetUserRunesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRuneAsync(string userId, Runes rune)
    {
        var oldRuneTask = _runesService.SumPowerRunesPercentAsync(userId);
        var oldUserRuneTask = _userRunesRepository.SumPowerUserRunesAsync(userId);

        await Task.WhenAll(oldRuneTask, oldUserRuneTask);

        Runes oldRune = oldRuneTask.Result;
        Runes oldUserRune = oldUserRuneTask.Result;

        var insertOrUpdateResult = await _userRunesRepository.InsertOrUpdateUserRuneAsync(userId, rune);

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

        await _runesGalleryService.InsertRuneGalleryAsync(userId, rune.Id);

        var newRuneTask = _runesService.SumPowerRunesPercentAsync(userId);
        var newUserRuneTask = _userRunesRepository.SumPowerUserRunesAsync(userId);

        await Task.WhenAll(newRuneTask, newUserRuneTask);

        PowerManager deltaPower = (PowerManager)newRuneTask.Result - (PowerManager)oldRune;
        PowerManager deltaUserPower = (PowerManager)newUserRuneTask.Result - (PowerManager)oldUserRune;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRunesBatchAsync(string userId, List<Runes> runes)
    {
        var oldRuneTask = _runesService.SumPowerRunesPercentAsync(userId);
        var oldUserRuneTask = _userRunesRepository.SumPowerUserRunesAsync(userId);

        await Task.WhenAll(oldRuneTask, oldUserRuneTask);

        Runes oldRune = oldRuneTask.Result;
        Runes oldUserRune = oldUserRuneTask.Result;

        var insertOrUpdateResult = await _userRunesRepository.InsertOrUpdateUserRunesBatchAsync(userId, runes);

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
            await _runesGalleryService.InsertBatchRunesGalleryAsync(userId, newlyInsertedCards);

            var newRuneTask = _runesService.SumPowerRunesPercentAsync(userId);
            var newUserRuneTask = _userRunesRepository.SumPowerUserRunesAsync(userId);

            await Task.WhenAll(newRuneTask, newUserRuneTask);

            PowerManager deltaPower = (PowerManager)newRuneTask.Result - (PowerManager)oldRune;
            PowerManager deltaUserPower = (PowerManager)newUserRuneTask.Result - (PowerManager)oldUserRune;

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

    public async Task<bool> UpdateUserRuneLevelAsync(string userId, Runes rune)
    {
        Runes oldUserRune = await _userRunesRepository.SumPowerUserRunesAsync(userId);

        var updateResult = await _userRunesRepository.UpdateUserRuneLevelAsync(userId, rune);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Runes newUserRune = await _userRunesRepository.SumPowerUserRunesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserRune - (PowerManager)oldUserRune;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserRuneStarAsync(string userId, Runes rune)
    {
        Runes oldUserRune = await _userRunesRepository.SumPowerUserRunesAsync(userId);

        var updateResult = await _userRunesRepository.UpdateUserRuneStarAsync(userId, rune);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _runesGalleryService.UpdateTempStarRuneGalleryAsync(userId, rune.Id, rune.Star);

        Runes newUserRune = await _userRunesRepository.SumPowerUserRunesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserRune - (PowerManager)oldUserRune;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Runes> GetUserRuneByIdAsync(string userId, string Id)
    {
        var result = await _userRunesRepository.GetUserRuneByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Runes> SumPowerUserRunesAsync(string userId)
    {
        return await _userRunesRepository.SumPowerUserRunesAsync(userId);
    }
}
