using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritBeastsService : IUserSpiritBeastsService
{
    private readonly IUserSpiritBeastsRepository _userSpiritBeastsRepository;
    private readonly ISpiritBeastsGalleryService _spiritBeastsGalleryService;
    private readonly ISpiritBeastsService _spiritBeastsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserSpiritBeastsService(
        IUserSpiritBeastsRepository userSpiritBeastsRepository,
        ISpiritBeastsGalleryService spiritBeastsGalleryService,
        ISpiritBeastsService spiritBeastsService,
        IPowerManagerService powerManagerService)
    {
        _userSpiritBeastsRepository = userSpiritBeastsRepository;
        _spiritBeastsGalleryService = spiritBeastsGalleryService;
        _spiritBeastsService = spiritBeastsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserSpiritBeastsService Create() => ServiceContainer.GetService<IUserSpiritBeastsService>();

    public async Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetUserSpiritBeastsAsync(userId, search, pageSize, offset, rare);

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

    public async Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string userId, int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetAllUserSpiritBeastsAsync(userId, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsByCardIdsAsync(string userId, List<string> cardIds)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetUserSpiritBeastsByCardIdsAsync(userId, cardIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritBeastsCountAsync(string userId, string search, string rare)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastAsync(string userId, SpiritBeasts spiritBeast)
    {
        var oldSpiritBeastTask = _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        var oldUserSpiritBeastTask = _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);

        await Task.WhenAll(oldSpiritBeastTask, oldUserSpiritBeastTask);

        SpiritBeasts oldSpiritBeast = oldSpiritBeastTask.Result;
        SpiritBeasts oldUserSpiritBeast = oldUserSpiritBeastTask.Result;

        var insertOrUpdateResult = await _userSpiritBeastsRepository.InsertOrUpdateUserSpiritBeastAsync(userId, spiritBeast);

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

        await _spiritBeastsGalleryService.InsertSpiritBeastGalleryAsync(userId, spiritBeast.Id);

        var newSpiritBeastTask = _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        var newUserSpiritBeastTask = _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);

        await Task.WhenAll(newSpiritBeastTask, newUserSpiritBeastTask);

        PowerManager deltaPower = (PowerManager)newSpiritBeastTask.Result - (PowerManager)oldSpiritBeast;
        PowerManager deltaUserPower = (PowerManager)newUserSpiritBeastTask.Result - (PowerManager)oldUserSpiritBeast;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastsBatchAsync(string userId, List<SpiritBeasts> spiritBeasts)
    {
        var oldSpiritBeastTask = _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        var oldUserSpiritBeastTask = _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);

        await Task.WhenAll(oldSpiritBeastTask, oldUserSpiritBeastTask);

        SpiritBeasts oldSpiritBeast = oldSpiritBeastTask.Result;
        SpiritBeasts oldUserSpiritBeast = oldUserSpiritBeastTask.Result;

        var insertOrUpdateResult = await _userSpiritBeastsRepository.InsertOrUpdateUserSpiritBeastsBatchAsync(userId, spiritBeasts);

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
            await _spiritBeastsGalleryService.InsertBatchSpiritBeastsGalleryAsync(userId, newlyInsertedCards);

            var newSpiritBeastTask = _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
            var newUserSpiritBeastTask = _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);

            await Task.WhenAll(newSpiritBeastTask, newUserSpiritBeastTask);

            PowerManager deltaPower = (PowerManager)newSpiritBeastTask.Result - (PowerManager)oldSpiritBeast;
            PowerManager deltaUserPower = (PowerManager)newUserSpiritBeastTask.Result - (PowerManager)oldUserSpiritBeast;

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

    public async Task<bool> UpdateUserSpiritBeastLevelAsync(string userId, SpiritBeasts spiritBeast)
    {
        SpiritBeasts oldUserSpiritBeast = await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);

        var updateResult = await _userSpiritBeastsRepository.UpdateUserSpiritBeastLevelAsync(userId, spiritBeast);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        SpiritBeasts newUserSpiritBeast = await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserSpiritBeast - (PowerManager)oldUserSpiritBeast;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserSpiritBeastStarAsync(string userId, SpiritBeasts spiritBeast)
    {
        SpiritBeasts oldUserSpiritBeast = await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);

        var updateResult = await _userSpiritBeastsRepository.UpdateUserSpiritBeastStarAsync(userId, spiritBeast);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _spiritBeastsGalleryService.UpdateTempStarSpiritBeastGalleryAsync(userId, spiritBeast.Id, spiritBeast.Star);

        SpiritBeasts newUserSpiritBeast = await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserSpiritBeast - (PowerManager)oldUserSpiritBeast;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string userId, string Id)
    {
        var result = await _userSpiritBeastsRepository.GetUserSpiritBeastByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync(string userId)
    {
        return await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);
    }
}
