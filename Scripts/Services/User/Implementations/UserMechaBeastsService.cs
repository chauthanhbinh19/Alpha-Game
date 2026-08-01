using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMechaBeastsService : IUserMechaBeastsService
{
    private readonly IUserMechaBeastsRepository _userMechaBeastsRepository;
    private readonly IMechaBeastsGalleryService _mechaBeastsGalleryService;
    private readonly IMechaBeastsService _mechaBeastsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserMechaBeastsService(
        IUserMechaBeastsRepository userMechaBeastsRepository,
        IMechaBeastsGalleryService mechaBeastsGalleryService,
        IMechaBeastsService mechaBeastsService,
        IPowerManagerService powerManagerService)
    {
        _userMechaBeastsRepository = userMechaBeastsRepository;
        _mechaBeastsGalleryService = mechaBeastsGalleryService;
        _mechaBeastsService = mechaBeastsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserMechaBeastsService Create() => ServiceContainer.GetService<IUserMechaBeastsService>();

    public async Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _userMechaBeastsRepository.GetUserMechaBeastsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMechaBeastsCountAsync(string userId, string search, string rare)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastAsync(string userId, MechaBeasts mechaBeast)
    {
        var oldMechaBeastTask = _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        var oldUserMechaBeastTask = _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);

        await Task.WhenAll(oldMechaBeastTask, oldUserMechaBeastTask);

        MechaBeasts oldMechaBeast = oldMechaBeastTask.Result;
        MechaBeasts oldUserMechaBeast = oldUserMechaBeastTask.Result;

        var insertOrUpdateResult = await _userMechaBeastsRepository.InsertOrUpdateUserMechaBeastAsync(userId, mechaBeast);

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

        await _mechaBeastsGalleryService.InsertMechaBeastGalleryAsync(userId, mechaBeast.Id);

        var newMechaBeastTask = _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        var newUserMechaBeastTask = _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);

        await Task.WhenAll(newMechaBeastTask, newUserMechaBeastTask);

        PowerManager deltaPower = (PowerManager)newMechaBeastTask.Result - (PowerManager)oldMechaBeast;
        PowerManager deltaUserPower = (PowerManager)newUserMechaBeastTask.Result - (PowerManager)oldUserMechaBeast;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastsBatchAsync(string userId, List<MechaBeasts> mechaBeasts)
    {
        var oldMechaBeastTask = _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        var oldUserMechaBeastTask = _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);

        await Task.WhenAll(oldMechaBeastTask, oldUserMechaBeastTask);

        MechaBeasts oldMechaBeast = oldMechaBeastTask.Result;
        MechaBeasts oldUserMechaBeast = oldUserMechaBeastTask.Result;

        var insertOrUpdateResult = await _userMechaBeastsRepository.InsertOrUpdateUserMechaBeastsBatchAsync(userId, mechaBeasts);

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
            await _mechaBeastsGalleryService.InsertBatchMechaBeastsGalleryAsync(userId, newlyInsertedCards);

            var newMechaBeastTask = _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
            var newUserMechaBeastTask = _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);

            await Task.WhenAll(newMechaBeastTask, newUserMechaBeastTask);

            PowerManager deltaPower = (PowerManager)newMechaBeastTask.Result - (PowerManager)oldMechaBeast;
            PowerManager deltaUserPower = (PowerManager)newUserMechaBeastTask.Result - (PowerManager)oldUserMechaBeast;

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

    public async Task<bool> UpdateUserMechaBeastLevelAsync(string userId, MechaBeasts mechaBeast)
    {
        MechaBeasts oldUserMechaBeast = await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);

        var updateResult = await _userMechaBeastsRepository.UpdateUserMechaBeastLevelAsync(userId, mechaBeast);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        MechaBeasts newUserMechaBeast = await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserMechaBeast - (PowerManager)oldUserMechaBeast;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserMechaBeastStarAsync(string userId, MechaBeasts mechaBeast)
    {
        MechaBeasts oldUserMechaBeast = await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);

        var updateResult = await _userMechaBeastsRepository.UpdateUserMechaBeastStarAsync(userId, mechaBeast);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _mechaBeastsGalleryService.UpdateTempStarMechaBeastGalleryAsync(userId, mechaBeast.Id, mechaBeast.Star);

        MechaBeasts newUserMechaBeast = await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserMechaBeast - (PowerManager)oldUserMechaBeast;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<MechaBeasts> GetUserMechaBeastByIdAsync(string userId, string Id)
    {
        var result = await _userMechaBeastsRepository.GetUserMechaBeastByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<MechaBeasts> SumPowerUserMechaBeastsAsync(string userId)
    {
        return await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);
    }
}
