using System.Collections.Generic;
using System.Threading.Tasks;

public class CardMonstersGalleryService : ICardMonstersGalleryService
{
    private static CardMonstersGalleryService _instance;
    private readonly ICardMonstersGalleryRepository _cardMonstersGalleryRepository;

    public CardMonstersGalleryService(ICardMonstersGalleryRepository cardMonstersGalleryRepository)
    {
        _cardMonstersGalleryRepository = cardMonstersGalleryRepository;
    }

    public static CardMonstersGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardMonstersGalleryService(new CardMonstersGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMonsters> list = await _cardMonstersGalleryRepository.GetCardMonstersCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMonstersCountAsync(string search, string type, string rare)
    {
        return await _cardMonstersGalleryRepository.GetCardMonstersCountAsync(search, type, rare);
    }

    public async Task InsertCardMonsterGalleryAsync(string Id)
    {
        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        await _cardMonstersGalleryRepository.InsertCardMonsterGalleryAsync(Id, await _service.GetCardMonsterByIdAsync(Id));
    }

    public async Task UpdateStatusCardMonsterGalleryAsync(string Id)
    {
        await _cardMonstersGalleryRepository.UpdateStatusCardMonsterGalleryAsync(Id);
    }

    public async Task<CardMonsters> SumPowerCardMonstersGalleryAsync()
    {
        return await _cardMonstersGalleryRepository.SumPowerCardMonstersGalleryAsync();
    }

    public async Task UpdateStarCardMonsterGalleryAsync(string Id, double star)
    {
        await _cardMonstersGalleryRepository.UpdateStarCardMonsterGalleryAsync(Id, star);
    }

    public async Task UpdateCardMonsterGalleryPowerAsync(string Id)
    {
        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        await _cardMonstersGalleryRepository.UpdateCardMonsterGalleryPowerAsync(Id, await _service.GetCardMonsterByIdAsync(Id));
    }
}
