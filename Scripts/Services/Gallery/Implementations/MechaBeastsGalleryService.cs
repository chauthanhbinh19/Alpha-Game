using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MechaBeastsGalleryService : IMechaBeastsGalleryService
{
    private readonly IMechaBeastsGalleryRepository _mechaBeastsGalleryRepository;
    private readonly IMechaBeastsService _mechaBeastsService;
    private readonly IPowerManagerService _powerManagerService;

    public MechaBeastsGalleryService(
        IMechaBeastsGalleryRepository mechaBeastsGalleryRepository,
        IMechaBeastsService mechaBeastsService,
        IPowerManagerService powerManagerService)
    {
        _mechaBeastsGalleryRepository = mechaBeastsGalleryRepository;
        _mechaBeastsService = mechaBeastsService;
        _powerManagerService = powerManagerService;
    }

    public static IMechaBeastsGalleryService Create() => ServiceContainer.GetService<IMechaBeastsGalleryService>();

    public async Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _mechaBeastsGalleryRepository.GetMechaBeastsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string search, string rare)
    {
        return await _mechaBeastsGalleryRepository.GetMechaBeastsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertMechaBeastGalleryAsync(string userId, string Id)
    {
        var checkResult = await _mechaBeastsService.IsMechaBeastDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _mechaBeastsGalleryRepository.InsertMechaBeastGalleryAsync(userId, Id, await _mechaBeastsService.GetMechaBeastByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusMechaBeastGalleryAsync(string userId, string mechaBeastId)
    {
        var checkResult = await _mechaBeastsService.IsMechaBeastDeletedOrInactiveAsync(mechaBeastId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _mechaBeastsGalleryRepository.UpdateStatusMechaBeastGalleryAsync(userId, mechaBeastId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        MechaBeasts mechaBeastGallery = await GetMechaBeastCollectionByIdAsync(userId, mechaBeastId) ?? new MechaBeasts();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)mechaBeastGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMechaBeastsGalleryAsync(string userId)
    {
        MechaBeasts oldMechaBeast = await SumPowerMechaBeastsGalleryAsync(userId);

        var updateResult = await _mechaBeastsGalleryRepository.UpdateBatchStatusMechaBeastsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        MechaBeasts newMechaBeast = await SumPowerMechaBeastsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMechaBeast - (PowerManager)oldMechaBeast;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync(string userId)
    {
        return await _mechaBeastsGalleryRepository.SumPowerMechaBeastsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarMechaBeastGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _mechaBeastsService.IsMechaBeastDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _mechaBeastsGalleryRepository.UpdateTempStarMechaBeastGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarMechaBeastGalleryAsync(string userId, string mechaBeastId)
    {
        var checkResult = await _mechaBeastsService.IsMechaBeastDeletedOrInactiveAsync(mechaBeastId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        MechaBeasts oldMechaBeast = await GetMechaBeastCollectionByIdAsync(userId, mechaBeastId) ?? new MechaBeasts();

        var updateResult = await _mechaBeastsGalleryRepository.UpdateCurrentStarMechaBeastGalleryAsync(userId, mechaBeastId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        MechaBeasts newMechaBeast = await GetMechaBeastCollectionByIdAsync(userId, mechaBeastId) ?? new MechaBeasts();
        PowerManager deltaPower = (PowerManager)newMechaBeast - (PowerManager)oldMechaBeast;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarMechaBeastsGalleryAsync(string userId)
    {
        MechaBeasts oldMechaBeast = await SumPowerMechaBeastsGalleryAsync(userId);

        var updateResult = await _mechaBeastsGalleryRepository.UpdateBatchCurrentStarMechaBeastsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        MechaBeasts newMechaBeast = await SumPowerMechaBeastsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMechaBeast - (PowerManager)oldMechaBeast;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchMechaBeastsGalleryAsync(string userId, List<MechaBeasts> mechaBeasts)
    {
        var insertResult = await _mechaBeastsGalleryRepository.InsertBatchMechaBeastsGalleryAsync(userId, mechaBeasts);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<MechaBeasts> GetMechaBeastCollectionByIdAsync(string userId, string mechaBeastId)
    {
        var result = await _mechaBeastsGalleryRepository.GetMechaBeastCollectionByIdAsync(userId, mechaBeastId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateMechaBeastGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _mechaBeastsService.IsMechaBeastDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        await _mechaBeastsGalleryRepository.UpdateMechaBeastGalleryPowerAsync(userId, Id, await _service.GetMechaBeastByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
