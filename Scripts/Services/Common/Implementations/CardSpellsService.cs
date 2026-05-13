using System.Collections.Generic;
using System.Threading.Tasks;

public class CardSpellsService : ICardSpellsService
{
    private static CardSpellsService _instance;
    private readonly ICardSpellsRepository _cardSpellsRepository;

    public CardSpellsService(ICardSpellsRepository cardSpellsRepository)
    {
        _cardSpellsRepository = cardSpellsRepository;
    }

    public static CardSpellsService Create()
    {
        if (_instance == null)
        {
            _instance = new CardSpellsService(new CardSpellsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueCardSpellsTypesAsync()
    {
        return await _cardSpellsRepository.GetUniqueCardSpellsTypesAsync();
    }

    public async Task<List<CardSpells>> GetCardSpellsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardSpells> list = await _cardSpellsRepository.GetCardSpellsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSpellsCountAsync(string search, string type, string rare)
    {
        return await _cardSpellsRepository.GetCardSpellsCountAsync(search, type, rare);
    }

    public async Task<List<CardSpells>> GetCardSpellsRandomAsync(string type, int pageSize)
    {
        return await _cardSpellsRepository.GetCardSpellsRandomAsync(type, pageSize);
    }

    public async Task<List<CardSpells>> GetAllCardSpellsAsync(string type)
    {
        return await _cardSpellsRepository.GetAllCardSpellsAsync(type);
    }

    public async Task<CardSpells> GetCardSpellByIdAsync(string Id)
    {
        return await _cardSpellsRepository.GetCardSpellByIdAsync(Id);
    }

    public async Task<List<CardSpells>> GetCardSpellsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardSpells> list = await _cardSpellsRepository.GetCardSpellsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSpellsWithPriceCountAsync(string type)
    {
        return await _cardSpellsRepository.GetCardSpellsWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardSpellsIdAsync()
    {
        return await _cardSpellsRepository.GetUniqueCardSpellsIdAsync();
    }

    public async Task<List<CardSpells>> GetCardSpellsWithoutLimitAsync()
    {
        return await _cardSpellsRepository.GetCardSpellsWithoutLimitAsync();
    }
}
