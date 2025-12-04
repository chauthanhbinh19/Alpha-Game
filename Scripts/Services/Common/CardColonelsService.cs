using System.Collections.Generic;
using System.Threading.Tasks;

public class CardColonelsService : ICardColonelsService
{
    private readonly ICardColonelsRepository _cardColonelsRepository;

    public CardColonelsService(ICardColonelsRepository cardColonelsRepository)
    {
        _cardColonelsRepository = cardColonelsRepository;
    }

    public static CardColonelsService Create()
    {
        return new CardColonelsService(new CardColonelsRepository());
    }

    public async Task<List<string>> GetUniqueCardColonelsTypesAsync()
    {
        return await _cardColonelsRepository.GetUniqueCardColonelsTypesAsync();
    }

    public async Task<List<CardColonels>> GetCardColonelsAsync(string type, int pageSize, int offset, string rare)
    {
        List<CardColonels> list = await _cardColonelsRepository.GetCardColonelsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardColonelsCountAsync(string type, string rare)
    {
        return await _cardColonelsRepository.GetCardColonelsCountAsync(type, rare);
    }

    public async Task<List<CardColonels>> GetCardColonelsRandomAsync(string type, int pageSize)
    {
        return await _cardColonelsRepository.GetCardColonelsRandomAsync(type, pageSize);
    }

    public async Task<List<CardColonels>> GetAllCardColonelsAsync(string type)
    {
        return await _cardColonelsRepository.GetAllCardColonelsAsync(type);
    }

    public async Task<CardColonels> GetCardColonelByIdAsync(string Id)
    {
        return await _cardColonelsRepository.GetCardColonelByIdAsync(Id);
    }

    public async Task<List<CardColonels>> GetCardColonelsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardColonels> list = await _cardColonelsRepository.GetCardColonelsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardColonelsWithPriceCountAsync(string type)
    {
        return await _cardColonelsRepository.GetCardColonelsWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardColonelsIdAsync()
    {
        return await _cardColonelsRepository.GetUniqueCardColonelsIdAsync();
    }
}