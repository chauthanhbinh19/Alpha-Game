using System.Collections.Generic;
using System.Threading.Tasks;

public class CardsGalleryService : ICardsGalleryService
{
    private readonly ICardsGalleryRepository _CardsGalleryRepository;

    public CardsGalleryService(ICardsGalleryRepository CardsGalleryRepository)
    {
        _CardsGalleryRepository = CardsGalleryRepository;
    }

    public static CardsGalleryService Create()
    {
        return new CardsGalleryService(new CardsGalleryRepository());
    }

    public async Task<List<Cards>> GetCardsCollectionAsync(int pageSize, int offset, string rare)
    {
        List<Cards> list = await _CardsGalleryRepository.GetCardsCollectionAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardsCountAsync(string rare)
    {
        return await _CardsGalleryRepository.GetCardsCountAsync(rare);
    }

    public async Task InsertCardGalleryAsync(string Id)
    {
        ICardsRepository _repository = new CardsRepository();
        CardsService _service = new CardsService(_repository);
        await _CardsGalleryRepository.InsertCardGalleryAsync(Id, await _service.GetCardByIdAsync(Id));
    }

    public async Task UpdateStatusCardGalleryAsync(string Id)
    {
        await _CardsGalleryRepository.UpdateStatusCardGalleryAsync(Id);
    }

    public async Task<Cards> SumPowerCardsGalleryAsync()
    {
        return await _CardsGalleryRepository.SumPowerCardsGalleryAsync();
    }

    public async Task UpdateStarCardGalleryAsync(string Id, double star)
    {
        await _CardsGalleryRepository.UpdateStarCardGalleryAsync(Id, star);
    }

    public async Task UpdateCardGalleryPowerAsync(string Id)
    {
        ICardsRepository _repository = new CardsRepository();
        CardsService _service = new CardsService(_repository);
        await _CardsGalleryRepository.UpdateCardGalleryPowerAsync(Id, await _service.GetCardByIdAsync(Id));
    }
}
