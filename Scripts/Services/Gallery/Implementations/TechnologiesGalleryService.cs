using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TechnologiesGalleryService : ITechnologiesGalleryService
{
    private readonly ITechnologiesGalleryRepository _technologiesGalleryRepository;
    private readonly ITechnologiesService _technologiesService;
    private readonly IPowerManagerService _powerManagerService;

    public TechnologiesGalleryService(
        ITechnologiesGalleryRepository technologiesGalleryRepository,
        ITechnologiesService technologiesService,
        IPowerManagerService powerManagerService)
    {
        _technologiesGalleryRepository = technologiesGalleryRepository;
        _technologiesService = technologiesService;
        _powerManagerService = powerManagerService;
    }

    public static ITechnologiesGalleryService Create() => ServiceContainer.GetService<ITechnologiesGalleryService>();

    public async Task<List<Technologies>> GetTechnologiesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _technologiesGalleryRepository.GetTechnologiesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesCountAsync(string search, string rare)
    {
        return await _technologiesGalleryRepository.GetTechnologiesCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertTechnologyGalleryAsync(string userId, string Id)
    {
        var checkResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _technologiesGalleryRepository.InsertTechnologyGalleryAsync(userId, Id, await _technologiesService.GetTechnologyByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusTechnologyGalleryAsync(string userId, string technologyId)
    {
        var checkResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(technologyId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _technologiesGalleryRepository.UpdateStatusTechnologyGalleryAsync(userId, technologyId);

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
        Technologies technologyGallery = await GetTechnologyCollectionByIdAsync(userId, technologyId) ?? new Technologies();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)technologyGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTechnologiesGalleryAsync(string userId)
    {
        Technologies oldTechnology = await SumPowerTechnologiesGalleryAsync(userId);

        var updateResult = await _technologiesGalleryRepository.UpdateBatchStatusTechnologiesGalleryAsync(userId);

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

        Technologies newTechnology = await SumPowerTechnologiesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTechnology - (PowerManager)oldTechnology;

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

    public async Task<Technologies> SumPowerTechnologiesGalleryAsync(string userId)
    {
        return await _technologiesGalleryRepository.SumPowerTechnologiesGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarTechnologyGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _technologiesGalleryRepository.UpdateTempStarTechnologyGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarTechnologyGalleryAsync(string userId, string technologyId)
    {
        var checkResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(technologyId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Technologies oldTechnology = await GetTechnologyCollectionByIdAsync(userId, technologyId) ?? new Technologies();

        var updateResult = await _technologiesGalleryRepository.UpdateCurrentStarTechnologyGalleryAsync(userId, technologyId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Technologies newTechnology = await GetTechnologyCollectionByIdAsync(userId, technologyId) ?? new Technologies();
        PowerManager deltaPower = (PowerManager)newTechnology - (PowerManager)oldTechnology;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarTechnologiesGalleryAsync(string userId)
    {
        Technologies oldTechnology = await SumPowerTechnologiesGalleryAsync(userId);

        var updateResult = await _technologiesGalleryRepository.UpdateBatchCurrentStarTechnologiesGalleryAsync(userId);

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

        Technologies newTechnology = await SumPowerTechnologiesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTechnology - (PowerManager)oldTechnology;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchTechnologiesGalleryAsync(string userId, List<Technologies> technologies)
    {
        var insertResult = await _technologiesGalleryRepository.InsertBatchTechnologiesGalleryAsync(userId, technologies);

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

    public async Task<Technologies> GetTechnologyCollectionByIdAsync(string userId, string technologyId)
    {
        var result = await _technologiesGalleryRepository.GetTechnologyCollectionByIdAsync(userId, technologyId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTechnologyGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _technologiesService.IsTechnologyDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        await _technologiesGalleryRepository.UpdateTechnologyGalleryPowerAsync(userId, Id, await _service.GetTechnologyByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
