using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardSoldiersGalleryService : ICardSoldiersGalleryService
{
    private readonly ICardSoldiersGalleryRepository _cardSoldiersGalleryRepository;
    private readonly ICardSoldiersService _cardSoldiersService;
    private readonly IPowerManagerService _powerManagerService;

    public CardSoldiersGalleryService(
        ICardSoldiersGalleryRepository cardSoldiersGalleryRepository,
        ICardSoldiersService cardSoldiersService,
        IPowerManagerService powerManagerService)
    {
        _cardSoldiersGalleryRepository = cardSoldiersGalleryRepository;
        _cardSoldiersService = cardSoldiersService;
        _powerManagerService = powerManagerService;
    }

    public static ICardSoldiersGalleryService Create() => ServiceContainer.GetService<ICardSoldiersGalleryService>();

    public async Task<List<CardSoldiers>> GetCardSoldiersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardSoldiers> list = await _cardSoldiersGalleryRepository.GetCardSoldiersCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSoldiersCountAsync(string search, string type, string rare)
    {
        return await _cardSoldiersGalleryRepository.GetCardSoldiersCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertCardSoldierGalleryAsync(string userId, string Id)
    {
        var checkResult = await _cardSoldiersService.IsCardSoldierDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }
        
        var insertResult = await _cardSoldiersGalleryRepository.InsertCardSoldierGalleryAsync(userId, Id, await _cardSoldiersService.GetCardSoldierByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusCardSoldierGalleryAsync(string userId, string cardSoldierId)
    {
        var checkResult = await _cardSoldiersService.IsCardSoldierDeletedOrInactiveAsync(cardSoldierId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardSoldiersGalleryRepository.UpdateStatusCardSoldierGalleryAsync(userId, cardSoldierId);

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
        CardSoldiers cardSoldierGallery = await GetCardSoldierCollectionByIdAsync(userId, cardSoldierId) ?? new CardSoldiers();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardSoldierGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardSoldiersGalleryAsync(string userId)
    {
        CardSoldiers oldCardSoldier = await SumPowerCardSoldiersGalleryAsync(userId);

        var updateResult = await _cardSoldiersGalleryRepository.UpdateBatchStatusCardSoldiersGalleryAsync(userId);

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

        CardSoldiers newCardSoldier = await SumPowerCardSoldiersGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardSoldier - (PowerManager)oldCardSoldier;

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

    public async Task<CardSoldiers> SumPowerCardSoldiersGalleryAsync(string userId)
    {
        return await _cardSoldiersGalleryRepository.SumPowerCardSoldiersGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarCardSoldierGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _cardSoldiersService.IsCardSoldierDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardSoldiersGalleryRepository.UpdateTempStarCardSoldierGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardSoldierGalleryAsync(string userId, string cardSoldierId)
    {
        var checkResult = await _cardSoldiersService.IsCardSoldierDeletedOrInactiveAsync(cardSoldierId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        CardSoldiers oldCardSoldier = await GetCardSoldierCollectionByIdAsync(userId, cardSoldierId) ?? new CardSoldiers();

        var updateResult = await _cardSoldiersGalleryRepository.UpdateCurrentStarCardSoldierGalleryAsync(userId, cardSoldierId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        CardSoldiers newCardSoldier = await GetCardSoldierCollectionByIdAsync(userId, cardSoldierId) ?? new CardSoldiers();
        PowerManager deltaPower = (PowerManager)newCardSoldier - (PowerManager)oldCardSoldier;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardSoldiersGalleryAsync(string userId)
    {
        CardSoldiers oldCardSoldier = await SumPowerCardSoldiersGalleryAsync(userId);

        var updateResult = await _cardSoldiersGalleryRepository.UpdateBatchCurrentStarCardSoldiersGalleryAsync(userId);

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

        CardSoldiers newCardSoldier = await SumPowerCardSoldiersGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardSoldier - (PowerManager)oldCardSoldier;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchCardSoldiersGalleryAsync(string userId, List<CardSoldiers> cardSoldiers)
    {
        var insertResult = await _cardSoldiersGalleryRepository.InsertBatchCardSoldiersGalleryAsync(userId, cardSoldiers);

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

    public async Task<CardSoldiers> GetCardSoldierCollectionByIdAsync(string userId, string cardSoldierId)
    {
        var result = await _cardSoldiersGalleryRepository.GetCardSoldierCollectionByIdAsync(userId, cardSoldierId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardSoldierGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _cardSoldiersService.IsCardSoldierDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ICardSoldiersRepository _repository = new CardSoldiersRepository();
        CardSoldiersService _service = new CardSoldiersService(_repository);
        await _cardSoldiersGalleryRepository.UpdateCardSoldierGalleryPowerAsync(userId, Id, await _service.GetCardSoldierByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
