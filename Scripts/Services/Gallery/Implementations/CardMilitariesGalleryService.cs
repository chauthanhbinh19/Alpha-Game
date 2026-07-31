using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardMilitariesGalleryService : ICardMilitariesGalleryService
{
    private readonly ICardMilitariesGalleryRepository _cardMilitariesGalleryRepository;
    private readonly ICardMilitariesService _cardMilitariesService;
    private readonly IPowerManagerService _powerManagerService;

    public CardMilitariesGalleryService(
        ICardMilitariesGalleryRepository cardMilitariesGalleryRepository,
        ICardMilitariesService cardMilitariesService,
        IPowerManagerService powerManagerService)
    {
        _cardMilitariesGalleryRepository = cardMilitariesGalleryRepository;
        _cardMilitariesService = cardMilitariesService;
        _powerManagerService = powerManagerService;
    }

    public static ICardMilitariesGalleryService Create() => ServiceContainer.GetService<ICardMilitariesGalleryService>();

    public async Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> list = await _cardMilitariesGalleryRepository.GetCardMilitariesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMilitariesCountAsync(string search, string type, string rare)
    {
        return await _cardMilitariesGalleryRepository.GetCardMilitariesCountAsync(search, type, rare);
    }

    public async Task<bool> InsertCardMilitaryGalleryAsync(string userId, string Id)
    {
        var insertResult = await _cardMilitariesGalleryRepository.InsertCardMilitaryGalleryAsync(userId, Id, await _cardMilitariesService.GetCardMilitaryByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCardMilitaryGalleryAsync(string userId, string cardMilitaryId)
    {
        var updateResult = await _cardMilitariesGalleryRepository.UpdateStatusCardMilitaryGalleryAsync(userId, cardMilitaryId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CardMilitaries cardMilitaryGallery = await GetCardMilitaryCollectionByIdAsync(userId, cardMilitaryId) ?? new CardMilitaries();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardMilitaryGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCardMilitariesGalleryAsync(string userId)
    {
        CardMilitaries oldCardMilitary = await SumPowerCardMilitariesGalleryAsync(userId);

        var updateResult = await _cardMilitariesGalleryRepository.UpdateBatchStatusCardMilitariesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CardMilitaries newCardMilitary = await SumPowerCardMilitariesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardMilitary - (PowerManager)oldCardMilitary;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync(string userId)
    {
        return await _cardMilitariesGalleryRepository.SumPowerCardMilitariesGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarCardMilitaryGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _cardMilitariesGalleryRepository.UpdateTempStarCardMilitaryGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCardMilitaryGalleryAsync(string userId, string cardMilitaryId)
    {
        CardMilitaries oldCardMilitary = await GetCardMilitaryCollectionByIdAsync(userId, cardMilitaryId) ?? new CardMilitaries();

        var updateResult = await _cardMilitariesGalleryRepository.UpdateCurrentStarCardMilitaryGalleryAsync(userId, cardMilitaryId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CardMilitaries newCardMilitary = await GetCardMilitaryCollectionByIdAsync(userId, cardMilitaryId) ?? new CardMilitaries();
        PowerManager deltaPower = (PowerManager)newCardMilitary - (PowerManager)oldCardMilitary;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCardMilitariesGalleryAsync(string userId)
    {
        CardMilitaries oldCardMilitary = await SumPowerCardMilitariesGalleryAsync(userId);

        var updateResult = await _cardMilitariesGalleryRepository.UpdateBatchCurrentStarCardMilitariesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CardMilitaries newCardMilitary = await SumPowerCardMilitariesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardMilitary - (PowerManager)oldCardMilitary;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCardMilitariesGalleryAsync(string userId, List<CardMilitaries> cardMilitaries)
    {
        var insertResult = await _cardMilitariesGalleryRepository.InsertBatchCardMilitariesGalleryAsync(userId, cardMilitaries);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CardMilitaries> GetCardMilitaryCollectionByIdAsync(string userId, string cardMilitaryId)
    {
        var result = await _cardMilitariesGalleryRepository.GetCardMilitaryCollectionByIdAsync(userId, cardMilitaryId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCardMilitaryGalleryPowerAsync(string userId, string Id)
    {
        ICardMilitariesRepository _repository = new CardMilitariesRepository();
        CardMilitariesService _service = new CardMilitariesService(_repository);
        await _cardMilitariesGalleryRepository.UpdateCardMilitaryGalleryPowerAsync(userId, Id, await _service.GetCardMilitaryByIdAsync(Id));
    }
}
