using System.Collections.Generic;
using System.Threading.Tasks;

public class CardMilitariesGalleryService : ICardMilitariesGallerService
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

    public async Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> list = await _cardMilitariesGalleryRepository.GetCardMilitariesCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMilitariesCountAsync(string search, string type, string rare)
    {
        return await _cardMilitariesGalleryRepository.GetCardMilitariesCountAsync(search, type, rare);
    }

    public async Task InsertCardMilitaryGalleryAsync(string Id)
    {
        ICardMilitariesRepository _repository = new CardMilitariesRepository();
        CardMilitariesService _service = new CardMilitariesService(_repository);
        await _cardMilitariesGalleryRepository.InsertCardMilitaryGalleryAsync(Id, await _service.GetCardMilitaryByIdAsync(Id));
    }

    public async Task UpdateStatusCardMilitaryGalleryAsync(string Id)
    {
        await _cardMilitariesGalleryRepository.UpdateStatusCardMilitaryGalleryAsync(Id);
    }

    public async Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync()
    {
        return await _cardMilitariesGalleryRepository.SumPowerCardMilitariesGalleryAsync();
    }

    public async Task UpdateStarCardMilitaryGalleryAsync(string Id, double star)
    {
        await _cardMilitariesGalleryRepository.UpdateStarCardMilitaryGalleryAsync(Id, star);
    }

    public async Task UpdateCardMilitaryGalleryPowerAsync(string Id)
    {
        ICardMilitariesRepository _repository = new CardMilitariesRepository();
        CardMilitariesService _service = new CardMilitariesService(_repository);
        await _cardMilitariesGalleryRepository.UpdateCardMilitaryGalleryPowerAsync(Id, await _service.GetCardMilitaryByIdAsync(Id));
    }
}
