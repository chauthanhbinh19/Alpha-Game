using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AlchemiesGalleryService : IAlchemiesGalleryService
{
    private readonly IAlchemiesGalleryRepository _alchemiesGalleryRepository;
    private readonly IAlchemiesService _alchemiesService;
    private readonly IPowerManagerService _powerManagerService;

    public AlchemiesGalleryService(
        IAlchemiesGalleryRepository alchemiesGalleryRepository, 
        IAlchemiesService alchemiesService,
        IPowerManagerService powerManagerService)
    {
        _alchemiesGalleryRepository = alchemiesGalleryRepository;
        _alchemiesService = alchemiesService;
        _powerManagerService = powerManagerService;
    }

    public static IAlchemiesGalleryService Create() => ServiceContainer.GetService<IAlchemiesGalleryService>();

    public async Task<List<Alchemies>> GetAlchemiesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _alchemiesGalleryRepository.GetAlchemiesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemyCountAsync(string search, string type, string rare)
    {
        return await _alchemiesGalleryRepository.GetAlchemyCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertAlchemyGalleryAsync(string userId, string Id)
    {
        var checkResult = await _alchemiesService.IsAlchemyDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _alchemiesGalleryRepository.InsertAlchemyGalleryAsync(userId, Id, await _alchemiesService.GetAlchemyByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusAlchemyGalleryAsync(string userId, string alchemyId)
    {
        var checkResult = await _alchemiesService.IsAlchemyDeletedOrInactiveAsync(alchemyId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _alchemiesGalleryRepository.UpdateStatusAlchemyGalleryAsync(userId, alchemyId);

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
        Alchemies alchemyGallery = await GetAlchemyCollectionByIdAsync(userId, alchemyId) ?? new Alchemies();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)alchemyGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAlchemiesGalleryAsync(string userId)
    {
        Alchemies oldAlchemy = await SumPowerAlchemiesGalleryAsync(userId);

        var updateResult = await _alchemiesGalleryRepository.UpdateBatchStatusAlchemiesGalleryAsync(userId);

        if (updateResult == null || 
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Alchemies newAlchemy = await SumPowerAlchemiesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAlchemy - (PowerManager)oldAlchemy;

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

    public async Task<Alchemies> SumPowerAlchemiesGalleryAsync(string userId)
    {
        return await _alchemiesGalleryRepository.SumPowerAlchemyGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarAlchemyGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _alchemiesService.IsAlchemyDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _alchemiesGalleryRepository.UpdateTempStarAlchemyGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarAlchemyGalleryAsync(string userId, string alchemyId)
    {
        var checkResult = await _alchemiesService.IsAlchemyDeletedOrInactiveAsync(alchemyId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Alchemies oldAlchemy = await GetAlchemyCollectionByIdAsync(userId, alchemyId) ?? new Alchemies();

        var updateResult = await _alchemiesGalleryRepository.UpdateCurrentStarAlchemyGalleryAsync(userId, alchemyId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Alchemies newAlchemy = await GetAlchemyCollectionByIdAsync(userId, alchemyId) ?? new Alchemies();
        PowerManager deltaPower = (PowerManager)newAlchemy - (PowerManager)oldAlchemy;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarAlchemiesGalleryAsync(string userId)
    {
        Alchemies oldAlchemy = await SumPowerAlchemiesGalleryAsync(userId);

        var updateResult = await _alchemiesGalleryRepository.UpdateBatchCurrentStarAlchemiesGalleryAsync(userId);

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

        Alchemies newAlchemy = await SumPowerAlchemiesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAlchemy - (PowerManager)oldAlchemy;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchAlchemiesGalleryAsync(string userId, List<Alchemies> alchemies)
    {
        var insertResult = await _alchemiesGalleryRepository.InsertBatchAlchemiesGalleryAsync(userId, alchemies);

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

    public async Task<Alchemies> GetAlchemyCollectionByIdAsync(string userId, string alchemyId)
    {
        var result = await _alchemiesGalleryRepository.GetAlchemyCollectionByIdAsync(userId, alchemyId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateAlchemyGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _alchemiesService.IsAlchemyDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        await _alchemiesGalleryRepository.UpdateAlchemyGalleryPowerAsync(userId, Id, await _service.GetAlchemyByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
