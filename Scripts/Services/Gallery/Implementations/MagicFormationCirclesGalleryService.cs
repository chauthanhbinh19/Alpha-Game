using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MagicFormationCirclesGalleryService : IMagicFormationCirclesGalleryService
{
    private readonly IMagicFormationCirclesGalleryRepository _magicFormationCirclesGalleryRepository;
    private readonly IMagicFormationCirclesService _magicFormationCirclesService;
    private readonly IPowerManagerService _powerManagerService;

    public MagicFormationCirclesGalleryService(
        IMagicFormationCirclesGalleryRepository magicFormationCirclesGalleryRepository,
        IMagicFormationCirclesService magicFormationCirclesService,
        IPowerManagerService powerManagerService)
    {
        _magicFormationCirclesGalleryRepository = magicFormationCirclesGalleryRepository;
        _magicFormationCirclesService = magicFormationCirclesService;
        _powerManagerService = powerManagerService;
    }

    public static IMagicFormationCirclesGalleryService Create() => ServiceContainer.GetService<IMagicFormationCirclesGalleryService>();

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _magicFormationCirclesGalleryRepository.GetMagicFormationCirclesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare)
    {
        return await _magicFormationCirclesGalleryRepository.GetMagicFormationCirclesCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertMagicFormationCircleGalleryAsync(string userId, string Id)
    {
        var checkResult = await _magicFormationCirclesService.IsMagicFormationCircleDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _magicFormationCirclesGalleryRepository.InsertMagicFormationCircleGalleryAsync(userId, Id, await _magicFormationCirclesService.GetMagicFormationCircleByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusMagicFormationCircleGalleryAsync(string userId, string magicFormationCircleId)
    {
        var checkResult = await _magicFormationCirclesService.IsMagicFormationCircleDeletedOrInactiveAsync(magicFormationCircleId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateStatusMagicFormationCircleGalleryAsync(userId, magicFormationCircleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await PowerManagerService.Create().GetUserStatsAsync(userId);
        MagicFormationCircles magicFormationCircleGallery = await GetMagicFormationCircleCollectionByIdAsync(userId, magicFormationCircleId) ?? new MagicFormationCircles();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)magicFormationCircleGallery;

        await PowerManagerService.Create().UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMagicFormationCirclesGalleryAsync(string userId)
    {
        MagicFormationCircles oldMagicFormationCircle = await SumPowerMagicFormationCirclesGalleryAsync(userId);

        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateBatchStatusMagicFormationCirclesGalleryAsync(userId);

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

        MagicFormationCircles newMagicFormationCircle = await SumPowerMagicFormationCirclesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMagicFormationCircle - (PowerManager)oldMagicFormationCircle;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await PowerManagerService.Create().GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await PowerManagerService.Create().UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync(string userId)
    {
        return await _magicFormationCirclesGalleryRepository.SumPowerMagicFormationCirclesGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarMagicFormationCircleGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _magicFormationCirclesService.IsMagicFormationCircleDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateTempStarMagicFormationCircleGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarMagicFormationCircleGalleryAsync(string userId, string magicFormationCircleId)
    {
        var checkResult = await _magicFormationCirclesService.IsMagicFormationCircleDeletedOrInactiveAsync(magicFormationCircleId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        MagicFormationCircles oldMagicFormationCircle = await GetMagicFormationCircleCollectionByIdAsync(userId, magicFormationCircleId) ?? new MagicFormationCircles();

        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateCurrentStarMagicFormationCircleGalleryAsync(userId, magicFormationCircleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        MagicFormationCircles newMagicFormationCircle = await GetMagicFormationCircleCollectionByIdAsync(userId, magicFormationCircleId) ?? new MagicFormationCircles();
        PowerManager deltaPower = (PowerManager)newMagicFormationCircle - (PowerManager)oldMagicFormationCircle;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await PowerManagerService.Create().GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await PowerManagerService.Create().UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarMagicFormationCirclesGalleryAsync(string userId)
    {
        MagicFormationCircles oldMagicFormationCircle = await SumPowerMagicFormationCirclesGalleryAsync(userId);

        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateBatchCurrentStarMagicFormationCirclesGalleryAsync(userId);

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

        MagicFormationCircles newMagicFormationCircle = await SumPowerMagicFormationCirclesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMagicFormationCircle - (PowerManager)oldMagicFormationCircle;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await PowerManagerService.Create().GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await PowerManagerService.Create().UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchMagicFormationCirclesGalleryAsync(string userId, List<MagicFormationCircles> magicFormationCircles)
    {
        var insertResult = await _magicFormationCirclesGalleryRepository.InsertBatchMagicFormationCirclesGalleryAsync(userId, magicFormationCircles);

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

    public async Task<MagicFormationCircles> GetMagicFormationCircleCollectionByIdAsync(string userId, string magicFormationCircleId)
    {
        var result = await _magicFormationCirclesGalleryRepository.GetMagicFormationCircleCollectionByIdAsync(userId, magicFormationCircleId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateMagicFormationCircleGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _magicFormationCirclesService.IsMagicFormationCircleDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        await _magicFormationCirclesGalleryRepository.UpdateMagicFormationCircleGalleryPowerAsync(userId, Id, await _service.GetMagicFormationCircleByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
