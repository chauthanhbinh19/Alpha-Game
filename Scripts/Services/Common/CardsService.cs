using System.Collections.Generic;
using System.Threading.Tasks;

public class CardsService : ICardsService
{
    private static CardsService _instance;
    private readonly ICardsRepository _cardsRepository;

    public CardsService(ICardsRepository cardRepository)
    {
        _cardsRepository = cardRepository;
    }

    public static CardsService Create()
    {
        if (_instance == null)
        {
            _instance = new CardsService(new CardsRepository());
        }
        return _instance;
    }

    public async Task<List<Cards>> GetCardsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Cards> list = await _cardsRepository.GetCardsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardsCountAsync(string search, string rare)
    {
        return await _cardsRepository.GetCardsCountAsync(search, rare);
    }

    public async Task<List<Cards>> GetCardsWithPriceAsync(int pageSize, int offset)
    {
        List<Cards> list = await _cardsRepository.GetCardsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardsWithPriceCountAsync()
    {
        return await _cardsRepository.GetCardsWithPriceCountAsync();
    }

    public async Task<Cards> GetCardByIdAsync(string Id)
    {
        return await _cardsRepository.GetCardByIdAsync(Id);
    }

    public async Task<Cards> SumPowerCardsPercentAsync()
    {
        return await _cardsRepository.SumPowerCardsPercentAsync();
    }

    public async Task<List<string>> GetUniqueCardsIdAsync()
    {
        return await _cardsRepository.GetUniqueCardsIdAsync();
    }
}
