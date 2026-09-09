using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardSpellsGalleryService : ICardSpellsGalleryService
{
    private readonly ICardSpellsGalleryRepository _cardSpellsGalleryRepository;
    private readonly ICardSpellsService _cardSpellsService;
    private readonly IPowerManagerService _powerManagerService;

    public CardSpellsGalleryService(
        ICardSpellsGalleryRepository cardSpellsGalleryRepository,
        ICardSpellsService cardSpellsService,
        IPowerManagerService powerManagerService)
    {
        _cardSpellsGalleryRepository = cardSpellsGalleryRepository;
        _cardSpellsService = cardSpellsService;
        _powerManagerService = powerManagerService;
    }

    public static ICardSpellsGalleryService Create() => ServiceContainer.GetService<ICardSpellsGalleryService>();

    public async Task<List<CardSpells>> GetCardSpellsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardSpells> list = await _cardSpellsGalleryRepository.GetCardSpellsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSpellsCountAsync(string search, string type, string rare)
    {
        return await _cardSpellsGalleryRepository.GetCardSpellsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertCardSpellGalleryAsync(string userId, string Id)
    {
        var checkResult = await _cardSpellsService.IsCardSpellDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }
        
        var insertResult = await _cardSpellsGalleryRepository.InsertCardSpellGalleryAsync(userId, Id, await _cardSpellsService.GetCardSpellByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusCardSpellGalleryAsync(string userId, string cardSpellId)
    {
        var checkResult = await _cardSpellsService.IsCardSpellDeletedOrInactiveAsync(cardSpellId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardSpellsGalleryRepository.UpdateStatusCardSpellGalleryAsync(userId, cardSpellId);

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
        CardSpells cardSpellGallery = await GetCardSpellCollectionByIdAsync(userId, cardSpellId) ?? new CardSpells();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardSpellGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardSpellsGalleryAsync(string userId)
    {
        CardSpells oldCardSpell = await SumPowerCardSpellsGalleryAsync(userId);

        var updateResult = await _cardSpellsGalleryRepository.UpdateBatchStatusCardSpellsGalleryAsync(userId);

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

        CardSpells newCardSpell = await SumPowerCardSpellsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardSpell - (PowerManager)oldCardSpell;

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

    public async Task<CardSpells> SumPowerCardSpellsGalleryAsync(string userId)
    {
        return await _cardSpellsGalleryRepository.SumPowerCardSpellsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarCardSpellGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _cardSpellsService.IsCardSpellDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardSpellsGalleryRepository.UpdateTempStarCardSpellGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardSpellGalleryAsync(string userId, string cardSpellId)
    {
        var checkResult = await _cardSpellsService.IsCardSpellDeletedOrInactiveAsync(cardSpellId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        CardSpells oldCardSpell = await GetCardSpellCollectionByIdAsync(userId, cardSpellId) ?? new CardSpells();

        var updateResult = await _cardSpellsGalleryRepository.UpdateCurrentStarCardSpellGalleryAsync(userId, cardSpellId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        CardSpells newCardSpell = await GetCardSpellCollectionByIdAsync(userId, cardSpellId) ?? new CardSpells();
        PowerManager deltaPower = (PowerManager)newCardSpell - (PowerManager)oldCardSpell;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardSpellsGalleryAsync(string userId)
    {
        CardSpells oldCardSpell = await SumPowerCardSpellsGalleryAsync(userId);

        var updateResult = await _cardSpellsGalleryRepository.UpdateBatchCurrentStarCardSpellsGalleryAsync(userId);

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

        CardSpells newCardSpell = await SumPowerCardSpellsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardSpell - (PowerManager)oldCardSpell;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchCardSpellsGalleryAsync(string userId, List<CardSpells> cardSpells)
    {
        var insertResult = await _cardSpellsGalleryRepository.InsertBatchCardSpellsGalleryAsync(userId, cardSpells);

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

    public async Task<CardSpells> GetCardSpellCollectionByIdAsync(string userId, string cardSpellId)
    {
        var result = await _cardSpellsGalleryRepository.GetCardSpellCollectionByIdAsync(userId, cardSpellId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardSpellGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _cardSpellsService.IsCardSpellDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ICardSpellsRepository _repository = new CardSpellsRepository();
        CardSpellsService _service = new CardSpellsService(_repository);
        await _cardSpellsGalleryRepository.UpdateCardSpellGalleryPowerAsync(userId, Id, await _service.GetCardSpellByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
