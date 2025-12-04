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

    public async Task<List<Cards>> GetCardsAsync(int pageSize, int offset, string rare)
    {
        List<Cards> list = await _CardsRepository.GetCardsAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardsCountAsync(string rare)
    {
        return await _CardsRepository.GetCardsCountAsync(rare);
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
