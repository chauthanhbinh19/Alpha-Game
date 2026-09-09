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

    public async Task<InsertOrUpdateResult<bool>> InsertCollaborationGalleryAsync(string userId, string Id)
    {
        var checkResult = await _collaborationsService.IsCollaborationDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _collaborationsGalleryRepository.InsertCollaborationGalleryAsync(userId, Id, await _collaborationsService.GetCollaborationByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusCollaborationGalleryAsync(string userId, string collaborationId)
    {
        var checkResult = await _collaborationsService.IsCollaborationDeletedOrInactiveAsync(collaborationId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _collaborationsGalleryRepository.UpdateStatusCollaborationGalleryAsync(userId, collaborationId);

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
        Collaborations collaborationGallery = await GetCollaborationCollectionByIdAsync(userId, collaborationId) ?? new Collaborations();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)collaborationGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCollaborationsGalleryAsync(string userId)
    {
        Collaborations oldCollaboration = await SumPowerCollaborationsGalleryAsync(userId);

        var updateResult = await _collaborationsGalleryRepository.UpdateBatchStatusCollaborationsGalleryAsync(userId);

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

        Collaborations newCollaboration = await SumPowerCollaborationsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCollaboration - (PowerManager)oldCollaboration;

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

    public async Task<Collaborations> SumPowerCollaborationsGalleryAsync(string userId)
    {
        return await _collaborationsGalleryRepository.SumPowerCollaborationsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarCollaborationGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _collaborationsService.IsCollaborationDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _collaborationsGalleryRepository.UpdateTempStarCollaborationGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCollaborationGalleryAsync(string userId, string collaborationId)
    {
        var checkResult = await _collaborationsService.IsCollaborationDeletedOrInactiveAsync(collaborationId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Collaborations oldCollaboration = await GetCollaborationCollectionByIdAsync(userId, collaborationId) ?? new Collaborations();

        var updateResult = await _collaborationsGalleryRepository.UpdateCurrentStarCollaborationGalleryAsync(userId, collaborationId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Collaborations newCollaboration = await GetCollaborationCollectionByIdAsync(userId, collaborationId) ?? new Collaborations();
        PowerManager deltaPower = (PowerManager)newCollaboration - (PowerManager)oldCollaboration;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCollaborationsGalleryAsync(string userId)
    {
        Collaborations oldCollaboration = await SumPowerCollaborationsGalleryAsync(userId);

        var updateResult = await _collaborationsGalleryRepository.UpdateBatchCurrentStarCollaborationsGalleryAsync(userId);

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

        Collaborations newCollaboration = await SumPowerCollaborationsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCollaboration - (PowerManager)oldCollaboration;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchCollaborationsGalleryAsync(string userId, List<Collaborations> collaborations)
    {
        var insertResult = await _collaborationsGalleryRepository.InsertBatchCollaborationsGalleryAsync(userId, collaborations);

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

    public async Task<Collaborations> GetCollaborationCollectionByIdAsync(string userId, string collaborationId)
    {
        var result = await _collaborationsGalleryRepository.GetCollaborationCollectionByIdAsync(userId, collaborationId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCollaborationGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _collaborationsService.IsCollaborationDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ICollaborationsRepository _repository = new CollaborationsRepository();
        CollaborationsService _service = new CollaborationsService(_repository);
        await _collaborationsGalleryRepository.UpdateCollaborationGalleryPowerAsync(userId, Id, await _service.GetCollaborationByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
