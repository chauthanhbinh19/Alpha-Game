using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCoresService : IUserCoresService
{
    private readonly IUserCoresRepository _userCoresRepository;
    private readonly ICoresGalleryService _coresGalleryService;
    private readonly ICoresService _coresService;
    private readonly IPowerManagerService _powerManagerService;

    public UserCoresService(
        IUserCoresRepository userCoresRepository,
        ICoresGalleryService coresGalleryService,
        ICoresService coresService,
        IPowerManagerService powerManagerService)
    {
        _userCoresRepository = userCoresRepository;
        _coresGalleryService = coresGalleryService;
        _coresService = coresService;
        _powerManagerService = powerManagerService;
    }

    public static IUserCoresService Create() => ServiceContainer.GetService<IUserCoresService>();

    public async Task<List<Cores>> GetUserCoresAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Cores> list = await _userCoresRepository.GetUserCoresAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCoresCountAsync(string userId, string search, string rare)
    {
        return await _userCoresRepository.GetUserCoresCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoreAsync(string userId, Cores core)
    {
        var oldCoreTask = _coresService.SumPowerCoresPercentAsync(userId);
        var oldUserCoreTask = _userCoresRepository.SumPowerUserCoresAsync(userId);

        await Task.WhenAll(oldCoreTask, oldUserCoreTask);

        Cores oldCore = oldCoreTask.Result;
        Cores oldUserCore = oldUserCoreTask.Result;

        var insertOrUpdateResult = await _userCoresRepository.InsertOrUpdateUserCoreAsync(userId, core);

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

        await _coresGalleryService.InsertCoreGalleryAsync(userId, core.Id);

        var newCoreTask = _coresService.SumPowerCoresPercentAsync(userId);
        var newUserCoreTask = _userCoresRepository.SumPowerUserCoresAsync(userId);

        await Task.WhenAll(newCoreTask, newUserCoreTask);

        PowerManager deltaPower = (PowerManager)newCoreTask.Result - (PowerManager)oldCore;
        PowerManager deltaUserPower = (PowerManager)newUserCoreTask.Result - (PowerManager)oldUserCore;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoresBatchAsync(string userId, List<Cores> cores)
    {
        var oldCoreTask = _coresService.SumPowerCoresPercentAsync(userId);
        var oldUserCoreTask = _userCoresRepository.SumPowerUserCoresAsync(userId);

        await Task.WhenAll(oldCoreTask, oldUserCoreTask);

        Cores oldCore = oldCoreTask.Result;
        Cores oldUserCore = oldUserCoreTask.Result;

        var insertOrUpdateResult = await _userCoresRepository.InsertOrUpdateUserCoresBatchAsync(userId, cores);

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
            await _coresGalleryService.InsertBatchCoresGalleryAsync(userId, newlyInsertedCards);

            var newCoreTask = _coresService.SumPowerCoresPercentAsync(userId);
            var newUserCoreTask = _userCoresRepository.SumPowerUserCoresAsync(userId);

            await Task.WhenAll(newCoreTask, newUserCoreTask);

            PowerManager deltaPower = (PowerManager)newCoreTask.Result - (PowerManager)oldCore;
            PowerManager deltaUserPower = (PowerManager)newUserCoreTask.Result - (PowerManager)oldUserCore;

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

    public async Task<bool> UpdateUserCoreLevelAsync(string userId, Cores core)
    {
        Cores oldUserCore = await _userCoresRepository.SumPowerUserCoresAsync(userId);

        var updateResult = await _userCoresRepository.UpdateUserCoreLevelAsync(userId, core);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Cores newUserCore = await _userCoresRepository.SumPowerUserCoresAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserCore - (PowerManager)oldUserCore;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserCoreStarAsync(string userId, Cores core)
    {
        Cores oldUserCore = await _userCoresRepository.SumPowerUserCoresAsync(userId);

        var updateResult = await _userCoresRepository.UpdateUserCoreStarAsync(userId, core);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _coresGalleryService.UpdateTempStarCoreGalleryAsync(userId, core.Id, core.Star);

        Cores newUserCore = await _userCoresRepository.SumPowerUserCoresAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserCore - (PowerManager)oldUserCore;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Cores> GetUserCoreByIdAsync(string userId, string Id)
    {
        var result = await _userCoresRepository.GetUserCoreByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Cores> SumPowerUserCoresAsync(string userId)
    {
        return await _userCoresRepository.SumPowerUserCoresAsync(userId);
    }
}
