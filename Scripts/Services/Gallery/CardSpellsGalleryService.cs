using System.Collections.Generic;
using System.Threading.Tasks;

public class CardSpellsGalleryService : ICardSpellsGalleryService
{
    private static CardSpellsGalleryService _instance;
    private readonly ICardSpellsGalleryRepository _cardSpellsGalleryRepository;

    public CardSpellsGalleryService(ICardSpellsGalleryRepository cardSpellsGalleryRepository)
    {
        _cardSpellsGalleryRepository = cardSpellsGalleryRepository;
    }

    public static CardSpellsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardSpellsGalleryService(new CardSpellsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardSpells>> GetCardSpellsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CardSpells> list = await _cardSpellsGalleryRepository.GetCardSpellsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSpellsCountAsync(string search, string type, string rare)
    {
        return await _cardSpellsGalleryRepository.GetCardSpellsCountAsync(search, type, rare);
    }

    public async Task InsertCardSpellGalleryAsync(string Id)
    {
        ICardSpellsRepository _repository = new CardSpellsRepository();
        CardSpellsService _service = new CardSpellsService(_repository);
        await _cardSpellsGalleryRepository.InsertCardSpellGalleryAsync(Id, await _service.GetCardSpellByIdAsync(Id));
    }

    public async Task UpdateStatusCardSpellGalleryAsync(string Id)
    {
        await _cardSpellsGalleryRepository.UpdateStatusCardSpellGalleryAsync(Id);
    }

    public async Task<CardSpells> SumPowerCardSpellsGalleryAsync()
    {
        return await _cardSpellsGalleryRepository.SumPowerCardSpellsGalleryAsync();
    }

    public async Task UpdateStarCardSpellGalleryAsync(string Id, double star)
    {
        await _cardSpellsGalleryRepository.UpdateStarCardSpellGalleryAsync(Id, star);
    }

    public async Task UpdateCardSpellGalleryPowerAsync(string Id)
    {
        ICardSpellsRepository _repository = new CardSpellsRepository();
        CardSpellsService _service = new CardSpellsService(_repository);
        await _cardSpellsGalleryRepository.UpdateCardSpellGalleryPowerAsync(Id, await _service.GetCardSpellByIdAsync(Id));
    }
}
