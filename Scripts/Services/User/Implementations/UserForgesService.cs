using System.Collections.Generic;
using System.Threading.Tasks;

public class UserForgesService : IUserForgesService
{
    private readonly IUserForgesRepository _userForgesRepository;
    private readonly IForgesGalleryService _forgesGalleryService;
    private readonly IForgesService _forgesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserForgesService(
        IUserForgesRepository userForgesRepository,
        IForgesGalleryService forgesGalleryService,
        IForgesService forgesService,
        IPowerManagerService powerManagerService)
    {
        _userForgesRepository = userForgesRepository;
        _forgesGalleryService = forgesGalleryService;
        _forgesService = forgesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserForgesService Create() => ServiceContainer.GetService<IUserForgesService>();

    public async Task<List<Forges>> GetUserForgesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _userForgesRepository.GetUserForgesAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserForgesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userForgesRepository.GetUserForgesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgeAsync(string userId, Forges forge)
    {
        var oldForgeTask = _forgesService.SumPowerForgesPercentAsync(userId);
        var oldUserForgeTask = _userForgesRepository.SumPowerUserForgesAsync(userId);

        await Task.WhenAll(oldForgeTask, oldUserForgeTask);

        Forges oldForge = oldForgeTask.Result;
        Forges oldUserForge = oldUserForgeTask.Result;

        var insertOrUpdateResult = await _userForgesRepository.InsertOrUpdateUserForgeAsync(userId, forge);

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

        await _forgesGalleryService.InsertForgeGalleryAsync(userId, forge.Id);

        var newForgeTask = _forgesService.SumPowerForgesPercentAsync(userId);
        var newUserForgeTask = _userForgesRepository.SumPowerUserForgesAsync(userId);

        await Task.WhenAll(newForgeTask, newUserForgeTask);

        PowerManager deltaPower = (PowerManager)newForgeTask.Result - (PowerManager)oldForge;
        PowerManager deltaUserPower = (PowerManager)newUserForgeTask.Result - (PowerManager)oldUserForge;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgesBatchAsync(string userId, List<Forges> forges)
    {
        var oldForgeTask = _forgesService.SumPowerForgesPercentAsync(userId);
        var oldUserForgeTask = _userForgesRepository.SumPowerUserForgesAsync(userId);

        await Task.WhenAll(oldForgeTask, oldUserForgeTask);

        Forges oldForge = oldForgeTask.Result;
        Forges oldUserForge = oldUserForgeTask.Result;

        var insertOrUpdateResult = await _userForgesRepository.InsertOrUpdateUserForgesBatchAsync(userId, forges);

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
            await _forgesGalleryService.InsertBatchForgesGalleryAsync(userId, newlyInsertedCards);

            var newForgeTask = _forgesService.SumPowerForgesPercentAsync(userId);
            var newUserForgeTask = _userForgesRepository.SumPowerUserForgesAsync(userId);

            await Task.WhenAll(newForgeTask, newUserForgeTask);

            PowerManager deltaPower = (PowerManager)newForgeTask.Result - (PowerManager)oldForge;
            PowerManager deltaUserPower = (PowerManager)newUserForgeTask.Result - (PowerManager)oldUserForge;

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

    public async Task<bool> UpdateUserForgeLevelAsync(string userId, Forges forge)
    {
        Forges oldUserForge = await _userForgesRepository.SumPowerUserForgesAsync(userId);

        var updateResult = await _userForgesRepository.UpdateUserForgeLevelAsync(userId, forge);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Forges newUserForge = await _userForgesRepository.SumPowerUserForgesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserForge - (PowerManager)oldUserForge;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserForgeStarAsync(string userId, Forges forge)
    {
        Forges oldUserForge = await _userForgesRepository.SumPowerUserForgesAsync(userId);

        var updateResult = await _userForgesRepository.UpdateUserForgeStarAsync(userId, forge);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _forgesGalleryService.UpdateTempStarForgeGalleryAsync(userId, forge.Id, forge.Star);

        Forges newUserForge = await _userForgesRepository.SumPowerUserForgesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserForge - (PowerManager)oldUserForge;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Forges> GetUserForgeByIdAsync(string userId, string Id)
    {
        var result = await _userForgesRepository.GetUserForgeByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Forges> SumPowerUserForgesAsync(string userId)
    {
        return await _userForgesRepository.SumPowerUserForgesAsync(userId);
    }
}
