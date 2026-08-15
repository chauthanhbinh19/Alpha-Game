using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBadgesService : IUserBadgesService
{
    private readonly IUserBadgesRepository _userBadgesRepository;
    private readonly IBadgesGalleryService _badgesGalleryService;
    private readonly IBadgesService _badgesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserBadgesService(
        IUserBadgesRepository userBadgesRepository,
        IBadgesGalleryService badgesGalleryService,
        IBadgesService badgesService,
        IPowerManagerService powerManagerService)
    {
        _userBadgesRepository = userBadgesRepository;
        _badgesGalleryService = badgesGalleryService;
        _badgesService = badgesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserBadgesService Create() => ServiceContainer.GetService<IUserBadgesService>();

    public async Task<List<Badges>> GetUserBadgesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Badges> result = await _userBadgesRepository.GetUserBadgesAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserBadgesCountAsync(string userId, string search, string rare)
    {
        return await _userBadgesRepository.GetUserBadgesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgeAsync(string userId, Badges badge)
    {
        var oldBadgeTask = _badgesService.SumPowerBadgesPercentAsync(userId);
        var oldUserBadgeTask = _userBadgesRepository.SumPowerUserBadgesAsync(userId);

        await Task.WhenAll(oldBadgeTask, oldUserBadgeTask);

        Badges oldBadge = oldBadgeTask.Result;
        Badges oldUserBadge = oldUserBadgeTask.Result;

        var insertOrUpdateResult = await _userBadgesRepository.InsertOrUpdateUserBadgeAsync(userId, badge);

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

        await _badgesGalleryService.InsertBadgeGalleryAsync(userId, badge.Id);

        var newBadgeTask = _badgesService.SumPowerBadgesPercentAsync(userId);
        var newUserBadgeTask = _userBadgesRepository.SumPowerUserBadgesAsync(userId);

        await Task.WhenAll(newBadgeTask, newUserBadgeTask);

        PowerManager deltaPower = (PowerManager)newBadgeTask.Result - (PowerManager)oldBadge;
        PowerManager deltaUserPower = (PowerManager)newUserBadgeTask.Result - (PowerManager)oldUserBadge;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgesBatchAsync(string userId, List<Badges> badges)
    {
        var oldBadgeTask = _badgesService.SumPowerBadgesPercentAsync(userId);
        var oldUserBadgeTask = _userBadgesRepository.SumPowerUserBadgesAsync(userId);

        await Task.WhenAll(oldBadgeTask, oldUserBadgeTask);

        Badges oldBadge = oldBadgeTask.Result;
        Badges oldUserBadge = oldUserBadgeTask.Result;

        var insertOrUpdateResult = await _userBadgesRepository.InsertOrUpdateUserBadgesBatchAsync(userId, badges);

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
            await _badgesGalleryService.InsertBatchBadgesGalleryAsync(userId, newlyInsertedCards);

            var newBadgeTask = _badgesService.SumPowerBadgesPercentAsync(userId);
            var newUserBadgeTask = _userBadgesRepository.SumPowerUserBadgesAsync(userId);

            await Task.WhenAll(newBadgeTask, newUserBadgeTask);

            PowerManager deltaPower = (PowerManager)newBadgeTask.Result - (PowerManager)oldBadge;
            PowerManager deltaUserPower = (PowerManager)newUserBadgeTask.Result - (PowerManager)oldUserBadge;

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

    public async Task<bool> UpdateUserBadgeLevelAsync(string userId, Badges badge)
    {
        Badges oldUserBadge = await _userBadgesRepository.SumPowerUserBadgesAsync(userId);

        var updateResult = await _userBadgesRepository.UpdateUserBadgeLevelAsync(userId, badge);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Badges newUserBadge = await _userBadgesRepository.SumPowerUserBadgesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserBadge - (PowerManager)oldUserBadge;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserBadgeStarAsync(string userId, Badges badge)
    {
        Badges oldUserBadge = await _userBadgesRepository.SumPowerUserBadgesAsync(userId);

        var updateResult = await _userBadgesRepository.UpdateUserBadgeStarAsync(userId, badge);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _badgesGalleryService.UpdateTempStarBadgeGalleryAsync(userId, badge.Id, badge.Star);

        Badges newUserBadge = await _userBadgesRepository.SumPowerUserBadgesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserBadge - (PowerManager)oldUserBadge;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Badges> GetUserBadgeByIdAsync(string userId, string Id)
    {
        var result = await _userBadgesRepository.GetUserBadgeByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Badges> SumPowerUserBadgesAsync(string userId)
    {
        return await _userBadgesRepository.SumPowerUserBadgesAsync(userId);
    }
}
