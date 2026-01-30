using System.Collections.Generic;
using System.Threading.Tasks;

public class CardsService : ICardsService
{
    private readonly ICardsRepository _CardsRepository;

    public CardsService(ICardsRepository titleRepository)
    {
        _CardsRepository = titleRepository;
    }

    public static CardsService Create()
    {
        return new CardsService(new CardsRepository());
    }

    public async Task<List<Cards>> GetCardsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Cards> list = await _CardsRepository.GetCardsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardsCountAsync(string search, string rare)
    {
        return await _CardsRepository.GetCardsCountAsync(search, rare);
    }

    public async Task<List<Cards>> GetCardsWithPriceAsync(int pageSize, int offset)
    {
        List<Cards> list = await _CardsRepository.GetCardsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardsWithPriceCountAsync()
    {
        return await _CardsRepository.GetCardsWithPriceCountAsync();
    }

    public async Task<Cards> GetCardByIdAsync(string Id)
    {
        return await _CardsRepository.GetCardByIdAsync(Id);
    }

    public async Task<Cards> SumPowerCardsPercentAsync()
    {
        return await _CardsRepository.SumPowerCardsPercentAsync();
    }

    public async Task<List<string>> GetUniqueCardsIdAsync()
    {
        return await _CardsRepository.GetUniqueCardsIdAsync();
    }
}
