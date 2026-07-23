using System.Collections.Generic;
using System.Threading.Tasks;

public class CardColonelsGalleryService : ICardColonelsGalleryService
{
    private static CardColonelsGalleryService _instance;
    private readonly ICardColonelsGalleryRepository _cardColonelsGalleryRepository;

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

    public async Task<List<CardColonels>> GetCardColonelsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardColonels> list = await _cardColonelsGalleryRepository.GetCardColonelsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardColonelsCountAsync(string search, string type, string rare)
    {
        return await _cardColonelsGalleryRepository.GetCardColonelsCountAsync(search, type, rare);
    }

    public async Task InsertCardColonelGalleryAsync(string userId, string Id)
    {
        ICardColonelsRepository _repository = new CardColonelsRepository();
        CardColonelsService _service = new CardColonelsService(_repository);
        await _cardColonelsGalleryRepository.InsertCardColonelGalleryAsync(userId, Id, await _service.GetCardColonelByIdAsync(Id));
    }

    public async Task UpdateStatusCardColonelGalleryAsync(string userId, string Id)
    {
        await _cardColonelsGalleryRepository.UpdateStatusCardColonelGalleryAsync(userId, Id);
    }

    public async Task<CardColonels> SumPowerCardColonelsGalleryAsync(string userId)
    {
        return await _cardColonelsGalleryRepository.SumPowerCardColonelsGalleryAsync(userId);
    }

    public async Task UpdateStarCardColonelGalleryAsync(string userId, string Id, double star)
    {
        await _cardColonelsGalleryRepository.UpdateStarCardColonelGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCardColonelGalleryPowerAsync(string userId, string Id)
    {
        ICardColonelsRepository _repository = new CardColonelsRepository();
        CardColonelsService _service = new CardColonelsService(_repository);
        await _cardColonelsGalleryRepository.UpdateCardColonelGalleryPowerAsync(userId, Id, await _service.GetCardColonelByIdAsync(Id));
    }
}
