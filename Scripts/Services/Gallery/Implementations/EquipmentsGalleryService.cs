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

    public async Task<bool> InsertEquipmentGalleryAsync(string userId, string Id)
    {
        var insertResult = await _equipmentsGalleryRepository.InsertEquipmentGalleryAsync(userId, Id, await _equipmentsService.GetEquipmentByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusEquipmentGalleryAsync(string userId, string equipmentId)
    {
        var updateResult = await _equipmentsGalleryRepository.UpdateStatusEquipmentGalleryAsync(userId, equipmentId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Equipments equipmentGallery = await GetEquipmentCollectionByIdAsync(userId, equipmentId) ?? new Equipments();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)equipmentGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusEquipmentsGalleryAsync(string userId)
    {
        Equipments oldEquipment = await SumPowerEquipmentsGalleryAsync(userId);

        var updateResult = await _equipmentsGalleryRepository.UpdateBatchStatusEquipmentsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Equipments newEquipment = await SumPowerEquipmentsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newEquipment - (PowerManager)oldEquipment;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Equipments> SumPowerEquipmentsGalleryAsync(string userId)
    {
        return await _equipmentsGalleryRepository.SumPowerEquipmentsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarEquipmentGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _equipmentsGalleryRepository.UpdateTempStarEquipmentGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarEquipmentGalleryAsync(string userId, string equipmentId)
    {
        Equipments oldEquipment = await GetEquipmentCollectionByIdAsync(userId, equipmentId) ?? new Equipments();

        var updateResult = await _equipmentsGalleryRepository.UpdateCurrentStarEquipmentGalleryAsync(userId, equipmentId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Equipments newEquipment = await GetEquipmentCollectionByIdAsync(userId, equipmentId) ?? new Equipments();
        PowerManager deltaPower = (PowerManager)newEquipment - (PowerManager)oldEquipment;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarEquipmentsGalleryAsync(string userId)
    {
        Equipments oldEquipment = await SumPowerEquipmentsGalleryAsync(userId);

        var updateResult = await _equipmentsGalleryRepository.UpdateBatchCurrentStarEquipmentsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Equipments newEquipment = await SumPowerEquipmentsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newEquipment - (PowerManager)oldEquipment;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchEquipmentsGalleryAsync(string userId, List<Equipments> equipments)
    {
        var insertResult = await _equipmentsGalleryRepository.InsertBatchEquipmentsGalleryAsync(userId, equipments);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Equipments> GetEquipmentCollectionByIdAsync(string userId, string equipmentId)
    {
        var result = await _equipmentsGalleryRepository.GetEquipmentCollectionByIdAsync(userId, equipmentId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateEquipmentGalleryPowerAsync(string userId, string Id)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        await _equipmentsGalleryRepository.UpdateEquipmentGalleryPowerAsync(userId, Id, await _service.GetEquipmentByIdAsync(Id));
    }
}
