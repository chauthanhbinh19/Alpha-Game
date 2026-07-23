using System.Collections.Generic;
using System.Threading.Tasks;

public class CardHeroesGalleryService : ICardHeroesGalleryService
{
    private static CardHeroesGalleryService _instance;
    private readonly ICardHeroesGalleryRepository _cardHeroesGalleryRepository;

    public CardHeroesGalleryService(ICardHeroesGalleryRepository cardHeroesGalleryRepository)
    {
        _cardHeroesGalleryRepository = cardHeroesGalleryRepository;
    }

    public static CardHeroesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardHeroesGalleryService(new CardHeroesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardHeroes> list = await _cardHeroesGalleryRepository.GetCardHeroesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardHeroesCountAsync(string search, string type, string rare)
    {
        return await _cardHeroesGalleryRepository.GetCardHeroesCountAsync(search, type, rare);
    }

    public async Task InsertCardHeroGalleryAsync(string userId, string Id)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        await _cardHeroesGalleryRepository.InsertCardHeroGalleryAsync(userId, Id, await _service.GetCardHeroByIdAsync(Id));
    }

    public async Task UpdateStatusCardHeroGalleryAsync(string userId, string Id)
    {
        await _cardHeroesGalleryRepository.UpdateStatusCardHeroGalleryAsync(userId, Id);
    }

    public async Task<CardHeroes> SumPowerCardHeroesGalleryAsync(string userId)
    {
        return await _cardHeroesGalleryRepository.SumPowerCardHeroesGalleryAsync(userId);
    }

    public async Task UpdateStarCardHeroGalleryAsync(string userId, string Id, double star)
    {
        await _cardHeroesGalleryRepository.UpdateStarCardHeroGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCardHeroGalleryPowerAsync(string userId, string Id)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        await _cardHeroesGalleryRepository.UpdateCardHeroGalleryPowerAsync(userId, Id, await _service.GetCardHeroByIdAsync(Id));
    }
}
