using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ArchitecturesGalleryService : IArchitecturesGalleryService
{
    private readonly IArchitecturesGalleryRepository _architecturesGalleryRepository;
    private readonly IArchitecturesService _architecturesService;
    private readonly IPowerManagerService _powerManagerService;

    public ArchitecturesGalleryService(
        IArchitecturesGalleryRepository architecturesGalleryRepository,
        IArchitecturesService architecturesService,
        IPowerManagerService powerManagerService)
    {
        _architecturesGalleryRepository = architecturesGalleryRepository;
        _architecturesService = architecturesService;
        _powerManagerService = powerManagerService;
    }

    public static IArchitecturesGalleryService Create() => ServiceContainer.GetService<IArchitecturesGalleryService>();

    public async Task<List<Architectures>> GetArchitecturesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Architectures> list = await _architecturesGalleryRepository.GetArchitecturesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArchitecturesCountAsync(string search, string rare)
    {
        return await _architecturesGalleryRepository.GetArchitecturesCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertArchitectureGalleryAsync(string userId, string Id)
    {
        var checkResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _architecturesGalleryRepository.InsertArchitectureGalleryAsync(userId, Id, await _architecturesService.GetArchitectureByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusArchitectureGalleryAsync(string userId, string architectureId)
    {
        var checkResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(architectureId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _architecturesGalleryRepository.UpdateStatusArchitectureGalleryAsync(userId, architectureId);

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
        Architectures architectureGallery = await GetArchitectureCollectionByIdAsync(userId, architectureId) ?? new Architectures();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)architectureGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArchitecturesGalleryAsync(string userId)
    {
        Architectures oldArchitecture = await SumPowerArchitecturesGalleryAsync(userId);

        var updateResult = await _architecturesGalleryRepository.UpdateBatchStatusArchitecturesGalleryAsync(userId);

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

        Architectures newArchitecture = await SumPowerArchitecturesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newArchitecture - (PowerManager)oldArchitecture;

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

    public async Task<Architectures> SumPowerArchitecturesGalleryAsync(string userId)
    {
        return await _architecturesGalleryRepository.SumPowerArchitecturesGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarArchitectureGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _architecturesGalleryRepository.UpdateTempStarArchitectureGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarArchitectureGalleryAsync(string userId, string architectureId)
    {
        var checkResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(architectureId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Architectures oldArchitecture = await GetArchitectureCollectionByIdAsync(userId, architectureId) ?? new Architectures();

        var updateResult = await _architecturesGalleryRepository.UpdateCurrentStarArchitectureGalleryAsync(userId, architectureId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Architectures newArchitecture = await GetArchitectureCollectionByIdAsync(userId, architectureId) ?? new Architectures();
        PowerManager deltaPower = (PowerManager)newArchitecture - (PowerManager)oldArchitecture;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarArchitecturesGalleryAsync(string userId)
    {
        Architectures oldArchitecture = await SumPowerArchitecturesGalleryAsync(userId);

        var updateResult = await _architecturesGalleryRepository.UpdateBatchCurrentStarArchitecturesGalleryAsync(userId);

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

        Architectures newArchitecture = await SumPowerArchitecturesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newArchitecture - (PowerManager)oldArchitecture;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchArchitecturesGalleryAsync(string userId, List<Architectures> architectures)
    {
        var insertResult = await _architecturesGalleryRepository.InsertBatchArchitecturesGalleryAsync(userId, architectures);

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

    public async Task<Architectures> GetArchitectureCollectionByIdAsync(string userId, string architectureId)
    {
        var result = await _architecturesGalleryRepository.GetArchitectureCollectionByIdAsync(userId, architectureId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateArchitectureGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _architecturesService.IsArchitectureDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        await _architecturesGalleryRepository.UpdateArchitectureGalleryPowerAsync(userId, Id, await _service.GetArchitectureByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
