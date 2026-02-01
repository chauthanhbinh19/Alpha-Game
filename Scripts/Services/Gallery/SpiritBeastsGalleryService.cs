using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritBeastsGalleryService : ISpiritBeastsGalleryService
{
    private static SpiritBeastsGalleryService _instance;
    private readonly ISpiritBeastsGalleryRepository _spiritBeastsGalleryRepository;

    public SpiritBeastsGalleryService(ISpiritBeastsGalleryRepository spiritBeastsGalleryRepository)
    {
        _spiritBeastsGalleryRepository = spiritBeastsGalleryRepository;
    }

    public static SpiritBeastsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new SpiritBeastsGalleryService(new SpiritBeastsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _spiritBeastsGalleryRepository.GetSpiritBeastsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsCountAsync(string search, string rare)
    {
        return await _spiritBeastsGalleryRepository.GetSpiritBeastsCountAsync(search, rare);
    }

    public async Task InsertSpiritBeastGalleryAsync(string Id)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        await _spiritBeastsGalleryRepository.InsertSpiritBeastGalleryAsync(Id, await _service.GetSpiritBeastByIdAsync(Id));
    }

    public async Task UpdateStatusSpiritBeastGalleryAsync(string Id)
    {
        await _spiritBeastsGalleryRepository.UpdateStatusSpiritBeastGalleryAsync(Id);
    }

    public async Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync()
    {
        return await _spiritBeastsGalleryRepository.SumPowerSpiritBeastsGalleryAsync();
    }

    public async Task UpdateStarSpiritBeastGalleryAsync(string Id, double star)
    {
        await _spiritBeastsGalleryRepository.UpdateStarSpiritBeastGalleryAsync(Id, star);
    }

    public async Task UpdateSpiritBeastGalleryPowerAsync(string Id)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        await _spiritBeastsGalleryRepository.UpdateSpiritBeastGalleryPowerAsync(Id, await _service.GetSpiritBeastByIdAsync(Id));
    }
}
