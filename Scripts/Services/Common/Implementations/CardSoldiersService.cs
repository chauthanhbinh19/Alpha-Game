using System.Collections.Generic;
using System.Threading.Tasks;

public class CardSoldiersService : ICardSoldiersService
{
    private static CardSoldiersService _instance;
    private readonly ICardSoldiersRepository _cardAdmiralsRepository;

    public CardSoldiersService(ICardSoldiersRepository cardAdmiralsRepository)
    {
        _cardAdmiralsRepository = cardAdmiralsRepository;
    }

    public static CardSoldiersService Create()
    {
        if (_instance == null)
        {
            _instance = new CardSoldiersService(new CardSoldiersRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueCardSoldiersTypesAsync()
    {
        return await _cardAdmiralsRepository.GetUniqueCardSoldiersTypesAsync();
    }

    public async Task<List<CardSoldiers>> GetCardSoldiersAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardSoldiers> list = await _cardAdmiralsRepository.GetCardSoldiersAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSoldiersCountAsync(string search, string type, string rare)
    {
        return await _cardAdmiralsRepository.GetCardSoldiersCountAsync(search, type, rare);
    }

    public async Task<List<CardSoldiers>> GetCardSoldiersRandomAsync(string type, int pageSize)
    {
        return await _cardAdmiralsRepository.GetCardSoldiersRandomAsync(type, pageSize);
    }

    public async Task<List<CardSoldiers>> GetAllCardSoldiersAsync(string type)
    {
        return await _cardAdmiralsRepository.GetAllCardSoldiersAsync(type);
    }

    public async Task<CardSoldiers> GetCardSoldierByIdAsync(string Id)
    {
        return await _cardAdmiralsRepository.GetCardSoldierByIdAsync(Id);
    }

    public async Task<List<CardSoldiers>> GetCardSoldiersWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardSoldiers> list = await _cardAdmiralsRepository.GetCardSoldiersWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSoldiersWithPriceCountAsync(string type)
    {
        return await _cardAdmiralsRepository.GetCardSoldiersWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardSoldiersIdAsync()
    {
        return await _cardAdmiralsRepository.GetUniqueCardSoldiersIdAsync();
    }
}