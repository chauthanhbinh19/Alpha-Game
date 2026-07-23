using System.Collections.Generic;
using System.Threading.Tasks;

public class CardGeneralsGalleryService : ICardGeneralsGalleryService
{
    private static CardGeneralsGalleryService _instance;
    private readonly ICardGeneralsGalleryRepository _cardGeneralsGalleryRepository;

    public CardGeneralsGalleryService(ICardGeneralsGalleryRepository cardGeneralsGalleryRepository)
    {
        _cardGeneralsGalleryRepository = cardGeneralsGalleryRepository;
    }

    public static CardGeneralsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardGeneralsGalleryService(new CardGeneralsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardGenerals> list = await _cardGeneralsGalleryRepository.GetCardGeneralsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardGeneralsCountAsync(string search, string type, string rare)
    {
        return await _cardGeneralsGalleryRepository.GetCardGeneralsCountAsync(search, type, rare);
    }

    public async Task InsertCardGeneralGalleryAsync(string userId, string Id)
    {
        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        await _cardGeneralsGalleryRepository.InsertCardGeneralGalleryAsync(userId, Id, await _service.GetCardGeneralByIdAsync(Id));
    }

    public async Task UpdateStatusCardGeneralGalleryAsync(string userId, string Id)
    {
        await _cardGeneralsGalleryRepository.UpdateStatusCardGeneralGalleryAsync(userId, Id);
    }

    public async Task<CardGenerals> SumPowerCardGeneralsGalleryAsync(string userId)
    {
        return await _cardGeneralsGalleryRepository.SumPowerCardGeneralsGalleryAsync(userId);
    }

    public async Task UpdateStarCardGeneralGalleryAsync(string userId, string Id, double star)
    {
        await _cardGeneralsGalleryRepository.UpdateStarCardGeneralGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCardGeneralGalleryPowerAsync(string userId, string Id)
    {
        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        await _cardGeneralsGalleryRepository.UpdateCardGeneralGalleryPowerAsync(userId, Id, await _service.GetCardGeneralByIdAsync(Id));
    }
}
