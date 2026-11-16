using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CardGeneralsGalleryService : ICardGeneralsGalleryService
{
    private readonly ICardGeneralsGalleryRepository _cardGeneralsGalleryRepository;
    private readonly ICardGeneralsService _cardGeneralsService;

    public CardGeneralsGalleryService(
        ICardGeneralsGalleryRepository cardGeneralsGalleryRepository,
        ICardGeneralsService cardGeneralsService)
    {
        _cardGeneralsGalleryRepository = cardGeneralsGalleryRepository;
        _cardGeneralsService = cardGeneralsService;
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

    public async Task<InsertOrUpdateResult<bool>> InsertCardGeneralGalleryAsync(string userId, string Id)
    {
        var checkResult = await _cardGeneralsService.IsCardGeneralDeletedOrInactiveAsync(Id);
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
        
        var insertResult = await _cardGeneralsGalleryRepository.InsertCardGeneralGalleryAsync(userId, Id, await _cardGeneralsService.GetCardGeneralByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusCardGeneralGalleryAsync(string userId, string cardGeneralId)
    {
        var checkResult = await _cardGeneralsService.IsCardGeneralDeletedOrInactiveAsync(cardGeneralId);
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

        var updateResult = await _cardGeneralsGalleryRepository.UpdateStatusCardGeneralGalleryAsync(userId, cardGeneralId);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardGeneralsGalleryAsync(string userId)
    {
        var updateResult = await _cardGeneralsGalleryRepository.UpdateBatchStatusCardGeneralsGalleryAsync(userId);

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

    public async Task<CardGenerals> SumPowerCardGeneralsGalleryAsync(string userId)
    {
        return await _cardGeneralsGalleryRepository.SumPowerCardGeneralsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarCardGeneralGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _cardGeneralsService.IsCardGeneralDeletedOrInactiveAsync(Id);
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

        var updateResult = await _cardGeneralsGalleryRepository.UpdateTempStarCardGeneralGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardGeneralGalleryAsync(string userId, string cardGeneralId)
    {
        var checkResult = await _cardGeneralsService.IsCardGeneralDeletedOrInactiveAsync(cardGeneralId);
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

        var updateResult = await _cardGeneralsGalleryRepository.UpdateCurrentStarCardGeneralGalleryAsync(userId, cardGeneralId);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardGeneralsGalleryAsync(string userId)
    {
        var updateResult = await _cardGeneralsGalleryRepository.UpdateBatchCurrentStarCardGeneralsGalleryAsync(userId);

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchCardGeneralsGalleryAsync(string userId, List<CardGenerals> cardGenerals)
    {
        var insertResult = await _cardGeneralsGalleryRepository.InsertBatchCardGeneralsGalleryAsync(userId, cardGenerals);

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

    public async Task<CardGenerals> GetCardGeneralCollectionByIdAsync(string userId, string cardGeneralId)
    {
        var result = await _cardGeneralsGalleryRepository.GetCardGeneralCollectionByIdAsync(userId, cardGeneralId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardGeneralGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _cardGeneralsService.IsCardGeneralDeletedOrInactiveAsync(Id);
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

        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        await _cardGeneralsGalleryRepository.UpdateCardGeneralGalleryPowerAsync(userId, Id, await _service.GetCardGeneralByIdAsync(Id));
        return new InsertOrUpdateResult<bool>{
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }
}
