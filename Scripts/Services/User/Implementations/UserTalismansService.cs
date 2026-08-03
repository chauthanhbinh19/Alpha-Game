using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTalismansService : IUserTalismansService
{
    private readonly IUserTalismansRepository _userTalismansRepository;
    private readonly ITalismansGalleryService _talismansGalleryService;
    private readonly ITalismansService _talismansService;
    private readonly IPowerManagerService _powerManagerService;

    public UserTalismansService(
        IUserTalismansRepository userTalismansRepository,
        ITalismansGalleryService talismansGalleryService,
        ITalismansService talismansService,
        IPowerManagerService powerManagerService)
    {
        _userTalismansRepository = userTalismansRepository;
        _talismansGalleryService = talismansGalleryService;
        _talismansService = talismansService;
        _powerManagerService = powerManagerService;
    }

    public static IUserTalismansService Create() => ServiceContainer.GetService<IUserTalismansService>();

    public async Task<List<Talismans>> GetUserTalismansAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = await _userTalismansRepository.GetUserTalismansAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserTalismansCountAsync(string userId, string search, string type, string rare)
    {
        return await _userTalismansRepository.GetUserTalismansCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismanAsync(string userId, Talismans talisman)
    {
        var oldTalismanTask = _talismansService.SumPowerTalismansPercentAsync(userId);
        var oldUserTalismanTask = _userTalismansRepository.SumPowerUserTalismansAsync(userId);

        await Task.WhenAll(oldTalismanTask, oldUserTalismanTask);

        Talismans oldTalisman = oldTalismanTask.Result;
        Talismans oldUserTalisman = oldUserTalismanTask.Result;

        var insertOrUpdateResult = await _userTalismansRepository.InsertOrUpdateUserTalismanAsync(userId, talisman);

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

        await _talismansGalleryService.InsertTalismanGalleryAsync(userId, talisman.Id);

        var newTalismanTask = _talismansService.SumPowerTalismansPercentAsync(userId);
        var newUserTalismanTask = _userTalismansRepository.SumPowerUserTalismansAsync(userId);

        await Task.WhenAll(newTalismanTask, newUserTalismanTask);

        PowerManager deltaPower = (PowerManager)newTalismanTask.Result - (PowerManager)oldTalisman;
        PowerManager deltaUserPower = (PowerManager)newUserTalismanTask.Result - (PowerManager)oldUserTalisman;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismansBatchAsync(string userId, List<Talismans> talismans)
    {
        var oldTalismanTask = _talismansService.SumPowerTalismansPercentAsync(userId);
        var oldUserTalismanTask = _userTalismansRepository.SumPowerUserTalismansAsync(userId);

        await Task.WhenAll(oldTalismanTask, oldUserTalismanTask);

        Talismans oldTalisman = oldTalismanTask.Result;
        Talismans oldUserTalisman = oldUserTalismanTask.Result;

        var insertOrUpdateResult = await _userTalismansRepository.InsertOrUpdateUserTalismansBatchAsync(userId, talismans);

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
            await _talismansGalleryService.InsertBatchTalismansGalleryAsync(userId, newlyInsertedCards);

            var newTalismanTask = _talismansService.SumPowerTalismansPercentAsync(userId);
            var newUserTalismanTask = _userTalismansRepository.SumPowerUserTalismansAsync(userId);

            await Task.WhenAll(newTalismanTask, newUserTalismanTask);

            PowerManager deltaPower = (PowerManager)newTalismanTask.Result - (PowerManager)oldTalisman;
            PowerManager deltaUserPower = (PowerManager)newUserTalismanTask.Result - (PowerManager)oldUserTalisman;

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

    public async Task<bool> UpdateUserTalismanLevelAsync(string userId, Talismans talisman)
    {
        Talismans oldUserTalisman = await _userTalismansRepository.SumPowerUserTalismansAsync(userId);

        var updateResult = await _userTalismansRepository.UpdateUserTalismanLevelAsync(userId, talisman);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Talismans newUserTalisman = await _userTalismansRepository.SumPowerUserTalismansAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserTalisman - (PowerManager)oldUserTalisman;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserTalismanStarAsync(string userId, Talismans talisman)
    {
        Talismans oldUserTalisman = await _userTalismansRepository.SumPowerUserTalismansAsync(userId);

        var updateResult = await _userTalismansRepository.UpdateUserTalismanStarAsync(userId, talisman);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _talismansGalleryService.UpdateTempStarTalismanGalleryAsync(userId, talisman.Id, talisman.Star);

        Talismans newUserTalisman = await _userTalismansRepository.SumPowerUserTalismansAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserTalisman - (PowerManager)oldUserTalisman;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Talismans> GetUserTalismanByIdAsync(string userId, string Id)
    {
        var result = await _userTalismansRepository.GetUserTalismanByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Talismans> SumPowerUserTalismansAsync(string userId)
    {
        return await _userTalismansRepository.SumPowerUserTalismansAsync(userId);
    }
}
