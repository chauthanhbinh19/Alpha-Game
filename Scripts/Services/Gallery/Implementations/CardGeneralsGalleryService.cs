using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardGeneralsGalleryService : ICardGeneralsGalleryService
{
    private readonly ICardGeneralsGalleryRepository _cardGeneralsGalleryRepository;
    private readonly ICardGeneralsService _cardGeneralsService;
    private readonly IPowerManagerService _powerManagerService;

    public CardGeneralsGalleryService(
        ICardGeneralsGalleryRepository cardGeneralsGalleryRepository,
        ICardGeneralsService cardGeneralsService,
        IPowerManagerService powerManagerService)
    {
        _cardGeneralsGalleryRepository = cardGeneralsGalleryRepository;
        _cardGeneralsService = cardGeneralsService;
        _powerManagerService = powerManagerService;
    }

    public static ICardGeneralsGalleryService Create() => ServiceContainer.GetService<ICardGeneralsGalleryService>();

    public async Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardGenerals> list = await _cardGeneralsGalleryRepository.GetCardGeneralsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardGeneralsCountAsync(string search, string type, string rare)
    {
        return await _cardGeneralsGalleryRepository.GetCardGeneralsCountAsync(search, type, rare);
    }

    public async Task<bool> InsertCardGeneralGalleryAsync(string userId, string Id)
    {
        var insertResult = await _cardGeneralsGalleryRepository.InsertCardGeneralGalleryAsync(userId, Id, await _cardGeneralsService.GetCardGeneralByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCardGeneralGalleryAsync(string userId, string cardGeneralId)
    {
        var updateResult = await _cardGeneralsGalleryRepository.UpdateStatusCardGeneralGalleryAsync(userId, cardGeneralId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CardGenerals cardGeneralGallery = await GetCardGeneralCollectionByIdAsync(userId, cardGeneralId) ?? new CardGenerals();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardGeneralGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCardGeneralsGalleryAsync(string userId)
    {
        CardGenerals oldCardGeneral = await SumPowerCardGeneralsGalleryAsync(userId);

        var updateResult = await _cardGeneralsGalleryRepository.UpdateBatchStatusCardGeneralsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CardGenerals newCardGeneral = await SumPowerCardGeneralsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardGeneral - (PowerManager)oldCardGeneral;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CardGenerals> SumPowerCardGeneralsGalleryAsync(string userId)
    {
        return await _cardGeneralsGalleryRepository.SumPowerCardGeneralsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarCardGeneralGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _cardGeneralsGalleryRepository.UpdateStarCardGeneralGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCardGeneralGalleryAsync(string userId, string cardGeneralId)
    {
        CardGenerals oldCardGeneral = await GetCardGeneralCollectionByIdAsync(userId, cardGeneralId) ?? new CardGenerals();

        var updateResult = await _cardGeneralsGalleryRepository.UpdateCurrentStarCardGeneralGalleryAsync(userId, cardGeneralId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CardGenerals newCardGeneral = await GetCardGeneralCollectionByIdAsync(userId, cardGeneralId) ?? new CardGenerals();
        PowerManager deltaPower = (PowerManager)newCardGeneral - (PowerManager)oldCardGeneral;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCardGeneralsGalleryAsync(string userId)
    {
        CardGenerals oldCardGeneral = await SumPowerCardGeneralsGalleryAsync(userId);

        var updateResult = await _cardGeneralsGalleryRepository.UpdateBatchCurrentStarCardGeneralsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CardGenerals newCardGeneral = await SumPowerCardGeneralsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardGeneral - (PowerManager)oldCardGeneral;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCardGeneralsGalleryAsync(string userId, List<CardGenerals> cardGenerals)
    {
        var insertResult = await _cardGeneralsGalleryRepository.InsertBatchCardGeneralsGalleryAsync(userId, cardGenerals);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CardGenerals> GetCardGeneralCollectionByIdAsync(string userId, string cardGeneralId)
    {
        var result = await _cardGeneralsGalleryRepository.GetCardGeneralCollectionByIdAsync(userId, cardGeneralId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCardGeneralGalleryPowerAsync(string userId, string Id)
    {
        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        await _cardGeneralsGalleryRepository.UpdateCardGeneralGalleryPowerAsync(userId, Id, await _service.GetCardGeneralByIdAsync(Id));
    }
}
