using System.Collections.Generic;
using System.Threading.Tasks;

public class CardMonstersService : ICardMonstersService
{
    private readonly ICardMonstersRepository _cardMonstersRepository;

    public CardMonstersService(ICardMonstersRepository cardMonstersRepository)
    {
        _cardMonstersRepository = cardMonstersRepository;
    }

    public static CardMonstersService Create()
    {
        return new CardMonstersService(new CardMonstersRepository());
    }

    public async Task<List<string>> GetUniqueCardMonstersTypesAsync()
    {
        return await _cardMonstersRepository.GetUniqueCardMonstersTypesAsync();
    }

    public async Task<List<CardMonsters>> GetCardMonstersAsync(string type, int pageSize, int offset, string rare)
    {
        List<CardMonsters> list = await _cardMonstersRepository.GetCardMonstersAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMonstersCountAsync(string type, string rare)
    {
        return await _cardMonstersRepository.GetCardMonstersCountAsync(type, rare);
    }

    public async Task<List<CardMonsters>> GetCardMonstersRandomAsync(string type, int pageSize)
    {
        return await _cardMonstersRepository.GetCardMonstersRandomAsync(type, pageSize);
    }

    public async Task<List<CardMonsters>> GetAllCardMonstersAsync(string type)
    {
        return await _cardMonstersRepository.GetAllCardMonstersAsync(type);
    }

    public async Task<CardMonsters> GetCardMonsterByIdAsync(string Id)
    {
        return await _cardMonstersRepository.GetCardMonsterByIdAsync(Id);
    }

    public async Task<List<CardMonsters>> GetCardMonstersWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardMonsters> list = await _cardMonstersRepository.GetCardMonstersWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMonstersWithPriceCountAsync(string type)
    {
        return await _cardMonstersRepository.GetCardMonstersWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardMonstersIdAsync()
    {
        return await _cardMonstersRepository.GetUniqueCardMonstersIdAsync();
    }
}
