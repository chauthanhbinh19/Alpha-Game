using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MedalsGalleryService : IMedalsGalleryService
{
    private readonly IMedalsGalleryRepository _medalsGalleryRepository;
    private readonly IMedalsService _medalsService;
    private readonly IPowerManagerService _powerManagerService;

    public MedalsGalleryService(
        IMedalsGalleryRepository medalsGalleryRepository,
        IMedalsService medalsService,
        IPowerManagerService powerManagerService)
    {
        _medalsGalleryRepository = medalsGalleryRepository;
        _medalsService = medalsService;
        _powerManagerService = powerManagerService;
    }

    public static IMedalsGalleryService Create() => ServiceContainer.GetService<IMedalsGalleryService>();

    public async Task<List<Medals>> GetMedalsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _medalsGalleryRepository.GetMedalsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsCountAsync(string search, string rare)
    {
        return await _medalsGalleryRepository.GetMedalsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertMedalGalleryAsync(string userId, string Id)
    {
        var checkResult = await _medalsService.IsMedalDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _medalsGalleryRepository.InsertMedalGalleryAsync(userId, Id, await _medalsService.GetMedalByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusMedalGalleryAsync(string userId, string medalId)
    {
        var checkResult = await _medalsService.IsMedalDeletedOrInactiveAsync(medalId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _medalsGalleryRepository.UpdateStatusMedalGalleryAsync(userId, medalId);

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
        Medals medalGallery = await GetMedalCollectionByIdAsync(userId, medalId) ?? new Medals();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)medalGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMedalsGalleryAsync(string userId)
    {
        Medals oldMedal = await SumPowerMedalsGalleryAsync(userId);

        var updateResult = await _medalsGalleryRepository.UpdateBatchStatusMedalsGalleryAsync(userId);

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

        Medals newMedal = await SumPowerMedalsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMedal - (PowerManager)oldMedal;

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

    public async Task<Medals> SumPowerMedalsGalleryAsync(string userId)
    {
        return await _medalsGalleryRepository.SumPowerMedalsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarMedalGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _medalsService.IsMedalDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _medalsGalleryRepository.UpdateTempStarMedalGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarMedalGalleryAsync(string userId, string medalId)
    {
        var checkResult = await _medalsService.IsMedalDeletedOrInactiveAsync(medalId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Medals oldMedal = await GetMedalCollectionByIdAsync(userId, medalId) ?? new Medals();

        var updateResult = await _medalsGalleryRepository.UpdateCurrentStarMedalGalleryAsync(userId, medalId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Medals newMedal = await GetMedalCollectionByIdAsync(userId, medalId) ?? new Medals();
        PowerManager deltaPower = (PowerManager)newMedal - (PowerManager)oldMedal;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarMedalsGalleryAsync(string userId)
    {
        Medals oldMedal = await SumPowerMedalsGalleryAsync(userId);

        var updateResult = await _medalsGalleryRepository.UpdateBatchCurrentStarMedalsGalleryAsync(userId);

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

        Medals newMedal = await SumPowerMedalsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMedal - (PowerManager)oldMedal;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchMedalsGalleryAsync(string userId, List<Medals> medals)
    {
        var insertResult = await _medalsGalleryRepository.InsertBatchMedalsGalleryAsync(userId, medals);

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

    public async Task<Medals> GetMedalCollectionByIdAsync(string userId, string medalId)
    {
        var result = await _medalsGalleryRepository.GetMedalCollectionByIdAsync(userId, medalId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateMedalGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _medalsService.IsMedalDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        await _medalsGalleryRepository.UpdateMedalGalleryPowerAsync(userId, Id, await _service.GetMedalByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
