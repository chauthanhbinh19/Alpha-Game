using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardCaptainsGalleryService : ICardCaptainsGalleryService
{
    private readonly ICardCaptainsGalleryRepository _cardCaptainsGalleryRepository;
    private readonly ICardCaptainsService _cardCaptainsService;
    private readonly IPowerManagerService _powerManagerService;

    public CardCaptainsGalleryService(
        ICardCaptainsGalleryRepository cardCaptainsGalleryRepository,
        ICardCaptainsService cardCaptainsService,
        IPowerManagerService powerManagerService)
    {
        _cardCaptainsGalleryRepository = cardCaptainsGalleryRepository;
        _cardCaptainsService = cardCaptainsService;
        _powerManagerService = powerManagerService;
    }

    public static ICardCaptainsGalleryService Create() => ServiceContainer.GetService<ICardCaptainsGalleryService>();

    public async Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardCaptains> list = await _cardCaptainsGalleryRepository.GetCardCaptainsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardCaptainsCountAsync(string search, string type, string rare)
    {
        return await _cardCaptainsGalleryRepository.GetCardCaptainsCountAsync(search, type, rare);
    }

    public async Task<bool> InsertCardCaptainGalleryAsync(string userId, string Id)
    {
        var insertResult = await _cardCaptainsGalleryRepository.InsertCardCaptainGalleryAsync(userId, Id, await _cardCaptainsService.GetCardCaptainByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCardCaptainGalleryAsync(string userId, string cardCaptainId)
    {
        var updateResult = await _cardCaptainsGalleryRepository.UpdateStatusCardCaptainGalleryAsync(userId, cardCaptainId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CardCaptains cardCaptainGallery = await GetCardCaptainCollectionByIdAsync(userId, cardCaptainId) ?? new CardCaptains();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardCaptainGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCardCaptainsGalleryAsync(string userId)
    {
        CardCaptains oldCardCaptain = await SumPowerCardCaptainsGalleryAsync(userId);

        var updateResult = await _cardCaptainsGalleryRepository.UpdateBatchStatusCardCaptainsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CardCaptains newCardCaptain = await SumPowerCardCaptainsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardCaptain - (PowerManager)oldCardCaptain;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CardCaptains> SumPowerCardCaptainsGalleryAsync(string userId)
    {
        return await _cardCaptainsGalleryRepository.SumPowerCardCaptainsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarCardCaptainGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _cardCaptainsGalleryRepository.UpdateTempStarCardCaptainGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCardCaptainGalleryAsync(string userId, string cardCaptainId)
    {
        CardCaptains oldCardCaptain = await GetCardCaptainCollectionByIdAsync(userId, cardCaptainId) ?? new CardCaptains();

        var updateResult = await _cardCaptainsGalleryRepository.UpdateCurrentStarCardCaptainGalleryAsync(userId, cardCaptainId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CardCaptains newCardCaptain = await GetCardCaptainCollectionByIdAsync(userId, cardCaptainId) ?? new CardCaptains();
        PowerManager deltaPower = (PowerManager)newCardCaptain - (PowerManager)oldCardCaptain;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCardCaptainsGalleryAsync(string userId)
    {
        CardCaptains oldCardCaptain = await SumPowerCardCaptainsGalleryAsync(userId);

        var updateResult = await _cardCaptainsGalleryRepository.UpdateBatchCurrentStarCardCaptainsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CardCaptains newCardCaptain = await SumPowerCardCaptainsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardCaptain - (PowerManager)oldCardCaptain;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCardCaptainsGalleryAsync(string userId, List<CardCaptains> cardCaptains)
    {
        var insertResult = await _cardCaptainsGalleryRepository.InsertBatchCardCaptainsGalleryAsync(userId, cardCaptains);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CardCaptains> GetCardCaptainCollectionByIdAsync(string userId, string cardCaptainId)
    {
        var result = await _cardCaptainsGalleryRepository.GetCardCaptainCollectionByIdAsync(userId, cardCaptainId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCardCaptainGalleryPowerAsync(string userId, string Id)
    {
        ICardCaptainsRepository _repository = new CardCaptainsRepository();
        CardCaptainsService _service = new CardCaptainsService(_repository);
        await _cardCaptainsGalleryRepository.UpdateCardCaptainGalleryPowerAsync(userId, Id, await _service.GetCardCaptainByIdAsync(Id));
    }
}
