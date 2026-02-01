using System.Collections.Generic;
using System.Threading.Tasks;

public class CardGeneralsService : ICardGeneralsService
{
    private static CardGeneralsService _instance;
    private readonly ICardGeneralsRepository _cardGeneralsRepository;

    public CardGeneralsService(ICardGeneralsRepository cardGeneralsRepository)
    {
        _cardGeneralsRepository = cardGeneralsRepository;
    }

    public static CardGeneralsService Create()
    {
        if (_instance == null)
        {
            _instance = new CardGeneralsService(new CardGeneralsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueCardGeneralsTypesAsync()
    {
        return await _cardGeneralsRepository.GetUniqueCardGeneralsTypesAsync();
    }

    public async Task<List<CardGenerals>> GetCardGeneralsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardGenerals> list = await _cardGeneralsRepository.GetCardGeneralsAsync(search, rare, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardGeneralsCountAsync(string search, string type, string rare)
    {
        return await _cardGeneralsRepository.GetCardGeneralsCountAsync(search, type, rare);
    }

    public async Task<List<CardGenerals>> GetCardGeneralsRandomAsync(string type, int pageSize)
    {
        return await _cardGeneralsRepository.GetCardGeneralsRandomAsync(type, pageSize);
    }

    public async Task<List<CardGenerals>> GetAllCardGeneralsAsync(string type)
    {
        return await _cardGeneralsRepository.GetAllCardGeneralsAsync(type);
    }

    public async Task<CardGenerals> GetCardGeneralByIdAsync(string Id)
    {
        return await _cardGeneralsRepository.GetCardGeneralByIdAsync(Id);
    }

    public async Task<List<CardGenerals>> GetCardGeneralsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardGenerals> list = await _cardGeneralsRepository.GetCardGeneralsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardGeneralsWithPriceCountAsync(string type)
    {
        return await _cardGeneralsRepository.GetCardGeneralsWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardGeneralsIdAsync()
    {
        return await _cardGeneralsRepository.GetUniqueCardGeneralsIdAsync();
    }
}