using System.Collections.Generic;
using System.Threading.Tasks;

public class CardHeroesGalleryService : ICardHeroesGalleryService
{
    private readonly ICardHeroesGalleryRepository _cardHeroesGalleryRepository;

    public CardHeroesGalleryService(ICardHeroesGalleryRepository cardHeroesGalleryRepository)
    {
        _cardHeroesGalleryRepository = cardHeroesGalleryRepository;
    }

    public static CardHeroesGalleryService Create()
    {
        return new CardHeroesGalleryService(new CardHeroesGalleryRepository());
    }

    public async Task<List<CardHeroes>> GetCardHeroesCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<CardHeroes> list = await _cardHeroesGalleryRepository.GetCardHeroesCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardHeroesCountAsync(string type, string rare)
    {
        return await _cardHeroesGalleryRepository.GetCardHeroesCountAsync(type, rare);
    }

    public async Task InsertCardHeroGalleryAsync(string Id)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        await _cardHeroesGalleryRepository.InsertCardHeroGalleryAsync(Id, await _service.GetCardHeroByIdAsync(Id));
    }

    public async Task UpdateStatusCardHeroGalleryAsync(string Id)
    {
        await _cardHeroesGalleryRepository.UpdateStatusCardHeroGalleryAsync(Id);
    }

    public async Task<CardHeroes> SumPowerCardHeroesGalleryAsync()
    {
        return await _cardHeroesGalleryRepository.SumPowerCardHeroesGalleryAsync();
    }

    public async Task UpdateStarCardHeroGalleryAsync(string Id, double star)
    {
        await _cardHeroesGalleryRepository.UpdateStarCardHeroGalleryAsync(Id, star);
    }

    public async Task UpdateCardHeroGalleryPowerAsync(string Id)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        await _cardHeroesGalleryRepository.UpdateCardHeroGalleryPowerAsync(Id, await _service.GetCardHeroByIdAsync(Id));
    }
}
