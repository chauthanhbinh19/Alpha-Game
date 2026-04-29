using System.Collections.Generic;
using System.Threading.Tasks;

public class CardAdmiralsService : ICardAdmiralsService
{
    private static CardAdmiralsService _instance;
    private readonly ICardAdmiralsRepository _cardAdmiralsRepository;

    public CardAdmiralsService(ICardAdmiralsRepository cardAdmiralsRepository)
    {
        _cardAdmiralsRepository = cardAdmiralsRepository;
    }

    public static CardAdmiralsService Create()
    {
        if (_instance == null)
        {
            _instance = new CardAdmiralsService(new CardAdmiralsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueCardAdmiralsTypesAsync()
    {
        return await _cardAdmiralsRepository.GetUniqueCardAdmiralsTypesAsync();
    }

    public async Task<List<CardAdmirals>> GetCardAdmiralsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardAdmirals> list = await _cardAdmiralsRepository.GetCardAdmiralsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare)
    {
        return await _cardAdmiralsRepository.GetCardAdmiralsCountAsync(search, type, rare);
    }

    public async Task<List<CardAdmirals>> GetCardAdmiralsRandomAsync(string type, int pageSize)
    {
        return await _cardAdmiralsRepository.GetCardAdmiralsRandomAsync(type, pageSize);
    }

    public async Task<List<CardAdmirals>> GetAllCardAdmiralsAsync(string type)
    {
        return await _cardAdmiralsRepository.GetAllCardAdmiralsAsync(type);
    }

    public async Task<CardAdmirals> GetCardAdmiralByIdAsync(string Id)
    {
        return await _cardAdmiralsRepository.GetCardAdmiralByIdAsync(Id);
    }

    public async Task<List<CardAdmirals>> GetCardAdmiralsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardAdmirals> list = await _cardAdmiralsRepository.GetCardAdmiralsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardAdmiralsWithPriceCountAsync(string type)
    {
        return await _cardAdmiralsRepository.GetCardAdmiralsWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueCardAdmiralsIdAsync()
    {
        return await _cardAdmiralsRepository.GetUniqueCardAdmiralsIdAsync();
    }
}