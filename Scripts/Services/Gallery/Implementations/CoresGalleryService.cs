using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CoresGalleryService : ICoresGalleryService
{
    private readonly ICoresGalleryRepository _coresGalleryRepository;
    private readonly ICoresService _coresService;
    private readonly IPowerManagerService _powerManagerService;

    public CoresGalleryService(
        ICoresGalleryRepository coresGalleryRepository,
        ICoresService coresService,
        IPowerManagerService powerManagerService)
    {
        _coresGalleryRepository = coresGalleryRepository;
        _coresService = coresService;
        _powerManagerService = powerManagerService;
    }

    public static ICoresGalleryService Create() => ServiceContainer.GetService<ICoresGalleryService>();

    public async Task<List<Cores>> GetCoresCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Cores> list = await _coresGalleryRepository.GetCoresCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCoresCountAsync(string search, string rare)
    {
        return await _coresGalleryRepository.GetCoresCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertCoreGalleryAsync(string userId, string Id)
    {
        var checkResult = await _coresService.IsCoreDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _coresGalleryRepository.InsertCoreGalleryAsync(userId, Id, await _coresService.GetCoreByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusCoreGalleryAsync(string userId, string coreId)
    {
        var checkResult = await _coresService.IsCoreDeletedOrInactiveAsync(coreId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _coresGalleryRepository.UpdateStatusCoreGalleryAsync(userId, coreId);

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
        Cores coreGallery = await GetCoreCollectionByIdAsync(userId, coreId) ?? new Cores();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)coreGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCoresGalleryAsync(string userId)
    {
        Cores oldCore = await SumPowerCoresGalleryAsync(userId);

        var updateResult = await _coresGalleryRepository.UpdateBatchStatusCoresGalleryAsync(userId);

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

        Cores newCore = await SumPowerCoresGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCore - (PowerManager)oldCore;

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

    public async Task<Cores> SumPowerCoresGalleryAsync(string userId)
    {
        return await _coresGalleryRepository.SumPowerCoresGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarCoreGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _coresService.IsCoreDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _coresGalleryRepository.UpdateTempStarCoreGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCoreGalleryAsync(string userId, string coreId)
    {
        var checkResult = await _coresService.IsCoreDeletedOrInactiveAsync(coreId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Cores oldCore = await GetCoreCollectionByIdAsync(userId, coreId) ?? new Cores();

        var updateResult = await _coresGalleryRepository.UpdateCurrentStarCoreGalleryAsync(userId, coreId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Cores newCore = await GetCoreCollectionByIdAsync(userId, coreId) ?? new Cores();
        PowerManager deltaPower = (PowerManager)newCore - (PowerManager)oldCore;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCoresGalleryAsync(string userId)
    {
        Cores oldCore = await SumPowerCoresGalleryAsync(userId);

        var updateResult = await _coresGalleryRepository.UpdateBatchCurrentStarCoresGalleryAsync(userId);

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

        Cores newCore = await SumPowerCoresGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCore - (PowerManager)oldCore;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchCoresGalleryAsync(string userId, List<Cores> cores)
    {
        var insertResult = await _coresGalleryRepository.InsertBatchCoresGalleryAsync(userId, cores);

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

    public async Task<Cores> GetCoreCollectionByIdAsync(string userId, string coreId)
    {
        var result = await _coresGalleryRepository.GetCoreCollectionByIdAsync(userId, coreId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCoreGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _coresService.IsCoreDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        await _coresGalleryRepository.UpdateCoreGalleryPowerAsync(userId, Id, await _service.GetCoreByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
