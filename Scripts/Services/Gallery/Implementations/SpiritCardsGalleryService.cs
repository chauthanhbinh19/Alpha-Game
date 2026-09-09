using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpiritCardsGalleryService : ISpiritCardsGalleryService
{
    private readonly ISpiritCardsGalleryRepository _spiritCardsGalleryRepository;
    private readonly ISpiritCardsService _spiritCardsService;
    private readonly IPowerManagerService _powerManagerService;

    public SpiritCardsGalleryService(
        ISpiritCardsGalleryRepository spiritCardsGalleryRepository,
        ISpiritCardsService spiritCardsService,
        IPowerManagerService powerManagerService)
    {
        _spiritCardsGalleryRepository = spiritCardsGalleryRepository;
        _spiritCardsService = spiritCardsService;
        _powerManagerService = powerManagerService;
    }

    public static ISpiritCardsGalleryService Create() => ServiceContainer.GetService<ISpiritCardsGalleryService>();

    public async Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _spiritCardsGalleryRepository.GetSpiritCardsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritCardsCountAsync(string search, string type, string rare)
    {
        return await _spiritCardsGalleryRepository.GetSpiritCardsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertSpiritCardGalleryAsync(string userId, string Id)
    {
        var checkResult = await _spiritCardsService.IsSpiritCardDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _spiritCardsGalleryRepository.InsertSpiritCardGalleryAsync(userId, Id, await _spiritCardsService.GetSpiritCardByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusSpiritCardGalleryAsync(string userId, string spiritCardId)
    {
        var checkResult = await _spiritCardsService.IsSpiritCardDeletedOrInactiveAsync(spiritCardId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _spiritCardsGalleryRepository.UpdateStatusSpiritCardGalleryAsync(userId, spiritCardId);

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
        SpiritCards spiritCardGallery = await GetSpiritCardCollectionByIdAsync(userId, spiritCardId) ?? new SpiritCards();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)spiritCardGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSpiritCardsGalleryAsync(string userId)
    {
        SpiritCards oldSpiritCard = await SumPowerSpiritCardsGalleryAsync(userId);

        var updateResult = await _spiritCardsGalleryRepository.UpdateBatchStatusSpiritCardsGalleryAsync(userId);

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

        SpiritCards newSpiritCard = await SumPowerSpiritCardsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritCard - (PowerManager)oldSpiritCard;

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

    public async Task<SpiritCards> SumPowerSpiritCardsGalleryAsync(string userId)
    {
        return await _spiritCardsGalleryRepository.SumPowerSpiritCardsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarSpiritCardGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _spiritCardsService.IsSpiritCardDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _spiritCardsGalleryRepository.UpdateTempStarSpiritCardGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarSpiritCardGalleryAsync(string userId, string spiritCardId)
    {
        var checkResult = await _spiritCardsService.IsSpiritCardDeletedOrInactiveAsync(spiritCardId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        SpiritCards oldSpiritCard = await GetSpiritCardCollectionByIdAsync(userId, spiritCardId) ?? new SpiritCards();

        var updateResult = await _spiritCardsGalleryRepository.UpdateCurrentStarSpiritCardGalleryAsync(userId, spiritCardId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        SpiritCards newSpiritCard = await GetSpiritCardCollectionByIdAsync(userId, spiritCardId) ?? new SpiritCards();
        PowerManager deltaPower = (PowerManager)newSpiritCard - (PowerManager)oldSpiritCard;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarSpiritCardsGalleryAsync(string userId)
    {
        SpiritCards oldSpiritCard = await SumPowerSpiritCardsGalleryAsync(userId);

        var updateResult = await _spiritCardsGalleryRepository.UpdateBatchCurrentStarSpiritCardsGalleryAsync(userId);

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

        SpiritCards newSpiritCard = await SumPowerSpiritCardsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritCard - (PowerManager)oldSpiritCard;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchSpiritCardsGalleryAsync(string userId, List<SpiritCards> spiritCards)
    {
        var insertResult = await _spiritCardsGalleryRepository.InsertBatchSpiritCardsGalleryAsync(userId, spiritCards);

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

    public async Task<SpiritCards> GetSpiritCardCollectionByIdAsync(string userId, string spiritCardId)
    {
        var result = await _spiritCardsGalleryRepository.GetSpiritCardCollectionByIdAsync(userId, spiritCardId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateSpiritCardGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _spiritCardsService.IsSpiritCardDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ISpiritCardsRepository _repository = new SpiritCardsRepository();
        SpiritCardsService _service = new SpiritCardsService(_repository);
        await _spiritCardsGalleryRepository.UpdateSpiritCardGalleryPowerAsync(userId, Id, await _service.GetSpiritCardByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
