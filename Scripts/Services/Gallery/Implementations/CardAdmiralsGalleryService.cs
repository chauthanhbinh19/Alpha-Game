using System.Collections.Generic;
using System.Threading.Tasks;

public class CardAdmiralsGalleryService : ICardAdmiralsGalleryService
{
    private static CardAdmiralsGalleryService _instance;
    private readonly ICardAdmiralsGalleryRepository _cardAdmiralsGalleryRepository;

    public CardAdmiralsGalleryService(ICardAdmiralsGalleryRepository cardAdmiralsGalleryRepository)
    {
        _cardAdmiralsGalleryRepository = cardAdmiralsGalleryRepository;
    }

    public static CardAdmiralsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardAdmiralsGalleryService(new CardAdmiralsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardAdmirals>> GetCardAdmiralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardAdmirals> list = await _cardAdmiralsGalleryRepository.GetCardAdmiralsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare)
    {
        return await _cardAdmiralsGalleryRepository.GetCardAdmiralsCountAsync(search, type, rare);
    }

    public async Task InsertCardAdmiralGalleryAsync(string userId, string Id)
    {
        ICardAdmiralsRepository _repository = new CardAdmiralsRepository();
        CardAdmiralsService _service = new CardAdmiralsService(_repository);
        await _cardAdmiralsGalleryRepository.InsertCardAdmiralGalleryAsync(userId, Id, await _service.GetCardAdmiralByIdAsync(Id));
    }

    public async Task UpdateStatusCardAdmiralGalleryAsync(string userId, string Id)
    {
        await _cardAdmiralsGalleryRepository.UpdateStatusCardAdmiralGalleryAsync(userId, Id);
    }

    public async Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync(string userId)
    {
        return await _cardAdmiralsGalleryRepository.SumPowerCardAdmiralsGalleryAsync(userId);
    }

    public async Task UpdateStarCardAdmiralGalleryAsync(string userId, string Id, double star)
    {
        await _cardAdmiralsGalleryRepository.UpdateStarCardAdmiralGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCardAdmiralGalleryPowerAsync(string userId, string Id)
    {
        ICardAdmiralsRepository _repository = new CardAdmiralsRepository();
        CardAdmiralsService _service = new CardAdmiralsService(_repository);
        await _cardAdmiralsGalleryRepository.UpdateCardAdmiralGalleryPowerAsync(userId, Id, await _service.GetCardAdmiralByIdAsync(Id));
    }
}
