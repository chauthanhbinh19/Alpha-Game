using System.Collections.Generic;
using System.Threading.Tasks;

public class CardMilitariesService : ICardMilitariesService
{
    private readonly ICardMilitariesRepository _cardMilitaryRepository;

    public CardMilitariesService(ICardMilitariesRepository cardMilitaryRepository)
    {
        _cardMilitaryRepository = cardMilitaryRepository;
    }

    public static CardMilitariesService Create()
    {
        return new CardMilitariesService(new CardMilitariesRepository());
    }

    public async Task<List<string>> GetUniqueCardMilitariesTypesAsync()
    {
        return await _cardMilitaryRepository.GetUniqueCardMilitariesTypesAsync();
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesAsync(string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> list = await _cardMilitaryRepository.GetCardMilitariesAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMilitariesCountAsync(string type, string rare)
    {
        return await _cardMilitaryRepository.GetCardMilitariesCountAsync(type, rare);
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesRandomAsync(string type, int pageSize)
    {
        return await _cardMilitaryRepository.GetCardMilitariesRandomAsync(type, pageSize);
    }

    public async Task<List<CardMilitaries>> GetAllCardMilitariesAsync(string type)
    {
        return await _cardMilitaryRepository.GetAllCardMilitariesAsync(type);
    }

    public async Task<CardMilitaries> GetCardMilitaryByIdAsync(string Id)
    {
        return await _cardMilitaryRepository.GetCardMilitaryByIdAsync(Id);
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardMilitaries> list = await _cardMilitaryRepository.GetCardMilitariesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMilitariesWithPriceCountAsync(string type)
    {
        return await _cardMilitaryRepository.GetCardMilitariesWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardMilitariesIdAsync()
    {
        return await _cardMilitaryRepository.GetUniqueCardMilitariesIdAsync();
    }
}