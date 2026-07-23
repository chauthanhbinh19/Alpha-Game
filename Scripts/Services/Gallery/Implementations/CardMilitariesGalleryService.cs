using System.Collections.Generic;
using System.Threading.Tasks;

public class CardMilitariesGalleryService : ICardMilitariesGalleryService
{
    private static CardMilitariesGalleryService _instance;
    private readonly ICardMilitariesGalleryRepository _cardMilitariesGalleryRepository;

    public CardMilitariesGalleryService(ICardMilitariesGalleryRepository cardMilitariesGalleryRepository)
    {
        _cardMilitariesGalleryRepository = cardMilitariesGalleryRepository;
    }

    public static CardMilitariesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardMilitariesGalleryService(new CardMilitariesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> list = await _cardMilitariesGalleryRepository.GetCardMilitariesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMilitariesCountAsync(string search, string type, string rare)
    {
        return await _cardMilitariesGalleryRepository.GetCardMilitariesCountAsync(search, type, rare);
    }

    public async Task InsertCardMilitaryGalleryAsync(string userId, string Id)
    {
        ICardMilitariesRepository _repository = new CardMilitariesRepository();
        CardMilitariesService _service = new CardMilitariesService(_repository);
        await _cardMilitariesGalleryRepository.InsertCardMilitaryGalleryAsync(userId, Id, await _service.GetCardMilitaryByIdAsync(Id));
    }

    public async Task UpdateStatusCardMilitaryGalleryAsync(string userId, string Id)
    {
        await _cardMilitariesGalleryRepository.UpdateStatusCardMilitaryGalleryAsync(userId, Id);
    }

    public async Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync(string userId)
    {
        return await _cardMilitariesGalleryRepository.SumPowerCardMilitariesGalleryAsync(userId);
    }

    public async Task UpdateStarCardMilitaryGalleryAsync(string userId, string Id, double star)
    {
        await _cardMilitariesGalleryRepository.UpdateStarCardMilitaryGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCardMilitaryGalleryPowerAsync(string userId, string Id)
    {
        ICardMilitariesRepository _repository = new CardMilitariesRepository();
        CardMilitariesService _service = new CardMilitariesService(_repository);
        await _cardMilitariesGalleryRepository.UpdateCardMilitaryGalleryPowerAsync(userId, Id, await _service.GetCardMilitaryByIdAsync(Id));
    }
}
