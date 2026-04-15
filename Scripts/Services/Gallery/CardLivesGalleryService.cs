using System.Collections.Generic;
using System.Threading.Tasks;

public class CardLivesGalleryService : ICardLivesGalleryService
{
    private static CardLivesGalleryService _instance;
    private readonly ICardLivesGalleryRepository _cardLifeGalleryRepository;

    public CardLivesGalleryService(ICardLivesGalleryRepository cardLifeGalleryRepository)
    {
        _cardLifeGalleryRepository = cardLifeGalleryRepository;
    }

    public static CardLivesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardLivesGalleryService(new CardLivesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardLives>> GetCardLivesCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = await _cardLifeGalleryRepository.GetCardLivesCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardLivesCountAsync(string search, string type, string rare)
    {
        return await _cardLifeGalleryRepository.GetCardLivesCountAsync(search, type, rare);
    }

    public async Task InsertCardLifeGalleryAsync(string Id)
    {
        ICardLivesRepository _repository = new CardLivesRepository();
        CardLivesService _service = new CardLivesService(_repository);
        await _cardLifeGalleryRepository.InsertCardLifeGalleryAsync(Id, await _service.GetCardLifeByIdAsync(Id));
    }

    public async Task UpdateStatusCardLifeGalleryAsync(string Id)
    {
        await _cardLifeGalleryRepository.UpdateStatusCardLifeGalleryAsync(Id);
    }

    public async Task<CardLives> SumPowerCardLivesGalleryAsync()
    {
        return await _cardLifeGalleryRepository.SumPowerCardLivesGalleryAsync();
    }

    public async Task UpdateStarCardLifeGalleryAsync(string Id, double star)
    {
        await _cardLifeGalleryRepository.UpdateStarCardLifeGalleryAsync(Id, star);
    }

    public async Task UpdateCardLifeGalleryPowerAsync(string Id)
    {
        ICardLivesRepository _repository = new CardLivesRepository();
        CardLivesService _service = new CardLivesService(_repository);
        await _cardLifeGalleryRepository.UpdateCardLifeGalleryPowerAsync(Id, await _service.GetCardLifeByIdAsync(Id));
    }
}
