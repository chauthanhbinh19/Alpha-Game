using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPuppetsService : IUserPuppetsService
{
    private readonly IUserPuppetsRepository _userPuppetsRepository;
    private readonly IPuppetsGalleryService _puppetsGalleryService;
    private readonly IPuppetsService _puppetsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserPuppetsService(
        IUserPuppetsRepository userPuppetsRepository,
        IPuppetsGalleryService puppetsGalleryService,
        IPuppetsService puppetsService,
        IPowerManagerService powerManagerService)
    {
        _userPuppetsRepository = userPuppetsRepository;
        _puppetsGalleryService = puppetsGalleryService;
        _puppetsService = puppetsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserPuppetsService Create() => ServiceContainer.GetService<IUserPuppetsService>();

    public async Task<List<Puppets>> GetUserPuppetsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _userPuppetsRepository.GetUserPuppetsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPuppetsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userPuppetsRepository.GetUserPuppetsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetAsync(string userId, Puppets puppet)
    {
        var oldPuppetTask = _puppetsService.SumPowerPuppetsPercentAsync(userId);
        var oldUserPuppetTask = _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);

        await Task.WhenAll(oldPuppetTask, oldUserPuppetTask);

        Puppets oldPuppet = oldPuppetTask.Result;
        Puppets oldUserPuppet = oldUserPuppetTask.Result;

        var insertOrUpdateResult = await _userPuppetsRepository.InsertOrUpdateUserPuppetAsync(userId, puppet);

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

        await _puppetsGalleryService.InsertPuppetGalleryAsync(userId, puppet.Id);

        var newPuppetTask = _puppetsService.SumPowerPuppetsPercentAsync(userId);
        var newUserPuppetTask = _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);

        await Task.WhenAll(newPuppetTask, newUserPuppetTask);

        PowerManager deltaPower = (PowerManager)newPuppetTask.Result - (PowerManager)oldPuppet;
        PowerManager deltaUserPower = (PowerManager)newUserPuppetTask.Result - (PowerManager)oldUserPuppet;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetsBatchAsync(string userId, List<Puppets> puppets)
    {
        var oldPuppetTask = _puppetsService.SumPowerPuppetsPercentAsync(userId);
        var oldUserPuppetTask = _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);

        await Task.WhenAll(oldPuppetTask, oldUserPuppetTask);

        Puppets oldPuppet = oldPuppetTask.Result;
        Puppets oldUserPuppet = oldUserPuppetTask.Result;

        var insertOrUpdateResult = await _userPuppetsRepository.InsertOrUpdateUserPuppetsBatchAsync(userId, puppets);

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
            await _puppetsGalleryService.InsertBatchPuppetsGalleryAsync(userId, newlyInsertedCards);

            var newPuppetTask = _puppetsService.SumPowerPuppetsPercentAsync(userId);
            var newUserPuppetTask = _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);

            await Task.WhenAll(newPuppetTask, newUserPuppetTask);

            PowerManager deltaPower = (PowerManager)newPuppetTask.Result - (PowerManager)oldPuppet;
            PowerManager deltaUserPower = (PowerManager)newUserPuppetTask.Result - (PowerManager)oldUserPuppet;

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

    public async Task<bool> UpdateUserPuppetLevelAsync(string userId, Puppets puppet)
    {
        Puppets oldUserPuppet = await _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);

        var updateResult = await _userPuppetsRepository.UpdateUserPuppetLevelAsync(userId, puppet);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Puppets newUserPuppet = await _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserPuppet - (PowerManager)oldUserPuppet;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserPuppetStarAsync(string userId, Puppets puppet)
    {
        Puppets oldUserPuppet = await _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);

        var updateResult = await _userPuppetsRepository.UpdateUserPuppetStarAsync(userId, puppet);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _puppetsGalleryService.UpdateTempStarPuppetGalleryAsync(userId, puppet.Id, puppet.Star);

        Puppets newUserPuppet = await _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserPuppet - (PowerManager)oldUserPuppet;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Puppets> GetUserPuppetByIdAsync(string userId, string Id)
    {
        var result = await _userPuppetsRepository.GetUserPuppetByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Puppets> SumPowerUserPuppetsAsync(string userId)
    {
        return await _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);
    }
}
