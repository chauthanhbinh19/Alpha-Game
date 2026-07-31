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

    public async Task<bool> InsertCardLifeGalleryAsync(string userId, string Id)
    {
        var insertResult = await _cardLivesGalleryRepository.InsertCardLifeGalleryAsync(userId, Id, await _cardLivesService.GetCardLifeByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCardLifeGalleryAsync(string userId, string cardLifeId)
    {
        var updateResult = await _cardLivesGalleryRepository.UpdateStatusCardLifeGalleryAsync(userId, cardLifeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CardLives cardLifeGallery = await GetCardLifeCollectionByIdAsync(userId, cardLifeId) ?? new CardLives();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardLifeGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCardLivesGalleryAsync(string userId)
    {
        CardLives oldCardLife = await SumPowerCardLivesGalleryAsync(userId);

        var updateResult = await _cardLivesGalleryRepository.UpdateBatchStatusCardLivesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CardLives newCardLife = await SumPowerCardLivesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardLife - (PowerManager)oldCardLife;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CardLives> SumPowerCardLivesGalleryAsync(string userId)
    {
        return await _cardLivesGalleryRepository.SumPowerCardLivesGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarCardLifeGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _cardLivesGalleryRepository.UpdateTempStarCardLifeGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCardLifeGalleryAsync(string userId, string cardLifeId)
    {
        CardLives oldCardLife = await GetCardLifeCollectionByIdAsync(userId, cardLifeId) ?? new CardLives();

        var updateResult = await _cardLivesGalleryRepository.UpdateCurrentStarCardLifeGalleryAsync(userId, cardLifeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CardLives newCardLife = await GetCardLifeCollectionByIdAsync(userId, cardLifeId) ?? new CardLives();
        PowerManager deltaPower = (PowerManager)newCardLife - (PowerManager)oldCardLife;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCardLivesGalleryAsync(string userId)
    {
        CardLives oldCardLife = await SumPowerCardLivesGalleryAsync(userId);

        var updateResult = await _cardLivesGalleryRepository.UpdateBatchCurrentStarCardLivesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CardLives newCardLife = await SumPowerCardLivesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardLife - (PowerManager)oldCardLife;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCardLivesGalleryAsync(string userId, List<CardLives> cardLives)
    {
        var insertResult = await _cardLivesGalleryRepository.InsertBatchCardLivesGalleryAsync(userId, cardLives);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CardLives> GetCardLifeCollectionByIdAsync(string userId, string cardLiveId)
    {
        var result = await _cardLivesGalleryRepository.GetCardLifeCollectionByIdAsync(userId, cardLiveId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCardLifeGalleryPowerAsync(string userId, string Id)
    {
        ICardLivesRepository _repository = new CardLivesRepository();
        CardLivesService _service = new CardLivesService(_repository);
        await _cardLivesGalleryRepository.UpdateCardLifeGalleryPowerAsync(userId, Id, await _service.GetCardLifeByIdAsync(Id));
    }
}
