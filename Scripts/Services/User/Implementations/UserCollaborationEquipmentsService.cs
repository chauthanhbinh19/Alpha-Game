using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCollaborationEquipmentsService : IUserCollaborationEquipmentsService
{
    private readonly IUserCollaborationEquipmentsRepository _userCollaborationEquipmentsRepository;
    private readonly ICollaborationEquipmentsGalleryService _collaborationEquipmentsGalleryService;
    private readonly ICollaborationEquipmentsService _collaborationEquipmentsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserCollaborationEquipmentsService(
        IUserCollaborationEquipmentsRepository userCollaborationEquipmentsRepository,
        ICollaborationEquipmentsGalleryService collaborationEquipmentsGalleryService,
        ICollaborationEquipmentsService collaborationEquipmentsService,
        IPowerManagerService powerManagerService)
    {
        _userCollaborationEquipmentsRepository = userCollaborationEquipmentsRepository;
        _collaborationEquipmentsGalleryService = collaborationEquipmentsGalleryService;
        _collaborationEquipmentsService = collaborationEquipmentsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserCollaborationEquipmentsService Create() => ServiceContainer.GetService<IUserCollaborationEquipmentsService>();

    public async Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> result = await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsAsync(userId, search, type, pageSize, offset, rare);

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

    public async Task<int> GetUserCollaborationEquipmentsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationEquipmentAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        var oldCollaborationEquipmentTask = _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        var oldUserCollaborationEquipmentTask = _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);

        await Task.WhenAll(oldCollaborationEquipmentTask, oldUserCollaborationEquipmentTask);

        CollaborationEquipments oldCollaborationEquipment = oldCollaborationEquipmentTask.Result;
        CollaborationEquipments oldUserCollaborationEquipment = oldUserCollaborationEquipmentTask.Result;

        var insertOrUpdateResult = await _userCollaborationEquipmentsRepository.InsertOrUpdateUserCollaborationEquipmentAsync(userId, collaborationEquipment);

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

        await _collaborationEquipmentsGalleryService.InsertCollaborationEquipmentGalleryAsync(userId, collaborationEquipment.Id);

        var newCollaborationEquipmentTask = _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        var newUserCollaborationEquipmentTask = _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);

        await Task.WhenAll(newCollaborationEquipmentTask, newUserCollaborationEquipmentTask);

        PowerManager deltaPower = (PowerManager)newCollaborationEquipmentTask.Result - (PowerManager)oldCollaborationEquipment;
        PowerManager deltaUserPower = (PowerManager)newUserCollaborationEquipmentTask.Result - (PowerManager)oldUserCollaborationEquipment;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationEquipmentsBatchAsync(string userId, List<CollaborationEquipments> collaborationEquipments)
    {
        var oldCollaborationEquipmentTask = _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        var oldUserCollaborationEquipmentTask = _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);

        await Task.WhenAll(oldCollaborationEquipmentTask, oldUserCollaborationEquipmentTask);

        CollaborationEquipments oldCollaborationEquipment = oldCollaborationEquipmentTask.Result;
        CollaborationEquipments oldUserCollaborationEquipment = oldUserCollaborationEquipmentTask.Result;

        var insertOrUpdateResult = await _userCollaborationEquipmentsRepository.InsertOrUpdateUserCollaborationEquipmentsBatchAsync(userId, collaborationEquipments);

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
            await _collaborationEquipmentsGalleryService.InsertBatchCollaborationEquipmentsGalleryAsync(userId, newlyInsertedCards);

            var newCollaborationEquipmentTask = _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
            var newUserCollaborationEquipmentTask = _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);

            await Task.WhenAll(newCollaborationEquipmentTask, newUserCollaborationEquipmentTask);

            PowerManager deltaPower = (PowerManager)newCollaborationEquipmentTask.Result - (PowerManager)oldCollaborationEquipment;
            PowerManager deltaUserPower = (PowerManager)newUserCollaborationEquipmentTask.Result - (PowerManager)oldUserCollaborationEquipment;

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

    public async Task<bool> UpdateUserCollaborationEquipmentLevelAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        CollaborationEquipments oldUserCollaborationEquipment = await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);

        var updateResult = await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentLevelAsync(userId, collaborationEquipment);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        CollaborationEquipments newUserCollaborationEquipment = await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserCollaborationEquipment - (PowerManager)oldUserCollaborationEquipment;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserCollaborationEquipmentStarAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        CollaborationEquipments oldUserCollaborationEquipment = await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);

        var updateResult = await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentStarAsync(userId, collaborationEquipment);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _collaborationEquipmentsGalleryService.UpdateTempStarCollaborationEquipmentGalleryAsync(userId, collaborationEquipment.Id, collaborationEquipment.Star);

        CollaborationEquipments newUserCollaborationEquipment = await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserCollaborationEquipment - (PowerManager)oldUserCollaborationEquipment;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string userId, string Id)
    {
        var result = await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync(string userId)
    {
        return await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);
    }
}
