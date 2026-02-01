using System.Collections.Generic;
using System.Threading.Tasks;

public class CardCaptainsGalleryService : ICardCaptainsGalleryService
{
    private static CardCaptainsGalleryService _instance;
    private ICardCaptainsGalleryRepository _cardCaptainsGalleryRepository;

    public CardCaptainsGalleryService(ICardCaptainsGalleryRepository cardCaptainsGalleryRepository)
    {
        _cardCaptainsGalleryRepository = cardCaptainsGalleryRepository;
    }

    public static CardCaptainsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardCaptainsGalleryService(new CardCaptainsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CardCaptains> list = await _cardCaptainsGalleryRepository.GetCardCaptainsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardCaptainsCountAsync(string search, string type, string rare)
    {
        return await _cardCaptainsGalleryRepository.GetCardCaptainsCountAsync(search, type, rare);
    }

    public async Task InsertCardCaptainGalleryAsync(string Id)
    {
        ICardCaptainsRepository _repository = new CardCaptainsRepository();
        CardCaptainsService _service = new CardCaptainsService(_repository);
        await _cardCaptainsGalleryRepository.InsertCardCaptainGalleryAsync(Id, await _service.GetCardCaptainByIdAsync(Id));
    }

    public async Task UpdateStatusCardCaptainGalleryAsync(string Id)
    {
        await _cardCaptainsGalleryRepository.UpdateStatusCardCaptainGalleryAsync(Id);
    }

    public async Task<CardCaptains> SumPowerCardCaptainsGalleryAsync()
    {
        return await _cardCaptainsGalleryRepository.SumPowerCardCaptainsGalleryAsync();
    }

    public async Task UpdateStarCardCaptainGalleryAsync(string Id, double star)
    {
        await _cardCaptainsGalleryRepository.UpdateStarCardCaptainGalleryAsync(Id, star);
    }

    public async Task UpdateCardCaptainGalleryPowerAsync(string Id)
    {
        ICardCaptainsRepository _repository = new CardCaptainsRepository();
        CardCaptainsService _service = new CardCaptainsService(_repository);
        await _cardCaptainsGalleryRepository.UpdateCardCaptainGalleryPowerAsync(Id, await _service.GetCardCaptainByIdAsync(Id));
    }
}
