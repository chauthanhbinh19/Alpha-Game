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

    public async Task<List<CardMonsters>> GetCardMonstersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMonsters> list = await _cardMonstersGalleryRepository.GetCardMonstersCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMonstersCountAsync(string search, string type, string rare)
    {
        return await _cardMonstersGalleryRepository.GetCardMonstersCountAsync(search, type, rare);
    }

    public async Task InsertCardMonsterGalleryAsync(string userId, string Id)
    {
        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        await _cardMonstersGalleryRepository.InsertCardMonsterGalleryAsync(userId, Id, await _service.GetCardMonsterByIdAsync(Id));
    }

    public async Task UpdateStatusCardMonsterGalleryAsync(string userId, string Id)
    {
        await _cardMonstersGalleryRepository.UpdateStatusCardMonsterGalleryAsync(userId, Id);
    }

    public async Task<CardMonsters> SumPowerCardMonstersGalleryAsync(string userId)
    {
        return await _cardMonstersGalleryRepository.SumPowerCardMonstersGalleryAsync(userId);
    }

    public async Task UpdateStarCardMonsterGalleryAsync(string userId, string Id, double star)
    {
        await _cardMonstersGalleryRepository.UpdateStarCardMonsterGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCardMonsterGalleryPowerAsync(string userId, string Id)
    {
        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        await _cardMonstersGalleryRepository.UpdateCardMonsterGalleryPowerAsync(userId, Id, await _service.GetCardMonsterByIdAsync(Id));
    }
}
