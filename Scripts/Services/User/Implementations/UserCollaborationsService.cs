using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCollaborationsService : IUserCollaborationsService
{
    private readonly IUserCollaborationsRepository _userCollaborationsRepository;
    private readonly ICollaborationsGalleryService _collaborationsGalleryService;
    private readonly ICollaborationsService _collaborationsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserCollaborationsService(
        IUserCollaborationsRepository userCollaborationsRepository,
        ICollaborationsGalleryService collaborationsGalleryService,
        ICollaborationsService collaborationsService,
        IPowerManagerService powerManagerService)
    {
        _userCollaborationsRepository = userCollaborationsRepository;
        _collaborationsGalleryService = collaborationsGalleryService;
        _collaborationsService = collaborationsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserCollaborationsService Create() => ServiceContainer.GetService<IUserCollaborationsService>();

    public async Task<List<Collaborations>> GetUserCollaborationsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Collaborations> list = await _userCollaborationsRepository.GetUserCollaborationsAsync(userId, search, pageSize, offset, rare);

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

    public async Task<int> GetUserCollaborationsCountAsync(string userId, string search, string rare)
    {
        return await _userCollaborationsRepository.GetUserCollaborationsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationAsync(string userId, Collaborations collaboration)
    {
        var oldCollaborationTask = _collaborationsService.SumPowerCollaborationsPercentAsync(userId);
        var oldUserCollaborationTask = _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);

        await Task.WhenAll(oldCollaborationTask, oldUserCollaborationTask);

        Collaborations oldCollaboration = oldCollaborationTask.Result;
        Collaborations oldUserCollaboration = oldUserCollaborationTask.Result;

        var insertOrUpdateResult = await _userCollaborationsRepository.InsertOrUpdateUserCollaborationAsync(userId, collaboration);

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

        await _collaborationsGalleryService.InsertCollaborationGalleryAsync(userId, collaboration.Id);

        var newCollaborationTask = _collaborationsService.SumPowerCollaborationsPercentAsync(userId);
        var newUserCollaborationTask = _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);

        await Task.WhenAll(newCollaborationTask, newUserCollaborationTask);

        PowerManager deltaPower = (PowerManager)newCollaborationTask.Result - (PowerManager)oldCollaboration;
        PowerManager deltaUserPower = (PowerManager)newUserCollaborationTask.Result - (PowerManager)oldUserCollaboration;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationsBatchAsync(string userId, List<Collaborations> collaborations)
    {
        var oldCollaborationTask = _collaborationsService.SumPowerCollaborationsPercentAsync(userId);
        var oldUserCollaborationTask = _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);

        await Task.WhenAll(oldCollaborationTask, oldUserCollaborationTask);

        Collaborations oldCollaboration = oldCollaborationTask.Result;
        Collaborations oldUserCollaboration = oldUserCollaborationTask.Result;

        var insertOrUpdateResult = await _userCollaborationsRepository.InsertOrUpdateUserCollaborationsBatchAsync(userId, collaborations);

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
            await _collaborationsGalleryService.InsertBatchCollaborationsGalleryAsync(userId, newlyInsertedCards);

            var newCollaborationTask = _collaborationsService.SumPowerCollaborationsPercentAsync(userId);
            var newUserCollaborationTask = _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);

            await Task.WhenAll(newCollaborationTask, newUserCollaborationTask);

            PowerManager deltaPower = (PowerManager)newCollaborationTask.Result - (PowerManager)oldCollaboration;
            PowerManager deltaUserPower = (PowerManager)newUserCollaborationTask.Result - (PowerManager)oldUserCollaboration;

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

    public async Task<bool> UpdateUserCollaborationLevelAsync(string userId, Collaborations collaboration)
    {
        Collaborations oldUserCollaboration = await _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);

        var updateResult = await _userCollaborationsRepository.UpdateUserCollaborationLevelAsync(userId, collaboration);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Collaborations newUserCollaboration = await _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserCollaboration - (PowerManager)oldUserCollaboration;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserCollaborationStarAsync(string userId, Collaborations collaboration)
    {
        Collaborations oldUserCollaboration = await _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);

        var updateResult = await _userCollaborationsRepository.UpdateUserCollaborationStarAsync(userId, collaboration);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _collaborationsGalleryService.UpdateTempStarCollaborationGalleryAsync(userId, collaboration.Id, collaboration.Star);

        Collaborations newUserCollaboration = await _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserCollaboration - (PowerManager)oldUserCollaboration;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Collaborations> GetUserCollaborationByIdAsync(string userId, string Id)
    {
        var result = await _userCollaborationsRepository.GetUserCollaborationByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Collaborations> SumPowerUserCollaborationsAsync(string userId)
    {
        return await _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);
    }
}
