using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EquipmentsGalleryService : IEquipmentsGalleryService
{
    private readonly IEquipmentsGalleryRepository _equipmentsGalleryRepository;
    private readonly IEquipmentsService _equipmentsService;
    private readonly IPowerManagerService _powerManagerService;

    public EquipmentsGalleryService(
        IEquipmentsGalleryRepository equipmentsGalleryRepository,
        IEquipmentsService equipmentsService,
        IPowerManagerService powerManagerService)
    {
        _equipmentsGalleryRepository = equipmentsGalleryRepository;
        _equipmentsService = equipmentsService;
        _powerManagerService = powerManagerService;
    }

    public static IEquipmentsGalleryService Create() => ServiceContainer.GetService<IEquipmentsGalleryService>();

    public async Task<List<Equipments>> GetEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Equipments> list = await _equipmentsGalleryRepository.GetEquipmentsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEquipmentsCountAsync(string search, string type, string rare)
    {
        return await _equipmentsGalleryRepository.GetEquipmentsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertEquipmentGalleryAsync(string userId, string Id)
    {
        var checkResult = await _equipmentsService.IsEquipmentDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _equipmentsGalleryRepository.InsertEquipmentGalleryAsync(userId, Id, await _equipmentsService.GetEquipmentByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusEquipmentGalleryAsync(string userId, string equipmentId)
    {
        var checkResult = await _equipmentsService.IsEquipmentDeletedOrInactiveAsync(equipmentId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _equipmentsGalleryRepository.UpdateStatusEquipmentGalleryAsync(userId, equipmentId);

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
        Equipments equipmentGallery = await GetEquipmentCollectionByIdAsync(userId, equipmentId) ?? new Equipments();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)equipmentGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusEquipmentsGalleryAsync(string userId)
    {
        Equipments oldEquipment = await SumPowerEquipmentsGalleryAsync(userId);

        var updateResult = await _equipmentsGalleryRepository.UpdateBatchStatusEquipmentsGalleryAsync(userId);

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

        Equipments newEquipment = await SumPowerEquipmentsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newEquipment - (PowerManager)oldEquipment;

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

    public async Task<Equipments> SumPowerEquipmentsGalleryAsync(string userId)
    {
        return await _equipmentsGalleryRepository.SumPowerEquipmentsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarEquipmentGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _equipmentsService.IsEquipmentDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _equipmentsGalleryRepository.UpdateTempStarEquipmentGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarEquipmentGalleryAsync(string userId, string equipmentId)
    {
        var checkResult = await _equipmentsService.IsEquipmentDeletedOrInactiveAsync(equipmentId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Equipments oldEquipment = await GetEquipmentCollectionByIdAsync(userId, equipmentId) ?? new Equipments();

        var updateResult = await _equipmentsGalleryRepository.UpdateCurrentStarEquipmentGalleryAsync(userId, equipmentId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Equipments newEquipment = await GetEquipmentCollectionByIdAsync(userId, equipmentId) ?? new Equipments();
        PowerManager deltaPower = (PowerManager)newEquipment - (PowerManager)oldEquipment;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarEquipmentsGalleryAsync(string userId)
    {
        Equipments oldEquipment = await SumPowerEquipmentsGalleryAsync(userId);

        var updateResult = await _equipmentsGalleryRepository.UpdateBatchCurrentStarEquipmentsGalleryAsync(userId);

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

        Equipments newEquipment = await SumPowerEquipmentsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newEquipment - (PowerManager)oldEquipment;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchEquipmentsGalleryAsync(string userId, List<Equipments> equipments)
    {
        var insertResult = await _equipmentsGalleryRepository.InsertBatchEquipmentsGalleryAsync(userId, equipments);

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

    public async Task<Equipments> GetEquipmentCollectionByIdAsync(string userId, string equipmentId)
    {
        var result = await _equipmentsGalleryRepository.GetEquipmentCollectionByIdAsync(userId, equipmentId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateEquipmentGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _equipmentsService.IsEquipmentDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        await _equipmentsGalleryRepository.UpdateEquipmentGalleryPowerAsync(userId, Id, await _service.GetEquipmentByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
