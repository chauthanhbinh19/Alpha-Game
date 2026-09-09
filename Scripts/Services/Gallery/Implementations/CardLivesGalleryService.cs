using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardLivesGalleryService : ICardLivesGalleryService
{
    private readonly ICardLivesGalleryRepository _cardLivesGalleryRepository;
    private readonly ICardLivesService _cardLivesService;
    private readonly IPowerManagerService _powerManagerService;

    public CardLivesGalleryService(
        ICardLivesGalleryRepository cardLivesGalleryRepository,
        ICardLivesService cardLivesService,
        IPowerManagerService powerManagerService)
    {
        _cardLivesGalleryRepository = cardLivesGalleryRepository;
        _cardLivesService = cardLivesService;
        _powerManagerService = powerManagerService;
    }

    public static ICardLivesGalleryService Create() => ServiceContainer.GetService<ICardLivesGalleryService>();

    public async Task<List<CardLives>> GetCardLivesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = await _cardLivesGalleryRepository.GetCardLivesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardLivesCountAsync(string search, string type, string rare)
    {
        return await _cardLivesGalleryRepository.GetCardLivesCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertCardLifeGalleryAsync(string userId, string Id)
    {
        var checkResult = await _cardLivesService.IsCardLifeDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _cardLivesGalleryRepository.InsertCardLifeGalleryAsync(userId, Id, await _cardLivesService.GetCardLifeByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusCardLifeGalleryAsync(string userId, string cardLifeId)
    {
        var checkResult = await _cardLivesService.IsCardLifeDeletedOrInactiveAsync(cardLifeId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardLivesGalleryRepository.UpdateStatusCardLifeGalleryAsync(userId, cardLifeId);

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
        CardLives cardLifeGallery = await GetCardLifeCollectionByIdAsync(userId, cardLifeId) ?? new CardLives();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardLifeGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardLivesGalleryAsync(string userId)
    {
        CardLives oldCardLife = await SumPowerCardLivesGalleryAsync(userId);

        var updateResult = await _cardLivesGalleryRepository.UpdateBatchStatusCardLivesGalleryAsync(userId);

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

        CardLives newCardLife = await SumPowerCardLivesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardLife - (PowerManager)oldCardLife;

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

    public async Task<CardLives> SumPowerCardLivesGalleryAsync(string userId)
    {
        return await _cardLivesGalleryRepository.SumPowerCardLivesGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarCardLifeGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _cardLivesService.IsCardLifeDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardLivesGalleryRepository.UpdateTempStarCardLifeGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardLifeGalleryAsync(string userId, string cardLifeId)
    {
        var checkResult = await _cardLivesService.IsCardLifeDeletedOrInactiveAsync(cardLifeId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        CardLives oldCardLife = await GetCardLifeCollectionByIdAsync(userId, cardLifeId) ?? new CardLives();

        var updateResult = await _cardLivesGalleryRepository.UpdateCurrentStarCardLifeGalleryAsync(userId, cardLifeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        CardLives newCardLife = await GetCardLifeCollectionByIdAsync(userId, cardLifeId) ?? new CardLives();
        PowerManager deltaPower = (PowerManager)newCardLife - (PowerManager)oldCardLife;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardLivesGalleryAsync(string userId)
    {
        CardLives oldCardLife = await SumPowerCardLivesGalleryAsync(userId);

        var updateResult = await _cardLivesGalleryRepository.UpdateBatchCurrentStarCardLivesGalleryAsync(userId);

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

        CardLives newCardLife = await SumPowerCardLivesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardLife - (PowerManager)oldCardLife;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchCardLivesGalleryAsync(string userId, List<CardLives> cardLives)
    {
        var insertResult = await _cardLivesGalleryRepository.InsertBatchCardLivesGalleryAsync(userId, cardLives);

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

    public async Task<CardLives> GetCardLifeCollectionByIdAsync(string userId, string cardLiveId)
    {
        var result = await _cardLivesGalleryRepository.GetCardLifeCollectionByIdAsync(userId, cardLiveId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardLifeGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _cardLivesService.IsCardLifeDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ICardLivesRepository _repository = new CardLivesRepository();
        CardLivesService _service = new CardLivesService(_repository);
        await _cardLivesGalleryRepository.UpdateCardLifeGalleryPowerAsync(userId, Id, await _service.GetCardLifeByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
