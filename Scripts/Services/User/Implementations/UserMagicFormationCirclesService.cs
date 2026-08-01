using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMagicFormationCirclesService : IUserMagicFormationCirclesService
{
    private readonly IUserMagicFormationCirclesRepository _userMagicFormationCirclesRepository;
    private readonly IMagicFormationCirclesGalleryService _magicFormationCirclesGalleryService;
    private readonly IMagicFormationCirclesService _magicFormationCirclesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserMagicFormationCirclesService(
        IUserMagicFormationCirclesRepository userMagicFormationCirclesRepository,
        IMagicFormationCirclesGalleryService magicFormationCirclesGalleryService,
        IMagicFormationCirclesService magicFormationCirclesService,
        IPowerManagerService powerManagerService)
    {
        _userMagicFormationCirclesRepository = userMagicFormationCirclesRepository;
        _magicFormationCirclesGalleryService = magicFormationCirclesGalleryService;
        _magicFormationCirclesService = magicFormationCirclesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserMagicFormationCirclesService Create() => ServiceContainer.GetService<IUserMagicFormationCirclesService>();

    public async Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMagicFormationCirclesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCircleAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        var oldMagicFormationCircleTask = _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        var oldUserMagicFormationCircleTask = _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);

        await Task.WhenAll(oldMagicFormationCircleTask, oldUserMagicFormationCircleTask);

        MagicFormationCircles oldMagicFormationCircle = oldMagicFormationCircleTask.Result;
        MagicFormationCircles oldUserMagicFormationCircle = oldUserMagicFormationCircleTask.Result;

        var insertOrUpdateResult = await _userMagicFormationCirclesRepository.InsertOrUpdateUserMagicFormationCircleAsync(userId, magicFormationCircle);

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

        await _magicFormationCirclesGalleryService.InsertMagicFormationCircleGalleryAsync(userId, magicFormationCircle.Id);

        var newMagicFormationCircleTask = _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        var newUserMagicFormationCircleTask = _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);

        await Task.WhenAll(newMagicFormationCircleTask, newUserMagicFormationCircleTask);

        PowerManager deltaPower = (PowerManager)newMagicFormationCircleTask.Result - (PowerManager)oldMagicFormationCircle;
        PowerManager deltaUserPower = (PowerManager)newUserMagicFormationCircleTask.Result - (PowerManager)oldUserMagicFormationCircle;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCirclesBatchAsync(string userId, List<MagicFormationCircles> magicFormationCircles)
    {
        var oldMagicFormationCircleTask = _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        var oldUserMagicFormationCircleTask = _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);

        await Task.WhenAll(oldMagicFormationCircleTask, oldUserMagicFormationCircleTask);

        MagicFormationCircles oldMagicFormationCircle = oldMagicFormationCircleTask.Result;
        MagicFormationCircles oldUserMagicFormationCircle = oldUserMagicFormationCircleTask.Result;

        var insertOrUpdateResult = await _userMagicFormationCirclesRepository.InsertOrUpdateUserMagicFormationCirclesBatchAsync(userId, magicFormationCircles);

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
            await _magicFormationCirclesGalleryService.InsertBatchMagicFormationCirclesGalleryAsync(userId, newlyInsertedCards);

            var newMagicFormationCircleTask = _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
            var newUserMagicFormationCircleTask = _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);

            await Task.WhenAll(newMagicFormationCircleTask, newUserMagicFormationCircleTask);

            PowerManager deltaPower = (PowerManager)newMagicFormationCircleTask.Result - (PowerManager)oldMagicFormationCircle;
            PowerManager deltaUserPower = (PowerManager)newUserMagicFormationCircleTask.Result - (PowerManager)oldUserMagicFormationCircle;

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

    public async Task<bool> UpdateUserMagicFormationCircleLevelAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        MagicFormationCircles oldUserMagicFormationCircle = await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);

        var updateResult = await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleLevelAsync(userId, magicFormationCircle);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        MagicFormationCircles newUserMagicFormationCircle = await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserMagicFormationCircle - (PowerManager)oldUserMagicFormationCircle;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserMagicFormationCircleStarAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        MagicFormationCircles oldUserMagicFormationCircle = await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);

        var updateResult = await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleStarAsync(userId, magicFormationCircle);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _magicFormationCirclesGalleryService.UpdateTempStarMagicFormationCircleGalleryAsync(userId, magicFormationCircle.Id, magicFormationCircle.Star);

        MagicFormationCircles newUserMagicFormationCircle = await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserMagicFormationCircle - (PowerManager)oldUserMagicFormationCircle;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string userId, string Id)
    {
        var result = await _userMagicFormationCirclesRepository.GetUserMagicFormationCircleByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync(string userId)
    {
        return await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);
    }
}
