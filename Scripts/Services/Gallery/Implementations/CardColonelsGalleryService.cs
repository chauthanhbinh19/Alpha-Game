using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardColonelsGalleryService : ICardColonelsGalleryService
{
    private readonly ICardColonelsGalleryRepository _cardColonelsGalleryRepository;
    private readonly ICardColonelsService _cardColonelsService;
    private readonly IPowerManagerService _powerManagerService;

    public CardColonelsGalleryService(
        ICardColonelsGalleryRepository cardColonelsGalleryRepository,
        ICardColonelsService cardColonelsService,
        IPowerManagerService powerManagerService)
    {
        _cardColonelsGalleryRepository = cardColonelsGalleryRepository;
        _cardColonelsService = cardColonelsService;
        _powerManagerService = powerManagerService;
    }

    public static ICardColonelsGalleryService Create() => ServiceContainer.GetService<ICardColonelsGalleryService>();

    public async Task<List<CardColonels>> GetCardColonelsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardColonels> list = await _cardColonelsGalleryRepository.GetCardColonelsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardColonelsCountAsync(string search, string type, string rare)
    {
        return await _cardColonelsGalleryRepository.GetCardColonelsCountAsync(search, type, rare);
    }

    public async Task<bool> InsertCardColonelGalleryAsync(string userId, string Id)
    {
        var insertResult = await _cardColonelsGalleryRepository.InsertCardColonelGalleryAsync(userId, Id, await _cardColonelsService.GetCardColonelByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCardColonelGalleryAsync(string userId, string cardColonelId)
    {
        var updateResult = await _cardColonelsGalleryRepository.UpdateStatusCardColonelGalleryAsync(userId, cardColonelId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CardColonels cardColonelGallery = await GetCardColonelCollectionByIdAsync(userId, cardColonelId) ?? new CardColonels();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardColonelGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCardColonelsGalleryAsync(string userId)
    {
        CardColonels oldCardColonel = await SumPowerCardColonelsGalleryAsync(userId);

        var updateResult = await _cardColonelsGalleryRepository.UpdateBatchStatusCardColonelsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CardColonels newCardColonel = await SumPowerCardColonelsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardColonel - (PowerManager)oldCardColonel;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CardColonels> SumPowerCardColonelsGalleryAsync(string userId)
    {
        return await _cardColonelsGalleryRepository.SumPowerCardColonelsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarCardColonelGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _cardColonelsGalleryRepository.UpdateTempStarCardColonelGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCardColonelGalleryAsync(string userId, string cardColonelId)
    {
        CardColonels oldCardColonel = await GetCardColonelCollectionByIdAsync(userId, cardColonelId) ?? new CardColonels();

        var updateResult = await _cardColonelsGalleryRepository.UpdateCurrentStarCardColonelGalleryAsync(userId, cardColonelId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CardColonels newCardColonel = await GetCardColonelCollectionByIdAsync(userId, cardColonelId) ?? new CardColonels();
        PowerManager deltaPower = (PowerManager)newCardColonel - (PowerManager)oldCardColonel;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCardColonelsGalleryAsync(string userId)
    {
        CardColonels oldCardColonel = await SumPowerCardColonelsGalleryAsync(userId);

        var updateResult = await _cardColonelsGalleryRepository.UpdateBatchCurrentStarCardColonelsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CardColonels newCardColonel = await SumPowerCardColonelsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardColonel - (PowerManager)oldCardColonel;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCardColonelsGalleryAsync(string userId, List<CardColonels> cardColonels)
    {
        var insertResult = await _cardColonelsGalleryRepository.InsertBatchCardColonelsGalleryAsync(userId, cardColonels);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CardColonels> GetCardColonelCollectionByIdAsync(string userId, string cardColonelId)
    {
        var result = await _cardColonelsGalleryRepository.GetCardColonelCollectionByIdAsync(userId, cardColonelId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCardColonelGalleryPowerAsync(string userId, string Id)
    {
        ICardColonelsRepository _repository = new CardColonelsRepository();
        CardColonelsService _service = new CardColonelsService(_repository);
        await _cardColonelsGalleryRepository.UpdateCardColonelGalleryPowerAsync(userId, Id, await _service.GetCardColonelByIdAsync(Id));
    }
}
