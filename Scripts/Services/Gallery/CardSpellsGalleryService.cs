using System.Collections.Generic;
using System.Threading.Tasks;

public class CardSpellsGalleryService : ICardSpellsGalleryService
{
    private readonly ICardSpellsGalleryRepository _cardSpellGalleryRepository;

    public CardSpellsGalleryService(ICardSpellsGalleryRepository cardSpellGalleryRepository)
    {
        _cardSpellGalleryRepository = cardSpellGalleryRepository;
    }

    public static CardSpellsGalleryService Create()
    {
        return new CardSpellsGalleryService(new CardSpellsGalleryRepository());
    }

    public async Task<List<CardSpells>> GetCardSpellsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CardSpells> list = await _cardSpellGalleryRepository.GetCardSpellsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSpellsCountAsync(string search, string type, string rare)
    {
        return await _cardSpellGalleryRepository.GetCardSpellsCountAsync(search, type, rare);
    }

    public async Task InsertCardSpellGalleryAsync(string Id)
    {
        ICardSpellsRepository _repository = new CardSpellsRepository();
        CardSpellsService _service = new CardSpellsService(_repository);
        await _cardSpellGalleryRepository.InsertCardSpellGalleryAsync(Id, await _service.GetCardSpellByIdAsync(Id));
    }

    public async Task UpdateStatusCardSpellGalleryAsync(string Id)
    {
        await _cardSpellGalleryRepository.UpdateStatusCardSpellGalleryAsync(Id);
    }

    public async Task<CardSpells> SumPowerCardSpellsGalleryAsync()
    {
        return await _cardSpellGalleryRepository.SumPowerCardSpellsGalleryAsync();
    }

    public async Task UpdateStarCardSpellGalleryAsync(string Id, double star)
    {
        await _cardSpellGalleryRepository.UpdateStarCardSpellGalleryAsync(Id, star);
    }

    public async Task UpdateCardSpellGalleryPowerAsync(string Id)
    {
        ICardSpellsRepository _repository = new CardSpellsRepository();
        CardSpellsService _service = new CardSpellsService(_repository);
        await _cardSpellGalleryRepository.UpdateCardSpellGalleryPowerAsync(Id, await _service.GetCardSpellByIdAsync(Id));
    }
}
