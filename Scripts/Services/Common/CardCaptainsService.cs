using System.Collections.Generic;
using System.Threading.Tasks;

public class CardCaptainsService : ICardCaptainsService
{
    private readonly ICardCaptainsRepository _cardCaptainsRepository;

    public CardCaptainsService(ICardCaptainsRepository cardCaptainsRepository)
    {
        _cardCaptainsRepository = cardCaptainsRepository;
    }

    public static CardCaptainsService Create()
    {
        return new CardCaptainsService(new CardCaptainsRepository());
    }

    public async Task<List<string>> GetUniqueCardCaptainsTypesAsync()
    {
        return await _cardCaptainsRepository.GetUniqueCardCaptainsTypesAsync();
    }

    public async Task<List<CardCaptains>> GetCardCaptainsAsync(string type, int pageSize, int offset, string rare)
    {
        List<CardCaptains> list = await _cardCaptainsRepository.GetCardCaptainsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardCaptainsCountAsync(string type, string rare)
    {
        return await _cardCaptainsRepository.GetCardCaptainsCountAsync(type, rare);
    }

    public async Task<List<CardCaptains>> GetCardCaptainsRandomAsync(string type, int pageSize)
    {
        return await _cardCaptainsRepository.GetCardCaptainsRandomAsync(type, pageSize);
    }

    public async Task<List<CardCaptains>> GetAllCardCaptainsAsync(string type)
    {
        return await _cardCaptainsRepository.GetAllCardCaptainsAsync(type);
    }

    public async Task<CardCaptains> GetCardCaptainByIdAsync(string Id)
    {
        return await _cardCaptainsRepository.GetCardCaptainByIdAsync(Id);
    }

    public async Task<List<CardCaptains>> GetCardCaptainsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardCaptains> list = await _cardCaptainsRepository.GetCardCaptainsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardCaptainsWithPriceCountAsync(string type)
    {
        return await _cardCaptainsRepository.GetCardCaptainsWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardCaptainsIdAsync()
    {
        return await _cardCaptainsRepository.GetUniqueCardCaptainsIdAsync();
    }
}