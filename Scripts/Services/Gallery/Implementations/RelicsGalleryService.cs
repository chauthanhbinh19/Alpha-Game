using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RelicsGalleryService : IRelicsGalleryService
{
    private readonly IRelicsGalleryRepository _relicsGalleryRepository;
    private readonly IRelicsService _relicsService;
    private readonly IPowerManagerService _powerManagerService;

    public RelicsGalleryService(
        IRelicsGalleryRepository relicsGalleryRepository,
        IRelicsService relicsService,
        IPowerManagerService powerManagerService)
    {
        _relicsGalleryRepository = relicsGalleryRepository;
        _relicsService = relicsService;
        _powerManagerService = powerManagerService;
    }

    public static IRelicsGalleryService Create() => ServiceContainer.GetService<IRelicsGalleryService>();

    public async Task<List<Relics>> GetRelicsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> list = await _relicsGalleryRepository.GetRelicsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRelicsCountAsync(string search, string type, string rare)
    {
        return await _relicsGalleryRepository.GetRelicsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertRelicGalleryAsync(string userId, string Id)
    {
        var checkResult = await _relicsService.IsRelicDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _relicsGalleryRepository.InsertRelicGalleryAsync(userId, Id, await _relicsService.GetRelicByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusRelicGalleryAsync(string userId, string relicId)
    {
        var checkResult = await _relicsService.IsRelicDeletedOrInactiveAsync(relicId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _relicsGalleryRepository.UpdateStatusRelicGalleryAsync(userId, relicId);

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
        Relics relicGallery = await GetRelicCollectionByIdAsync(userId, relicId) ?? new Relics();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)relicGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusRelicsGalleryAsync(string userId)
    {
        Relics oldRelic = await SumPowerRelicsGalleryAsync(userId);

        var updateResult = await _relicsGalleryRepository.UpdateBatchStatusRelicsGalleryAsync(userId);

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

        Relics newRelic = await SumPowerRelicsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRelic - (PowerManager)oldRelic;

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

    public async Task<Relics> SumPowerRelicsGalleryAsync(string userId)
    {
        return await _relicsGalleryRepository.SumPowerRelicsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarRelicGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _relicsService.IsRelicDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _relicsGalleryRepository.UpdateTempStarRelicGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarRelicGalleryAsync(string userId, string relicId)
    {
        var checkResult = await _relicsService.IsRelicDeletedOrInactiveAsync(relicId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Relics oldRelic = await GetRelicCollectionByIdAsync(userId, relicId) ?? new Relics();

        var updateResult = await _relicsGalleryRepository.UpdateCurrentStarRelicGalleryAsync(userId, relicId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Relics newRelic = await GetRelicCollectionByIdAsync(userId, relicId) ?? new Relics();
        PowerManager deltaPower = (PowerManager)newRelic - (PowerManager)oldRelic;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarRelicsGalleryAsync(string userId)
    {
        Relics oldRelic = await SumPowerRelicsGalleryAsync(userId);

        var updateResult = await _relicsGalleryRepository.UpdateBatchCurrentStarRelicsGalleryAsync(userId);

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

        Relics newRelic = await SumPowerRelicsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRelic - (PowerManager)oldRelic;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchRelicsGalleryAsync(string userId, List<Relics> relics)
    {
        var insertResult = await _relicsGalleryRepository.InsertBatchRelicsGalleryAsync(userId, relics);

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

    public async Task<Relics> GetRelicCollectionByIdAsync(string userId, string relicId)
    {
        var result = await _relicsGalleryRepository.GetRelicCollectionByIdAsync(userId, relicId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateRelicGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _relicsService.IsRelicDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        await _relicsGalleryRepository.UpdateRelicGalleryPowerAsync(userId, Id, await _service.GetRelicByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
