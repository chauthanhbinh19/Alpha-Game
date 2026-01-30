using System.Collections.Generic;
using System.Threading.Tasks;

public class CardSpellsService : ICardSpellsService
{
    private readonly ICardSpellsRepository _cardSpellRepository;

    public CardSpellsService(ICardSpellsRepository cardSpellRepository)
    {
        _cardSpellRepository = cardSpellRepository;
    }

    public static CardSpellsService Create()
    {
        return new CardSpellsService(new CardSpellsRepository());
    }

    public async Task<List<string>> GetUniqueCardSpellsTypesAsync()
    {
        return await _cardSpellRepository.GetUniqueCardSpellsTypesAsync();
    }

    public async Task<List<CardSpells>> GetCardSpellsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardSpells> list = await _cardSpellRepository.GetCardSpellsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSpellsCountAsync(string search, string type, string rare)
    {
        return await _cardSpellRepository.GetCardSpellsCountAsync(search, type, rare);
    }

    public async Task<List<CardSpells>> GetCardSpellsRandomAsync(string type, int pageSize)
    {
        return await _cardSpellRepository.GetCardSpellsRandomAsync(type, pageSize);
    }

    public async Task<List<CardSpells>> GetAllCardSpellsAsync(string type)
    {
        return await _cardSpellRepository.GetAllCardSpellsAsync(type);
    }

    public async Task<CardSpells> GetCardSpellByIdAsync(string Id)
    {
        return await _cardSpellRepository.GetCardSpellByIdAsync(Id);
    }

    public async Task<List<CardSpells>> GetCardSpellsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardSpells> list = await _cardSpellRepository.GetCardSpellsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSpellsWithPriceCountAsync(string type)
    {
        return await _cardSpellRepository.GetCardSpellsWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardSpellsIdAsync()
    {
        return await _cardSpellRepository.GetUniqueCardSpellsIdAsync();
    }
}
