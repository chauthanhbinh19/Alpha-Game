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

    public async Task<List<CardSpells>> GetCardSpellsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardSpells> list = await _cardSpellsGalleryRepository.GetCardSpellsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSpellsCountAsync(string search, string type, string rare)
    {
        return await _cardSpellsGalleryRepository.GetCardSpellsCountAsync(search, type, rare);
    }

    public async Task InsertCardSpellGalleryAsync(string userId, string Id)
    {
        ICardSpellsRepository _repository = new CardSpellsRepository();
        CardSpellsService _service = new CardSpellsService(_repository);
        await _cardSpellsGalleryRepository.InsertCardSpellGalleryAsync(userId, Id, await _service.GetCardSpellByIdAsync(Id));
    }

    public async Task UpdateStatusCardSpellGalleryAsync(string userId, string Id)
    {
        await _cardSpellsGalleryRepository.UpdateStatusCardSpellGalleryAsync(userId, Id);
    }

    public async Task<CardSpells> SumPowerCardSpellsGalleryAsync(string userId)
    {
        return await _cardSpellsGalleryRepository.SumPowerCardSpellsGalleryAsync(userId);
    }

    public async Task UpdateStarCardSpellGalleryAsync(string userId, string Id, double star)
    {
        await _cardSpellsGalleryRepository.UpdateStarCardSpellGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCardSpellGalleryPowerAsync(string userId, string Id)
    {
        ICardSpellsRepository _repository = new CardSpellsRepository();
        CardSpellsService _service = new CardSpellsService(_repository);
        await _cardSpellsGalleryRepository.UpdateCardSpellGalleryPowerAsync(userId, Id, await _service.GetCardSpellByIdAsync(Id));
    }
}
