using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardMonstersGalleryService : ICardMonstersGalleryService
{
    private readonly ICardMonstersGalleryRepository _cardMonstersGalleryRepository;
    private readonly ICardMonstersService _cardMonstersService;
    private readonly IPowerManagerService _powerManagerService;

    public CardMonstersGalleryService(
        ICardMonstersGalleryRepository cardMonstersGalleryRepository,
        ICardMonstersService cardMonstersService,
        IPowerManagerService powerManagerService)
    {
        _cardMonstersGalleryRepository = cardMonstersGalleryRepository;
        _cardMonstersService = cardMonstersService;
        _powerManagerService = powerManagerService;
    }

    public static ICardMonstersGalleryService Create() => ServiceContainer.GetService<ICardMonstersGalleryService>();

    public async Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMonsters> list = await _cardMonstersGalleryRepository.GetCardMonstersCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMonstersCountAsync(string search, string type, string rare)
    {
        return await _cardMonstersGalleryRepository.GetCardMonstersCountAsync(search, type, rare);
    }

    public async Task<bool> InsertCardMonsterGalleryAsync(string userId, string Id)
    {
        var insertResult = await _cardMonstersGalleryRepository.InsertCardMonsterGalleryAsync(userId, Id, await _cardMonstersService.GetCardMonsterByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusCardMonsterGalleryAsync(string userId, string cardMonsterId)
    {
        var updateResult = await _cardMonstersGalleryRepository.UpdateStatusCardMonsterGalleryAsync(userId, cardMonsterId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        CardMonsters cardMonsterGallery = await GetCardMonsterCollectionByIdAsync(userId, cardMonsterId) ?? new CardMonsters();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)cardMonsterGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusCardMonstersGalleryAsync(string userId)
    {
        CardMonsters oldCardMonster = await SumPowerCardMonstersGalleryAsync(userId);

        var updateResult = await _cardMonstersGalleryRepository.UpdateBatchStatusCardMonstersGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        CardMonsters newCardMonster = await SumPowerCardMonstersGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardMonster - (PowerManager)oldCardMonster;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<CardMonsters> SumPowerCardMonstersGalleryAsync(string userId)
    {
        return await _cardMonstersGalleryRepository.SumPowerCardMonstersGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarCardMonsterGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _cardMonstersGalleryRepository.UpdateStarCardMonsterGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarCardMonsterGalleryAsync(string userId, string cardMonsterId)
    {
        CardMonsters oldCardMonster = await GetCardMonsterCollectionByIdAsync(userId, cardMonsterId) ?? new CardMonsters();

        var updateResult = await _cardMonstersGalleryRepository.UpdateCurrentStarCardMonsterGalleryAsync(userId, cardMonsterId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        CardMonsters newCardMonster = await GetCardMonsterCollectionByIdAsync(userId, cardMonsterId) ?? new CardMonsters();
        PowerManager deltaPower = (PowerManager)newCardMonster - (PowerManager)oldCardMonster;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarCardMonstersGalleryAsync(string userId)
    {
        CardMonsters oldCardMonster = await SumPowerCardMonstersGalleryAsync(userId);

        var updateResult = await _cardMonstersGalleryRepository.UpdateBatchCurrentStarCardMonstersGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        CardMonsters newCardMonster = await SumPowerCardMonstersGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardMonster - (PowerManager)oldCardMonster;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchCardMonstersGalleryAsync(string userId, List<CardMonsters> cardMonsters)
    {
        var insertResult = await _cardMonstersGalleryRepository.InsertBatchCardMonstersGalleryAsync(userId, cardMonsters);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<CardMonsters> GetCardMonsterCollectionByIdAsync(string userId, string cardMonsterId)
    {
        var result = await _cardMonstersGalleryRepository.GetCardMonsterCollectionByIdAsync(userId, cardMonsterId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateCardMonsterGalleryPowerAsync(string userId, string Id)
    {
        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        await _cardMonstersGalleryRepository.UpdateCardMonsterGalleryPowerAsync(userId, Id, await _service.GetCardMonsterByIdAsync(Id));
    }
}
