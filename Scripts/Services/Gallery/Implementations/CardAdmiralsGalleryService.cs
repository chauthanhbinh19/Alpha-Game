using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardAdmiralsGalleryService : ICardAdmiralsGalleryService
{
    private readonly ICardAdmiralsGalleryRepository _cardAdmiralsGalleryRepository;
    private readonly ICardAdmiralsService _cardAdmiralsService;
    private readonly IPowerManagerService _powerManagerService;

    public CardAdmiralsGalleryService(
        ICardAdmiralsGalleryRepository cardAdmiralsGalleryRepository,
        ICardAdmiralsService cardAdmiralsService,
        IPowerManagerService powerManagerService)
    {
        _cardAdmiralsGalleryRepository = cardAdmiralsGalleryRepository;
        _cardAdmiralsService = cardAdmiralsService;
        _powerManagerService = powerManagerService;
    }

    public static ICardAdmiralsGalleryService Create() => ServiceContainer.GetService<ICardAdmiralsGalleryService>();

    public async Task<List<CardAdmirals>> GetCardAdmiralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardAdmirals> list = await _cardAdmiralsGalleryRepository.GetCardAdmiralsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare)
    {
        return await _cardAdmiralsGalleryRepository.GetCardAdmiralsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertCardAdmiralGalleryAsync(string userId, string Id)
    {
        var checkResult = await _cardAdmiralsService.IsCardAdmiralDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }
        
        var insertResult = await _cardAdmiralsGalleryRepository.InsertCardAdmiralGalleryAsync(userId, Id, await _cardAdmiralsService.GetCardAdmiralByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusCardAdmiralGalleryAsync(string userId, string cardAdmiralId)
    {
        var checkResult = await _cardAdmiralsService.IsCardAdmiralDeletedOrInactiveAsync(cardAdmiralId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardAdmiralsGalleryRepository.UpdateStatusCardAdmiralGalleryAsync(userId, cardAdmiralId);

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
        CardAdmirals cardAdmiralGallery = await GetCardAdmiralCollectionByIdAsync(userId, cardAdmiralId) ?? new CardAdmirals();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardAdmiralGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardAdmiralsGalleryAsync(string userId)
    {
        CardAdmirals oldCardAdmiral = await SumPowerCardAdmiralsGalleryAsync(userId);

        var updateResult = await _cardAdmiralsGalleryRepository.UpdateBatchStatusCardAdmiralsGalleryAsync(userId);

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

        CardAdmirals newCardAdmiral = await SumPowerCardAdmiralsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardAdmiral - (PowerManager)oldCardAdmiral;

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

    public async Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync(string userId)
    {
        return await _cardAdmiralsGalleryRepository.SumPowerCardAdmiralsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarCardAdmiralGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _cardAdmiralsService.IsCardAdmiralDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardAdmiralsGalleryRepository.UpdateTempStarCardAdmiralGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardAdmiralGalleryAsync(string userId, string cardAdmiralId)
    {
        var checkResult = await _cardAdmiralsService.IsCardAdmiralDeletedOrInactiveAsync(cardAdmiralId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        CardAdmirals oldCardAdmiral = await GetCardAdmiralCollectionByIdAsync(userId, cardAdmiralId) ?? new CardAdmirals();

        var updateResult = await _cardAdmiralsGalleryRepository.UpdateCurrentStarCardAdmiralGalleryAsync(userId, cardAdmiralId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        CardAdmirals newCardAdmiral = await GetCardAdmiralCollectionByIdAsync(userId, cardAdmiralId) ?? new CardAdmirals();
        PowerManager deltaPower = (PowerManager)newCardAdmiral - (PowerManager)oldCardAdmiral;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardAdmiralsGalleryAsync(string userId)
    {
        CardAdmirals oldCardAdmiral = await SumPowerCardAdmiralsGalleryAsync(userId);

        var updateResult = await _cardAdmiralsGalleryRepository.UpdateBatchCurrentStarCardAdmiralsGalleryAsync(userId);

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

        CardAdmirals newCardAdmiral = await SumPowerCardAdmiralsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardAdmiral - (PowerManager)oldCardAdmiral;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchCardAdmiralsGalleryAsync(string userId, List<CardAdmirals> cardAdmirals)
    {
        var insertResult = await _cardAdmiralsGalleryRepository.InsertBatchCardAdmiralsGalleryAsync(userId, cardAdmirals);

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

    public async Task<CardAdmirals> GetCardAdmiralCollectionByIdAsync(string userId, string cardAdmiralId)
    {
        var result = await _cardAdmiralsGalleryRepository.GetCardAdmiralCollectionByIdAsync(userId, cardAdmiralId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardAdmiralGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _cardAdmiralsService.IsCardAdmiralDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ICardAdmiralsRepository _repository = new CardAdmiralsRepository();
        CardAdmiralsService _service = new CardAdmiralsService(_repository);
        await _cardAdmiralsGalleryRepository.UpdateCardAdmiralGalleryPowerAsync(userId, Id, await _service.GetCardAdmiralByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
