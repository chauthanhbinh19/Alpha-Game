using System.Collections.Generic;
using System.Threading.Tasks;

public class CardCaptainsService : ICardCaptainsService
{
    private readonly ICardCaptainsRepository _cardCaptainsRepository;

    public CardCaptainsService(ICardCaptainsRepository cardCaptainsRepository)
    {
        _cardCaptainsRepository = cardCaptainsRepository;
    }

    public static ICardCaptainsService Create() => ServiceContainer.GetService<ICardCaptainsService>();

    public async Task<List<string>> GetUniqueCardCaptainsTypesAsync()
    {
        return await _cardCaptainsRepository.GetUniqueCardCaptainsTypesAsync();
    }

    public async Task<List<CardCaptains>> GetCardCaptainsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardCaptains> list = await _cardCaptainsRepository.GetCardCaptainsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardCaptainsCountAsync(string search, string type, string rare)
    {
        return await _cardCaptainsRepository.GetCardCaptainsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertCardCaptainAsync(CardCaptains entity)
    {
        var result = await _cardCaptainsRepository.InsertCardCaptainAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardCaptainAsync(CardCaptains entity)
    {
        var result = await _cardCaptainsRepository.UpdateCardCaptainAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<CardCaptains>> GetCardCaptainsRandomAsync(string type, int pageSize)
    {
        return await _cardCaptainsRepository.GetCardCaptainsRandomAsync(type, pageSize);
    }

    public async Task<List<CardCaptains>> GetAllCardCaptainsAsync(string type)
    {
        return await _cardCaptainsRepository.GetAllCardCaptainsAsync(type);
    }

    public async Task<CardCaptains> GetCardCaptainByIdAsync(string Id)
    {
        return await _cardCaptainsRepository.GetCardCaptainByIdAsync(Id);
    }

    public async Task<List<CardCaptains>> GetCardCaptainsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardCaptains> list = await _cardCaptainsRepository.GetCardCaptainsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardCaptainsWithPriceCountAsync(string type)
    {
        return await _cardCaptainsRepository.GetCardCaptainsWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardCaptainsIdAsync()
    {
        return await _cardCaptainsRepository.GetUniqueCardCaptainsIdAsync();
    }

    public async Task<List<CardCaptains>> GetCardCaptainsWithoutLimitAsync()
    {
        return await _cardCaptainsRepository.GetCardCaptainsWithoutLimitAsync();
    }
}