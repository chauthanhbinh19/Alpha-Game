using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CollaborationEquipmentsGalleryService : ICollaborationEquipmentsGalleryService
{
    private readonly ICollaborationEquipmentsGalleryRepository _collaborationEquipmentsGalleryRepository;
    private readonly ICollaborationEquipmentsService _collaborationEquipmentsService;
    private readonly IPowerManagerService _powerManagerService;

    public CollaborationEquipmentsGalleryService(
        ICollaborationEquipmentsGalleryRepository collaborationEquipmentsGalleryRepository,
        ICollaborationEquipmentsService collaborationEquipmentsService,
        IPowerManagerService powerManagerService)
    {
        _collaborationEquipmentsGalleryRepository = collaborationEquipmentsGalleryRepository;
        _collaborationEquipmentsService = collaborationEquipmentsService;
        _powerManagerService = powerManagerService;
    }

    public static ICollaborationEquipmentsGalleryService Create() => ServiceContainer.GetService<ICollaborationEquipmentsGalleryService>();

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _collaborationEquipmentsGalleryRepository.GetCollaborationEquipmentsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare)
    {
        return await _collaborationEquipmentsGalleryRepository.GetCollaborationEquipmentsCountAsync(search, type, rare);
    }

    public async Task<bool> InsertCollaborationEquipmentGalleryAsync(string userId, string Id)
    {
        var insertResult = await _collaborationEquipmentsGalleryRepository.InsertCollaborationEquipmentGalleryAsync(userId, Id, await _collaborationEquipmentsService.GetCollaborationEquipmentByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCollaborationEquipmentGalleryAsync(string userId, string collaborationEquipmentId)
    {
        var updateResult = await _collaborationEquipmentsGalleryRepository.UpdateStatusCollaborationEquipmentGalleryAsync(userId, collaborationEquipmentId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CollaborationEquipments collaborationEquipmentGallery = await GetCollaborationEquipmentCollectionByIdAsync(userId, collaborationEquipmentId) ?? new CollaborationEquipments();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)collaborationEquipmentGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCollaborationEquipmentsGalleryAsync(string userId)
    {
        CollaborationEquipments oldCollaborationEquipment = await SumPowerCollaborationEquipmentsGalleryAsync(userId);

        var updateResult = await _collaborationEquipmentsGalleryRepository.UpdateBatchStatusCollaborationEquipmentsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CollaborationEquipments newCollaborationEquipment = await SumPowerCollaborationEquipmentsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCollaborationEquipment - (PowerManager)oldCollaborationEquipment;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync(string userId)
    {
        return await _collaborationEquipmentsGalleryRepository.SumPowerCollaborationEquipmentsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarCollaborationEquipmentGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _collaborationEquipmentsGalleryRepository.UpdateTempStarCollaborationEquipmentGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCollaborationEquipmentGalleryAsync(string userId, string collaborationEquipmentId)
    {
        CollaborationEquipments oldCollaborationEquipment = await GetCollaborationEquipmentCollectionByIdAsync(userId, collaborationEquipmentId) ?? new CollaborationEquipments();

        var updateResult = await _collaborationEquipmentsGalleryRepository.UpdateCurrentStarCollaborationEquipmentGalleryAsync(userId, collaborationEquipmentId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CollaborationEquipments newCollaborationEquipment = await GetCollaborationEquipmentCollectionByIdAsync(userId, collaborationEquipmentId) ?? new CollaborationEquipments();
        PowerManager deltaPower = (PowerManager)newCollaborationEquipment - (PowerManager)oldCollaborationEquipment;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCollaborationEquipmentsGalleryAsync(string userId)
    {
        CollaborationEquipments oldCollaborationEquipment = await SumPowerCollaborationEquipmentsGalleryAsync(userId);

        var updateResult = await _collaborationEquipmentsGalleryRepository.UpdateBatchCurrentStarCollaborationEquipmentsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CollaborationEquipments newCollaborationEquipment = await SumPowerCollaborationEquipmentsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCollaborationEquipment - (PowerManager)oldCollaborationEquipment;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCollaborationEquipmentsGalleryAsync(string userId, List<CollaborationEquipments> collaborationEquipments)
    {
        var insertResult = await _collaborationEquipmentsGalleryRepository.InsertBatchCollaborationEquipmentsGalleryAsync(userId, collaborationEquipments);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CollaborationEquipments> GetCollaborationEquipmentCollectionByIdAsync(string userId, string collaborationEquipmentId)
    {
        var result = await _collaborationEquipmentsGalleryRepository.GetCollaborationEquipmentCollectionByIdAsync(userId, collaborationEquipmentId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCollaborationEquipmentGalleryPowerAsync(string userId, string Id)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        await _collaborationEquipmentsGalleryRepository.UpdateCollaborationEquipmentGalleryPowerAsync(userId, Id, await _service.GetCollaborationEquipmentByIdAsync(Id));
    }
}
