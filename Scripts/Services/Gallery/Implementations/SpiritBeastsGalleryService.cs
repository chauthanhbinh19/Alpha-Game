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

    public async Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _spiritBeastsGalleryRepository.GetSpiritBeastsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsCountAsync(string search, string rare)
    {
        return await _spiritBeastsGalleryRepository.GetSpiritBeastsCountAsync(search, rare);
    }

    public async Task InsertSpiritBeastGalleryAsync(string userId, string Id)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        await _spiritBeastsGalleryRepository.InsertSpiritBeastGalleryAsync(userId, Id, await _service.GetSpiritBeastByIdAsync(Id));
    }

    public async Task UpdateStatusSpiritBeastGalleryAsync(string userId, string Id)
    {
        await _spiritBeastsGalleryRepository.UpdateStatusSpiritBeastGalleryAsync(userId, Id);
    }

    public async Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync(string userId)
    {
        return await _spiritBeastsGalleryRepository.SumPowerSpiritBeastsGalleryAsync(userId);
    }

    public async Task UpdateStarSpiritBeastGalleryAsync(string userId, string Id, double star)
    {
        await _spiritBeastsGalleryRepository.UpdateStarSpiritBeastGalleryAsync(userId, Id, star);
    }

    public async Task UpdateSpiritBeastGalleryPowerAsync(string userId, string Id)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        await _spiritBeastsGalleryRepository.UpdateSpiritBeastGalleryPowerAsync(userId, Id, await _service.GetSpiritBeastByIdAsync(Id));
    }
}
