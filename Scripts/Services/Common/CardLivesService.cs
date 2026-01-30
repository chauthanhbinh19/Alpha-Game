using System.Collections.Generic;
using System.Threading.Tasks;

public class CardLivesService : ICardLivesService
{
    private readonly ICardLivesRepository _cardLifeRepository;

    public CardLivesService(ICardLivesRepository cardLifeRepository)
    {
        _cardLifeRepository = cardLifeRepository;
    }

    public static CardLivesService Create()
    {
        return new CardLivesService(new CardLivesRepository());
    }

    public async Task<List<string>> GetUniqueCardLivesTypesAsync()
    {
        return await _cardLifeRepository.GetUniqueCardLivesTypesAsync();
    }

    public async Task<List<CardLives>> GetCardLivesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardLives> list = await _cardLifeRepository.GetCardLivesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardLivesCountAsync(string search, string type, string rare)
    {
        return await _cardLifeRepository.GetCardLivesCountAsync(search, type, rare);
    }

    public async Task<List<CardLives>> GetCardLivesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardLives> list = await _cardLifeRepository.GetCardLivesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardLivesWithPriceCountAsync(string type)
    {
        return await _cardLifeRepository.GetCardLivesWithPriceCountAsync(type);
    }

    public async Task<CardLives> GetCardLifeByIdAsync(string Id)
    {
        return await _cardLifeRepository.GetCardLifeByIdAsync(Id);
    }

    public async Task<CardLives> SumPowerCardLivesPercentAsync()
    {
        return await _cardLifeRepository.SumPowerCardLivesPercentAsync();
    }

    public async Task<List<string>> GetUniqueCardLivesIdAsync()
    {
        return await _cardLifeRepository.GetUniqueCardLivesIdAsync();
    }
}