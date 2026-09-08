using System.Collections.Generic;
using System.Threading.Tasks;

public class CardSoldiersService : ICardSoldiersService
{
    private readonly ICardSoldiersRepository _cardSoldiersRepository;

    public CardSoldiersService(ICardSoldiersRepository cardSoldiersRepository)
    {
        _cardSoldiersRepository = cardSoldiersRepository;
    }

    public static ICardSoldiersService Create() => ServiceContainer.GetService<ICardSoldiersService>();

    public async Task<List<string>> GetUniqueCardSoldiersTypesAsync()
    {
        return await _cardSoldiersRepository.GetUniqueCardSoldiersTypesAsync();
    }

    public async Task<List<CardSoldiers>> GetCardSoldiersAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardSoldiers> list = await _cardSoldiersRepository.GetCardSoldiersAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSoldiersCountAsync(string search, string type, string rare)
    {
        return await _cardSoldiersRepository.GetCardSoldiersCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertCardSoldierAsync(CardSoldiers entity)
    {
        var result = await _cardSoldiersRepository.InsertCardSoldierAsync(entity);

        if (result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCardSoldierAsync(CardSoldiers entity)
    {
        var result = await _cardSoldiersRepository.UpdateCardSoldierAsync(entity);

        if (result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public Task<bool> IsCardSoldierDeletedOrInactiveAsync(string id)
    {
        return _cardSoldiersRepository.IsCardSoldierDeletedOrInactiveAsync(id);
    }

    public async Task<List<CardSoldiers>> GetCardSoldiersRandomAsync(string type, int pageSize)
    {
        return await _cardSoldiersRepository.GetCardSoldiersRandomAsync(type, pageSize);
    }

    public async Task<List<CardSoldiers>> GetAllCardSoldiersAsync(string type)
    {
        return await _cardSoldiersRepository.GetAllCardSoldiersAsync(type);
    }

    public async Task<CardSoldiers> GetCardSoldierByIdAsync(string Id)
    {
        return await _cardSoldiersRepository.GetCardSoldierByIdAsync(Id);
    }

    public async Task<List<CardSoldiers>> GetCardSoldiersWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardSoldiers> list = await _cardSoldiersRepository.GetCardSoldiersWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSoldiersWithPriceCountAsync(string type)
    {
        return await _cardSoldiersRepository.GetCardSoldiersWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardSoldiersIdAsync()
    {
        return await _cardSoldiersRepository.GetUniqueCardSoldiersIdAsync();
    }

    public async Task<List<CardSoldiers>> GetCardSoldiersWithoutLimitAsync()
    {
        return await _cardSoldiersRepository.GetCardSoldiersWithoutLimitAsync();
    }
}