using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritCardsGalleryService : ISpiritCardsGalleryService
{
    private static SpiritCardsGalleryService _instance;
    private readonly ISpiritCardsGalleryRepository _spiritCardsGalleryRepository;

    public SpiritCardsGalleryService(ISpiritCardsGalleryRepository spiritCardsGalleryRepository)
    {
        _spiritCardsGalleryRepository = spiritCardsGalleryRepository;
    }

    public static SpiritCardsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new SpiritCardsGalleryService(new SpiritCardsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _spiritCardsGalleryRepository.GetSpiritCardsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritCardsCountAsync(string search, string type, string rare)
    {
        return await _spiritCardsGalleryRepository.GetSpiritCardsCountAsync(search, type, rare);
    }

    public async Task InsertSpiritCardGalleryAsync(string Id)
    {
        ISpiritCardsRepository _repository = new SpiritCardsRepository();
        SpiritCardsService _service = new SpiritCardsService(_repository);
        await _spiritCardsGalleryRepository.InsertSpiritCardGalleryAsync(Id, await _service.GetSpiritCardByIdAsync(Id));
    }

    public async Task UpdateStatusSpiritCardGalleryAsync(string Id)
    {
        await _spiritCardsGalleryRepository.UpdateStatusSpiritCardGalleryAsync(Id);
    }

    public async Task<SpiritCards> SumPowerSpiritCardsGalleryAsync()
    {
        return await _spiritCardsGalleryRepository.SumPowerSpiritCardsGalleryAsync();
    }

    public async Task UpdateStarSpiritCardGalleryAsync(string Id, double star)
    {
        await _spiritCardsGalleryRepository.UpdateStarSpiritCardGalleryAsync(Id, star);
    }

    public async Task UpdateSpiritCardGalleryPowerAsync(string Id)
    {
        ISpiritCardsRepository _repository = new SpiritCardsRepository();
        SpiritCardsService _service = new SpiritCardsService(_repository);
        await _spiritCardsGalleryRepository.UpdateSpiritCardGalleryPowerAsync(Id, await _service.GetSpiritCardByIdAsync(Id));
    }
}
