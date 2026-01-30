using System.Collections.Generic;
using System.Threading.Tasks;

public class CardMilitariesGalleryService : ICardMilitariesGallerService
{
    private readonly ICardMilitariesGalleryRepository _cardMilitaryGalleryRepository;

    public CardMilitariesGalleryService(ICardMilitariesGalleryRepository cardMilitaryGalleryRepository)
    {
        _cardMilitaryGalleryRepository = cardMilitaryGalleryRepository;
    }

    public static CardMilitariesGalleryService Create()
    {
        return new CardMilitariesGalleryService(new CardMilitariesGalleryRepository());
    }

    public async Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> list = await _cardMilitaryGalleryRepository.GetCardMilitariesCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardMilitariesCountAsync(string search, string type, string rare)
    {
        return await _cardMilitaryGalleryRepository.GetCardMilitariesCountAsync(search, type, rare);
    }

    public async Task InsertCardMilitaryGalleryAsync(string Id)
    {
        ICardMilitariesRepository _repository = new CardMilitariesRepository();
        CardMilitariesService _service = new CardMilitariesService(_repository);
        await _cardMilitaryGalleryRepository.InsertCardMilitaryGalleryAsync(Id, await _service.GetCardMilitaryByIdAsync(Id));
    }

    public async Task UpdateStatusCardMilitaryGalleryAsync(string Id)
    {
        await _cardMilitaryGalleryRepository.UpdateStatusCardMilitaryGalleryAsync(Id);
    }

    public async Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync()
    {
        return await _cardMilitaryGalleryRepository.SumPowerCardMilitariesGalleryAsync();
    }

    public async Task UpdateStarCardMilitaryGalleryAsync(string Id, double star)
    {
        await _cardMilitaryGalleryRepository.UpdateStarCardMilitaryGalleryAsync(Id, star);
    }

    public async Task UpdateCardMilitaryGalleryPowerAsync(string Id)
    {
        ICardMilitariesRepository _repository = new CardMilitariesRepository();
        CardMilitariesService _service = new CardMilitariesService(_repository);
        await _cardMilitaryGalleryRepository.UpdateCardMilitaryGalleryPowerAsync(Id, await _service.GetCardMilitaryByIdAsync(Id));
    }
}
