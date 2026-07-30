using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardHeroesGalleryService : ICardHeroesGalleryService
{
    private readonly ICardHeroesGalleryRepository _cardHeroesGalleryRepository;
    private readonly ICardHeroesService _cardHeroesService;
    private readonly IPowerManagerService _powerManagerService;

    public CardHeroesGalleryService(
        ICardHeroesGalleryRepository cardHeroesGalleryRepository,
        ICardHeroesService cardHeroesService,
        IPowerManagerService powerManagerService)
    {
        _cardHeroesGalleryRepository = cardHeroesGalleryRepository;
        _cardHeroesService = cardHeroesService;
        _powerManagerService = powerManagerService;
    }

    public static ICardHeroesGalleryService Create() => ServiceContainer.GetService<ICardHeroesGalleryService>();

    public async Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardHeroes> list = await _cardHeroesGalleryRepository.GetCardHeroesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardHeroesCountAsync(string search, string type, string rare)
    {
        return await _cardHeroesGalleryRepository.GetCardHeroesCountAsync(search, type, rare);
    }

    public async Task<bool> InsertCardHeroGalleryAsync(string userId, string Id)
    {
        var insertResult = await _cardHeroesGalleryRepository.InsertCardHeroGalleryAsync(userId, Id, await _cardHeroesService.GetCardHeroByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCardHeroGalleryAsync(string userId, string cardHeroId)
    {
        var updateResult = await _cardHeroesGalleryRepository.UpdateStatusCardHeroGalleryAsync(userId, cardHeroId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CardHeroes cardHeroGallery = await GetCardHeroCollectionByIdAsync(userId, cardHeroId) ?? new CardHeroes();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardHeroGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCardHeroesGalleryAsync(string userId)
    {
        CardHeroes oldCardHeroe = await SumPowerCardHeroesGalleryAsync(userId);

        var updateResult = await _cardHeroesGalleryRepository.UpdateBatchStatusCardHeroesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CardHeroes newCardHeroe = await SumPowerCardHeroesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardHeroe - (PowerManager)oldCardHeroe;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CardHeroes> SumPowerCardHeroesGalleryAsync(string userId)
    {
        return await _cardHeroesGalleryRepository.SumPowerCardHeroesGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarCardHeroGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _cardHeroesGalleryRepository.UpdateStarCardHeroGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCardHeroGalleryAsync(string userId, string cardHeroId)
    {
        CardHeroes oldCardHero = await GetCardHeroCollectionByIdAsync(userId, cardHeroId) ?? new CardHeroes();

        var updateResult = await _cardHeroesGalleryRepository.UpdateCurrentStarCardHeroGalleryAsync(userId, cardHeroId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CardHeroes newCardHero = await GetCardHeroCollectionByIdAsync(userId, cardHeroId) ?? new CardHeroes();
        PowerManager deltaPower = (PowerManager)newCardHero - (PowerManager)oldCardHero;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCardHeroesGalleryAsync(string userId)
    {
        CardHeroes oldCardHero = await SumPowerCardHeroesGalleryAsync(userId);

        var updateResult = await _cardHeroesGalleryRepository.UpdateBatchCurrentStarCardHeroesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CardHeroes newCardHero = await SumPowerCardHeroesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardHero - (PowerManager)oldCardHero;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCardHeroesGalleryAsync(string userId, List<CardHeroes> cardHeroes)
    {
        var insertResult = await _cardHeroesGalleryRepository.InsertBatchCardHeroesGalleryAsync(userId, cardHeroes);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CardHeroes> GetCardHeroCollectionByIdAsync(string userId, string cardHeroId)
    {
        var result = await _cardHeroesGalleryRepository.GetCardHeroCollectionByIdAsync(userId, cardHeroId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCardHeroGalleryPowerAsync(string userId, string Id)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        await _cardHeroesGalleryRepository.UpdateCardHeroGalleryPowerAsync(userId, Id, await _service.GetCardHeroByIdAsync(Id));
    }
}
