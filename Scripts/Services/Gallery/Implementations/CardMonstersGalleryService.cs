using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardMonstersGalleryService : ICardMonstersGalleryService
{
    private readonly ICardMonstersGalleryRepository _cardMonstersGalleryRepository;
    private readonly ICardMonstersService _cardMonstersService;

    public CardMonstersGalleryService(
        ICardMonstersGalleryRepository cardMonstersGalleryRepository,
        ICardMonstersService cardMonstersService)
    {
        _cardMonstersGalleryRepository = cardMonstersGalleryRepository;
        _cardMonstersService = cardMonstersService;
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

    public async Task<InsertOrUpdateResult<bool>> InsertCardMonsterGalleryAsync(string userId, string Id)
    {
        var checkResult = await _cardMonstersService.IsCardMonsterDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }
        
        var insertResult = await _cardMonstersGalleryRepository.InsertCardMonsterGalleryAsync(userId, Id, await _cardMonstersService.GetCardMonsterByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusCardMonsterGalleryAsync(string userId, string cardMonsterId)
    {
        var checkResult = await _cardMonstersService.IsCardMonsterDeletedOrInactiveAsync(cardMonsterId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardMonstersGalleryRepository.UpdateStatusCardMonsterGalleryAsync(userId, cardMonsterId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = true,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardMonstersGalleryAsync(string userId)
    {
        var updateResult = await _cardMonstersGalleryRepository.UpdateBatchStatusCardMonstersGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = true,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<CardMonsters> SumPowerCardMonstersGalleryAsync(string userId)
    {
        return await _cardMonstersGalleryRepository.SumPowerCardMonstersGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarCardMonsterGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _cardMonstersService.IsCardMonsterDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardMonstersGalleryRepository.UpdateTempStarCardMonsterGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardMonsterGalleryAsync(string userId, string cardMonsterId)
    {
        var checkResult = await _cardMonstersService.IsCardMonsterDeletedOrInactiveAsync(cardMonsterId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _cardMonstersGalleryRepository.UpdateCurrentStarCardMonsterGalleryAsync(userId, cardMonsterId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = true,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardMonstersGalleryAsync(string userId)
    {
        var updateResult = await _cardMonstersGalleryRepository.UpdateBatchCurrentStarCardMonstersGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = true,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchCardMonstersGalleryAsync(string userId, List<CardMonsters> cardMonsters)
    {
        var insertResult = await _cardMonstersGalleryRepository.InsertBatchCardMonstersGalleryAsync(userId, cardMonsters);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<CardMonsters> GetCardMonsterCollectionByIdAsync(string userId, string cardMonsterId)
    {
        var result = await _cardMonstersGalleryRepository.GetCardMonsterCollectionByIdAsync(userId, cardMonsterId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardMonsterGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _cardMonstersService.IsCardMonsterDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower =false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        await _cardMonstersGalleryRepository.UpdateCardMonsterGalleryPowerAsync(userId, Id, await _service.GetCardMonsterByIdAsync(Id));
        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }
}
