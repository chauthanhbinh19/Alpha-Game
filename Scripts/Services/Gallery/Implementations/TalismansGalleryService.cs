using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TalismansGalleryService : ITalismansGalleryService
{
    private readonly ITalismansGalleryRepository _talismansGalleryRepository;
    private readonly ITalismansService _talismansService;
    private readonly IPowerManagerService _powerManagerService;

    public TalismansGalleryService(
        ITalismansGalleryRepository talismansGalleryRepository,
        ITalismansService talismansService,
        IPowerManagerService powerManagerService)
    {
        _talismansGalleryRepository = talismansGalleryRepository;
        _talismansService = talismansService;
        _powerManagerService = powerManagerService;
    }

    public static ITalismansGalleryService Create() => ServiceContainer.GetService<ITalismansGalleryService>();

    public async Task<List<Talismans>> GetTalismansCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = await _talismansGalleryRepository.GetTalismansCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansCountAsync(string search, string type, string rare)
    {
        return await _talismansGalleryRepository.GetTalismansCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertTalismanGalleryAsync(string userId, string Id)
    {
        var checkResult = await _talismansService.IsTalismanDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _talismansGalleryRepository.InsertTalismanGalleryAsync(userId, Id, await _talismansService.GetTalismanByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusTalismanGalleryAsync(string userId, string talismanId)
    {
        var checkResult = await _talismansService.IsTalismanDeletedOrInactiveAsync(talismanId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _talismansGalleryRepository.UpdateStatusTalismanGalleryAsync(userId, talismanId);

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
        Talismans talismanGallery = await GetTalismanCollectionByIdAsync(userId, talismanId) ?? new Talismans();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)talismanGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTalismansGalleryAsync(string userId)
    {
        Talismans oldTalisman = await SumPowerTalismansGalleryAsync(userId);

        var updateResult = await _talismansGalleryRepository.UpdateBatchStatusTalismansGalleryAsync(userId);

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

        Talismans newTalisman = await SumPowerTalismansGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTalisman - (PowerManager)oldTalisman;

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

    public async Task<Talismans> SumPowerTalismansGalleryAsync(string userId)
    {
        return await _talismansGalleryRepository.SumPowerTalismansGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarTalismanGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _talismansService.IsTalismanDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _talismansGalleryRepository.UpdateTempStarTalismanGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarTalismanGalleryAsync(string userId, string talismanId)
    {
        var checkResult = await _talismansService.IsTalismanDeletedOrInactiveAsync(talismanId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Talismans oldTalisman = await GetTalismanCollectionByIdAsync(userId, talismanId) ?? new Talismans();

        var updateResult = await _talismansGalleryRepository.UpdateCurrentStarTalismanGalleryAsync(userId, talismanId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Talismans newTalisman = await GetTalismanCollectionByIdAsync(userId, talismanId) ?? new Talismans();
        PowerManager deltaPower = (PowerManager)newTalisman - (PowerManager)oldTalisman;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarTalismansGalleryAsync(string userId)
    {
        Talismans oldTalisman = await SumPowerTalismansGalleryAsync(userId);

        var updateResult = await _talismansGalleryRepository.UpdateBatchCurrentStarTalismansGalleryAsync(userId);

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

        Talismans newTalisman = await SumPowerTalismansGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTalisman - (PowerManager)oldTalisman;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchTalismansGalleryAsync(string userId, List<Talismans> talismans)
    {
        var insertResult = await _talismansGalleryRepository.InsertBatchTalismansGalleryAsync(userId, talismans);

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

    public async Task<Talismans> GetTalismanCollectionByIdAsync(string userId, string talismanId)
    {
        var result = await _talismansGalleryRepository.GetTalismanCollectionByIdAsync(userId, talismanId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTalismanGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _talismansService.IsTalismanDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ITalismansRepository _repository = new TalismansRepository();
        TalismansService _service = new TalismansService(_repository);
        await _talismansGalleryRepository.UpdateTalismanGalleryPowerAsync(userId, Id, await _service.GetTalismanByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
