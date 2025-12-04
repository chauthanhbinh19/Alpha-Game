using System.Collections.Generic;
using System.Threading.Tasks;

public class CardGeneralsGalleryService : ICardGeneralsGalleryService
{
    private readonly ICardGeneralsGalleryRepository _cardGeneralsGalleryRepository;

    public CardGeneralsGalleryService(ICardGeneralsGalleryRepository cardGeneralsGalleryRepository)
    {
        _cardGeneralsGalleryRepository = cardGeneralsGalleryRepository;
    }

    public static CardGeneralsGalleryService Create()
    {
        return new CardGeneralsGalleryService(new CardGeneralsGalleryRepository());
    }

    public async Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<CardGenerals> list = await _cardGeneralsGalleryRepository.GetCardGeneralsCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardGeneralsCountAsync(string type, string rare)
    {
        return await _cardGeneralsGalleryRepository.GetCardGeneralsCountAsync(type, rare);
    }

    public async Task InsertCardGeneralGalleryAsync(string Id)
    {
        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        await _cardGeneralsGalleryRepository.InsertCardGeneralGalleryAsync(Id, await _service.GetCardGeneralByIdAsync(Id));
    }

    public async Task UpdateStatusCardGeneralGalleryAsync(string Id)
    {
        await _cardGeneralsGalleryRepository.UpdateStatusCardGeneralGalleryAsync(Id);
    }

    public async Task<CardGenerals> SumPowerCardGeneralsGalleryAsync()
    {
        return await _cardGeneralsGalleryRepository.SumPowerCardGeneralsGalleryAsync();
    }

    public async Task UpdateStarCardGeneralGalleryAsync(string Id, double star)
    {
        await _cardGeneralsGalleryRepository.UpdateStarCardGeneralGalleryAsync(Id, star);
    }

    public async Task UpdateCardGeneralGalleryPowerAsync(string Id)
    {
        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        await _cardGeneralsGalleryRepository.UpdateCardGeneralGalleryPowerAsync(Id, await _service.GetCardGeneralByIdAsync(Id));
    }
}
