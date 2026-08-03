using System.Collections.Generic;
using System.Threading.Tasks;

public class UserOutfitsService : IUserOutfitsService
{
    private readonly IUserOutfitsRepository _userOutfitsRepository;
    private readonly IOutfitsGalleryService _outfitsGalleryService;
    private readonly IOutfitsService _outfitsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserOutfitsService(
        IUserOutfitsRepository userOutfitsRepository,
        IOutfitsGalleryService outfitsGalleryService,
        IOutfitsService outfitsService,
        IPowerManagerService powerManagerService)
    {
        _userOutfitsRepository = userOutfitsRepository;
        _outfitsGalleryService = outfitsGalleryService;
        _outfitsService = outfitsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserOutfitsService Create() => ServiceContainer.GetService<IUserOutfitsService>();

    public async Task<List<Outfits>> GetUserOutfitsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Outfits> list = await _userOutfitsRepository.GetUserOutfitsAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserOutfitsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userOutfitsRepository.GetUserOutfitsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserOutfitAsync(string userId, Outfits outfit)
    {
        var oldOutfitTask = _outfitsService.SumPowerOutfitsPercentAsync(userId);
        var oldUserOutfitTask = _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);

        await Task.WhenAll(oldOutfitTask, oldUserOutfitTask);

        Outfits oldOutfit = oldOutfitTask.Result;
        Outfits oldUserOutfit = oldUserOutfitTask.Result;

        var insertOrUpdateResult = await _userOutfitsRepository.InsertOrUpdateUserOutfitAsync(userId, outfit);

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

        await _outfitsGalleryService.InsertOutfitGalleryAsync(userId, outfit.Id);

        var newOutfitTask = _outfitsService.SumPowerOutfitsPercentAsync(userId);
        var newUserOutfitTask = _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);

        await Task.WhenAll(newOutfitTask, newUserOutfitTask);

        PowerManager deltaPower = (PowerManager)newOutfitTask.Result - (PowerManager)oldOutfit;
        PowerManager deltaUserPower = (PowerManager)newUserOutfitTask.Result - (PowerManager)oldUserOutfit;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserOutfitsBatchAsync(string userId, List<Outfits> outfits)
    {
        var oldOutfitTask = _outfitsService.SumPowerOutfitsPercentAsync(userId);
        var oldUserOutfitTask = _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);

        await Task.WhenAll(oldOutfitTask, oldUserOutfitTask);

        Outfits oldOutfit = oldOutfitTask.Result;
        Outfits oldUserOutfit = oldUserOutfitTask.Result;

        var insertOrUpdateResult = await _userOutfitsRepository.InsertOrUpdateUserOutfitsBatchAsync(userId, outfits);

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
            await _outfitsGalleryService.InsertBatchOutfitsGalleryAsync(userId, newlyInsertedCards);

            var newOutfitTask = _outfitsService.SumPowerOutfitsPercentAsync(userId);
            var newUserOutfitTask = _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);

            await Task.WhenAll(newOutfitTask, newUserOutfitTask);

            PowerManager deltaPower = (PowerManager)newOutfitTask.Result - (PowerManager)oldOutfit;
            PowerManager deltaUserPower = (PowerManager)newUserOutfitTask.Result - (PowerManager)oldUserOutfit;

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

    public async Task<bool> UpdateUserOutfitLevelAsync(string userId, Outfits outfit)
    {
        Outfits oldUserOutfit = await _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);

        var updateResult = await _userOutfitsRepository.UpdateUserOutfitLevelAsync(userId, outfit);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Outfits newUserOutfit = await _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserOutfit - (PowerManager)oldUserOutfit;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserOutfitStarAsync(string userId, Outfits outfit)
    {
        Outfits oldUserOutfit = await _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);

        var updateResult = await _userOutfitsRepository.UpdateUserOutfitStarAsync(userId, outfit);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _outfitsGalleryService.UpdateTempStarOutfitGalleryAsync(userId, outfit.Id, outfit.Star);

        Outfits newUserOutfit = await _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserOutfit - (PowerManager)oldUserOutfit;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Outfits> GetUserOutfitByIdAsync(string userId, string Id)
    {
        var result = await _userOutfitsRepository.GetUserOutfitByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Outfits> SumPowerUserOutfitsAsync(string userId)
    {
        return await _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);
    }
}
