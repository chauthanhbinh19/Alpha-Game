using System.Collections.Generic;
using System.Threading.Tasks;

public class CardColonelsService : ICardColonelsService
{
    private readonly ICardColonelsRepository _cardColonelsRepository;

    public CardColonelsService(ICardColonelsRepository cardColonelsRepository)
    {
        _cardColonelsRepository = cardColonelsRepository;
    }

    public static ICardColonelsService Create() => ServiceContainer.GetService<ICardColonelsService>();

    public async Task<List<string>> GetUniqueCardColonelsTypesAsync()
    {
        return await _cardColonelsRepository.GetUniqueCardColonelsTypesAsync();
    }

    public async Task<List<CardColonels>> GetCardColonelsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardColonels> list = await _cardColonelsRepository.GetCardColonelsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardColonelsCountAsync(string search, string type, string rare)
    {
        return await _cardColonelsRepository.GetCardColonelsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertCardColonelAsync(CardColonels entity)
    {
        var result = await _cardColonelsRepository.InsertCardColonelAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardColonelAsync(CardColonels entity)
    {
        var result = await _cardColonelsRepository.UpdateCardColonelAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<CardColonels>> GetCardColonelsRandomAsync(string type, int pageSize)
    {
        return await _cardColonelsRepository.GetCardColonelsRandomAsync(type, pageSize);
    }

    public async Task<List<CardColonels>> GetAllCardColonelsAsync(string type)
    {
        return await _cardColonelsRepository.GetAllCardColonelsAsync(type);
    }

    public async Task<CardColonels> GetCardColonelByIdAsync(string Id)
    {
        return await _cardColonelsRepository.GetCardColonelByIdAsync(Id);
    }

    public async Task<List<CardColonels>> GetCardColonelsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardColonels> list = await _cardColonelsRepository.GetCardColonelsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardColonelsWithPriceCountAsync(string type)
    {
        return await _cardColonelsRepository.GetCardColonelsWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardColonelsIdAsync()
    {
        return await _cardColonelsRepository.GetUniqueCardColonelsIdAsync();
    }

    public async Task<List<CardColonels>> GetCardColonelsWithoutLimitAsync()
    {
        return await _cardColonelsRepository.GetCardColonelsWithoutLimitAsync();
    }
}