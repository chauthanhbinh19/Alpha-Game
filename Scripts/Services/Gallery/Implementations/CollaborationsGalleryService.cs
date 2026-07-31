using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CollaborationsGalleryService : ICollaborationsGalleryService
{
    private readonly ICollaborationsGalleryRepository _collaborationsGalleryRepository;
    private readonly ICollaborationsService _collaborationsService;
    private readonly IPowerManagerService _powerManagerService;

    public CollaborationsGalleryService(
        ICollaborationsGalleryRepository collaborationsGalleryRepository,
        ICollaborationsService collaborationsService,
        IPowerManagerService powerManagerService)
    {
        _collaborationsGalleryRepository = collaborationsGalleryRepository;
        _collaborationsService = collaborationsService;
        _powerManagerService = powerManagerService;
    }

    public static ICollaborationsGalleryService Create() => ServiceContainer.GetService<ICollaborationsGalleryService>();

    public async Task<List<Collaborations>> GetCollaborationsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Collaborations> list = await _collaborationsGalleryRepository.GetCollaborationsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationsCountAsync(string search, string rare)
    {
        return await _collaborationsGalleryRepository.GetCollaborationsCountAsync(search, rare);
    }

    public async Task<bool> InsertCollaborationGalleryAsync(string userId, string Id)
    {
        var insertResult = await _collaborationsGalleryRepository.InsertCollaborationGalleryAsync(userId, Id, await _collaborationsService.GetCollaborationByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCollaborationGalleryAsync(string userId, string collaborationId)
    {
        var updateResult = await _collaborationsGalleryRepository.UpdateStatusCollaborationGalleryAsync(userId, collaborationId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Collaborations collaborationGallery = await GetCollaborationCollectionByIdAsync(userId, collaborationId) ?? new Collaborations();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)collaborationGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCollaborationsGalleryAsync(string userId)
    {
        Collaborations oldCollaboration = await SumPowerCollaborationsGalleryAsync(userId);

        var updateResult = await _collaborationsGalleryRepository.UpdateBatchStatusCollaborationsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Collaborations newCollaboration = await SumPowerCollaborationsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCollaboration - (PowerManager)oldCollaboration;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Collaborations> SumPowerCollaborationsGalleryAsync(string userId)
    {
        return await _collaborationsGalleryRepository.SumPowerCollaborationsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarCollaborationGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _collaborationsGalleryRepository.UpdateTempStarCollaborationGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCollaborationGalleryAsync(string userId, string collaborationId)
    {
        Collaborations oldCollaboration = await GetCollaborationCollectionByIdAsync(userId, collaborationId) ?? new Collaborations();

        var updateResult = await _collaborationsGalleryRepository.UpdateCurrentStarCollaborationGalleryAsync(userId, collaborationId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Collaborations newCollaboration = await GetCollaborationCollectionByIdAsync(userId, collaborationId) ?? new Collaborations();
        PowerManager deltaPower = (PowerManager)newCollaboration - (PowerManager)oldCollaboration;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCollaborationsGalleryAsync(string userId)
    {
        Collaborations oldCollaboration = await SumPowerCollaborationsGalleryAsync(userId);

        var updateResult = await _collaborationsGalleryRepository.UpdateBatchCurrentStarCollaborationsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Collaborations newCollaboration = await SumPowerCollaborationsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCollaboration - (PowerManager)oldCollaboration;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCollaborationsGalleryAsync(string userId, List<Collaborations> collaborations)
    {
        var insertResult = await _collaborationsGalleryRepository.InsertBatchCollaborationsGalleryAsync(userId, collaborations);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Collaborations> GetCollaborationCollectionByIdAsync(string userId, string collaborationId)
    {
        var result = await _collaborationsGalleryRepository.GetCollaborationCollectionByIdAsync(userId, collaborationId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCollaborationGalleryPowerAsync(string userId, string Id)
    {
        ICollaborationsRepository _repository = new CollaborationsRepository();
        CollaborationsService _service = new CollaborationsService(_repository);
        await _collaborationsGalleryRepository.UpdateCollaborationGalleryPowerAsync(userId, Id, await _service.GetCollaborationByIdAsync(Id));
    }
}
