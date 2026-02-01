using System.Collections.Generic;
using System.Threading.Tasks;

public class CardHeroesService : ICardHeroesService
{
    private static CardHeroesService _instance;
    private readonly ICardHeroesRepository _cardHeroesRepository;

    public CardHeroesService(ICardHeroesRepository cardHeroesRepository)
    {
        _cardHeroesRepository = cardHeroesRepository;
    }

    public static CardHeroesService Create()
    {
        if (_instance == null)
        {
            _instance = new CardHeroesService(new CardHeroesRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueCardHeroesTypesAsync()
    {
        return await _cardHeroesRepository.GetUniqueCardHeroesTypesAsync();
    }

    public async Task<List<CardHeroes>> GetCardHeroesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardHeroes> list = await _cardHeroesRepository.GetCardHeroesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardHeroesCountAsync(string search, string type, string rare)
    {
        return await _cardHeroesRepository.GetCardHeroesCountAsync(search, type, rare);
    }

    public async Task<List<CardHeroes>> GetCardHeroesRandomAsync(string type, int pageSize)
    {
        return await _cardHeroesRepository.GetCardHeroesRandomAsync(type, pageSize);
    }

    public async Task<List<CardHeroes>> GetAllCardHeroesAsync(string type)
    {
        return await _cardHeroesRepository.GetAllCardHeroesAsync(type);
    }

    public async Task<int> GetMaxQuantityAsync(string Id)
    {
        return await _cardHeroesRepository.GetMaxQuantityAsync(Id);
    }

    public async Task<CardHeroes> GetCardHeroByIdAsync(string Id)
    {
        return await _cardHeroesRepository.GetCardHeroByIdAsync(Id);
    }

    public async Task<List<CardHeroes>> GetCardHeroesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardHeroes> list = await _cardHeroesRepository.GetCardHeroesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardHeroesWithPriceCountAsync(string type)
    {
        return await _cardHeroesRepository.GetCardHeroesWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardHeroesIdAsync()
    {
        return await _cardHeroesRepository.GetUniqueCardHeroesIdAsync();
    }
}