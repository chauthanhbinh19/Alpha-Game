using System.Collections.Generic;
using System.Threading.Tasks;

public class CardLivesService : ICardLivesService
{
    private static CardLivesService _instance;
    private readonly ICardLivesRepository _cardLivesRepository;

    public CardLivesService(ICardLivesRepository cardLivesRepository)
    {
        _cardLivesRepository = cardLivesRepository;
    }

    public static CardLivesService Create()
    {
        if (_instance == null)
        {
            _instance = new CardLivesService(new CardLivesRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueCardLivesTypesAsync()
    {
        return await _cardLivesRepository.GetUniqueCardLivesTypesAsync();
    }

    public async Task<List<CardLives>> GetCardLivesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardLives> list = await _cardLivesRepository.GetCardLivesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardLivesCountAsync(string search, string type, string rare)
    {
        return await _cardLivesRepository.GetCardLivesCountAsync(search, type, rare);
    }

    public async Task<List<CardLives>> GetCardLivesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardLives> list = await _cardLivesRepository.GetCardLivesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardLivesWithPriceCountAsync(string type)
    {
        return await _cardLivesRepository.GetCardLivesWithPriceCountAsync(type);
    }

    public async Task<CardLives> GetCardLifeByIdAsync(string Id)
    {
        return await _cardLivesRepository.GetCardLifeByIdAsync(Id);
    }

    public async Task<CardLives> SumPowerCardLivesPercentAsync()
    {
        return await _cardLivesRepository.SumPowerCardLivesPercentAsync();
    }

    public async Task<List<string>> GetUniqueCardLivesIdAsync()
    {
        return await _cardLivesRepository.GetUniqueCardLivesIdAsync();
    }

    public async Task<List<CardLives>> GetCardLivesWithoutLimitAsync()
    {
        return await _cardLivesRepository.GetCardLivesWithoutLimitAsync();
    }
}