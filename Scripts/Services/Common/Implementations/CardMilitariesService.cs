using System.Collections.Generic;
using System.Threading.Tasks;

public class CardMilitariesService : ICardMilitariesService
{
    private static CardMilitariesService _instance;
    private readonly ICardMilitariesRepository _cardMilitariesRepository;

    public CardMilitariesService(ICardMilitariesRepository cardMilitariesRepository)
    {
        _cardMilitariesRepository = cardMilitariesRepository;
    }

    public static CardMilitariesService Create()
    {
        if (_instance == null)
        {
            _instance = new CardMilitariesService(new CardMilitariesRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueCardMilitariesTypesAsync()
    {
        return await _cardMilitariesRepository.GetUniqueCardMilitariesTypesAsync();
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardMilitaries> list = await _cardMilitariesRepository.GetCardMilitariesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMilitariesCountAsync(string search, string type, string rare)
    {
        return await _cardMilitariesRepository.GetCardMilitariesCountAsync(search, type, rare);
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesRandomAsync(string type, int pageSize)
    {
        return await _cardMilitariesRepository.GetCardMilitariesRandomAsync(type, pageSize);
    }

    public async Task<List<CardMilitaries>> GetAllCardMilitariesAsync(string type)
    {
        return await _cardMilitariesRepository.GetAllCardMilitariesAsync(type);
    }

    public async Task<CardMilitaries> GetCardMilitaryByIdAsync(string Id)
    {
        return await _cardMilitariesRepository.GetCardMilitaryByIdAsync(Id);
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardMilitaries> list = await _cardMilitariesRepository.GetCardMilitariesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMilitariesWithPriceCountAsync(string type)
    {
        return await _cardMilitariesRepository.GetCardMilitariesWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardMilitariesIdAsync()
    {
        return await _cardMilitariesRepository.GetUniqueCardMilitariesIdAsync();
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesWithoutLimitAsync()
    {
        return await _cardMilitariesRepository.GetCardMilitariesWithoutLimitAsync();
    }
}