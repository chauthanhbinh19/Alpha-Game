using System.Collections.Generic;
using System.Threading.Tasks;

public class CardColonelsGalleryService : ICardColonelsGalleryService
{
    private static CardColonelsGalleryService _instance;
    private ICardColonelsGalleryRepository _cardColonelsGalleryRepository;

    public CardColonelsGalleryService(ICardColonelsGalleryRepository cardColonelsGalleryRepository)
    {
        _cardColonelsGalleryRepository = cardColonelsGalleryRepository;
    }

    public static CardColonelsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardColonelsGalleryService(new CardColonelsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardColonels>> GetCardColonelsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CardColonels> list = await _cardColonelsGalleryRepository.GetCardColonelsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardColonelsCountAsync(string search, string type, string rare)
    {
        return await _cardColonelsGalleryRepository.GetCardColonelsCountAsync(search, type, rare);
    }

    public async Task InsertCardColonelGalleryAsync(string Id)
    {
        ICardColonelsRepository _repository = new CardColonelsRepository();
        CardColonelsService _service = new CardColonelsService(_repository);
        await _cardColonelsGalleryRepository.InsertCardColonelGalleryAsync(Id, await _service.GetCardColonelByIdAsync(Id));
    }

    public async Task UpdateStatusCardColonelGalleryAsync(string Id)
    {
        await _cardColonelsGalleryRepository.UpdateStatusCardColonelGalleryAsync(Id);
    }

    public async Task<CardColonels> SumPowerCardColonelsGalleryAsync()
    {
        return await _cardColonelsGalleryRepository.SumPowerCardColonelsGalleryAsync();
    }

    public async Task UpdateStarCardColonelGalleryAsync(string Id, double star)
    {
        await _cardColonelsGalleryRepository.UpdateStarCardColonelGalleryAsync(Id, star);
    }

    public async Task UpdateCardColonelGalleryPowerAsync(string Id)
    {
        ICardColonelsRepository _repository = new CardColonelsRepository();
        CardColonelsService _service = new CardColonelsService(_repository);
        await _cardColonelsGalleryRepository.UpdateCardColonelGalleryPowerAsync(Id, await _service.GetCardColonelByIdAsync(Id));
    }
}
