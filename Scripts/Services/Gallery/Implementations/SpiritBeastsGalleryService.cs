using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpiritBeastsGalleryService : ISpiritBeastsGalleryService
{
    private readonly ISpiritBeastsGalleryRepository _spiritBeastsGalleryRepository;
    private readonly ISpiritBeastsService _spiritBeastsService;
    private readonly IPowerManagerService _powerManagerService;

    public SpiritBeastsGalleryService(
        ISpiritBeastsGalleryRepository spiritBeastsGalleryRepository,
        ISpiritBeastsService spiritBeastsService,
        IPowerManagerService powerManagerService)
    {
        _spiritBeastsGalleryRepository = spiritBeastsGalleryRepository;
        _spiritBeastsService = spiritBeastsService;
        _powerManagerService = powerManagerService;
    }

    public static ISpiritBeastsGalleryService Create() => ServiceContainer.GetService<ISpiritBeastsGalleryService>();

    public async Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _spiritBeastsGalleryRepository.GetSpiritBeastsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsCountAsync(string search, string rare)
    {
        return await _spiritBeastsGalleryRepository.GetSpiritBeastsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertSpiritBeastGalleryAsync(string userId, string Id)
    {
        var checkResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _spiritBeastsGalleryRepository.InsertSpiritBeastGalleryAsync(userId, Id, await _spiritBeastsService.GetSpiritBeastByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusSpiritBeastGalleryAsync(string userId, string spiritBeastId)
    {
        var checkResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(spiritBeastId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _spiritBeastsGalleryRepository.UpdateStatusSpiritBeastGalleryAsync(userId, spiritBeastId);

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
        SpiritBeasts spiritBeastGallery = await GetSpiritBeastCollectionByIdAsync(userId, spiritBeastId) ?? new SpiritBeasts();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)spiritBeastGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSpiritBeastsGalleryAsync(string userId)
    {
        SpiritBeasts oldSpiritBeast = await SumPowerSpiritBeastsGalleryAsync(userId);

        var updateResult = await _spiritBeastsGalleryRepository.UpdateBatchStatusSpiritBeastsGalleryAsync(userId);

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

        SpiritBeasts newSpiritBeast = await SumPowerSpiritBeastsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritBeast - (PowerManager)oldSpiritBeast;

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

    public async Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync(string userId)
    {
        return await _spiritBeastsGalleryRepository.SumPowerSpiritBeastsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarSpiritBeastGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _spiritBeastsGalleryRepository.UpdateTempStarSpiritBeastGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarSpiritBeastGalleryAsync(string userId, string spiritBeastId)
    {
        var checkResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(spiritBeastId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        SpiritBeasts oldSpiritBeast = await GetSpiritBeastCollectionByIdAsync(userId, spiritBeastId) ?? new SpiritBeasts();

        var updateResult = await _spiritBeastsGalleryRepository.UpdateCurrentStarSpiritBeastGalleryAsync(userId, spiritBeastId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        SpiritBeasts newSpiritBeast = await GetSpiritBeastCollectionByIdAsync(userId, spiritBeastId) ?? new SpiritBeasts();
        PowerManager deltaPower = (PowerManager)newSpiritBeast - (PowerManager)oldSpiritBeast;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarSpiritBeastsGalleryAsync(string userId)
    {
        SpiritBeasts oldSpiritBeast = await SumPowerSpiritBeastsGalleryAsync(userId);

        var updateResult = await _spiritBeastsGalleryRepository.UpdateBatchCurrentStarSpiritBeastsGalleryAsync(userId);

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

        SpiritBeasts newSpiritBeast = await SumPowerSpiritBeastsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritBeast - (PowerManager)oldSpiritBeast;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchSpiritBeastsGalleryAsync(string userId, List<SpiritBeasts> spiritBeasts)
    {
        var insertResult = await _spiritBeastsGalleryRepository.InsertBatchSpiritBeastsGalleryAsync(userId, spiritBeasts);

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

    public async Task<SpiritBeasts> GetSpiritBeastCollectionByIdAsync(string userId, string spiritBeastId)
    {
        var result = await _spiritBeastsGalleryRepository.GetSpiritBeastCollectionByIdAsync(userId, spiritBeastId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateSpiritBeastGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _spiritBeastsService.IsSpiritBeastDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        await _spiritBeastsGalleryRepository.UpdateSpiritBeastGalleryPowerAsync(userId, Id, await _service.GetSpiritBeastByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
