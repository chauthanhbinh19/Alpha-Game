using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PuppetsGalleryService : IPuppetsGalleryService
{
    private readonly IPuppetsGalleryRepository _puppetsGalleryRepository;
    private readonly IPuppetsService _puppetsService;
    private readonly IPowerManagerService _powerManagerService;

    public PuppetsGalleryService(
        IPuppetsGalleryRepository puppetsGalleryRepository,
        IPuppetsService puppetsService,
        IPowerManagerService powerManagerService)
    {
        _puppetsGalleryRepository = puppetsGalleryRepository;
        _puppetsService = puppetsService;
        _powerManagerService = powerManagerService;
    }

    public static IPuppetsGalleryService Create() => ServiceContainer.GetService<IPuppetsGalleryService>();

    public async Task<List<Puppets>> GetPuppetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _puppetsGalleryRepository.GetPuppetsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPuppetsCountAsync(string search, string type, string rare)
    {
        return await _puppetsGalleryRepository.GetPuppetsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertPuppetGalleryAsync(string userId, string Id)
    {
        var checkResult = await _puppetsService.IsPuppetDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _puppetsGalleryRepository.InsertPuppetGalleryAsync(userId, Id, await _puppetsService.GetPuppetByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusPuppetGalleryAsync(string userId, string puppetId)
    {
        var checkResult = await _puppetsService.IsPuppetDeletedOrInactiveAsync(puppetId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _puppetsGalleryRepository.UpdateStatusPuppetGalleryAsync(userId, puppetId);

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
        Puppets puppetGallery = await GetPuppetCollectionByIdAsync(userId, puppetId) ?? new Puppets();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)puppetGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusPuppetsGalleryAsync(string userId)
    {
        Puppets oldPuppet = await SumPowerPuppetsGalleryAsync(userId);

        var updateResult = await _puppetsGalleryRepository.UpdateBatchStatusPuppetsGalleryAsync(userId);

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

        Puppets newPuppet = await SumPowerPuppetsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newPuppet - (PowerManager)oldPuppet;

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

    public async Task<Puppets> SumPowerPuppetsGalleryAsync(string userId)
    {
        return await _puppetsGalleryRepository.SumPowerPuppetsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarPuppetGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _puppetsService.IsPuppetDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _puppetsGalleryRepository.UpdateTempStarPuppetGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarPuppetGalleryAsync(string userId, string puppetId)
    {
        var checkResult = await _puppetsService.IsPuppetDeletedOrInactiveAsync(puppetId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Puppets oldPuppet = await GetPuppetCollectionByIdAsync(userId, puppetId) ?? new Puppets();

        var updateResult = await _puppetsGalleryRepository.UpdateCurrentStarPuppetGalleryAsync(userId, puppetId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Puppets newPuppet = await GetPuppetCollectionByIdAsync(userId, puppetId) ?? new Puppets();
        PowerManager deltaPower = (PowerManager)newPuppet - (PowerManager)oldPuppet;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarPuppetsGalleryAsync(string userId)
    {
        Puppets oldPuppet = await SumPowerPuppetsGalleryAsync(userId);

        var updateResult = await _puppetsGalleryRepository.UpdateBatchCurrentStarPuppetsGalleryAsync(userId);

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

        Puppets newPuppet = await SumPowerPuppetsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newPuppet - (PowerManager)oldPuppet;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchPuppetsGalleryAsync(string userId, List<Puppets> puppets)
    {
        var insertResult = await _puppetsGalleryRepository.InsertBatchPuppetsGalleryAsync(userId, puppets);

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

    public async Task<Puppets> GetPuppetCollectionByIdAsync(string userId, string puppetId)
    {
        var result = await _puppetsGalleryRepository.GetPuppetCollectionByIdAsync(userId, puppetId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdatePuppetGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _puppetsService.IsPuppetDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        await _puppetsGalleryRepository.UpdatePuppetGalleryPowerAsync(userId, Id, await _service.GetPuppetByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
