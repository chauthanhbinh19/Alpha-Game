using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritCardsGalleryService : ISpiritCardsGalleryService
{
    private readonly ISpiritCardsGalleryRepository _SpiritCardGalleryRepository;

    public SpiritCardsGalleryService(ISpiritCardsGalleryRepository SpiritCardGalleryRepository)
    {
        _SpiritCardGalleryRepository = SpiritCardGalleryRepository;
    }

    public static SpiritCardsGalleryService Create()
    {
        return new SpiritCardsGalleryService(new SpiritCardsGalleryRepository());
    }

    public async Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _SpiritCardGalleryRepository.GetSpiritCardsCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritCardsCountAsync(string type, string rare)
    {
        return await _SpiritCardGalleryRepository.GetSpiritCardsCountAsync(type, rare);
    }

    public async Task InsertSpiritCardGalleryAsync(string Id)
    {
        ISpiritCardsRepository _repository = new SpiritCardsRepository();
        SpiritCardsService _service = new SpiritCardsService(_repository);
        await _SpiritCardGalleryRepository.InsertSpiritCardGalleryAsync(Id, await _service.GetSpiritCardByIdAsync(Id));
    }

    public async Task UpdateStatusSpiritCardGalleryAsync(string Id)
    {
        await _SpiritCardGalleryRepository.UpdateStatusSpiritCardGalleryAsync(Id);
    }

    public async Task<SpiritCards> SumPowerSpiritCardsGalleryAsync()
    {
        return await _SpiritCardGalleryRepository.SumPowerSpiritCardsGalleryAsync();
    }

    public async Task UpdateStarSpiritCardGalleryAsync(string Id, double star)
    {
        await _SpiritCardGalleryRepository.UpdateStarSpiritCardGalleryAsync(Id, star);
    }

    public async Task UpdateSpiritCardGalleryPowerAsync(string Id)
    {
        ISpiritCardsRepository _repository = new SpiritCardsRepository();
        SpiritCardsService _service = new SpiritCardsService(_repository);
        await _SpiritCardGalleryRepository.UpdateSpiritCardGalleryPowerAsync(Id, await _service.GetSpiritCardByIdAsync(Id));
    }
}
