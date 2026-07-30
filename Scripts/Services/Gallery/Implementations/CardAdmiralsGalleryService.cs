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

    public async Task<bool> InsertCardAdmiralGalleryAsync(string userId, string Id)
    {
        var insertResult = await _cardAdmiralsGalleryRepository.InsertCardAdmiralGalleryAsync(userId, Id, await _cardAdmiralsService.GetCardAdmiralByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCardAdmiralGalleryAsync(string userId, string cardAdmiralId)
    {
        var updateResult = await _cardAdmiralsGalleryRepository.UpdateStatusCardAdmiralGalleryAsync(userId, cardAdmiralId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CardAdmirals cardAdmiralGallery = await GetCardAdmiralCollectionByIdAsync(userId, cardAdmiralId) ?? new CardAdmirals();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardAdmiralGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCardAdmiralsGalleryAsync(string userId)
    {
        CardAdmirals oldCardAdmiral = await SumPowerCardAdmiralsGalleryAsync(userId);

        var updateResult = await _cardAdmiralsGalleryRepository.UpdateBatchStatusCardAdmiralsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CardAdmirals newCardAdmiral = await SumPowerCardAdmiralsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardAdmiral - (PowerManager)oldCardAdmiral;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync(string userId)
    {
        return await _cardAdmiralsGalleryRepository.SumPowerCardAdmiralsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarCardAdmiralGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _cardAdmiralsGalleryRepository.UpdateStarCardAdmiralGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCardAdmiralGalleryAsync(string userId, string cardAdmiralId)
    {
        CardAdmirals oldCardAdmiral = await GetCardAdmiralCollectionByIdAsync(userId, cardAdmiralId) ?? new CardAdmirals();

        var updateResult = await _cardAdmiralsGalleryRepository.UpdateCurrentStarCardAdmiralGalleryAsync(userId, cardAdmiralId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CardAdmirals newCardAdmiral = await GetCardAdmiralCollectionByIdAsync(userId, cardAdmiralId) ?? new CardAdmirals();
        PowerManager deltaPower = (PowerManager)newCardAdmiral - (PowerManager)oldCardAdmiral;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCardAdmiralsGalleryAsync(string userId)
    {
        CardAdmirals oldCardAdmiral = await SumPowerCardAdmiralsGalleryAsync(userId);

        var updateResult = await _cardAdmiralsGalleryRepository.UpdateBatchCurrentStarCardAdmiralsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CardAdmirals newCardAdmiral = await SumPowerCardAdmiralsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardAdmiral - (PowerManager)oldCardAdmiral;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCardAdmiralsGalleryAsync(string userId, List<CardAdmirals> cardAdmirals)
    {
        var insertResult = await _cardAdmiralsGalleryRepository.InsertBatchCardAdmiralsGalleryAsync(userId, cardAdmirals);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CardAdmirals> GetCardAdmiralCollectionByIdAsync(string userId, string cardAdmiralId)
    {
        var result = await _cardAdmiralsGalleryRepository.GetCardAdmiralCollectionByIdAsync(userId, cardAdmiralId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCardAdmiralGalleryPowerAsync(string userId, string Id)
    {
        ICardAdmiralsRepository _repository = new CardAdmiralsRepository();
        CardAdmiralsService _service = new CardAdmiralsService(_repository);
        await _cardAdmiralsGalleryRepository.UpdateCardAdmiralGalleryPowerAsync(userId, Id, await _service.GetCardAdmiralByIdAsync(Id));
    }
}
